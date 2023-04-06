using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQueryHandler : IRequestHandler<GetElectionUsersQuery, List<UserEntity>?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<GetElectionUsersQuery> _validator;

    public GetElectionUsersQueryHandler(IApplicationDbContext context, IValidator<GetElectionUsersQuery> validator,
        IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<List<UserEntity>?> Handle(GetElectionUsersQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _mediator.Send(new GetElectionQuery { Id = request.ElectionId }, cancellationToken);
        if (election == null) return null;

        var result = await _context.ElectionUsers
            .Include(x => x.User)
            .Where(x => x.ElectionId == request.ElectionId)
            .ToListAsync(cancellationToken);

        var dto = result.Select(x => x.User).ToList();

        return dto;
    }
}