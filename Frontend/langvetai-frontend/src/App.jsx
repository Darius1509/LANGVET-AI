import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import InputPage from './pages/InputPage';
import OutputPage from './pages/OutputPage';

const App = () => {
  const [outputData, setOutputData] = useState({ clusters: [], terms: [] });

  const handleInputSubmit = (data) => {
    setOutputData(data);
  };

  return (
    <Router>
      <Routes>
        <Route path="/" element={<InputPage onSubmit={handleInputSubmit} />} />
        <Route path="/output" element={<OutputPage clusters={outputData.clusters} />} />
      </Routes>
    </Router>
  );
};

export default App;
