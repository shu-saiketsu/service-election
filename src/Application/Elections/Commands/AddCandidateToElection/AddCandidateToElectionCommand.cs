using MediatR;

namespace Saiketsu.Service.Election.Application.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommand : IRequest<bool>
{
    public int CandidateId { get; set; }
    public int ElectionId { get; set; }
}