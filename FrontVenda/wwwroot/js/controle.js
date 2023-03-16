﻿function Formata(valor) {
    var valFormat = valor.replace(/\D/g, "");

    valFormat = (valFormat / 100).toFixed(2) + "";
    valFormat = valFormat.replace(".", ",")
    valFormat = valFormat.replace(/(\d)(\d{3})(\d{3}),/g, "$1.$2.$3,");
    valFormat = valFormat.replace(/(\d)(\d{3}),/g, "$1.$2,");

    document.getElementById("precoUnitario").value = valFormat;

}

function VerificaeEdit() {
    var url_atual = window.location.href;

    if (url_atual.includes("Editar")) {        

        $(".validaDesabilita").prop("disabled", true);

    }

}

VerificaeEdit();
//var local = url_atual.substring((url_atual.indexOf("Editar") + 6), url_atual.lastIndexOf("/")); //Pega o ambiente depois de Edit e antes da barra
//switch (local) {
//    case "Funcionario":
//    case "Cliente":
//        $("#cpf").prop("disabled", true);
//        break;

//    case "Produto":
//        $("#quantidadeEstoque").prop("disabled", true);
//        break;

//    default:
//        $("#cnpj").prop("disabled", true);
//}