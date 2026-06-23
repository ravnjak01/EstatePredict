using EstatePredict.DTO;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // GET api/property
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        return Ok(properties);
    }

    // GET api/property/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PropertyDTO>> GetById(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);

        if (property is null)
            return NotFound(new { message = $"Property with id {id} was not found." });

        return Ok(property);
    }

    // POST api/property
    [HttpPost]
    [ProducesResponseType(typeof(PropertyDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<PropertyDTO>> Create([FromBody] CreatePropertyRequest request)
    {
        var created = await _propertyService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/property/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PropertyDTO>> Update(int id, [FromBody] UpdatePropertyRequest request)
    {
        var updated = await _propertyService.UpdateAsync(id, request);

        if (updated is null)
            return NotFound(new { message = $"Property with id {id} was not found." });

        return Ok(updated);
    }

    // DELETE api/property/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _propertyService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = $"Property with id {id} was not found." });

        return NoContent();
    }
}