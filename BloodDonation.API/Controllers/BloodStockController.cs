using BloodDonation.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class BloodStockController : ControllerBase
{
    private readonly BloodDonationDbContext _context;
    public BloodStockController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var donor = _context.Donors.Include(d => d.Donations).ToList();

        var stocks = _context.BloodStocks;
        return Ok(stocks);
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Created();
    }

}