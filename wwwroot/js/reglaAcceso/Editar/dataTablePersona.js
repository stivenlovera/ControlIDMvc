var datatable_personas_disponibles = $("#datatable_personas_disponibles").DataTable();

$(document).on("click", "#open_modal_persona", function () {

    $('#modal_añadir_persona').modal('show');
});

var datatable_personas = $("#datatable_personas").DataTable();

/*añadir a persona */
$(document).on("click", "#añadir_persona", function () {
    let area_persona = [];
    $('.check_persona:checked').each(function (i, value) {
        area_persona.push({
            id: $(value).val(),
            ci: $(value).data('ci'),
            nombre: $(value).data('nombre'),
            apellido: $(value).data('apellido'),
            celular: $(value).data('celular')
        })
    })
    validar_persona(area_persona);
    limpiarChecked();
});

function validar_persona(valores) {
    let personas_ocupadas = [];
    $('.delete_persona').each(function (i, value) {
        personas_ocupadas.push($(this).data('id'))
    });

    let bandera = true;
    personas_ocupadas.map(x => {
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
            añadir_persona(valor.ci, valor.nombre, valor.apellido, valor.celular, valor.id);
        });

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Persona ya fue vinculada',
            html: '',
        });
    }
}
function añadir_persona(ci, nombre, apelido, celular, id) {
    console.log("run")
    datatable_personas.row.add([
        `<td>${ci}</td>`,
        `<td>${nombre}</td>`,
        `<td>${apelido}</td>`,
        `<td>${celular}</td>`,
        `<td>
            <button class="btn btn-danger btn-xs delete_persona"
                data-id="${id}" role="button">
                <i class="fa fa-trash" title="Eliminar Persona"></i>
            </button>
        </td>`,
    ]).draw();

}

$(document).on("click", ".delete_persona", function () {
    console.log($(this).parents('tr'))
    datatable_personas.row($(this).parents('tr'))
    .remove()
    .draw();
});