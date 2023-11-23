$(document).ready(function () {
    $.getJSON('/Especialidade/SerchEspecialidades', function (data) {
        var dropdown = $('#especialidadeDropdown');
        dropdown.empty();
        dropdown.append($('<option />').val(0).text("Todos"));
        $.each(data, function (i, especialidade) {
            dropdown.append($('<option />').val(especialidade.id).text(especialidade.descricao));
        });
    });
});