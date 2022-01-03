using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.CamadaControle;
using SDE.Constantes;
using SDE.PDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes : SuperNuvem
    {
        #region Cad Listas

        public void Cad_Generico_Novos(int idCorp, string classe, IList objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Type t = Assembly.Load("SDE").GetType(string.Concat("SDE.Entidade.", classe));
                    if (t == null)
                        throw new Exception("CLASSE '"+classe +"' NÃO ENCONTRADA EM 'SDE'");

                    int proxID = AppFacade.get.reaproveitamento.getIncremento(db, t, objetos.Count);
                    
                    foreach (Object xxx in objetos)
                    {
                        t.GetField("id").SetValue(xxx, proxID++);
                        Utils.filtraCampos(xxx);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void Cad_Generico_Reseta(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Object o in db.Query<Cad_Secao>())
                        db.Delete(o);
                    foreach (Object o in db.Query<Cad_Marca>())
                        db.Delete(o);
                    foreach (Object o in db.Query<Cad_Grade>())
                        db.Delete(o);

                    

                    //
                    Cad_Secao s = new Cad_Secao(){ idClienteFuncionarioLogado=1, secao="Geral", grupo="", subgrupo="" };
                    Cad_Marca m = new Cad_Marca(){ idClienteFuncionarioLogado=1, marca="Geral", modelo="" };

                    //int proxID = AppFacade.get.reaproveitamento.getIncremento(db, t, objetos.Count);
                    AppFacade.get.reaproveitamento.setIncremento(db, typeof(Cad_Secao), 1);
                    AppFacade.get.reaproveitamento.setIncremento(db, typeof(Cad_Marca), 1);
                    AppFacade.get.reaproveitamento.setIncremento(db, typeof(Cad_Grade), 0);
                    m.id = 1;
                    s.id = 1;
                    /*
                    ProximoCampo prox = new ProximoCampo(idCorp);
                    prox.reiniciaCampo(Campo.idCadMarca);
                    prox.reiniciaCampo(Campo.idCadSecao);
                    prox.reiniciaCampo(Campo.idCadGrade);
                    */

                    dbStore(db, m);
                    dbStore(db, s);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        #endregion



        #region Venda

        public void MovEscrevePdf(int idCorp, int idEmp, int idMov)
        {
            ConstrutorPDF.relatorioMov(idCorp, idEmp, idMov);
        }

        public int Mov_Salva(
            int idCorp, int idEmp, int idClienteFuncionarioLogado,
            Mov mov,
            List<MovItem> carrinho,
            List<Cx_Lancamento> valores,
            OrdemServico ordemServico)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    ReaproveitamentoCodigo r = AppFacade.get.reaproveitamento;

                    int idTransacao = r.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    int idOperacao = r.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);

                    int idMov = r.getIncremento(db, typeof(Mov), 0);
                    int idMovItem = r.getIncremento(db, typeof(MovItem), carrinho.Count);

                    mov.idOperacao = idOperacao;
                    mov.idTransacao = idTransacao;
                    mov.id = idMov;
                    mov.idEmp = idEmp;
                    if (mov.dthrMovEmissao == "")
                        mov.dthrMovEmissao = Utils.getAgoraString();
                    mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;

                    //foreach (MovItem mi in carrinho)
                    for (int i = 0; i < carrinho.Count; i++)
                    {
                        MovItem mi = carrinho[i];
                        mi.idClienteFuncionario = idClienteFuncionarioLogado;
                        mi.idMov = idMov;
                        mi.id = idMovItem++;
                        mi.idOperacao = idOperacao;
                        mi.idTransacao = idTransacao;


                        //aqui tratamos apenas o "tipo", não a impressão
                        switch (mov.tipo)
                        {
                            case EMovTipo.entrada_devolucao:
                                {
                                    IQuery query = db.Query();
                                    query.Constrain(typeof(Item));
                                    query.Descend("id").Constrain(mi.idItem);
                                    if ((query.Execute()[0] as Item).tipo == EItemTipo.produto)
                                    {
                                        ItemEmpEstoque iee = r.Item_LoadEstoque(db, mi.idIEE, false);
                                        iee.qtd += mi.qtd;
                                        iee.qtd = Utils.formataQuantidade(iee.qtd);
                                        dbStore(db, iee);
                                    }
                                }
                                break;
                            case EMovTipo.saida_venda:
                                {
                                    IQuery query = db.Query();
                                    query.Constrain(typeof(Item));
                                    query.Descend("id").Constrain(mi.idItem);
                                    if ((query.Execute()[0] as Item).tipo == EItemTipo.produto)
                                    {
                                        ItemEmpEstoque iee = r.Item_LoadEstoque(db, mi.idIEE, false);
                                        if (mov.idOrdemServico != 0)
                                            iee.qtdReserva -= mi.qtd;
                                        iee.qtd -= mi.qtd;
                                        iee.qtd = Utils.formataQuantidade(iee.qtd);
                                        iee.qtdReserva = Utils.formataQuantidade(iee.qtdReserva);
                                        dbStore(db, iee);
                                    }
                                }
                                break;
                            case EMovTipo.outros_reserva:
                                {
                                    ItemEmpEstoque iee = r.Item_LoadEstoque(db, mi.idIEE, false);
                                    iee.qtdReserva -= mi.qtd;
                                    iee.qtdReserva = Utils.formataQuantidade(iee.qtdReserva);
                                    dbStore(db, iee);
                                }
                                break;
                            case EMovTipo.outros_orcamento:
                                break;
                            case EMovTipo.outros_pedido:
                                {
                                    IQuery query = db.Query();
                                    query.Constrain(typeof(Item));
                                    query.Descend("id").Constrain(mi.idItem);
                                    if ((query.Execute()[0] as Item).tipo == EItemTipo.produto)
                                    {
                                        ItemEmpEstoque iee = r.Item_LoadEstoque(db, mi.idIEE, false);
                                        if (mov.idOrdemServico != 0)
                                            iee.qtdReserva -= mi.qtd;
                                        iee.qtd -= mi.qtd;

                                        iee.qtdReserva = Utils.formataQuantidade(iee.qtdReserva);
                                        iee.qtd = Utils.formataQuantidade(iee.qtd);

                                        dbStore(db, iee);
                                    }
                                }
                                break;
                            default:
                                throw new ExcecaoSDE("CÓDIGO NÃO TRATADO, 'int Mov_Salva' com mov.tipo == "+mov.tipo);
                        }

                        Utils.filtraCampos(mi);
                        dbStore(db, mi);
                    }




                    if (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido)
                    {
                        Cx_Lancamento cxLPadrao = valores[0];
                        List<Finan_TituloItem> listaTituloItem = new List<Finan_TituloItem>();

                        int idFinanTitulo = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);

                        double valorTotalTitulo = 0;
                        int parcela = 1;
                        bool existeContaReceber = false;
                        for (int i = 0; i < valores.Count; i++)
                        {
                            Cx_Lancamento cxl = valores[i];
                            cxl.id = r.getIncremento(db, typeof(Cx_Lancamento), 0);
                            cxl.idEmp = idEmp;
                            if (mov.impressao == EMovImpressao.pedido)
                                cxl.dthr = mov.dthrMovEmissao;
                            else
                                cxl.dthr = Utils.getAgoraString();
                            cxl.idOperacao = idOperacao;
                            cxl.idTransacao = idTransacao;
                            if (!cxl.tipoPagamento_geraContasPagar && !cxl.tipoPagamento_geraContasReceber)
                                cxl.situacao = ECxLancamentoSituacao.lancado;
                            else
                                cxl.situacao = ECxLancamentoSituacao.aberto;
                            cxl.tipo = ECxLancamentoTipo.venda;

                            if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, mov.dthrMovEmissao.Substring(0, 10)))
                                Caixa_Abertura(idCorp, idEmp, mov.dthrMovEmissao.Substring(0, 10), 0, true);

                            //Busca a forma de pagamento
                            IQuery query = db.Query();
                            query.Constrain(typeof(Finan_TipoPagamento));
                            query.Descend("id").Constrain(cxl.idTipoPagamento);
                            Finan_TipoPagamento ftp = query.Execute()[0] as Finan_TipoPagamento;

                            //Lança o valor recebido no Caixa Diário
                            if (ftp.nome == "DINHEIRO" || ftp.nome == "A VISTA")
                                if (cxl.situacao == ECxLancamentoSituacao.lancado)
                                    Caixa_LancamentoDiario(db, idEmp, cxl.dthr.Substring(0, 10), cxl.valorCobrado, true);

                            if (cxl.tipoPagamento_geraContasReceber)
                            {
                                Finan_TituloItem finanTituloItem = new Finan_TituloItem();
                                finanTituloItem.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TituloItem), 0);
                                finanTituloItem.idTitulo = idFinanTitulo;
                                finanTituloItem.idEmp = idEmp;
                                finanTituloItem.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                                finanTituloItem.idGrupoTipoPagamento = cxl.idGrupoTipoPagamento;
                                finanTituloItem.idTipoPagamento = cxl.idTipoPagamento;
                                finanTituloItem.dtLancamento = cxl.dthr;
                                finanTituloItem.dtPagamento = cxl.dtPagamento;
                                finanTituloItem.valorCobrado = cxl.valorCobrado;
                                finanTituloItem.parcela = parcela;
                                finanTituloItem.situacao = ETituloSituacao.em_aberto;
                                finanTituloItem.identificador = defineNumeroTituloItem(idFinanTitulo.ToString(), parcela.ToString());

                                valorTotalTitulo += cxl.valorCobrado;
                                parcela++;

                                dbStore(db, finanTituloItem);
                                existeContaReceber = true;
                            }

                            Utils.filtraCampos(cxl);
                            dbStore(db, cxl);
                        }

                        if (existeContaReceber)
                        {
                            Finan_Titulo finanTitulo = new Finan_Titulo();
                            finanTitulo.id = idFinanTitulo;
                            finanTitulo.idOperacao = cxLPadrao.idOperacao;
                            finanTitulo.idTransacao = cxLPadrao.idTransacao;
                            finanTitulo.idClienteAPagar = cxLPadrao.idClientePagador;
                            finanTitulo.dtLancamento = cxLPadrao.dthr;
                            finanTitulo.valorCobrado = valorTotalTitulo;
                            finanTitulo.valorOriginal = valorTotalTitulo;
                            finanTitulo.grupoTipoPagamento_nome = cxLPadrao.grupoTipoPagamento_nome;
                            finanTitulo.tipoDocumento_nome = cxLPadrao.tipoPagamento_nome;
                            finanTitulo.tipoPagamento_geraContasPagar = cxLPadrao.tipoPagamento_geraContasPagar;
                            finanTitulo.tipoPagamento_geraContasReceber = cxLPadrao.tipoPagamento_geraContasReceber;
                            finanTitulo.tipo = ETipoTitulo.titulo_a_receber;
                            finanTitulo.situacao = ETituloSituacao.em_aberto;
                            finanTitulo.identificador = defineNumeroTitulo(finanTitulo.id.ToString());
                            dbStore(db, finanTitulo);
                        }
                    }
                    else if (mov.tipo == EMovTipo.outros_orcamento)
                    {
                        for (int i = 0; i < valores.Count; i++)
                        {
                            Cx_Lancamento cxl = valores[i];
                            Orcamento_Lancamento orcamento = new Orcamento_Lancamento();
                            orcamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Orcamento_Lancamento), 0);
                            orcamento.dthr = Utils.getAgoraString();
                            orcamento.dtPagamento = cxl.dtPagamento;
                            orcamento.idTransacao = idTransacao;
                            orcamento.idOperacao = idOperacao;
                            orcamento.idClientePagador = cxl.idClientePagador;
                            orcamento.idClienteRecebedor = cxl.idClienteRecebedor;
                            orcamento.idGrupoTipoPagamento = cxl.idGrupoTipoPagamento;
                            orcamento.idMovMapa = cxl.idMovMapa;
                            orcamento.idOperacao = cxl.idOperacao;
                            orcamento.idPortador = cxl.idPortador;
                            orcamento.idTipoPagamento = cxl.idTipoPagamento;
                            orcamento.nome = cxl.nome;
                            orcamento.observacoes = cxl.observacoes;
                            orcamento.tipoLancamentoDoCaixa = cxl.tipoLancamentoDoCaixa;
                            orcamento.tipoPagamento_nome = cxl.tipoPagamento_nome;
                            orcamento.tipoPagamento_pctComissao = cxl.tipoPagamento_pctComissao;
                            orcamento.txJuroAtraso = cxl.txJuroAtraso;
                            orcamento.txJuroParcelamento = cxl.txJuroParcelamento;
                            orcamento.txMultaAtraso = cxl.txMultaAtraso;
                            orcamento.valorCobrado = cxl.valorCobrado;
                            orcamento.valorOriginal = cxl.valorOriginal;
                            orcamento.valorRecebido = cxl.valorRecebido;
                            dbStore(db, orcamento);
                        }
                    }



                    //Calcula Comissão
                    if (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido)
                    {
                        IQuery query;
                        if (mov.idClienteFuncionarioVendedor != 1)
                        {
                            Cliente cliente = null;
                            query = db.Query();
                            query.Constrain(typeof(Cliente));
                            query.Descend("id").Constrain(mov.idClienteFuncionarioVendedor);
                            if (query.Execute().Count == 0)
                                throw new Exception("Funcionário não encontrado. ID: " + mov.idClienteFuncionarioVendedor);
                            else
                                cliente = query.Execute()[0] as Cliente;

                            /*
                            double baseCalculoComissaoMontanteTotal = 0;
                            double baseCalculoComissaoMaoDeObra = 0;
                            double baseCalculoComissaoMaoDeObraGarantia = 0;
                            double baseCalculoComissaoProdutosEmGarantia = 0;
                            double baseCalculoComissaoProdutos = 0;
                             * */

                            foreach (MovItem mi in carrinho)
                            {
                                Item item = null;
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(mi.idItem);
                                if (query.Execute().Count == 0)
                                    throw new Exception("Item não encontrado. ID:" + mi.idItem);
                                else
                                    item = query.Execute()[0] as Item;



                                /*
                                if (item.tipo == EItemTipo.produto)
                                {
                                    baseCalculoComissaoMontanteTotal += mi.vlrUnitVendaFinalQtd;
                                    baseCalculoComissaoProdutosEmGarantia += mi.vlrUnitVendaFinalQtd;
                                    baseCalculoComissaoProdutos += mi.vlrUnitVendaFinalQtd;
                                }
                                else if (item.tipo == EItemTipo.servico)
                                {
                                    baseCalculoComissaoMontanteTotal += mi.vlrUnitVendaFinalQtd;
                                    baseCalculoComissaoMaoDeObra += mi.vlrUnitVendaFinalQtd;
                                    baseCalculoComissaoMaoDeObraGarantia += mi.vlrUnitVendaFinalQtd;
                                }
                                 * */
                            }

                            mov.comissaoFucionarioMontanteTotal = cliente.comissaoMontanteTotal;
                            mov.comissaoFucionarioMaoDeObra = cliente.comissaoMaoDeObra;
                            mov.comissaoFucionarioMaoDeObraGarantia = cliente.comissaoMaoDeObraGarantia;
                            mov.comissaoFucionarioProdutosEmGarantia = cliente.comissaoProdutosEmGarantia;
                            mov.comissaoFucionarioProdutos = cliente.comissaoProdutos;

                            /*
                            mov.comissaoFucionarioMontanteTotal = (baseCalculoComissaoMontanteTotal * cliente.comissaoMontanteTotal) / 100;
                            mov.comissaoFucionarioMaoDeObra = (baseCalculoComissaoMaoDeObra * cliente.comissaoMaoDeObra) / 100;
                            mov.comissaoFucionarioMaoDeObraGarantia = (baseCalculoComissaoMaoDeObraGarantia * cliente.comissaoMaoDeObraGarantia) / 100;
                            mov.comissaoFucionarioProdutosEmGarantia = (baseCalculoComissaoProdutosEmGarantia * cliente.comissaoProdutosEmGarantia) / 100;
                            mov.comissaoFucionarioProdutos = (baseCalculoComissaoProdutos * cliente.comissaoProdutos) / 100;
                             * */
                        }
                    }

                    if (ordemServico != null && (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido))
                    {
                        IQuery query = db.Query();
                        query.Constrain(typeof(OrdemServico));
                        query.Descend("id").Constrain(ordemServico.id);
                        IObjectSet rs_ordemServico = query.Execute();

                        OrdemServico ordemServico_db = rs_ordemServico[0] as OrdemServico;
                        ordemServico_db.idMovFinalizacao = mov.id;
                        ordemServico_db.status = EOrdemServicoStatus.finalizada;

                        dbStore(db, ordemServico_db);
                    }



                    Utils.filtraCampos(mov);
                    dbStore(db, mov);




                    /*
                    List<Finan_Titulo> titulosGeramContasReceb = new List<Finan_Titulo>();
                    List<Finan_Titulo> outrasFormasPgto = new List<Finan_Titulo>();

                    foreach (Finan_Titulo t in parcelas)
                    {
                        if (t.valorRecebido > 0)//não vai gerar um titulo
                        {
                            outrasFormasPgto.Add(t);
                        }
                        else
                        {
                            titulosGeramContasReceb.Add(t);
                            //gerar t.idPortador
                        }
                    }
                    */

                    //t.idEmp = idEmp;
                    //Utils.filtraCampos(t);
                    //dbStore(db, t);
                    dbCommit(db);
                    //Console.Beep(2000, 500);
                    return idMov;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //Console.Beep(200, 500);
                    throw;
                    //return 0;
                }
            }
        }

        public int Mov_Salva_EntradaRetorno(
            int idCorp, int idEmp, int idClienteFuncionarioLogado,
            Mov mov,
            List<MovItem> carrinho,
            List<Cx_Lancamento> valores)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    ReaproveitamentoCodigo r = AppFacade.get.reaproveitamento;

                    int idTransacao = r.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    int idOperacao = r.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);

                    int idMov = r.getIncremento(db, typeof(Mov), 0);
                    int idMovItem = r.getIncremento(db, typeof(MovItem), carrinho.Count);

                    mov.idOperacao = idOperacao;
                    mov.idTransacao = idTransacao;
                    mov.id = idMov;
                    mov.idEmp = idEmp;
                    mov.dthrMovEmissao = Utils.getAgoraString();

                    mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;

                    for (int i = 0; i < carrinho.Count; i++)
                    {
                        MovItem mi = carrinho[i];

                        mi.idClienteFuncionario = idClienteFuncionarioLogado;
                        mi.idMov = idMov;
                        mi.id = idMovItem++;
                        mi.idOperacao = idOperacao;
                        mi.idTransacao = idTransacao;

                        IQuery query = db.Query();
                        query.Constrain(typeof(Item));
                        query.Descend("id").Constrain(mi.idItem);

                        if ((query.Execute()[0] as Item).tipo == EItemTipo.produto)
                        {
                            ItemEmpEstoque iee = r.Item_LoadEstoque(db, mi.idIEE, false);
                            iee.qtd += mi.qtd;
                            iee.qtd = Utils.formataQuantidade(iee.qtd);
                            dbStore(db, iee);
                        }

                        Utils.filtraCampos(mi);
                        dbStore(db, mi);
                    }
                    dbStore(db, mov);
                    dbCommit(db);
                    return idMov;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //Console.Beep(200, 500);
                    throw;
                    //return 0;
                }
            }
        }


        public void Mov_Cancela(int idCorp, int idEmp, int idMov, int idClienteFuncionarioLogado)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    Mov mov = new Mov();
                    List<MovItem> listaMovItem;

                    //busca movimentação a cancelar
                    query = db.Query();
                    query.Constrain(typeof(Mov));
                    query.Descend("id").Constrain(idMov);
                    if (query.Execute().Count > 0)
                        mov = query.Execute()[0] as Mov;
                    else
                        throw new Exception("Movimentação não encontrada");

                    //busca itens ta movimentação
                    listaMovItem = AppFacade.get.reaproveitamento.movItem_List(db, mov.id);

                    //movimentação de cancelamento
                    Mov movCanceladora = new Mov();
                    Utils.copiaCamposBasicos(mov, movCanceladora);
                    movCanceladora.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    movCanceladora.dthrMovEmissao = Utils.getAgoraString();
                    movCanceladora.dtLancaCaixa = Utils.getAgoraString();
                    movCanceladora.dtMovTicks = Utils.getAgoraTicks();
                    movCanceladora.dtLancTicks = Utils.getAgoraTicks();

                    movCanceladora.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                    movCanceladora.idMovCancelada = mov.id;
                    mov.idMovCanceladora = movCanceladora.id;

                    //copia movitens para movimentação canceladora (pode-se usar a mesma lista?)

                    MovItem miCancelador = new MovItem();
                    switch (mov.resumo)
                    {
                        case EMovResumo.entrada:

                            mov.tipo = EMovTipo.entrada_cancel;

                            movCanceladora.resumo = EMovResumo.entrada;
                            movCanceladora.tipo = EMovTipo.entrada_cancel;

                            foreach (MovItem mi in listaMovItem)
                            {
                                miCancelador = new MovItem();
                                Utils.copiaCamposBasicos(mi, miCancelador);
                                miCancelador.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                                miCancelador.idMov = movCanceladora.id;
                                miCancelador.qtd = -mi.qtd;

                                ItemEmpEstoque iee;
                                query = db.Query();
                                query.Constrain(typeof(ItemEmpEstoque));
                                query.Descend("id").Constrain(miCancelador.idIEE);
                                if (query.Execute().Count > 0)
                                    iee = query.Execute()[0] as ItemEmpEstoque;
                                else
                                    throw new Exception("IEE não pode ser encontrado. id: " + miCancelador.idIEE);
                                iee.qtd -= mi.qtd;

                                dbStore(db, miCancelador, iee);
                            }
                            break;
                        case EMovResumo.saida:

                            movCanceladora.resumo = EMovResumo.saida;
                            movCanceladora.tipo = EMovTipo.saida_cancel;

                            foreach (MovItem mi in listaMovItem)
                            {
                                miCancelador = new MovItem();
                                Utils.copiaCamposBasicos(mi, miCancelador);
                                miCancelador.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                                miCancelador.idMov = movCanceladora.id;
                                miCancelador.qtd = mi.qtd;

                                ItemEmpEstoque iee;
                                query = db.Query();
                                query.Constrain(typeof(ItemEmpEstoque));
                                query.Descend("id").Constrain(miCancelador.idIEE);
                                if (query.Execute().Count > 0)
                                    iee = query.Execute()[0] as ItemEmpEstoque;
                                else
                                    throw new Exception("IEE não pode ser encontrado. id: " + miCancelador.idIEE);
                                iee.qtd += mi.qtd;

                                dbStore(db, miCancelador, iee);
                            }
                            break;
                        case EMovResumo.outros:

                            movCanceladora.resumo = EMovResumo.outros;

                            if (mov.tipo == EMovTipo.outros_reserva)
                            {
                                movCanceladora.tipo = EMovTipo.outros_cancel;
                                movCanceladora.isReservaDevolvida = true;

                                foreach (MovItem mi in listaMovItem)
                                {
                                    miCancelador = new MovItem();
                                    Utils.copiaCamposBasicos(mi, miCancelador);
                                    miCancelador.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                                    miCancelador.idMov = movCanceladora.id;
                                    miCancelador.qtd = -mi.qtd;

                                    ItemEmpEstoque iee;
                                    query = db.Query();
                                    query.Constrain(typeof(ItemEmpEstoque));
                                    query.Descend("id").Constrain(miCancelador.idIEE);
                                    if (query.Execute().Count > 0)
                                        iee = query.Execute()[0] as ItemEmpEstoque;
                                    else
                                        throw new Exception("IEE não pode ser encontrado. id: " + miCancelador.idIEE);
                                    iee.qtd += mi.qtd;

                                    dbStore(db, miCancelador, iee);
                                }
                            }
                            break;
                        case EMovResumo.ambos:
                            mov.resumo = EMovResumo.ambos;
                            if (mov.tipo == EMovTipo.ambos_balan)
                            {
                                movCanceladora.tipo = EMovTipo.ambos_cancel;
                                movCanceladora.isReservaDevolvida = true;

                                foreach (MovItem mi in listaMovItem)
                                {
                                    miCancelador = new MovItem();
                                    Utils.copiaCamposBasicos(mi, miCancelador);
                                    miCancelador.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                                    miCancelador.idMov = movCanceladora.id;
                                    miCancelador.qtd = -mi.qtd;

                                    ItemEmpEstoque iee;
                                    query = db.Query();
                                    query.Constrain(typeof(ItemEmpEstoque));
                                    query.Descend("id").Constrain(miCancelador.idIEE);
                                    if (query.Execute().Count > 0)
                                        iee = query.Execute()[0] as ItemEmpEstoque;
                                    else
                                        throw new Exception("IEE não pode ser encontrado. id: " + miCancelador.idIEE);
                                    iee.qtd += mi.qtd;

                                    dbStore(db, miCancelador, iee);
                                }
                            }
                            break;
                    }
                    dbStore(db, mov, movCanceladora);

                    //rotina de retirada do saldo de entrada do caixa
                    //temporário para funcionamento correto do saldo de caixa
                    if (mov.dthrMovEmissao.Trim() != String.Empty)
                    {
                        if (mov.dthrMovEmissao.Substring(0, 10) == Utils.getAgoraString().Substring(0, 10))
                        {
                            query = db.Query();
                            query.Constrain(typeof(Cx_Lancamento));
                            query.Descend("idEmp").Constrain(idEmp);
                            query.Descend("idOperacao").Constrain(mov.idOperacao);
                            IObjectSet rs_listaCxLancamento = query.Execute();
                            foreach (Cx_Lancamento cxLancamento in rs_listaCxLancamento)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Finan_TipoPagamento));
                                query.Descend("id").Constrain(cxLancamento.idTipoPagamento);
                                IObjectSet rs_finanTipoPagamento = query.Execute();
                                Finan_TipoPagamento finanTipoPagamento = rs_finanTipoPagamento[0] as Finan_TipoPagamento;

                                if (!finanTipoPagamento.ehPrazo && (finanTipoPagamento.nome == "A PRAZO" || finanTipoPagamento.nome == ""))
                                {
                                    query = db.Query();
                                    query.Constrain(typeof(Cx_Diario));
                                    query.Descend("idEmp").Constrain(idEmp);
                                    query.Descend("data").Constrain(Utils.getAgoraString().Substring(0, 10));
                                    IObjectSet rs_cxDiario = query.Execute();
                                    Cx_Diario cxDiario = rs_cxDiario[0] as Cx_Diario;

                                    cxDiario.totalEntradas -= cxLancamento.valorOriginal;
                                    dbStore(db, cxDiario);
                                }
                            }
                        }
                    }

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        /*
        public void Mov_Salva(int idCorp, int idMov, MovNFE mov_nf)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;
                    
                    query = db.Query();
                    query.Constrain(typeof(Mov));
                    query.Descend("id").Constrain(mov_nf.idMov);
                    Mov mov = query.Execute()[0] as Mov;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }
         * */

        #endregion

        /*
        public Item Item_Novo()
        {
            return null;
        }
        */


        #region Ajuste de Estoque

        public void RealizaAjusteEstoque(int idCorp, int idEmp, Mov mov)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                    mov.dthrMovEmissao = Utils.getAgoraString();
                    mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;
                    dbStore(db, mov);

                    foreach (MovItem mi in mov.__mItens)
                    {
                        IQuery query = db.Query();
                        query.Constrain(typeof(ItemEmpEstoque));
                        query.Descend("id").Constrain(mi.idIEE);
                        ItemEmpEstoque iee = query.Execute()[0] as ItemEmpEstoque;
                        iee.qtd += mi.qtd;

                        mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                        mi.idMov = mov.id;

                        dbStore(db, iee, mi);
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        #endregion

        public void Cad_Secao_Atualizacoes(int idCorp, List<Cad_Secao> listaAtualizacao)
        {
            defineCorp(idCorp);
            IQuery query;
            foreach (Cad_Secao cadSecao in listaAtualizacao)
            {
                query = db.Query();
                query.Constrain(typeof(Cad_Secao));
                query.Descend("id").Constrain(cadSecao.id);
                IObjectSet rs_cadSecao = query.Execute();
                Cad_Secao cadSecao_db = rs_cadSecao[0] as Cad_Secao;
                Utils.copiaCamposBasicos(cadSecao, cadSecao_db);
                Utils.filtraCampos(cadSecao_db);
                dbStore(db, cadSecao_db);
            }
            dbCommit(db);
        }

        public void GeraCarne(int idCorp, int idMov)
        {
            ConstrutorPDF.GeraCarne(idCorp, idMov);
        }         
    }
}