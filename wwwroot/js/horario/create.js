
$(document).on('click', '.copiar', function () {
    let hora_inicio;
    let hora_fin;
    console.log($(this).parent().parent().parent().children().each(function (i, values) {
        console.log(i, values)
        switch (i) {
            case 2:
                $('#hora_inicio_lunes').val($(values).find('input').val());
                $('#hora_inicio_martes').val($(values).find('input').val());
                $('#hora_inicio_miercoles').val($(values).find('input').val());
                $('#hora_inicio_jueves').val($(values).find('input').val());
                $('#hora_inicio_viernes').val($(values).find('input').val());
                $('#hora_inicio_sabado').val($(values).find('input').val());
                $('#hora_inicio_domingo').val($(values).find('input').val());
                break;
            case 3:
                $('#hora_fin_lunes').val($(values).find('input').val());
                $('#hora_fin_martes').val($(values).find('input').val());
                $('#hora_fin_miercoles').val($(values).find('input').val());
                $('#hora_fin_jueves').val($(values).find('input').val());
                $('#hora_fin_viernes').val($(values).find('input').val());
                $('#hora_fin_sabado').val($(values).find('input').val());
                $('#hora_fin_domingo').val($(values).find('input').val());
                break;
            default:
                break;
        }
    }))
});