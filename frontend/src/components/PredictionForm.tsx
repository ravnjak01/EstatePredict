import { useEffect, useState } from "react";
import { getLocations } from "../services/locationService";
import { getPropertyTypes } from "../services/propertyTypeService";
import type { PropertyType } from "../models/propertyType";
import type { Location } from "../models/location";
import "./PredictionForm.css";

import type { PredictionRequest, PredictionResponse } from "../models/prediction";
import { predictPrice } from "../services/predictionService";

const PredictionForm = () => {
  const [locations, setLocations] = useState<Location[]>([]);
  const [propertyTypes, setPropertyTypes] = useState<PropertyType[]>([]);

  const [formData, setFormData] = useState<PredictionRequest>({
    area: 0,
    numberOfRooms: 0,
    yearBuilt: 2000,
    hasParking: false,
    hasLift: false,
    condition: "",
    locationId: 0,
    propertyTypeId: 0,
  });

  const [prediction, setPrediction] =
    useState<PredictionResponse | null>(null);

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [locationsData, propertyTypesData] = await Promise.all([
        getLocations(),
        getPropertyTypes(),
      ]);

      setLocations(locationsData);
      setPropertyTypes(propertyTypesData);
    } catch {
      setError("Failed to load locations and property types.");
    }
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target;

    setFormData((previous) => ({
      ...previous,
      [name]:
        type === "checkbox"
          ? (e.target as HTMLInputElement).checked
          : name === "area" ||
            name === "numberOfRooms" ||
            name === "yearBuilt" ||
            name === "locationId" ||
            name === "propertyTypeId"
          ? Number(value)
          : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(formData);
    setLoading(true);
    setError("");
    setPrediction(null);

    try {
      const result = await predictPrice(formData);

      setPrediction(result);
    } catch {
      setError("Prediction failed.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="prediction-container">
      <h1 className="prediction-title">Property Price Prediction</h1>

      <form onSubmit={handleSubmit}>
        <div className="form-grid">
          <div className="form-group">
            <label>Area (m²)</label>
            <input
              type="number"
              name="area"
              value={formData.area}
              onChange={handleChange}
              min="1"
              required
            />
          </div>

          <div className="form-group">
            <label>Number of rooms</label>
            <input
              type="number"
              name="numberOfRooms"
              value={formData.numberOfRooms}
              onChange={handleChange}
              min="1"
              required
            />
          </div>

          <div className="form-group">
            <label>Year built</label>
            <input
              type="number"
              name="yearBuilt"
              value={formData.yearBuilt}
              onChange={handleChange}
              min="1800"
              max={new Date().getFullYear()}
              required
            />
          </div>

          <div className="form-group">
            <label>Condition</label>
            <select
              name="condition"
              value={formData.condition}
              onChange={handleChange}
              required
            >
              <option value="">Select condition</option>
              <option value="New">New</option>
              <option value="Excellent">Excellent</option>
              <option value="Good">Good</option>
              <option value="Needs renovation">Needs renovation</option>
            </select>
          </div>

          <div className="form-group">
            <label>Location</label>
            <select
              name="locationId"
              value={formData.locationId}
              onChange={handleChange}
              required
            >
              <option value={0}>Select location</option>
              {locations.map((location) => (
                <option key={location.id} value={location.id}>
                  {location.city} - {location.municipality}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label>Property type</label>
            <select
              name="propertyTypeId"
              value={formData.propertyTypeId}
              onChange={handleChange}
              required
            >
              <option value={0}>Select property type</option>
              {propertyTypes.map((propertyType) => (
                <option key={propertyType.id} value={propertyType.id}>
                  {propertyType.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="checkbox-group">
          <label className="checkbox-label">
            <input
              type="checkbox"
              name="hasParking"
              checked={formData.hasParking}
              onChange={handleChange}
            />
            Has parking
          </label>

          <label className="checkbox-label">
            <input
              type="checkbox"
              name="hasLift"
              checked={formData.hasLift}
              onChange={handleChange}
            />
            Has lift
          </label>
        </div>

        <button type="submit" className="submit-btn" disabled={loading}>
          {loading ? "Predicting..." : "Predict price"}
        </button>
      </form>

      {error && <p className="error-message">{error}</p>}

      {prediction && (
        <div className="result-card">
          <h2>Prediction result</h2>
          <p>
            Predicted price:{" "}
            <strong>
              {prediction.predictedPrice.toLocaleString()} €
            </strong>
          </p>
          <p>
            Price per m²:{" "}
            <strong>
              {prediction.predictedPricePerSquareMeter.toLocaleString()} €
            </strong>
          </p>
        </div>
      )}
    </div>
  );
};

export default PredictionForm;