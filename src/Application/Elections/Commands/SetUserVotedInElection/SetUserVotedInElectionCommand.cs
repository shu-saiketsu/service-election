using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Service.Election.Application.Elections.Commands.SetUserVotedInElection
{
    public sealed class SetUserVotedInElectionCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
