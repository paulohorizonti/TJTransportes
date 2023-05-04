using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TJTransportesApp.Models;
using TJTransportesApp.Models.ViewModel;

namespace TJTransportesApp.Controllers
{
    public class PerfilController : Controller
    {
        readonly TjDbContext db;

        List<Perfil> listPerfil = new List<Perfil>();
        List<Acesso> listaAcessos = new List<Acesso>();
        List<Acesso> acess = new List<Acesso>();
        string Tabela = "PERFIL";
        Acesso acesso;
        public PerfilController() 
        {
            db = new TjDbContext();
        }
        public ActionResult Index(string param, string ordenacao, string qtdSalvos, string procuraAtivo, string procuraPerfil,
            string filtroAtivo, string filtroPerfil, int? page, int? numeroLinhas, int? inputStatus)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.PerfilUsuario = Session["perfilId"]; //PEGA O ID ODO PERFIL DO USUARIO
            }
            this.acess = db.Acessos.ToList();
            this.acess = this.acess.Where(x => x.IdPerfil == ViewBag.PerfilUsuario).ToList(); //aqui pega os acessos BASEADO NO ID


            //AQUI ELE PEGA O ACESSO DE ACORDO COM A TABELA
            ViewBag.AcessoPermitido = this.acess.Where(x => x.Tabela.Equals(this.Tabela)).ToList();
            
           
            string resultado = param;

            procuraAtivo = (filtroAtivo != null) ? filtroAtivo : procuraAtivo; //procura por nome
            procuraPerfil = (procuraPerfil != null) ? procuraPerfil : null;

            //numero de linhas
            ViewBag.NumeroLinhas = (numeroLinhas != null) ? numeroLinhas : 10;
            ViewBag.Status = (inputStatus != null) ? inputStatus : 0;

            ViewBag.Ordenacao = ordenacao;
            ViewBag.ParametroNome = String.IsNullOrEmpty(ordenacao) ? "Nome_desc" : ""; //Se nao vier nula a ordenacao aplicar por nome decrescente

            //atribui 1 a pagina caso os parametros nao sejam nulos
            page = (procuraAtivo != null) || (procuraPerfil != null) ? 1 : page; //atribui 1 à pagina caso procurapor seja diferente de nullo


            procuraAtivo = (procuraAtivo == null) ? filtroAtivo : procuraAtivo; //atribui o filtro corrente se procuraPor estiver nulo
            procuraPerfil = (procuraPerfil == null) ? filtroPerfil : procuraPerfil;

            ViewBag.FiltroAtivo = procuraAtivo;
            if (procuraPerfil != null)
            {
                ViewBag.FiltroCorrentePerfilInt = int.Parse(procuraPerfil);
            }


            this.listPerfil = db.Perfis.ToList();

            //buscar os auditados
            switch (ViewBag.Status)
            {
                case 0://somente os não auditados
                    this.listPerfil = this.listPerfil.Where(s => s.Ativo == 1).ToList();
                    break;
                case 1: //somente os auditados
                    this.listPerfil = this.listPerfil.Where(s => s.Ativo == 0).ToList();
                    break;
                case 2: //todos
                    this.listPerfil = this.listPerfil.Where(s => s.Ativo == 0 || s.Ativo == 1).ToList();
                    break;
            }


            //procura
            if (!String.IsNullOrEmpty(procuraAtivo))
            {
                this.listPerfil = listPerfil.Where(s => s.Ativo.HasValue).ToList();
            }
            if (!String.IsNullOrEmpty(procuraPerfil))
            {
                this.listPerfil = this.listPerfil.Where(s => s.Descricao.ToString() == procuraPerfil).ToList();
            }


            //montar a pagina
            int tamanhoPagina = 0;

            //Ternario para tamanho da pagina
            tamanhoPagina = (ViewBag.NumeroLinha != null) ? ViewBag.NumeroLinhas : (tamanhoPagina = (numeroLinhas != 10) ? ViewBag.numeroLinhas : (int)numeroLinhas);
            int numeroPagina = (page ?? 1);

            ViewBag.MensagemGravar = (resultado != null) ? resultado : "";
            ViewBag.RegSalvos = (qtdSalvos != null) ? qtdSalvos : "";


