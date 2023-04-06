using MediatR;

namespace Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommand : IRequest<bool>
{
    public int Id { get; set; }
}