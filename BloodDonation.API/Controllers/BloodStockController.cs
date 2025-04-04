using BloodDonation.API.Data;
using Microsoft.AspNetCore.Mvc;

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


}