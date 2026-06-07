import { URLS_MAPBOX } from '../env/urls.js';

const latitudInput = document.getElementById('Latitud');
const longitudInput = document.getElementById('Longitud');

window.addEventListener('load', () => {
    const collection = mapboxsearch.autofill({
        accessToken: mapboxAccessToken,
        options: {
            country: 'AR',
            language: 'es',
            types: ['address', 'poi', 'place'],
            limit: 5
        }
    });

    collection.addEventListener('retrieve', (e) => {
        const [lng, lat] = e.detail.features[0].geometry.coordinates;

        const calleInput = document.getElementById('Calle');
        calleInput.value = e.detail.features[0].properties.address_line1 || '';

        latitudInput.value = lat.toString().replace('.', ',');
        longitudInput.value = lng.toString().replace('.', ',');
    });
});

document.getElementById("form").addEventListener("submit", async function (e) {
    e.preventDefault();

    const formulario = document.getElementById("form");

    if (latitudInput.value && longitudInput.value) {

        formulario.submit();
        return;
    }

    const calle = document.getElementById('Calle');
    const ciudad = document.getElementById('Ciudad');
    const provincia = document.getElementById('Provincia');
    const codigoPostal = document.getElementById('CodigoPostal');

    const query = `${calle.value}, ${ciudad.value}, ${provincia.value}, ${codigoPostal.value}`;
    const sessionToken = crypto.randomUUID();

    try {
        const response = await fetch(URLS_MAPBOX.obtenerDireccion(query, mapboxAccessToken, sessionToken));

        const body = await response.json();
        console.log(body);
        if (!response.ok) {
            throw new Error(
                body.message.error
            );
        }

        if (body.suggestions.length === 0) {
            throw new Error('No se encontraron resultados para la dirección proporcionada.');
        }

        const mapBoxId = body.suggestions[0].mapbox_id;

        calle.value = body.suggestions[0].address || '';
        ciudad.value = body.suggestions[0].context.place.name || '';
        provincia.value = body.suggestions[0].context.region.name || '';
        codigoPostal.value = body.suggestions[0].context.postcode.name || '';

        const { latitud, longitud } = await obtenerCoordenadas(mapBoxId, sessionToken);

        latitudInput.value = latitud;
        longitudInput.value = longitud;

        formulario.submit();

    } catch (error) {
        console.error('Error al obtener las coordenadas:', error);
    }
});

async function obtenerCoordenadas(mapBoxId, sessionToken) {

    try {
        const response = await fetch(URLS_MAPBOX.obtenerCoordenadas(mapBoxId, mapboxAccessToken, sessionToken));
        const body = await response.json();

        if (!response.ok) {
            throw new Error(
                body.message
            );
        }

        const [lng, lat] = body.features[0].geometry.coordinates;

        return {
            latitud: lat.toString().replace('.', ','),
            longitud: lng.toString().replace('.', ',')
        }

    } catch (error) {
        console.error('Error al obtener las coordenadas:', error);
    }
}