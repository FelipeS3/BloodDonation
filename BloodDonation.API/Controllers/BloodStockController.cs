using BloodDonation.Application.Models;
using BloodDonation.Application.Services;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class BloodStockController : ControllerBase
{
    private readonly IBloodStockService _service;
    public BloodStockController(IBloodStockService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("type-blood")]
    public IActionResult Get(string blood = "", string rhFactor = "")
    {
        var result = _service.Get(blood, rhFactor);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

}