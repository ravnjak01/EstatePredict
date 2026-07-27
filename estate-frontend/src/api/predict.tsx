import axios from "axios";
import type { CreatePredictionRequest, PredictionDTO } from "../models/prediction";
import api from "../interceptor/axios_interceptor";



export const PredictionService={
    create: async (data: CreatePredictionRequest): Promise<PredictionDTO> => {
    const response = await api.post<PredictionDTO>('/prediction', data);
    return response.data;
  },

  getById: async (id: number): Promise<PredictionDTO> => {
    const response = await api.get<PredictionDTO>(`/prediction/${id}`);
    return response.data;
  },

  getByUserId: async (userId: number): Promise<PredictionDTO[]> => {
    const response = await api.get<PredictionDTO[]>(`/prediction/user/${userId}`);
    return response.data;
  }
}