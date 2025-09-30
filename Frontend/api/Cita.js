
document.addEventListener('DOMContentLoaded', () => {

    const API_URL_CITAS = 'https://localhost:7172/api/Cita';
    const API_URL_SERVICIOS = 'https://localhost:7172/api/Servicio';
    const API_URL_LUGARES = 'https://localhost:7172/api/Lugar';
    const API_URL_USUARIOS = 'https://localhost:7172/api/Usuario';
    const API_URL_CONFIGURACION = 'https://localhost:7172/api/ConfiguracionTurno';

    const citasTableBody = document.getElementById('citas-table');
    const filtroFechaInput = document.getElementById('filtro-fecha');
    
    let servicios = [];
    let lugares = [];
    
    /**
     * @async
     * @function fetchRecursos
     * @description 
     */
    const fetchRecursos = async () => {
        try {
            const [serviciosRes, lugaresRes] = await Promise.all([
                fetch(API_URL_SERVICIOS),
                fetch(API_URL_LUGARES),
            ]);
            servicios = await serviciosRes.json();
            lugares = await lugaresRes.json();
        } catch (error) {
            console.error('Error al cargar servicios o lugares:', error);
        }
    };

    /**
     * @async
     * @function fetchConfiguracionById
     * @description 
     * @param {string|number} id - 
     * @returns {Promise<object|null>} 
     */
    const fetchConfiguracionById = async (id) => {
        try {
            const response = await fetch(`${API_URL_CONFIGURACION}/${id}`);
            if (!response.ok) {
                return null;
            }
            return await response.json();
        } catch (error) {
            console.error(`Error al buscar configuración de turno con ID ${id}:`, error);
            return null;
        }
    };
    
    /**
     * @async
     * @function fetchUsuarioById
     * @description 
     * @param {string|number} id - 
     * @returns {Promise<object>} 
     */
    const fetchUsuarioById = async (id) => {
       
        const fallbackUser = { nombre: 'Desconocido', correo: '' };
        try {
            const response = await fetch(`${API_URL_USUARIOS}/${id}`);
            if (!response.ok) {
                throw new Error(`Error en la respuesta de la API: ${response.status}`);
            }
            const userData = await response.json();
            
            if (userData && (userData.nombre || userData.Value)) {
                console.log(`Datos del usuario ${id} recibidos correctamente:`, userData);
                return userData;
            } else {
                console.warn(`Respuesta inesperada para el usuario con ID ${id}. La respuesta fue:`, userData);
                return fallbackUser;
            }
        } catch (error) {
            console.error(`Error al buscar usuario con ID ${id}. Causa: ${error.message}`);
            return fallbackUser;
        }
    };
    
    /**
     * @async
     * @function fetchAllCitas
     * @description 
     */
    const fetchAllCitas = async () => {
        try {
            await fetchRecursos();
            const response = await fetch(API_URL_CITAS);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const citas = await response.json();
            await renderCitasTable(citas);
        } catch (error) {
            console.error('Error al cargar las citas:', error);
            citasTableBody.innerHTML = `<tr><td colspan="6" class="text-center text-danger">Error al cargar las citas. ${error.message}</td></tr>`;
        }
    };

    /**
     * @async
     * @function fetchCitaById
     * @description .
     * @param {string|number} id 
     * @returns {Promise<object|null>}
     */
    const fetchCitaById = async (id) => {
        try {
            const response = await fetch(`${API_URL_CITAS}/${id}`);
            if (!response.ok) {
                if (response.status === 404) {
                    return null;
                }
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const cita = await response.json();
            return cita;
        } catch (error) {
            console.error(`Error al buscar la cita con ID ${id}:`, error);
            return null;
        }
    };

    /**
     * @async
     * @function renderCitasTable
     * @description 
     * @param {Array<object>} citas 
     */
    const renderCitasTable = async (citas) => {
        citasTableBody.innerHTML = '<tr><td colspan="6" class="text-center text-muted">Cargando citas...</td></tr>';
        
        if (citas.length === 0) {
            citasTableBody.innerHTML = `<tr><td colspan="6" class="text-center text-muted">No hay citas registradas.</td></tr>`;
            return;
        }


        const userIds = citas.map(cita => cita.idUsuario);
        const uniqueUserIds = [...new Set(userIds)];
        
        const usersCache = {};
        
        const userPromises = uniqueUserIds.map(id => fetchUsuarioById(id));
        
        const usersData = await Promise.all(userPromises);
        
        uniqueUserIds.forEach((id, index) => {
            usersCache[id] = usersData[index];
        });

        citasTableBody.innerHTML = '';

        for (const cita of citas) {
            const row = document.createElement('tr');
            
            const servicio = servicios.find(s => s.servicioId === cita.servicioId);
            const lugar = lugares.find(l => l.lugarId === cita.lugarId);
            
            const usuario = usersCache[cita.idUsuario];
            
            let franjaHorariaTexto = 'N/A';
            const configuracion = await fetchConfiguracionById(cita.turnoId);
            if (configuracion && configuracion.franjas) {
                const franja = configuracion.franjas.find(f => f.franjaId === cita.franjaId);
                if (franja) {
                    franjaHorariaTexto = `${franja.horaInicio.substring(0, 5)} - ${franja.horaFin.substring(0, 5)}`;
                }
            }

            const nombreServicio = servicio ? servicio.nombre : 'Desconocido';
            const nombreLugar = lugar ? lugar.nombre : 'Desconocido';

            const estadoNumero = cita.estado;
            let estadoTexto = 'Desconocido';
            let estadoClass = 'bg-secondary';
            
            switch (estadoNumero) {
                case 1:
                    estadoTexto = 'Confirmada';
                    estadoClass = 'bg-success';
                    break;
                case 2:
                    estadoTexto = 'Cancelada';
                    estadoClass = 'bg-danger';
                    break;
                default:
                    estadoTexto = 'Pendiente';
                    estadoClass = 'bg-warning';
                    break;
            }

            let nombreUsuario = 'Desconocido';
            if (usuario.nombre) {
                const regex = /{ Value = (.*) }/;
                const match = usuario.nombre.match(regex);
                if (match && match[1]) {
                    nombreUsuario = match[1];
                } else {
                    nombreUsuario = usuario.nombre;
                }
            }

            row.innerHTML = `
                <td>
                    <div class="user-info">
                        <strong>${nombreUsuario}</strong>
                    </div>
                </td>
                <td>${nombreServicio}</td>
                <td>${nombreLugar}</td>
                <td>${new Date(cita.fechaCita).toLocaleDateString()}</td>
                <td>${franjaHorariaTexto}</td>
                <td><span class="badge ${estadoClass}">${estadoTexto}</span></td>
            `;
            citasTableBody.appendChild(row);
        }
    };
    
    filtroFechaInput.addEventListener('change', async (e) => {
        const fechaSeleccionada = e.target.value;
        if (fechaSeleccionada) {
            console.log(`Buscando citas para la fecha: ${fechaSeleccionada}`);
            const idToSearch = '123'; 
            const cita = await fetchCitaById(idToSearch);
            if (cita) {
                renderCitasTable([cita]);
            } else {
                citasTableBody.innerHTML = `<tr><td colspan="6" class="text-center text-muted">No se encontró la cita con el ID ${idToSearch}.</td></tr>`;
            }
        } else {
            fetchAllCitas();
        }
    });

    fetchAllCitas();
});
