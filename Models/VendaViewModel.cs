using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class VendaViewModel
    {
        public int? Codigo { get; set; }
        
        [Required(ErrorMessage = "O Preenchimento do Campo é obrigatório.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "O Preenchimento do Campo é obrigatório.")]
        public int? CodigoCliente { get; set; }      
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }
        public string JsonProdutos { get; set; }
        public decimal Total { get; set; }            
    }
}
