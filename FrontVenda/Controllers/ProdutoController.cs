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
    public class ProdutoController : Controller
    {
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
        public IActionResult CadastroProduto()
        {
            return View();
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

        private List<Produto> ObterProduto()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/controller/exibeprodutos");
            request.Method = "GET";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new(webStream);
            string response = responseReader.ReadToEnd();
            var produtos = JsonSerializer.Deserialize<List<Produto>>(response);
            return produtos;
        }

    }
}
