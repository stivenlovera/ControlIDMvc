$(document).ready(function () {
    $("#PaqueteId").select2()
        .on("select2:select", function (e) {
            
            var data = e.params.data;
            $("#Costo").val($(data.element).data('costo'));
            $("#Dias").val($(data.element).data('dias'));
            
            console.log($(data.element).data('dias'))

            let cant_dias=$(data.element).data('dias');
            if (validar_fecha($('#FechaFinCliente').val())) {
                //añadir
                var resultado=añadir_dias(cant_dias);
                console.log('añadir',resultado)
                $('#FechaInicio').val(resultado.fecha_inicio);
                $('#FechaFin').val(resultado.fecha_fin);
            } else {
                //reestablecer
                var resultado=reestablecer_dias(cant_dias);
                console.log('reestablecer',resultado)
                $('#FechaInicio').val(resultado.fecha_inicio);
                $('#FechaFin').val(resultado.fecha_fin);
            }
        });
});
function validar_fecha(fecha_fin) {
    if (moment(fecha_fin).format("YYYY/MM/DD") > moment().format("YYYY/MM/DD")) {
        return true;
    } else {
        return false;
    }
}

function añadir_dias(cantidad) {
    /*validate  */
    let dias = moment($('#FechaFinCliente').val()).add(cantidad, 'days');
    return {
        fecha_inicio: $('#FechaInicioCliente').val(),
        fecha_fin: dias.format("YYYY/MM/DD")
    };
}

function reestablecer_dias(cantidad) {
    /*validate  */
    const fecha_actual = moment().format("YYYY/MM/DD");
    let dias = moment(fecha_actual).add(cantidad, 'days');
    return {
        fecha_inicio: moment().format("YYYY/MM/DD"),
        fecha_fin: dias.format("YYYY/MM/DD")
    };
}

$('#formulario_inscripcion').on('keyup keypress', function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13) {
        e.preventDefault();
        $("#buscar_cliente").trigger("click");
        return false;
    }
});