using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class ClienteViewModel
    {
        public int? codigo { get; set; }
        [Required(ErrorMessage ="O preenchimento do campo é obrigatório.")]
        public string nome { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do campo é obrigatório.")]
        public string cpf { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do campo é obrigatório.")]
        public string celular { get; set; }      
    }
}
