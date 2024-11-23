import axios from 'axios';

const API = axios.create({
    baseURL: 'http://localhost:5000/api', // Adjust backend URL.
});

export const createInput = (data) => API.post('/inputs', data);
export const updateInput = (id, data) => API.put(`/inputs/${id}`, data);
export const getHighlightedTerms = () => API.get('/highlighted-terms');
