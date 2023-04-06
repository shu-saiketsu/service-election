using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.IntegrationEvents;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class CandidateDeletedIntegrationEventHandler : IRequestHandler<CandidateDeletedIntegrationEvent>
{
    private readonly IApplicationDbContext _context;

    public CandidateDeletedIntegrationEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CandidateDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (candidate == null) return;

        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync(cancellationToken);
    }
}