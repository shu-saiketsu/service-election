namespace Saiketsu.Service.Election.Domain.Entities;

public sealed class ElectionCandidateEntity
{
    public int ElectionId { get; set; }
    public ElectionEntity Election { get; set; } = null!;

    public int CandidateId { get; set; }
    public CandidateEntity Candidate { get; set; } = null!;
}