using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace controle_de_estoque.Model
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public double Preco { get; set; }

        [Required]
        public int Quantidade { get; set; }

    }
}