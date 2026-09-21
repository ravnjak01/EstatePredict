using EstatePredict.Api.Data;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Requests;
using EstatePredict.Api.Requests.Prediction;
using EstatePredict.Api.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace EstatePredict.Api.Services;

public class PredictionService : IPredictionService
{
    private readonly EstatePredictContext _context;
    private readonly HttpClient _httpClient;

    public PredictionService(
        EstatePredictContext context,
        HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task<PredictionResponse> PredictAsync(
        PredictionRequest request)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == request.LocationId);

        if (location == null)
        {
            throw new KeyNotFoundException("Location not found.");
        }

        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == request.PropertyTypeId);

        if (propertyType == null)
        {
            throw new KeyNotFoundException("Property type not found.");
        }

        var pythonRequest = new PythonPredictionRequest
        {
            Area = request.Area,
            NumberOfRooms = request.NumberOfRooms,
            YearBuilt = request.YearBuilt,
            HasParking = request.HasParking,
            HasLift = request.HasLift,
            Condition = request.Condition,

            Country = location.Country,
            City = location.City,
            Municipality = location.Municipality,

            PropertyType = propertyType.Name
        };

        var response = await _httpClient.PostAsJsonAsync(
            "predict",
            pythonRequest);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                "Prediction service is unavailable.");
        }

        var pythonResponse =
            await response.Content.ReadFromJsonAsync<PythonPredictionResponse>();

        if (pythonResponse == null)
        {
            throw new InvalidOperationException(
                "Invalid response from prediction service.");
        }

        var pricePerSquareMeter =
            pythonResponse.PredictedPrice / request.Area;

        return new PredictionResponse
        {
            PredictedPrice = pythonResponse.PredictedPrice,
            PredictedPricePerSquareMeter = pricePerSquareMeter
        };
    }
}