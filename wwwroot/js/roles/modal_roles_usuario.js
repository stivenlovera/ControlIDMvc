$(document).on("click", ".modal_persona", function () {
    console.log($(this).data('id'))
    $('#modal_usuario_rol').modal('show');
    $.ajax({
        type: 'GET',
        url: `/roles/show/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            $('#UsuarioId').val(response.data.ci);
            $('#Id').val(response.data.id);
            $('#Ci').val(response.data.ci);
            $('#PersonaId').val(response.data.personaId);
            $('#User').val(response.data.usuario);
            $('#Password').val(response.data.password);
            $('#nombres').val(response.data.nombre);
            $('#apellidos').val(response.data.apellido);
            $('#direccion').val(response.data.direccion); 
            let option=[];
            response.data.roles.forEach(rol => {
                option.push( rol.id);
            });
           
            $('#RolIds').val(option).trigger('change');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});

$(document).on("click", "#save_usuario_rol", function () {
    $.ajax({
        type: 'POST',
        url: `/roles/store`,
        dataType: "json",
        data: $('#form_usuario_rol').serialize(),
        success: function (response) {
            if (response) {
                Swal.fire({
                    icon: 'error',
                    title: response.message,
                    html: validateError(response)
                });
            } else {
                Swal.fire({
                    icon: 'success',
                    title: response.message,
                    html: response.descripcion,
                    showConfirmButton: true,
                })
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});

/* validar errores */
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