using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        // lista.......

        public IActionResult listaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());

        }

        // Inserção.....

         public IActionResult RegistarUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();

        }
        [HttpPost]

         public IActionResult RegistarUsuario(Usuario novoUsuario)
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

        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

        [HttpPost]

         public IActionResult editarUsuario(Usuario userEditado)
        {
            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }

        // Exclusão ....

        public IActionResult excluirUsuario(string decisao,int id)
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