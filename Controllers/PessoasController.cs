using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using desafioLar.Data;
using desafioLar.Models;

namespace desafioLar.Controllers
{
    [Controller]
    [Route("Pessoas")]
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext db;

        public PessoasController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
              return db.Pessoa != null ? 
                          View(await db.Pessoa.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pessoa'  is null.");
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await db.Pessoa
                .FirstOrDefaultAsync(m => m.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPessoa,nmNome,nmCPF,dtNascimento,flAtivo")] Pessoa pessoa)
        {

            //Telefone tl = new Telefone();
            //tl.nmNumero = "123456789";
            //tl.Tipo = "1";
            //pessoa.Telefones.Add(tl);

            if (ModelState.IsValid)
            {
                try
                {
                    db.Add(pessoa);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await db.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoa,nmNome,nmCPF,dtNascimento,flAtivo")] Pessoa pessoa)
        {
            if (id != pessoa.IdPessoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(pessoa);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.IdPessoa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await db.Pessoa
                .FirstOrDefaultAsync(m => m.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Pessoa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pessoa'  is null.");
            }
            var pessoa = await db.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                db.Pessoa.Remove(pessoa);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
          return (db.Pessoa?.Any(e => e.IdPessoa == id)).GetValueOrDefault();
        }
    }
}
