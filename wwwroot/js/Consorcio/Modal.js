
document.addEventListener('DOMContentLoaded', () => {

    const modal = document.getElementById('eliminarModal');

    modal.addEventListener('show.bs.modal', event => {

        const button = event.relatedTarget;

        const id = button.getAttribute('data-id');
        const nombre = button.getAttribute('data-nombre');

        document.getElementById('consorcioId').value = id;

        document.getElementById('mensajeEliminar').textContent =
            `¿Está seguro que desea eliminar el consorcio "${nombre}"?`;
    });

});