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

        if (donation == null) return NotFound();

        var donationsView = donation.Select(d => DonationViewModel.FromEntity(d)).ToList();

        return Ok(donationsView);
    }

    [HttpGet("{id}")]
    public IActionResult GetDonation(int id)
    {
        var donation = _context.Donations.Include(d => d.Donor).SingleOrDefault(d => d.Id == id);

        if (donation == null) return NotFound();

        var model = DonationViewModel.FromEntity(donation);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult PostDonation([FromBody] CreateDonationInputModel input)
    {
        try
        {
            var donor = _context.Donors.Find(input.DonorId);

            if (donor == null) return BadRequest("Donor not found.");

            if (donor.Age < 18) throw new ArgumentException("Donor must be at least 18 years old.");

            var donation = input.ToEntity();
            _context.Donations.Add(donation);

            var bloodStock =
                _context.BloodStocks.FirstOrDefault(b =>
                    b.BloodType == donor.BloodType && donor.RhFactor == b.RhFactor);

            if (bloodStock == null)
            {
                bloodStock = new BloodStock(donor.BloodType, donor.RhFactor, input.VolumeInML);
                _context.SaveChanges();
            }
            else
            {
                bloodStock.Update(bloodStock.BloodType,bloodStock.RhFactor,input.VolumeInML);
                _context.BloodStocks.Update(bloodStock);
            }

            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDonation), new { id = donation.Id }, DonationViewModel.FromEntity(donation));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}
