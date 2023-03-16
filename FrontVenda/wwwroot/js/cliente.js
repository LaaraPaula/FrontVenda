//$("#botao-excluir").on('click', function () {
//    excluirCliente();
//});
function cadastroCliente() {
    $.ajax(
        {
            method: "POST",
            url: "CadastroCliente",
            dataType: "json",
            data: Cliente,
            error: function (request, status, error)
            {
                alert(request.responseText);
            },
            success: function (retorno) 
            {
                if (!edicao) {
                    alert('Cliente ' + Cliente.nome + ' cadastrado(a) com sucesso');

                    openInNewTab();
                }
                else {
                    alert('Cliente ' + Cliente.nome + ' editado(a) com sucesso');

                    openInNewTab();
                }
            },        

        })
}
function openInNewTab(url = "") {
    var win = window.open("https://localhost:5010" + url, "_self");
    win.focus();
}
function excluirCliente(id) {    
    $.ajax(
        {
            method: "GET",
            url: "Cliente/ExcluirCliente?id=" + id,
            success: function (retorno) {
                if (retorno.includes('deletado'))
                {
                    $("#tr-table-" + id).fadeOut();
                } alert(retorno);
            }

        })
}
Cliente = new Object();
Cliente.id;
Cliente.nome;
Cliente.telefone;
Cliente.endereco;
Cliente.cpf;

$("#id, #nome, #telefone, #endereco, #cpf").keyup(function (e) {
    eval("Cliente." + e.target.id + " = e.target.value;");

});

var edicao = false;

$(document).ready(function (e) {
    Cliente.id = $("#id").val();
    Cliente.nome = $("#nome").val();
    Cliente.telefone = $("#telefone").val();
    Cliente.endereco = $("#endereco").val();
    Cliente.cpf = $("#cpf").val();

    var valida = parseInt(Cliente.id);
    //Para valida se está editando ou cadastrando    
    
    //Verifica se tem um id na tela, se tiver é porque é um editar
    if (valida > 0) {
        $("#btnEnviar").html("Editar"); //Troca o texto do botão
        $("#btnEnviar").removeClass("btn btn-primary"); //Remove a cor azul
        $("#btnEnviar").addClass("btn btn-warning"); //Adiciona a cor amarela
        $("#btnVoltar").removeClass("btn btn-primary");
        $("#btnVoltar").addClass("btn btn-warning");
        $("#divTitulo").html("Editar"); //troca o título para editar
        $('.nav-pills .nav-link.active, .nav-pills .show>.nav-link').css("background-color", "#ffc107"); //Muda a cor do background do active

        edicao = true; // indica que o processo atual é edição    
    }

})

