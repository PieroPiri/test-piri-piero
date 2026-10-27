const API_URL = "http://localhost:5270/api";

async function obtener(url) {
    const respuesta = await fetch(url);
    if (!respuesta.ok) {
        throw new Error(`Error ${respuesta.status} al consultar ${url}`);
    }
    return respuesta.json();
}

function mostrarError(mensaje) {
    const divError = document.getElementById("error");
    divError.textContent = mensaje;
    divError.classList.remove("oculto");
}

function mostrarTalleres(talleres) {
    const tabla = document.getElementById("tabla-talleres");
    tabla.innerHTML = "";
    if (talleres.length === 0) {
        tabla.innerHTML = "<tr><td colspan='4'>No hay talleres cargados.</td></tr>";
        return;
    }
    talleres.forEach(t => {
        const fila = document.createElement("tr");

        const celdaNombre = document.createElement("td");
        celdaNombre.textContent = t.nombre;

        const celdaDia = document.createElement("td");
        celdaDia.textContent = t.dia;

        const celdaHorario = document.createElement("td");
        celdaHorario.textContent = `${t.horaInicio}:00 a ${t.horaFin}:00`;

        const celdaCupos = document.createElement("td");
        celdaCupos.textContent = `${t.cuposDisponibles} de ${t.cupo}`;
        if (t.cuposDisponibles === 0) {
            celdaCupos.className = "sin-cupo";
        }

        fila.append(celdaNombre, celdaDia, celdaHorario, celdaCupos);
        tabla.appendChild(fila);
    });
}

async function cargarTalleres() {
    try {
        const talleres = await obtener(`${API_URL}/talleres`);
        mostrarTalleres(talleres);
    } catch (error) {
        mostrarError(`No se pudieron cargar los talleres: ${error.message}`);
    }
}

document.addEventListener("DOMContentLoaded", cargarTalleres);
