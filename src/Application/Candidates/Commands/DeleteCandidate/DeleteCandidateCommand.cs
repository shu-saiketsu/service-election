using MediatR;

namespace Saiketsu.Service.Election.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommand : IRequest<bool>
{
    public int CandidateId { get; set; }
}