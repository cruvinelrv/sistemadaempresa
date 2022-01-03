using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using SDE.Enumerador;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void Temp_Salva(int idCorp, int idClienteFuncionarioLogado, Mov mov, MovNFE movNfe, List<MovItem> listaMovItens)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //SALVA TEMPMOV
                    TempMov tMov = new TempMov();
                    List<TempMovItem> tListaMovItem = new List<TempMovItem>();

                    int idMov = 1;
                    int idMovItem = 1;
                    mov.id = idMov;
                    mov.dthrMovEmissao = Utils.getAgoraString();
                    mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;
                    foreach (MovItem mi in listaMovItens)
                    {
                        TempMovItem tmi = new TempMovItem();
                        tmi.idClienteFuncionario = idClienteFuncionarioLogado;
                        tmi.idMov = idMov;
                        tmi.id = idMovItem++;
                        tmi.bcCOFINS = mi.bcCOFINS;
                        tmi.bcICMS = mi.bcICMS;
                        tmi.bcIcmsSubstTrib = mi.bcIcmsSubstTrib;
                        tmi.bcIPI = mi.bcIPI;
                        tmi.bcISS = mi.bcISS;
                        tmi.bcPIS = mi.bcPIS;
                        tmi.bcSubsCOFINS = mi.bcSubsCOFINS;
                        tmi.bcSubsICMS = mi.bcSubsICMS;
                        tmi.bcSubsIPI = mi.bcSubsIPI;
                        tmi.bcSubsPIS = mi.bcSubsPIS;
                        tmi.cfop = mi.cfop;
                        tmi.cofinsAliq = mi.cofinsAliq;
                        tmi.cofinsCst = mi.cofinsCst;
                        tmi.estoque_identificador = mi.estoque_identificador;
                        tmi.icmsAliq = mi.icmsAliq;
                        tmi.icmsAliqPadrao = mi.icmsAliqPadrao;
                        tmi.icmsCst = mi.icmsCst;
                        tmi.idIEE = mi.idIEE;
                        tmi.idItem = mi.idItem;
                        tmi.ipiAliq = mi.ipiAliq;
                        tmi.ipiCst = mi.ipiCst;
                        tmi.item_nome = mi.item_nome;
                        tmi.nomeCompl = mi.nomeCompl;
                        tmi.ordenador = mi.ordenador;
                        tmi.pctComissaoPreco = mi.pctComissaoPreco;
                        tmi.pisAliq = mi.pisAliq;
                        tmi.pisCst = mi.pisCst;
                        tmi.qtd = mi.qtd;
                        tmi.recolhidoPeloTomador = mi.recolhidoPeloTomador;
                        tmi.rf_auxiliar = mi.rf_auxiliar;
                        tmi.rf_unica = mi.rf_unica;
                        tmi.saldoAtual = mi.saldoAtual;
                        tmi.unid_med = mi.unid_med;
                        tmi.vlrCofins = mi.vlrCofins;
                        tmi.vlrComissaoPreco = mi.vlrComissaoPreco;
                        tmi.vlrDesc = mi.vlrDesc;
                        tmi.vlrDescMax = mi.vlrDescMax;
                        tmi.vlrFrete = mi.vlrFrete;
                        tmi.vlrICMS = mi.vlrICMS;
                        tmi.vlrIcmsIsento = mi.vlrIcmsIsento;
                        tmi.vlrIcmsSubstTrib = mi.vlrIcmsSubstTrib;
                        tmi.vlrIPI = mi.vlrIPI;
                        tmi.vlrIsentoIPI = mi.vlrIsentoIPI;
                        tmi.vlrPis = mi.vlrPis;
                        tmi.vlrSeguro = mi.vlrSeguro;
                        tmi.vlrUnitCompra = mi.vlrUnitCompra;
                        tmi.vlrUnitCusto = mi.vlrUnitCusto;
                        tmi.vlrUnitVendaFinal = mi.vlrUnitVendaFinal;
                        tmi.vlrUnitVendaFinalQtd = mi.vlrUnitVendaFinalQtd;
                        tmi.vlrUnitVendaInicial = mi.vlrUnitVendaInicial;
                        dbStore(db, tmi);
                    }
                    tMov.anoReferencia = mov.anoReferencia;
                    tMov.cfop = mov.cfop;
                    tMov.cliente_contato = mov.cliente_contato;
                    tMov.cliente_cpf = mov.cliente_cpf;
                    tMov.cliente_endereco_cobranca = mov.cliente_endereco_cobranca;
                    tMov.cliente_endereco_entrega = mov.cliente_endereco_entrega;
                    tMov.cliente_endereco_faturamento = mov.cliente_endereco_faturamento;
                    tMov.cliente_nome = mov.cliente_nome;
                    tMov.cooTEF = mov.cooTEF;
                    tMov.dtEntSai = mov.dtEntSai;
                    tMov.dtExpiraReserva = mov.dtExpiraReserva;
                    tMov.dthrMovEmissao = mov.dthrMovEmissao;
                    tMov.dtLancaCaixa = mov.dtLancaCaixa;
                    tMov.dtLancTicks = mov.dtLancTicks;
                    tMov.dtMovTicks = mov.dtMovTicks;
                    tMov.dtNF = mov.dtNF;
                    tMov.dtNotaTicks = mov.dtNotaTicks;
                    tMov.fatura = mov.fatura;
                    tMov.foraMun = mov.foraMun;
                    tMov.id = mov.id;
                    tMov.idCaixa = mov.idCaixa;
                    tMov.idCaixaDia = mov.idCaixaDia;
                    tMov.idCliente = mov.idCliente;
                    tMov.idClienteEndereco = mov.idClienteEndereco;
                    tMov.idClienteFuncionarioLogado = mov.idClienteFuncionarioLogado;
                    tMov.idClienteFuncionarioVendedor = mov.idClienteFuncionarioVendedor;
                    tMov.idClienteParceiro = mov.idClienteParceiro;
                    tMov.idClienteTransportador = mov.idClienteTransportador;
                    tMov.idEmp = mov.idEmp;
                    tMov.idMovCancelada = mov.idMovCancelada;
                    tMov.idMovCanceladora = mov.idMovCanceladora;
                    tMov.idMovMapa = mov.idMovMapa;
                    tMov.idOperacao = mov.idOperacao;
                    tMov.idTransacao = mov.idTransacao;
                    tMov.impressao = mov.impressao;
                    tMov.isNfeEnviada = mov.isNfeEnviada;
                    tMov.isNfePreenchida = mov.isNfePreenchida;
                    tMov.isNfsCancelada = mov.isNfsCancelada;
                    tMov.isReservaDevolvida = mov.isReservaDevolvida;
                    tMov.mesReferencia = mov.mesReferencia;
                    tMov.modeloNF = mov.modeloNF;
                    tMov.noMun = mov.noMun;
                    tMov.numeroNF = mov.numeroNF;
                    tMov.obs = mov.obs;
                    tMov.resumo = mov.resumo;
                    tMov.retencaoINSS = mov.retencaoINSS;
                    tMov.retencaoISSQN = mov.retencaoISSQN;
                    tMov.serieNF = mov.serieNF;
                    tMov.situacaoDms = mov.situacaoDms;
                    tMov.situacaoNF = mov.situacaoNF;
                    tMov.tipo = mov.tipo;
                    tMov.tipoDeclaracaoDms = mov.tipoDeclaracaoDms;
                    tMov.tipoServicoDms = mov.tipoServicoDms;
                    tMov.vlrAcrescimo = mov.vlrAcrescimo;
                    tMov.vlrItensFinal = mov.vlrItensFinal;
                    tMov.vlrItensInicial = mov.vlrItensInicial;
                    tMov.vlrTotal = mov.vlrTotal;
                    dbStore(db, tMov);

                    //SALVA TEMPMOVNFE
                    TempMovNFE tMovNfe = new TempMovNFE();
                    Utils.filtraCampos(movNfe);
                    tMovNfe.id = 1;
                    tMovNfe.idMov = 1;
                    tMov.isNfePreenchida = true;
                    tMovNfe.ambienteNFE = movNfe.ambienteNFE;
                    tMovNfe.cfop = movNfe.cfop;
                    tMovNfe.chaveAcessoNFE = movNfe.chaveAcessoNFE;
                    tMovNfe.clienteIE = movNfe.clienteIE;
                    tMovNfe.codNumericoNFE = movNfe.codNumericoNFE;
                    tMovNfe.dtSaiEnt = movNfe.dtSaiEnt;
                    tMovNfe.horaSaida = movNfe.horaSaida;
                    tMovNfe.ehVendaVeiculo = movNfe.ehVendaVeiculo;
                    tMovNfe.fatura = movNfe.fatura;
                    tMovNfe.finalidadeNFE = movNfe.finalidadeNFE;
                    tMovNfe.formaPgtoNFE = movNfe.formaPgtoNFE;
                    tMovNfe.idCliente = movNfe.idCliente;
                    tMovNfe.idClienteTransp = movNfe.idClienteTransp;
                    tMovNfe.idEmp = movNfe.idEmp;
                    tMovNfe.idEnderecoCliente = movNfe.idEnderecoCliente;
                    tMovNfe.idEnderecoEmp = movNfe.idEnderecoEmp;
                    tMovNfe.idEnderecoEntrega = movNfe.idEnderecoEntrega;
                    tMovNfe.idEnderecoRetirada = movNfe.idEnderecoRetirada;
                    tMovNfe.idEnderecoTransp = movNfe.idEnderecoTransp;
                    tMovNfe.idmovReferenciada = movNfe.idmovReferenciada;
                    tMovNfe.idReboque = movNfe.idReboque;
                    tMovNfe.idVeiculo = movNfe.idVeiculo;
                    tMovNfe.infoAdicional = movNfe.infoAdicional;
                    tMovNfe.numeroNota = movNfe.numeroNota;
                    tMovNfe.serieNota = movNfe.serieNota;
                    tMovNfe.tipoTranspNFE = movNfe.tipoTranspNFE;
                    tMovNfe.volEspecie = movNfe.volEspecie;
                    tMovNfe.volMarca = movNfe.volMarca;
                    tMovNfe.volNumeracao = movNfe.volNumeracao;
                    tMovNfe.volPesoBruto = movNfe.volPesoBruto;
                    tMovNfe.volPesoLiquido = movNfe.volPesoLiquido;
                    tMovNfe.volQuantidade = movNfe.volQuantidade;
                    tMovNfe.valorFrete = movNfe.valorFrete;
                    tMovNfe.valorSeguro = movNfe.valorSeguro;
                    tMovNfe.valorOutrasDespesas = movNfe.valorOutrasDespesas;
                    dbStore(db, tMov);
                    dbStore(db, tMovNfe);

                    dbCommit(db); ;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }
        
        public void Temp_Deleta(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (TempMov mov in db.Query<TempMov>())
                        dbRemove(db, mov);
                    foreach (TempMovItem mi in db.Query<TempMovItem>())
                        dbRemove(db, mi);
                    foreach (TempMovNFE nfe in db.Query<TempMovNFE>())
                        dbRemove(db, nfe);

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }
    }
}