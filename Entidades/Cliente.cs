using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class Cliente
    {
        [Key]
        public int? codigo { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }       
        public string celular { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
