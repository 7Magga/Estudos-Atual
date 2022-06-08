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

        public static List<ShoppingList> shoppingList = new List<ShoppingList>();
       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ShoppingList result = shoppingList.FirstOrDefault(x => x.Id == id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public ShoppingList Post([FromBody]ShoppingList newList)
        {
            newList.Id = shoppingList.Count;
            shoppingList.Add(newList);
            return newList;
        }
    }
}
