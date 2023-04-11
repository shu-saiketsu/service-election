using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;

namespace Saiketsu.Service.Election.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<DeleteCandidateCommand> _validator;

    public DeleteCandidateCommandHandler(IApplicationDbContext context, IValidator<DeleteCandidateCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidate =
            await _context.Candidates.SingleOrDefaultAsync(x => x.Id == request.CandidateId, cancellationToken);
        if (candidate == null) return false;

        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}