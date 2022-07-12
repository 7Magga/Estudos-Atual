namespace WebCrawler.Models
{
    public class ReqMoedaModel
    {
        public object prev { get; set; }
        public object next { get; set; }
        public MoedaModel[] docs { get; set; }
    }
}
