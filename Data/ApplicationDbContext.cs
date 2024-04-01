using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using desafioLar.Models;
using Microsoft.AspNetCore.Mvc;

namespace desafioLar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<desafioLar.Models.Pessoa>? Pessoa { get; set; }
        public DbSet<desafioLar.Models.Telefone>? Telefone { get; set; }

        internal ActionResult<Pessoa> Find(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}