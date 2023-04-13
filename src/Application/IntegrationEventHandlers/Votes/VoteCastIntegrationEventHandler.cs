using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Election.Application.Elections.Commands.SetUserVotedInElection;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Votes;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers.Votes
{
    public sealed class VoteCastIntegrationEventHandler : IRequestHandler<VoteCastIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public VoteCastIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(VoteCastIntegrationEvent request, CancellationToken cancellationToken)
        {
            var command = new SetUserVotedInElectionCommand
                { ElectionId = request.ElectionId, UserId = request.UserId };

            await _mediator.Send(command, cancellationToken);
        }
    }
}
