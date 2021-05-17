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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext Context;

        public ClienteController(ApplicationDbContext context) 
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Cliente> lista = Context.Cliente.ToList();
            Context.Dispose();                              
            
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id) 
        {
            ClienteViewModel cliente = new ClienteViewModel();
            if (id != null) 
            {
                var entidade    = Context.Cliente.Where(x => x.codigo == id).FirstOrDefault();
                cliente.codigo  = entidade.codigo;
                cliente.nome    = entidade.nome;
                cliente.cpf     = entidade.cpf;
                cliente.celular = entidade.celular;
            }

            return View(cliente);        
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade) 
        {
            if (ModelState.IsValid) 
            {
                Cliente cliente = new Cliente()
                {
                    codigo  = entidade.codigo,
                    nome    = entidade.nome,
                    cpf     = entidade.cpf,
                    celular = entidade.celular
                };

                if (entidade.codigo == null) 
                {
                    Context.Cliente.Add(cliente);
                }else{
                    Context.Entry(cliente).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }else{
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id) 
        {
            var entidade = new Cliente()
            {
                codigo = id
            };

            Context.Attach(entidade);
            Context.Remove(entidade);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
