using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetDemo.Data;
using Microsoft.AspNetCore.Authorization;

namespace AspNetDemo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contatos
        [HttpGet]
        public IEnumerable<Contato> GetContatos()
        {
            return _context.Contatos;
        }

        // GET: api/Contatos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contato = await _context.Contatos.FindAsync(id);

            if (contato == null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        // PUT: api/Contatos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato([FromRoute] int id, [FromBody] Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contato.Id)
            {
                return BadRequest();
            }

            _context.Entry(contato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contatos
        [HttpPost]
        public async Task<IActionResult> PostContato([FromBody] Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = contato.Id }, contato);
        }

        // DELETE: api/Contatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return Ok(contato);
        }

        private bool ContatoExists(int id)
        {
            return _context.Contatos.Any(e => e.Id == id);
        }
    }
}