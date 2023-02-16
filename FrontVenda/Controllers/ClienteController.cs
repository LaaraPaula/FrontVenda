using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System;
using System.Net.Http;
using System.Text.Json;
using FrontVenda.Models;
using System.Collections.Generic;
using AngleSharp.Io;

namespace FrontVenda.Controllers
{
    public class ClienteController : Controller
    {
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
        public IActionResult CadastroCliente()
        {
            return View();
        }
        public IActionResult EditarCliente(int id)
        {
            return View();
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
            var clientes = JsonSerializer.Deserialize<List<Cliente>>(response);
            return clientes;
        }
    }
}
