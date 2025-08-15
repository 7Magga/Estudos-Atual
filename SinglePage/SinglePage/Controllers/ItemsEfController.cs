using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinglePage.Data;
using SinglePage.Models;

namespace SinglePage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsEfController : Controller
    {
        private readonly SinglePageContext _context;

        public ItemsEfController(SinglePageContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            ShoppingList _shoppingList = _context.ShoppingList.Include(s=>s.items).FirstOrDefault(x => x.Id == item.shoppingListId);

            if (_shoppingList is null) return NotFound();
            _shoppingList.items.Add(item);
            _context.Item.Add(item);
            _context.SaveChanges();

            return Ok(_shoppingList);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Item item)
        {
            Item changeItem = _context.Item.Where(i => i.Id == item.Id).FirstOrDefault();
            if (changeItem is null) return NotFound();
            changeItem.Checked = item.Checked;
            _context.Item.Update(changeItem);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Item _item)
        {
            ShoppingList _shoppingList = _context.ShoppingList.FirstOrDefault(x => x.Id == _item.shoppingListId);
            if (_shoppingList is null) return NotFound();

            Item item = _shoppingList.items.FirstOrDefault(x => x.Id == _item.Id);

            _shoppingList.items.Remove(item);
            _context.SaveChanges();
            
            return Ok(_shoppingList);
        }
    }
}
