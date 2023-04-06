using MediatR;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.IntegrationEvents;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class UserCreatedIntegrationEventHandler : IRequestHandler<UserCreatedIntegrationEvent>
{
    private readonly IApplicationDbContext _context;

    public UserCreatedIntegrationEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UserCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            Id = request.Id
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}