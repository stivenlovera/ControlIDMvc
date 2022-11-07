$(document).on('click', '.open_modal_rubro', function () {
    console.log($(this).data('id'))
    $.ajax({
        type: 'GET',
        url: `/PlanDeCuentas/mostrar-one-grupo/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#form_rubro .codigo-grupo').text(response.data.codigo)
            $('#form_rubro .nombre-grupo').text(response.data.nombreCuenta)
            $('#form_rubro .nivel-grupo').text(response.data.nivel)
            $('#form_rubro .id-grupo').val(response.data.id)
            $('#modal_rubro').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });

});
$(document).on('click', '#guardar_rubro', function () {
    console.log("guardado");
    $("#form_rubro").serialize();
    console.log($("#form_rubro").serialize())
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/crear-rubro`,
        dataType: "json",
        data: $("#form_rubro").serialize(),
        success: function (response) {
            console.log(response.message);
            $('#modal_rubro').modal("toggle");
            $("#form_rubro").trigger("reset");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });

});