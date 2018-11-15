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
    public class DeleteModel : PageModel
    {
        private readonly AspNetDemo.Data.ApplicationDbContext _context;

        public DeleteModel(AspNetDemo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contato Contato { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contato = await _context.Contatos.FirstOrDefaultAsync(m => m.Id == id);

            if (Contato == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contato = await _context.Contatos.FindAsync(id);

            if (Contato != null)
            {
                _context.Contatos.Remove(Contato);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
