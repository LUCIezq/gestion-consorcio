const buttons = document.querySelectorAll("button[data-action]");

const actions = {
    guardar: () => {
        guardarConsorcio();

    },
    guardarYCrearOtro: () => console.log("guardo y creo otro"),
    guardarYCrearUnidad: () => console.log("guardo y creo unidad"),
    cancelar: () => console.log("cancelando")
}

function guardarConsorcio() {
    const form = document.getElementById("formConsorcio");

    console.log(form.checkValidity());
}

buttons.forEach((button) => {
    button.addEventListener("click", (e) => {
        const action = button.getAttribute('data-action');
        if (action && actions[action]) {
            actions[action]();
        }
    })
})