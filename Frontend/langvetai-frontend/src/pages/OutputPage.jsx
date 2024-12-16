import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../styles/OutputPage.css';

const OutputPage = ({ clusters }) => {
  const navigate = useNavigate();
  const [termDetails, setTermDetails] = useState({}); // Cache for term details
  const [error, setError] = useState('');

  const handleLogoClick = () => {
    navigate('/');
  };

  const fetchTermDetails = async (termId) => {
    if (termDetails[termId]) {
      return; 
    }

    try {
      const response = await axios.get(`http://127.0.0.1:5000/api/highlighted-terms/${termId}`);
      setTermDetails((prev) => ({
        ...prev,
        [termId]: response.data, // Cache the fetched term details
      }));
    } catch (err) {
      console.error('Error fetching term details:', err);
      setError(`Failed to fetch details for term ID: ${termId}`);
    }
  };

  return (
    <div className="output-container">
      <header className="header">
        <div className="logo-container" onClick={handleLogoClick}>
          <img src="/logo2.svg" alt="LangvetAI Logo" className="logo" />
          <h1 id="LangvetAI">LANGVET-AI</h1>
        </div>
      </header>

      <h1>NLP Output</h1>
      {error && <p className="error">{error}</p>}

      <div id="clusters-list">
        {clusters && clusters.length > 0 ? (
          clusters.map((cluster, clusterIndex) => (
            <div key={clusterIndex} className="cluster-card">
              <h2>Cluster {clusterIndex + 1}</h2>
              {cluster.summary && (
                <p>
                  <strong>Summary:</strong> {cluster.summary}
                </p>
              )}
              <div className="nodes-list">
                {cluster.nodes.map((node, nodeIndex) => (
                  <div
                    key={nodeIndex}
                    className="term-card"
                    onClick={() => fetchTermDetails(node.term_id)} // Trigger fetch on click or hover
                  >
                    <h3>{node.term_name}</h3>
                    <p>
                      <strong>Context:</strong> {node.context}
                    </p>
                    <p>
                      <strong>Term ID:</strong> {node.term_id}
                    </p>
                    {/* Show term details if available in the cache */}
                    {termDetails[node.term_id] && (
                      <div className="term-details">
                        <p>
                          <strong>Definition:</strong>{' '}
                          {termDetails[node.term_id].termDefinition || 'N/A'}
                        </p>
                        <p>
                          <strong>Description:</strong>{' '}
                          {termDetails[node.term_id].termDescription || 'N/A'}
                        </p>
                      </div>
                    )}
                  </div>
                ))}
              </div>
            </div>
          ))
        ) : (
          <p>No clusters found.</p>
        )}
      </div>
    </div>
  );
};

export default OutputPage;
