import axios from "axios";
import type { CreatePredictionRequest, PredictionDTO } from "../models/prediction";

const API_URL='http://localhost:5151/api/prediction/';


export const PredictionService={
    create: async (data: CreatePredictionRequest): Promise<PredictionDTO> => {
    const response = await axios.post<PredictionDTO>('/api/prediction', data);
    return response.data;
  },

  getById: async (id: number): Promise<PredictionDTO> => {
    const response = await axios.get<PredictionDTO>(`/api/prediction/${id}`);
    return response.data;
  },

  getByUserId: async (userId: number): Promise<PredictionDTO[]> => {
    const response = await axios.get<PredictionDTO[]>(`/api/prediction/user/${userId}`);
    return response.data;
  }
}