namespace WebCrawler.Models
{
    public class Model
    {
        public bool erro { get; set; }
        public string mensagem { get; set; }
        public int total { get; set; }
        public LograModel[] dados { get; set; }
    }
}
