using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQuery : IRequest<List<UserEntity>?>
{
    public int ElectionId { get; set; }
}