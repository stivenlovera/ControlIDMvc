var area_disponibles = $("#area_disponibles").DataTable({
    columns: [
        {
            width: "40%",
        },
        {
            width: "25%",
        },
        {
            width: "25%",
        },
        {
            width: "10%",
        }
    ]
});

var area_selecionadas = $('#area_selecionadas').DataTable({
    columns: [
        {
            width: "40%",
        },
        {
            width: "25%",
        },
        {
            width: "25%",
        },
        {
            width: "10%",
        }
    ]
});


$(document).on('click', '.añadir_area_seleccionada', function () {
    let PuertasDisponibleId;
    let PuertasDisponibleNombre;
    let PuertasDisponibleAreaEntrada;
    let PuertasDisponibleAreaEntradaNombre;
    let PuertasDisponibleAreaSalida;
    let PuertasDisponibleAreaSalidaNombre;
    $(this).parent().parent().children().each(function (i, value) {
        //console.log(i, value)
        switch (i) {
            case 0:
                PuertasDisponibleNombre = $(value).find('input[name="PuertasDisponibleNombre"]').val();
                PuertasDisponibleId = $(value).find('input[name="PuertasDisponibleId"]').val();
                break;
            case 1:
                PuertasDisponibleAreaEntrada = $(value).find('input[name="PuertasDisponibleAreaEntrada"]').val();
                PuertasDisponibleAreaEntradaNombre = $(value).find('input[name="PuertasDisponibleAreaEntradaNombre"]').val();
                break;
            case 2:
                PuertasDisponibleAreaSalida = $(value).find('input[name="PuertasDisponibleAreaSalida"]').val();
                PuertasDisponibleAreaSalidaNombre = $(value).find('input[name="PuertasDisponibleAreaSalidaNombre"]').val();
                break;
            default:
                break;
        }
    });
    load_data_areas_selecionada(
        PuertasDisponibleId,
        PuertasDisponibleNombre,
        PuertasDisponibleAreaEntrada,
        PuertasDisponibleAreaEntradaNombre,
        PuertasDisponibleAreaSalida,
        PuertasDisponibleAreaSalidaNombre
    );
    area_disponibles
        .row($(this).parents('tr'))
        .remove()
        .draw();
    //console.log(data)
});

function load_data_areas_selecionada(
    PuertasDisponibleId,
    PuertasDisponibleNombre,
    PuertasDisponibleAreaEntrada,
    PuertasDisponibleAreaEntradaNombre,
    PuertasDisponibleAreaSalida,
    PuertasDisponibleAreaSalidaNombre
) {
    //console.log('data', data)
    area_selecionadas.row.add([
        `<td> ${PuertasDisponibleNombre} <input value="${PuertasDisponibleNombre}" name="PuertasSelecionadasNombre" hidden> <input value="${PuertasDisponibleId}" name="PuertasSelecionadasId" hidden></td>`,
        `<td>${PuertasDisponibleAreaEntradaNombre} <input value="${PuertasDisponibleAreaEntradaNombre}" name="PuertasSelecionadasAreaEntradaNombre" hidden> <input value="${PuertasDisponibleAreaEntrada}" name="PuertasSelecionadasAreaEntrada" hidden></td>`,
        `<td>${PuertasDisponibleAreaSalidaNombre} <input value="${PuertasDisponibleAreaSalidaNombre}" name="PuertasSelecionadasAreaSalidaNombre" hidden> <input value="${PuertasDisponibleAreaSalida}" name="PuertasSelecionadasAreaSalida" hidden></td>`,
        `<button class="btn btn-success btn-xs añadir_area_disponible" type="button"><i class="fa fa-arrow-left"></i></button>`,
    ]).draw();
}



$(document).on('click', '.añadir_area_disponible', function () {
    let PuertasSelecionadasId;
    let PuertasSelecionadasNombre;
    let PuertasSelecionadasAreaEntradaNombre;
    let PuertasSelecionadasAreaEntrada;
    let PuertasSelecionadasAreaSalida;
    let PuertasSelecionadasAreaSalidaNombre;
    $(this).parent().parent().children().each(function (i, value) {
        //console.log(i, value)
        switch (i) {
            case 0:
                PuertasSelecionadasNombre = $(value).find('input[name="PuertasSelecionadasNombre"]').val();
                PuertasSelecionadasId = $(value).find('input[name="PuertasSelecionadasId"]').val();
                break;
            case 1:
                PuertasSelecionadasAreaEntrada = $(value).find('input[name="PuertasSelecionadasAreaEntrada"]').val();
                PuertasSelecionadasAreaEntradaNombre = $(value).find('input[name="PuertasSelecionadasAreaEntradaNombre"]').val();
                break;
            case 2:
                PuertasSelecionadasAreaSalida = $(value).find('input[name="PuertasSelecionadasAreaSalida"]').val();
                PuertasSelecionadasAreaSalidaNombre = $(value).find('input[name="PuertasSelecionadasAreaSalidaNombre"]').val();
                break;
            default:
                break;
        }
    });
    load_data_areas_disponible(
        PuertasSelecionadasId,
        PuertasSelecionadasNombre,
        PuertasSelecionadasAreaEntradaNombre,
        PuertasSelecionadasAreaEntrada,
        PuertasSelecionadasAreaSalida,
        PuertasSelecionadasAreaSalidaNombre
    );
    area_selecionadas
        .row($(this).parents('tr'))
        .remove()
        .draw();
});

function load_data_areas_disponible(
        PuertasSelecionadasId,
        PuertasSelecionadasNombre,
        PuertasSelecionadasAreaEntradaNombre,
        PuertasSelecionadasAreaEntrada,
        PuertasSelecionadasAreaSalida,
        PuertasSelecionadasAreaSalidaNombre
) {

    area_disponibles.row.add([
        `<td> ${PuertasSelecionadasNombre} <input value="${PuertasSelecionadasNombre}" name="PuertasDisponibleNombre" hidden> <input value="${PuertasSelecionadasId}" name="PuertasDisponibleId" hidden></td>`,
        `<td>${PuertasSelecionadasAreaEntradaNombre} <input value="${PuertasSelecionadasAreaEntradaNombre}" name="PuertasDisponibleAreaEntradaNombre" hidden> <input value="${PuertasSelecionadasAreaEntrada}" name="PuertasDisponibleAreaEntrada" hidden></td>`,
        `<td>${PuertasSelecionadasAreaSalidaNombre} <input value="${PuertasSelecionadasAreaSalidaNombre}" name="PuertasDisponibleAreaSalidaNombre" hidden> <input value="${PuertasSelecionadasAreaSalida}" name="PuertasDisponibleAreaSalida" hidden></td>`,
        `<button class="btn btn-success btn-xs añadir_area_seleccionada" type="button"><i class="fa fa-arrow-right"></i></button>`,
    ]).draw();
}
