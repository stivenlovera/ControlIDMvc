var tarjetas = $("#tarjetas").DataTable();
$(document).on('click', '#añadir_tarjeta', function () {
    const area = $('#area').val();
    const codigo = $('#codigo').val();
    if ((area && codigo) == '') {
        alert('complete los campos')
    } else {
        load_data(area, codigo)
    }
});

function load_data(area, codigo) {
    tarjetas.row.add([
        `${area},${codigo}`,
        `
                <button class="btn btn-danger btn-xs remove_tarjeta" role="button"><i class="fa fa fa-trash"></i></button>
            `,
    ]).draw(false);
    $('#area').val('');
    $('#codigo').val('');
}
$('#tarjetas tbody').on('click', '.remove_tarjeta', function () {
    tarjetas.row($(this).parents('tr')).remove().draw();
});