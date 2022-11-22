$(document).on('click', '#open_modal_grupo', function () {
    $('#button_grupo_guardar')
        .removeClass('guardar_grupo actualizar_grupo')
        .addClass('guardar_grupo');
        $("#form_grupo").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/PlanDeCuentas/mostrar-grupo`,
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#modal_grupo .modal-title').text('Crear Grupo')
            $('#form_grupo .codigo-grupo').text(response.data.codigo)
            $('#form_grupo .nombre-grupo').text(response.data.nombreCuenta)
            $('#form_grupo .nivel-grupo').text(response.data.nivel)
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

$(document).on('click', '.open_editar_modal_grupo', function () {
    //cambio de evento
    console.log('cambios de evento  actua lizar ')
    $('#button_grupo_guardar')
        .removeClass('guardar_grupo actualizar_grupo')
        .addClass('actualizar_grupo');
    $('#button_grupo_guardar').data('id', $(this).data('id'));
    $("#form_grupo").trigger("reset");
    $.ajax({
        type: 'GET',
        url: `/PlanDeCuentas/mostrar-one-grupo/${$(this).data('id')}`,
        dataType: "json",
        success: function (response) {
            console.log(response)
            $('#modal_grupo .modal-title').text('Editar Grupo')
            $('#form_grupo .codigo').val(response.data.codigo)
            $('#form_grupo .nombre').val(response.data.nombreCuenta)
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

$(document).on('click', '.guardar_grupo', function () {
    console.log('cambios de evento a nuevo')
    $.ajax({
        type: 'POST',
        url: `/PlanDeCuentas/store-grupo`,
        dataType: "json",
        data: $("#form_grupo").serialize(),
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
                $('#modal_grupo').modal("hide");
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

$(document).on('click', '.actualizar_grupo', function () {
    console.log($(this).data('id'))
    $.ajax({
        type: 'PUT',
        url: `/PlanDeCuentas/update-grupo/${$(this).data('id')}`,
        dataType: "json",
        data: $("#form_grupo").serialize(),
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
                $('#modal_grupo').modal("hide");
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
    $('#modal_grupo').modal("toggle");
    $("#form_grupo").trigger("reset");
});

$(document).on("click", ".open_delete_modal_grupo", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar este Grupo?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_grupo(id)
        }
    })
});
function eliminar_grupo(id) {
    $.ajax({
        type: "DELETE",
        url: `/PlanDeCuentas/delete-grupo/${id}`,
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