from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field
import pandas as pd
from predict import predict_price, load_model

app = FastAPI()

@app.on_event("startup")
def startup_event():
    load_model()

class PropertyRequest(BaseModel):
    Area: float = Field(..., description="Property area in square meters", gt=0)
    NumberOfRooms: int = Field(..., description="Number of rooms", gt=0)
    YearBuilt: int = Field(..., description="The year the property was built")
    PropertyTypeId: int = Field(..., description="ID of the property type")
    LocationId: int = Field(..., description="ID of the location (Country/City/Municipality)")
    Condition: str = Field(..., description="Property condition")
    HasParking: bool = Field(..., description="Does the property have parking?")
    HasLift: bool = Field(..., description="Does the building have an elevator?")
    TargetYear: int = Field(..., description="The future year selected for prediction")

@app.post("/predict")
async def predict(request: PropertyRequest):
    try:
        input_data = request.dict()
        
        prediction_results = predict_price(input_data)
        
        return prediction_results
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))