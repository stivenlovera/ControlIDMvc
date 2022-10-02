var tarjetas_editar = $("#tarjetas_editar").DataTable({
    columnDefs: [
        { width: "90%", targets: 0 },
        { width: "10%", targets: 1 },
    ]
});

$(document).on('click', '#añadir_tarjeta', function () {
    const area = $('#area').val();
    const codigo = $('#codigo').val();
    if ((area && codigo) == '') {
        alert('complete los campos')
    } else {
        load_data_tarjeta_editar(area, codigo)
    }
});

function load_data_tarjeta_editar(area, codigo) {
    tarjetas_editar.row.add([
        `${area},${codigo}`,
        `
                <input name="area" type="text" value="${area}" hidden >
                <input name="codigo" type="text" value="${codigo}" hidden >
                <button class="btn btn-danger btn-xs remove_tarjeta" role="button"><i class="fa fa fa-trash"></i></button>
            `,
    ]).draw(false);
    $('#area').val('');
    $('#codigo').val('');
}
$('#tarjetas_editar tbody').on('click', '.remove_tarjeta', function () {
    tarjetas_editar.row($(this).parents('tr')).remove().draw();
});