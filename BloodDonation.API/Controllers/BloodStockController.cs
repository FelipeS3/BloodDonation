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
        var stocks = _context.BloodStocks.ToList();

        return Ok(stocks);
    }

    [HttpGet("tipo-sangue")]
    public IActionResult Get()
    {
        var stock = _context.BloodStocks.Include(x=>x.RhFactor).Select(x => x.BloodType);

        return Ok(stock);
    }

}