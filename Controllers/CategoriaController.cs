using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext Context;

        public CategoriaController(ApplicationDbContext context) 
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            //Lista com as Categorias
            IEnumerable<Categoria> lista = Context.Categoria.ToList();
            Context.Dispose();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id) 
        {
            CategoriaViewModel categoria = new CategoriaViewModel();            

            if (id != null) 
            {
               var entidade        = Context.Categoria.Where(x => x.codigo == id).FirstOrDefault();
               categoria.codigo    = entidade.codigo;
               categoria.descricao = entidade.descricao;
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade) 
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria()
                {
                  codigo    = entidade.codigo,
                  descricao = entidade.descricao
                };

                if (entidade.codigo == null)
                {
                    Context.Categoria.Add(categoria);
                }
                else {
                    Context.Entry(categoria).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            else {  
                return View(entidade);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id) 
        {
            var entidade = new Categoria() { codigo = id };
            Context.Attach(entidade);
            Context.Remove(entidade);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
