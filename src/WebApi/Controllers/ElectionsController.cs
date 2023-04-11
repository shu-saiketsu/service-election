using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Service.Election.Application.Elections.Commands.AddCandidateToElection;
using Saiketsu.Service.Election.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Service.Election.Application.Elections.Commands.CreateElection;
using Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection;
using Saiketsu.Service.Election.Application.Elections.Commands.RemoveCandidateFromElection;
using Saiketsu.Service.Election.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElectionCandidates;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElections;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElectionsForUser;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElectionUsers;

namespace Saiketsu.Service.Election.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ElectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ElectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var message = new GetElectionsQuery();

        var response = await _mediator.Send(message);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateElectionCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == null) return BadRequest();

        return CreatedAtAction(nameof(Get), new { electionId = response.Id }, response);
    }

    [HttpGet("{electionId:int}")]
    public async Task<IActionResult> Get(int electionId)
    {
        var message = new GetElectionQuery { Id = electionId };

        var response = await _mediator.Send(message);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpDelete("{electionId:int}")]
    public async Task<IActionResult> Delete(int electionId)
    {
        var message = new DeleteElectionCommand { Id = electionId };

        var response = await _mediator.Send(message);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpGet("{electionId:int}/users")]
    public async Task<IActionResult> GetElectionUsers(int electionId)
    {
        var message = new GetElectionUsersQuery
        {
            ElectionId = electionId
        };

        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserElections(string userId, [FromQuery] [Required] bool eligible)
    {
        var message = new GetElectionsForUserQuery { UserId = userId, Eligible = eligible };

        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost("{electionId:int}/users/{userId}")]
    public async Task<IActionResult> AddUserToElection(int electionId, string userId)
    {
        var command = new AddUserToElectionCommand { ElectionId = electionId, UserId = userId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpGet("{electionId:int}/candidates")]
    public async Task<IActionResult> GetElectionCandidates(int electionId)
    {
        var message = new GetElectionCandidatesQuery
        {
            ElectionId = electionId
        };

        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost("{electionId:int}/candidates/{candidateId:int}")]
    public async Task<IActionResult> AddCandidateToElection(int electionId, int candidateId)
    {
        var command = new AddCandidateToElectionCommand { ElectionId = electionId, CandidateId = candidateId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpDelete("{electionId:int}/candidates/{candidateId:int}")]
    public async Task<IActionResult> RemoveElectionCandidate(int electionId, int candidateId)
    {
        var message = new RemoveCandidateFromElectionCommand
        {
            ElectionId = electionId,
            CandidateId = candidateId
        };

        var response = await _mediator.Send(message);

        if (!response)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{electionId:int}/users/{userId}")]
    public async Task<IActionResult> RemoveElectionUser(int electionId, string userId)
    {
        var message = new RemoveUserFromElectionCommand
        {
            ElectionId = electionId,
            UserId = userId
        };

        var response = await _mediator.Send(message);

        if (!response)
            return NotFound();

        return Ok(response);
    }
}