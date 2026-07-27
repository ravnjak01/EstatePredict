export interface LocationDTO{
    id:number;
    country:string;
    city:string;
    municipality:string;

}
export interface CreateLocationRequest {
  city: string;
  country?: string; 
  municipality?: string;
}