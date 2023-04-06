using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElections;

public sealed class GetElectionsQueryHandler : IRequestHandler<GetElectionsQuery, List<ElectionEntity>>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetElectionsQuery> _validator;

    public GetElectionsQueryHandler(IApplicationDbContext context, IValidator<GetElectionsQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<List<ElectionEntity>> Handle(GetElectionsQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var elections = await _context.Elections
            .Include(x => x.Type)
            .Include(x => x.Owner)
            .ToListAsync(cancellationToken);

        return elections;
    }
}