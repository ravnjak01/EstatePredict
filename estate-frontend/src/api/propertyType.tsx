import axios from "axios";
import type { CreatePropertyTypeRequest } from "../models/propertyType";

const API_URL='http://localhost:5151/api/propertytype';

export const createType = async (data:CreatePropertyTypeRequest) => {
  const response = await axios.post(`${API_URL}`, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
  return response.data;
};

export const getAllTypes=async ()=>{
    const response=await axios.get(`${API_URL}`);
  return response.data;

}
