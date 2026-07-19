import { useState } from 'react'
import './App.css'
import RegisterPage from './pages/register'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/login'

const App: React.FC = () => {
    return (
        <BrowserRouter> {/* Use BrowserRouter here */}
            <Routes>
                <Route path="/" element={<LoginPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App
