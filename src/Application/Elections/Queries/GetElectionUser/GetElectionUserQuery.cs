using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUser
{
    public sealed class GetElectionUserQuery : IRequest<ElectionUserEntity?>
    {
        public int ElectionId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
