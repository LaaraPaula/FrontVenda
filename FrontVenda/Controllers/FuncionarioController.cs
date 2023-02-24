using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using System.Text.Json;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.SharePoint.Client;

namespace FrontVenda.Controllers
{
    public class FuncionarioController : Controller
    {
        private RestRequest _request;
        private RestClient _cliente;
        private const string _urlBase = "https://localhost:5001/controller/";
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
        public IActionResult CadastroFuncionarioForm(Funcionario funcionario)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveFuncionario");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(funcionario, "application/json");

                RestResponse response = _cliente.Execute(_request);

                if (response.Content.ToUpper().Contains("ID") && response.StatusCode == HttpStatusCode.OK)
                {
                    Funcionario funcionarioCadastrado = JsonConvert.DeserializeObject<Funcionario>(response.Content);

                    return RedirectToAction(
                                            "CadastroFuncionario",
                                            "Funcionario",
                                            new
                                            {
                                                Alerta = funcionarioCadastrado.id > 0 ?
                                                "Funcionario cadastrado com sucesso" : "Erro ao cadastrar funconario."
                                            });
                }
                else
                {
                    return RedirectToAction("CadastroFuncionario", "Funcionario", new { Alerta = response.Content });

                }
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisição.\n" + ex.Message;
                
                return RedirectToAction("CadastroFuncioanrioView", "Funcionario", new { Alerta = result });
            }
        }
        public IActionResult CadastroFuncionario(string alerta = null)
        {
            try
            {
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                return View();
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisão.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("CadastroClienteView", "Cliente", new { Alerta = result });
            }
        }
        public IActionResult EditarFuncionario(int id)
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
        private List<FuncionarioGet> ObterFuncionario()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibefuncionarios");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var funcionarios = System.Text.Json.JsonSerializer.Deserialize<List<FuncionarioGet>>(response);
            return funcionarios;
        }
    }
    
}
