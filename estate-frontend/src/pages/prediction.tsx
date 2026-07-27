import React, { useState, useEffect } from 'react';
import type { CreatePropertyRequest } from '../models/property';
import { createProperty } from '../api/property';
import { getAllLocations } from '../api/location'; 
import { getAllTypes } from '../api/propertyType';  

interface PropertyFormProps {
  onSuccess?: (createdProperty: any) => void;
}

export const PropertyForm: React.FC<PropertyFormProps> = ({ onSuccess }) => {
  const [locations, setLocations] = useState<any[]>([]);
  const [propertyTypes, setPropertyTypes] = useState<any[]>([]);

  // Inicijalno stanje forme
  const [formData, setFormData] = useState<CreatePropertyRequest>({
    title: '',
    description: '',
    area: 50,
    numberOfRooms: 2,
    yearBuilt: new Date().getFullYear(),
    currentPrice: 100000,
    locationId: 0,
    propertyTypeId: 0,
    userId: 1, 
  });

  const [loading, setLoading] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  useEffect(() => {
    const fetchDropdownData = async () => {
      try {
        const [locationsData, typesData] = await Promise.all([
          getAllLocations(),
          getAllTypes(),
        ]);

        setLocations(locationsData);
        setPropertyTypes(typesData);

        if (locationsData.length > 0 && typesData.length > 0) {
          setFormData((prev) => ({
            ...prev,
            locationId: locationsData[0].id,
            propertyTypeId: typesData[0].id,
          }));
        }
      } catch (error) {
        console.error('Greška pri učitavanju dropdown podataka:', error);
        setErrorMessage('Greška pri učitavanju lokacija i tipova nekretnina.');
      }
    };

    fetchDropdownData();
  }, []);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target;

    const isNumericField = [
      'area',
      'numberOfRooms',
      'yearBuilt',
      'currentPrice',
      'locationId',
      'propertyTypeId',
    ].includes(name);

    setFormData((prev) => ({
      ...prev,
      [name]: isNumericField ? (value === '' ? 0 : Number(value)) : value,
    }));
  };

  // Slanje forme na backend
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!formData.locationId || !formData.propertyTypeId) {
      setErrorMessage('Molimo izaberite lokaciju i tip nekretnine.');
      return;
    }

    setLoading(true);
    setErrorMessage(null);
    setSuccessMessage(null);

    try {
      const response = await createProperty(formData);
      setSuccessMessage('Nekretnina je uspješno sačuvana!');

      // Resetovanje forme
      setFormData({
        title: '',
        description: '',
        area: 50,
        numberOfRooms: 2,
        yearBuilt: new Date().getFullYear(),
        currentPrice: 100000,
        locationId: locations.length > 0 ? locations[0].id : 0,
        propertyTypeId: propertyTypes.length > 0 ? propertyTypes[0].id : 0,
        userId: 1,
      });

      if (onSuccess) {
        onSuccess(response);
      }
    } catch (err: any) {
      const message =
        err.response?.data?.message ||
        err.response?.data ||
        'Došlo je do greške prilikom spremanja nekretnine.';
      setErrorMessage(typeof message === 'string' ? message : JSON.stringify(message));
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: '0 auto', padding: '20px' }}>
      <h2>Unos nove nekretnine</h2>

      {errorMessage && (
        <div style={{ color: 'red', marginBottom: '10px', padding: '10px', border: '1px solid red' }}>
          {errorMessage}
        </div>
      )}

      {successMessage && (
        <div style={{ color: 'green', marginBottom: '10px', padding: '10px', border: '1px solid green' }}>
          {successMessage}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '15px' }}>
          <label style={{ display: 'block', fontWeight: 'bold' }}>Naslov *</label>
          <input
            type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            minLength={3}
            maxLength={200}
            required
            style={{ width: '100%', padding: '8px' }}
          />
        </div>

        <div style={{ marginBottom: '15px' }}>
          <label style={{ display: 'block', fontWeight: 'bold' }}>Opis</label>
          <textarea
            name="description"
            value={formData.description || ''}
            onChange={handleChange}
            maxLength={2000}
            rows={4}
            style={{ width: '100%', padding: '8px' }}
          />
        </div>

        <div style={{ display: 'flex', gap: '15px', marginBottom: '15px' }}>
          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Površina (m²) *</label>
            <input
              type="number"
              name="area"
              value={formData.area}
              onChange={handleChange}
              min={1}
              max={100000}
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </div>

          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Broj soba *</label>
            <input
              type="number"
              name="numberOfRooms"
              value={formData.numberOfRooms}
              onChange={handleChange}
              min={1}
              max={100}
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </div>
        </div>

        <div style={{ display: 'flex', gap: '15px', marginBottom: '15px' }}>
          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Godina izgradnje *</label>
            <input
              type="number"
              name="yearBuilt"
              value={formData.yearBuilt}
              onChange={handleChange}
              min={1800}
              max={2100}
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </div>

          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Trenutna cijena (€) *</label>
            <input
              type="number"
              name="currentPrice"
              value={formData.currentPrice}
              onChange={handleChange}
              min={0.01}
              step="any"
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </div>
        </div>

        {/* Dropdown-i za Lokaciju i Tip Nekretnine */}
        <div style={{ display: 'flex', gap: '15px', marginBottom: '20px' }}>
          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Lokacija *</label>
            <select
              name="locationId"
              value={formData.locationId}
              onChange={handleChange}
              required
              style={{ width: '100%', padding: '8px' }}
            >
              <option value={0} disabled>-- Izaberite lokaciju --</option>
              {locations.map((loc) => (
                <option key={loc.id} value={loc.id}>
                  {loc.city}, {loc.municipality} ({loc.country})
                </option>
              ))}
            </select>
          </div>

          <div style={{ flex: 1 }}>
            <label style={{ display: 'block', fontWeight: 'bold' }}>Tip nekretnine *</label>
            <select
              name="propertyTypeId"
              value={formData.propertyTypeId}
              onChange={handleChange}
              required
              style={{ width: '100%', padding: '8px' }}
            >
              <option value={0} disabled>-- Izaberite tip --</option>
              {propertyTypes.map((type) => (
                <option key={type.id} value={type.id}>
                  {type.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
          style={{
            width: '100%',
            padding: '10px',
            backgroundColor: loading ? '#ccc' : '#007bff',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: loading ? 'not-allowed' : 'pointer',
            fontSize: '16px',
          }}
        >
          {loading ? 'Spremanje...' : 'Sačuvaj nekretninu'}
        </button>
      </form>
    </div>
  );
};