using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Common;

public interface IApplicationDbContext
{
    DbSet<ElectionEntity> Elections { get; }
    DbSet<UserEntity> Users { get; }
    DbSet<CandidateEntity> Candidates { get; }
    DbSet<ElectionUserEntity> ElectionUsers { get; }
    DbSet<ElectionCandidateEntity> ElectionCandidates { get; }
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}