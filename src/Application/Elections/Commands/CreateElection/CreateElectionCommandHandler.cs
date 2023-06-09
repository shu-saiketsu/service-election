﻿using FluentValidation;
using MediatR;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Elections;

namespace Saiketsu.Service.Election.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandHandler : IRequestHandler<CreateElectionCommand, ElectionEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventBus _eventBus;
    private readonly IValidator<CreateElectionCommand> _validator;

    public CreateElectionCommandHandler(IApplicationDbContext context, IValidator<CreateElectionCommand> validator,
        IEventBus eventBus)
    {
        _context = context;
        _validator = validator;
        _eventBus = eventBus;
    }

    public async Task<ElectionEntity?> Handle(CreateElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        request.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
        request.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);

        var election = new ElectionEntity
        {
            Name = request.Name,
            OwnerId = request.OwnerId,
            TypeId = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        // save data to database
        await _context.Elections.AddAsync(election, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // return references
        await _context.Entry(election)
            .Reference("Type")
            .LoadAsync(cancellationToken);

        await _context.Entry(election)
            .Reference("Owner")
            .LoadAsync(cancellationToken);

        var @event = new ElectionCreatedIntegrationEvent { Id = election.Id };
        _eventBus.Publish(@event);

        return election;
    }
}