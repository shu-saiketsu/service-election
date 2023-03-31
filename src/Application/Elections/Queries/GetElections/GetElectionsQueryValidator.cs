using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElections
{
    public sealed class GetElectionsQueryValidator : AbstractValidator<GetElectionsQuery>
    {
        public GetElectionsQueryValidator()
        {
            
        }
    }
}
