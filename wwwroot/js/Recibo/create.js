let cuenta = `
<div class="form-group row">
    <label class="col-form-label col-md-1 col-sm-1 "
        style="font-size:small; text-align:right">Cuenta</label>
    <div class="col-md-2 col-sm-2 ">
        <input type="text" class="form-control" placeholder="Còdigo" name="codigo"
            style="font-size:small">
    </div>
    <div class="col-md-2 col-sm-2 ">
        <input type="text" class="form-control" placeholder="Nombre" name="nombre"
            style="font-size:small">
    </div>
    <div class="col-md-2 col-sm-2 ">
        <input type="text" class="form-control" placeholder="DEBE" name="debe"
            style="font-size:small">
    </div>
    <div class="col-md-2 col-sm-2 ">
        <input type="text" class="form-control" placeholder="HABER" name="haber"
            style="font-size:small">
    </div>
    <div class="col-md-2 col-sm-2 ">
        <button class="btn btn-danger btn-xs delete_cuenta" type="button"  role="button"><i class="fa fa-trash"></i></button>
    </div>
</div>
`;
function item(cuenta_hijos) {
    return `
<div class="x_panel" style="background-color: aqua;">
    <div class="row">
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 " style="font-size:small">
                Concepto:</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Descripcion"
                    style="font-size:small">
            </div>
        </div>
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 "
                style="font-size:small">Monto</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Monto"
                    style="font-size:small">
            </div>
        </div>
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 "
                style="font-size:small">Facturado</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Monto"
                    style="font-size:small">
            </div>
        </div>
        <div class="col-md-3">
            <button class="btn btn-success btn-xs editar_item" type="button"><i
                class="fa fa-pencil"></i></button>
            <button class="btn btn-danger btn-xs delete_item" type="button"><i
                class="fa fa-trash"></i></button>
        </div>
    </div>
    <div class="text-center">
        <h4>Cuentas</h4>
        ${cuenta_hijos}
    </div>

</div>
`;
}

let item_cuenta = `
        <div class="row p-1">
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Cuenta</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Còdigo"
                        name="codigo" style="font-size:small">
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Nombre</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Còdigo"
                        name="codigo" style="font-size:small">
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Debe</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Còdigo"
                        name="codigo" style="font-size:small">
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Haber</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Còdigo"
                        name="codigo" style="font-size:small">
                </div>
            </div>
        </div>
`;
$(document).on('click', '#open_modal_item', function () {
    $('#modal_cuenta').modal('show');
});
$(document).on('click', '#añadir_cuenta', function () {
    $('#cuentas').append(cuenta);
});
$(document).on('click', '.delete_cuenta', function () {
    $(this).parent().parent().remove();
});

$(document).on('click', '.delete_item', function () {
    $(this).parent().parent().parent().remove();
});

$(document).on('click', '.editar_item', function () {
    $('#modal_cuenta').modal('show');
});
$(document).on('click', '#guardar_item', function () {
    $('#modal_cuenta').modal('hide');
    console.log($('#form_item input[name=codigo]').val());
    console.log($('#cuentas').children());
    let cuentas = ``;
    $('#cuentas').children().each(function (i, value) {
        console.log("aki")
        cuentas = cuentas + item_cuenta
    });
    let resultado = item(cuentas);
    $('#lista_items').append(resultado);
});

