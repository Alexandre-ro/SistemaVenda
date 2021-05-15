using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class Categoria
    {
        [Key]
        public int? codigo { get; set; }
        public string descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
