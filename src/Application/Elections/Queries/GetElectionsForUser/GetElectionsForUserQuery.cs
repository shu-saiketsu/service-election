using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQuery : IRequest<List<ElectionEntity>?>
{
    public string UserId { get; set; } = null!;
    public bool Eligible { get; set; }
}