using MediatR;

namespace Saiketsu.Service.Election.Application.Elections.Commands.RemoveUserFromElection;

public sealed class RemoveUserFromElectionCommand : IRequest<bool>
{
    public int ElectionId { get; set; }
    public string UserId { get; set; } = null!;
}