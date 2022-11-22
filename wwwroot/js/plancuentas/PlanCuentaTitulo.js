$(document).on('click', '.open_create_modal_titulo', function () {
    $('#button_titulo_guardar')
        .removeClass('guardar_titulo actualizar_titulo')
        .addClass('guardar_titulo');
    $('#button_titulo_guardar').data('id', $(this).data('id'));
    $("#form_titulo").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-rubro/mostrar-one-rubro/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_titulo").serialize(),
        success: function (response) {
            console.log(response)
            $('#modal_titulo .modal-title').text('Crear Titulo')
            $('#form_titulo #PlanCuentaRubroId').val(response.data.id);
            $('#form_titulo #codigoPadreRubro').val(response.data.codigo)
            //titulo
            $('#form_titulo .codigo-rubro').text(response.data.codigo)
            $('#form_titulo .nombre-rubro').text(response.data.nombreCuenta)
            $('#form_titulo .nivel-rubro').text(response.data.nivel)

            $('#form_titulo .codigo-grupo').text(response.data.planCuentaGrupo.codigo)
            $('#form_titulo .nombre-grupo').text(response.data.planCuentaGrupo.nombreCuenta)
            $('#form_titulo .nivel-grupo').text(response.data.planCuentaGrupo.nivel)
            $('#modal_titulo').modal('show');
            console.log(response.message);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });

});
$(document).on('click', '.guardar_titulo', function () {
    console.log("guardado", $("#form_titulo").serialize());
    console.log($(this).data('data'))
    $.ajax({
        type: 'POST',
        url: `/plan-titulo/store-titulo`,
        dataType: "json",
        data: $("#form_titulo").serialize(),
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
    $('#modal_titulo').modal("toggle");
    $("#form_titulo").trigger("reset");
});

/*editar */
$(document).on('click', '.open_editar_modal_titulo', function () {
    //cambio de evento
    console.log('open actualizar titulo ')
    $('#button_titulo_guardar')
        .removeClass('guardar_titulo actualizar_titulo')
        .addClass('actualizar_titulo');
    $('#button_titulo_guardar').data('id', $(this).data('id'));
    $("#form_titulo").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/plan-titulo/mostrar-one-titulo/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response)
            $('#form_titulo #PlanCuentaRubroId').val(response.data.id)
            $('#form_titulo #codigoPadreGrupo').val(response.data.codigo)

            $('#form_titulo .codigo-rubro').text(response.data.planCuentaRubro.codigo)
            $('#form_titulo .nombre-rubro').text(response.data.planCuentaRubro.nombreCuenta)
            $('#form_titulo .nivel-rubro').text(response.data.planCuentaRubro.nivel)

            $('#form_titulo .codigo-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.codigo)
            $('#form_titulo .nombre-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.nombreCuenta)
            $('#form_titulo .nivel-grupo').text(response.data.planCuentaRubro.planCuentaGrupo.nivel)

            $('#modal_titulo .modal-title').text('Editar Titulo')
            $('#form_titulo .codigo').val(response.data.codigo)
            $('#form_titulo .nombre').val(response.data.nombreCuenta)

            $('#modal_titulo').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });
});

$(document).on('click', '.actualizar_titulo', function () {
    console.log(' actualizar titulo ')
    $.ajax({
        type: 'PUT',
        url: `/plan-titulo/update-titulo/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_titulo").serialize(),
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
                $('#modal_titulo').modal("hide");
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


$(document).on("click", ".open_delete_modal_titulo", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar este Titulo?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_titulo(id)
        }
    })
});
function eliminar_titulo(id) {
    $.ajax({
        type: "DELETE",
        url: `/plan-titulo/delete-titulo/${id}`,
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

