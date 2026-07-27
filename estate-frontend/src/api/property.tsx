import type { CreatePropertyRequest, PropertyDTO } from "../models/property";
import api from "../interceptor/axios_interceptor";


export const createProperty = async (data:CreatePropertyRequest) => {
  const response = await api.post(`/property`, data);
  return response.data;
};