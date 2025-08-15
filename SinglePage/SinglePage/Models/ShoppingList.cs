namespace SinglePage.Models
{
    public class ShoppingList
    {
        public int Id{ get; set; }
        public string name{ get; set; }
        public List<Item> items{ get; set; }

        public ShoppingList()
        {
            Id = 0;
            name = string.Empty;
            items = new List<Item>();
        }
    }
}
