using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HandsOn.Aplicacao;
using HandsOn.Dominio;
using HandsOn.UI.Web.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HandsOn.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {
            var ListaDadosApp = new ListaDadosAplicacao();
            var SuspeitoAleatorioApp = new SuspeitoAleatorioAplicacao();

            var Model = new ListaDados();
            // Chamda assincrona
            Model = await ListaDadosApp.obterListaObjetos();

            // Chamda assincrona
            var oSuspeitoAleatorio = await SuspeitoAleatorioApp.ObterSuspeitoAleatorioAsync(Model);

            // Serializando lista de suspeitos, licais e arma
            var Model_serializado = JsonConvert.SerializeObject(Model);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\listaObjetos.json", Model_serializado);

            // Serializando suspeito aleatorio
            var suspeitoAleatorio_serializado = JsonConvert.SerializeObject(oSuspeitoAleatorio);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\suspeitoAleatorio.json", suspeitoAleatorio_serializado);

            return View(Model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ListaDados Model)
        {
            var oSuspeitoSelecionado = new SuspeitoAleatorio();
            var oRetorno = new Retorno();

            // Desserializando Lista de Suspeitos, Locais e armar
            var listaObjetos_serializado = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\listaObjetos.json");
            var oListaObjetos = JsonConvert.DeserializeObject<ListaDados>(listaObjetos_serializado);

            if (Model.IdSuspeito == 0 || Model.IdLocal == 0 || Model.IdArma == 0 || Model.IdSuspeito == null || Model.IdLocal == null || Model.IdArma == null)
            {
                Model = oListaObjetos;
                return View(Model).Mensagem("Um suspeito, um local e uma arma devem ser selecionados!", "Erro");
            }

            oSuspeitoSelecionado.IdSuspeito = Model.IdSuspeito;
            oSuspeitoSelecionado.IdLocal = Model.IdLocal;
            oSuspeitoSelecionado.IdArma = Model.IdArma;

            // Desserializando Lista de Suspeitos, Locais e armar
            var suspeitoAleatorio_json = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\suspeitoAleatorio.json");
            var oSuspeitoAleatorio = JsonConvert.DeserializeObject<SuspeitoAleatorio>(suspeitoAleatorio_json);

            var oSuspeitoAleatorioApp = new SuspeitoAleatorioAplicacao();

            // Chamda assincrona
            oRetorno = await oSuspeitoAleatorioApp.VerificarSuspeito(oSuspeitoSelecionado, oSuspeitoAleatorio);

            Model = oListaObjetos;

            return View(Model).Mensagem(oRetorno.Descricao, "Resultado");
        }
    }
}
