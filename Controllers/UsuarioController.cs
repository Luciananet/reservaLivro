using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuariosController userEditado;

        public IActionResult ListaDeUsuarios()
        {
       Autenticacao.CheckLogin(this);
       Autenticacao.verificaSeUsuarioEAdmim(this);
        return View(new UsuarioService().Listar());
        }
        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);

            return View (u);
        }
        [HttpPost]          
        public IActionResult editarUsuario( Usuario userEditado)
        {
                UsuarioService us = new UsuarioService();
                us.editarUsuario(userEditado);
                return RedirectToAction("ListaDeUsuarios");
        }
        public  IActionResult RegistrarUsuarios ()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmim(this);
            return View();

         }
         [HttpPost]

         public IActionResult RegistrarUsuarios(Usuario novoUser)
         {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmim(this);

            novoUser.senha = Criptografo.TextoCriptografado(novoUser.senha);

            UsuarioService us = new UsuarioService(); 
            
            us.incluirUsuario(novoUser);
             return RedirectToAction("Cadastro realizado");

         }
        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }
        [HttpPost]
        public IActionResult ExcluirUsuario (string decisao,int id)
        {
         if(decisao == "EXCLUIR")
         {
           ViewData["Mensagem"] = "Exclusão do usuario" + new UsuarioService().Listar(id).Nome+"Realizada com sucesso";
           new UsuarioService().excluirUsuario(id);
           return View("ListaDeUsuarios" ,new UsuarioService().Listar());
         }
         else
         {

            ViewData["Mensagem"] ="Exclusão cancelada";
            return View("ListaDeUsuarios",new UsuarioService().Listar());
         }



        }
        public IActionResult cadastroRealizado()
        {
         Autenticacao.CheckLogin(this);
         Autenticacao.verificaSeUsuarioEAdmim(this);
         return View();

        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
}

}
