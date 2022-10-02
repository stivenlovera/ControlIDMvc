var area_disponibles = $("#area_disponibles").DataTable({
    // ServerSide Setups
    processing: true,
    serverSide: true,
    // Paging Setups
    paging: true,
    filter: true,
    order: [],
    ajax: {
        url: `/area/data-table`,
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
                <button class="btn btn-success btn-xs añadir_area" data-id="${data}" type="button"><i class="fa fa-arrow-right"></i></button>
                `;
            }
        }
    ]
});

var area_selecionadas = $('#area_selecionadas').DataTable();


$(document).on('click', '.añadir_area', function () {
    var areas = area_selecionadas.rows().data().toArray()
    console.log(area_selecionadas.rows().data())
    var data = area_disponibles.row($(this).parent().parent()).data();

    var alerta = false;
    areas.map(usuario => {
        if (usuario[2] == data.id) {
            alerta = true;
        }
    });
    if (alerta == false) {
        load_data_area(data);
    } else {
        console.log("ya fue registrado")
    }

});

function load_data_area(data) {
    area_selecionadas.row.add([ //  <button class="btn btn-success btn-xs" data-id="${data.id}" role="button"><i class="fa fa-arrow-left"></i></button>
        `
        <button class="btn btn-danger btn-xs remover_area" data-id="${data.id}" role="button"><i class="fa fa fa-trash"></i></button>
        <input type="text" name="AreaSelecionadas[]" value="${data.id}" hidden>
        <input type="text" name="AreasSelecionadosNombre[]" value="${data.nombre}" hidden>
        `,
        data.nombre,
        data.id,
    ]).draw(false);
}

$('#area_selecionadas tbody').on('click', '.remover_area', function () {
    area_selecionadas
        .row($(this).parents('tr'))
        .remove()
        .draw();
});
