using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Text.Json;

namespace FrontVenda.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult ExibeProduto()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibeprodutos");
            request.Method = "GET";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();
                var produtos = JsonSerializer.Deserialize<List<Produto>>(response);
                return View(produtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }
        public IActionResult CadastroProduto()
        {
            return View();
        }

    }
}
