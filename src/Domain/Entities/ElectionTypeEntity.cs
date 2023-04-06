using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Domain.Entities;

public sealed class ElectionTypeEntity
{
    public ElectionType Id { get; set; }
    public string Name { get; set; } = null!;
}