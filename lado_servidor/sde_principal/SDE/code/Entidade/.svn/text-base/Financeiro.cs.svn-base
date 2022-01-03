using System;
using System.Collections.Generic;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{

    [AtEnt(EnumBanco.bancoCorp, true, true)]
    public class ContadorTransacao
    {
        public int id, idEmp, idClienteFuncionarioLogado;
        public string dthr;
    }
    [AtEnt(EnumBanco.bancoCorp, true, true)]
    public class ContadorOperacao
    {
        public int  id, idEmp, idClienteFuncionarioLogado;
        public string dthr;
        /*
         * IList
         *      titulos, tituloItens,
         *      finLancamentos,
         *      cxLancamentos,
         *      movs, movItens,
         *      //mapas
         * */
    }
    /*
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_Titulo_Mapa
    {
        public int
            id;
    }
     * */
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_Titulo
    {
        public int
            id,

            idOperacao, idTransacao,

            idTituloOrigem,


            idEmp, idClienteFuncionarioLogado,
            idTituloMapa, idTipoDocumento, idGrupoTipoPagamento, idPortador,
            idClienteAPagar, idClienteAReceber,
            idClienteChequeRepassado;
        public string
            dtLancamento,
            tipoDocumento_nome, tipoPagamento_nome, dtPagamento, portador_nome, grupoTipoPagamento_nome,
            identificador, descricao,
            observacoes;
        public double
            valorOriginal, valorCobrado, valorRecebido,
            txJuroParcelamento, txJuroAtraso, txMultaAtraso,
            tipoPagamento_pctComissao;
        public bool
            isVencido, isBaixado, isCompensado, isAlterado, tipoPagamento_geraContasReceber, tipoPagamento_geraContasPagar,
            isDevolvido1, isDevolvido2;
        //public EValorEspecie resumoCaixa;

        //propiedades usadas no cadastro de cheque + algumas aí em cima =p
        public int
            idFornecedorCheque, idContaDestino;
        public string
            telefone, banco, agencia, conta, numCheque,
            dtEmissao, dtBaixa, dtCompensacao, dtDevolucao1, dtDevolucao2, obs;

        public ETipoTitulo tipo;
        public ETituloSituacao situacao;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_TituloItem
    {
        public int
            idEmp, id, idClienteFuncionarioLogado,
            idContadorOperacao,
            idTituloMapa, idTipoPagamento, idGrupoTipoPagamento, idTitulo,
            
            parcela;
        public string
            texto, dtLancamento, dtPagamento,

            descontoValidade, condEspeciais, obs,

            identificador;
        public double
            valorCobrado,

            descontoPct, descontoVlr;

        public ETituloSituacao situacao;
    }



    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_Lancamento
    {
        public int
            id,


            idOperacao, idTransacao,



            idEmp, idClienteFuncionarioLogado,
            idContaOrigem, idContaDestino, idCentroCusto,
            idTipoLancamento, idTipoPagamento;
        public String
            dtFluxoCaixa, dtLancamento,
            nome, tipoLancamento_nome,
            historico;
        public double
            valorLancado, saldoAtual, saldoAnterior;
        public bool
            isCredito;

        //dados usados somente no cliente Bilharbol
        public bool
            isRecebimentoRota;

        public int
            idFinan_LancamentoOrigem,
            qtdEstimada, qtdRecebida;
        
        public double
            valorBruto, valorDespesas, valorLiquido,
            porcentagemComissao, valorComissao;

        //

    }








    /**
     * 
     * a classe a seguir sobrescreve o chamado "Resumo de Caixa"
     * que também servia para agrupar os tipos de pagamento,
     * 
     * toda e qualquer verificação se parcela.resumoCaixa==ABC||parcela.resumoCaixa==XYZ
     * deve ser substituída por uma verificação no tipo de pagamento usado na parcela,
     * tais quais se é um "Haver", se é um "Cheque", se "Gera Parcelas" (e outras atribuições de comportamento)
     * 
     * a implementação dessa classe é dada pelos mesmos motivos
     * que das classes "Finan_PortadorTipo" e "Finan_PlanoConta"
     * que é: não é confiável fazermos verificações com valores de enumeradores,
     * é mais confiável criarmos classes a parte para representar essas caracteristicas,
     * ou que coloquemos as caracteristicas embutidas na classe que contém a variável:
     * exemplo:
     * é preferivel confiarmos na verificação: (tipoPagamento.ehCheque || tipoPagamento.ehDinheiro)
     * que se confiássemos na verificação: (tipoPagamento.resumoCaixa==EResumoCaixa.Cheque || tipoPagamento.resumoCaixa==EResumoCaixa.Dinheiro)
     * 
     * */
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_GrupoTipoPagamento
    {
        public int
            idEmp, id, idClienteFuncionarioLogado;
        public String
            nome;
    }
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_CentroCusto
    {
        public int idEmp, id, idClienteFuncionarioLogado;
        public String nome;
        //public List<Finan_CentroCustoMes> __meses;
    }
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_TipoDocumento
    {
        public int idEmp, id,
            idClienteFuncionarioLogado;
        public String nome;
    }
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_PortadorTipo
    {
        public int idEmp, id,
            idClienteFuncionarioLogado;
        public String nome;
    }
    /*
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_PlanoContaTipo
    {
        public int idEmp, id,
            idClienteFuncionarioLogado;
        public String nome, sigla;
    }
    */
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nomeTipoLancamento")]
    public class Finan_TipoLancamento
    {
        public int idEmp, id, idClienteFuncionarioLogado,
            codigoTipoLancamento, codigoGrupoLancamento;
        public String nomeGrupoLancamento, nomeTipoLancamento, codigo;
        //exemplos
        //codigo: LAN001003
        //codigoGrupoLancamento: 1;
        //codigoTipoLancamento: 3;
    }
    /*
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_PlanoConta
    {
        public int idEmp, id,
            codConta, codContaPai, idTipoPlanoConta,
            idClienteFuncionarioLogado;
        public String nomeContaPai, nomeConta, cod;
        //public EPlanoContaTipo tipo; //são eles: Im, RF, RV, DF, DV
        //Foi discutido a não-obrigatoriedade de "direção e sentido" ao plano de contas
        //o que possibilitaria planos de contas a fazerem operações positivas e negativas,
        //para facilitar a visualização de setores de devolução
    }
     * */
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_Portador
    {
        public int idEmp, id, idTipoPortador,
            idClienteFuncionarioLogado;
        public String nome;
        //public Finan_PortadorTipo tipoPortador;
        //public EPortadorTipo tipo; //são eles: Carteira, Cartorio, Banco, Cobrador, Juridico
        //Esse campo foi transposto para um cadastro a parte
    }
    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_Conta
    {
        public int idEmp, id,
            idClienteFuncionarioLogado;
        public String
            nome, operacao, banco, ag,
            conta, carteira, dtSaldoInicial;
        public double limite, saldoInicial, saldoAtual, saldoAnterior;
        public EContaTipo tipo;
        public bool __forcaAtualizacao, isCapitalTotal;
    }





    [AtEnt(EnumBanco.bancoCorp, true, toString = "nome")]
    public class Finan_TipoPagamento
    {
        public bool
            isHabilitado, ehPrazo,
            podeAlterarQtdParcelas, podeAlterarJuroParcela, podeAlterarPeriodo,
            geraContasPagar, geraContasReceber, utilizaSenha, imprimeCarne,
            __forcaAtualizacao;
        public int
            idEmp, id,
            idPortador, idTipoDocumento,
            idGrupoTipoPagamento,
            idClienteFuncionarioLogado,
            qtdParcelas, periodo;
        public double
            txJuroParcelamento, txJuroAtraso, txMultaAtraso, pctCustoAdministrativo,
            pctComissao;
        public String
            nome, senha, grupoTipoPagamento_nome;// portador_nome, nomeTipoDocumento;

        //public Finan_Portador __portador;
        //public Finan_TipoDocumento __tipoDocumento;
        //public IList<Finan_TipoPagamento_Parcela> __parcelas;
        //public EValorEspecie resumoCaixa;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public class Finan_TipoPagamento_Parcela
    {
        public int id, idTipoPagamento;
        public int numParcela, dias;
        public double porcentagem, taxaJuro, taxaMultaDiaria;
    }
















}
