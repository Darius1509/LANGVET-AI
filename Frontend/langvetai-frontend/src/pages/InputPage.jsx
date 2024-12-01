import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../styles/InputPage.css';

const InputPage = ({ onSubmit }) => {
  const [inputText, setInputText] = useState('');
  const [inputName, setInputName] = useState('');
  const [inputFile, setInputFile] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setIsSubmitting(true);

    try {
      const response = await axios.get(`http://127.0.0.1:5000/api/get_terms?text="${encodeURIComponent(inputText)}"`);
      
      console.log("Terms received from API:", response.data.terms);

      const terms = response.data.terms;
      const termsDetails = await getTermsDetails(terms);
      console.log("Fetched terms details:", termsDetails);

      onSubmit(termsDetails);
      navigate('/output');

    } catch (err) {
      setErrorMessage('Failed to fetch terms. Please try again.');
      console.error('Error fetching terms:', err);
    } finally {
      setIsSubmitting(false);
    }
  };


  const getTermsDetails = async (terms) => {
    try {
      const termDetails = await Promise.all(terms.map(async (termId) => {
        const response = await axios.get(`https://localhost:7231/api/v1/HighlightedTerm/id/${termId}`);
        console.log("Fetched term details for ID", termId, response.data);
        return response.data;
      }));
      return termDetails;
    } catch (err) {
      console.error('Error fetching term details:', err);
      setErrorMessage('Failed to fetch term details.');
      return [];
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
          <div>
            <label htmlFor="inputName">Input Name:</label>
            <input
              type="text"
              id="inputName"
              value={inputName}
              onChange={(e) => setInputName(e.target.value)}
            />
          </div>
          <div>
            <label htmlFor="inputFile">Input File:</label>
            <input
              type="file"
              id="inputFile"
              accept=".txt,.pdf,.docx"
              onChange={(e) => setInputFile(e.target.files[0])}
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
