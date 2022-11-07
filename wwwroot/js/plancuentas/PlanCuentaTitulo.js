$(document).on('click', '#open_modal_titulo', function () {
    $('#modal_titulo').modal('show');


});
$(document).on('click', '#guardar_titulo', function () {
    console.log("guardado");

    $("#form_titulo").serialize();
    console.log($("#form_titulo").serialize())
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/crear-titulo`,
        dataType: "json",
        data: $("#form_titulo").serialize(),
        success: function (response) {
            console.log(response.message);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
    $('#modal_titulo').modal("toggle");
    $("#form_titulo").trigger("reset");
});