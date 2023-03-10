using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using AngleSharp.Io;
using Newtonsoft.Json;
using RestSharp;

namespace FrontVenda.Controllers
{
    public class ProdutoController : Controller
    {
        private RestRequest _request;
        private RestClient _cliente;
        private const string _urlBase = "https://localhost:5001/controller/";
        public IActionResult ExibeProduto(string alerta = null)
        {
            try
            {
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                var produtos = ObterProduto();

                return View(produtos);
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
                return RedirectToAction("ExibeProduto", "Produto", new { Alerta = result });

            }
        }
        public IActionResult CadastroProdutoForm(Produto produto)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveProduto");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(produto, "application/json");

                RestResponse response = _cliente.Execute(_request);

                if (response.Content.ToUpper().Contains("ID") && response.StatusCode == HttpStatusCode.OK)
                {
                    Produto produtoCadastrado = JsonConvert.DeserializeObject<Produto>(response.Content);

                    if (produto.id == 0)
                    {
                        return RedirectToAction(
                                                "CadastroProduto",
                                                "Produto",
                                                new
                                                {
                                                    Alerta = produtoCadastrado.id > 0 ?
                                                    "Produto cadastrado com sucesso" : "Erro ao cadastrar produto."
                                                });
                    }
                    return RedirectToAction(
                                            "ExibeProduto",
                                            "Produto",
                                            new
                                            {
                                                Alerta = produtoCadastrado.id > 0 ?
                                                $"Produto  editado com sucesso" : $"Erro ao editar produto ."
                                            });
                }
                return RedirectToAction("CadastroProduto", "Produto", new { Alerta = response.Content.Replace("\"", "") });
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisiçao.\n" + ex.Message;

                return RedirectToAction("CadastroProdutoView", "Produto", new { Alerta = result });
            }
        }
        public IActionResult CadastroProduto(string alerta = null)
        {
            try
            {
                Produto produto = new Produto();
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                return View(produto);
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
                return RedirectToAction("CadastroProdutoView", "Produto", new { Alerta = result });
            }
        }
        public IActionResult EditarProduto(int id)
        {
            Produto produto = new Produto();
            produto.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest($"ExibeProdutosPorId?id={produto.id}");

            RestResponse response = _cliente.Execute(_request);
            produto = JsonConvert.DeserializeObject<Produto>(response.Content);

            ViewData["Titulo"] = "Editar";
            return View("CadastroProduto", produto);
        }
        public IActionResult ExcluirProduto(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/controller/deletaproduto?id={id}");
            request.Method = "DELETE";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();

                return RedirectToAction("ExibeProduto", "Produto", new { Alerta = response });
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
                return RedirectToAction("ExibeProduto", "Produto", new { Alerta = result });

            }

        }

        private List<Produto> ObterProduto()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibeprodutos");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var produtos = System.Text.Json.JsonSerializer.Deserialize<List<Produto>>(response);
            return produtos;
        }

    }
}
