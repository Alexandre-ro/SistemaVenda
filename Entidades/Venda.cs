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
        [ForeignKey("cliente")]
        public int codigo_cliente { get; set; }
        [ForeignKey("usuario")]
        public int codigo_usuario { get; set; }
        public DateTime data_venda { get; set; }
        public Decimal total { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<VendaProdutos> Produtos { get; set; }
    }
}
