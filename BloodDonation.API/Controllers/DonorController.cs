using BloodDonation.API.Data;
using BloodDonation.API.Entities;
using BloodDonation.API.Models;
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
        var donors = _context.Donors.Include(d => d.Donations).ToList();

        return Ok(donors);
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
    public IActionResult Post([FromBody] DonorInputViewModel input)
    {
        var donor = new Donor(input.FullName, input.Email, input.BirthDate, input.Gender, input.Weight,
            input.BloodType, input.RhFactor);

        _context.Donors.Add(donor);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = donor.Id }, input);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] DonorUpdateInputModel update)
    {
        var donor = _context.Donors.Find(id);
        if (donor == null) return NotFound();

        if(update.Weight < 54) throw new ArgumentException("Minimum weight of 50 kg");

        donor.Update(update.FullName, update.Email, update.Weight);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var donor = _context.Donors.Find(id);

        if (donor == null) return NotFound();

        

        return NoContent();
    }
}