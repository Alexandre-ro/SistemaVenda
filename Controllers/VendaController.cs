using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext Context;
        public VendaController(ApplicationDbContext context) 
        {
            this.Context = context;        
        }

        public IActionResult Index()
        {
            IEnumerable<Venda> lista = Context.Venda.ToList();
            //IEnumerable<Venda> lista = Context.Venda.Include(x => x.Cliente).ToList();
            Context.Dispose();

            return View(lista);
        }

        public IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text  = string.Empty
            });

            foreach (var item in Context.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.codigo.ToString(),
                    Text  = item.descricao.ToString()
                });
            }

            return lista;
        }

        private IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text  = string.Empty
            });

            foreach (var item in Context.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.codigo.ToString(),
                    Text  = item.nome.ToString()
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id) 
        {
            VendaViewModel model = new VendaViewModel();
            model.ListaClientes  = ListaClientes();
            model.ListaProdutos  = ListaProdutos();

            if (id != null) 
            {
                var entidade        = Context.Venda.Where(x => x.codigo == id).FirstOrDefault();
                model.Codigo        = entidade.codigo;
                model.Data          = entidade.data_venda;
                model.CodigoCliente = entidade.codigo_cliente;
                model.Total         = entidade.total;
            }

            return View(model);        
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade) 
        {
            if (ModelState.IsValid) 
            {
                Venda obj = new Venda()
                {
                   codigo         = entidade.Codigo,
                   codigo_cliente = (int)entidade.CodigoCliente,
                   data_venda     = (DateTime)entidade.Data,                   
                   total          = entidade.Total,
                   Produtos       = JsonConvert.DeserializeObject<ICollection<VendaProdutos>> (entidade.JsonProdutos)
                };

                if (entidade.Codigo == null)
                {
                    Context.Venda.Add(obj);
                }
                else {
                    Context.Entry(obj).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }else{
                entidade.ListaClientes = ListaClientes();
                entidade.ListaProdutos = ListaProdutos();
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entidade = new Venda{ codigo = id };
            Context.Attach(entidade);
            Context.Remove(entidade);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto) 
        {
            return Context.Produto.Where(x => x.codigo == CodigoProduto)
                                  .Select(x => x.valor).FirstOrDefault();        
        }

    }
}
