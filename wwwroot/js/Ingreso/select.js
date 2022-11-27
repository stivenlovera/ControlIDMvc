
$( document ).ready(function() {
    //console.log("run")
    
});

function select() {
    $(".buscarPlanCaja").select2({
        ajax: {
            url: `/PlanDeCuentas/select-plan-cuenta`,
            type: "POST",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    searchTerm: params.term // search term
                };
            },
            processResults: function (response) {
                return {
                    results: response
                };
            },
            cache: true
        },
    })
    .on("select2:select", function (e) {
        //console.log(e.params.data["codigo"])
        $(this).parent().next().find('input').val(e.params.data["nombre"])
        //$(".nombre").val(e.params.data["nombre"]);
    });
}