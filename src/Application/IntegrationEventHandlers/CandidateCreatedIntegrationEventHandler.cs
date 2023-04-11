using MediatR;
using Saiketsu.Service.Election.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Service.Election.Domain.IntegrationEvents;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class CandidateCreatedIntegrationEventHandler : IRequestHandler<CandidateCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CandidateCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CandidateCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateCandidateCommand { Id = request.Id, Name = request.Name };
        await _mediator.Send(command, cancellationToken);
    }
}