using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;

namespace Saiketsu.Service.Election.Application.Elections.Commands.RemoveCandidateFromElection;

public sealed class
    RemoveCandidateFromElectionCommandHandler : IRequestHandler<RemoveCandidateFromElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveCandidateFromElectionCommand> _validator;

    public RemoveCandidateFromElectionCommandHandler(IApplicationDbContext context,
        IValidator<RemoveCandidateFromElectionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(RemoveCandidateFromElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var electionCandidate = await _context.ElectionCandidates.SingleOrDefaultAsync(
            x => x.CandidateId == request.CandidateId && x.ElectionId == request.ElectionId, cancellationToken);
        if (electionCandidate == null) return false;

        _context.ElectionCandidates.Remove(electionCandidate);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}