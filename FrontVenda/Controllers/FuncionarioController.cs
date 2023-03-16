using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;
using Newtonsoft.Json;

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
                return Ok(result);

            }
        }
        [HttpPost]
        public IActionResult CadastroFuncionario(Funcionario funcionario)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveFuncionario");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(funcionario, "application/json");

                RestResponse response = _cliente.Execute(_request);

                HttpStatusCode statusCode = response.StatusCode;
                int iStatusCode = (int)statusCode;

                return StatusCode(iStatusCode, response.Content);
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisiçao.\n" + ex.Message;
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult CadastroFuncionario()
        {
            return View(new Funcionario());
        }
        [HttpGet]
        public IActionResult EditarFuncionario(int id)
        {
            Funcionario funcionario = new Funcionario();
            funcionario.id = id;

            _cliente = new RestClient(_urlBase);
            _request = new RestRequest($"ExibeFuncionarioPorId?id={funcionario.id}");

            RestResponse response = _cliente.Execute(_request);
            funcionario = JsonConvert.DeserializeObject<Funcionario>(response.Content);

            return View("CadastroFuncionario", funcionario);
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
