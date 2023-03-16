using System.Collections.Generic;
using System.IO;
using System.Net;
using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(result);
            }
        }
        [HttpPost]
        public IActionResult CadastroProduto(Produto produto)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveProduto");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(produto, "application/json");

                RestResponse response = _cliente.Execute(_request);

                HttpStatusCode statusCode = response.StatusCode;
                int iStatusCode = (int)statusCode;

                return StatusCode(iStatusCode, response.Content);
            }
            catch (WebException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult CadastroProduto()
        {
            return View(new Produto());
        }
        [HttpGet]
        public IActionResult EditarProduto(int id)
        {
            Produto produto = new Produto();
            produto.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest($"ExibeProdutosPorId?id={produto.id}");

            RestResponse response = _cliente.Execute(_request);
            produto = JsonConvert.DeserializeObject<Produto>(response.Content);

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

                return Ok(response);
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
                return Ok(result);
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