            if (ViewBag.AcessoPermitido == null)
            {
                int par = 7;
                return RedirectToAction("Index", "Erro", new { param = par });
            }
            else
            {
                foreach (var item in ViewBag.AcessoPermitido)
                {
                    if (item.TabelaV == true)
                    {
                        return View(this.listPerfil.ToPagedList(numeroPagina, tamanhoPagina));//retorna o pagedlist
                    }
                    else
                    {
                        int par = 7;
                        return RedirectToAction("Index", "Erro", new { param = par });

                    }

                }

            }

           


           
            return View();
        }

        public ActionResult Incluir()
        { 
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
           
            var model = new PerfilViewModel();
          
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(PerfilViewModel model)
        {
            string resultado = "";
            int regSalvos = 0;
            model.DataCAd = DateTime.Now;
            model.DataAlt = DateTime.Now;
            model.Ativo = 1;

            if (ModelState.IsValid)
            {
                var perfil = from s in db.Perfis select s;

                perfil = perfil.Where(s => s.Descricao.Contains(model.Descricao));

                if (perfil.Count() > 0)
                {
                    resultado = "Já Existe um perfil com essa descrição";
                    regSalvos = 0;
                    return RedirectToAction("Index", new { param = resultado, qtdSalvos = regSalvos });

                }
                var perf = new Perfil()
                {
                    Descricao = model.Descricao.ToUpper(), //passa para maiusculo
                    Ativo = model.Ativo,
                    DataCAd = model.DataCAd,
                    DataAlt = model.DataAlt

                };

                try
                {
                    db.Perfis.Add(perf);
                    db.SaveChanges();
                    regSalvos++;
                    resultado = "Registro Salvo Com Sucesso !!";
                }
                catch (Exception e)
                {
                    string ex = e.ToString();
                    regSalvos = 0;
                    resultado = "Não foi possivel salvar o registro";
                }
            }
            else 
            {
                return View();
            }

            return RedirectToAction("Index", new { param = resultado, qtdSalvos = regSalvos });
        }

        public ActionResult Edit(int? Id, bool? atv)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
           

            if (atv !=null)
            {
                var perfilAtivo = db.Perfis.Find(Id);

                if(perfilAtivo.Ativo == 1)
                {
                    perfilAtivo.Ativo = 0;
                }
                else
                {
                    perfilAtivo.Ativo = 1;
                }

                db.SaveChanges();
               
                return RedirectToAction("Index");

            }

            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Perfil perfil = db.Perfis.Find(Id);

            if(perfil == null)
            {
                return HttpNotFound();
            }

           
            return View(perfil);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Descricao, Ativo, DataAlt")] Perfil model)
        {
            string resultado = "";
            model.DataAlt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var perfil = db.Perfis.Find(model.Id);
                if(perfil == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

                }
                perfil.Descricao = model.Descricao.ToUpper();
                perfil.DataAlt = model.DataAlt;
                try
                {
                    db.SaveChanges();
                    resultado = "Registro Salvo com sucesso";
                }
                catch (Exception e) {
                    resultado = "Não foi possivel salvar o registro";
                
                }
                
                return RedirectToAction("Index", new { param = resultado });

            }
            return View(model);

        }

        


        [HttpGet]
        public ActionResult Detalhes(int? Id)
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Perfil perfil = db.Perfis.Find(Id);
            ViewBag.Ativo = perfil.Ativo;

            if (perfil == null)
            {
                return HttpNotFound();
            }

            this.listaAcessos = db.Acessos.ToList();

            ViewBag.Acessos = this.listaAcessos.Where(m => m.IdPerfil.Equals(Id));
            return View(perfil);


        }


        public ActionResult Deletar(int? Id)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            string resultado;
            Perfil perfil = db.Perfis.Find(Id);

            this.acesso = db.Acessos.Where(x => x.IdPerfil.Equals(perfil.Id)).FirstOrDefault();

            if(acesso != null)
            {
                resultado = "Não é possivel Deletear esse registro. Há acessos usando este perfil!";
                return RedirectToAction("Index", new { param = resultado });
            }
            ViewBag.Ativo = perfil.Ativo;

           

            if (perfil == null)
            {
                return HttpNotFound();
            }
          
            return View(perfil);
        
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            string resultado;
            Perfil perfil = db.Perfis.Find(Id);
            db.Perfis.Remove(perfil);

            try{
                db.SaveChanges();
                resultado = "Registro Excluído com sucesso";
                return RedirectToAction("Index", new { param = resultado });

            }
            catch(Exception e)
            {
                resultado = "Não foi possivel Excluir o registro";
                return RedirectToAction("Index", new { param = resultado });
            }
           
           
        }

    }
}