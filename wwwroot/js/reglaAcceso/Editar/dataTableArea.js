var area_disponibles = $("#area_disponibles").DataTable();

$(document).on("click", "#open_modal_area", function () {
    $('#modal_añadir_area').modal('show');
});

var datatable_area = $("#datatable_area").DataTable();

/*añadir a area */
$(document).on("click", "#añadir_area", function () {
    let area_nuevas = [];
    $('.check_area:checked').each(function (i, value) {
        area_nuevas.push({
            id: $(value).val(),
            nombre: $(value).data('nombre'),
            descripcion: $(value).data('descripcion')
        })
    })
    validar_area(area_nuevas);
    limpiarChecked();
});

function validar_area(valores) {
    let areas_ocupadas = [];
    $('.delete_area').each(function (i, value) {
        areas_ocupadas.push($(this).data('id'))
    });
    
    let bandera = true;
    areas_ocupadas.map(x => {
        valores.map(valor => {
            if (valor.id == x) {
                bandera = false;
                console.log("ya existe");
            } else {
                console.log("no existe");
            }
        })
    })
    if (bandera) {
        console.log("añadir", valores);
        valores.forEach(valor => {
            añadir_area(valor.nombre, valor.descripcion, valor.id);
        });

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Area ya fue vinculada',
            html: '',
        });
    }
}
function añadir_area(nombre, descripcion, id) {
    console.log("run")
    datatable_area.row.add([
        `<td>${nombre}</td>`,
        `<td>${descripcion}</td>`,
        `<td>
            <button class="btn btn-danger btn-xs delete_area"
                data-id="${id}" role="button">
                <i class="fa fa-trash" title="Eliminar Area"></i>
            </button>
        </td>`,
    ]).draw();

}
function limpiarChecked() {
    $('.check_area').each(function (i, value) {
        $(value).prop('checked', false)
    });
    $('.check_horario').each(function (i, value) {
        $(value).prop('checked', false)
    });
    $('.check_persona').each(function (i, value) {
        $(value).prop('checked', false)
    });
}

$(document).on("click", ".delete_area", function () {
    console.log($(this).parents('tr'))
    datatable_area
        .row($(this).parents('tr'))
        .remove()
        .draw();
});