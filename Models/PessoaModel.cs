using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace desafioLar.Models
{
    public class Pessoa
    {

        public Pessoa()
        {
            Telefones = new List<Telefone>();
        }

        public Pessoa(string nmNome, string nmCPF, DateTime dtNascimento, bool flAtivo, List<Telefone> telefone )
        {
            this.idPessoa = 0;
            this.nmNome = nmNome;
            this.nmCPF = nmCPF;
            this.dtNascimento = dtNascimento;
            this.flAtivo = flAtivo;
            this.Telefones = telefone;
        }

        [Key]
        public int idPessoa { get; set; }

        [Required]
        public string nmNome { get; set; }

        [Required]
        public string nmCPF { get; set; }

        public DateTime dtNascimento { get; set; }
        public bool flAtivo { get; set; }

        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
    }

    public class Telefone
    {
        [Key]
        public int idTelefone { get; set; }
        
        [Required]
        public string flTipo { get; set; }
        
        [Required]
        public string nmNumero { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int idPessoa { get; set; }

        [JsonIgnore]
        public Pessoa Pessoa { get; set; }

    }

}