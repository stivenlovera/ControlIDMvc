$(document).on("click", "#guardar_usuario", function () {
    $.ajax({
        type: 'POST',
        url: `/persona/store-ajax`,
        dataType: "json",
        data: $('#formulario_nueva_persona').serialize(),
        success: function (response) {
            console.log(response)
            añadir_cliente(
                response.data.id,
                response.data.ci,
                response.data.nombre,
                response.data.apellido,
                response.data.fecha_inicio,
                response.data.fecha_fin
            );
            $('#PaqueteId').prop("disabled", false);
            $('#modal_crear_usuario').modal('hide');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});