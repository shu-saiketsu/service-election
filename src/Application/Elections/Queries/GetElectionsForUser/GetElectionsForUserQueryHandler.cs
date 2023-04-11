using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Users.Queries.GetUser;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQueryHandler : IRequestHandler<GetElectionsForUserQuery, List<ElectionEntity>?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<GetElectionsForUserQuery> _validator;

    public GetElectionsForUserQueryHandler(IApplicationDbContext context,
        IValidator<GetElectionsForUserQuery> validator, IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<List<ElectionEntity>?> Handle(GetElectionsForUserQuery request,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _mediator.Send(new GetUserQuery { UserId = request.UserId }, cancellationToken);
        if (user == null) return null;

        List<ElectionEntity>? elections;
        if (request.Eligible)
            elections = await GetEligibleUserElections(request.UserId, cancellationToken);
        else
            elections = await GetAllUserElections(request.UserId, cancellationToken);

        return elections;
    }

    private async Task<List<ElectionEntity>?> GetAllUserElections(string userId, CancellationToken cancellationToken)
    {
        var elections = await _context.ElectionUsers
            .Where(x => x.UserId == userId)
            .Select(x => x.Election)
            .ToListAsync(cancellationToken);

        return elections;
    }

    private async Task<List<ElectionEntity>?> GetEligibleUserElections(string userId,
        CancellationToken cancellationToken)
    {
        var elections = await _context.ElectionUsers
            .Where(x => x.UserId == userId && x.Voted == false)
            .Select(x => x.Election)
            .ToListAsync(cancellationToken);

        return elections;
    }
}