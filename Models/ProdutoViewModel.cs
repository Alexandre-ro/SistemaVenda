using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class ProdutoViewModel
    {
        public int? codigo { get; set; }
       
        [Required(ErrorMessage ="O preenchimento do campo é obrigatório.")]
        public string descricao { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do campo é obrigatório.")]
        public int quantidade { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do campo é obrigatório.")]
        [Range(0.1, Double.PositiveInfinity)]
        public decimal? valor { get; set; }
        
        [Required(ErrorMessage = "O preenchimento do campo é obrigatório.")]
        public int? codigo_categoria { get; set; }           
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
