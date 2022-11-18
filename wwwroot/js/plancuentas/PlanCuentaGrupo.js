$(document).on('click', '#open_modal_grupo', function () {
    $.ajax({
        type: 'GET',
        url: `/PlanDeCuentas/mostrar-grupo`,
        dataType: "json",
        success: function (response) {
            console.log(response);

            $('#form_grupo input[name=codigo]').val(response.data.codigoGrupo)
            $('#modal_grupo').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });

});
$(document).on('click', '#guardar_grupo', function () {
    console.log("guardado");

    $("#form_grupo").serialize();
    console.log($("#form_grupo").serialize())
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/crear-grupo`,
        dataType: "json",
        data: $("#form_grupo").serialize(),
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
    $('#modal_grupo').modal("toggle");
    $("#form_grupo").trigger("reset");
});