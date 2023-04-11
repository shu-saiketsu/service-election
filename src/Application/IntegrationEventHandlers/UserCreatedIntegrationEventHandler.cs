using MediatR;
using Saiketsu.Service.Election.Application.Users.Commands.CreateUser;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Users;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class UserCreatedIntegrationEventHandler : IRequestHandler<UserCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public UserCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand { UserId = request.Id };

        await _mediator.Send(command, cancellationToken);
    }
}