using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class Produto
    {
        [Key]
        public int? codigo { get; set; }
        public string descricao  { get; set; }
        public int quantidade { get; set; }
        public decimal valor { get; set; }
        [ForeignKey("Categoria")]
        public int codigo_categoria { get; set; }
        public Categoria Categoria {get; set; }
        public ICollection<VendaProdutos> Vendas { get; set; }
    }
}
