using EstatePredict.DTOs;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
// 💡 Globalne greške koje middleware može da aktivira za bilo koju rutu
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
    // 😎 Swagger sam zna da ovo vraća 200 OK sa listom objekata
    public async Task<ActionResult<IEnumerable<PropertyTypeDTO>>> GetAll()
    {
        var types = await _propertyTypeService.GetAllAsync();
        return Ok(types);
    }

    // GET api/propertytype/{id}
    [HttpGet("{id:int}")]
    // 😎 Swagger sam zna da ovo vraća 200 OK sa jednim objektom
    public async Task<ActionResult<PropertyTypeDTO>> GetById(int id)
    {
        var propertyType = await _propertyTypeService.GetByIdAsync(id);

        if (propertyType is null)
            return NotFound(new { message = $"Property type with id {id} was not found." });

        return Ok(propertyType);
    }

    // POST api/propertytype
    [HttpPost]
    // 💡 Pošto CreatedAtAction vraća 201 (a ne 200), ovdje je korisno ostaviti ovaj atribut
    // da bi Swagger znao tačan tip podatka koji se kreira.
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
    // 💡 Pošto NoContent() ne vraća nikakav objekat (tijelo je prazno), 
    // ovdje koristimo običan IActionResult i eksplicitno kažemo da je to 204.
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _propertyTypeService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = $"Property type with id {id} was not found." });

        return NoContent();
    }
}