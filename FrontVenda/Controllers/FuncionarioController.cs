using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using System.Text.Json;

namespace FrontVenda.Controllers
{
    public class FuncionarioController : Controller
    {
        public IActionResult ExibeFuncionario()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibefuncionarios");
            request.Method = "GET";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();
                var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(response);
                return View(funcionarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public IActionResult CadastroFuncionario()
        {
            return View();
        }
    }
}
