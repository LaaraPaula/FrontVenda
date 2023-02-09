using Microsoft.AspNetCore.Mvc;

namespace FrontVenda.Controllers
{
    public class ClienteController :Controller
    {
        public IActionResult ExibeCliente()
        {
            return View();
        }
        public IActionResult CadastroCliente()
        {
            return View();
        }
    }
}
