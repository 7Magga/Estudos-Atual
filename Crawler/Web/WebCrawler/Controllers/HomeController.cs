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

        public JsonResult Correio([FromBody]string cep)
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
        public JsonResult Cotacoes()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Request = new HttpRequestMessage(HttpMethod.Get, "https://api.cotacoes.uol.com/mixed/summary?&currencies=1,11,5&itens=1,23243,1168&fields=name,openbidvalue,askvalue,variationpercentbid,price,exchangeasset,open,pctChange,date,abbreviation&jsonp=jsonp");

                var Result = client.SendAsync(Request).Result.Content.ReadAsStringAsync();
                string teste = Result.Result.Replace("/**/jsonp(", "").Replace(");", "");

                var json = JsonSerializer.Deserialize<ReqMoedaModel>(teste);
                if (json.docs != null)
                {
                    return Json(json.docs);

                    //Console.WriteLine($"Resultado do(a) {doc.name}");
                    //Console.WriteLine($"-Abriu em R${doc.openbidvalue} Fechou em R${doc.askvalue} com uma variação de {doc.variationpercentbid}");
                    //Console.WriteLine("");
                    //Console.WriteLine($"Resultado do(a) {doc.exchangeasset}");
                    //Console.WriteLine($"-Abriu em R${doc.open} Fechou em R${doc.price} com uma variação de {doc.pctChange}");
                    //Console.WriteLine("");
                }
                return Json("");
            }
        }
        public JsonResult BuscaProduto([FromBody]string produto)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Request = new HttpRequestMessage(HttpMethod.Get, $"https://www.google.com/search?tbm=shop&q={produto}&ved=2ahUKEwip-fD8nbX2AhXvOLkGHWsNANkQ3IcGegQIAhAA");
                var response = client.SendAsync(Request).Result.Content.ReadAsStringAsync();

                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(Convert.ToString(response.Result));
                var Linha = html.DocumentNode.SelectNodes(".//span[@class='HRLxBb']")[0].InnerText;
                var _produto = html.DocumentNode.SelectNodes(".//div[@class='rgHvZc']")[0].InnerText;
                var LinhaLoja = html.DocumentNode.SelectNodes(".//div[@class='dD8iuc']")[0];

                string strLoja = LinhaLoja.InnerHtml.ToString();
                string[] separatingStrings = { "</span>" };
                string[] strings = strLoja.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                ProdutoModel _result = new ProdutoModel()
                {
                    Valor = Linha,
                    Produto = _produto,
                    Loja = strings[1].Substring(3)
                };

                return Json(_result);
            }
        
        }
        #region Default Mvc
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}