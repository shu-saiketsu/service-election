using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Users.Queries.GetUser;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetUserQuery> _validator;

    public GetUserQueryHandler(IApplicationDbContext context, IValidator<GetUserQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UserEntity?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        return user;
    }
}