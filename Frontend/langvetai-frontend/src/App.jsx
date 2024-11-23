import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes, useNavigate } from 'react-router-dom';
import InputPage from './pages/InputPage';
import OutputPage from './pages/OutputPage';

const App = () => {
  const [nlpOutput, setNlpOutput] = useState(null);

  return (
    <Router>
      <Routes>
        {/* Pass the mock data handler as a prop to InputPage */}
        <Route
          path="/"
          element={<InputPage onSubmit={(data) => setNlpOutput(data)} />}
        />
        {/* Pass the NLP output data as a prop to OutputPage */}
        <Route
          path="/output"
          element={<OutputPage terms={nlpOutput?.terms || []} />}
        />
      </Routes>
    </Router>
  );
};

export default App;
