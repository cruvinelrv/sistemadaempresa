using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using SDE.PDF;
using Db4objects.Db4o;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void Estoque_InventarioEscrevePdf(int idCorp, int idEmp, bool porGrupo, string tipoPreco, double pctSobreValor, string dataInventario, string cabecalhoInventario, bool mostraZerados)
        {
            ConstrutorPDF.relatorioInventario(idCorp, idEmp, porGrupo, tipoPreco, pctSobreValor, dataInventario, cabecalhoInventario, mostraZerados);
        }

        public int NovaEntrada(int idCorp, int idEmp, Mov mov, List<ItemEmpPreco> listIEP)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    foreach (ItemEmpPreco iep in listIEP)
                    {
                        query = db.Query();
                        query.Constrain(typeof(ItemEmpPreco));
                        query.Descend("idEmp").Constrain(iep.idEmp);
                        query.Descend("idItem").Constrain(iep.idItem);
                        IObjectSet rs_iep = query.Execute();
                        ItemEmpPreco iep_db = rs_iep[0] as ItemEmpPreco;

                        iep_db.venda = iep.venda;
                        iep_db.custo = iep.custo;
                        iep_db.compra = iep.compra;

                        dbStore(db,iep_db);
                    }

                    
                    
                    if (mov.__mItens == null)
                        throw new Exception("Entrada sem itens");

                    mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                    mov.dthrMovEmissao = Utils.getAgoraString();
                    mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;

                    foreach (MovItem mi in mov.__mItens)
                    {
                        mi.idMov = mov.id;

                        foreach (MovItemEstoque mie in mi.__mIEstoques)
                        {
                            MovItem miNovo = new MovItem();
                            Utils.copiaCamposBasicos(mi, miNovo);
                            miNovo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);

                            mie.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItemEstoque), 0);

                            ItemEmpEstoque iee = null;

                            query = db.Query();
                            query.Constrain(typeof(ItemEmpEstoque));
                            query.Descend("idItem").Constrain(miNovo.idItem);

                            if (query.Execute().Count > 0)
                            {
                                foreach (ItemEmpEstoque xxx in query.Execute())
                                    if (xxx.identificador == mie.identificador && xxx.idEmp == idEmp)
                                        iee = xxx;
                            }
                            if (iee == null)
                            {
                                iee = new ItemEmpEstoque();
                                iee.codBarras = "GERAR";
                                iee.idItem = mi.idItem;
                                iee.identificador = mie.identificador;
                                iee = NovoItemEmpresaEstoque(iee, idEmp);
                            }

                            if (iee.custo == 0)
                                iee.custo = mi.vlrUnitCusto;
                            iee.qtd += mie.qtd;
                            iee.lote = mie.lote;
                            iee.codBarrasGrade = mie.codBarrasGrade;
                            iee.dtFab = mie.dtFab;
                            iee.dtVal = mie.dtVal;

                            mie.idIEE = iee.id;
                            mie.idMovItem = miNovo.id;
                            Utils.filtraCampos(mie);

                            miNovo.idIEE = iee.id;
                            miNovo.qtd = mie.qtd;
                            miNovo.estoque_identificador = mie.identificador;
                            Utils.filtraCampos(miNovo);

                            dbStore(db, mie, iee, miNovo);
                        }
                    }

                    if (mov.__mValores != null)
                    {
                        foreach (MovValor mv in mov.__mValores)
                        {
                            mv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovValor), 0);
                            mv.idMov = mov.id;
                            dbStore(db, mv);
                        }
                    }

                    Utils.filtraCampos(mov);
                    dbStore(db, mov);
                    dbCommit(db);
                    return mov.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public int AlteraEntrada(int idCorp, int idEmp, Mov movOriginal, Mov movAlterada, List<ItemEmpPreco> listIEP)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Mov_Cancela(idCorp, idEmp, movOriginal.id, movOriginal.idClienteFuncionarioLogado);
                    movAlterada.id = NovaEntrada(idCorp, idEmp, movAlterada, listIEP);
                    
                    return movAlterada.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        private ItemEmpEstoque NovoItemEmpresaEstoque(ItemEmpEstoque iee, int idEmp)
        {
            iee.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpEstoque), 0);
            if (iee.codBarras == "GERAR")
                iee.codBarras = _GeraCodigoBarra(iee.id);
            iee.idEmp = idEmp;
            dbStore(db, iee);
            return iee;
        }
        
        private string _GeraCodigoBarra(int idIEE)
        {
            return string.Concat("B", idIEE.ToString().PadLeft(6, '0'));
        }

    }
}
