from fastapi import FastAPI, HTTPException
from pydantic import BaseModel

from predict import predict_price


app = FastAPI(title="EstatePredict ML API")


class PredictionRequest(BaseModel):
    area: float
    numberOfRooms: float
    yearBuilt: int
    hasParking: bool
    hasLift: bool
    condition: str
    country: str
    city: str
    municipality: str
    propertyType: str


class PredictionResponse(BaseModel):
    predictedPrice: float


@app.get("/")
def root():
    return {"message": "EstatePredict ML API is running"}


@app.post("/predict", response_model=PredictionResponse)
def predict(request: PredictionRequest):
    try:
        price = predict_price(
            area=request.area,
            number_of_rooms=request.numberOfRooms,
            year_built=request.yearBuilt,
            has_parking=request.hasParking,
            has_lift=request.hasLift,
            condition=request.condition,
            country=request.country,
            city=request.city,
            municipality=request.municipality,
            property_type=request.propertyType
        )

        return PredictionResponse(predictedPrice=price)

    except Exception as exc:
        raise HTTPException(
            status_code=500,
            detail="Prediction failed."
        ) from exc