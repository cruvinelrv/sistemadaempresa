using System;
using System.Collections;
using System.Collections.Generic;
using SDE.Enumerador;
using SDE.Atributos;

namespace SDE.Entidade
{
    /*
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class TempMov
    {
        public int
            id;
        public bool isBalanco, isFinalizado;
        public string
            dthrInicio, dthrFim;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class TempMovItem
    {
        public int
            id, idTempMov,
            idItem, idIEE, idClienteFuncionario,
            qtdLancada, qtdAnterior;
        public bool isBalanco;
        public string
            item_nome, item_identificador;
    }
    */





    /*
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class MovMapa
    {
        public int id;
    }
    */
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class Mov
    {
        public string
            cfop,
            cliente_nome, cliente_cpf, cliente_contato,
            cliente_endereco_faturamento, cliente_endereco_cobranca, cliente_endereco_entrega;

        public int
            id,
            
            idOperacao, idTransacao,

            idOrdemServico,
            
            idMovMapa,
            idMovCanceladora, idMovCancelada,
            idEmp, idCaixa, idCaixaDia,
            idClienteParceiro, idCliente, 
            idClienteFuncionarioLogado, idClienteFuncionarioVendedor,
            idClienteTransportador, 
            idClienteEndereco,
 
            validadeDias,
            //idEnderecoCliente,
            cooTEF, modeloNF, numeroNF;
        public bool
            noMun, foraMun, isEmailEnviado;
        public double
             vlrAcrescimo, vlrItensInicial, vlrItensFinal, vlrTotal,
             retencaoISSQN, retencaoINSS,

             //valores salvos na entrada de mercadoria
             vlrFrete, vlrSubstituicaoTributaria, vlrArredondamentoNota;

        public double
            comissaoFucionarioMontanteTotal, comissaoFucionarioMaoDeObra, comissaoFuncionarioMaoDeObraGeral,
            comissaoFucionarioMaoDeObraGarantia, comissaoFuncionarioMaoDeObraGeralGarantia,
            comissaoFucionarioProdutosEmGarantia, comissaoFucionarioProdutos;

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
            situacaoDms, tipoDeclaracaoDms, tipoServicoDms,
            
            pagamento, entrega, frete, impostos;
        public long
            dtMovTicks, dtLancTicks, dtNotaTicks;
        public bool
            isReservaDevolvida, isNfePreenchida, isNfeEnviada,
            isNfsCancelada;
        /*
        //CAMPOS OrdemServico

        public string
            numServico, descricao,
            dthrInicioOS, //"00/00/0000 00:00:00"
            dthrPrevisaoOS; //"00/00/0000 00:00:00"
        public long dtInicioOSTicks, dtPrevisaoOSTicks;
        public bool isContratoOS;
        public EOrdemServicoTipo tipoOS;

        //FIM CAMPOS OrdemServico
         * */
        public Cliente
            __cli, __cliFuncionario,
            __cliTransp, __cliParceiro;
        public List<MovItem> __mItens;
        public List<MovValor> __mValores;
        public EMovResumo resumo;
        public EMovTipo tipo;
        public EMovImpressao impressao;
        public ESituacaoNota situacaoNF;

        /*
        public EDmsSituacao situacaoDms;
        public EDmsTipoDeclaracao tipoDeclaracaoDms;
        public EDmsTipoServico tipoServicoDms;
         * */

        public MovNFE __movNfe;
        public Empresa __emp;
    }

    //class MovNFE
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class MovNFE
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
    public sealed class MovNfeVeiculo
    {
        public int
            id;
        public int
            idMovNFE;
        public double
            pesoL, pesoB;
        public int
            tipoOperacao, //1- , 2- , 3- , 4-
            tipoCombustivel,
            tipoVeiculo,
            tipoPintura,
            especie,
            condicaoVIN,
            condicaoVeic,
            
            distanciaEixos,
            codMarcaModelo, codCor,

            anoModel, anoFab,
            cmkg, cm3, potencia;

        public string
            chassi, numeroMotor,
            serie,
            desCor;

    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class MovItem
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
            vlrFrete, vlrSeguro, vlrDesc, vlrDescUnit, vlrDescMax; //valores adicionais
        
        public bool
            recolhidoPeloTomador;

        public Item
            __item;
        public List<MovItemEstoque>
            __mIEstoques;
        public IList
            __executores;
        
    }


    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class MovItemEstoque
    {
        public int
            id;
        public int
            idMovItem;
        public int
            idIEE;
        public double
            qtd;
        public string
            identificador, lote, codBarrasGrade, dtFab, dtVal;
        public ItemEmpEstoque __iee;
    }
    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class MovValor
    {
        public int
            id, idMov,
            idCaixa, idCaixaDia,
            qtdParcelas;
        public double
            valor;
        public string
            dtPrimeiraParcela;
        public long
            dtPrimeiraParcelaTicks;
        public EValorEspecie
            especie;
    }

    [AtEnt(EnumBanco.bancoCorp, true)]
    public sealed class Orcamento_Lancamento
    {
        public int
            id,


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
    }

}
