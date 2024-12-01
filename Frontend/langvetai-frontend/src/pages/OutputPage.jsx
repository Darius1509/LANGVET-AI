import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/OutputPage.css';

const OutputPage = ({ terms }) => {
  const navigate = useNavigate();

  const handleLogoClick = () => {
    navigate('/');
  };

  console.log("Terms passed to OutputPage:", terms);

  return (
    <div className="output-container">
      <header className="header">
        <div className="logo-container" onClick={handleLogoClick}>
          <img 
            src="/logo2.svg" 
            alt="LangvetAI Logo" 
            className="logo" 
            onClick={handleLogoClick} 
          />
          <h1 id="LangvetAI" onClick={handleLogoClick}>
            LANGVET-AI
          </h1>
        </div>
      </header>

      <h1>NLP Output</h1>
      <div id="terms-list">
        {terms && terms.length > 0 ? (
          terms.map((term, index) => (
            <div key={index} className="term-card">
              <h2>{term.termName}</h2>
              <p><strong>Definition:</strong> {term.termDefinition}</p>
              <p><strong>Description:</strong> {term.termDescription || "No description available."}</p>
              <p>
                <strong>Link:</strong>{' '}
                <a href={term.termLink || "#"} target="_blank" rel="noopener noreferrer">
                  {term.termLink || "No link available"}
                </a>
              </p>
              <p><strong>SubCluster:</strong> {term.termSubCluster || "Not available"}</p>
            </div>
          ))
        ) : (
          <p>No terms found.</p>
        )}
      </div>
    </div>
  );
};

export default OutputPage;
