const form = document.getElementById('form');
const submitButton = form.querySelector('button[type="submit"]');
const formData = new FormData(form);
const campos = [
    'Nombre',
    'Latitud',
    'Longitud',
    'Calle',
    'Provincia',
    'Ciudad',
    'CodigoPostal',
    'DiaVencimientoExpensas'
]

const puedeActualizar = () => {
    return campos.some(campo => {
        const inputValue = form.querySelector(`#${campo}`).value;
        return formData.get(campo) !== inputValue && inputValue.trim() !== '';
    });
}

form.addEventListener('input', () => {
    submitButton.disabled = !puedeActualizar();
});

submitButton.disabled = true;


