using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;

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
                return Ok(result);

            }
        }
        [HttpPost]
        public IActionResult CadastroFornecedor(Fornecedor fornecedor)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveFornecedor");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;

                _request.AddBody(fornecedor, "application/json");

                RestResponse response = _cliente.Execute(_request);

                HttpStatusCode statusCode = response.StatusCode;
                int iStatusCode = (int)statusCode;

                return StatusCode(iStatusCode, response.Content); ;
            }
            catch (WebException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult CadastroFornecedor()
        {
            return View(new Fornecedor());
        }
        [HttpGet]
        public IActionResult EditarFornecedor(int id = 0)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("ExibeFornecedorPorId?id=" + id);

            RestResponse response = _cliente.Execute(_request);

            fornecedor = System.Text.Json.JsonSerializer.Deserialize<Fornecedor>(response.Content);


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
