using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;

namespace Saiketsu.Service.Election.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<DeleteUserCommand> _validator;

    public DeleteUserCommandHandler(IApplicationDbContext context, IValidator<DeleteUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}