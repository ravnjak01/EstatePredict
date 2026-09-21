export interface PredictionRequest {
  area: number;
  numberOfRooms: number;
  yearBuilt: number;
  hasParking: boolean;
  hasLift: boolean;
  condition: string;
  locationId: number;
  propertyTypeId: number;
}

export interface PredictionResponse {
  predictedPrice: number;
  predictedPricePerSquareMeter: number;
}