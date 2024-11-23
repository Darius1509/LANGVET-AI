import React from 'react';
import '../styles/OutputPage.css';

const OutputPage = ({ terms }) => {
  return (
    <div className="output-container">
    <header className="header">
        <div className="logo-container">
          <img src="/logo2.svg" alt="LangvetAI Logo" className="logo" />
          <h1 id="LangvetAI">LANGVET-AI</h1>
        </div>
      </header>

      <h1>NLP Output</h1>
      <div id="terms-list">
        {terms && terms.length > 0 ? (
          terms.map((term, index) => (
            <div key={index} className="term-card">
              <h2>{term.termName}</h2>
              <p><strong>Definition:</strong> {term.termDefinition}</p>
              <p><strong>Description:</strong> {term.termDescription}</p>
              <p>
                <strong>Link:</strong>{' '}
                <a href={term.termLink} target="_blank" rel="noopener noreferrer">
                  {term.termLink}
                </a>
              </p>
              <p><strong>SubCluster:</strong> {term.termSubCluster}</p>
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
