using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using desafioLar.Data;
using desafioLar.Models;
using System;

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

        public async Task<ActionResult<Pessoa>> GetPessoaById(int id)
        {
            try
            {

                Pessoa? person = await db.Pessoa
                            .Include(p => p.Telefones)
                            .Where(p => p.idPessoa == id)
                            .FirstOrDefaultAsync();

                return person;

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro GetPessoaById: {ex.Message}");
            }
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
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null || db.Pessoa == null)
                {
                    return NotFound();
                }

                var pessoa = await db.Pessoa
                            .Include(p => p.Telefones)
                            .Where(p => p.idPessoa == id)
                            .FirstOrDefaultAsync();


                return View(pessoa);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro GetPessoaById: {ex.Message}");
            }
        }

        // POST: Pessoas/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("idPessoa,nmNome,nmCPF,dtNascimento,flAtivo, Telefones")] Pessoa pessoa)
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

        // GET: Pessoas/Edit/5
        [HttpGet("{id}")]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var pessoa = await db.Pessoa
                                .Include(p => p.Telefones)
                                .Where(p => p.idPessoa == id)
                                .FirstOrDefaultAsync();

                if (pessoa == null)
                {
                    return NotFound();
                }

                return View(pessoa);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        // POST: Pessoas/Edit/5
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("IdPessoa,nmNome,nmCPF,dtNascimento,flAtivo,Telefones")] Pessoa pessoa)
        {
            try
            {
                db.Update(pessoa);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex) 
            {
                    throw (ex);
            }
        }

        // GET: Pessoas/Delete/5
        [HttpDelete("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                var pessoa  = await db.Pessoa
                            .Include(p => p.Telefones)
                            .Where(p => p.idPessoa == id)
                            .FirstOrDefaultAsync(); ;

                if (pessoa == null)
                {
                    return NotFound();
                }

                return View(pessoa);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // POST: Pessoas/Delete/5
        [HttpPost("{id}")]
        [Route("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int idPessoa)
        {
            try
            {
                if (db.Pessoa == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Pessoa'  is null.");
                }

                var person = await db.Pessoa.FindAsync(idPessoa);

                if (person != null)
                {
                    db.Pessoa.Remove(person);
                }
            
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
