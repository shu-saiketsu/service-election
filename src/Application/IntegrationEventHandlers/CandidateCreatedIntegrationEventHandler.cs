using MediatR;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.IntegrationEvents;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class CandidateCreatedIntegrationEventHandler : IRequestHandler<CandidateCreatedIntegrationEvent>
{
    private readonly IApplicationDbContext _context;

    public CandidateCreatedIntegrationEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CandidateCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var candidate = new CandidateEntity
        {
            Id = request.Id,
            Name = request.Name
        };

        await _context.Candidates.AddAsync(candidate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}