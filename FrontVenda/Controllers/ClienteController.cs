using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System;
using System.Net.Http;
using System.Text.Json;
using FrontVenda.Models;
using System.Collections.Generic;

namespace FrontVenda.Controllers
{
    public class ClienteController :Controller
    {
        public IActionResult ExibeCliente()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibeclientes");
            request.Method = "GET";
            //request.ContentType = "application/json";
            //request.ContentLength = DATA.Length;
            //StreamWriter requestWriter = new(request.GetRequestStream(), System.Text.Encoding.ASCII);
            //requestWriter.Write(DATA);
            //requestWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new(webStream);
                string response = responseReader.ReadToEnd();
                var clientes = JsonSerializer.Deserialize<List<Cliente>>(response);
                return View(clientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public IActionResult CadastroCliente()
        {
            return View();
        }
        public IActionResult ExcluirCliente(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/deletacliente");
            request.Method = "DELETE";
        }
    }
}
