using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : Controller
    {
        [HttpPost]
        [Route("Correio")]
        public JsonResult Correio([FromBody] string cep)
        {
            cep = cep.Replace("-", "");
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

                        json.dados[0].uf = json.dados[0].uf is "" ? "Informação não encontrada" : json.dados[0].uf;
                        json.dados[0].localidade = json.dados[0].localidade is "" ? "Informação não encontrada" : json.dados[0].localidade;
                        json.dados[0].logradouroDNEC = json.dados[0].logradouroDNEC is "" ? "Informação não encontrada" : json.dados[0].logradouroDNEC;
                        json.dados[0].bairro = json.dados[0].bairro is "" ? "Informação não encontrada" : json.dados[0].bairro;
                        json.dados[0].tipoCep = json.dados[0].tipoCep is "" ? "Informação não encontrada" : json.dados[0].tipoCep;
                        _cep = cep;

                        return Json(json.dados[0]);

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
        [HttpGet]
        [Route("Cotacoes")]
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
                }
                return Json("");
            }
        }
        [HttpPost]
        [Route("BuscaProduto")]
        public JsonResult BuscaProduto([FromBody] string produto)
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();
            
            using (var client = new HttpClient())
            {
                #region Google
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

                produtos.Add(_result);
                #endregion

                #region ZOOM
                var RequestZoom = new HttpRequestMessage(HttpMethod.Get, $"https://www.zoom.com.br/search?q={produto.Replace(" ","%20")}");
                var responseZoom = client.SendAsync(RequestZoom).Result.Content.ReadAsStringAsync();

                html.LoadHtml(Convert.ToString(responseZoom.Result));
           
                #endregion
                return Json(_result);
            }

        }
    }
}
