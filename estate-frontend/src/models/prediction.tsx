export interface CreatePredictionRequest {
  propertyId: number;
  userId: number;
  targetYear: number;
}

export interface PredictionDTO {
  id: number;
  userId: number;
  userFullName: string;
  propertyId: number;
  propertyTitle: string;
  propertyArea: number;
  targetYear: number;
  predictedPrice: number;
  predictedPricePerSquareMeter: number;
  confidenceScore?: number;
  modelVersion: string;
  createdAt: string;
}