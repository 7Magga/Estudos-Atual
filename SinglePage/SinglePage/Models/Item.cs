namespace SinglePage.Models
{
    public class Item
    {
        public int Id{ get; set; }
        public string name { get; set; }
        public bool Checked { get; set; }
        public int shoppingListId{ get; set; }

        public Item()
        {
            Id = 0;
            name = String.Empty;
            Checked = false;
            shoppingListId = -1;
        }
    }
}
