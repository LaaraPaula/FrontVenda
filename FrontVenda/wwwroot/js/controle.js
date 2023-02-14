function Editar(id) {

}
function Excluir(id)
{
    $.ajax(
        {
            url: "http://localhost:5001/controller/DeletaCliente?id=" + id
            type: "DELETE",
            
        })
    .success(function (data)
    {
        var retorno = data;
        confirm(data.message);
        location.reload();
    });

}