import type { CreateLocationRequest, LocationDTO } from "../models/location";
import api from "../interceptor/axios_interceptor";

const API_URL='http://localhost:5151/api/location';

export const createLocation = async (locationData:CreateLocationRequest) => {
   const response = await api.post(`/location`, locationData);
  return response.data;
};

export const getAllLocations=async ()=>{
      const response = await api.get(`/location`);
  return response.data;

}
