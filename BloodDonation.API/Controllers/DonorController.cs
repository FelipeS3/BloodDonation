using BloodDonation.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonorController : ControllerBase
{
    private readonly BloodDonationDbContext _context;
    private DonorController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var donations = _context.Donations
            .Where(d => d.DonorId == id)
            .ToList();

        return Ok(donations);
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Created();
    }

    [HttpPut]
    public IActionResult Put()
    {
        return Ok();
    }
}