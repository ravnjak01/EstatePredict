using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    // POST api/prediction
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePredictionRequest request)
    {
        try
        {
            var created = await _predictionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET api/prediction/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var prediction = await _predictionService.GetByIdAsync(id);

        if (prediction is null)
            return NotFound(new { message = $"Prediction with id {id} was not found." });

        return Ok(prediction);
    }

    // GET api/prediction/user/{userId}
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var predictions = await _predictionService.GetByUserIdAsync(userId);
        return Ok(predictions);
    }
}