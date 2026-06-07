const geojson = {
    'type': 'FeatureCollection',
    'features': [
    ]
};

const map = new mapboxgl.Map({
    accessToken: mapboxAccessToken,
    container: 'map',
    center: [-58.503610, -34.599553],
    zoom: 11
});

const obtenerCoordenadas = async () => {
    const response = await fetch('http://localhost:5202/Consorcio/ObtenerCoordenadas');
    const body = await response.json();

    if (!response.ok) {
        throw new Error(body.message);
    }

    return body;
}


const cargarMarcadores = async () => {
    const data = await obtenerCoordenadas();

    console.log(data);

    geojson.features.push(...data.map(consorcio => ({
        'type': 'Feature',
        'geometry': {
            'type': 'Point',
            'coordinates': [consorcio.longitud, consorcio.latitud]
        },
        'properties': {
            'title': consorcio.nombre,
            'calle': consorcio.calle,
            'ciudad': consorcio.ciudad
        }
    })));

    for (const feature of geojson.features) {

        const el = document.createElement('div');
        el.className = 'marker';

        new mapboxgl.Marker(el)
            .setLngLat(feature.geometry.coordinates)
            .setPopup(
                new mapboxgl.Popup({ offset: 25 })
                    .setHTML(
                        `<h3>${feature.properties.title}</h3><p>${feature.properties.calle}, ${feature.properties.ciudad}</p>`
                    )
            )
            .addTo(map);
    }

}

cargarMarcadores();


