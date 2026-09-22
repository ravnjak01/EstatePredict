from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()


class PredictionRequest(BaseModel):
    area: float
    numberOfRooms: int
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

    # Privremeno testna vrijednost
    predicted_price = 185000

    return PredictionResponse(
        predictedPrice=predicted_price
    )