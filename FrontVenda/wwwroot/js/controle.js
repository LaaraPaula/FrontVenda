function Formata(valor) {
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
        var local = url_atual.substring((url_atual.indexOf("Editar") + 6), url_atual.lastIndexOf("/")); //Pega o ambiente depois de Edit e antes da barra

        switch (local) {
            case "Funcionario":
            case "Cliente":
                document.getElementById('cpf').setAttribute('disabled', 'disabled');
                break;

            case "Produto":
                document.getElementById('quantidadeEstoque').setAttribute('disabled', 'disabled');
                break;

            default:
                document.getElementById('cnpj').setAttribute('disabled', 'disabled');
        }

    }

}

VerificaeEdit();