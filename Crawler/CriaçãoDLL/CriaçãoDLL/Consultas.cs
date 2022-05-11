using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ConsultaDLL
{
    public class model
    {
        public bool erro { get; set; }
        public string mensagem { get; set; }
        public int total { get; set; }
        public logra[] dados { get; set; }
    }

    public class logra
    {
        public string uf { get; set; }
        public string localidade { get; set; }
        public string logradouroDNEC { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string tipoCep { get; set; }
    }

    public class Moeda
    {
        public string name { get; set; }
        public double openbidvalue { get; set; }
        public double askvalue { get; set; }
        public double variationpercentbid { get; set; }
        public string date { get; set; }
        public string abbreviation { get; set; }
        public double pctChange { get; set; }
        public double open { get; set; }
        public string exchangeasset { get; set; }
        public double price { get; set; }
    }

    public class ReqMoeda
    {
        public object prev { get; set; }
        public object next { get; set; }
        public Moeda[] docs { get; set; }
    }

    public class Produto
    {
        public string produto { get; set; }
        public string valor { get; set; }
        public string loja { get; set; }
    }

    public class Consultas
    {
        public dynamic BuscaCep(string CEP,string tipoRetorno)
        {
            #region Construtores
            string _uf = "";
            string _localidade = "";
            string _rua = "";
            string _bairro = "";
            string _cep = "";
            string _tipo = "";
            #endregion
            string cep = CEP.Replace("-", "");
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
                var json = JsonSerializer.Deserialize<model>(Result.Result);
                if (json.erro == false)
                {
                    if (json.total == 1)
                    {
                        _uf = json.dados[0].uf;
                        _localidade = json.dados[0].localidade;
                        _rua = json.dados[0].logradouroDNEC;
                        _bairro = json.dados[0].bairro;
                        _cep = json.dados[0].cep;
                        _tipo = json.dados[0].tipoCep;
                        switch (tipoRetorno)
                        {
                            case "json": 
                                return json.dados[0];
                            case "string":
                                return $"A rua é {_rua}, o bairro é {_bairro} a cidade é {_localidade} e o Estado é {_uf}";
                        }
                    }
                    else if (json.total != 0)
                    {
                        return "Mais de um resultado";
                    }
                    else if (json.total == 0)
                    {
                        return "Insira um CEP Valido";
                    }
                }
                else
                {
                    return "Não foi possivel localizar devido a um erro";
                }
            }
            return "Não foi possivel localizar devido a um erro";
        }

        public dynamic Cotacao()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Request = new HttpRequestMessage(HttpMethod.Get, "https://api.cotacoes.uol.com/mixed/summary?&currencies=1,11,5&itens=1,23243,1168&fields=name,openbidvalue,askvalue,variationpercentbid,price,exchangeasset,open,pctChange,date,abbreviation&jsonp=jsonp");

                var Result = client.SendAsync(Request).Result.Content.ReadAsStringAsync();
                string teste = Result.Result.Replace("/**/jsonp(", "").Replace(");", "");

                var json = JsonSerializer.Deserialize<ReqMoeda>(teste);
                if (json.docs != null)
                {
                    return json;
                }
                return "Erro";
            }
        }

        public dynamic BuscaProduto(string produto, string tipoRetorno)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                var Request = new HttpRequestMessage(HttpMethod.Get, $"https://www.google.com/search?tbm=shop&q={produto}&ved=2ahUKEwip-fD8nbX2AhXvOLkGHWsNANkQ3IcGegQIAhAA");
                var response = client.SendAsync(Request).Result.Content.ReadAsStringAsync();

                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(Convert.ToString(response.Result));
                var Linha = html.DocumentNode.SelectNodes(".//span[@class='HRLxBb']")[0];
                var LinhaLoja = html.DocumentNode.SelectNodes(".//div[@class='dD8iuc']")[0];

                string strLoja = LinhaLoja.InnerHtml.ToString();
                string[] separatingStrings = { "</span>" };
                string[] strings = strLoja.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                Produto _produto = new Produto()
                {
                    produto = produto,
                    valor = Linha.InnerText,
                    loja = strings[1].Substring(3)
                };

                if (tipoRetorno is "string")
                    return $"O valor de {produto} é {Linha.InnerText} na{strings[1].Substring(3)}";
                else if (tipoRetorno is "json")
                    return JsonSerializer.Serialize(_produto);
            }
            return "erro";
        }
    }  
}
