using FrontVenda.Response;
using Refit;
using System.Threading.Tasks;

namespace FrontVenda.Interfaces
{
    public interface IClienteApiService
    {
        [Get("/controller/ExibeClientes/")]
        Task <ClienteResponse> GetAddressAsync(string cep);
    }
}
