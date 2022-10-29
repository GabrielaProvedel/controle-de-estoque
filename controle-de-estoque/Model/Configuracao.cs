﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controle_de_estoque.Model
{
    [Table("Configuracao")]

    public class Configuracao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int quantidadeBaixa { get; set; } = 10;
    }
}
