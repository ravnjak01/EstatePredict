import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { register } from '../api/auth'
import axios, { AxiosError } from 'axios';


const RegisterPage = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const navigate = useNavigate();

    const handleSubmit = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError('');

        if (password !== confirmPassword) {
            setError("Passwords do not match!");
            return;
        }

        setLoading(true);

        try {
            await register(firstName, lastName, email, password);
            navigate('/login'); 
        } catch (err) {
            const axiosError=err as AxiosError<{detail?:string}>
            setError(axiosError.response?.data?.detail || 'Registration failed. Check your validation parameters.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={styles.container}>
            <div style={styles.card}>
                <h2 style={styles.title}>Create Account</h2>
                <p style={styles.subtitle}>Join EstatePredict to check real estate growth trends</p>

                {error && <div style={styles.errorAlert}>{error}</div>}

                <form onSubmit={handleSubmit}>
                    <div style={styles.row}>
                        <div style={{ ...styles.formGroup, flex: 1, marginRight: '10px' }}>
                            <label style={styles.label}>First Name</label>
                            <input
                                type="text"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                style={styles.input}
                                required
                            />
                        </div>
                        <div style={{ ...styles.formGroup, flex: 1 }}>
                            <label style={styles.label}>Last Name</label>
                            <input
                                type="text"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                style={styles.input}
                                required
                            />
                        </div>
                    </div>

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
                            placeholder="Minimum 6 characters"
                            required
                        />
                    </div>

                    <div style={styles.formGroup}>
                        <label style={styles.label}>Confirm Password</label>
                        <input
                            type="password"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
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
                        {loading ? 'Creating Account...' : 'Register'}
                    </button>
                </form>

                <p style={styles.footerText}>
                    Already have an account? <Link to="/login" style={styles.link}>Sign In</Link>
                </p>
            </div>
        </div>
    );
};

const styles: { [key: string]: React.CSSProperties } = {

    container: { display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', backgroundColor: '#f4f6f9', fontFamily: 'sans-serif' },
    card: { backgroundColor: '#fff', padding: '40px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.1)', width: '100%', maxWidth: '450px' },
    title: { margin: '0 0 10px 0', textAlign: 'center', color: '#1a202c' },
    subtitle: { margin: '0 0 24px 0', textAlign: 'center', color: '#718096', fontSize: '14px' },
    row: { display: 'flex', justifyContent: 'space-between' },
    formGroup: { marginBottom: '16px' },
    label: { display: 'block', marginBottom: '6px', fontSize: '14px', fontWeight: 'bold', color: '#4a5568' },
     input: { 
        width: '100%', 
        padding: '10px', 
        boxSizing: 'border-box', 
        border: '1px solid #cbd5e0', 
        borderRadius: '4px', 
        fontSize: '16px' 
    },
    button: { width: '100%', padding: '12px', backgroundColor: '#38a169', color: '#fff', border: 'none', borderRadius: '4px', fontSize: '16px', fontWeight: 'bold', cursor: 'pointer' },
    buttonDisabled: { width: '100%', padding: '12px', backgroundColor: '#a0aec0', color: '#fff', border: 'none', borderRadius: '4px', fontSize: '16px', fontWeight: 'bold', cursor: 'not-allowed' },
    errorAlert: { padding: '10px', backgroundColor: '#fed7d7', color: '#c53030', borderRadius: '4px', marginBottom: '20px', fontSize: '14px', border: '1px solid #feb2b2' },
    footerText: { textAlign: 'center', marginTop: '24px', fontSize: '14px', color: '#718096' },
    link: { color: '#38a169', textDecoration: 'none', fontWeight: 'bold' }
};

export default RegisterPage;