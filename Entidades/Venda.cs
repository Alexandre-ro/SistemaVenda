using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class Venda
    {
        [Key]
        public int? codigo { get; set; }
        [ForeignKey("Cliente")]
        public int codigo_cliente { get; set; }    
        public DateTime data_venda { get; set; }
        public decimal total { get; set; }
        public Cliente Cliente { get; set; }
       
        public ICollection<VendaProdutos> Produtos { get; set; }
    }
}
