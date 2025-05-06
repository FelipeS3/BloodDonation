using BloodDonation.Application.Commands.InsertDonation;
using BloodDonation.Application.Queries.GetAllDonations;
using BloodDonation.Application.Queries.GetDonationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonationController : ControllerBase
{
    private readonly IMediator _mediator;
    public DonationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllDonationsQuery();

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetDonationsByIdQuery(id);

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostDonation(InsertDonationCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        if (command == null)
        {
            return BadRequest("Dados da doação inválidos");
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }
}
