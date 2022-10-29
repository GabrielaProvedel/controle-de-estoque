using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace controle_de_estoque.Model
{
    public class Item
    {
        public int Id { get; set; }
        public Produto produto { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
    }

    [Table("Venda")]
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public double Total { get; set; } = 0;

        public List<Item> carrinho { get; set; }
    }
}