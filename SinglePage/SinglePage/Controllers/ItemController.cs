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
            _shoppingList.items.Add(item);
            return Ok(_shoppingList);
        }
    }
}
