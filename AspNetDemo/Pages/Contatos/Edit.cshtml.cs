using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetDemo.Data;

namespace AspNetDemo.Pages.Contatos
{
    public class EditModel : PageModel
    {
        private readonly AspNetDemo.Data.ApplicationDbContext _context;

        public EditModel(AspNetDemo.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoExists(Contato.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContatoExists(int id)
        {
            return _context.Contatos.Any(e => e.Id == id);
        }
    }
}
