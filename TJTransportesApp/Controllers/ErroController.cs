using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TJTransportesApp.Controllers
{
    public class ErroController : Controller
    {
        // GET: Erro
        public ActionResult Index(int param)
        {
            if(param == 7)
            {
                ViewBag.Erro = "Usuário não permitido para visualizar essa tabela";
            }
            return View();
        }
    }
}