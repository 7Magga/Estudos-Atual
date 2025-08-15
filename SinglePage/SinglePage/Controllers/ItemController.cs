using Microsoft.AspNetCore.Mvc;
using SinglePage.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SinglePage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemController>
        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            ShoppingList _shoppingList = ShoppingController.shoppingList.FirstOrDefault(x => x.Id == item.shoppingListId);
            if (_shoppingList is null) return NotFound();
            if (_shoppingList.items.Count() == 0) 
                item.Id = 1;
            else
                item.Id = _shoppingList.items.Max(i=>i.Id) + 1;
            _shoppingList.items.Add(item);
            return Ok(_shoppingList);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Item item)
        {
            ShoppingList _shoppingList = ShoppingController.shoppingList.FirstOrDefault(x => x.Id == item.shoppingListId);
            if (_shoppingList is null) return NotFound();

            Item changeItem = _shoppingList.items.Where(i => i.Id == item.Id).FirstOrDefault();
            if(changeItem is null) return NotFound();

            changeItem.Checked = item.Checked;
            return Ok(_shoppingList);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ShoppingList _shoppingList = ShoppingController.shoppingList[0];
            if (_shoppingList is null) return NotFound();

            Item item = _shoppingList.items.FirstOrDefault(x => x.Id == id);

            var reslut = _shoppingList.items.Remove(item);
            return Ok(_shoppingList);
        }
    }
}
