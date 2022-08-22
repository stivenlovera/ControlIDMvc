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
                <button class="btn btn-success btn-xs añadir" data-id="${data}" role="button"><i class="fa fa-arrow-right"></i></button>
                `;
            }
        }
    ]
});

var personas_selecionadas = $('#personas_selecionadas').dataTable();

$(document).on('click', '.añadir', function () {
    //console.log($(this))
    console.log(personas_disponibles.row($(this).parent().parent()).data());
});
