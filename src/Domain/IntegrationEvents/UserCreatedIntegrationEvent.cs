﻿using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents;

public sealed class UserCreatedIntegrationEvent : IRequest
{
    public string Id { get; set; } = null!;
}