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
    public DonorController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var donors = _context.Donors.Include(d => d.Donations).ToList();

        var model = donors.Select(d => DonorInputViewModel.ToEntity(d));

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var donor = _context.Donors.FirstOrDefault(d => d.Id == id);

        var model = DonorInputViewModel.ToEntity(donor);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult PostDonor([FromBody] CreateDonorInputViewModel input)
    {
        var donor = input.ToEntity(); 

        _context.Donors.Add(donor);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = donor.Id }, input);
    }

    [HttpPut("{id}")]
    public IActionResult PutDonor(int id, [FromBody] DonorUpdateInputModel update)
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

        _context.Donors.Remove(donor);
        _context.SaveChanges();

        return NoContent();
    }
}