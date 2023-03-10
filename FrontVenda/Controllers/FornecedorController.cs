using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using RestSharp;
using Newtonsoft.Json;

namespace FrontVenda.Controllers
{
    public class FornecedorController : Controller
    {
        private RestRequest _request;
        private RestClient _cliente;
        private const string _urlBase = "https://localhost:5001/controller/";
        public IActionResult ExibeFornecedor(string alerta = null)
        {
            try
            {
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                var fornecedor = ObterFornecedor();

                return View(fornecedor);
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
                return RedirectToAction("ExibeFornecedor", "Fornecedor", new { Alerta = result });

            }
        }
        public IActionResult CadastroFornecedorForm(Fornecedor fornecedor)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveFornecedor");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(fornecedor, "application/json");

                RestResponse response = _cliente.Execute(_request);

                if (response.Content.ToUpper().Contains("ID") && response.StatusCode == HttpStatusCode.OK)
                {
                    Fornecedor fornecedorCadastrado = JsonConvert.DeserializeObject<Fornecedor>(response.Content);

                    if (fornecedor.id == 0)
                    {
                        return RedirectToAction(
                                                "CadastroFornecedor",
                                                "Fornecedor",
                                                new
                                                {
                                                    Alerta = fornecedorCadastrado.id > 0 ?
                                                    "Fornecedor cadastrado com sucesso" : "Erro ao cadastrar fornecedor."
                                                });
                    }

                    return RedirectToAction(
                                                "ExibeFornecedor",
                                                "Fornecedor",
                                                new
                                                {
                                                    Alerta = fornecedorCadastrado.id > 0 ?
                                                    $"Fornecedor editado com sucesso" : $"Erro ao editar cliente ."
                                                });
                }
                return RedirectToAction("CadastroFornecedor", "Fornecedor", new { Alerta = response.Content.Replace("\"", "") });
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisiçao.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("CadastroFornecedorView", "Fornecedor", new { Alerta = result });
            }
        }
        public IActionResult CadastroFornecedor(string alerta = null)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor();
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                return View(fornecedor);
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisiçao.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("CadastroFornecedorView", "Fornecedor", new { Alerta = result });
            }
        }
        public IActionResult EditarFornecedor(int id)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest($"ExibeFornecedorPorId?id={fornecedor.id}");

            RestResponse response = _cliente.Execute(_request);
            fornecedor = JsonConvert.DeserializeObject<Fornecedor>(response.Content);

            ViewData["Titulo"] = "Editar";
            return View("CadastroFornecedor", fornecedor);
        }
        public IActionResult ExcluirFornecedor(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/controller/deletafornecedor?id={id}");
            request.Method = "DELETE";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();

                return RedirectToAction("ExibeFornecedor", "Fornecedor", new { Alerta = response.Replace("\"", "") });
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisiçao.\n" + ex.Message;
                var response = (HttpWebResponse)ex.Response;

                if (response != null)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    result = stream.ReadToEnd().ToString();

                    if (string.IsNullOrEmpty(result))
                        result = ex.Message;
                }
                return RedirectToAction("ExibeFornecedor", "Fornecedor", new { Alerta = result });

            }

        }
        private List<Fornecedor> ObterFornecedor()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibefornecedores");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var fornecedores = System.Text.Json.JsonSerializer.Deserialize<List<Fornecedor>>(response);
            return fornecedores;
        }
    }
}
