import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../styles/OutputPage.css';

const OutputPage = ({ clusters }) => {
  const navigate = useNavigate();
  const [termDetails, setTermDetails] = useState({}); // Cache for term details
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false); // To show loading indicator

  const handleLogoClick = () => {
    navigate('/');
  };

  // Function to fetch term details for a list of terms
  const getTermsDetails = async (terms) => {
    try {
      setIsLoading(true); // Start loading indicator
      const termDetails = await Promise.all(
        terms.map(async (termId) => {
          const response = await axios.get(`https://localhost:7231/api/v1/HighlightedTerm/id/${termId}`);
          console.log("Fetched term details for ID", termId, response.data);
          return { termId, ...response.data }; // Include termId for later reference
        })
      );
      
      const termDetailsMap = termDetails.reduce((acc, termDetail) => {
        acc[termDetail.termId] = termDetail;
        return acc;
      }, {});
      
      setTermDetails((prevDetails) => ({
        ...prevDetails,
        ...termDetailsMap, // Merge with existing term details in state
      }));
    } catch (err) {
      console.error('Error fetching term details:', err);
      setError('Failed to fetch term details.');
    } finally {
      setIsLoading(false); // End loading indicator
    }
  };

  // Use useEffect to fetch details for all terms when clusters are loaded
  useEffect(() => {
    const termIds = clusters.flatMap(cluster => cluster.nodes.map(node => node.term_id));
    getTermsDetails(termIds);
  }, [clusters]); // Fetch term details when clusters change

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
      {isLoading && <p>Loading term details...</p>} {/* Loading message */}

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
                {cluster.nodes.map((node) => (
                  <div
                    key={node.term_id}
                    className="term-card"
                  >
                    <h3>{node.term_name}</h3>
                    <p><strong>Context:</strong> {node.context}</p>
                    <p><strong>Term ID:</strong> {node.term_id}</p>

                    {/* Show term details if available in the cache */}
                    {termDetails[node.term_id] && (
                      <div className="term-details">
                        
                        <p><strong>Definition:</strong> {termDetails[node.term_id].termDefinition || 'N/A'}</p>
                        <p><strong>Description:</strong> {termDetails[node.term_id].termDescription || 'N/A'}</p>
                        {termDetails[node.term_id].termLink && (
                          <p>
                            <strong>Link:</strong>{' '}
                            <a href={termDetails[node.term_id].termLink} target="_blank" rel="noopener noreferrer">
                              {termDetails[node.term_id].termLink}
                            </a>
                          </p>
                        )}
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
