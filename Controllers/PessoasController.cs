using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await db.Pessoa.Include(p => p.Telefones).ToListAsync());
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpGet("{id}")]
        [Route("Details")]
        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await db.Pessoa
                .FirstOrDefaultAsync(m => m.idPessoa == id);
            
            return View(pessoa);
        }




        // POST: Pessoas/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("idPessoa,nmNome,nmCPF,dtNascimento,flAtivo, Telefones")] Pessoa pessoa)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.Add(pessoa);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return View("Index");
        }

        // GET: Pessoas/Edit/5
        [HttpGet("{id}")]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var pessoa = await db.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoa,nmNome,nmCPF,dtNascimento,flAtivo")] Pessoa pessoa)
        {
            if (id != pessoa.idPessoa)
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
                    if (!PessoaExists(pessoa.idPessoa))
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
        [HttpDelete("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await db.Pessoa
                .FirstOrDefaultAsync(m => m.idPessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }   

        // POST: Pessoas/Delete/5
        [HttpPost]
        [Route("Delete")]
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
          return (db.Pessoa?.Any(e => e.idPessoa == id)).GetValueOrDefault();
        }
    }
}
