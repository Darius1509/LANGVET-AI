import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';
import axios from 'axios';
import InputPage from '.././pages/InputPage';

// Mock axios
jest.mock('axios');

// Mock react-router-dom's useNavigate
const mockNavigate = jest.fn();
jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: () => mockNavigate,
}));

describe('InputPage', () => {
  const mockOnSubmit = jest.fn();

  beforeEach(() => {
    jest.clearAllMocks(); // Reset mocks before each test
  });

  test('renders the input form', () => {
    render(
      <Router>
        <InputPage onSubmit={mockOnSubmit} />
      </Router>
    );

    // Check if input form elements are rendered
    expect(screen.getByText(/LANGVET-AI/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Input Text:/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /Submit/i })).toBeInTheDocument();
  });

  test('disables the button while submitting', async () => {
    render(
      <Router>
        <InputPage onSubmit={mockOnSubmit} />
      </Router>
    );

    const button = screen.getByRole('button', { name: /Submit/i });

    // Simulate button click
    fireEvent.submit(button);

    // Check if button is disabled while submitting
    expect(button).toBeDisabled();
  });

  test('calls onSubmit and navigates on successful API response', async () => {
    // Mock successful API response
    axios.get.mockResolvedValueOnce({
      data: {
        clusters: ['cluster1', 'cluster2'],
        terms: ['term1', 'term2'],
      },
    });

    render(
      <Router>
        <InputPage onSubmit={mockOnSubmit} />
      </Router>
    );

    // Simulate input change and form submission
    const input = screen.getByLabelText(/Input Text:/i);
    fireEvent.change(input, { target: { value: 'test input' } });
    fireEvent.click(screen.getByRole('button', { name: /Submit/i }));

    // Wait for API response and DOM updates
    await waitFor(() => {
      // Check if onSubmit was called with the correct data
      expect(mockOnSubmit).toHaveBeenCalledWith({
        clusters: ['cluster1', 'cluster2'],
        terms: ['term1', 'term2'],
      });

      // Check if navigate was called to redirect
      expect(mockNavigate).toHaveBeenCalledWith('/output');
    });
  });

  test('displays an error message on API failure', async () => {
    // Mock API failure
    axios.get.mockRejectedValueOnce(new Error('API error'));

    render(
      <Router>
        <InputPage onSubmit={mockOnSubmit} />
      </Router>
    );

    // Simulate input change and form submission
    const input = screen.getByLabelText(/Input Text:/i);
    fireEvent.change(input, { target: { value: 'test input' } });
    fireEvent.click(screen.getByRole('button', { name: /Submit/i }));

    // Wait for error message to appear
    await waitFor(() => {
      // Check if error message is displayed
      expect(screen.getByText(/Failed to fetch terms. Please try again./i)).toBeInTheDocument();
    });

    // Ensure onSubmit was not called
    expect(mockOnSubmit).not.toHaveBeenCalled();
  });
});
