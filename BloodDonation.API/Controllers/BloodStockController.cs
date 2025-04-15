using BloodDonation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        var stocks = _context.BloodStocks.ToList();

        if (stocks.IsNullOrEmpty()) return NotFound("Empty stock");

        return Ok(stocks);
    }

    [HttpGet("type-blood")]
    public IActionResult Get(string blood = "", string rhFactor = "")
    {
        var stock = _context.BloodStocks
            .Include(x=>x.RhFactor)
            .Where(x=>x.BloodType == blood || x.RhFactor == rhFactor)
            .Select(x => x.BloodType);

        return Ok(stock);
    }

}