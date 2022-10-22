/*obtener data de table*/
function obtener_data_table() {
    let total_egreso = 0;
    let total_ingreso = 0;
    let total = 0;
    table.rows().eq(0).each(function (index) {
        var row = table.row(index);
        var data = row.data();
        console.log("data", data)
        total_ingreso += data.ingreso;
        total_egreso += data.egreso;
    });
    total = (total_ingreso - total_egreso);
    return {
        total_egreso,
        total_ingreso,
        total,
    }

}
function recarga_totales() {
    setTimeout(function(){
        const resutados=obtener_data_table();
        $('#total_egreso').text( resutados.total_egreso);
        $('#total_ingreso').text( resutados.total_ingreso);
        $('#total').text( resutados.total);
    }, 800);
}
$(document).ready(function () {
    recarga_totales();
});