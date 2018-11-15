using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetDemo.Data
{
    public class Contato
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        [Required]
        public string Telefone { get; set; }

        public string Observacoes { get; set; }
    }
}
