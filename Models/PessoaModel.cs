using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string nmNome { get; set; }
        public string nmCPF { get; set; }
        public DateTime dtNascimento { get; set; }
        public bool flAtivo { get; set; }
       public List<Telefone> Telefones { get; set; }
    }

    public class Telefone
    {
        [Key]
        public int idTelefone { get; set; }
        public string flTipo { get; set; }
        public string nmNumero { get; set;
        }
        [ForeignKey("Pessoa")]
        public int idPessoa { get; set; }
        public Pessoa Pessoa { get; set; }

    }

}