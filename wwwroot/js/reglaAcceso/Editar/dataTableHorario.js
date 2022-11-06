var area_disponibles = $("#datatable_horario_disponibles").DataTable();

$(document).on("click", "#open_modal_horario", function () {

    $('#modal_añadir_horario').modal('show');
});

var datatable_horario = $("#datatable_horario").DataTable();

/*añadir a area */
$(document).on("click", "#añadir_horario", function () {
    let horario_nuevas = [];
    $('.check_horario:checked').each(function (i, value) {
        horario_nuevas.push({
            id: $(value).val(),
            nombre: $(value).data('nombre'),
            descripcion: $(value).data('descripcion')
        })
    })
    validar_horario(horario_nuevas);
    limpiarChecked();
});

function validar_horario(valores) {
    let horario_ocupadas = [];
    $('.delete_horario').each(function (i, value) {
        horario_ocupadas.push($(this).data('id'))
    });
    
    let bandera = true;
    horario_ocupadas.map(x => {
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
            añadir_horario(valor.nombre, valor.descripcion, valor.id);
        });

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Horario ya fue vinculada',
            html: '',
        });
    }
}

function añadir_horario(nombre, descripcion, id) {
    console.log("run")
    datatable_horario.row.add([
        `<td>${nombre}</td>`,
        `<td>${descripcion}</td>`,
        `<td>
            <button class="btn btn-danger btn-xs delete_horario"
                data-id="${id}" role="button">
                <i class="fa fa-trash" title="Eliminar Area"></i>
            </button>
        </td>`,
    ]).draw();
}

$(document).on("click", ".delete_horario", function () {
    console.log($(this).parents('tr'))
    datatable_horario
        .row($(this).parents('tr'))
        .remove()
        .draw();
});