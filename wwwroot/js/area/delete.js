
$(document).on("click", ".delete", function () {
    const id = $(this).data('id');
    Swal.fire({
        title: 'Esta seguro de eliminar esta area?',
        text: "Este procesos es irreversible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar esto!'
    }).then((result) => {
        if (result.isConfirmed) {
            eliminar_evento(id)
        }
    })
});
function eliminar_evento(id) {
    $.ajax({
        type: "DELETE",
        url: `/area/delete/${id}`,
        dataType: "json",
        success: function (response) {
            if (response.status == 'success') {
                Swal.fire(
                    response.message,
                    'Resgistro eliminado',
                    'success'
                );
                datatable.draw();
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error server',
                    html: '',
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            error_status(jqXHR);
        },
        fail: function (response) {
            fail()
        }
    });
    
}