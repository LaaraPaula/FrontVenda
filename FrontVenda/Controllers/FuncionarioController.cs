using Microsoft.AspNetCore.Mvc;

namespace FrontVenda.Controllers
{
    public class FuncionarioController : Controller
    {
        public IActionResult ExibeFuncionario()
        {
            return View();
        }
        public IActionResult CadastroFuncionario()
        {
            return View();
        }
    }
}
