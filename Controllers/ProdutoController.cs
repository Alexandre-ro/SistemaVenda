using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        protected ApplicationDbContext Context;

        public ProdutoController(ApplicationDbContext context) 
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> lista = Context.Produto.Include(x => x.Categoria).ToList();
            Context.Dispose();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id) 
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.ListaCategorias  = ListaCategorias();
            if (id != null) 
            {
                var entidade             = Context.Produto.Where(x => x.codigo == id).FirstOrDefault();
                produto.codigo           = entidade.codigo;
                produto.codigo_categoria = entidade.codigo_categoria;
                produto.descricao        = entidade.descricao;
                produto.valor            = entidade.valor;
                produto.quantidade       = entidade.quantidade;
            }

            return View(produto);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade) 
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto()
                {
                    codigo           = entidade.codigo,
                    descricao        = entidade.descricao,
                    codigo_categoria = (int)entidade.codigo_categoria,
                    valor            = (decimal)entidade.valor,
                    quantidade       = entidade.quantidade
                };

                if (entidade.codigo == null)
                {
                    Context.Produto.Add(produto);
                }else { 
                    Context.Entry(produto).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }else {
                entidade.ListaCategorias = ListaCategorias();
                return View(entidade);
            }

            return RedirectToAction("Index");            
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var entidade = new Produto { codigo = id };
            Context.Attach(entidade);
            Context.Remove(entidade);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> ListaCategorias() 
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem()
            {  
                Value = string.Empty,
                Text = string.Empty
            });

            foreach (var item in Context.Categoria.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.codigo.ToString(),
                    Text  = item.descricao.ToString() 
                });
            }

            return lista;            
        }
    }
}
