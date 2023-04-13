using MediatR;
using Saiketsu.Service.Election.Application.Candidates.Commands.DeleteCandidate;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Candidates;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers.Candidates;

public sealed class CandidateDeletedIntegrationEventHandler : IRequestHandler<CandidateDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CandidateDeletedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CandidateDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new DeleteCandidateCommand
        {
            CandidateId = request.Id
        };

        await _mediator.Send(command, cancellationToken);
    }
}