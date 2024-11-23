import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import InputPage from './pages/InputPage';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<InputPage />} />
        <Route path="/input" element={<InputPage />} />
      </Routes>
    </Router>
  );
};

export default App;
