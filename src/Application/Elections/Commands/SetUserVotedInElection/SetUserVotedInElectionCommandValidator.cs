using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Saiketsu.Service.Election.Application.Elections.Commands.SetUserVotedInElection
{
    public sealed class SetUserVotedInElectionCommandValidator : AbstractValidator<SetUserVotedInElectionCommand>
    {
    }
}
