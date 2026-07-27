import React, { useState } from 'react';
import { createLocation } from '../api/location';

const AddLocationForm = () => {
  const [formData, setFormData] = useState({ country: '', city: '',municipality:'' });
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  const handleSubmit = async (e:React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');

    try {
      const result = await createLocation(formData);
      setMessage(`Uspešno kreirana lokacija: ${result.name} (ID: ${result.id})`);
      setFormData({ country: '', city: '' ,municipality:''}); // Reset forme
    } catch (error) {
      setMessage('Greška pri kreiranju lokacije.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>

        <div>
        <label>Država:</label>
        <input 
          type="text" 
          value={formData.country} 
          onChange={(e) => setFormData({ ...formData, country: e.target.value })} 
          required 
        />
      </div>
      
      <div>
        <label>Naziv općine:</label>
        <input 
          type="text" 
          value={formData.municipality} 
          onChange={(e) => setFormData({ ...formData, municipality: e.target.value })} 
          required 
        />
      </div>
      <div>
        <label>Grad:</label>
        <input 
          type="text" 
          value={formData.city} 
          onChange={(e) => setFormData({ ...formData, city: e.target.value })} 
          required 
        />
      </div>
      <button type="submit" disabled={loading}>
        {loading ? 'Spremanje...' : 'Dodaj Lokaciju'}
      </button>
      {message && <p>{message}</p>}
    </form>
  );
};

export default AddLocationForm;