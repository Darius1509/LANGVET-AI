import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../styles/InputPage.css';

const InputPage = ({ onSubmit }) => {
  const [inputText, setInputText] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setIsSubmitting(true);

    try {
      const response = await axios.get(
        `http://127.0.0.1:5000/api/get_terms?text="${encodeURIComponent(inputText)}"`
      );
      console.log('API Response:', response.data);

      const { clusters, terms } = response.data; // Extract clusters and terms
      onSubmit({ clusters, terms }); // Pass both clusters and terms
      navigate('/output');
    } catch (err) {
      setErrorMessage('Failed to fetch terms. Please try again.');
      console.error('Error fetching terms:', err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="container">
      <h1>LANGVET-AI</h1>
      <div id="input-form">
        <h2>Submit Input</h2>
        {errorMessage && <p className="error">{errorMessage}</p>}
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="inputText">Input Text:</label>
            <textarea
              id="inputText"
              value={inputText}
              onChange={(e) => setInputText(e.target.value)}
              required
            />
          </div>
          <button type="submit" disabled={isSubmitting}>
            {isSubmitting ? 'Processing...' : 'Submit'}
          </button>
        </form>
      </div>
    </div>
  );
};

export default InputPage;
