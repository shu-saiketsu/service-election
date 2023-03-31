﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Infrastructure.Persistence.Configurations
{
    public sealed class CandidateEntityConfiguration : IEntityTypeConfiguration<CandidateEntity>
    {
        public void Configure(EntityTypeBuilder<CandidateEntity> builder)
        {
            builder.ToTable("candidate");
        }
    }
}
