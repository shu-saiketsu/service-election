using MediatR;
using Saiketsu.Service.Election.Domain.Entities;

namespace Saiketsu.Service.Election.Application.Elections.Queries.GetElection;

public sealed class GetElectionQuery : IRequest<ElectionEntity?>
{
    public int Id { get; set; }
}