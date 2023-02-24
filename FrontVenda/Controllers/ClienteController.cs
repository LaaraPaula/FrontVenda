using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System;
using System.Net.Http;
using System.Text.Json;
using FrontVenda.Models;
using System.Collections.Generic;
using AngleSharp.Io;
using RestSharp;
using Newtonsoft.Json;

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
                return RedirectToAction("ExibeCliente", "Cliente", new { Alerta = result });

            }
        }
        
        public IActionResult CadastroClienteForm(Cliente cliente)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveClient");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(cliente, "application/json");

                RestResponse response = _cliente.Execute(_request);

                if (response.Content.ToUpper().Contains("ID") && response.StatusCode == HttpStatusCode.OK)
                {
                    Cliente clienteCadastrado = JsonConvert.DeserializeObject<Cliente>(response.Content);

                    return RedirectToAction(
                                            "CadastroCliente",
                                            "Cliente",
                                            new
                                            {
                                                Alerta = clienteCadastrado.id > 0 ?
                                                "Cliente cadastrado com sucesso" : "Erro ao cadastrar cliente."
                                            });
                }
                else
                {
                    return RedirectToAction("CadastroCliente", "CLiente", new { Alerta = response.Content });

                }
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisição.\n" + ex.Message;
                
                return RedirectToAction("CadastroClienteView", "Cliente", new { Alerta = result });
            }
        }
        public IActionResult CadastroCliente(string alerta = null)
        {
            
            try
            {
                Cliente cliente = new Cliente();
                if (alerta != null)
                {
                    ViewBag.Alerta = alerta;
                }
                return View(cliente);
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
        public IActionResult EditarClienteForm(Cliente cliente)
        {
            _cliente = new RestClient(_urlBase);
            _request = new RestRequest("SaveClient");

            try
            {
                _request.Timeout = 0;
                _request.Method = Method.Post;
                _request.AddBody(cliente, "application/json");

                RestResponse response = _cliente.Execute(_request);

                if (response.Content.ToUpper().Contains("ID") && response.StatusCode == HttpStatusCode.OK)
                {
                    Cliente clienteCadastrado = JsonConvert.DeserializeObject<Cliente>(response.Content);

                    return RedirectToAction(
                                            "EditarCliente",
                                            "Cliente",
                                            new
                                            {
                                                Alerta = clienteCadastrado.id > 0 ?
                                                $"Cliente {cliente.nome} editado com sucesso" : $"Erro ao editar cliente {cliente.nome}."
                                            });
                }
                else
                {
                    return RedirectToAction("CadastroCliente", "CLiente", new { Alerta = response.Content });

                }
            }
            catch (WebException ex)
            {
                string result = "Erro ao realizar requisição.\n" + ex.Message;

                return RedirectToAction("CadastroClienteView", "Cliente", new { Alerta = result });
            }
        }
        public IActionResult EditarCliente(int id = 0)
        {
            Cliente cliente = new Cliente();
            cliente.id = id;

            ViewData["Titulo"] = "Editar";
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

                return RedirectToAction("ExibeCliente", "Cliente", new { Alerta = response });
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
                return RedirectToAction("ExibeCliente", "Cliente", new { Alerta = result });
                
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
