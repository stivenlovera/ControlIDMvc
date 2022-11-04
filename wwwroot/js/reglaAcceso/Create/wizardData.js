
function init_SmartWizard() {

    if (typeof ($.fn.smartWizard) === 'undefined') { return; }
    console.log('init_SmartWizard');

    $('#wizard').smartWizard({
        transitionEffect: 'slide',
        onFinish: function () {
            $("#formulario_reglas_acceso").submit();
        },
        onLeaveStep: function leaveAStepCallback(obj, contex) {
            $('#lista_personas').html('');
            $('#lista_horarios').html('');
            $('#lista_areas').html('');
            if (contex.toStep == 5) {
                const PersonasValues = $.map($('input[type=text][name="PersonasSelecionadasNombre[]"]'), function (el) { return el.value; });
                /* personas */
                personasHTML = ``;
                PersonasValues.forEach(persona => {
                    personasHTML = `
                        <blockquote class="message">
                        ${persona}
                        </blockquote>
                    `;
                });
                $('#lista_personas').append(personasHTML);
                /* horarios */
                const horarioValues = $.map($('input[type=text][name="HorarioSelecionadosNombre[]"]'), function (el) { return el.value; });
                horarioHTML = ``;
                horarioValues.forEach(horario => {
                    horarioHTML = `
                        <blockquote class="message">
                        ${horario}
                        </blockquote>
                    `;
                });
                $('#lista_horarios').append(horarioHTML);
                /* areas */
                const areasValues = $.map($('input[type=text][name="AreasSelecionadosNombre[]"]'), function (el) { return el.value; });
                areaHTML = ``;
                areasValues.forEach(area => {
                    areaHTML = `
                        <blockquote class="message">
                        ${area}
                        </blockquote>
                    `;
                });
                $('#lista_areas').append(areaHTML);
            }
            return true;
        }
    });

    $('.buttonNext').addClass('btn btn-success');
    $('.buttonNext').text('Siguiente');

    $('.buttonPrevious').addClass('btn btn-secondary');
    $('.buttonPrevious').text('Atras');

    $('.buttonFinish').addClass('btn btn-primary');
    $('.buttonFinish').text('Finalizar');


};
init_SmartWizard();

function validateSteps() {
    return true;
}
