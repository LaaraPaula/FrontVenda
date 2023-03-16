function cadastroFornecedor() {
    $.ajax(
        {
            method: "POST",
            url: "CadastroFornecedor",
            dataType: "json",
            data: Fornecedor,
            error: function (request, status, error) {
                debugger;
                alert(request.responseText);
            },
            success: function (retorno) {
                debugger;
                if (!edicao) {
                    alert('Fornecedor ' + Fornecedor.nome + ' cadastrado com sucesso');

                    openInNewTab('/Fornecedor/ExibeFornecedor');
                }
                else {
                    alert('Fornecedor ' + Fornecedor.nome + ' editado com sucesso');

                    openInNewTab('/Fornecedor/ExibeFornecedor');
                }
            },

        }
    )
}
function openInNewTab(url = "") {
    var win = window.open("https://localhost:5010" + url, "_self");
    win.focus();
}

function excluirFornecedor(id) {
    $.ajax(
        {
            method: "GET",
            url: "ExcluirFornecedor?id=" + id,
            success: function (retorno) {
                if (retorno.includes('deletado')) {
                    $("#tr-table-" + id).fadeOut();
                } alert(retorno);
            }

        }
    )
}
Fornecedor = new Object();
Fornecedor.id;
Fornecedor.nome;
Fornecedor.telefone;
Fornecedor.endereco;
Fornecedor.cnpj;

$("#id, #nome, #telefone, #endereco, #cnpj").keyup(function (e) {
    eval("Fornecedor." + e.target.id + " = e.target.value;");

});

var edicao = false;

$(document).ready(function (e) {
    Fornecedor.id = $("#id").val();
    Fornecedor.nome = $("#nome").val();
    Fornecedor.telefone = $("#telefone").val();
    Fornecedor.endereco = $("#endereco").val();
    Fornecedor.cnpj = $("#cnpj").val();

    var valida = parseInt(Fornecedor.id);
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
