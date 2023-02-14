using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Text.Json;

namespace FrontVenda.Controllers
{
    public class FornecedorController :Controller
    {
        public IActionResult ExibeFornecedor()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibefornecedores");
            request.Method = "GET";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();
                var fornecedores = JsonSerializer.Deserialize<List<Fornecedor>>(response);
                return View(fornecedores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public IActionResult CadastroFornecedor()
        {
            return View();
        }
    }
}
