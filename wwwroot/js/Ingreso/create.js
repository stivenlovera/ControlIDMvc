$(document).on('click', '#guardar', function () {
    let data = actualizacion_monto();
    $.ajax({
        type: "POST",
        url: `/recibo-ingreso/store`,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.status == 'success') {
                window.location.replace("/Movimiento");
            } else {

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR);
        },
        fail: function (response) {
            fail()
        }
    });
});