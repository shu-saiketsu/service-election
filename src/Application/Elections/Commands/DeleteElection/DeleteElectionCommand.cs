using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection
{
    public sealed class DeleteElectionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
