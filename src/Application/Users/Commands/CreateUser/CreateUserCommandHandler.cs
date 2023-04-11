using FluentValidation;
using MediatR;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IApplicationDbContext context, IValidator<CreateUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = new UserEntity
        {
            Id = request.UserId
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}