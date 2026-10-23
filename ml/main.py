# ml/main.py

import os
import joblib
import pandas as pd
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel

MODEL_PATH = os.path.join("models", "estate_model.pkl")

app = FastAPI(title="EstatePredict ML API")

model = None


@app.on_event("startup")
def load_model():
    global model
    if not os.path.exists(MODEL_PATH):
        raise RuntimeError(
            f"Model file not found at '{MODEL_PATH}'. Run train.py first."
        )
    model = joblib.load(MODEL_PATH)


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
    if model is None:
        raise HTTPException(status_code=503, detail="Model is not loaded")

    # Column names here MUST match the column names used in train.py exactly,
    # because the saved pipeline's OneHotEncoder was fitted on these exact names.
    input_df = pd.DataFrame([{
        "Area": request.area,
        "NumberOfRooms": request.numberOfRooms,
        "YearBuilt": request.yearBuilt,
        "HasParking": request.hasParking,
        "HasLift": request.hasLift,
        "Condition": request.condition,
        "Country": request.country,
        "City": request.city,
        "Municipality": request.municipality,
        "PropertyType": request.propertyType,
    }])

    predicted_price = model.predict(input_df)[0]

    return PredictionResponse(predictedPrice=float(predicted_price))