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
        public void RelatorioOrdemServico(int idCorp, int idEmp, int idOrdemServico)
        {
            ConstrutorPDF.RelatorioOrdemServico(idCorp, idEmp, idOrdemServico);
        }

        public void OrdemServico_Tipo_NovosAtualizacoes(int idCorp, List<OrdemServico_Tipo> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (OrdemServico_Tipo xxx in objetos)
                    {
                        if (xxx.id == 0)
                        {
                            //se trata de um cadastro NOVO
                            //ProximoCampo prox = new ProximoCampo(idCorp);
                            //Random r = new Random();
                            xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico_Tipo), 0);
                            dbStore(db, xxx);
                        }
                        else
                        {
                            //se trata de uma ALTERAÇÃO
                            IQuery q = db.Query();
                            q.Constrain(typeof(OrdemServico_Tipo));
                            q.Descend("id").Constrain(xxx.id);
                            IObjectSet r = q.Execute();
                            OrdemServico_Tipo cadOrdemServicoTipo = (OrdemServico_Tipo)r[0];
                            Utils.copiaCamposBasicos(xxx, cadOrdemServicoTipo);
                            dbStore(db, cadOrdemServicoTipo);
                        }
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public int OrdemServico_NovoAtualizacao(int idCorp, int idEmp, int idClienteFuncionarioLogado, OrdemServico ordemServico_atualizacao)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (ordemServico_atualizacao.id == 0)
                    {
                        //Nova OS
                        IList<OrdemServico_Item> listaItens = new List<OrdemServico_Item>(ordemServico_atualizacao.__oSItens);
                        ordemServico_atualizacao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico), 0);
                        ordemServico_atualizacao.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                        ordemServico_atualizacao.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                        foreach (OrdemServico_Item osi in listaItens)
                        {
                            IList<OrdemServico_Executor> listaExecutores = new List<OrdemServico_Executor>(osi.__executores);
                            osi.id = OrdemServico_NovoItem(ordemServico_atualizacao.id, osi);
                            foreach (OrdemServico_Executor ose in listaExecutores)
                            {
                                OrdemServico_NovoExecutor(ordemServico_atualizacao.id, osi.id, ose);
                            }
                        }
                        ordemServico_atualizacao.dthrLancamento = Utils.getAgoraString();
                        ordemServico_atualizacao.dtOrdemServicoTicks = DateTime.Parse(ordemServico_atualizacao.dthrLancamento).Ticks;
                        Utils.filtraCampos(ordemServico_atualizacao);
                        dbStore(db, ordemServico_atualizacao);
                    }
                    else
                    {
                        //Atualização de OS
                        IQuery query;

                        query = db.Query();
                        query.Constrain(typeof(OrdemServico));
                        query.Descend("id").Constrain(ordemServico_atualizacao.id);
                        IObjectSet setter_ordemServico = query.Execute();
                        OrdemServico ordemServico_db = (OrdemServico)setter_ordemServico[0];

                        //Se a ordem de serviço foi finalizada então uma movimentação foi gerada com origem na mesma, devo então
                        //cancelar tal movimentação, alterar o status da ordem de serviço para cancelada e criar uma nova ordem de serviço
                        // com os dados inseridos pelo usuário ao reabrir a OS
                        if (ordemServico_atualizacao.status == EOrdemServicoStatus.finalizada)
                        {
                            //a ordem de seviço é cancelada, pois uma nova será gerada
                            ordemServico_db.status = EOrdemServicoStatus.cancelada;
                            //a movimentação é cancelada
                            Mov_Cancela(idCorp, idEmp, ordemServico_db.idMovFinalizacao, idClienteFuncionarioLogado);
                            dbStore(db, ordemServico_db);

                            OrdemServico ordemServico_novo = new OrdemServico();
                            ordemServico_novo = ordemServico_atualizacao;

                            IList<OrdemServico_Item> listaItens = new List<OrdemServico_Item>(ordemServico_novo.__oSItens);
                            ordemServico_novo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico), 0);
                            ordemServico_novo.idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                            ordemServico_novo.idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                            foreach (OrdemServico_Item osi in listaItens)
                            {
                                IList<OrdemServico_Executor> listaExecutores = new List<OrdemServico_Executor>(osi.__executores);
                                osi.id = OrdemServico_NovoItem(ordemServico_novo.id, osi);
                                foreach (OrdemServico_Executor ose in listaExecutores)
                                {
                                    if (!ose.__removaMe)
                                        OrdemServico_NovoExecutor(ordemServico_novo.id, osi.id, ose);
                                }
                            }
                            ordemServico_novo.status = EOrdemServicoStatus.nao_iniciada;
                            ordemServico_novo.idMovFinalizacao = 0;
                            ordemServico_novo.obs = "OS reaberta com origem na OS de Nº:" + ordemServico_db.numOS + " de abertura no dia " + ordemServico_db.dthrInicio.Substring(0, 10) + ".";
                            ordemServico_novo.dthrLancamento = Utils.getAgoraString();
                            ordemServico_novo.dtOrdemServicoTicks = DateTime.Parse(ordemServico_novo.dthrLancamento).Ticks;
                            dbStore(db, ordemServico_novo);
                        }
                        else
                        {
                            foreach (OrdemServico_Item osi in ordemServico_atualizacao.__oSItens)
                            {
                                IList<OrdemServico_Executor> listaExecutores = new List<OrdemServico_Executor>(osi.__executores);
                                if (osi.id == 0)
                                {
                                    // novo item
                                    osi.id = OrdemServico_NovoItem(ordemServico_atualizacao.id, osi);
                                    foreach (OrdemServico_Executor ose in listaExecutores)
                                    {
                                        OrdemServico_NovoExecutor(ordemServico_atualizacao.id, osi.id, ose);
                                    }
                                }
                                else
                                {
                                    //atualiza item
                                    OrdemServico_AtualizaItem(osi);
                                    foreach (OrdemServico_Executor ose in listaExecutores)
                                    {
                                        if (ose.id == 0)
                                            OrdemServico_NovoExecutor(ordemServico_atualizacao.id, osi.id, ose);
                                        else
                                            OrdemServico_AtualizaExecutor(ose);
                                    }
                                }
                            }

                            Utils.copiaCamposBasicos(ordemServico_atualizacao, ordemServico_db);
                            dbStore(db, ordemServico_db);
                        }
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
            return ordemServico_atualizacao.id;
        }

        public int OrdemServico_NovoItem(int idOrdemServico, OrdemServico_Item osi)
        {
            osi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico_Item), 0);
            osi.idOrdemServico = idOrdemServico;

            if (osi.__item.tipo == EItemTipo.produto)
            {
                IQuery query = db.Query();
                query.Constrain(typeof(ItemEmpEstoque));
                query.Descend("id").Constrain(osi.idIEE);
                IObjectSet setter_iee = query.Execute();
                ItemEmpEstoque iee = (ItemEmpEstoque)setter_iee[0];
                iee.qtdReserva += osi.qtd;
                dbStore(db, iee);
            }

            Utils.filtraCampos(osi);
            dbStore(db, osi);
            return osi.id;
        }

        public void OrdemServico_AtualizaItem(OrdemServico_Item osi)
        {
            IQuery query;
            query = db.Query();
            query.Constrain(typeof(OrdemServico_Item));
            query.Descend("id").Constrain(osi.id);
            IObjectSet setter_osi = query.Execute();
            OrdemServico_Item osi_atualiza = (OrdemServico_Item)setter_osi[0];

            query = db.Query();
            query.Constrain(typeof(ItemEmpEstoque));
            query.Descend("id").Constrain(osi.idIEE);
            IObjectSet setter_iee = query.Execute();
            ItemEmpEstoque iee = (ItemEmpEstoque)setter_iee[0];

            /*
            if (osi.status == EOrdemServicoStatus.cancelada)
            {
                iee.qtdReserva -= osi.qtd;
                iee.qtd += osi.qtd;
                Utils.copiaCamposBasicos(osi, osi_atualiza);
                dbStore(db, iee);
                dbStore(db, osi_atualiza);
                return;
            }
             * */

            if (osi.__removaMe)
            {
                iee.qtdReserva -= osi_atualiza.qtd;
                iee.qtd += osi_atualiza.qtd;
                dbStore(db, iee);
                dbRemove(db, osi_atualiza);
            }
            else
            {
                double diferenca = osi.qtd - osi_atualiza.qtd;
                iee.qtdReserva += diferenca;
                iee.qtd -= diferenca;
                Utils.copiaCamposBasicos(osi, osi_atualiza);
                dbStore(db, iee);
                dbStore(db, osi_atualiza);
            }
        }

        public int OrdemServico_NovoExecutor(int idOrdemServico, int idOrdemServicoItem, OrdemServico_Executor ose)
        {
            ose.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico_Executor), 0);
            ose.idOrdemServico = idOrdemServico;
            ose.idOrdemServicoItem = idOrdemServicoItem;
            Utils.filtraCampos(ose);
            dbStore(db, ose);
            return ose.id;
        }

        public void OrdemServico_AtualizaExecutor(OrdemServico_Executor ose)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(OrdemServico_Executor));
            query.Descend("id").Constrain(ose.id);
            IObjectSet setter_ose = query.Execute();
            OrdemServico_Executor ose_novo = (OrdemServico_Executor)setter_ose[0];

            if (ose.__removaMe)
            {
                dbRemove(db, ose_novo);
            }
            else
            {
                Utils.copiaCamposBasicos(ose, ose_novo);
                dbStore(db, ose_novo);
            }
        }
    }


}