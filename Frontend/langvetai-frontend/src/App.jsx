import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import InputPage from './pages/InputPage';
import OutputPage from './pages/OutputPage';

const App = () => {
  const [terms, setTerms] = useState([]);

  const handleTermsSubmit = (termsDetails) => {
    setTerms(termsDetails);
  };

  return (
    <Router>
      <Routes>
        <Route path="/" element={<InputPage onSubmit={handleTermsSubmit} />} />
        <Route path="/output" element={<OutputPage terms={terms} />} />
      </Routes>
    </Router>
  );
};

export default App;
