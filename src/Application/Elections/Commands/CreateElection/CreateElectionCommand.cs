using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Election.Domain.Entities;
using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Application.Elections.Commands.CreateElection
{
    public sealed class CreateElectionCommand : IRequest<ElectionEntity?>
    {
        public string Name { get; set; } = null!;
        public ElectionType Type { get; set; }
        public string OwnerId { get; set; } = null!;
    }
}
