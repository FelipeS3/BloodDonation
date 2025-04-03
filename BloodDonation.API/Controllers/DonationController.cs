using BloodDonation.API.Data;
using BloodDonation.API.Entities;
using BloodDonation.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonationController : ControllerBase
{
    private readonly  BloodDonationDbContext _context;
    public DonationController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var donation = _context.Donations.Include(d => d.Donor).ToList();

        var donationsView = donation.Select(d => DonationInputViewModel.FromEntity(d)).ToList();

        return Ok(donationsView);
    }

    [HttpGet("{id}")]
    public IActionResult GetDonation(int id)
    {
        var donation = _context.Donations.Include(d => d.Donor).SingleOrDefault(d => d.Id == id);

        var model = DonationInputViewModel.FromEntity(donation);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult RegisterDonation([FromBody] CreateDonationInputModel input)
    {
        var donation = input.ToEntity();
        
        _context.Donations.Add(donation);
        _context.SaveChanges();
       
        return Ok(new
        {
            Message = "Donation Registred!",
            DonationId = donation.Id
        });
    }
}
