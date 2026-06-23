
using EstatePredict.DTOs;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    // POST api/prediction
    [HttpPost]
    [ProducesResponseType(typeof(PredictionDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<PredictionDTO>> Create([FromBody] CreatePredictionRequest request)
    {
        var created = await _predictionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // GET api/prediction/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PredictionDTO>> GetById(int id)
    {
        var prediction = await _predictionService.GetByIdAsync(id);

        if (prediction is null)
            return NotFound(new { message = $"Prediction with id {id} was not found." });

        return Ok(prediction);
    }

    // GET api/prediction/user/{userId}
    [HttpGet("user/{userId:int}")] 

    public async Task<ActionResult<IEnumerable<PredictionDTO>>> GetByUserId(int userId)
    {
        var predictions = await _predictionService.GetByUserIdAsync(userId);
        return Ok(predictions);
    }
}