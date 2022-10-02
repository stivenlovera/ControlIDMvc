$(document).on('click', '#probar_conexion', function () {

    $('#loading').show();
    $.ajax({
        type: 'POST',
        url: `/dispositivo/probar-conexion`,
        dataType: "json",
        data: $('#formulario_dispositivo').serialize(),
        success: function (response) {
            if (response.errors) {
                Swal.fire({
                    icon: 'error',
                    title: response.message,
                    html: validateError(response.errors)
                });

            } else {

                Swal.fire({
                    icon: 'success',
                    title: response.message,
                    html: response.descripcion,
                    showConfirmButton: true,
                })
            }
            $('#loading').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    })
});
/* validar errores */
function validateError(errors) {
    let resultado = "";
    let claves = Object.keys(errors); // claves = ["nombre", "color", "macho", "edad"]
    for (let i = 0; i < claves.length; i++) {
        let clave = claves[i];
        resultado+=`${errors[clave].errors[0].errorMessage}<br>`;
    }
    return resultado
}
function isKeyExists(obj, key) {
    return key in obj;
}
