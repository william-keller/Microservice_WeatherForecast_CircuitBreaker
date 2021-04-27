using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RIEG.Compras.Caderno.Data;
using RIEG.Compras.Caderno.Pages.Models;

namespace RIEG.Compras.Caderno.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly RIEG.Compras.Caderno.Data.ApplicationDbContext _context;

        public IndexModel(RIEG.Compras.Caderno.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
