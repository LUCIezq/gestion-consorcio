window.addEventListener('load', () => {
    const collection = mapboxsearch.autofill({
        accessToken: mapboxAccessToken,
        options: {
            country: 'AR',
            language: 'es',
        }
    });

    collection.addEventListener('retrieve', (e) => {
        const [lng, lat] = e.detail.features[0].geometry.coordinates;

        const calleInput = document.getElementById('Calle');
        calleInput.value = e.detail.features[0].properties.address_line1 || '';

        console.log(e)

        document.getElementById('latitud').value = lat.toString().replace('.', ',');
        document.getElementById('longitud').value = lng.toString().replace('.', ',');
    });
});

document.getElementById("form").addEventListener("submit", function (e) {
    e.preventDefault();

    const data = new FormData(this);
    const obj = Object.fromEntries(data.entries());

    console.log(obj);
});






