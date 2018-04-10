using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HandsOn.Dominio;
using HandsOn.Repositorio;
using System.Threading.Tasks;
using System.Threading;

namespace HandsOn.Aplicacao
{
    public class SuspeitoAleatorioAplicacao
    {
        public async Task<SuspeitoAleatorio> ObterSuspeitoAleatorioAsync(ListaDados oLista)
        {
            return await Task.Run(() => gerarSuspeitoAleatorio(oLista));
        }

        private SuspeitoAleatorio gerarSuspeitoAleatorio(ListaDados oLista)
        {
            var oSuspeitoAleatorio = new SuspeitoAleatorio();

            oSuspeitoAleatorio.IdSuspeito = oSuspeitoAleatorio.obterSuspeitoAleatorio(oLista.Suspeitos.ToList());
            oSuspeitoAleatorio.IdLocal = oSuspeitoAleatorio.obterLocalAleatorio(oLista.Locais.ToList());
            oSuspeitoAleatorio.IdArma = oSuspeitoAleatorio.obterArmaAleatorio(oLista.Armas.ToList());

            return oSuspeitoAleatorio;
        }

        public async Task<Retorno> VerificarSuspeito(SuspeitoAleatorio oSuspeitoSelecionado, SuspeitoAleatorio oSuspeitoAleatorio)
        {
            return await Task.Run(() =>  oSuspeitoAleatorio.verificarSuspeito(oSuspeitoSelecionado, oSuspeitoAleatorio));
        }
    }
}