using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents.Users;

public sealed class UserCreatedIntegrationEvent : IRequest
{
    public string Id { get; set; } = null!;
}