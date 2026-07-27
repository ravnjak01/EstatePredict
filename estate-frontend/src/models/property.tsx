
export interface CreatePropertyRequest {
  title: string;
  description?: string | null; 
  area: number;                
  numberOfRooms: number;
  yearBuilt: number;
  currentPrice: number;        
  locationId: number;
  propertyTypeId: number;
  userId: number;
}


export interface UpdatePropertyRequest {
  title: string;
  description?: string | null;
  area: number;
  numberOfRooms: number;
  yearBuilt: number;
  currentPrice: number;
  locationId: number;
  propertyTypeId: number;
}


export interface PropertyDTO {
  id: number;
  title: string;
  description?: string | null;
  area: number;
  numberOfRooms: number;
  yearBuilt: number;
  currentPrice: number;

  locationId: number;
  country: string;
  city: string;
  municipality: string;

  propertyTypeId: number;
  propertyTypeName: string;

  userId: number;
}