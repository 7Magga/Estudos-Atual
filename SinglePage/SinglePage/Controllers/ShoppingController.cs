using Microsoft.AspNetCore.Mvc;
using SinglePage.Models;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SinglePage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {

        public static List<ShoppingList> shoppingList = new List<ShoppingList>()
        {
            new ShoppingList() { Id = 1, name = "Extra" , items={
                new Item() { name = "Melancia"},
                new Item() { name = "Alface"}
                } },
            new ShoppingList() { Id = 2, name = "Makro",items={ 
                new Item(){ name = "Alface"},
                new Item(){ name = "Melancia"}
                } }
        };

        // GET api/<ShoppingController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ShoppingList result = shoppingList.FirstOrDefault(x => x.Id == id);
            return result is not null ? Ok(result) : NotFound();
        }

        // POST api/<ShoppingController>
        [HttpPost]
        public IEnumerable Post([FromBody]ShoppingList newList)
        {
            newList.Id = shoppingList.Count;
            shoppingList.Add(newList);
            return shoppingList;
        }
    }
}
