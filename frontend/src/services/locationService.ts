import api from "./api";
import type { Location } from "../models/location";

export const getLocations=async ():Promise<Location[]>=>{
    const response=await api.get<Location[]>("/location");

    return response.data;
}