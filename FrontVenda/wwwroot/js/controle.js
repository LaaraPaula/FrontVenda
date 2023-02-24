function Formata(valor)
{
    var valFormat = valor.replace(/\D/g, "");

    valFormat = (valFormat / 100).toFixed(2) + "";
    valFormat = valFormat.replace(".", ",")
    valFormat = valFormat.replace(/(\d)(\d{3})(\d{3}),/g, "$1.$2.$3,");
    valFormat = valFormat.replace(/(\d)(\d{3}),/g, "$1.$2,");

    document.getElementById("precoUnitario").value = valFormat;

}