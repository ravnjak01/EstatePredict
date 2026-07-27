import axios from "axios";
import type { CreatePredictionRequest, PredictionDTO } from "../models/prediction";

const API_URL='http://localhost:5151/api/prediction';


export const PredictionService={
    create: async (data: CreatePredictionRequest): Promise<PredictionDTO> => {
    const response = await axios.post<PredictionDTO>('${API_URL}', data);
    return response.data;
  },

  getById: async (id: number): Promise<PredictionDTO> => {
    const response = await axios.get<PredictionDTO>(`${API_URL}/${id}`);
    return response.data;
  },

  getByUserId: async (userId: number): Promise<PredictionDTO[]> => {
    const response = await axios.get<PredictionDTO[]>(`${API_URL}/user/${userId}`);
    return response.data;
  }
}