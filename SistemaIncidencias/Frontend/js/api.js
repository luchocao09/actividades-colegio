// API Helper module for connecting Frontend to C# Backend
const API_BASE_URL = 'http://localhost:5281/api';

const API = {
  getToken: () => localStorage.getItem('token'),
  
  getUser: () => {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  },

  setAuthSession: (token, user) => {
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(user));
  },

  clearAuthSession: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  headers: (includeAuth = true) => {
    const headers = {
      'Content-Type': 'application/json'
    };
    if (includeAuth) {
      const token = API.getToken();
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }
    }
    return headers;
  },

  // HTTP Helpers
  get: async (endpoint) => {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'GET',
      headers: API.headers()
    });
    return API.handleResponse(response);
  },

  post: async (endpoint, body, includeAuth = true) => {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: API.headers(includeAuth),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  patch: async (endpoint, body) => {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'PATCH',
      headers: API.headers(),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  put: async (endpoint, body) => {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'PUT',
      headers: API.headers(),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  delete: async (endpoint) => {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      method: 'DELETE',
      headers: API.headers()
    });
    return API.handleResponse(response);
  },

  handleResponse: async (response) => {
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      const errorMsg = data.mensaje || data.title || 'Ocurrió un error en la solicitud.';
      throw new Error(errorMsg);
    }
    return data;
  }
};
