using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Infrastructure.Persistence.Configurations
{
    public sealed class ElectionTypeEntityConfiguration : IEntityTypeConfiguration<ElectionTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ElectionTypeEntity> builder)
        {
            builder.ToTable("election_type");

            var data = Enum.GetValues(typeof(ElectionType)).Cast<ElectionType>().Select(x => new ElectionTypeEntity
            {
                Id = x,
                Name = x.ToString()
            });

            builder.HasData(data);
        }
    }
}
