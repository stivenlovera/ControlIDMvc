
$(document).on("click", "#save_regla_acceso", function () {

    let personas = [];
    $('.delete_persona').each(function (i, value) {
        personas.push($(this).data('id'))
    });
    let horarios = [];
    $('.delete_horario').each(function (i, value) {
        horarios.push($(this).data('id'))
    });
    let areas = [];
    $('.delete_area').each(function (i, value) {
        areas.push($(this).data('id'))
    });

    $.ajax({
        type: "PUT",
        url: `/regla-acceso/update/${$('#id').val()}`,
        dataType: "json",
        data: {
            Id:$('#id').val(),
            Nombre:$('#nombre').val(),
            Descripcion:$('#descripcion').val(),
            PersonasSelecionadas:personas,
            HorarioSelecionados:horarios,
            AreaSelecionadas:areas,
        },
        success: function (response) {
            if (response.status == 'success') {
                window.location.href = '/regla-acceso';
                //table.draw();
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
});
