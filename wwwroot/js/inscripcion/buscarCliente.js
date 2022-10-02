
$(document).on("click", "#buscar_cliente", function () {
    console.log('evento click');
    $('#contenido').html('');
    $('#modal_buscar_cliente').modal('show');
    $.ajax({
        type: 'POST',
        url: `/persona/buscar`,
        dataType: "json",
        data:{
            Ci:$('#PersonaId').val()
        },
        success: function (response) {
            $('#contenido').append(render_resutado(response));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR)
        },
        fail: function () {
            fail()
        }
    });


});
function render_resutado(response) {
    resultadoHtml = ``;
    response.forEach(persona => {
        resultadoHtml += ` 
    <div class="col-md-6 col-sm-6 profile_details">
        <div class="well profile_view">
            <div class="col-sm-12">
                <div class="left col-md-7 col-sm-7">
                    <p><strong>CI: </strong>${persona.ci}</p>
                    <p><strong>Nombres: </strong>${persona.nombre}</p>
                    <p><strong>Apellidos: </strong>${persona.apellido}</p>
                    <p><strong>Dirrecion: </strong>${persona.dirrecion}</p>
                    <ul class="list-unstyled">
                        <!--li><i class="fa fa-building"></i>${persona.dirrecion}</li-->
                        <li><i class="fa fa-phone"></i>${persona.celular} </li>
                    </ul>
                </div>
                <div class="right col-md-5 col-sm-5 text-center">
                    <img src="https://d500.epimg.net/cincodias/imagenes/2016/07/04/lifestyle/1467646262_522853_1467646344_noticia_normal.jpg"
                        alt="" class="img-circle img-fluid">
                </div>
            </div>
            <div class=" profile-bottom text-center">
                <div class="col-sm-12 emphasis">
                    <button type="button" class="btn btn-success btn-sm añadir_cliente" data-id="${persona.id}" data-ci="${persona.ci}" data-nombre="${persona.nombre}" data-apellido="${persona.apellido}"  data-fecha_inicio="${persona.fecha_inicio}"  data-fecha_fin="${persona.fecha_fin}">
                        Inscribir
                    </button>
                </div>
            </div>
        </div>
    </div>`;
    });
    return resultadoHtml;
}
/*detectar si el input esta lleno */
/* $('#CICliente').keyup(function(event) {
    if ($(this).val().length>0) {
        $('#buscar_cliente').prop('disabled',false)
    }else{
        $('#buscar_cliente').prop('disabled',true)
    }
}); */


/*captura de informacion */
$(document).on('click','.añadir_cliente',function(){
    /*adicionar campos */
    $('#CICliente').val($(this).data('ci'))    
    $('#Cliente').val(`${$(this).data('nombre')} ${$(this).data('apellido')}`)  
    $('#FechaInicio').val(moment($(this).data('fecha_inicio')).format("YYYY/MM/DD"))  
    $('#FechaFin').val(moment($(this).data('fecha_fin')).format("YYYY/MM/DD"))  
    $('#FechaInicioCliente').val(moment($(this).data('fecha_inicio')).format("YYYY/MM/DD"))  
    $('#FechaFinCliente').val(moment($(this).data('fecha_fin')).format("YYYY/MM/DD"))  
    //$('#CICliente').val($(this).data('ci'))  
    $('#modal_buscar_cliente').modal('hide');
});
