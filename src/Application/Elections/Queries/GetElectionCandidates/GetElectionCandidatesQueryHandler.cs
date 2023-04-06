using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionCandidates;

public sealed class
    GetElectionCandidatesQueryHandler : IRequestHandler<GetElectionCandidatesQuery, List<CandidateEntity>?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IValidator<GetElectionCandidatesQuery> _validator;

    public GetElectionCandidatesQueryHandler(IApplicationDbContext context,
        IValidator<GetElectionCandidatesQuery> validator, IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<List<CandidateEntity>?> Handle(GetElectionCandidatesQuery request,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _mediator.Send(new GetElectionQuery { Id = request.ElectionId }, cancellationToken);
        if (election == null) return null;

        var result = await _context.ElectionCandidates
            .Include(x => x.Candidate)
            .Where(x => x.ElectionId == request.ElectionId)
            .ToListAsync(cancellationToken);

        var dto = result.Select(x => x.Candidate).ToList();

        return dto;
    }
}