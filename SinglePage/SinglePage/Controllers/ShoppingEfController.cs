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
    public class ShoppingEfController : Controller
    {
        private readonly SinglePageContext _context;

        public ShoppingEfController(SinglePageContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ShoppingList result = _context.ShoppingList.Include(s=>s.items).FirstOrDefault(x => x.Id == id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public ShoppingList Post([FromBody] ShoppingList newList)
        {
            _context.ShoppingList.Add(newList);
            _context.SaveChanges();
            return newList;
        }
    }
}
