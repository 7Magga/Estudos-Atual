using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinglePage.Models;

namespace SinglePage.Data
{
    public class SinglePageContext : DbContext
    {
        public SinglePageContext (DbContextOptions<SinglePageContext> options)
            : base(options)
        {
        }

        public DbSet<Item>? Item { get; set; }

        public DbSet<ShoppingList>? ShoppingList { get; set; }
    }
}
