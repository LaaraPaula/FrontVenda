function cadastroProduto() {
    $.ajax(
        {
            method: "POST",
            url: "CadastroProduto",
            dataType: "json",
            data: Produto,
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (retorno) {
                if (!edicao) {
                    alert('Produto ' + Produto.nome + ' cadastrado(a) com sucesso');

                    openInNewTab('/Produto/ExibeProduto');
                }
                else {
                    alert('Produto ' + Produto.nome + ' editado(a) com sucesso');

                    openInNewTab('/Produto/ExibeProduto');
                }
            },

        })
}
function openInNewTab(url = "") {
    var win = window.open("https://localhost:5010" + url, "_self");
    win.focus();
}
function excluirProduto(id) {
    $.ajax(
        {
            method: "GET",
            url: "ExcluirProduto?id=" + id,
            success: function (retorno) {
                if (retorno.includes('deletado')) {
                    $("#tr-table-" + id).fadeOut();
                } alert(retorno);
            }

        })

}
Produto = new Object();
Produto.id;
Produto.nome;
Produto.descricao;
Produto.precoUnitario;
Produto.quantidadeEstoque;

$("#id, #nome, #descricao, #precoUnitario, #quantidadeEstoque").keyup(function (e) {
    eval("Produto." + e.target.id + " = e.target.value;");

});

var edicao = false;

$(document).ready(function (e) {
    Produto.id = $("#id").val();
    Produto.nome = $("#nome").val();
    Produto.descricao = $("#descricao").val();
    Produto.precoUnitario = $("#precoUnitario").val();
    Produto.quantidadeEstoque = $("#quantidadeEstoque").val();

    var valida = parseInt(Produto.id);

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

})
