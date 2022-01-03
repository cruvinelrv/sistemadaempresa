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
    public partial class NuvemModificacoes
    {

        #region FINANCEIRO

        public List<Finan_Portador> Finan_Portador_Novos(int idCorp, List<Finan_Portador> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_Portador xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Portador), 0);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        public List<Finan_PortadorTipo> Finan_PortadorTipo_Novos(int idCorp, List<Finan_PortadorTipo> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_PortadorTipo xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_PortadorTipo), 0);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        public List<Finan_CentroCusto> Finan_CentroCusto_Novos(int idCorp, List<Finan_CentroCusto> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_CentroCusto fcc in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        fcc.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_CentroCusto), 0);
                        //salva
                        dbStore(db, fcc);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        public void Finan_GrupoTipoPagamento_Novos(int idCorp, List<Finan_GrupoTipoPagamento> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_GrupoTipoPagamento xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_GrupoTipoPagamento), 0);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    //return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //return null;
                }
            }
        }
        public List<Finan_TipoDocumento> Finan_TipoDocumento_Novos(int idCorp, List<Finan_TipoDocumento> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_TipoDocumento xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TipoDocumento), 0);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        /*
        public List<Finan_PlanoContaTipo> Finan_PlanoContaTipo_Novos(int idCorp, List<Finan_PlanoContaTipo> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_PlanoContaTipo xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_PlanoContaTipo), 0);
                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        */

        public void Finan_TipoLancamento_Novos(int idCorp, List<Finan_TipoLancamento> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_TipoLancamento xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TipoLancamento), 0);

                        //é uma conta-grupo?
                        if (xxx.codigoGrupoLancamento == 0)
                        {
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_TipoLancamento));//FROM Finan_TipoLancamento
                            q.Descend("codigoGrupoLancamento").OrderDescending();//ORDER BY codigoGrupoLancamento DESC
                            IObjectSet r = q.Execute();
                            if (r.Count == 0)
                                xxx.codigoGrupoLancamento = 1;
                            else
                            {
                                Finan_TipoLancamento maior = (Finan_TipoLancamento)r[0];//TOP 1
                                xxx.codigoGrupoLancamento = maior.codigoGrupoLancamento + 1;
                            }
                        }
                        else
                        {
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_TipoLancamento));//FROM Finan_TipoLancamento
                            q.Descend("codigoGrupoLancamento").Constrain(xxx.codigoGrupoLancamento).Equal();//WHERE codigoGrupoLancamento == 3
                            q.Descend("codigoTipoLancamento").OrderDescending();//ORDER BY codConta DESC
                            IObjectSet r = q.Execute();
                            if (r.Count == 0)
                                xxx.codigoTipoLancamento = 1;
                            else
                            {
                                Finan_TipoLancamento maior = (Finan_TipoLancamento)r[0];//TOP 1
                                xxx.codigoTipoLancamento = maior.codigoTipoLancamento + 1;
                            }
                        }

                        xxx.codigo = String.Format("{0:000}.{1:000}", xxx.codigoGrupoLancamento, xxx.codigoTipoLancamento);

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
        /*
        public List<Finan_PlanoConta> Finan_PlanoConta_Novos(int idCorp, List<Finan_PlanoConta> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_PlanoConta xxx in objetos)
                    {
                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_PlanoConta), 0);

                        //é uma conta-grupo?
                        if (xxx.codContaPai == 0)
                        {
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_PlanoConta));//FROM Finan_PlanoConta
                            q.Descend("codContaPai").OrderDescending();//ORDER BY codContaPai DESC
                            IObjectSet r = q.Execute();
                            if (r.Count == 0)
                                xxx.codContaPai = 1;
                            else
                            {
                                Finan_PlanoConta maior = (Finan_PlanoConta)r[0];//TOP 1
                                xxx.codContaPai = maior.codContaPai + 1;
                            }
                        }
                        else
                        {
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_PlanoConta));//FROM Finan_PlanoConta
                            q.Descend("codContaPai").Constrain(xxx.codContaPai).Equal();//WHERE codContaPai == 3
                            q.Descend("codConta").OrderDescending();//ORDER BY codConta DESC
                            IObjectSet r = q.Execute();
                            if (r.Count == 0)
                                xxx.codConta = 1;
                            else
                            {
                                Finan_PlanoConta maior = (Finan_PlanoConta)r[0];//TOP 1
                                xxx.codConta = maior.codConta + 1;
                            }
                        }

                        xxx.cod = String.Format("{0:000}.{1:000}.{2:00}", xxx.codContaPai, xxx.codConta, xxx.idTipoPlanoConta);

                        //salva
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        */
        public List<Finan_Conta> Finan_Conta_NovosAtualizacoes(int idCorp, int idEmp, int idClienteFuncionarioLogado, List<Finan_Conta> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_Conta xxx in objetos)
                    {
                        if (xxx.id==0)
                        {
                            //se trata de uma instancia NOVA
                            //ProximoCampo prox = new ProximoCampo(idCorp);
                            xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Conta), 0);
                            //salva
                            dbStore(db, xxx);
                        }
                        else
                        {
                            //estou alterando uma instancia
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_Conta));//FROM Finan_Conta
                            q.Descend("id").Constrain(xxx.id);//WHERE id = 3
                            IObjectSet r = q.Execute();
                            Finan_Conta finC = (Finan_Conta)r[0];

                            if (finC.saldoInicial != xxx.saldoInicial && finC.dtSaldoInicial != xxx.dtSaldoInicial)
                                Utils.copiaCamposBasicos(Finan_Conta_SaldoInicial(db, idEmp, idClienteFuncionarioLogado, xxx), finC);
                            else
                                Utils.copiaCamposBasicos(xxx, finC);
                            //salva
                            dbStore(db, finC);
                        }
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
        private Finan_Conta Finan_Conta_SaldoInicial(IObjectContainer db, int idEmp, int idClienteFuncionarioLogado, Finan_Conta finanConta)
        {
            int idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
            int idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);

            Finan_Lancamento lancamento = new Finan_Lancamento();
            lancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
            lancamento.idTransacao = idTransacao;
            lancamento.idOperacao = idOperacao; lancamento.idEmp = idEmp;
            lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
            lancamento.idContaDestino = finanConta.id;

            lancamento.dtFluxoCaixa = finanConta.dtSaldoInicial;
            lancamento.dtLancamento = Utils.getAgoraString().Substring(0, 10);
            lancamento.valorBruto = finanConta.saldoInicial;
            lancamento.valorLancado = finanConta.saldoInicial;

            lancamento.isCredito = true;

            Finan_TipoLancamento finanTipoLancamento = null;
            IQuery query = db.Query();
            query.Constrain(typeof(Finan_TipoLancamento));
            query.Descend("nomeTipoLancamento").Constrain("SALDO INICIAL");
            IObjectSet setter = query.Execute();
            if (setter.Count > 0)
                finanTipoLancamento = setter[0] as Finan_TipoLancamento;
            else
                throw new Exception("Nome do tipo de lançamento não encontrado\nDeve existir o tipo 'SALDO INICIAL'");
            lancamento.idTipoLancamento = finanTipoLancamento.id;

            Finan_Conta contaDestino = null;
            foreach (Finan_Conta conta in db.Query<Finan_Conta>())
            {
                if (conta.id != lancamento.idContaDestino)
                    continue;
                contaDestino = conta;
                break;
            }
            Utils.copiaCamposBasicos(finanConta, contaDestino);

            contaDestino.saldoAtual = Math.Round(contaDestino.saldoAtual, 2);

            lancamento.saldoAnterior = contaDestino.saldoAtual;
            contaDestino.saldoAnterior = contaDestino.saldoAtual;
            contaDestino.saldoAtual += lancamento.valorLancado;
            lancamento.saldoAtual = contaDestino.saldoAtual;

            dbStore(db, lancamento);
            return contaDestino;
        }

        //List<Finan_TipoPagamento>
        public void Finan_TipoPagamento_NovosAtualizacoes(int idCorp, List<Finan_TipoPagamento> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_TipoPagamento xxx in objetos)
                    {
                        if (xxx.id == 0)
                        {
                            //se trata de uma instancia NOVA
                            //ProximoCampo prox = new ProximoCampo(idCorp);
                            xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TipoPagamento), 0);
                            //salva
                            dbStore(db, xxx);
                        }
                        else
                        {
                            //estou alterando uma instancia
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(Finan_TipoPagamento));//FROM Finan_TipoPagamento
                            q.Descend("id").Constrain(xxx.id);//WHERE id = 3
                            IObjectSet r = q.Execute();
                            Finan_TipoPagamento finTP = (Finan_TipoPagamento)r[0];
                            Utils.copiaCamposBasicos(xxx, finTP);
                            //salva
                            dbStore(db, finTP);
                        }
                    }
                    dbCommit(db);
                    //return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //return null;
                }
            }
        }

        public void Finan_LancaCapitalTotal(int idCorp, int idEmp, int idClienteFuncionarioLogado, double valor)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //
                    /*
                    int idContaCapitalTotal = -1;
                    foreach (SdeConfig s in db.Query<SdeConfig>())
                        if (s.variavel == Variaveis_SdeConfig.FINANCEIRO_CONTAS_CAPITALTOTAL)
                        {
                            idContaCapitalTotal = int.Parse(s.valor);
                            break;
                        }
                    //
                    Finan_Conta contaCapitalTotal = null;
                    foreach (Finan_Conta c in db.Query<Finan_Conta>())
                        if (c.id == idContaCapitalTotal)
                            contaCapitalTotal = c;
                    //
                     * */
                    Finan_Conta contaCapitalTotal = null;
                    foreach (Finan_Conta c in db.Query<Finan_Conta>())
                        if (c.isCapitalTotal)
                            contaCapitalTotal = c;
                    //
                    contaCapitalTotal.saldoAnterior = contaCapitalTotal.saldoAtual;
                    contaCapitalTotal.saldoAtual += valor;
                    dbStore(db, contaCapitalTotal);
                    //
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }


        public void Finan_Lancamento_Transferencia(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Lancamento lancamento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    lancamento.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    lancamento.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                    lancamento.idEmp = idEmp;
                    lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    lancamento.valorLancado = Math.Round(lancamento.valorLancado, 2);
                    lancamento.dtLancamento = Utils.getAgoraString().Substring(0, 10);

                    Finan_Lancamento lancaDebito = new Finan_Lancamento();
                    Finan_Lancamento lancaCredito = new Finan_Lancamento();
                    Utils.copiaCamposBasicos(lancamento, lancaDebito);
                    Utils.copiaCamposBasicos(lancamento, lancaCredito);

                    //o destino de um é a origem do outro, e os valores são inversos
                    lancaDebito.idContaDestino = lancaCredito.idContaOrigem;
                    lancaDebito.idContaOrigem = lancaCredito.idContaDestino;
                    lancaDebito.valorLancado = -lancaCredito.valorLancado;

                    lancaDebito.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 1);
                    lancaCredito.id = lancaDebito.id + 1;

                    Finan_Conta contaDebitar = null, contaCreditar = null;
                    foreach (Finan_Conta c in db.Query<Finan_Conta>())
                    {
                        if (c.id == lancaCredito.idContaOrigem)
                            contaDebitar = c;
                        else if (c.id == lancaCredito.idContaDestino)
                            contaCreditar = c;
                    }

                    contaDebitar.saldoAtual = Math.Round(contaDebitar.saldoAtual, 2);
                    contaCreditar.saldoAtual = Math.Round(contaCreditar.saldoAtual, 2);

                    //contaDebitar
                    lancaDebito.isCredito = false;
                    lancaDebito.saldoAnterior = contaDebitar.saldoAtual;
                    contaDebitar.saldoAnterior = contaDebitar.saldoAtual;
                    contaDebitar.saldoAtual += lancaDebito.valorLancado;
                    lancaDebito.saldoAtual = contaDebitar.saldoAtual;

                    //contaCreditar
                    lancaCredito.isCredito = true;
                    lancaCredito.saldoAnterior = contaCreditar.saldoAtual;
                    contaCreditar.saldoAnterior = contaCreditar.saldoAtual;
                    contaCreditar.saldoAtual += lancaCredito.valorLancado;
                    lancaCredito.saldoAtual = contaCreditar.saldoAtual;



                    dbStore(db, contaDebitar, contaCreditar, lancaCredito, lancaDebito);

                    Console.Beep();
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //return null;
                }
            }
        }

        public void Finan_lancamento_DebitoCreditoVista(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Lancamento lancamento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    lancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                    if (lancamento.idFinan_LancamentoOrigem == 0)
                    {
                        lancamento.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                        lancamento.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                    }
                    lancamento.idEmp = idEmp;
                    lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    lancamento.valorLancado = Math.Round(lancamento.valorLancado, 2);

                    Finan_Conta contaLancamento = null;
                    foreach (Finan_Conta conta in db.Query<Finan_Conta>())
                    {
                        if (conta.id == lancamento.idContaDestino)
                        {
                            contaLancamento = conta;
                            continue;
                        }
                    }

                    contaLancamento.saldoAtual = Math.Round(contaLancamento.saldoAtual, 2);

                    lancamento.saldoAnterior = contaLancamento.saldoAtual;
                    contaLancamento.saldoAnterior = contaLancamento.saldoAtual;
                    contaLancamento.saldoAtual += lancamento.valorLancado;
                    lancamento.saldoAtual = contaLancamento.saldoAtual;

                    dbStore(db, lancamento, contaLancamento);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_lancamento_RecebimentoRota(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Lancamento lancamento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    lancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                    if (lancamento.idFinan_LancamentoOrigem == 0)
                    lancamento.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    lancamento.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                    lancamento.idEmp = idEmp;
                    lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    lancamento.valorLancado = Math.Round(lancamento.valorLancado, 2);

                    Finan_Conta contaLancamento = null;
                    foreach (Finan_Conta conta in db.Query<Finan_Conta>())
                    {
                        if (conta.id == lancamento.idContaDestino)
                        {
                            contaLancamento = conta;
                            continue;
                        }
                    }

                    contaLancamento.saldoAtual = Math.Round(contaLancamento.saldoAtual, 2);

                    lancamento.saldoAnterior = contaLancamento.saldoAtual;
                    contaLancamento.saldoAnterior = contaLancamento.saldoAtual;
                    contaLancamento.saldoAtual += lancamento.valorLancado;
                    lancamento.saldoAtual = contaLancamento.saldoAtual;

                    //salva comissão
                    Finan_Lancamento lancamentoComissao = new Finan_Lancamento();
                    Utils.copiaCamposBasicos(lancamento, lancamentoComissao);
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_TipoLancamento));
                    query.Descend("nomeTipoLancamento").Constrain("COMISSAO");
                    if (query.Execute().Count > 0)
                        lancamentoComissao.idTipoLancamento = (query.Execute()[0] as Finan_TipoLancamento).id;
                    else
                        throw new Exception("Nome do tipo de lançamento não encontrado\nDeve existir o tipo 'COMISSAO'");
                    lancamentoComissao.isCredito = false;
                    lancamentoComissao.idFinan_LancamentoOrigem = lancamento.id;
                    lancamentoComissao.valorLancado = -lancamento.valorComissao;
                    Finan_lancamento_DebitoCreditoVista(idCorp, idEmp, idClienteFuncionarioLogado, lancamentoComissao);

                    //salva despesa
                    Finan_Lancamento lancamentoDespesa = new Finan_Lancamento();
                    Utils.copiaCamposBasicos(lancamento, lancamentoDespesa);
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoLancamento));
                    query.Descend("nomeTipoLancamento").Constrain("DESPESAS DE VIAGEM");
                    if (query.Execute().Count > 0)
                        lancamentoDespesa.idTipoLancamento = (query.Execute()[0] as Finan_TipoLancamento).id;
                    else
                        throw new Exception("Nome do tipo de lançamento não encontrado\nDeve existir o tipo 'DESPESAS DE VIAGEM'");
                    lancamentoDespesa.isCredito = false;
                    lancamentoDespesa.idFinan_LancamentoOrigem = lancamento.id;
                    lancamentoDespesa.valorLancado = -lancamento.valorDespesas;
                    Finan_lancamento_DebitoCreditoVista(idCorp, idEmp, idClienteFuncionarioLogado, lancamentoDespesa);

                    dbStore(db, lancamento, contaLancamento);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_ChequeNovo(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Titulo finanTitulo, Cliente cliente)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (cliente.id == 0)
                    {
                        cliente.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
                        dbStore(db, cliente);
                    }
                    if (finanTitulo.idFornecedorCheque == 0)
                        finanTitulo.idFornecedorCheque = cliente.id;
                    finanTitulo.idClienteAPagar = cliente.id;

                    Finan_TipoDocumento finanTipoDocumento = null;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_TipoDocumento));
                    query.Descend("nome").Constrain("CHEQUE");
                    if (query.Execute().Count > 0)
                        finanTipoDocumento = query.Execute()[0] as Finan_TipoDocumento;
                    else
                        throw new Exception("Tipo de Documento 'CHEQUE' não encontrado, cadastre o tipo para poder lançar cheques");

                    finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);
                    finanTitulo.idTipoDocumento = finanTipoDocumento.id;
                    finanTitulo.tipo = ETipoTitulo.cheque_a_receber;
                    if (finanTitulo.idTransacao == 0)
                        finanTitulo.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    if (finanTitulo.idOperacao == 0)
                        finanTitulo.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                    dbStore(db, finanTitulo);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_ChequeAltera(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Titulo finanTitulo, Cliente cliente)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (cliente.id == 0)
                    {
                        cliente.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
                        dbStore(db, cliente);
                    }
                    if (finanTitulo.idFornecedorCheque == 0)
                        finanTitulo.idFornecedorCheque = cliente.id;
                    finanTitulo.idClienteAPagar = cliente.id;

                    Finan_Titulo finanTituloAlterado;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_Titulo));
                    query.Descend("id").Constrain(finanTitulo.id);
                    finanTituloAlterado = query.Execute()[0] as Finan_Titulo;
                    finanTituloAlterado.isAlterado = true;
                    dbStore(db, finanTituloAlterado);

                    finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);
                    finanTitulo.idTituloOrigem = finanTituloAlterado.id;
                    finanTitulo.idTransacao = finanTituloAlterado.idTransacao;
                    finanTitulo.idOperacao = finanTituloAlterado.idOperacao;
                    dbStore(db, finanTitulo);

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_ChequeBaixa(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Titulo finanTitulo, Finan_Conta finanConta, Cliente clieteDestino)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    string obs = finanTitulo.obs;
                    string dtBaixa = finanTitulo.dtBaixa;

                    Finan_Titulo finanTituloAlterado;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_Titulo));
                    query.Descend("id").Constrain(finanTitulo.id);
                    finanTituloAlterado = query.Execute()[0] as Finan_Titulo;

                    finanTituloAlterado.isAlterado = true;
                    dbStore(db, finanTituloAlterado);

                    Utils.copiaCamposBasicos(finanTituloAlterado, finanTitulo);
                    finanTitulo.obs = obs;
                    finanTitulo.dtBaixa = dtBaixa;
                    finanTitulo.isAlterado = false;

                    finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);
                    finanTitulo.idTituloOrigem = finanTituloAlterado.id;
                    finanTitulo.isBaixado = true;

                    if (finanConta != null)
                    {
                        finanTitulo.idContaDestino = finanConta.id;
                        if (finanConta.tipo == EContaTipo.Caixa)
                        {
                            finanTitulo.dtPagamento = finanTitulo.dtBaixa;

                            Finan_Lancamento lancamento = new Finan_Lancamento();
                            lancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                            lancamento.idTransacao = finanTitulo.idTransacao;
                            lancamento.idOperacao = finanTitulo.idOperacao;
                            lancamento.idEmp = idEmp;
                            lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                            lancamento.idContaDestino = finanConta.id;

                            lancamento.dtFluxoCaixa = Utils.getAgoraString().Substring(0, 10);
                            lancamento.dtFluxoCaixa = finanTitulo.dtBaixa;
                            lancamento.valorBruto = finanTitulo.valorCobrado;
                            lancamento.valorLancado = finanTitulo.valorCobrado;
                            lancamento.valorLancado = Math.Round(lancamento.valorLancado, 2);

                            Finan_TipoPagamento finanTipoPagamento = null;
                            query = db.Query();
                            query.Constrain(typeof(Finan_TipoPagamento));
                            query.Descend("nome").Constrain("DINHEIRO");
                            if (query.Execute().Count > 0)
                                finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;
                            else
                                throw new Exception("Nome do tipo de pagamento não encontrado\nDeve existir o tipo 'DINHEIRO'");
                            lancamento.idTipoPagamento = finanTipoPagamento.id;

                            Finan_Conta contaLancamento = null;
                            foreach (Finan_Conta conta in db.Query<Finan_Conta>())
                            {
                                if (conta.id == lancamento.idContaDestino)
                                {
                                    contaLancamento = conta;
                                    continue;
                                }
                            }

                            contaLancamento.saldoAtual = Math.Round(contaLancamento.saldoAtual, 2);

                            lancamento.saldoAnterior = contaLancamento.saldoAtual;
                            contaLancamento.saldoAnterior = contaLancamento.saldoAtual;
                            contaLancamento.saldoAtual += lancamento.valorLancado;
                            lancamento.saldoAtual = contaLancamento.saldoAtual;

                            dbStore(db, lancamento, contaLancamento);
                        }
                    }
                    if (clieteDestino != null)
                    {
                        finanTitulo.idClienteChequeRepassado = clieteDestino.id;
                        finanTitulo.situacao = ETituloSituacao.cheque_repassado;
                    }
                    dbStore(db, finanTitulo);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_ChequeCompensa(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Titulo finanTitulo, Finan_Conta finanConta, bool isDevolucao)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    string obs = finanTitulo.obs;
                    string dtCompensacao = finanTitulo.dtCompensacao;
                    string dtDevolucao1 = finanTitulo.dtDevolucao1;
                    string dtDevolucao2 = finanTitulo.dtDevolucao2;
                    bool isDevolvido1 = finanTitulo.isDevolvido1;
                    bool isDevolvido2 = finanTitulo.isDevolvido2;

                    Finan_Titulo finanTituloAlterado;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_Titulo));
                    query.Descend("id").Constrain(finanTitulo.id);
                    finanTituloAlterado = query.Execute()[0] as Finan_Titulo;

                    finanTituloAlterado.isAlterado = true;
                    dbStore(db, finanTituloAlterado);

                    Utils.copiaCamposBasicos(finanTituloAlterado, finanTitulo);
                    finanTitulo.obs = obs;
                    finanTitulo.dtCompensacao = dtCompensacao;
                    finanTitulo.dtDevolucao1 = dtDevolucao1;
                    finanTitulo.dtDevolucao2 = dtDevolucao2;
                    finanTitulo.isDevolvido1 = isDevolvido1;
                    finanTitulo.isDevolvido2 = isDevolvido2;
                    finanTitulo.isAlterado = false;

                    finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);
                    finanTitulo.idTituloOrigem = finanTituloAlterado.id;

                    if (isDevolucao)
                    {
                        if (!finanTitulo.isDevolvido1 && !finanTitulo.isDevolvido2)
                        {
                            finanTitulo.isDevolvido1 = true;
                        }
                        else if (finanTitulo.isDevolvido1 && !finanTitulo.isDevolvido2)
                        {
                            finanTitulo.isDevolvido2 = true;
                        }
                    }
                    else
                    {
                        finanTitulo.dtPagamento = finanTitulo.dtCompensacao;
                        finanTitulo.isCompensado = true;

                        Finan_Lancamento lancamento = new Finan_Lancamento();
                        lancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                        lancamento.idTransacao = finanTitulo.idTransacao;
                        lancamento.idOperacao = finanTitulo.idOperacao;
                        lancamento.idEmp = idEmp;
                        lancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        lancamento.idContaDestino = finanConta.id;

                        lancamento.dtFluxoCaixa = finanTitulo.dtCompensacao;
                        lancamento.valorBruto = finanTitulo.valorCobrado;
                        lancamento.valorLancado = finanTitulo.valorCobrado;
                        lancamento.valorLancado = Math.Round(lancamento.valorLancado, 2);

                        Finan_TipoPagamento finanTipoPagamento = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_TipoPagamento));
                        query.Descend("nome").Constrain("DINHEIRO");
                        if (query.Execute().Count > 0)
                            finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;
                        else
                            throw new Exception("Nome do tipo de pagamento não encontrado\nDeve existir o tipo 'DINHEIRO'");
                        lancamento.idTipoPagamento = finanTipoPagamento.id;

                        Finan_Conta contaLancamento = null;
                        foreach (Finan_Conta conta in db.Query<Finan_Conta>())
                        {
                            if (conta.id == lancamento.idContaDestino)
                            {
                                contaLancamento = conta;
                                continue;
                            }
                        }

                        contaLancamento.saldoAtual = Math.Round(contaLancamento.saldoAtual, 2);

                        lancamento.saldoAnterior = contaLancamento.saldoAtual;
                        contaLancamento.saldoAnterior = contaLancamento.saldoAtual;
                        contaLancamento.saldoAtual += lancamento.valorLancado;
                        lancamento.saldoAtual = contaLancamento.saldoAtual;

                        dbStore(db, lancamento, contaLancamento);
                    }
                    dbStore(db, finanTitulo);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void Finan_TituloBaixa(int idCorp, int idEmp, int idClienteFuncionarioLogado,
            List<Finan_TituloItem> listaFinanTituloItem, Finan_Conta finanConta, Finan_TipoPagamento finanTipoPagamento,
            int idCentroCusto, int idTipoLancamento)
        {
            IQuery query;

            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Finan_TituloItem fti in listaFinanTituloItem)
                    {
                        Finan_TituloItem finanTituloItem = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_TituloItem));
                        query.Descend("id").Constrain(fti.id);
                        if (query.Execute().Count == 0)
                            throw new Exception("Finan_TituloItem não encontrado. ID: " + fti.id);
                        else
                        {
                            finanTituloItem = query.Execute()[0] as Finan_TituloItem;
                            finanTituloItem.descontoVlr = fti.descontoVlr;
                        }

                        Finan_Titulo finanTitulo = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_Titulo));
                        query.Descend("id").Constrain(finanTituloItem.idTitulo);
                        if (query.Execute().Count == 0)
                            throw new Exception("Finan_Titulo não encontrado. ID: " + finanTituloItem.idTitulo);
                        else
                            finanTitulo = query.Execute()[0] as Finan_Titulo;

                        Cx_Lancamento cxLancamento = null;
                        query = db.Query();
                        query.Constrain(typeof(Cx_Lancamento));
                        query.Descend("idTransacao").Constrain(finanTitulo.idTransacao);
                        if (query.Execute().Count == 0)
                            throw new Exception("Cx_Lancamento não encontrados. ID TRANSAÇÃO: " + finanTitulo.idTransacao);
                        else
                            cxLancamento = query.Execute()[0] as Cx_Lancamento;

                        int numeroParcelas = 0;
                        query = db.Query();
                        query.Constrain(typeof(Finan_TituloItem));
                        query.Descend("idTitulo").Constrain(finanTitulo.id);
                        numeroParcelas = query.Execute().Count;

                        if (finanConta == null)
                        {
                            Cx_Lancamento cxL = new Cx_Lancamento();
                            Utils.copiaCamposBasicos(cxLancamento, cxL);
                            cxL.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Lancamento), 0);
                            cxL.idEmp = idEmp;
                            cxL.idTipoPagamento = finanTipoPagamento.id;
                            cxL.tipoPagamento_nome = finanTipoPagamento.nome;
                            cxL.grupoTipoPagamento_nome = finanTipoPagamento.grupoTipoPagamento_nome;
                            cxL.dthr = Utils.getAgoraString();
                            cxL.dtPagamento = cxL.dthr.Substring(0, 10);
                            cxL.valorRecebido = finanTituloItem.valorCobrado - finanTituloItem.descontoVlr;
                            cxL.tipoPagamento_geraContasReceber = false;
                            cxL.isEntrada = true;
                            cxL.situacao = ECxLancamentoSituacao.lancado;
                            cxL.tipo = ECxLancamentoTipo.recebimento;

                            if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, Utils.getAgoraString().Substring(0, 10)))
                                Caixa_Abertura(idCorp, idEmp, Utils.getAgoraString().Substring(0, 10), 0, true);

                            //Lança valor da entrada no Caixa Diário
                            Caixa_LancamentoDiario(db, idEmp, cxL.dtPagamento, cxL.valorRecebido, true);

                            dbStore(db, cxL);
                        }
                        else
                        {
                            Finan_Conta finanContaLancamento = null;
                            query = db.Query();
                            query.Constrain(typeof(Finan_Conta));
                            query.Descend("id").Constrain(finanConta.id);
                            if (query.Execute().Count == 0)
                                throw new Exception("Finan_Conta não encontrado. ID: " + finanConta.id);
                            else
                                finanContaLancamento = query.Execute()[0] as Finan_Conta;

                            Mov mov = null;
                            query = db.Query();
                            query.Constrain(typeof(Mov));
                            query.Descend("idTransacao").Constrain(finanTitulo.idTransacao);
                            if (query.Execute().Count == 0)
                                throw new Exception("Mov não encontrado. ID TRANSAÇÃO: " + finanTitulo.idTransacao);
                            else
                                mov = query.Execute()[0] as Mov;

                            Cliente cliente = null;
                            query = db.Query();
                            query.Constrain(typeof(Cliente));
                            query.Descend("id").Constrain(mov.idCliente);
                            if (query.Execute().Count == 0)
                                throw new Exception("Cliente não encontrado. ID CLIENTE: " + mov.idCliente);
                            else
                                cliente = query.Execute()[0] as Cliente;

                            Finan_Lancamento finanLancamento = new Finan_Lancamento();
                            finanLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                            finanLancamento.idEmp = idEmp;
                            finanLancamento.idTransacao = finanTitulo.idTransacao;
                            finanLancamento.idOperacao = finanTitulo.idOperacao;
                            finanLancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                            finanLancamento.idCentroCusto = idCentroCusto;
                            finanLancamento.idTipoLancamento = idTipoLancamento;
                            finanLancamento.idContaDestino = finanContaLancamento.id;
                            finanLancamento.idTipoPagamento = finanTipoPagamento.id;
                            finanLancamento.dtFluxoCaixa = Utils.getAgoraString().Substring(0, 10);
                            finanLancamento.dtLancamento = finanLancamento.dtFluxoCaixa;
                            finanLancamento.isCredito = true;
                            finanLancamento.valorBruto = finanTituloItem.valorCobrado;
                            finanLancamento.valorLancado = finanTituloItem.valorCobrado - finanTituloItem.descontoVlr;
                            finanLancamento.historico = "Recebimento titulo: "+finanTituloItem.identificador+" - Cliente: " + cliente.nome;

                            finanContaLancamento.saldoAtual = Math.Round(finanContaLancamento.saldoAtual, 2);

                            finanLancamento.saldoAnterior = finanContaLancamento.saldoAtual;
                            finanContaLancamento.saldoAnterior = finanContaLancamento.saldoAtual;
                            finanContaLancamento.saldoAtual += finanLancamento.valorLancado;
                            finanLancamento.saldoAtual = finanContaLancamento.saldoAtual;

                            dbStore(db, finanLancamento, finanContaLancamento);
                        }

                        if (finanTituloItem.parcela == numeroParcelas)
                            finanTitulo.situacao = ETituloSituacao.lancado;

                        finanTituloItem.situacao = ETituloSituacao.lancado;
                        cxLancamento.situacao = ECxLancamentoSituacao.lancado;

                        dbStore(db, finanTitulo, finanTituloItem, cxLancamento);
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

        #region Caixa

        public void Caixa_RetiradaPagamento(int idCorp, int idEmp, int idClienteFuncionarioLogado, Cx_Lancamento cxLancamento, Finan_Lancamento finanLancamento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    int idTransacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorTransacao), 0);
                    int idOperacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorOperacao), 0);

                    Finan_TipoPagamento finanTipoPagamento = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoPagamento));
                    query.Descend("nome").Constrain("DINHEIRO");
                    if (query.Execute().Count == 0)
                        throw new Exception("Tipo de Pagamento 'DINHEIRO' não encontrado.");
                    else
                        finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;

                    Finan_TipoLancamento finanTipoLancamento = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoLancamento));
                    query.Descend("nomeTipoLancamento").Constrain("TRANSFERENCIA DE CAIXA");
                    if (query.Execute().Count == 0)
                        throw new Exception("Tipo de Lancamento 'TRANSFERENCIA DE CAIXA' não encontrado");
                    else
                        finanTipoLancamento = query.Execute()[0] as Finan_TipoLancamento;

                    Finan_Conta finanConta = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_Conta));
                    query.Descend("id").Constrain(finanLancamento.idContaDestino);
                    if (query.Execute().Count > 0)
                        finanConta = query.Execute()[0] as Finan_Conta;
                    /*
                    if (query.Execute().Count == 0)
                        throw new Exception("Conta não encontrado. ID: " + finanLancamento.idContaDestino);
                    else
                        finanConta = query.Execute()[0] as Finan_Conta;
                     * */

                    cxLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Lancamento), 0);
                    cxLancamento.idEmp = idEmp;
                    cxLancamento.idTransacao = idTransacao;
                    cxLancamento.idOperacao = idOperacao;
                    cxLancamento.idGrupoTipoPagamento = finanTipoPagamento.idGrupoTipoPagamento;
                    cxLancamento.idTipoPagamento = finanTipoPagamento.id;
                    cxLancamento.dthr = Utils.getAgoraString();
                    cxLancamento.dtPagamento = cxLancamento.dthr.Substring(0, 10);
                    cxLancamento.tipoPagamento_nome = finanTipoPagamento.nome;
                    cxLancamento.grupoTipoPagamento_nome = finanTipoPagamento.grupoTipoPagamento_nome;
                    cxLancamento.situacao = ECxLancamentoSituacao.lancado;
                    cxLancamento.tipo = ECxLancamentoTipo.retirada;

                    if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, Utils.getAgoraString().Substring(0, 10)))
                        Caixa_Abertura(idCorp, idEmp, Utils.getAgoraString().Substring(0, 10), 0, true);

                    //Lança o valor da retirada em Cx_Diario
                    Caixa_LancamentoDiario(db, idEmp, cxLancamento.dtPagamento, cxLancamento.valorCobrado, false);

                    dbStore(db, cxLancamento);

                    finanLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                    finanLancamento.idEmp = idEmp;
                    finanLancamento.idTransacao = idTransacao;
                    finanLancamento.idOperacao = idOperacao;
                    finanLancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    finanLancamento.idTipoPagamento = finanTipoPagamento.id;
                    finanLancamento.dtFluxoCaixa = Utils.getAgoraString().Substring(0, 10);
                    finanLancamento.dtLancamento = Utils.getAgoraString().Substring(0, 10);
                    finanLancamento.isCredito = true;
                    finanLancamento.historico = "Transferência do caixa de faturamento na data " + Utils.getAgoraString();
                    dbStore(db, finanLancamento);

                    if (finanConta != null)
                    {
                        finanConta.saldoAtual = Math.Round(finanConta.saldoAtual, 2);
                        finanLancamento.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAtual += finanLancamento.valorLancado;
                        finanLancamento.saldoAtual = finanConta.saldoAtual;
                        dbStore(db, finanConta);
                    }

                    Finan_Lancamento finanLancamentoDebito = new Finan_Lancamento();
                    Utils.copiaCamposBasicos(finanLancamento, finanLancamentoDebito);
                    finanLancamentoDebito.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                    finanLancamentoDebito.valorBruto = -finanLancamentoDebito.valorBruto;
                    finanLancamentoDebito.valorLancado = -finanLancamentoDebito.valorLancado;
                    finanLancamentoDebito.isCredito = false;
                    finanLancamentoDebito.historico = cxLancamento.observacoes;
                    finanLancamentoDebito.idTipoLancamento = finanTipoLancamento.id;
                    finanLancamentoDebito.tipoLancamento_nome = finanTipoLancamento.nomeTipoLancamento;
                    dbStore(db, finanLancamentoDebito);

                    if (finanConta != null)
                    {
                        finanConta.saldoAtual = Math.Round(finanConta.saldoAtual, 2);
                        finanLancamentoDebito.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAtual += finanLancamentoDebito.valorLancado;
                        finanLancamentoDebito.saldoAtual = finanConta.saldoAtual;
                        dbStore(db, finanConta);
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
        public void Caixa_TransferenciaConta(int idCorp, int idEmp, int idClienteFuncionarioLogado, Cx_Lancamento cxLancamento, Finan_Lancamento finanLancamento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    int idTransacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorTransacao), 0);
                    int idOperacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorOperacao), 0);

                    Finan_TipoPagamento finanTipoPagamento = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoPagamento));
                    query.Descend("nome").Constrain("DINHEIRO");
                    if (query.Execute().Count == 0)
                        throw new Exception("Tipo de Pagamento 'DINHEIRO' não encontrado.");
                    else
                        finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;

                    Finan_Conta finanConta = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_Conta));
                    query.Descend("id").Constrain(finanLancamento.idContaDestino);
                    if (query.Execute().Count > 0)
                        finanConta = query.Execute()[0] as Finan_Conta;
                    /*
                    if (query.Execute().Count == 0)
                        throw new Exception("Conta não encontrado. ID: " + finanLancamento.idContaDestino);
                    else
                        finanConta = query.Execute()[0] as Finan_Conta;
                     * */

                    cxLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Lancamento), 0);
                    cxLancamento.idEmp = idEmp;
                    cxLancamento.idTransacao = idTransacao;
                    cxLancamento.idOperacao = idOperacao;
                    cxLancamento.idGrupoTipoPagamento = finanTipoPagamento.idGrupoTipoPagamento;
                    cxLancamento.idTipoPagamento = finanTipoPagamento.id;
                    cxLancamento.dthr = Utils.getAgoraString();
                    cxLancamento.dtPagamento = cxLancamento.dthr.Substring(0, 10);
                    cxLancamento.tipoPagamento_nome = finanTipoPagamento.nome;
                    cxLancamento.grupoTipoPagamento_nome = finanTipoPagamento.grupoTipoPagamento_nome;
                    cxLancamento.situacao = ECxLancamentoSituacao.lancado;
                    cxLancamento.tipo = ECxLancamentoTipo.retirada;

                    if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, Utils.getAgoraString().Substring(0, 10)))
                        Caixa_Abertura(idCorp, idEmp, Utils.getAgoraString().Substring(0, 10), 0, true);

                    //Lança o valor da retirada em Cx_Diario
                    Caixa_LancamentoDiario(db, idEmp, cxLancamento.dtPagamento, cxLancamento.valorCobrado, false);

                    dbStore(db, cxLancamento);

                    finanLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                    finanLancamento.idEmp = idEmp;
                    finanLancamento.idTransacao = idTransacao;
                    finanLancamento.idOperacao = idOperacao;
                    finanLancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    finanLancamento.idTipoPagamento = finanTipoPagamento.id;
                    finanLancamento.dtFluxoCaixa = Utils.getAgoraString().Substring(0, 10);
                    finanLancamento.dtLancamento = Utils.getAgoraString().Substring(0, 10);
                    finanLancamento.isCredito = true;
                    finanLancamento.historico = "Transferência do caixa de faturamento na data " + Utils.getAgoraString();
                    dbStore(db, finanLancamento);

                    if (finanConta != null)
                    {
                        finanConta.saldoAtual = Math.Round(finanConta.saldoAtual, 2);
                        finanLancamento.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAnterior = finanConta.saldoAtual;
                        finanConta.saldoAtual += finanLancamento.valorLancado;
                        finanLancamento.saldoAtual = finanConta.saldoAtual;
                        dbStore(db, finanConta);
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
        public void Caixa_Entrada(int idCorp, int idEmp, int idClienteFuncionarioLogado, Cx_Lancamento cxLancamento)
        {
            defineCorp(idCorp);
            try
            {
                Finan_TipoPagamento finanTipoPagamento = null;
                IQuery query = db.Query();
                query.Constrain(typeof(Finan_TipoPagamento));
                query.Descend("nome").Constrain("DINHEIRO");
                if (query.Execute().Count > 0)
                    finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;
                if (finanTipoPagamento == null)
                {
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoPagamento));
                    query.Descend("nome").Constrain("A VISTA");
                    if (query.Execute().Count > 0)
                        finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;
                }
                if (finanTipoPagamento == null)
                    throw new Exception("Tipo de Pagamento 'DINHEIRO' é necessário para a entrada de caixa.");

                int idTransacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorTransacao), 0);
                int idOperacao = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ContadorOperacao), 0);

                cxLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Lancamento), 0);
                cxLancamento.idEmp = idEmp;
                cxLancamento.idTransacao = idTransacao;
                cxLancamento.idOperacao = idOperacao;
                cxLancamento.idTipoPagamento = finanTipoPagamento.id;
                cxLancamento.dthr = Utils.getAgoraString();
                cxLancamento.dtPagamento = cxLancamento.dthr.Substring(0, 10);
                cxLancamento.situacao = ECxLancamentoSituacao.lancado;
                cxLancamento.tipo = ECxLancamentoTipo.entrada;
                cxLancamento.tipoPagamento_nome = finanTipoPagamento.nome;

                if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, Utils.getAgoraString().Substring(0, 10)))
                    Caixa_Abertura(idCorp, idEmp, Utils.getAgoraString().Substring(0, 10), 0, true);

                //Lança o valor da entrada no Caixa Diário
                Caixa_LancamentoDiario(db, idEmp, cxLancamento.dtPagamento, cxLancamento.valorRecebido, true);

                dbStore(db, cxLancamento);
                dbCommit(db);
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
                throw;
            }
        }
        public void Caixa_Abertura(int idCorp, int idEmp, string data, double valorAbertura, bool abertoPeloSistema)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Cx_Diario cxD = new Cx_Diario();
                    if (AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, data))
                    {
                        IQuery query = db.Query();
                        query.Descend("idEmp").Constrain(idEmp);
                        query.Descend("data").Constrain(data);
                        cxD = query.Execute()[0] as Cx_Diario;
                        cxD.valorAbertura = valorAbertura;
                        cxD.situacao = ECxDiarioSituacao.aberto;
                    }
                    else
                    {
                        cxD = new Cx_Diario();
                        cxD.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Diario), 0);
                        cxD.idEmp = idEmp;
                        //cxD.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        //cxD.isAberto = true;
                        cxD.data = data;
                        //cxD.saldo = saldo;
                        if (abertoPeloSistema)
                        {
                            cxD.situacao = ECxDiarioSituacao.aberto_pelo_sistema;
                            cxD.valorAbertura = AppFacade.get.reaproveitamento.getValorSaldoAtualCaixaPorData(db, idEmp, data);
                        }
                        else
                        {
                            cxD.situacao = ECxDiarioSituacao.aberto;
                            cxD.valorAbertura = valorAbertura;
                        }
                    }
                    dbStore(db, cxD);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }
        private void Caixa_LancamentoDiario(IObjectContainer db, int idEmp, string data, double valor, bool isEntrada)
        {
            Cx_Diario cxD = null;
            IQuery query = db.Query();
            query.Constrain(typeof(Cx_Diario));
            query.Descend("idEmp").Constrain(idEmp);
            query.Descend("data").Constrain(data);
            if (query.Execute().Count > 0)
                cxD = query.Execute()[0] as Cx_Diario;
            else
                throw new Exception("Caixa Diário não encontrado. Data: " + data);

            if (isEntrada)
                cxD.totalEntradas += valor;
            else
                cxD.totalSaidas += valor;

            dbStore(db, cxD);
        }


        #endregion


        public Finan_TituloItem Finan_DuplicataIpressa(int idCorp, Finan_TituloItem finanTituloItem)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Finan_TituloItem finanTituloItemDB = null;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Finan_TituloItem));
                    query.Descend("id").Constrain(finanTituloItem.id);
                    finanTituloItemDB = query.Execute()[0] as Finan_TituloItem;

                    Utils.copiaCamposBasicos(finanTituloItem, finanTituloItemDB);
                    finanTituloItemDB.situacao = ETituloSituacao.duplicata_impressa;
                    dbStore(db, finanTituloItemDB);
                    dbCommit(db);
                    return finanTituloItemDB;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        #region Contas a Pagar

        public void Finan_ContasPagar_Novo(int idCorp, int idEmp, int idClienteFuncionarioLogado, Finan_Titulo finanTitulo, List<Finan_TituloItem> listaFinanTituloItem)
        {
            defineCorp(idCorp);
            try
            {
                int idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                int idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                string dataLancamento = Utils.getAgoraString();

                finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);
                finanTitulo.idTransacao = idTransacao;
                finanTitulo.idOperacao = idOperacao;
                finanTitulo.idEmp = idEmp;
                finanTitulo.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                finanTitulo.dtLancamento = dataLancamento;
                finanTitulo.tipo = ETipoTitulo.titulo_a_pagar;
                finanTitulo.situacao = ETituloSituacao.em_aberto;
                finanTitulo.identificador = Utils.defineNumeroTitulo("CP", finanTitulo.id.ToString());

                foreach (Finan_TituloItem fti in listaFinanTituloItem)
                {
                    fti.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TituloItem), 0);
                    fti.idEmp = idEmp;
                    fti.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    fti.idTitulo = finanTitulo.id;
                    fti.dtLancamento = dataLancamento;
                    fti.situacao = ETituloSituacao.em_aberto;
                    fti.identificador = Utils.defineNumeroTituloItem("CP", finanTitulo.id.ToString(), fti.parcela.ToString());
                    dbStore(db, fti);
                }

                dbStore(db, finanTitulo);
                dbCommit(db);
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
                throw;
            }
        }

        public void Finan_ContasPagar_Baixa(int idCorp, int idEmp, int idClienteFuncionarioLogado,
            List<Finan_TituloItem> listaFinanTituloItem, Finan_Conta finanConta,
            string tipoPagamento, int idCentroCusto, int idTipoLancamento)
        {
            IQuery query;

            defineCorp(idCorp);
            try
            {
                foreach (Finan_TituloItem fti in listaFinanTituloItem)
                {
                    Empresa emp = null;
                    query = db.Query();
                    query.Constrain(typeof(Empresa));
                    query.Descend("id").Constrain(idEmp);
                    emp = query.Execute()[0] as Empresa;

                    Cliente cli_emp = null;
                    query = db.Query();
                    query.Constrain(typeof(Cliente));
                    query.Descend("id").Constrain(emp.idCliente);
                    cli_emp = query.Execute()[0] as Cliente;

                    Finan_TituloItem finanTituloItem = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_TituloItem));
                    query.Descend("id").Constrain(fti.id);
                    if (query.Execute().Count == 0)
                        throw new Exception("Finan_TituloItem não encontrado. ID: " + fti.id);
                    else
                        finanTituloItem = query.Execute()[0] as Finan_TituloItem;

                    Finan_Titulo finanTitulo = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_Titulo));
                    query.Descend("id").Constrain(finanTituloItem.idTitulo);
                    if (query.Execute().Count == 0)
                        throw new Exception("Finan_Titulo não encontrado. ID: " + finanTituloItem.idTitulo);
                    else
                        finanTitulo = query.Execute()[0] as Finan_Titulo;

                    Finan_TipoPagamento finanTipoPagamento = null;
                    query = db.Query();
                    query.Constrain(typeof(Finan_TipoPagamento));
                    query.Descend("nome").Constrain(tipoPagamento);
                    if (query.Execute().Count == 0)
                        throw new Exception("Finan_TipoPagamento não encontrado. TIPO PAGAMENTO: " + tipoPagamento);
                    else
                        finanTipoPagamento = query.Execute()[0] as Finan_TipoPagamento;

                    if (finanConta == null)
                    {
                        Cx_Lancamento cxL = new Cx_Lancamento();
                        cxL.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cx_Lancamento), 0);
                        cxL.idTransacao = finanTitulo.idTransacao;
                        cxL.idOperacao = finanTitulo.idOperacao;
                        cxL.idTipoPagamento = finanTipoPagamento.id;
                        cxL.idClientePagador = cli_emp.id;
                        cxL.idClienteRecebedor = finanTitulo.idClienteAReceber;
                        cxL.tipoPagamento_nome = finanTipoPagamento.nome;
                        cxL.grupoTipoPagamento_nome = finanTipoPagamento.grupoTipoPagamento_nome;
                        cxL.dthr = Utils.getAgoraString();
                        cxL.dtPagamento = fti.dtPagamento;
                        cxL.valorOriginal = -fti.valorCobrado;
                        cxL.valorCobrado = -fti.valorCobrado;
                        cxL.situacao = ECxLancamentoSituacao.lancado;
                        cxL.tipo = ECxLancamentoTipo.pagamento;

                        if (!AppFacade.get.reaproveitamento.caixaAberto(db, idEmp, Utils.getAgoraString().Substring(0, 10)))
                            Caixa_Abertura(idCorp, idEmp, Utils.getAgoraString().Substring(0, 10), 0, true);

                        //Lança o valor da saida no Caixa Diário
                        Caixa_LancamentoDiario(db, idEmp, Utils.getDateString(DateTime.Now), cxL.valorCobrado, false);
                        
                        dbStore(db, cxL);
                    }
                    else
                    {
                        Finan_Conta finanContaLancamento = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_Conta));
                        query.Descend("id").Constrain(finanConta.id);
                        if (query.Execute().Count == 0)
                            throw new Exception("Finan_Conta não encontrado. ID: " + finanConta.id);
                        else
                            finanContaLancamento = query.Execute()[0] as Finan_Conta;

                        Cliente fornecedor = null;
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(finanTitulo.idClienteAReceber);
                        if (query.Execute().Count == 0)
                            throw new Exception("Cliente não encontrado (Fornecedor). ID CLIENTE: " + finanTitulo.idClienteAReceber);
                        else
                            fornecedor = query.Execute()[0] as Cliente;

                        Finan_Lancamento finanLancamento = new Finan_Lancamento();
                        finanLancamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Lancamento), 0);
                        finanLancamento.idEmp = idEmp;
                        finanLancamento.idTransacao = finanTitulo.idTransacao;
                        finanLancamento.idOperacao = finanTitulo.idOperacao;
                        finanLancamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        finanLancamento.idCentroCusto = idCentroCusto;
                        finanLancamento.idTipoLancamento = idTipoLancamento;
                        finanLancamento.idContaDestino = finanContaLancamento.id;
                        finanLancamento.idTipoPagamento = finanTipoPagamento.id;
                        finanLancamento.dtFluxoCaixa = Utils.getAgoraString().Substring(0, 10);
                        finanLancamento.dtLancamento = finanLancamento.dtFluxoCaixa;
                        finanLancamento.valorBruto = fti.valorCobrado;
                        finanLancamento.valorLancado = fti.valorCobrado;
                        finanLancamento.historico = "Pagamento titulo: " + fti.identificador + " - Fornecedor: " + fornecedor.nome;

                        finanContaLancamento.saldoAtual = Math.Round(finanContaLancamento.saldoAtual, 2);

                        finanLancamento.saldoAnterior = finanContaLancamento.saldoAtual;
                        finanContaLancamento.saldoAnterior = finanContaLancamento.saldoAtual;
                        finanContaLancamento.saldoAtual -= finanLancamento.valorLancado;
                        finanLancamento.saldoAtual = finanContaLancamento.saldoAtual;

                        dbStore(db, finanLancamento, finanContaLancamento);
                    }

                    finanTituloItem.situacao = ETituloSituacao.lancado;
                    dbStore(db, finanTituloItem);

                    query = db.Query();
                    query.Constrain(typeof(Finan_TituloItem));
                    query.Descend("idTitulo").Constrain(finanTitulo.id);
                    IObjectSet rs_titulosItemByTitulo = query.Execute();
                    bool todosItensBaixados = true;
                    foreach (Finan_TituloItem item in rs_titulosItemByTitulo)
                    {
                        if (item.situacao != ETituloSituacao.em_aberto)
                            continue;
                        todosItensBaixados = false;
                        break;
                    }

                    if (todosItensBaixados)
                        finanTitulo.situacao = ETituloSituacao.lancado;

                    dbStore(db, finanTitulo);
                }
                dbCommit(db);
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
                throw;
            }
        }

        #endregion

        public void Finan_DuplicataEscrevePdf(int idCorp, int idEmp, List<Finan_TituloItem> listaFinanTitulo)
        {
            ConstrutorPDF.duplicataAcoRio(idCorp, idEmp, listaFinanTitulo);
        }

        #endregion

    }


}