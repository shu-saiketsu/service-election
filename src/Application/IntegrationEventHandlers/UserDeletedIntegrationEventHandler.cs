using MediatR;
using Saiketsu.Service.Election.Application.Users.Commands.DeleteUser;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Users;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class UserDeletedIntegrationEventHandler : IRequestHandler<UserDeletedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public UserDeletedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { UserId = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}