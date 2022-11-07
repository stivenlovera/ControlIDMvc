$(document).on('click', '#open_modal_compuesta', function () {
    $('#modal_compuesta').modal('show');


});
$(document).on('click', '#guardar_compuesta', function () {
    console.log("guardado");

    $("#form_compuesta").serialize();
    console.log($("#form_compuesta").serialize())
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/crear-compuesta`,
        dataType: "json",
        data: $("#form_compuesta").serialize(),
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
    $('#modal_compuesta').modal("toggle");
    $("#form_compuesta").trigger("reset");
});