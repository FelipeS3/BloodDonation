using BloodDonation.Core.Entities;
using BloodDonation.Application.Models;
using BloodDonation.Application.Services;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _service;
    public DonationController(IDonationService service)
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.Get(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult PostDonation(CreateDonationInputModel input)
    {
        var result = _service.Insert(input);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, input);
    }
}
