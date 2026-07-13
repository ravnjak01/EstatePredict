from fastapi import FastAPI
import pickle 
import pandas as pd
from pydantic import BaseModel
from predict import make_prediction

class PropertyRequest(BaseModel):
    Area: float = Field(..., description="Property area in square meters", gt=0)
    NumberOfRooms: int = Field(..., description="Number of rooms", gt=0)
    YearBuilt: int = Field(..., description="The year the property was built")
    
    PropertyTypeId: int = Field(..., description="ID of the property type")
    LocationId: int = Field(..., description="ID of the location (Country/City/Municipality)")
    
    TargetYear: int = Field(..., description="The future year selected for prediction")
app=FastAPI()

@app.post("/predict")
async def predict_price(request:PropertyRequest):
    prediction_results=make_prediction(
        area=request.Area,
        rooms=request.NumberOfRooms,
        year_built=request.YearBuilt,
        target_year=request.TargetYear,
        location_id=request.LocationId,
        property_type_id=request.PropertyTypeId
    )
    return prediction_results