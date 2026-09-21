using EstatePredict.Api.Requests.Prediction;
using EstatePredict.Api.Responses;

namespace EstatePredict.Api.Interfaces
{
    public interface IPredictionService
    {
        Task<PredictionResponse> PredictAsync(PredictionRequest request);
    }
}
