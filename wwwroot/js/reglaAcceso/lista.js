var personas_disponibles = $("#datatable").DataTable({
    // ServerSide Setups
    processing: true,
    serverSide: true,
    // Paging Setups
    paging: true,
    filter: true,
    order: [],
    ajax: {
        url: `/regla-acceso/data-table`,
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