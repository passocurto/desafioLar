using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using desafioLar.Data;
using desafioLar.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace desafioLar.Controllers.Api
{
    [ApiController]
    [Route("api/PessoasApi")]
    public class PessoasApiController : Controller
    {
        private readonly ApplicationDbContext db;

        public PessoasApiController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/pessoas
        [Route("Index")]
        public async Task<ActionResult<string>> Index()
        {
            try
            {

                ICollection<Pessoa> ListPessoa = await db.Pessoa
                                                   .Include(p => p.Telefones)
                                                   .ToListAsync();

                return new JsonResult(ListPessoa);

            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erro GetPessoaById: {ex.Message}"));
            }

        }

        // GET: api/pessoas/5
        [HttpGet("GetPessoaById/{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoaById(int id)
        {
            try
            {
                Pessoa? person = await db.Pessoa
                                    .Include(p => p.Telefones.Select(t => new { t.idPessoa, t.idTelefone, t.nmNumero, t.flTipo }))
                                    .Where(p => p.idPessoa == id)
                                    .FirstOrDefaultAsync();

                return new JsonResult(person);
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erro GetPessoaById: {ex.Message}"));
            }
        }

        [HttpPost("Pessoa")]
        [Route("Create")]
        public IActionResult CreatePessoa([FromBody] dynamic jsonData)
        {
            try
            {

                var person = JsonSerializer.Deserialize<Pessoa>(jsonData);
                db.Add(person);
                db.SaveChanges();

               
                return new JsonResult(CreatedAtAction(nameof(GetPessoaById), new { id = person.idPessoa }, person));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest($"Erro ao criar pessoa: {ex.Message}"));
            }
        }


        
        [HttpPut("Pessoa")]
        [Route("UpdatePessoa")]
        public async Task<ActionResult> UpdatePessoa([FromBody] dynamic jsonData)
        {
            try
            {
                Pessoa person = JsonSerializer.Deserialize<Pessoa>(jsonData);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult(CreatedAtAction(nameof(GetPessoaById), new { id = person.idPessoa }, person));

            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult(BadRequest("Erro ao Atualizar a Pessoa:"));
            }
        }

        [HttpDelete("DeletePessoa/{id}")]
        public async Task<ActionResult> DeletePessoa(int id)
        {
            try
            {
                var pessoa = await db.Pessoa
                                .Include(p => p.Telefones)
                                .Where(p => p.idPessoa == id)
                                .FirstOrDefaultAsync();

            if (pessoa == null)
            {
                return new JsonResult(BadRequest("Pessoa não encontrada"));
            }

            db.Pessoa.Remove(pessoa);
            await db.SaveChangesAsync();

            return new JsonResult(Ok("Pessoa removida com sucesso."));

            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult(BadRequest("Erro ao Atualizar a Pessoa:"));
            }
        }

    }
}


