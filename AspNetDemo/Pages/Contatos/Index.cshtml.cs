using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetDemo.Data;

namespace AspNetDemo.Pages.Contatos
{
    public class IndexModel : PageModel
    {
        private readonly AspNetDemo.Data.ApplicationDbContext _context;

        public IndexModel(AspNetDemo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Contato> Contato { get;set; }

        public async Task OnGetAsync()
        {
            Contato = await _context.Contatos.ToListAsync();
        }
    }
}
