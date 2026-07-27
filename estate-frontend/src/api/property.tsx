import axios from "axios";
import type { CreatePropertyRequest, PropertyDTO } from "../models/property";

const API_URL='http://localhost:5151/api/property';


export const createProperty = async (data:CreatePropertyRequest) => {
  const response = await axios.post(`${API_URL}`, data);
  return response.data;
};