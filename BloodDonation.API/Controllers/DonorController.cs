using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonorController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(id);
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Created();
    }

    [HttpPut]
    public IActionResult Put()
    {
        return Ok();
    }
}