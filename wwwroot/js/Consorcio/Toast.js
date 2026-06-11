document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll('.toast').forEach(toastElement => {

        const toast = new bootstrap.Toast(toastElement, {
            delay: 2000
        });

        toast.show();
    });

});