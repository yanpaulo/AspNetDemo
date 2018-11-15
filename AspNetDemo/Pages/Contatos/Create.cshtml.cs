using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetDemo.Data;

namespace AspNetDemo.Pages.Contatos
{
    public class CreateModel : PageModel
    {
        private readonly AspNetDemo.Data.ApplicationDbContext _context;

        public CreateModel(AspNetDemo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contato Contato { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contatos.Add(Contato);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}