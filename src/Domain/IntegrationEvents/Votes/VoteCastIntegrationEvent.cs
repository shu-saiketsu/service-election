using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Service.Election.Domain.IntegrationEvents.Votes
{
    public sealed class VoteCastIntegrationEvent : IRequest
    {
        public int ElectionId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
