import React, { useState } from 'react';
import {useNavigate,Link} from 'react-router-dom'
import {login} from '../api/auth'
import axios, { AxiosError } from 'axios';

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    
    const navigate = useNavigate();

    const handleSubmit = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError('');
        setLoading(true); // Disable duplicate submission clicks

        try {
            await login(email, password);
            navigate('/predict'); // Redirect to estimation calculator upon success
        } catch (err) {
            const axiosError=err as AxiosError<{detail?:string}>
            // Gracefully catch 400 or 401 validation/auth errors from the API
            setError(axiosError.response?.data?.detail || 'Invalid email or password. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={styles.container}>
            <div style={styles.card}>
                <h2 style={styles.title}>EstatePredict Login</h2>
                <p style={styles.subtitle}>Enter your details to predict future real estate prices</p>
                
                {error && <div style={styles.errorAlert}>{error}</div>}

                <form onSubmit={handleSubmit}>
                    <div style={styles.formGroup}>
                        <label style={styles.label}>Email Address</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            style={styles.input}
                            placeholder="name@example.com"
                            required
                        />
                    </div>

                    <div style={styles.formGroup}>
                        <label style={styles.label}>Password</label>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            style={styles.input}
                            placeholder="••••••••"
                            required
                        />
                    </div>

                    <button 
                        type="submit" 
                        disabled={loading} 
                        style={loading ? styles.buttonDisabled : styles.button}
                    >
                        {loading ? 'Logging in...' : 'Sign In'}
                    </button>
                </form>

                <p style={styles.footerText}>
                    Don't have an account? <Link to="/register" style={styles.link}>Register here</Link>
                </p>
            </div>
        </div>
    );
};

// Simple, modern embedded styles to avoid external CSS layout breaks
const styles: { [key: string]: React.CSSProperties } = {
    container: { display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', backgroundColor: '#f4f6f9', fontFamily: 'sans-serif' },
    card: { backgroundColor: '#fff', padding: '40px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.1)', width: '100%', maxWidth: '400px' },
    title: { margin: '0 0 10px 0', textAlign: 'center', color: '#1a202c' },
    subtitle: { margin: '0 0 24px 0', textAlign: 'center', color: '#718096', fontSize: '14px' },
    formGroup: { marginBottom: '20px' },
    label: { display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#4a5568' },
  input: { 
        width: '100%', 
        padding: '10px', 
        boxSizing: 'border-box', // TypeScript now knows this must be a valid CSS value
        border: '1px solid #cbd5e0', 
        borderRadius: '4px', 
        fontSize: '16px' 
    },
    button: { width: '100%', padding: '12px', backgroundColor: '#3182ce', color: '#fff', border: 'none', borderRadius: '4px', fontSize: '16px', fontWeight: 'bold', cursor: 'pointer', transition: 'background-color 0.2s' },
    buttonDisabled: { width: '100%', padding: '12px', backgroundColor: '#a0aec0', color: '#fff', border: 'none', borderRadius: '4px', fontSize: '16px', fontWeight: 'bold', cursor: 'not-allowed' },
    errorAlert: { padding: '10px', backgroundColor: '#fed7d7', color: '#c53030', borderRadius: '4px', marginBottom: '20px', fontSize: '14px', border: '1px solid #feb2b2' },
    footerText: { textAlign: 'center', marginTop: '24px', fontSize: '14px', color: '#718096' },
    link: { color: '#3182ce', textDecoration: 'none', fontWeight: 'bold' }
};

export default LoginPage;