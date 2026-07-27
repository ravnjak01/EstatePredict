import axios from "axios";
import type { CreateLocationRequest, LocationDTO } from "../models/location";
import type { CreatePredictionRequest } from "../models/prediction";

const API_URL='http://localhost:5151/api/location';

export const createLocation = async (locationData:CreateLocationRequest) => {
  const response = await axios.post(`${API_URL}`, locationData, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
  return response.data;
};

export const getAllLocations=async ()=>{
    const response=await axios.get(`${API_URL}`);
  return response.data;

}
