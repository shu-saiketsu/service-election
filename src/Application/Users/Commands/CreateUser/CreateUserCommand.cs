using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommand : IRequest<UserEntity>
{
    public string UserId { get; set; } = null!;
}