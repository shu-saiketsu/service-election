using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saiketsu.Service.Election.Domain.Enum;

namespace Saiketsu.Service.Election.Domain.Entities
{
    public sealed class ElectionTypeEntity
    {
        public ElectionType Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
