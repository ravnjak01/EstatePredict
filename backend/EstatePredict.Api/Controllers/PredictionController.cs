using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests.Prediction;
using EstatePredict.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpPost]
    public async Task<ActionResult<PredictionResponse>> Predict(
        PredictionRequest request)
    {
        var prediction = await _predictionService.PredictAsync(request);

        return Ok(prediction);
    }
}