using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using desafioLar.Data;
using desafioLar.Models;
using System.Net;
using NuGet.Protocol;
using System.Collections;

namespace desafioLar.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasApiController : Controller
    {
        private readonly ApplicationDbContext db;

        public PessoasApiController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/pessoas
        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> Index()
        {
            return db.Pessoa != null ?
                        (await db.Pessoa.ToListAsync()).ToList() :
                        NotFound();
        }

        // GET: api/pessoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoaById(int id)
        {
            Pessoa? people =  db.Pessoa != null && id != 0 ?
                            (await db.Pessoa.FindAsync(id)) :
                            null;

            return people;
        }


        [HttpPost]
        public ActionResult<Pessoa> CreatePessoa(Pessoa pessoa)
        {
            try
            {

                db.Add(pessoa);
                db.SaveChanges();

                return CreatedAtAction(nameof(GetPessoaById), new { id = pessoa.idPessoa }, pessoa);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna um código de erro 400 BadRequest com uma mensagem
                return BadRequest($"Erro ao criar pessoa: {ex.Message}");
            }
        }


        [HttpPut]
        public async Task<ActionResult> UpdatePessoa(Pessoa pessoa)
        {
            
            try
            {
                db.Entry(pessoa).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPessoaById), new { id = pessoa.idPessoa }, pessoa);

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
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePessoa(int id)
        {
            var pessoa = await db.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            db.Pessoa.Remove(pessoa);
            await db.SaveChangesAsync();

            return Ok("Pessoa removida com sucesso.");
        }


        private bool PessoaExists(int id)
        {
            return db.Pessoa.Any(e => e.idPessoa == id);
        }
    }
}


