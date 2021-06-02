using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected ApplicationDbContext Context;

        public RelatorioController(ApplicationDbContext context) 
        {
            this.Context = context;
        }

        public IActionResult Grafico()
        {
            string valores = string.Empty;
            string labels  = string.Empty;        

            var lista = (from r in Context.VendaProdutos
                         group  r by new { r.codigo_produto, r.Produto.descricao }
                         into g
                         select new GraficoViewModel {
                             codigo_produto = g.Key.codigo_produto,
                             descricao      = g.Key.descricao,
                             total_venda    = g.Sum(x=> x.quantidade) 
                         }).ToList();                      

            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].total_venda.ToString() + ",";
                labels  += "'"+lista[i].descricao.ToString() + "',";
            }

            ViewBag.valores = valores;
            ViewBag.labels  = labels;

            return View();
        }
    }
}
