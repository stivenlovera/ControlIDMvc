function error_status(jqXHR) {
    switch (jqXHR.status) {
        case 419:
            Swal.fire({
                icon: 'error',
                title: 'Expired session',
                html: 'Please reload the page'
            });
            break;
        case 401:
            Swal.fire({
                icon: 'error',
                title: 'Expired session',
                html: 'Please reload the page'
            });
            break;
        default:
            Swal.fire({
                icon: 'error',
                title: 'An error occurred on the server',
                html: 'error',
            });
            break;
    }
}
function fail() {
    Swal.fire({
        icon: 'error',
        title: 'An error occurred',
        html: '',
    });
}
function validateError(errors) {
    let resultado = "";
    let claves = Object.keys(errors); // claves = ["nombre", "color", "macho", "edad"]
    for (let i = 0; i < claves.length; i++) {
        console.log(claves[i])
        let clave = claves[i];
        if (errors[clave].errors.length > 0) {
            resultado += `${errors[clave].errors[0].errorMessage}<br>`;
        }
    }
    return resultado
}