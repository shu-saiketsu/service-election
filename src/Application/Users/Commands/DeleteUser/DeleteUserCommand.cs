using MediatR;

namespace Saiketsu.Service.Election.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommand : IRequest<bool>
{
    public string UserId { get; set; } = null!;
}