function cadastroFuncionario() {
    $.ajax(
        {
            method: "POST",
            url: "CadastroFuncionario",
            dataType: "json",
            data: Funcionario,
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (retorno) {
                if (!edicao) {
                    alert('Funcionario ' + Funcionario.nome + ' cadastrado(a) com sucesso');

                    openInNewTab('/Funcionario/ExibeFuncionario');
                }
                else {
                    alert('Funcionario ' + Funcionario.nome + ' editado(a) com sucesso');

                    openInNewTab('/Funcionario/ExibeFuncionario');
                }
            },

        })
}
function openInNewTab(url = "") {
    var win = window.open("https://localhost:5010" + url, "_self");
    win.focus();
}
function excluirFuncionario(id) {
    debugger;
    $.ajax(
        {
            method: "GET",
            url: "ExcluirFuncionario?id=" + id,
            success: function (retorno) {
                if (retorno.includes('deletado')) {
                    $("#tr-table-" + id).fadeOut();
                } alert(retorno);
            }

        })

}

Funcionario = new Object();
Funcionario.id;
Funcionario.nome;
Funcionario.telefone;
Funcionario.endereco;
Funcionario.cpf;
Funcionario.cargo;

$("#id, #nome, #telefone, #endereco, #cpf, #cargo").keyup(function (e) {
    eval("Funcionario." + e.target.id + " = e.target.value;");

});

var edicao = false;

$(document).ready(function (e) {
    Funcionario.id = $("#id").val();
    Funcionario.nome = $("#nome").val();
    Funcionario.telefone = $("#telefone").val();
    Funcionario.endereco = $("#endereco").val();
    Funcionario.cpf = $("#cpf").val();
    Funcionario.cargo = $("#cargo").val();

    var valida = parseInt(Funcionario.id);
    
    if (valida > 0) {
        $("#btnEnviar").html("Editar"); 
        $("#btnEnviar").removeClass("btn btn-primary"); 
        $("#btnEnviar").addClass("btn btn-warning"); 
        $("#btnVoltar").removeClass("btn btn-primary");
        $("#btnVoltar").addClass("btn btn-warning");
        $("#divTitulo").html("Editar");
        $('.nav-pills .nav-link.active, .nav-pills .show>.nav-link').css("background-color", "#ffc107"); 

        edicao = true;   
    }
});
