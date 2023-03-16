using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using FrontVenda.Models;
using System.Collections.Generic;
using RestSharp;

namespace FrontVenda.Controllers
{
    public class ClienteController : Controller
    {
        private RestRequest _request;
        private RestClient _cliente;
        private const string _urlBase = "https://localhost:5001/controller/";

        public IActionResult ExibeCliente(string alerta = null)
        {
            try
            {
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                var clientes = ObterClientes();

                return View(clientes);
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
        public IActionResult CadastroCliente(Cliente cliente)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveClient");
            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(cliente, "application/json");

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
        public IActionResult CadastroCliente()
        {
            return View(new Cliente());
        }
        [HttpGet]
        public IActionResult EditarClienteView(int id = 0)
        {
            Cliente cliente = new Cliente();
            cliente.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("ExibeClientesPorId?id=" + id);

            RestResponse response = _cliente.Execute(_request);

            cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>(response.Content);

            return View("CadastroCliente", cliente);
        }
        public IActionResult ExcluirCliente(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/controller/deletacliente?id={id}");
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

        private List<Cliente> ObterClientes()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibeclientes");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var clientes = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(response);
            return clientes;
        }
    }
}
