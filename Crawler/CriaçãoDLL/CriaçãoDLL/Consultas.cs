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
    }  
}
