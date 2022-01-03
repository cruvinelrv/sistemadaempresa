using System;
using System.Collections.Generic;
using System.Text;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{

    //aqui ficam todos os lançamentos feitos ao caixa, dinheiro, titulos, tudo
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Cx_Lancamento
    {
        public bool
            isEntrada, tipoPagamento_geraContasPagar, tipoPagamento_geraContasReceber;
        public int
            id, idEmp,


            idOperacao, idTransacao,


            idClientePagador, idClienteRecebedor,
            //se isso for uma venda
            idMovMapa,
            idTipoPagamento, idGrupoTipoPagamento,
            //se for venda a prazo
            idPortador
            
            ;
        public string
            
            dthr,

            tipoLancamentoDoCaixa,//venda_a_vista, venda_a_prazo, lancamento_manual, fechamento_caixa

            dtPagamento, grupoTipoPagamento_nome,
            tipoPagamento_nome,

            nome, observacoes;//dthrLancamento, 
        public double
            tipoPagamento_pctComissao,
            valorOriginal, valorCobrado, valorRecebido,
            txJuroParcelamento, txJuroAtraso, txMultaAtraso;

        public ECxLancamentoSituacao
            situacao;
        public ECxLancamentoTipo tipo;
    }

    //Guarda data de abertura de caixa e o valor do saldo na abertura no dia especificado
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Cx_Diario
    {
        public int id, idEmp;
        public string data;
        public double valorAbertura, valorFechamento, totalEntradas, totalSaidas, saldo;
        public ECxDiarioSituacao situacao;
    }

    /*
    public class Cx_Caixa
    {
        public int
            id, idEmp;
        
        //public string dtInicio;
        //public long dtInicio_ticks;
        //public bool fechado;
        idFinanConta
    }
    public class Cx_CaixaDia
    {
        public int
            id, idCaixa, idClienteFuncionarioLogado;
        public string
            mes, dtDia;
        public IList<Mov>
            __movimentacoes;
        public Dictionary<string, double>
            __totais;
    }
    */
}
