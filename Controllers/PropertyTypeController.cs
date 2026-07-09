using EstatePredict.DTOs;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeService _propertyTypeService;

    public PropertyTypeController(IPropertyTypeService propertyTypeService)
    {
        _propertyTypeService = propertyTypeService;
    }

    // GET api/propertytype
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyTypeDTO>>> GetAll()
    {
        var types = await _propertyTypeService.GetAllAsync();
        return Ok(types);
    }

    // GET api/propertytype/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PropertyTypeDTO>> GetById(int id)
    {
        var propertyType = await _propertyTypeService.GetByIdAsync(id);

        if (propertyType is null)
            return NotFound(new { message = $"Property type with id {id} was not found." });

        return Ok(propertyType);
    }

    // POST api/propertytype
    [HttpPost]
    [ProducesResponseType(typeof(PropertyTypeDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<PropertyTypeDTO>> Create([FromBody] CreatePropertyTypeRequest request)
    {
        var created = await _propertyTypeService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/propertytype/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PropertyTypeDTO>> Update(int id, [FromBody] UpdatePropertyTypeRequest request)
    {
        var updated = await _propertyTypeService.UpdateAsync(id, request);

        if (updated is null)
            return NotFound(new { message = $"Property type with id {id} was not found." });

        return Ok(updated);
    }

    // DELETE api/propertytype/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _propertyTypeService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = $"Property type with id {id} was not found." });

        return NoContent();
    }
}