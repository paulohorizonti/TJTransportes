using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TJTransportesApp.Models;

namespace TJTransportesApp.Controllers
{
    public class HomeController : Controller
    {
        readonly TjDbContext db;

        Usuario user;

        public HomeController()
        {
            db = new TjDbContext();
        }

        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login");
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //se naou houver sessao ele direciona para essa action
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Login, Senha")] Usuario usuario)
        {


            try
            {
                this.user = db.Usuarios.Where(x => x.Login == usuario.Login).FirstOrDefault();
            }
            catch (Exception e)
            {
                var erro = e.ToString();
                int par = 7;
                return RedirectToAction("../Erro/ErroLogin", new { param = par });
            }


            if (user == null)
            {
                Session["usuario"] = null;
                ViewBag.Message = "Usuário não encontrato!";
                return View();
            }
            else
            {
                if (usuario.Senha == null)
                {
                    Session["usuario"] = null;
                    ViewBag.Message = "Por favor digite sua senha";
                    return View();
                }


                if (user.Senha.Equals(usuario.Senha))
                {

                    ViewBag.Message = "Bem vindo : " + user.Nome;

                    Session["usuario"] = user.Nome;
                    Session["empFantasia"] = user.Empresa.Fantasia;
                    Session["idEmpresa"] = user.Empresa.Id;
                    
                    Session["login"] = user.Login;
                    Session["id"] = user.Id;
                    Session["perfil"] = user.Perfil.Descricao;
                    Session["perfilId"] = user.IdPerfil;
                    Session["dep"] = user.Departamento.Nome;

                   
                  //fazer ele puxar as tabelas do banco ao inves de estar fixo  
                  string[]  tabelas = { "PERFIL", "ACESSO", "DEPARTAMENTO", "EMPRESA", "USUARIO" };

                    Session["tabelas"] = tabelas;

                    string usuarioSessao = Session["usuario"].ToString(); //pega o usuário da sessão
                   
                    //this.usuario = (from a in db.Usuarios where a.nome == usuarioSessao select a).FirstOrDefault(); //pega o usuario


                    try
                    {
                        this.user = db.Usuarios.Where(x => x.Nome == usuarioSessao).FirstOrDefault();
                    }
                    catch (Exception e)
                    {
                        var erro = e.ToString();
                        int par = 7;
                        return RedirectToAction("../Erro/ErroLogin", new { param = par });
                    }




                    //this.empresa = (from a in db.Empresas where a.cnpj == empresaUsuario select a).FirstOrDefault(); //empresa





                    Session["usuarios"] = usuario;




                }
                else
                {
                    Session["usuario"] = null;
                    ViewBag.Message = "Senha incorreta!";
                    return View();
                }


            }

            return RedirectToAction("Index", "Home");
        }
        

        public ActionResult LogOut()
        {
            if (Session["usuario"] != null)
            {
                Session["usuario"] = null;
               
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}