import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/InputPage.css';

const InputPage = ({ onSubmit }) => {
  const [inputText, setInputText] = useState('');
  const [inputName, setInputName] = useState('');
  const [inputFile, setInputFile] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  // Mock NLP response for demonstration
  const mockNlpResponse = {
    terms: [
      {
        termName: "Bengal tiger",
        termDefinition: "A subspecies of tiger primarily found in India.",
        termDescription: "The Bengal tiger is known for its orange coat with black stripes.",
        termLink: "https://en.wikipedia.org/wiki/Bengal_tiger",
        termSubCluster: "Mammals",
      },
      {
        termName: "Panthera tigris tigris",
        termDefinition: "The scientific name for the Bengal tiger.",
        termDescription: "Panthera tigris tigris is the nominate subspecies of tiger.",
        termLink: "https://en.wikipedia.org/wiki/Tiger",
        termSubCluster: "Mammals",
      },
      {
        termName: "tropical rainforests",
        termDefinition: "Dense forests in tropical regions with high rainfall.",
        termDescription: "Tropical rainforests support a diverse range of species.",
        termLink: "https://en.wikipedia.org/wiki/Rainforest",
        termSubCluster: "Ecosystems",
      },
    ],
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setIsSubmitting(true);

    try {
      // Simulate the processing and pass the mock response to the parent component
      setTimeout(() => {
        onSubmit(mockNlpResponse); // Pass mock data
        navigate('/output'); // Redirect to OutputPage
      }, 1000);
    } catch (err) {
      setErrorMessage(err.message || 'Failed to process input.');
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
