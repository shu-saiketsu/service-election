using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents.Candidates;

public sealed class CandidateDeletedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}