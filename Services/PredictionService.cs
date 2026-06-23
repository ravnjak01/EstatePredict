using EstatePredict.Api.Database;
using EstatePredict.DTOs;
using EstatePredict.Models.Entities;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Services;

public class PredictionService : IPredictionService
{
    private readonly EstatePredictContext _db;

    public PredictionService(EstatePredictContext db)
    {
        _db = db;
    }

    public async Task<PredictionDTO> CreateAsync(CreatePredictionRequest request)
    {
        var property = await _db.Properties
            .Include(p => p.Location)
            .Include(p => p.PropertyType)
            .FirstOrDefaultAsync(p => p.Id == request.PropertyId)
            ?? throw new ArgumentException($"Property with id {request.PropertyId} does not exist.");

        var userExists = await _db.Users.AnyAsync(u => u.Id == request.UserId);
        if (!userExists)
            throw new ArgumentException($"User with id {request.UserId} does not exist.");

   
        // Ovdje će kasnije biti poziv prema Python ML servisu.
   
        //
        // Primjer kasnije:
        // var result = await _pythonPredictionClient.PredictAsync(property, request.TargetYear);

        var prediction = new Prediction
        {
            UserId = request.UserId,
            PropertyId = request.PropertyId,
            TargetYear = request.TargetYear,

            // Privremeno 0 dok se ne spoji Python model.
            PredictedPrice = 0,
            PredictedPricePerSquareMeter = 0,
            ConfidenceScore = null,
            ModelVersion = "python-ml-pending",

            CreatedAt = DateTime.UtcNow
        };

        _db.Predictions.Add(prediction);
        await _db.SaveChangesAsync();

        await _db.Entry(prediction).Reference(p => p.User).LoadAsync();
        await _db.Entry(prediction).Reference(p => p.Property).LoadAsync();

        return MapToDto(prediction);
    }

    public async Task<PredictionDTO?> GetByIdAsync(int id)
    {
        var prediction = await LoadWithRelationsQuery()
            .FirstOrDefaultAsync(p => p.Id == id);

        return prediction is null ? null : MapToDto(prediction);
    }

    public async Task<IEnumerable<PredictionDTO>> GetByUserIdAsync(int userId)
    {
        return await LoadWithRelationsQuery()
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    private IQueryable<Prediction> LoadWithRelationsQuery()
    {
        return _db.Predictions
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Property);
    }

    private static PredictionDTO MapToDto(Prediction p) => new()
    {
        Id = p.Id,
        UserId = p.UserId,
        UserFullName = $"{p.User.FirstName} {p.User.LastName}",
        PropertyId = p.PropertyId,
        PropertyTitle = p.Property.Title,
        PropertyArea = p.Property.Area,
        TargetYear = p.TargetYear,
        PredictedPrice = p.PredictedPrice,
        PredictedPricePerSquareMeter = p.PredictedPricePerSquareMeter,
        ConfidenceScore = p.ConfidenceScore,
        ModelVersion = p.ModelVersion,
        CreatedAt = p.CreatedAt
    };
}