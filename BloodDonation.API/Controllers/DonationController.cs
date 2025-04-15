using BloodDonation.Core.Entities;
using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


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

        if (donation.IsNullOrEmpty()) return NotFound("No donations registered yet");

        var donationsView = donation.Select(d => DonationViewModel.FromEntity(d)).ToList();

        return Ok(donationsView);
    }

    [HttpGet("{id}")]
    public IActionResult GetDonation(int id)
    {
        var donation = _context.Donations.Include(d => d.Donor).SingleOrDefault(d => d.Id == id);

        if (donation == null) return NotFound("No donations registered yet");

        var model = DonationViewModel.FromEntity(donation);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult PostDonation(CreateDonationInputModel input)
    {
        try
        {
            var donor = _context.Donors.Find(input.DonorId);

            if (donor == null) throw new ArgumentException("Donor not found.");
            if (donor.Age < 18) throw new ArgumentException("Donor must be at least 18 years old to donate.");
            if (donor.Weight < 50) throw new ArgumentException("Minimum weight must be 50 kilos to donate.");
            if (input.VolumeInML is < 420 or > 470) throw new ArgumentException("Blood donation volume must be between 420ml and 470ml");

            donor.CanDonate();

            var donation = input.ToEntity();
            _context.Donations.Add(donation);

            var bloodStock =
                _context.BloodStocks.FirstOrDefault(x =>
                    x.BloodType == donor.BloodType && x.RhFactor == donor.RhFactor);


            if (bloodStock == null)
            {
                var stock = new BloodStock(donor.BloodType, donor.RhFactor, input.VolumeInML);
                _context.BloodStocks.Add(stock);
            }

            if (bloodStock != null)
            {
                bloodStock.AddVolume(input.VolumeInML);
            }
            

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDonation), new { id = input.DonorId }, input);
        }
        catch (Exception ex)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Detail = $"{ex.Message}"
            });
        }
    }
}
