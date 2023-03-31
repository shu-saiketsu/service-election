﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Service.Election.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Service.Election.Application.Elections.Commands.CreateElection;
using Saiketsu.Service.Election.Application.Elections.Commands.DeleteElection;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElection;
using Saiketsu.Service.Election.Application.Elections.Queries.GetElections;

namespace Saiketsu.Service.Election.WebApi.Controllers
{
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

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserToElection([FromBody] AddUserToElectionCommand command)
        {
            var response = await _mediator.Send(command);

            if (response == false) return BadRequest();

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var message = new GetElectionQuery { Id = id };

            var response = await _mediator.Send(message);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = new DeleteElectionCommand { Id = id };

            var response = await _mediator.Send(message);

            if (response == false) return BadRequest();

            return Ok();
        }
        }
}
