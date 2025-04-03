﻿using BloodDonation.API.Data;
using BloodDonation.API.Entities;
using BloodDonation.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonationController : ControllerBase
{
    private readonly  BloodDonationDbContext _context;
    private DonationController(BloodDonationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Donors.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetDonation(int id)
    {
        var donation = _context.Donations.Find(id);

        if (donation == null) return NotFound();

        return Ok(donation);
    }

    [HttpPost]
    public IActionResult RegisterDonation([FromBody] DonationInputViewModel input)
    {
        var donation = new Donation(input.DonorId, input.VolumeInML);
        
        _context.Donations.Add(donation);
        _context.SaveChanges();
       
        return Ok(new
        {
            Message = "Donation Registred!",
            DonationId = donation.Id
        });
    }
}
