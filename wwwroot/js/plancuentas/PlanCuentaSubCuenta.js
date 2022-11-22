$(document).on('click', '.open_create_modal_subCuenta', function () {
    $('#button_sub_cuenta_guardar')
        .removeClass('guardar_subCuenta actualizar_subCuenta')
        .addClass('guardar_subCuenta');
        $("#form_subcuenta").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-compuesta/mostrar-one-compuesta/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#modal_subcuenta .modal-title').text('Crear Sub Cuenta')
            $('#form_subcuenta #PlanCuentaCompuestaId').val(response.data.id);
            $('#form_subcuenta #codigoPadreCompuesta').val(response.data.codigo);

            $('#form_subcuenta .codigo-compuesta').text(response.data.codigo)
            $('#form_subcuenta .nombre-compuesta').text(response.data.nombreCuenta)
            $('#form_subcuenta .nivel-compuesta').text(response.data.nivel)

            $('#form_subcuenta .codigo-titulo').text(response.data.planCuentaTitulo.codigo)
            $('#form_subcuenta .nombre-titulo').text(response.data.planCuentaTitulo.nombreCuenta)
            $('#form_subcuenta .nivel-titulo').text(response.data.planCuentaTitulo.nivel)

            $('#form_subcuenta .codigo-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.codigo)
            $('#form_subcuenta .nombre-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.nombreCuenta)
            $('#form_subcuenta .nivel-rubro').text(response.data.planCuentaTitulo.planCuentaRubro.nivel)

            $('#form_subcuenta .codigo-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.codigo)
            $('#form_subcuenta .nombre-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nombreCuenta)
            $('#form_subcuenta .nivel-grupo').text(response.data.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nivel)
            $('#modal_subcuenta').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});
$(document).on('click', '.guardar_subCuenta', function () {
    console.log("guardado");

    $.ajax({
        type: 'POST',
        url: `/plan-sub-cuenta/store-subcuenta`,
        dataType: "json",
        data: $("#form_subcuenta").serialize(),
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
                $('#modal_subcuenta').modal("hide");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: response.message,
                    html: validateError(response.errors)
                });
            }
            $('#modal_subcuenta').modal('hide');
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
$(document).on('click', '.open_editar_modal_subCuenta', function () {
    //cambio de evento
    console.log('open actualizar subcuenta ')
    $('#button_sub_cuenta_guardar')
        .removeClass('guardar_subCuenta actualizar_subCuenta')
        .addClass('actualizar_subCuenta');
    $('#button_sub_cuenta_guardar').data('id', $(this).data('id'));
    $("#form_subcuenta").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-sub-cuenta/mostrar-one-subcuenta/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response)
            $('#form_subcuenta #PlanCuentaCompuestaId').val(response.data.id)
            $('#form_subcuenta #codigoPadreCompuesta').val(response.data.codigo)

            $('#form_subcuenta .codigo-compuesta').text(response.data.planCuentaCompuesta.codigo)
            $('#form_subcuenta .nombre-compuesta').text(response.data.planCuentaCompuesta.nombreCuenta)
            $('#form_subcuenta .nivel-compuesta').text(response.data.planCuentaCompuesta.nivel)

            $('#form_subcuenta .codigo-titulo').text(response.data.planCuentaCompuesta.planCuentaTitulo.codigo)
            $('#form_subcuenta .nombre-titulo').text(response.data.planCuentaCompuesta.planCuentaTitulo.nombreCuenta)
            $('#form_subcuenta .nivel-titulo').text(response.data.planCuentaCompuesta.planCuentaTitulo.nivel)

            $('#form_subcuenta .codigo-rubro').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.codigo)
            $('#form_subcuenta .nombre-rubro').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.nombreCuenta)
            $('#form_subcuenta .nivel-rubro').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.nivel)

            $('#form_subcuenta .codigo-grupo').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.codigo)
            $('#form_subcuenta .nombre-grupo').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nombreCuenta)
            $('#form_subcuenta .nivel-grupo').text(response.data.planCuentaCompuesta.planCuentaTitulo.planCuentaRubro.planCuentaGrupo.nivel)

            $('#modal_subcuenta .modal-title').text('Editar SubCuenta')
            $('#form_subcuenta .codigo').val(response.data.codigo)
            $('#form_subcuenta .nombre').val(response.data.nombreCuenta)
            $('#form_subcuenta .moneda').val(response.data.moneda)
            $('#form_subcuenta .valor').val(response.data.valor)
            
            $('#modal_subcuenta').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});

$(document).on('click', '.actualizar_subCuenta', function () {
    console.log(' actualizar titulo ')
    $.ajax({
        type: 'PUT',
        url: `/plan-sub-cuenta/update-subcuenta/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_subcuenta").serialize(),
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
                $('#modal_subcuenta').modal("hide");
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
$(document).on("click", ".open_delete_modal_subCuenta", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar este SubCuenta?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_subCuenta(id)
        }
    })
});
function eliminar_subCuenta(id) {
    $.ajax({
        type: "DELETE",
        url: `/plan-sub-cuenta/delete-subcuenta/${id}`,
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