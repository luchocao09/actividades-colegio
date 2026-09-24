document.addEventListener('DOMContentLoaded', () => {
  // Redireccionar si ya está autenticado
  if (API.getToken()) {
    window.location.href = 'dashboard.html';
    return;
  }

  const loginForm = document.getElementById('login-form');
  const registerForm = document.getElementById('register-form');
  const toggleAuthBtn = document.getElementById('toggle-auth');
  const switchText = document.getElementById('switch-text');
  const alertBox = document.getElementById('alert-box');

  let isLogin = true;

  const showAlert = (message, isError = true) => {
    alertBox.classList.remove('is-error', 'is-success');
    alertBox.classList.add('is-visible', isError ? 'is-error' : 'is-success');
    alertBox.innerText = message;
  };

  const hideAlert = () => {
    alertBox.classList.remove('is-visible', 'is-error', 'is-success');
  };

  // Toggle entre Login y Registro
  const toggleAuth = (e) => {
    e.preventDefault();
    hideAlert();
    isLogin = !isLogin;
    if (isLogin) {
      loginForm.style.display = 'block';
      registerForm.style.display = 'none';
      switchText.innerHTML = '¿No tienes una cuenta? <a id="toggle-auth">Regístrate</a>';
    } else {
      loginForm.style.display = 'none';
      registerForm.style.display = 'block';
      switchText.innerHTML = '¿Ya tienes una cuenta? <a id="toggle-auth">Inicia sesión</a>';
    }
    const newToggleBtn = document.getElementById('toggle-auth');
    if (newToggleBtn) {
      newToggleBtn.addEventListener('click', toggleAuth);
    }
  };

  toggleAuthBtn.addEventListener('click', toggleAuth);

  // Login Submit
  loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    hideAlert();

    const email = document.getElementById('login-email').value;
    const password = document.getElementById('login-password').value;

    try {
      const data = await API.post('/auth/login', { email, password }, false);
      API.setAuthSession(data.token, {
        id: data.id,
        nombreCompleto: data.nombreCompleto,
        email: data.email,
        rol: data.rol
      });
      window.location.href = 'dashboard.html';
    } catch (error) {
      showAlert(error.message);
    }
  });

  // Register Submit
  registerForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    hideAlert();

    const nombreCompleto = document.getElementById('reg-name').value;
    const email = document.getElementById('reg-email').value;
    const password = document.getElementById('reg-password').value;
    const rol = document.getElementById('reg-role').value;

    try {
      const data = await API.post('/auth/register', { nombreCompleto, email, password, rol }, false);
      API.setAuthSession(data.token, {
        id: data.id,
        nombreCompleto: data.nombreCompleto,
        email: data.email,
        rol: data.rol
      });
      window.location.href = 'dashboard.html';
    } catch (error) {
      showAlert(error.message);
    }
  });
});
