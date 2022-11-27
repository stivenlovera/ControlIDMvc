$(document).on('click', '#guardar', function () {
    let data = actualizacion_monto();
    let id=$(this).data('id');
    $.ajax({
        type: "PUT",
        url: `/recibo-ingreso/update/${id}`,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.status == 'success') {
                window.location.replace("/movimientos");
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