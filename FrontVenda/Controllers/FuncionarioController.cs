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
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisição.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("ExibeFuncionario", "Funcionario", new { Alerta = result });

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
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisição.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("ExibeFuncionario", "Funcionario", new { Alerta = result });

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
