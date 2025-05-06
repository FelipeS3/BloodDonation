using BloodDonation.Application.Queries.GetAllBloodStock;
using BloodDonation.Application.Queries.GetBloodStockById;
using BloodDonation.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class BloodStockController : ControllerBase
{
    private readonly IMediator _mediator;
    public BloodStockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllBloodStockQuery();

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("type-blood")]
    public async Task<IActionResult> Get(string typeBlood = "", string rhFactor = "")
    {
        var query = new GetBloodStockByIdQuery();

        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

}