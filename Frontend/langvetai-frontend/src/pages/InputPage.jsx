import React, { useState } from 'react';
import '../styles/InputPage.css';

const InputPage = () => {
  const [inputText, setInputText] = useState('');
  const [inputName, setInputName] = useState('');
  const [inputFile, setInputFile] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  // Mock function to simulate processInput from an API
  const processInput = async (inputData) => {
    console.log('Processing Input:', inputData);

    // Simulate a successful response
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve({ message: 'Input processed successfully!' });
      }, 2000);
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setIsSubmitting(true);

    let fileData = null;

    if (inputFile) {
      const reader = new FileReader();
      fileData = await new Promise((resolve) => {
        reader.onload = () => resolve(btoa(reader.result)); // Convert file to Base64
        reader.readAsBinaryString(inputFile);
      });
    }

    const inputData = {
      inputText,
      inputName,
      inputFile: fileData,
    };

    try {
      const result = await processInput(inputData);
      alert(result.message); // Display success message
    } catch (err) {
      setErrorMessage(err.message || 'Failed to process input.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="container">
      <h1>LangvetAI</h1>
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
              required
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
