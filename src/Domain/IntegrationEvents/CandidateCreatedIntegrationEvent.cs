﻿using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents;

public sealed class CandidateCreatedIntegrationEvent : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}