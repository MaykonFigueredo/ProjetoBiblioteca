using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        // lista.......

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());

        }

        // Inserção.....

         public IActionResult RegistarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();

        }
        [HttpPost]

         public IActionResult RegistarUsuarios(Usuario novoUsuario)
        {
            novoUsuario.Senha = Criptografo.TextoCriptografado(novoUsuario.Senha);
            new UsuarioService().IncluirUsuario(novoUsuario);
            return RedirectToAction("CadastroRealizado");

        }

         public IActionResult CadastroRealizado()
         {
             return View();
         }




        //Edição......

        public IActionResult EditarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

        [HttpPost]

         public IActionResult EditarUsuario(Usuario userEditado)
        {
            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }

        // Exclusão ....

        public IActionResult ExcluirUsuario(string decisao,int id)
        {
            if(decisao=="Excluir")
            {
                ViewData["Mensagem"] = "Exclusão do usuario"+ new UsuarioService().Listar(id).Nome + "realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"]= "Exclusão Cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }

        // Funções Estras ...... 
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
    }
}