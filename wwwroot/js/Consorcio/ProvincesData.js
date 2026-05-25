import { URLS } from "../env/urls.js";

const provinciaSelect = document.getElementById('provinciaSelect');
const ciudadesSelect = document.getElementById('ciudadSelect');

loadProvincesToSelect();

async function loadProvincesToSelect() {
    try {
        const data = await getProvinces();

        data.provincias.forEach(e => {
            createElement(e, provinciaSelect);
        });

    } catch (error) {
        console.error('Error al cargar las provincias:', error);
    }
}

function createElement(e, node) {
    const option = document.createElement('option');

    option.value = e.id;
    option.textContent = e.nombre;

    node.appendChild(option);
}

async function getProvinces() {
    const response = await fetch(URLS.provincias);

    if (!response.ok) {
        throw new Error(`Error al obtener las provincias: ${response.status} ${response.statusText}`);
    }
    return await response.json();
}

async function obtenerCantidadCiudadesPorProvincia(id, max = 1) {
    const URL_CIUDADES = URLS.municipios(id, max);

    const data = await fetch(URL_CIUDADES);

    if (!data.ok) {
        throw new Error(`Error al obtener las ciudades: ${data.status} ${data.statusText}`);
    }

    return await data.json();

}

function clearSelect(select) {
    while (select.options.length > 1) {
        select.remove(1);
    }
}

provinciaSelect.addEventListener('change', async e => {
    clearSelect(ciudadesSelect);

    const id = provinciaSelect.value;
    const data = await obtenerCantidadCiudadesPorProvincia(id);
    const total = data.total;

    const dataJSON = await obtenerCantidadCiudadesPorProvincia(id, total);

    dataJSON.municipios.forEach(e => {
        createElement(e, ciudadesSelect);
    })
})