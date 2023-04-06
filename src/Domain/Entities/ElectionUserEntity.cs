namespace Saiketsu.Service.Election.Domain.Entities;

public sealed class ElectionUserEntity
{
    public int ElectionId { get; set; }
    public ElectionEntity Election { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public bool Voted { get; set; }
}