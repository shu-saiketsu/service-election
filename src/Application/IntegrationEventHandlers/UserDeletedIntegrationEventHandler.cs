using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.IntegrationEvents;

namespace Saiketsu.Service.Election.Application.IntegrationEventHandlers;

public sealed class UserDeletedIntegrationEventHandler : IRequestHandler<UserDeletedIntegrationEvent>
{
    private readonly IApplicationDbContext _context;

    public UserDeletedIntegrationEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UserDeletedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user == null) return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}