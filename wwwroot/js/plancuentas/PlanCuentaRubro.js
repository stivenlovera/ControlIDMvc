$(document).on('click', '.open_create_modal_rubro', function () {
    $('#button_rubro_guardar')
        .removeClass('guardar_rubro actualizar_rubro')
        .addClass('guardar_rubro');
    $("#form_rubro").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-rubro/mostrar-one-grupo/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#modal_rubro .modal-title').text('Crear Rubro')
            $('#form_rubro #PlanCuentaGrupoId').val(response.data.id)
            $('#form_rubro #codigoPadreGrupo').val(response.data.codigo)
            $('#form_rubro .codigo-grupo').text(response.data.codigo)
            $('#form_rubro .nombre-grupo').text(response.data.nombreCuenta)
            $('#form_rubro .nivel-grupo').text(response.data.nivel)
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
$(document).on('click', '.guardar_rubro', function () {
    console.log("guardado", $("#form_rubro #PlanCuentaGrupoId").serialize());
    $.ajax({
        type: 'POST',
        url: `/Plan-rubro/store-rubro`,
        dataType: "json",
        data: $("#form_rubro").serialize(),
        success: function (response) {
            console.log(response.message);
            if (response.status == 'ok') {
                Swal.fire({
                    icon: 'success',
                    title: response.message,
                    html: '',
                    showConfirmButton: true
                });
                $('#modal_rubro').modal("hide");
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
$(document).on('click', '.open_editar_modal_rubro', function () {
    //cambio de evento
    console.log('open actualizar rubro ')
    $('#button_rubro_guardar')
        .removeClass('guardar_rubro actualizar_rubro')
        .addClass('actualizar_rubro');
    $("#form_rubro").trigger("reset");
    $('#button_rubro_guardar').data('id', $(this).data('id'));
    $.ajax({
        type: 'GET',
        url: `/plan-rubro/mostrar-one-rubro/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response)
            $('#form_rubro #PlanCuentaGrupoId').val(response.data.id)
            $('#form_rubro #codigoPadreGrupo').val(response.data.codigo)
            $('#form_rubro .codigo-grupo').text(response.data.planCuentaGrupo.codigo)
            $('#form_rubro .nombre-grupo').text(response.data.planCuentaGrupo.nombreCuenta)
            $('#form_rubro .nivel-grupo').text(response.data.planCuentaGrupo.nivel)

            $('#modal_rubro .modal-title').text('Editar Rubro')
            $('#form_rubro .codigo').val(response.data.codigo)
            $('#form_rubro .nombre').val(response.data.nombreCuenta)
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

$(document).on('click', '.actualizar_rubro', function () {
    console.log(' actualizar rubro ')
    $.ajax({
        type: 'PUT',
        url: `/plan-rubro/update-rubro/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_rubro").serialize(),
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
                $('#modal_rubro').modal("hide");
                $("#form_rubro").trigger("reset");
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

$(document).on("click", ".open_delete_modal_rubro", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar Rubro?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_rubro(id)
        }
    })
});
function eliminar_rubro(id) {
    $.ajax({
        type: "DELETE",
        url: `/plan-rubro/delete-rubro/${id}`,
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
