using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommandHandler : IRequestHandler<AddCandidateToElectionCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<AddCandidateToElectionCommand> _validator;

    public AddCandidateToElectionCommandHandler(IApplicationDbContext context,
        IValidator<AddCandidateToElectionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(AddCandidateToElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var result = await _context.ElectionCandidates.SingleOrDefaultAsync(x =>
            x.CandidateId == request.CandidateId && x.ElectionId == request.ElectionId, cancellationToken);

        if (result != null)
            return false;

        var electionCandidate = new ElectionCandidateEntity
            { ElectionId = request.ElectionId, CandidateId = request.CandidateId };

        await _context.ElectionCandidates.AddAsync(electionCandidate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}