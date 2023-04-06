using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Commands.AddUserToElection;

public sealed class AddUserToElectionCommandHandler : IRequestHandler<AddUserToElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<AddUserToElectionCommand> _validator;

    public AddUserToElectionCommandHandler(IApplicationDbContext context,
        IValidator<AddUserToElectionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(AddUserToElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var result = await _context.ElectionUsers.SingleOrDefaultAsync(x =>
            x.UserId == request.UserId && x.ElectionId == request.ElectionId, cancellationToken);

        if (result != null)
            return false;

        var electionUser = new ElectionUserEntity { ElectionId = request.ElectionId, UserId = request.UserId };

        await _context.ElectionUsers.AddAsync(electionUser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}