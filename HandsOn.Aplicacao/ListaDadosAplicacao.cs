using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HandsOn.Dominio;
using HandsOn.Repositorio;
using System.Threading;
using System.Threading.Tasks;

namespace HandsOn.Aplicacao
{
    public class ListaDadosAplicacao
    {
        public async Task<ListaDados> obterListaObjetos()
        {
            return await Task.Run(() => gerarObjetosModelAsync());
        }

        private ListaDados gerarObjetosModelAsync()
        {
                var SuspeitoApp = SuspeitoAplicacaoConstrutor.SuspeitoAplicacao();
                var LocalApp = LocalAplicacaoConstrutor.LocalAplicacao();
                var ArmaApp = ArmaAplicacaoConstrutor.ArmaAplicacao();

                var Lista = new ListaDados();

                Lista.Suspeitos = SuspeitoApp.listarTodos();
                Lista.Locais = LocalApp.listarTodos();
                Lista.Armas = ArmaApp.listarTodos();

                return Lista;
        }
    }
}