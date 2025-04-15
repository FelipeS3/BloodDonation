using BloodDonation.Application.Models;
using BloodDonation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _service;
    public DonorController(IDonorService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        //Ainda n retornando casos de null ou error na controller.
        var result = _service.GetAll();

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult PostDonor(CreateDonorInputViewModel input)
    {
        var result = _service.Insert(input);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new {id = result.Data}, input);

    }

    [HttpPut("{id}")]
    public IActionResult PutDonor(int id,DonorUpdateInputModel update)
    {
        var result = _service.Update(id, update);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return NoContent();
    }
}