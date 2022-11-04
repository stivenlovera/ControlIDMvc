var personas_disponibles = $("#personas_disponibles").DataTable({
    // ServerSide Setups
    processing: true,
    serverSide: true,
    // Paging Setups
    paging: true,
    filter: true,
    order: [],
    ajax: {
        url: `/persona/data-table`,
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
                <button class="btn btn-success btn-xs añadir" type="button" data-id="${data}" role="button"><i class="fa fa-arrow-right"></i></button>
                `;
            }
        }
    ]
});
var personas_selecionadas = $('#personas_selecionadas').DataTable();


$(document).on('click', '.añadir', function () {
    var usuarios = personas_selecionadas.rows().data().toArray()
    console.log(personas_selecionadas.rows().data())
    var data = personas_disponibles.row($(this).parent().parent()).data();

    var alerta = false;
    usuarios.map(usuario => {
        if (usuario[2] == data.id) {
            alerta = true;
        }
    });
    if (alerta == false) {
        load_data_usuario(data);
    } else {
        console.log("ya fue registrado")
    }

});

function load_data_usuario(data) {
    personas_selecionadas.row.add([ //  <button class="btn btn-success btn-xs" data-id="${data.id}" role="button"><i class="fa fa-arrow-left"></i></button>
        `
        <button class="btn btn-danger btn-xs remover_usuario" data-id="${data.id}" role="button"><i class="fa fa fa-trash"></i></button>
        <input type="text" name="PersonasSelecionadas[]" value="${data.id}" hidden>
        <input type="text" name="PersonasSelecionadasNombre[]" value="${data.nombre}" hidden>
        `,
        data.nombre,
        data.id,
    ]).draw(false);
}

$('#personas_selecionadas tbody').on('click', '.remover_usuario', function () {
    personas_selecionadas
        .row($(this).parents('tr'))
        .remove()
        .draw();
});



