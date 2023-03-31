using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Infrastructure.Persistence.Configurations
{
    public sealed class ElectionUserEntityConfiguration : IEntityTypeConfiguration<ElectionUserEntity>
    {
        public void Configure(EntityTypeBuilder<ElectionUserEntity> builder)
        {
            builder.ToTable("election_user");

            builder.HasKey(x => new { x.UserId, x.ElectionId });
        }
    }
}
