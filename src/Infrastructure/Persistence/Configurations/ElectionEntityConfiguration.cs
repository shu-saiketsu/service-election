using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Infrastructure.Persistence.Configurations;

public sealed class ElectionEntityConfiguration : IEntityTypeConfiguration<ElectionEntity>
{
    public void Configure(EntityTypeBuilder<ElectionEntity> builder)
    {
        builder.ToTable("election");

        builder.Property(x => x.TypeId).HasConversion<int>();
    }
}