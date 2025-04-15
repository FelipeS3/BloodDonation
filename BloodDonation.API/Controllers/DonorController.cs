using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonorController : ControllerBase
{
    private readonly BloodDonationDbContext _context;
    public DonorController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var donors = _context.Donors.Include(d=>d.Donations).ToList();

        var model = donors.Select(d => DonorViewModel.FromEntity(d)).ToList();

        if (model.IsNullOrEmpty()) return NotFound("No donors registered yet");

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var donor = _context.Donors.Include(d=>d.Donations).FirstOrDefault(d => d.Id == id);

        if (donor == null) return NotFound("Donor Not Found");

        var model = DonorDetailsViewModel.FromEntity(donor);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult PostDonor(CreateDonorInputViewModel input)
    {
        try
        {
            var donor = input.ToEntity();

            if (_context.Donors.Any(d => d.Email == donor.Email))
            {
                return BadRequest("Email already exist.");
            }

            if (string.IsNullOrEmpty(donor.FullName)) throw new ArgumentException("Full name cannot be empty.", nameof(donor.FullName));

            _context.Donors.Add(donor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = donor.Id }, input);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult PutDonor(int id,DonorUpdateInputModel update)
    {
        var donor = _context.Donors.Find(id);
        if (donor == null) return NotFound("No donors registered yet");

        if(update.Weight < 50) throw new ArgumentException("Minimum weight of 50 kg");

        donor.Update(update.FullName, update.Email, update.Weight);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var donor = _context.Donors.Find(id);

        if (donor == null) return NotFound("No donors registered yet");

        _context.Donors.Remove(donor);
        _context.SaveChanges();

        return NoContent();
    }
}