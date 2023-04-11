using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommand : IRequest<CandidateEntity>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}