using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Correio([FromBody] string cep)
        {
            #region Construtores
            string _uf = "";
            string _localidade = "";
            string _rua = "";
            string _bairro = "";
            string _cep = "";
            string _tipo = "";
            #endregion

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("endereco", cep),
                        new KeyValuePair<string, string>("tipoCEP","ALL"),
                        new KeyValuePair<string, string>("pagina","/app/endereco/index.php")
                    };

                var Request = new HttpRequestMessage(HttpMethod.Post, "https://buscacepinter.correios.com.br/app/endereco/carrega-cep-endereco.php")
                {
                    Content = new FormUrlEncodedContent(Parameters)
                };

                var Result = client.SendAsync(Request).Result.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<Model>(Result.Result);
                if (json.erro == false)
                {
                    if (json.total == 1 && json.dados is not null)
                    {
                        return Json(json.dados[0]);

                        _uf = json.dados[0].uf;
                        _localidade = json.dados[0].localidade;
                        _rua = json.dados[0].logradouroDNEC;
                        _bairro = json.dados[0].bairro;
                        _cep = cep;
                        _tipo = json.dados[0].tipoCep;


                    }
                    else if (json.total != 0)
                    {
                        return Json("Mais de um resultado");
                    }
                    else if (json.total == 0)
                    {
                        return Json("Insira um Cep Valido");
                    }
                }
                else
                {
                    return Json("Tente novamente, ocorreu um erro nesta solicitação");
                }
            }

            return Json("");
        }
    }
}