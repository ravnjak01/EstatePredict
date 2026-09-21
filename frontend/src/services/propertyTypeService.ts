import type { PropertyType } from "../models/propertyType";
import api from "./api";

export const getPropertyTypes=async ():Promise<PropertyType[]>=>{
    const response=await api.get<PropertyType[]>("/propertyType");

    return response.data;
}