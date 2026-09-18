document.addEventListener('DOMContentLoaded', () => {
  // Verificar autenticación
  const currentUser = API.getUser();
  if (!API.getToken() || !currentUser) {
    window.location.href = 'index.html';
    return;
  }

  // Elementos DOM
  const currentUserAvatar = document.getElementById('current-user-avatar');
  const currentUserName = document.getElementById('current-user-name');
  const currentUserRole = document.getElementById('current-user-role');
  const feedContainer = document.getElementById('incidents-feed');
  
  const searchInput = document.getElementById('search-input');
  const statusFilter = document.getElementById('status-filter');
  const categoryItems = document.querySelectorAll('.category-item');

  const navNewIncident = document.getElementById('nav-new-incident');
  const navLogout = document.getElementById('nav-logout');

  const modalCreate = document.getElementById('modal-create-incident');
  const btnCloseModal = document.getElementById('btn-close-modal');
  const createForm = document.getElementById('create-incident-form');

  const modalStatus = document.getElementById('modal-change-status');
  const btnCloseStatusModal = document.getElementById('btn-close-status-modal');
  const changeStatusForm = document.getElementById('change-status-form');
  const statusIncidenciaId = document.getElementById('status-incidencia-id');
  const selectNuevoEstado = document.getElementById('select-nuevo-estado');

  // Estado local de filtros
  let selectedCategory = '';
  let selectedStatus = '';
  let searchQuery = '';

  // Inicializar Info de Usuario en Sidebar
  currentUserName.innerText = currentUser.nombreCompleto || 'Usuario';
  currentUserRole.innerText = currentUser.rol || 'Ciudadano';
  currentUserAvatar.innerText = (currentUser.nombreCompleto || 'U').charAt(0).toUpperCase();

  // Cargar Incidencias del Backend
  const loadIncidents = async () => {
    feedContainer.innerHTML = '<div style="text-align: center; padding: 40px; color: var(--text-muted);">Cargando incidencias...</div>';

    try {
      let queryParams = [];
      if (selectedStatus) queryParams.push(`estado=${encodeURIComponent(selectedStatus)}`);
      if (selectedCategory) queryParams.push(`categoria=${encodeURIComponent(selectedCategory)}`);
      if (searchQuery) queryParams.push(`buscar=${encodeURIComponent(searchQuery)}`);

      const url = `/incidencias${queryParams.length > 0 ? '?' + queryParams.join('&') : ''}`;
      const incidencias = await API.get(url);

      renderFeed(incidencias);
    } catch (error) {
      feedContainer.innerHTML = `<div style="text-align: center; padding: 40px; color: var(--danger-color);">Error al cargar incidencias: ${error.message}</div>`;
    }
  };

  // Renderizar publicaciones estilo Instagram Card
  const renderFeed = (incidencias) => {
    if (!incidencias || incidencias.length === 0) {
      feedContainer.innerHTML = `
        <div class="post-card" style="padding: 40px; text-align: center; color: var(--text-muted);">
          📷 No hay incidencias para mostrar con los filtros seleccionados.
        </div>
      `;
      return;
    }

    feedContainer.innerHTML = incidencias.map(inc => {
      const userInitial = inc.usuario && inc.usuario.nombreCompleto ? inc.usuario.nombreCompleto.charAt(0).toUpperCase() : 'U';
      const userName = inc.usuario ? inc.usuario.nombreCompleto : 'Ciudadano Anónimo';
      const estadoClass = `badge-${(inc.estado || '').toLowerCase().replace(/\s+/g, '')}`;
      
      const direccionTexto = `${inc.calle} ${inc.altura}${inc.entreCalles ? ' (' + inc.entreCalles + ')' : ''}, ${inc.localidad || 'Morón'}`;
      const fechaFormat = new Date(inc.fechaReporte).toLocaleDateString('es-AR', { day: 'numeric', month: 'short', hour: '2-digit', minute: '2-digit' });

      const canManage = currentUser.rol === 'Administrador' || (inc.usuario && inc.usuario.id === currentUser.id);

      return `
        <article class="post-card" data-id="${inc.id}">
          <!-- Header de la Publicación -->
          <div class="post-header">
            <div class="post-user">
              <div class="user-avatar">${userInitial}</div>
              <div class="user-info">
                <span class="user-name">${escapeHtml(userName)}</span>
                <span class="post-location">📍 ${escapeHtml(direccionTexto)}</span>
              </div>
            </div>
            <span class="badge-status ${estadoClass}">${escapeHtml(inc.estado)}</span>
          </div>

          <!-- Imagen si existe -->
          ${inc.imagenUrl ? `
            <div class="post-image-container">
              <img src="${escapeHtml(inc.imagenUrl)}" alt="Foto Incidencia" class="post-image" onerror="this.parentElement.style.display='none'">
            </div>
          ` : ''}

          <!-- Botones de Acción -->
          <div class="post-actions">
            <div class="action-btns">
              <span class="category-label" style="font-weight: 600; background: rgba(0,0,0,0.05); padding: 4px 10px; border-radius: 12px;">🏷️ ${escapeHtml(inc.categoria)}</span>
            </div>
            ${canManage ? `
              <div class="action-btns">
                <button class="action-btn btn-change-status" data-id="${inc.id}" data-estado="${inc.estado}" title="Cambiar Estado">⚙️</button>
                <button class="action-btn btn-delete-inc" data-id="${inc.id}" title="Eliminar Reporte" style="color: var(--danger-color);">🗑️</button>
              </div>
            ` : ''}
          </div>

          <!-- Cuerpo / Descripción -->
          <div class="post-body">
            <h4 class="post-title">${escapeHtml(inc.titulo)}</h4>
            <p class="post-caption">${escapeHtml(inc.descripcion)}</p>
            <span class="post-time">${fechaFormat}</span>
          </div>
        </article>
      `;
    }).join('');

    // Re-bind click events para acciones
    document.querySelectorAll('.btn-change-status').forEach(btn => {
      btn.addEventListener('click', (e) => {
        const id = e.currentTarget.dataset.id;
        const estadoActual = e.currentTarget.dataset.estado;
        statusIncidenciaId.value = id;
        selectNuevoEstado.value = estadoActual;
        modalStatus.classList.add('active');
      });
    });

    document.querySelectorAll('.btn-delete-inc').forEach(btn => {
      btn.addEventListener('click', async (e) => {
        const id = e.currentTarget.dataset.id;
        if (confirm('¿Estás seguro de que deseas borrar lógicamente esta incidencia?')) {
          try {
            await API.delete(`/incidencias/${id}`);
            loadIncidents();
          } catch (err) {
            alert('Error al eliminar: ' + err.message);
          }
        }
      });
    });
  };

  const escapeHtml = (str) => {
    if (!str) return '';
    return String(str)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;');
  };

  // Eventos de Categoría (Stories)
  categoryItems.forEach(item => {
    item.addEventListener('click', () => {
      categoryItems.forEach(c => c.classList.remove('active'));
      item.classList.add('active');
      selectedCategory = item.dataset.categoria;
      loadIncidents();
    });
  });

  // Evento de Estado
  statusFilter.addEventListener('change', (e) => {
    selectedStatus = e.target.value;
    loadIncidents();
  });

  // Evento de Búsqueda
  let debounceTimer;
  searchInput.addEventListener('input', (e) => {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(() => {
      searchQuery = e.target.value.trim();
      loadIncidents();
    }, 400);
  });

  // Abrir y Cerrar Modales
  navNewIncident.addEventListener('click', () => {
    modalCreate.classList.add('active');
  });

  btnCloseModal.addEventListener('click', () => {
    modalCreate.classList.remove('active');
  });

  btnCloseStatusModal.addEventListener('click', () => {
    modalStatus.classList.remove('active');
  });

  // Submit Nuevo Reporte
  createForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const dto = {
      titulo: document.getElementById('inc-titulo').value,
      categoria: document.getElementById('inc-categoria').value,
      calle: document.getElementById('inc-calle').value,
      altura: document.getElementById('inc-altura').value,
      entreCalles: document.getElementById('inc-entrecalles').value,
      localidad: document.getElementById('inc-localidad').value,
      descripcion: document.getElementById('inc-descripcion').value,
      imagenUrl: document.getElementById('inc-imagen').value || null
    };

    try {
      await API.post('/incidencias', dto);
      modalCreate.classList.remove('active');
      createForm.reset();
      loadIncidents();
    } catch (err) {
      alert('Error al crear reporte: ' + err.message);
    }
  });

  // Submit Cambiar Estado
  changeStatusForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const id = statusIncidenciaId.value;
    const nuevoEstado = selectNuevoEstado.value;

    try {
      await API.patch(`/incidencias/${id}/estado`, { estado: nuevoEstado });
      modalStatus.classList.remove('active');
      loadIncidents();
    } catch (err) {
      alert('Error al actualizar estado: ' + err.message);
    }
  });

  // Logout
  navLogout.addEventListener('click', () => {
    API.clearAuthSession();
    window.location.href = 'index.html';
  });

  // Carga Inicial
  loadIncidents();
});
