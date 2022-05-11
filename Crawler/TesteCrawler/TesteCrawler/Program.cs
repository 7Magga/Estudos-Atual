using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using HtmlAgilityPack;
using static TesteCrawler.repository;
using System.Data.SqlClient;
using System.Data;

namespace MAGGAUtil
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

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cnn = AbrirConn();
            inicio();

            void inicio()
            {
                cnn.Close();    
                Console.WriteLine("SELECIONE O CRAWLER DESEJADO");
                Console.WriteLine("1 - Busa CEP Correio");
                Console.WriteLine("2 - Cotações");
                Console.WriteLine("3 - Busca Produto");
                Console.WriteLine("4 - Imprimir Banco de Dados");
                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Correio();
                        break;
                    case "2":
                        Cotacoes();
                        break;
                    case "3":
                        BuscaProduto();
                        break;
                    case "4":
                        Imprimir();
                        break;
                }
            }

            void Correio()
            {
                #region Construtores
                string _uf = "";
                string _localidade = "";
                string _rua = "";
                string _bairro = "";
                string _cep = "";
                string _tipo = "";
                #endregion
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Digite um cep:");
                string cep = Console.ReadLine().Replace("-", "").Trim();
                Console.WriteLine("");
                Console.WriteLine("Resultado da busca:");
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
                            _cep = cep;
                            _tipo = json.dados[0].tipoCep;

                            #region Banco de Dados
                            string command = $"INSERT INTO TB_CEP(uf,localidade,logradouroDNEC,bairro,cep,tipoCep)" +
                                $"VALUES('{_uf}','{_localidade}','{_rua}','{_bairro}','{_cep}','{_tipo}')";
                            SqlCommand cmd = new SqlCommand(command,cnn);
                            cmd.CommandType = CommandType.Text;
                            try
                            {
                                cnn.Open();
                                cmd.ExecuteNonQuery();
                                cnn.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro {e.Message}");
                            }
                            #endregion
                            Console.WriteLine($"A rua é {_rua}, o bairro é {_bairro} a cidade é {_localidade} e o Estado é {_uf}");
                        }
                        else if (json.total != 0)
                        {
                            Console.WriteLine("Mais de um resultado");
                        }
                        else if (json.total == 0)
                        {
                            Console.WriteLine("Insira um CEP Valido");
                        }
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("");
                        inicio();
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel localizar devido a um erro");
                    }
                }
            }
            void Cotacoes()
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Resultado da busca:");
                Console.WriteLine("");
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
                        foreach (var doc in json.docs)
                        {
                            if (doc.exchangeasset == null)
                            {
                                Console.WriteLine($"Resultado do(a) {doc.name}");
                                Console.WriteLine($"-Abriu em R$2{doc.openbidvalue} Fechou em R${doc.askvalue} com uma variação de {doc.variationpercentbid}");
                                Console.WriteLine("");
                                #region Banco
                                string command = $"INSERT INTO TB_COTACAO(Nome,Abertura,Fechamento,Variacao)" +
                                            $"VALUES('{doc.name}','{doc.openbidvalue}','{doc.askvalue}','{doc.variationpercentbid}')";
                                SqlCommand cmd = new SqlCommand(command, cnn);
                                cmd.CommandType = CommandType.Text;
                                try
                                {
                                    cnn.Open();
                                    cmd.ExecuteNonQuery();
                                    cnn.Close();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Erro {e.Message}");
                                }
                                #endregion
                            }
                            else
                            {
                                Console.WriteLine($"Resultado do(a) {doc.exchangeasset}");
                                Console.WriteLine($"-Abriu em R${doc.open} Fechou em R${doc.price} com uma variação de {doc.pctChange}");
                                Console.WriteLine("");
                                #region Banco
                                string command = $"INSERT INTO TB_COTACAO(Nome,Abertura,Fechamento,Variacao)" +
                                            $"VALUES('{doc.exchangeasset}','{doc.open}','{doc.price}','{doc.pctChange}')";
                                SqlCommand cmd = new SqlCommand(command, cnn);
                                cmd.CommandType = CommandType.Text;
                                try
                                {
                                    cnn.Open();
                                    cmd.ExecuteNonQuery();
                                    cnn.Close();
                                }
                                catch (Exception e)
                                {
                                    //Console.WriteLine($"Erro {e.Message}");
                                }
                                #endregion
                            }
                        }
                    }
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");
                    inicio();
                }
            }

            void BuscaProduto()
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Digite o nome do produto que deseja buscar:");
                var produto = Console.ReadLine();
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Resultado da busca:");
                Console.WriteLine("");
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
                    string[] separatingStrings = {"</span>"};
                    string[] strings = strLoja.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                    
                    Console.WriteLine($"O valor de {_produto} é {Linha} na{strings[1].Substring(3)}");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");

                    #region Banco
                    string command = $"INSERT INTO TB_PRODUTO(Nome,Valor,Loja)" +
                                $"VALUES('{_produto}','{Linha}','{strings[1].Substring(3)}')";
                    SqlCommand cmd = new SqlCommand(command, cnn);
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Erro {e.Message}");
                    }
                    #endregion
                    inicio();
                }
            }
            void Imprimir()
            {
                cnn.Open();
                
                cnn.Close();
                inicio();
            }
        }
    }
}
