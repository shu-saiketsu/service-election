using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Users.Queries.GetUser;

public sealed class GetUserQuery : IRequest<UserEntity?>
{
    public string UserId { get; set; } = null!;
}