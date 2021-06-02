using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class VendaProdutos
    {
        public int codigo_venda { get; set; }
        public int codigo_produto { get; set; }
        public int quantidade { get; set; }
        public decimal valor_unitario { get; set; }       
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }
    }
}
