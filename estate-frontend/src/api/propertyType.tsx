import type { CreatePropertyTypeRequest } from "../models/propertyType";
import api from "../interceptor/axios_interceptor";


export const createType = async (data:CreatePropertyTypeRequest) => {
  const response = await api.post(`/propertytype`, data);
  return response.data;
};

export const getAllTypes=async ()=>{
  const response = await api.get(`/propertytype`);
  return response.data;

}
