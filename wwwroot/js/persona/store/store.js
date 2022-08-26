$(document).on('click', '#save_persona', function () {
    $.ajax({
        type: "POST",
        url: `/persona/store`,
        dataType: "json",
        data:$('#formulario_activacion').serialize(),
        success: function (response) {
            console.log(response)
        },
        
    });
});