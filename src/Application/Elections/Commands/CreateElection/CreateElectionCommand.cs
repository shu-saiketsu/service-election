﻿using MediatR;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommand : IRequest<ElectionEntity?>
{
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ElectionType Type { get; set; }
    public string OwnerId { get; set; } = null!;
}