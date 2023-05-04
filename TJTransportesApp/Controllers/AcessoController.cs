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
    public class AcessoController : Controller
    {
        readonly TjDbContext db;

        List<Perfil> listPerfil = new List<Perfil>();
        List<Acesso> listaAcessos = new List<Acesso>();
        List<Acesso> acess = new List<Acesso>();
       
        string Tabela = "ACESSO";
        public AcessoController()
        {
            db = new TjDbContext();
        }
        public ActionResult Index(string param, string param2, string ordenacao, string qtdSalvos, string procuraAtivo, string procuraTabela,
            string filtroAtivo, string filtroTabela, int? page, int? numeroLinhas, int? inputStatus, string procuraPerfil)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.PerfilUsuario = Session["perfilId"];
            }
            if(param == null)
            {
                param = "";
            }

            if (param2 == null)
            {
                param2 = "";
            }

         


            //lista de acessos
            this.acess = db.Acessos.ToList();

            //lista de acesso do perfil do usuario logado
            this.acess = this.acess.Where(x => x.IdPerfil == ViewBag.PerfilUsuario).ToList(); //aqui pega os acessos

            //filtra pela tabela que sera manipulada pela action
            ViewBag.AcessoPerfmitido = this.acess.Where(x => x.Tabela.Equals(this.Tabela)).ToList();

            string resultado = param;
            string resultado2 = param2;
            procuraAtivo = (filtroAtivo != null) ? filtroAtivo : procuraAtivo; //procura por nome
            procuraTabela = (procuraTabela != null) ? procuraTabela : null;

            //numero de linhas
            ViewBag.NumeroLinhas = (numeroLinhas != null) ? numeroLinhas : 10;
            ViewBag.Status = (inputStatus != null) ? inputStatus : 0;




            ViewBag.Ordenacao = ordenacao;
            ViewBag.ParametroNome = String.IsNullOrEmpty(ordenacao) ? "Nome_desc" : ""; //Se nao vier nula a ordenacao aplicar por nome decrescente

            //atribui 1 a pagina caso os parametros nao sejam nulos
            page = (procuraAtivo != null) || (procuraTabela != null) ? 1 : page; //atribui 1 à pagina caso procurapor seja diferente de nullo


            procuraAtivo = (procuraAtivo == null) ? filtroAtivo : procuraAtivo; //atribui o filtro corrente se procuraPor estiver nulo
            procuraTabela = (procuraTabela == null) ? filtroTabela : procuraTabela;


           


            ViewBag.Tabelaativo = procuraAtivo;
            ViewBag.FiltroCorrenteTabela = procuraTabela;

            this.listaAcessos = db.Acessos.ToList();

            //buscar os ativos
            switch (ViewBag.Status)
            {
                case 0://somente os não ativos
                    this.listaAcessos = this.listaAcessos.Where(s => s.Ativo == 1).ToList();
                    break;
                case 1: //somente os ativos
                    this.listaAcessos = this.listaAcessos.Where(s => s.Ativo == 0).ToList();
                    break;
                case 2: //todos
                    this.listaAcessos = this.listaAcessos.Where(s => s.Ativo == 0 || s.Ativo == 1).ToList();
                    break;
            }
            /*TO-DO 
             TEMP DATA PARA FILTRO DE PEFIL
            */

            if (TempData["perfilAtivo"]==null)
            {
                if (procuraPerfil != null)
                {
                    TempData["perfilAtivo"] = procuraPerfil;
                    ViewBag.FiltroCorrentePerfilInt = int.Parse(procuraPerfil);
                    TempData.Keep("perfilAtivo");

                }

            }
            else
            {
                if (procuraPerfil != null)
                {
                    if(TempData["perfilAtivo"].ToString() != procuraPerfil)
                    {
                        TempData["perfilAtivo"] = procuraPerfil;
                        ViewBag.FiltroCorrentePerfilInt = int.Parse(procuraPerfil);
                        TempData.Keep("perfilAtivo");
                    }
                }
                else
                {
                    procuraPerfil = TempData["perfilAtivo"].ToString();
                    ViewBag.FiltroCorrentePerfilInt = int.Parse(procuraPerfil);

                }
            }
           




            //procura
            if (!String.IsNullOrEmpty(procuraAtivo))
            {
                this.listaAcessos = this.listaAcessos.Where(s => s.Ativo.HasValue).ToList();
            }
            if (!String.IsNullOrEmpty(procuraTabela))
            {
                this.listaAcessos = this.listaAcessos.Where(s => s.Tabela.ToString() == procuraTabela).ToList();
            }
            if (!String.IsNullOrEmpty(procuraPerfil))
            {
                this.listaAcessos = this.listaAcessos.Where(s => s.IdPerfil.ToString() == procuraPerfil).ToList();
            }
            else
            {
                this.listaAcessos = this.listaAcessos.Where(s => s.Perfil.Descricao.ToString() == "ADMINISTRATIVOS").ToList();
            }


            //montar a pagina
            int tamanhoPagina = 0;

            //Ternario para tamanho da pagina
            tamanhoPagina = (ViewBag.NumeroLinha != null) ? ViewBag.NumeroLinhas : (tamanhoPagina = (numeroLinhas != 10) ? ViewBag.numeroLinhas : (int)numeroLinhas);
            int numeroPagina = (page ?? 1);

            ViewBag.MensagemGravar = (resultado != null) ? resultado : "";

            ViewBag.MensagemDeletar = (resultado2 != null) ? resultado2 : "";

            ViewBag.RegSalvos = (qtdSalvos != null) ? qtdSalvos : "";
           
            ViewBag.Perfil = db.Perfis.AsNoTracking().OrderBy(s => s.Descricao).ToList();

           
            foreach (var item in ViewBag.AcessoPerfmitido)
            {
                //aqui ele verifica se o usuario tem acesso a visualizar a pagina (TABELA v)
              if(item.TabelaV == true)
                {
                   
                    //if (param != null)
                    //{
                    //    ViewBag.MessagemErro = param;
                    //}
                    
                    return View(this.listaAcessos.ToPagedList(numeroPagina, tamanhoPagina));//retorna o pagedlist
                }
                else
                {
                    int par = 7;
                    return RedirectToAction("Index","Erro", new { param = par });
                   
                }
            
            }

            return View();


        }

        //incluir novos acessos ao perfil: o usuario pode criar novos perfis desde que tenha acesso a isso
        public ActionResult Incluir() 
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Perfil = db.Perfis;
            var model = new AcessoViewModel();

            return View(model);

        }

        //Incluir com POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(AcessoViewModel model)
        {
            string resultado = ""; //vai a msg de resultado
            int regSalvos = 0; //salvo registros

            model.DataCAd = DateTime.Now;
            model.DataAlt = DateTime.Now;
            model.Ativo = 1;

            if(ModelState.IsValid)
            {
                var acesso = from s in db.Acessos select s; //banco de acessos
                acesso = acesso.Where(s => s.Tabela.Contains(model.Tabela) && s.IdPerfil==model.IdPerfil);//busca se tem tabela
                if(acesso.Count()>0)
                {
                    resultado = "Já existe um acesso para essa tabela";
                    regSalvos = 0;
                    return RedirectToAction("Index", new { param = resultado, qtdSalvos = regSalvos });
                }

                //objeto para ser salvo
                Acesso access = new Acesso();

                access.Tabela  = model.Tabela;
                access.TabelaV = model.TabelaV;
                access.TabelaI = model.TabelaI;
                access.TabelaA = model.TabelaA;
                access.TabelaE = model.TabelaE;

                //access.Obs = (model != null) ? model.Obs.ToUpper() : model.Obs;
                if (model.Obs != null)
                {
                    access.Obs = model.Obs.ToUpper();
                }
                else
                {
                    access.Obs = model.Obs;
                }

                access.DataCAd = model.DataCAd;
                access.DataAlt = model.DataAlt;
                access.Ativo = model.Ativo;
                access.IdPerfil = model.IdPerfil;

                try
                {
                    db.Acessos.Add(access);
                    db.SaveChanges();
                    regSalvos++;
                    resultado = "Registro salvo com sucesso!";
                }
                catch(Exception e)
                {
                    string ex = e.ToString();
                    regSalvos = 0;
                    resultado = "Não foi possivel salvar o registro";

                }

            }
            else
            {
                regSalvos = 0;
                resultado = "Problemas com o formulário, verifique e tente novamente";
                return RedirectToAction("Index", new { param = resultado, qtdSalvos = regSalvos });

            }
            return RedirectToAction("Index", new { param = resultado, qtdSalvos = regSalvos });


        }

        
        
        
        //editar os perfis
        /*O usuario clica sobre o icone de aleração do acesso, VISUALIZAR, INCLUIR, ALTERAR, EXCULUIR
         * o siatema cai nessa action que vai verfificar se deve trocar ou nao de acordo com o acesso do usuario*/
        public ActionResult EditPerfil(int? Id, bool? tabvis, bool? tabinc, bool? tabAlt, bool? tabex, bool? atv)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.PerfilUsuario = Session["perfilId"];
            }

            var perfilAtivo = db.Acessos.Find(Id); //perfil com o nome da tabela e os outros dados
            //int teste = ViewBag.PerfilUsuario;

            //metodo para verificar o perfil passando o id do perfil do usuario
            VerificaPerfil((int)(ViewBag.PerfilUsuario));

            

            //Verifica se na tabela de acsso quais as acoes permitidas para esse usuario
            ViewBag.AcessoPerfmitido = this.acess.ToList();


            //foreach para verificar se ele pode alterar a tabela em questao
            foreach (var item in ViewBag.AcessoPerfmitido)
            {

                //se o usuario puder editar a tablea ele pode mudar os perfis da tabela
                if(item.TabelaA == true) 
                {
                    if (tabvis != null)
                    {
                        if (perfilAtivo.TabelaV == 1)
                        {
                            perfilAtivo.TabelaV = 0;
                        }
                        else
                        {
                            perfilAtivo.TabelaV = 1;
                        }

                    }
                   

                    if (tabex != null)
                    {
                        if (perfilAtivo.TabelaE == 1)
                        {
                            perfilAtivo.TabelaE = 0;
                        }
                        else
                        {
                            perfilAtivo.TabelaE = 1;
                        }

                    }

                    if (tabinc != null)
                    {
                        if (perfilAtivo.TabelaI == 1)
                        {
                            perfilAtivo.TabelaI = 0;
                        }
                        else
                        {
                            perfilAtivo.TabelaI = 1;
                        }

                    }

                    if (tabAlt != null)
                    {
                        if (perfilAtivo.TabelaA == 1)
                        {
                            perfilAtivo.TabelaA = 0;
                        }
                        else
                        {
                            perfilAtivo.TabelaA = 1;
                        }

                    }
                    if (atv != null)
                    {
                        if (perfilAtivo.Ativo == 1)
                        {
                            perfilAtivo.Ativo = 0;
                        }
                        else
                        {
                            perfilAtivo.Ativo = 1;
                        }

                    }



                }
                else
                {
                    //uma mensagem na propria pagina
                    String par = "Usuário não permitido para essa operação";
                    return RedirectToAction("Index", "Acesso", new { param = par });

                }


            }
           


            db.SaveChanges();

            return RedirectToAction("Index");


        }

        //ACTION PARA EDITAR
        public ActionResult Edit(int? Id, bool? atv)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //ativar ou desativar o acesso
            if (atv != null)
            {
                var acessoAtivo = db.Acessos.Find(Id);

                if (acessoAtivo.Ativo == 1)
                {
                    acessoAtivo.Ativo = 0;
                }
                else
                {
                    acessoAtivo.Ativo = 1;
                }

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Acesso acesso = db.Acessos.Find(Id);

            if (acesso == null)
            {
                return HttpNotFound();
            }


            return View(acesso);
        }


        public  EmptyResult VerificaPerfil(int? perfilId)
        {

            //lista de acessos
            this.acess = db.Acessos.ToList();

            //lista de acesso do perfil do usuario logado
            this.acess = this.acess.Where(x => x.IdPerfil == perfilId).ToList(); //aqui pega os acessos

            //Verifica se na tabela de acsso quais as acoes permitidas para esse usuario
            this.acess = this.acess.Where(x => x.Tabela.Equals(this.Tabela)).ToList();
            return null;
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
            Acesso acesso = db.Acessos.Find(Id);

            ViewBag.AcessoAtivo = acesso.Ativo;
            ViewBag.Perfil = db.Perfis.Find(acesso.IdPerfil).Descricao;

            if(acesso == null)
            {
                return HttpNotFound();
            }

            return View(acesso);
            
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            string resultado;
            Acesso acesso = db.Acessos.Find(Id);
            db.Acessos.Remove(acesso);
            try
            {
                db.SaveChanges();
                resultado = "Registro Excluído com sucesso";
                return RedirectToAction("Index", new { param2 = resultado });

            }
            catch (Exception e)
            {
                resultado = "Não foi possivel Excluir o registro";
                return RedirectToAction("Index", new { param2 = resultado });
            }

        }

    }
}