using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Elections;

namespace Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandHandler : IRequestHandler<DeleteElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventBus _eventBus;
    private readonly IValidator<DeleteElectionCommand> _validator;

    public DeleteElectionCommandHandler(IApplicationDbContext context, IValidator<DeleteElectionCommand> validator,
        IEventBus eventBus)
    {
        _context = context;
        _validator = validator;
        _eventBus = eventBus;
    }

    public async Task<bool> Handle(DeleteElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _context.Elections.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (election == null) return false;

        _context.Elections.Remove(election);
        await _context.SaveChangesAsync(cancellationToken);

        var @event = new ElectionDeletedIntegrationEvent { Id = request.Id };
        _eventBus.Publish(@event);

        return true;
    }
}