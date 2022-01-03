using SDE.Atributos;
using System.Collections.Generic;
using SDE.Enumerador;
using System.Collections;

namespace SDE.Entidade
{
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class TempMov
    {
        public string
            cfop,
            cliente_nome, cliente_cpf, cliente_contato,
            cliente_endereco_faturamento, cliente_endereco_cobranca, cliente_endereco_entrega;

        public int
            id,

            idOperacao, idTransacao,

            idMovMapa,
            idMovCanceladora, idMovCancelada,
            idEmp, idCaixa, idCaixaDia,
            idClienteParceiro, idCliente,
            idClienteFuncionarioLogado, idClienteFuncionarioVendedor,
            idClienteTransportador,
            idClienteEndereco,
            //idEnderecoCliente,
            cooTEF, modeloNF, numeroNF;
        public bool
            noMun, foraMun;
        public double
             vlrAcrescimo, vlrItensInicial, vlrItensFinal, vlrTotal,
             retencaoISSQN, retencaoINSS;
        public string
            dthrMovEmissao, //"00/00/0000 00:00:00"
            dtLancaCaixa,   //"00/00/0000"
            dtExpiraReserva,//"00/00/0000"
            dtNF, //"00/00/0000"
            dtEntSai, //"00/00/0000"
            anoReferencia,
            mesReferencia,
            serieNF,
            fatura,
            obs,
            situacaoDms, tipoDeclaracaoDms, tipoServicoDms;
        public long
            dtMovTicks, dtLancTicks, dtNotaTicks;
        public bool
            isReservaDevolvida, isNfePreenchida, isNfeEnviada,
            isNfsCancelada;
        public Cliente
            __cli, __cliFuncionario,
            __cliTransp, __cliParceiro;
        public List<MovItem> __mItens;
        public List<MovValor> __mValores;
        public EMovResumo resumo;
        public EMovTipo tipo;
        public EMovImpressao impressao;
        public ESituacaoNota situacaoNF;

        public MovNFE __movNfe;
        public Empresa __emp;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class TempMovNFE
    {
        public string
            dtSaiEnt,
            cfop, infoAdicional,
            chaveAcessoNFE, clienteIE,
            fatura; //codigo da chave de acesso da NFE 44-DIGITOS.....
        public long
            codNumericoNFE; //codigo numerico q compoe a chave de acesso
        //dados da Nota
        public int
            idMov, id,
            idmovReferenciada,
            numeroNota, serieNota;

        public ENfeAmbiente
            ambienteNFE;
        public ENfeFinalidade
            finalidadeNFE;
        public ENfeFormaPgto
            formaPgtoNFE;
        //Dados do Emitente/destinatariso
        public int
            idEmp, idEnderecoEmp,
            idCliente, idEnderecoCliente,
            idEnderecoEntrega, idEnderecoRetirada;

        //Dados do Transporte
        public int
            idClienteTransp,
            idEnderecoTransp,
            idVeiculo, idReboque;

        public ENfeTipoTransporte
            tipoTranspNFE;// 0 - por conta do Emitente, 1 - por conta destinatario
        public double
            volQuantidade, volPesoLiquido, volPesoBruto,
            
            valorFrete, valorSeguro, valorOutrasDespesas;
        public string
            volEspecie, volMarca, volNumeracao,

            horaSaida;


        public Cliente
            __transportador;
        public ClienteVeiculo
            __veiculo, __reboque;
        public ClienteEndereco
            __ceCliente, __ceEmpresa, __ceTransporte;
        //dados movimentacao Referenciada
        public MovNFE
            __nfeRf;

        public bool
            ehVendaVeiculo;
        public MovNfeVeiculo
            __nfeVeiculo;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class TempMovItem
    {
        public int
            id,


            idOperacao, idTransacao,


            idMov, idIEE,
            idBalancoItem,
            idItem, idClienteFuncionario,
            cfop, ordenador;
        public string
            nomeCompl, item_nome, estoque_identificador,
            rf_unica, rf_auxiliar, unid_med,
            icmsCst, ipiCst, cofinsCst, pisCst; //descrição CST
        public double
            vlrICMS, vlrIcmsIsento,//50
            vlrIPI, vlrIsentoIPI,//51
            vlrCofins, vlrPis,
            bcICMS, bcSubsICMS,
            bcIPI, bcSubsIPI,
            bcPIS, bcSubsPIS,
            bcCOFINS, bcSubsCOFINS,

            bcISS,

            bcIcmsSubstTrib, vlrIcmsSubstTrib,//53
            icmsAliqPadrao, icmsAliq, ipiAliq, pisAliq, cofinsAliq;//aliquotas
        public double
            pctComissaoPreco,
            qtd, saldoAtual,
            vlrUnitCusto, vlrUnitCompra,
            vlrUnitVendaInicial, vlrUnitVendaFinal,
            vlrUnitVendaFinalQtd, vlrComissaoPreco,
            vlrFrete, vlrSeguro, vlrDesc, vlrDescMax; //valores adicionais

        public bool
            recolhidoPeloTomador;

        public Item
            __item;
        public List<MovItemEstoque>
            __mIEstoques;
        public IList
            __executores;

    }
}
