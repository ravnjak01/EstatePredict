using EstatePredict.Api.DTO.Location;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.Location;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LocationResponse>>> GetAll()
    {
        var locations = await _locationService.GetAllAsync();

        return Ok(locations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationResponse>> GetById(int id)
    {
        var location = await _locationService.GetByIdAsync(id);

        if (location == null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPost]
    public async Task<ActionResult<LocationResponse>> Insert(
        LocationInsertRequest request)
    {
        var location = await _locationService.InsertAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = location.Id },
            location);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocationResponse>> Update(
        int id,
        LocationUpdateRequest request)
    {
        var location = await _locationService.UpdateAsync(id, request);

        if (location == null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _locationService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}