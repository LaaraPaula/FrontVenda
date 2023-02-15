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
        public IActionResult ExibeFuncionario(string alerta = null)
        {
            try
            {
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                var funcionarios = ObterFuncionario();

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
        public IActionResult ExcluirFuncionario(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/controller/deletafuncionario?id={id}");
            request.Method = "DELETE";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();

                return RedirectToAction("ExibeFuncionario", "Funcionario", new { Alerta = response });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        private List<Funcionario> ObterFuncionario()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibefuncionarios");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var funcionarios = JsonSerializer.Deserialize<List<Funcionario>>(response);
            return funcionarios;
        }
    }
    
}
