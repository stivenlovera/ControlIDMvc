var horarios_disponibles = $("#horario_disponibles").DataTable({
    // ServerSide Setups
    processing: true,
    serverSide: true,
    // Paging Setups
    paging: true,
    filter: true,
    order: [],
    ajax: {
        url: `/horario/data-table`,
        type: "POST",
    },
    columns: [
        {
            name: "nombre",
            width: "90%",
            data: "nombre"
        },
        {
            name: "id",
            width: "10%",
            data: "id",
            render: function (data, type, row) {
                return `
                <button class="btn btn-success btn-xs añadir_horario" data-id="${data}" type="button"><i class="fa fa-arrow-right"></i></button>
                `;
            }
        }
    ]
});

var horarios_seleccionados = $('#horarios_seleccionados').DataTable();


$(document).on('click', '.añadir_horario', function () {
    var horarios = horarios_seleccionados.rows().data().toArray()
    console.log(horarios_seleccionados.rows().data())
    var data = horarios_disponibles.row($(this).parent().parent()).data();

    var alerta = false;
    horarios.map(usuario => {
        if (usuario[2] == data.id) {
            alerta = true;
        }
    });
    if (alerta == false) {
        load_data_horario(data);
    } else {
        console.log("ya fue registrado")
    }

});

function load_data_horario(data) {
    horarios_seleccionados.row.add([ //  <button class="btn btn-success btn-xs" data-id="${data.id}" role="button"><i class="fa fa-arrow-left"></i></button>
        `
        <button class="btn btn-danger btn-xs remover_horario" data-id="${data.id}" role="button"><i class="fa fa fa-trash"></i></button>
        <input type="text" name="HorarioSelecionados[]" value="${data.id}" hidden>
        <input type="text"  name="HorarioSelecionadosNombre[]" value="${data.nombre}" hidden>
        `,
        data.nombre,
        data.id,
    ]).draw(false);
}

$('#horarios_seleccionados tbody').on('click', '.remover_horario', function () {
    horarios_seleccionados
        .row($(this).parents('tr'))
        .remove()
        .draw();
});
