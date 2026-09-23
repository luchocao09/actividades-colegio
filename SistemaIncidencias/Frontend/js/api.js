// Servicio centralizado para interactuar con la API del Backend (C#)

const API = {
  // Manejo de Sesión / LocalStorage
  getToken: () => localStorage.getItem(CONFIG.STORAGE_KEYS.TOKEN),
  
  getUser: () => {
    const userStr = localStorage.getItem(CONFIG.STORAGE_KEYS.USER);
    try {
      return userStr ? JSON.parse(userStr) : null;
    } catch {
      return null;
    }
  },

  setAuthSession: (token, user) => {
    localStorage.setItem(CONFIG.STORAGE_KEYS.TOKEN, token);
    localStorage.setItem(CONFIG.STORAGE_KEYS.USER, JSON.stringify(user));
  },

  clearAuthSession: () => {
    localStorage.removeItem(CONFIG.STORAGE_KEYS.TOKEN);
    localStorage.removeItem(CONFIG.STORAGE_KEYS.USER);
  },

  // Generador de Headers HTTP
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

  // Métodos HTTP genéricos
  get: async (endpoint) => {
    const response = await fetch(`${CONFIG.API_BASE_URL}${endpoint}`, {
      method: 'GET',
      headers: API.headers()
    });
    return API.handleResponse(response);
  },

  post: async (endpoint, body, includeAuth = true) => {
    const response = await fetch(`${CONFIG.API_BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: API.headers(includeAuth),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  patch: async (endpoint, body) => {
    const response = await fetch(`${CONFIG.API_BASE_URL}${endpoint}`, {
      method: 'PATCH',
      headers: API.headers(),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  put: async (endpoint, body) => {
    const response = await fetch(`${CONFIG.API_BASE_URL}${endpoint}`, {
      method: 'PUT',
      headers: API.headers(),
      body: JSON.stringify(body)
    });
    return API.handleResponse(response);
  },

  delete: async (endpoint) => {
    const response = await fetch(`${CONFIG.API_BASE_URL}${endpoint}`, {
      method: 'DELETE',
      headers: API.headers()
    });
    return API.handleResponse(response);
  },

  // Procesador unificado de respuestas HTTP
  handleResponse: async (response) => {
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      // Si el token venció o es inválido, cerramos sesión automáticamente
      if (response.status === 401 && API.getToken()) {
        API.clearAuthSession();
        window.location.href = 'index.html';
      }
      const errorMsg = data.mensaje || data.title || (data.errors ? Object.values(data.errors).flat().join(', ') : 'Ocurrió un error en la solicitud.');
      throw new Error(errorMsg);
    }
    return data;
  }
};
