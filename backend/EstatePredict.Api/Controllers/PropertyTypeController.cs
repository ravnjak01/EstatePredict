using EstatePredict.Api.DTO.PropertyType;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.PropertyType;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeService _propertyTypeService;

    public PropertyTypeController(
        IPropertyTypeService propertyTypeService)
    {
        _propertyTypeService = propertyTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PropertyTypeResponse>>> GetAll()
    {
        var propertyTypes = await _propertyTypeService.GetAllAsync();

        return Ok(propertyTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyTypeResponse>> GetById(int id)
    {
        var propertyType =
            await _propertyTypeService.GetByIdAsync(id);

        if (propertyType == null)
        {
            return NotFound();
        }

        return Ok(propertyType);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyTypeResponse>> Insert(
        PropertyTypeInsertRequest request)
    {
        var propertyType =
            await _propertyTypeService.InsertAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = propertyType.Id },
            propertyType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyTypeResponse>> Update(
        int id,
        PropertyTypeUpdateRequest request)
    {
        var propertyType =
            await _propertyTypeService.UpdateAsync(id, request);

        if (propertyType == null)
        {
            return NotFound();
        }

        return Ok(propertyType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _propertyTypeService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}