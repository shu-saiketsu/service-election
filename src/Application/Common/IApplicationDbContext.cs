using Saiketsu.Service.Election.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Saiketsu.Service.Election.Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<ElectionEntity> Elections { get; }
        DbSet<ElectionUserEntity> ElectionUsers { get; }
        EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
