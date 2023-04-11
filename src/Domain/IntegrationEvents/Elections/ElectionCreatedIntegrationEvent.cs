using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents.Elections;

public sealed class ElectionCreatedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}