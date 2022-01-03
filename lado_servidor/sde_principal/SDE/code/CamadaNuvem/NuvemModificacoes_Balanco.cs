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
using SDE.EntidadeNFE;
using System.Text;
using SDE.PDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void RelatorioBalanco_Parcial(int idCorp, int idEmp, int idBalanco)
        {
            ConstrutorPDF.RelatorioParcialBalanco(idCorp, idEmp, idBalanco);
        }

        public int Balanco_Abre(int idCorp, int idEmp, int idClienteFuncionarioLogado)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Balanco b = new Balanco();
                    ReaproveitamentoCodigo r = AppFacade.get.reaproveitamento;
                    b.id = r.getIncremento(db, typeof(Balanco), 0);
                    b.idOperacao = r.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                    b.idTransacao = r.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);
                    b.dthrInicio = Utils.getAgoraString();
                    b.nome = string.Format("Balanço {0} iniciado em {1}", b.id, b.dthrInicio);

                    dbStore(db, b);
                    dbCommit(db);
                    return b.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return 0;
                }
            }
        }
        public void Balanco_Lanca(int idCorp, BalancoItem bi)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    bi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(BalancoItem), 0);

                    if (bi.idBalanco == 0 || bi.idOperacao == 0 || bi.idTransacao == 0)
                        throw new Exception("é de obrigação do lado-cliente preencher os campos bi.idBalancoItem, bi.idOperacao e bi.idTransacao");

                    //bi.idClienteFuncionario = idClienteFuncionarioLogado;
                    dbStore(db, bi);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }
        public void Balanco_Remove(int idCorp, int idBalancoItem)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (BalancoItem bi in db.Query<BalancoItem>())
                        if (bi.id == idBalancoItem)
                            dbRemove(db, bi);

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }
        public void Balanco_Fecha(int idCorp, int idEmp, /*int idClienteFuncionarioLogado, */ int idBalanco, bool boolConcluiCancela)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //essa função faz loop ao invés de [0],
                    //pois aí será a prova de erros, caso encontre 0

                    IQuery q;
                    q = db.Query();
                    q.Constrain(typeof(Balanco));
                    q.Descend("id").Constrain(idBalanco);
                    Balanco b = q.Execute()[0] as Balanco;

                    b.dthrFim = Utils.getAgoraString();

                    if (!boolConcluiCancela)
                    {
                        b.nome = string.Format("Balanço {0} iniciado em {1}, cancelado em {2}", b.id, b.dthrInicio, b.dthrFim);
                        b.situacao = EBalancoSituacao.cancelado;
                    }
                    else
                    {
                        b.nome = string.Format("Balanço {0} iniciado em {1}, concluído em {2}", b.id, b.dthrInicio, b.dthrFim);
                        b.situacao = EBalancoSituacao.efetuado;

                        Mov m = new Mov();
                        m.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                        m.idOperacao = b.idOperacao;
                        m.idTransacao = b.idTransacao;
                        m.tipo = EMovTipo.ambos_balan;
                        m.resumo = EMovResumo.ambos;
                        m.impressao = EMovImpressao.sem_impressao;
                        m.dthrMovEmissao = b.dthrFim;


                        //
                        q = db.Query();
                        q.Constrain(typeof(BalancoItem));
                        q.Descend("idBalanco").Constrain(idBalanco);
                        IList bi_Usados = q.Execute();
                        //
                        q = db.Query();
                        q.Constrain(typeof(ItemEmpEstoque));
                        IList iee_todos = q.Execute();
                        //
                        //List<ItemEmpEstoque> iee_Usados = new List<ItemEmpEstoque>();
                        //



                        foreach (BalancoItem bi in bi_Usados)
                        {
                            /*
                            bool jaExiste = false;
                            //procura usados, para ver se já existe
                            foreach (ItemEmpEstoque iee in iee_Usados)
                                if (iee.id==bi.idIEE)
                                {
                                    jaExiste=true;
                                    break;
                                }
                            //procura em todos, para adicionar o IEE desejado
                            if (!jaExiste)
                             * */
                                foreach (ItemEmpEstoque iee in iee_todos)
                                    if (iee.id == bi.idIEE && iee.idEmp == idEmp)
                                    {

                                        iee.qtd = 0;
                                        //salva sem notificar
                                        db.Store(iee);
                                        //Console.Beep(1000, 200);

                                        //iee_Usados.Add(iee);
                                        break;
                                    }
                        }


                        q = db.Query();
                        q.Constrain(typeof(ItemEmpEstoque));
                        iee_todos = q.Execute();

                        //modifica os estoques abrangidos
                        foreach (BalancoItem bi in bi_Usados)
                        {
                            //Console.Beep(2000, 200);
                            //BalancoItem bi = itensBalanco[k];
                            //ItemEmpEstoque iee = null;



                            MovItem mi = new MovItem();
                            mi.idMov = m.id;
                            mi.idOperacao = m.idOperacao;
                            mi.idTransacao = m.idTransacao;
                            mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                            mi.idBalancoItem = bi.id;
                            //
                            mi.item_nome = bi.item_nome;
                            mi.qtd += bi.qtdLancada;
                            mi.idIEE = bi.idIEE;
                            mi.idOperacao = m.idOperacao;
                            mi.idTransacao = m.idTransacao;



                            foreach (ItemEmpEstoque iee in iee_todos)
                                if (iee.id == bi.idIEE && iee.idEmp == idEmp)
                                {
                                    iee.qtd += bi.qtdLancada;
                                    mi.saldoAtual = iee.qtd;
                                    dbStore(db, iee);
                                    break;
                                }


                            dbStore(db, mi);
                        }
                        dbStore(db, m);
                    }
                    dbStore(db, b);

                    //throw new ExcecaoSDE("aaa");

                    dbCommit(db);
                    Console.Beep(500, 500);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }




        
    }


}