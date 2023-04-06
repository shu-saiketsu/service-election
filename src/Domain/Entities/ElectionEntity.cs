using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Domain.Entities;

public sealed class ElectionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ElectionTypeEntity Type { get; set; } = null!;
    public ElectionType TypeId { get; set; }

    public string OwnerId { get; set; } = null!;
    public UserEntity Owner { get; set; } = null!;
}