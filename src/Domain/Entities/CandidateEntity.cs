using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saiketsu.Service.Election.Domain.Entities
{
    public sealed class CandidateEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
