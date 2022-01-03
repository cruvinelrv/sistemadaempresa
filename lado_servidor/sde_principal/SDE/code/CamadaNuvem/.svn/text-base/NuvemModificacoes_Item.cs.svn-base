using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using System.Reflection;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public int ItemNovo(int idCorp, int idEmp, Item item, ItemEmpPreco iep)
        {
            defineCorp(idCorp);

            IQuery query;

            /*
            query = db.Query();
            query.Constrain(typeof(Item));
            query.Descend("nome").Constrain(item.nome);
            if (query.Execute().Count > 1)
                throw new Exception("Item com descrição '"+item.nome+"' já cadastrado.");
             * */

            if (item.rfUnica != "GERAR")
            {
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("rfUnica").Constrain(item.rfUnica);
                if (query.Execute().Count > 0 && !((query.Execute()[0] as Item).desuso))
                    throw new Exception("Item com código único '" + item.rfUnica + "' já cadastrado");
            }

            lock (db.Ext().Lock())
            {
                try
                {
                    //Item
                    item.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Item), 0);
                    if (item.rfUnica == "GERAR")
                        item.rfUnica = string.Concat("#", item.id);

                    //ItemEmp
                    ItemEmp ie = new ItemEmp();
                    ie.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmp), 0);
                    ie.idEmp = idEmp;
                    ie.idItem = item.id;

                    //ItemEmpPreco
                    iep.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpPreco), 0);
                    iep.idEmp = idEmp;
                    iep.idItem = item.id;

                    foreach (Empresa empresa in db.Query<Empresa>())
                    {
                        //ItemEmpEstoque
                        ItemEmpEstoque iee = new ItemEmpEstoque();
                        iee.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpEstoque), 0);
                        iee.idEmp = empresa.id;
                        iee.idItem = item.id;
                        iee.custo = iep.custo;
                        iee.identificador = "U:U";
                        iee.codBarras = _GeraCodigoBarra(iee.id);

                        //ItemEmpAliquotas
                        ItemEmpAliquotas iea = new ItemEmpAliquotas();
                        iea.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpAliquotas), 0);
                        iea.idEmp = empresa.id;
                        iea.idItem = item.id;
                        foreach (FieldInfo field in typeof(ItemEmpAliquotas).GetFields())
                        {
                            if (field.Name.StartsWith("icmsAliq"))
                                field.SetValue(iea, 17);
                            else if (field.Name.StartsWith("icmsCST"))
                                field.SetValue(iea, "000");
                        }
                        iea.pisCST = "08";    //operacao sem incidência
                        iea.cofinsCST = "08"; //operacao sem incidência
                        iea.ipiCST = "52";    //saída    sem incidência
                        iea.ipiCodEnquad = "999";

                        dbStore(db, iee, iea);
                    }

                    if (Utils.numeroCaracteresEtiqueta(idCorp) > 0)
                    {
                        if (item.nome.Length > Utils.numeroCaracteresEtiqueta(idCorp))
                            item.nomeEtiqueta = item.nome.Substring(0, Utils.numeroCaracteresEtiqueta(idCorp));
                        else
                            item.nomeEtiqueta = item.nome;
                    }

                    dbStore(db, item, ie, iep);
                    dbCommit(db);

                    return item.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public int ItemAtualiza(int idCorp, Item item, ItemEmpPreco iep, ItemEmpAliquotas iea)
        {
            defineCorp(idCorp);

            IQuery query;

            lock (db.Ext().Lock())
            {
                try
                {
                    //Item
                    Item itemDados = null;
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(item.id);
                    if (query.Execute().Count == 0)
                        throw new Exception("Item não encontrado. ID: " + item.id);
                    else
                        itemDados = query.Execute()[0] as Item;
                    Utils.copiaCamposBasicos(item, itemDados);

                    //ItemEmpPreco
                    ItemEmpPreco iepDados = null;
                    query = db.Query();
                    query.Constrain(typeof(ItemEmpPreco));
                    query.Descend("id").Constrain(iep.id);
                    if (query.Execute().Count == 0)
                        throw new Exception("ItemEmpPreco não encontrado. ID: " + iep.id);
                    else
                        iepDados = query.Execute()[0] as ItemEmpPreco;
                    Utils.copiaCamposBasicos(iep, iepDados);

                    //ItemEmpAliquotas
                    ItemEmpAliquotas ieaDados = null;
                    query = db.Query();
                    query.Constrain(typeof(ItemEmpAliquotas));
                    query.Descend("id").Constrain(iea.id);
                    if (query.Execute().Count == 0)
                        throw new Exception("ItemEmpAliquotas não encontrado. ID: " + iea.id);
                    else
                        ieaDados = query.Execute()[0] as ItemEmpAliquotas;
                    Utils.copiaCamposBasicos(iea, ieaDados);
                    dbStore(db, itemDados, iepDados, ieaDados);
                    dbCommit(db);
                    return itemDados.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void ItemPrecoAjusteSecao(int idCorp, int idEmp, int idSecao, int porcentagemAjuste)
        {
            defineCorp(idCorp);

            IQuery query;

            lock (db.Ext().Lock())
            {
                try
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("idSecao").Constrain(idSecao);
                    foreach (Item item in query.Execute())
                    {
                        query = db.Query();
                        query.Constrain(typeof(ItemEmpPreco));
                        query.Descend("idItem").Constrain(item.id);
                        foreach (ItemEmpPreco iep in query.Execute())
                        {
                            if (iep.idEmp == idEmp)
                            {
                                //Vinicius 24set
                                iep.venda = iep.venda + ((iep.venda * porcentagemAjuste) / 100);
                                //iep.atacado = iep.atacado - ((iep.atacado * porcentagemAjuste)/100);
                                dbStore(db, iep);
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

        public void ItemPrecoAjusteMarca(int idCorp, int idEmp, int idMarca, double porcentagemAjuste)
        {
            defineCorp(idCorp);

            IQuery query;

            lock (db.Ext().Lock())
            {
                try
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("idMarca").Constrain(idMarca);
                    foreach (Item item in query.Execute())
                    {
                        query = db.Query();
                        query.Constrain(typeof(ItemEmpPreco));
                        query.Descend("idItem").Constrain(item.id);
                        foreach (ItemEmpPreco iep in query.Execute())
                        {
                            if (iep.idEmp == idEmp)
                            {
                                iep.venda = iep.venda + ((iep.venda * porcentagemAjuste) / 100);
                                dbStore(db, iep);
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
    }
}