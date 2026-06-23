using EstatePredict.DTOs;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    // GET /api/location
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAll()
    {
        var locations = await _locationService.GetAllAsync();
        return Ok(locations);
    }

    // GET /api/location/{id}
    // Napomena: Ova metoda je tehnički potrebna da bi CreatedAtAction u POST-u mogao generisati 'Location' zaglavlje
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LocationDTO>> GetById(int id)
    {
        var location = await _locationService.GetByIdAsync(id);

        if (location is null)
            return NotFound(new { message = $"Location with id {id} was not found." });

        return Ok(location);
    }

    // POST /api/location
    [HttpPost]
    [ProducesResponseType(typeof(LocationDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<LocationDTO>> Create([FromBody] CreateLocationRequest request)
    {
        var created = await _locationService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}