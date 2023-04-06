using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElections;

public sealed class GetElectionsQuery : IRequest<List<ElectionEntity>>
{
}