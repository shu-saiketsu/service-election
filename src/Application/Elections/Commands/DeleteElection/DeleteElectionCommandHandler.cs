using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Elections.Commands.CreateElection;

namespace Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection
{
    public sealed class DeleteElectionCommandHandler : IRequestHandler<DeleteElectionCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<DeleteElectionCommand> _validator;

        public DeleteElectionCommandHandler(IApplicationDbContext context, IValidator<DeleteElectionCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteElectionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var election = await _context.Elections.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (election == null) return false;

            _context.Elections.Remove(election);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
