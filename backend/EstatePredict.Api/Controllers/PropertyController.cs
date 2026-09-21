using EstatePredict.Api.DTO.Property;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.Property;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
	private readonly IPropertyService _propertyService;

	public PropertyController(IPropertyService propertyService)
	{
		_propertyService = propertyService;
	}

	[HttpGet]
	public async Task<ActionResult<List<PropertyResponse>>> GetAll()
	{
		var properties = await _propertyService.GetAllAsync();

		return Ok(properties);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PropertyResponse>> GetById(int id)
	{
		var property = await _propertyService.GetByIdAsync(id);

		if (property == null)
		{
			return NotFound();
		}

		return Ok(property);
	}

	[HttpPost]
	public async Task<ActionResult<PropertyResponse>> Insert(
		PropertyInsertRequest request)
	{
		var property = await _propertyService.InsertAsync(request);

		return CreatedAtAction(
			nameof(GetById),
			new { id = property.Id },
			property);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<PropertyResponse>> Update(
		int id,
		PropertyUpdateRequest request)
	{
		var property = await _propertyService.UpdateAsync(id, request);

		if (property == null)
		{
			return NotFound();
		}

		return Ok(property);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var deleted = await _propertyService.DeleteAsync(id);

		if (!deleted)
		{
			return NotFound();
		}

		return NoContent();
	}
}