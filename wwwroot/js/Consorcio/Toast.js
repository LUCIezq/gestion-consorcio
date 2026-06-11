document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll('.toast').forEach(toastElement => {

        const toast = new bootstrap.Toast(toastElement, {
            delay: 5000
        });

        toast.show();
    });

});