using Microsoft.AspNetCore.Mvc;

namespace FrontVenda.Controllers
{
    public class FornecedorController :Controller
    {
        public IActionResult ExibeFornecedor()
        {
            return View();
        }
        public IActionResult CadastroFornecedor()
        {
            return View();
        }
    }
}
