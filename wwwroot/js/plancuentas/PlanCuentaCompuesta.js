$(document).on('click', '.open_create_modal_compuesta', function () {
    $('#button_compuesta_guardar')
        .removeClass('guardar_compuesta actualizar_titulo')
        .addClass('guardar_compuesta');
    $('#button_compuesta_guardar').data('id', $(this).data('id'));
    $("#form_compuesta").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-titulo/mostrar-one-titulo/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#form_compuesta #PlanCuentaTituloId').val(response.data.id);
            $('#form_compuesta #codigoPadreTitulo').val(response.data.codigo);

            $('#form_compuesta .codigo-titulo').text(response.data.codigo)
            $('#form_compuesta .nombre-titulo').text(response.data.nombreCuenta)
            $('#form_compuesta .nivel-titulo').text(response.data.nivel)

            $('#form_compuesta .codigo-rubro').text(response.data.planCuentaRubro.codigo)
            $('#form_compuesta .nombre-rubro').text(response.data.planCuentaRubro.nombreCuenta)
            $('#form_compuesta .nivel-rubro').text(response.data.planCuentaRubro.nivel)

            $('#form_compuesta .codigo-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.codigo)
            $('#form_compuesta .nombre-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.nombreCuenta)
            $('#form_compuesta .nivel-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.nivel)

            $('#modal_compuesta').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});
$(document).on('click', '.guardar_compuesta', function () {
    console.log("guardado");

    $("#form_compuesta").serialize();
    console.log($("#form_compuesta").serialize())
    $.ajax({
        type: 'POST',
        url: `/plan-compuesta/crear-compuesta`,
        dataType: "json",
        data: $("#form_compuesta").serialize(),
        success: function (response) {
            console.log(response.message);
            if (response.status == 'ok') {
                Swal.fire({
                    icon: 'success',
                    title: response.message,
                    html: '',
                    showConfirmButton: true
                });
                $('#modal_compuesta').modal('hide');
                table.draw();
            } else {
                Swal.fire({
                    icon: 'error',
                    title: response.message,
                    html: validateError(response.errors)
                });
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

/*editar */
$(document).on('click', '.open_editar_modal_compuesta', function () {
    //cambio de evento
    console.log('open actualizar compuesta ')
    $('#button_compuesta_guardar')
        .removeClass('guardar_compuesta actualizar_compuesta')
        .addClass('actualizar_compuesta');
    $('#button_compuesta_guardar').data('id', $(this).data('id'));
    $("#form_compuesta").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-compuesta/mostrar-one-compuesta/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response)
            $('#form_compuesta #PlanCuentaTituloId').val(response.data.id)
            $('#form_compuesta #codigoPadreTitulo').val(response.data.codigo)

            $('#form_compuesta .codigo-titulo').text(response.data.planCuentaTitulo.codigo)
            $('#form_compuesta .nombre-titulo').text(response.data.planCuentaTitulo.nombreCuenta)
            $('#form_compuesta .nivel-titulo').text(response.data.planCuentaTitulo.nivel)

            $('#form_compuesta .codigo-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.codigo)
            $('#form_compuesta .nombre-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.nombreCuenta)
            $('#form_compuesta .nivel-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.nivel)

            $('#form_compuesta .codigo-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.codigo)
            $('#form_compuesta .nombre-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nombreCuenta)
            $('#form_compuesta .nivel-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nivel)

            $('#modal_compuesta .modal-title').text('Editar Compuesta')
            $('#form_compuesta .codigo').val(response.data.codigo)
            $('#form_compuesta .nombre').val(response.data.nombreCuenta)

            $('#modal_compuesta').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});

$(document).on('click', '.actualizar_compuesta', function () {
    console.log(' actualizar compuesta ')
    $.ajax({
        type: 'PUT',
        url: `/plan-compuesta/update-compuesta/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_compuesta").serialize(),
        success: function (response) {
            console.log(response.message);
            if (response.status == 'ok') {
                Swal.fire({
                    icon: 'success',
                    title: response.message,
                    html: '',
                    showConfirmButton: true
                });
                table.draw();
                $('#modal_compuesta').modal("hide");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: response.message,
                    html: validateError(response.errors)
                });
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


$(document).on("click", ".open_delete_modal_compuesta", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar Compuesta?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_compuesta(id)
        }
    })
});
function eliminar_compuesta(id) {
    $.ajax({
        type: "DELETE",
        url: `/plan-compuesta/delete-compuesta/${id}`,
        dataType: "json",
        success: function (response) {
            if (response.status == 'ok') {
                Swal.fire(
                    response.message,
                    'Resgistro eliminado',
                    'success'
                );
                table.draw();
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error server',
                    html: '',
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR);
        },
        fail: function (response) {
            fail()
        }
    });
}