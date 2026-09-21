import type { PredictionRequest, PredictionResponse } from "../models/prediction";
import api from "./api";


export const predictPrice = async (
  request: PredictionRequest
): Promise<PredictionResponse> => {
  const response = await api.post<PredictionResponse>(
    "/prediction",
    request
  );

  return response.data;
};