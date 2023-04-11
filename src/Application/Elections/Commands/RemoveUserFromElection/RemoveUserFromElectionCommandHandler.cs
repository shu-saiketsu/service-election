using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;

namespace Saiketsu.Service.Election.Application.Elections.Commands.RemoveUserFromElection;

public sealed class RemoveUserFromElectionCommandHandler : IRequestHandler<RemoveUserFromElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveUserFromElectionCommand> _validator;

    public RemoveUserFromElectionCommandHandler(IApplicationDbContext context,
        IValidator<RemoveUserFromElectionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(RemoveUserFromElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var electionUser =
            await _context.ElectionUsers.SingleOrDefaultAsync(
                x => x.UserId == request.UserId && x.ElectionId == request.ElectionId, cancellationToken);
        if (electionUser == null) return false;

        _context.ElectionUsers.Remove(electionUser);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}