using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUser;

namespace Saiketsu.Service.Election.Application.Elections.Commands.SetUserVotedInElection
{
    public sealed class SetUserVotedInElectionCommandHandler : IRequestHandler<SetUserVotedInElectionCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<SetUserVotedInElectionCommand> _validator;
        private readonly IMediator _mediator;

        public SetUserVotedInElectionCommandHandler(IApplicationDbContext context, IValidator<SetUserVotedInElectionCommand> validator, IMediator mediator)
        {
            _context = context;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<bool> Handle(SetUserVotedInElectionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var query = new GetElectionUserQuery { ElectionId = request.ElectionId, UserId = request.UserId };
            var electionUser = await _mediator.Send(query, cancellationToken);

            if (electionUser == null) return false;

            electionUser.Voted = true;

            _context.ElectionUsers.Update(electionUser);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
