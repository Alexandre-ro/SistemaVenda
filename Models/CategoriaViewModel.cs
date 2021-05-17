using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class CategoriaViewModel
    {
        public int? codigo { get; set; }        
        
        [Required(ErrorMessage = "O preenchimento da Descrição é obrigatório.")]
        public string descricao { get; set; }
    }
}
