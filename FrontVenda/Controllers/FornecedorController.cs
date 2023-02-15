using FrontVenda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.SharePoint.Client;

namespace FrontVenda.Controllers
{
    public class FornecedorController :Controller
    {
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public IActionResult CadastroFornecedor()
        {
            return View();
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

                return RedirectToAction("ExibeFornecedor", "Fornecedor", new { Alerta = response });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            var fornecedores = JsonSerializer.Deserialize<List<Fornecedor>>(response);
            return fornecedores;
        }
    }
}
