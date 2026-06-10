
function botonAgregarArchivo() {

    var contenedor = document.getElementById("contenedorArchivo");

    if (contenedor.classList.contains("d-none")) {
        contenedor.classList.remove("d-none");
    } else {
        contenedor.classList.add("d-none");
    }
 
}
