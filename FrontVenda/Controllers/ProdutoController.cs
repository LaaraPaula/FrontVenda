using Microsoft.AspNetCore.Mvc;

namespace FrontVenda.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult ExibeProduto()
        {
            return View();
        }
        public IActionResult CadastroProduto()
        {
            return View();
        }

    }
}
