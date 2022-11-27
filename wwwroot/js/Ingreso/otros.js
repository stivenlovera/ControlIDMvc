function cuenta(PlanId,PlanCuenta,Debe,Haber) {
    return `
    <div class="form-group row">
        <label class="col-form-label col-md-1 col-sm-1 "
            style="font-size:small; text-align:right">Cuenta</label>
        <div class="col-md-2 col-sm-2 ">
         <select class="js-example-responsive buscarPlanCaja" value="${PlanId}" style="width: 100%" style="font-size:small">
            <option value="${PlanId}"> ${PlanId}</option>
         </select>
        </div>
        <div class="col-md-2 col-sm-2 ">
            <input type="text" class="form-control nombre" placeholder="Nombre" name="nombre"
                style="font-size:small" value="${PlanCuenta}" readonly>
        </div>
        <div class="col-md-2 col-sm-2 ">
            <input type="text" class="form-control debe" placeholder="DEBE" name="debe"
                style="font-size:small" value="${Debe}" >
        </div>
        <div class="col-md-2 col-sm-2 ">
            <input type="text" class="form-control haber" placeholder="HABER" name="haber"
                style="font-size:small" value="${Haber}" >
        </div>
        <div class="col-md-2 col-sm-2 ">
            <button class="btn btn-danger btn-xs delete_cuenta" type="button"  role="button"><i class="fa fa-trash"></i></button>
        </div>
    </div>
    `;
}

function item(descripcion, monto, facturado, cuenta_hijos) {
    return `
<div class="x_panel" style="background-color: #F9F4F4;">
    <div class="row">
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 " style="font-size:small">
                Concepto:</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Descripcion" value="${descripcion}"
                    style="font-size:small" readonly>
            </div>
        </div>
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 "
                style="font-size:small">Monto</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Monto" value="${monto}"
                    style="font-size:small" readonly>
            </div>
        </div>
        <div class="col-md-3">
            <label class="col-form-label col-md-4 col-sm-4 "
                style="font-size:small">Facturado</label>
            <div class="col-md-8 col-sm-8 ">
                <input type="text" class="form-control" placeholder="Facturado" value="${facturado}"
                    style="font-size:small" readonly>
            </div>
        </div>
        <div class="col-md-3">
            <button class="btn btn-success btn-xs editar_item" type="button"><i
                class="fa fa-pencil"></i></button>
            <button class="btn btn-danger btn-xs delete_item" type="button"><i
                class="fa fa-trash"></i></button>
        </div>
    </div>
    <h4>Cuentas</h4>
    <div class="text-center cuenta_hijo">
        ${cuenta_hijos}
    </div>

</div>
`;
}

function CuentaItem(cuenta, nombre, debe, haber) {
    return `
        <div class="row p-1">
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Cuenta</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Cuenta"
                        name="cuenta" value="${cuenta}" style="font-size:small" readonly>
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Nombre</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Nombre"
                        name="nombre" value="${nombre}" style="font-size:small" readonly>
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Debe</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Debe"
                        name="debe" value="${debe}" style="font-size:small" readonly>
                </div>
            </div>
            <div class="col-md-3">
                <label class="col-form-label col-md-6 col-sm-6 "
                    style="font-size:small; text-align:right">Haber</label>
                <div class="col-md-6 col-sm-6 ">
                    <input type="text" class="form-control" placeholder="Haber"
                        name="haber" value="${haber}" style="font-size:small" readonly>
                </div>
            </div>
        </div>
`;
}
$(document).on('click', '#open_modal_item', function () {
    $("#modal_cuenta").removeAttr("tabindex");
    $('#modal_cuenta').modal('show');
    $("#form_item").trigger("reset");
    $('#cuentas').html('');

});
$(document).on('click', '#añadir_cuenta', function () {
    $('#cuentas').append(cuenta('','','',''));
    select();
});
$(document).on('click', '.delete_cuenta', function () {
    $(this).parent().parent().remove();
});

$(document).on('click', '.delete_item', function () {
    $(this).parent().parent().parent().remove();
    actualizacion_monto()
});

$(document).on('click', '.editar_item', function () {
    $('#cuentas').html('');
    let consepto_item;
    let monto_item;
    let facturado_item;
    let cuentas = [];
    $(this).parent().parent().parent().children().each(function (i, val) {
        switch (i) {
            case 0:
                console.log(i, $(val).children().each(function (j, valor) {
                    switch (j) {
                        case 0:
                            consepto_item = $(valor).find('div').find('input').val()
                            break;
                        case 1:
                            monto_item = $(valor).find('div').find('input').val()
                            break;
                        case 2:
                            facturado_item = $(valor).find('div').find('input').val()
                            break;

                        default:
                            break;
                    }
                }));
                break;
            case 2:
                console.log(i, $(val).children().each(function (index, val) {
                    let PlanId = ""
                    let PlanCuenta = ""
                    let Debe = 0
                    let Haber = 0
                    console.log($(val).children().each(function (k, aux) {
                        switch (k) {
                            case 0:
                                PlanId = $(aux).find('div').find('input').val()
                                break
                            case 1:
                                PlanCuenta = $(aux).find('div').find('input').val()
                                break;
                            case 2:
                                Debe = $(aux).find('div').find('input').val()
                                break;
                            case 3:
                                Haber = $(aux).find('div').find('input').val()
                                break;

                            default:
                                break;
                        }
                    }));
                    cuentas.push({
                        PlanId: PlanId,
                        PlanCuenta: PlanCuenta,
                        Debe: Debe,
                        Haber: Haber
                    });
                }));
                break;

            default:
                break;
        }
    });
    edit_modal(consepto_item, monto_item, facturado_item, cuentas);
    actualizacion_monto(consepto_item, monto_item, facturado_item);
    select()
    $('#modal_cuenta').modal('show');
});
function edit_modal(consepto_item, monto_item, facturado_item, cuentas) {
    console.log("relleno ", consepto_item, monto_item, facturado_item, cuentas)
    $('#form_item #consepto_item').val(consepto_item);
    $('#form_item #monto_item').val(monto_item);
    $('#form_item #facturado_item').val(facturado_item);
    let cuentas_hijos = ``;
    cuentas.forEach((plan, index) => {
        cuentas_hijos += cuenta(plan.PlanId, plan.PlanCuenta, plan.Debe, plan.Haber)
    });
    $('#cuentas').append(cuentas_hijos);
}
$(document).on('click', '#guardar_item', function () {
    guardar_item();
    actualizacion_monto();
});

