using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElection;

public sealed class GetElectionQueryHandler : IRequestHandler<GetElectionQuery, ElectionEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetElectionQuery> _validator;

    public GetElectionQueryHandler(IApplicationDbContext context, IValidator<GetElectionQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<ElectionEntity?> Handle(GetElectionQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _context.Elections
            .Include(x => x.Type)
            .Include(x => x.Owner)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return election;
    }
}