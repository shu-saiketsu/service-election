using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents.Elections;

public sealed class ElectionDeletedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}