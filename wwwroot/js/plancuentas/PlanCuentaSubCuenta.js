$(document).on('click', '#open_modal_subcuenta', function () {
    $('#modal_subcuenta').modal('show');


});
$(document).on('click', '#guardar_subcuenta', function () {
    console.log("guardado");

    $("#form_subcuenta").serialize();
    console.log($("#form_subcuenta").serialize())
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/crear-subcuenta`,
        dataType: "json",
        data: $("#form_subcuenta").serialize(),
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
    $('#modal_subcuenta').modal("toggle");
    $("#form_subcuenta").trigger("reset");
});