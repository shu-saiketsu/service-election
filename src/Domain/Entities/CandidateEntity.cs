namespace Saiketsu.Service.Election.Domain.Entities;

public sealed class CandidateEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}