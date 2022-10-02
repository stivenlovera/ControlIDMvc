var area_disponibles = $("#area_disponibles").DataTable({
    columns: [
        {
            width: "90%",
        },
        {
            width: "10%",
        }
    ]
});

var area_selecionadas = $('#area_selecionadas').DataTable({
    columns: [
        {
            width: "10%",
        },
        {
            width: "90%",
        }
    ]
});


$(document).on('click', '.añadir_area_seleccionada', function () {
    var data = area_disponibles.row($(this).parent().parent()).data();
    load_data_areas_selecionada(data);
    area_disponibles
        .row($(this).parents('tr'))
        .remove()
        .draw();
});

function load_data_areas_selecionada(data) {
    //console.log('data', data)
    area_selecionadas.row.add([
        `<button class="btn btn-success btn-xs añadir_area_disponible" type="button" data-nombre="${$(data[1]).data('nombre')}" data-id="${$(data[1]).data('id')}"><i class="fa fa-arrow-left"></i></button>
        <input type="text" name="PuertasSelecionadas[]" hidden value="${$(data[1]).data('id')}">`,
        $(data[1]).data('nombre')
    ]).draw();
}



$(document).on('click', '.añadir_area_disponible', function () {
    var data = area_selecionadas.row($(this).parent().parent()).data();
    load_data_areas_disponible(data);
    area_selecionadas
        .row($(this).parents('tr'))
        .remove()
        .draw();
});

function load_data_areas_disponible(data) {
    //console.log('data', data)
    area_disponibles.row.add([
        $(data[0]).data('nombre'),
        `<button class="btn btn-success btn-xs añadir_area_seleccionada" type="button" data-nombre="${$(data[0]).data('nombre')}" data-id="${$(data[0]).data('id')}"><i class="fa fa-arrow-right"></i></button>
        <input type="text" name="PuertasDisponibles[]" hidden value="${$(data[1]).data('nombre')}" hidden>`
    ]).draw();
}