/*Control Monto */
$(document).on('keyup', '#monto_item', function () {

});

/*guardar items */

function guardar_item() {
    let facturado = $('#form_item #facturado_item').val();
    let consepto = $('#form_item #consepto_item').val();
    let monto = $('#form_item #monto_item').val();
    //validate
    if ($('#cuentas').children().length == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            html: 'por favor agrege al menos una cuenta'
        });
    } else {
        let planes = [];
        let nombres = [];
        let deberes = [];
        let haberes = [];
        $('#cuentas').children().each(function (index, value) {
            $(value).children().each(function (i, valor) {
                //console.log(plan)
                switch (i) {
                    case 1:
                        planes.push($(valor).find('select').val())
                        break;
                    case 2:
                        nombres.push($(valor).find('input').val())
                        break;
                    case 3:
                        deberes.push($(valor).find('input').val())
                        break;
                    case 4:
                        haberes.push($(valor).find('input').val())
                        break;

                    default:
                        break;
                }
            })
        });
        let cuentas_hijos = ``;
        planes.forEach((plan, index) => {
            cuentas_hijos += CuentaItem(plan, nombres[index], deberes[index], haberes[index])
        });
        $('#lista_items').append(item(consepto, monto, facturado, cuentas_hijos));
        $('#modal_cuenta').modal('hide');
    }
}

/*guardar items */


function actualizacion_monto() {
    /*analizando valores */
    let entrege = $('#entreado_a').val();
    let monto_literal = $('#monto_literal').val();
    let fecha = $('#fecha').val();
    let usuario = $('#usuario').val();
    let numeroRecibo = $('#nro_recibo').val();
    let monto_total = 0;
    let monto_concepto = 0;
    let items = [];

    /*items */
    let concepto = "";
    $('#lista_items').children().each(function (index, value) {
        //console.log(value)
        let planes = [];
        $(value).children().each(function (i, val) {

            switch (i) {
                case 0:
                    $(val).children().each(function (j, monto) {
                        switch (j) {
                            case 0:
                                $(monto).children().each(function (k, valor) {
                                    switch (k) {
                                        case 0:

                                            break;
                                        case 1:
                                            concepto = $(valor).find('input').val()
                                            break;
                                        default:
                                            break;
                                    }
                                });
                                break;
                            case 1:
                                $(monto).children().each(function (k, valor) {
                                    switch (k) {
                                        case 0:

                                            break;
                                        case 1:
                                            monto_total += parseInt($(valor).find('input').val());
                                            monto_concepto = parseInt($(valor).find('input').val());
                                            break;
                                        default:
                                            break;
                                    }
                                });
                                break;
                            default:
                                break;
                        }
                    });

                    break;
                case 2:
                    $(val).children().each(function (j, plan) {
                        //console.log(j, plan)
                        //valores
                        let PlanId = ""
                        let PlanCuenta = ""
                        let Debe = 0
                        let Haber = 0
                        $(plan).children().each(function (k, aux) {
                            switch (k) {
                                case 0:
                                    PlanId = $(aux).find('div').find('input').val()
                                    break
                                case 1:
                                    PlanCuenta = $(aux).find('div').find('input').val()
                                    break;
                                case 2:
                                    Debe = $(aux).find('div').find('input').val()
                                    break;
                                case 3:
                                    Haber = $(aux).find('div').find('input').val()
                                    break;

                                default:
                                    break;
                            }
                        });
                        planes.push({
                            PlanId,
                            PlanCuenta,
                            Debe,
                            Haber
                        });
                    });
                    break;
                default:
                    break;
            }
        });
        items.push(
            {
                Concepto: concepto,
                Monto: monto_concepto,
                Planes: planes,
                Facturacion: 1
            });
    });
    $('#monto').val(monto_total);
    /*  console.log({
         EntregeA:entrege,
         Monto:monto_total,
         MontoLiteral:monto_literal,
         Fecha:fecha,
         NroRecibo:numeroRecibo,
         Items:items
     }); */
    return {
        Usuario: usuario,
        EntregeA: entrege,
        Monto: monto_total,
        MontoLiteral: monto_literal,
        Fecha: fecha,
        NroRecibo: numeroRecibo,
        Items: items
    }
}
