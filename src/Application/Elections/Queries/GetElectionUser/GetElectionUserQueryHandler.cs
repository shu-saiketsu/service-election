using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUser
{
    public sealed class GetElectionUserQueryHandler : IRequestHandler<GetElectionUserQuery, ElectionUserEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<GetElectionUserQuery> _validator;

        public GetElectionUserQueryHandler(IApplicationDbContext context, IValidator<GetElectionUserQuery> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ElectionUserEntity?> Handle(GetElectionUserQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var electionUser = await _context.ElectionUsers.SingleOrDefaultAsync(x =>
                x.ElectionId == request.ElectionId && x.UserId == request.UserId, cancellationToken);

            return electionUser;
        }
    }
}
