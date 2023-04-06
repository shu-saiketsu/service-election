using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionCandidates;

public sealed class GetElectionCandidatesQuery : IRequest<List<CandidateEntity>?>
{
    public int ElectionId { get; set; }
}