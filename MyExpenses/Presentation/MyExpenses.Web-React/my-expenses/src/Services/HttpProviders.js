import axios from 'axios';

// Get the token from localStorage or any other storage


// Set up an Axios instance
const api = axios.create({
  baseURL: 'https://localhost:7203/api',
});

// Add a request interceptor to include the token
api.interceptors.request.use((config) => {
  // If token exists, add it to the headers
  const token = localStorage.getItem('token');
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

export const get= (endpoint) => {
    return api.get(endpoint);
}

export const post = (endpoint, data) => {
    return api.post(endpoint, data);
}

export const put = (endpoint, data) => {
    return api.put(endpoint, data);
}

 export const remove = (endpoint) => {
    return api.delete(endpoint);
}
