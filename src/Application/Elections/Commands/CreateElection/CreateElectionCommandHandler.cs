﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Commands.CreateElection
{
    public sealed class CreateElectionCommandHandler : IRequestHandler<CreateElectionCommand, ElectionEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<CreateElectionCommand> _validator;

        public CreateElectionCommandHandler(IApplicationDbContext context, IValidator<CreateElectionCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ElectionEntity?> Handle(CreateElectionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var election = new ElectionEntity
            {
                Name = request.Name,
                OwnerId = request.OwnerId,
                TypeId = request.Type
            };

            // save data to database
            await _context.Elections.AddAsync(election, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // return references
            await _context.Entry(election)
                .Reference("Type")
                .LoadAsync(cancellationToken);

            await _context.Entry(election)
                .Reference("Owner")
                .LoadAsync(cancellationToken);

            return election;
        }
    }
}