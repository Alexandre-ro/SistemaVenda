using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System;
using System.Linq;


namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        public ApplicationDbContext Context;
        public IHttpContextAccessor HttpAcessor;

        public LoginController(ApplicationDbContext context, IHttpContextAccessor httpAcessor) 
        {
           Context      = context;
           HttpAcessor  = httpAcessor;
        }

        public IActionResult Login(int? id)
        {
            if (id !=null) 
            {
                if (id == 0) 
                {
                    HttpAcessor.HttpContext.Session.Clear();
                }
            }

           return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model) 
        {
                       
            ViewData["ErroLogin"] = String.Empty;
            if (ModelState.IsValid)
            {
                var Senha = Criptografia.getMD5Hash(model.Senha);
                var usuario = Context.Usuario.Where(x => x.email == model.Email && x.senha == Senha).FirstOrDefault();
                if (usuario == null)
                {
                    ViewData["ErroLogin"] = "O Email ou senha informado não existe no sistema.";
                   return View(model);
                }
                else 
                {
                    HttpAcessor.HttpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, (int)usuario.codigo);
                    HttpAcessor.HttpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.nome);
                    HttpAcessor.HttpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.email);
                    HttpAcessor.HttpContext.Session.SetInt32(Sessao.LOGADO, 1);

                    return RedirectToAction("Index", "Home");
                }               
            }
            else 
            {
                return View(model);
            }                            
         
        }
    }
}
