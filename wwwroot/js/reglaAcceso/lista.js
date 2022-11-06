var reglas_acceso = $("#datatable").DataTable({
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
                <a class="btn btn-success btn-xs" href="/regla-acceso/edit/${data}" role="button"><i class="fa fa-pencil" title="Editar"></i></a>
                <button class="btn btn-danger btn-xs delete" data-id="${data}" role="button"><i class="fa fa-trash" title="Eliminar"></i></button>`
            }
        }
    ]
});