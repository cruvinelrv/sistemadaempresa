using System;
using System.Collections.Generic;
using System.Collections;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using System.Text;
using System.IO;
using Db4objects;
using Db4objects.Db4o;

namespace SDE.CamadaServico
{
    public class SItem : SuperServico
    {
        
        public Item Load(int idCorp, int idEmpresa, int idItem, ParamLoadItem pl)
        {
            setBancoID(idCorp);
            try
            {
                Item item = null;
                //se estive preenchido E não ignorando
                //explicitamente ignorando
                if (pl != null && !pl.ignorar)
                {
                    item = cItem.Load(idItem);
                    if (item == null)
                        return null;
                    item.__ie = cItem.LoadItemEmp(item.id, idEmpresa);
                }
                else
                {
                    item = new Item();
                    item.__ie = new ItemEmp();
                }

                if (pl != null)
                {
                    if (pl.precos)
                        item.__ie.__preco = cItem.LoadPreco(item.id, idEmpresa);
                    if (pl.aliquotas)
                        item.__ie.__aliquotas = cItem.LoadAliquotas(idEmpresa, item.id);
                    if (pl.estoques)
                        item.__estoques = cMov.PesquisaEstoque(idItem, 0);
                    if (pl.fornecedores)
                        item.__fornecedores = cItem.ListaFornecedores(idItem);
                }
                return item;
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
        }

        public IList<Item> Pesquisa(int idCorp, int idEmpresa, ParamFiltroItem pf, ParamLoadItem pl)
        {
            setBancoID(idCorp);
            if (pf == null)
                pf = new ParamFiltroItem();
            IList<Item> itens = cItem.Pesquisa(pf.texto, pf.secao, pf.grupo, pf.subgrupo, pf);
            List<Item> listaRet = new List<Item>();
            if (pf.limit == 0)
                pf.limit = 15;
            int qtdItensListar = Math.Min(itens.Count, pf.limit);

            for (int i = pf.offSet; (i < qtdItensListar); i++)
            {
                Item it = itens[i];
                if (pl != null)
                {
                    it.__ie = cItem.LoadItemEmp(it.id, idEmpresa);
                    //Load Precos, estoques, movimentacao
                    if (pl.precos)
                        it.__ie.__preco = cItem.LoadPreco(it.id, 1);
                    if (pl.fornecedores)
                        it.__fornecedores = cItem.ListaFornecedores(it.id);
                    
                    
                }
                listaRet.Add(it);
            }
            return listaRet;
        }

        public Item Novo(int idCorp, int idEmpresa, Item item, string codBarras)
        {
            setBancoID(idCorp);
            if (item.id > 0)
                throw new Exception("ITEM JÁ CADASTRADO");
            if (item.rfUnica != "GERAR" && cItem.Load(item.rfUnica) != null)
                throw new Exception("REFERÊNCIA JÁ CADASTRADA");
            lock (db.Ext().Lock())
            {
                try
                {
                    Item itemDB = cItem.Novo(item);
                    item.id = itemDB.id;
                    item.__ie.idItem = itemDB.id;
                    item.__ie.idEmp = idEmpresa;
                    if (item.nome.Length > 18)
                        item.nomeEtiqueta = item.nome.Substring(0, 18);
                    else
                        item.nomeEtiqueta = item.nome;

                    //item.__ie.__preco
                    ItemEmpPreco iep = cItem.LoadPreco(item.id, idEmpresa);
                    iep.custo = item.__ie.__preco.custo;
                    iep.compra = item.__ie.__preco.compra;
                    iep.venda = item.__ie.__preco.venda;
                    iep.descontoMaximo = item.__ie.__preco.descontoMaximo;
                    cItem.Atualiza(iep, iep);

                    ItemEmpEstoque iee = new ItemEmpEstoque();
                    iee.idEmp = idEmpresa;
                    iee.idItem = item.id;
                    iee.custo = iep.custo;
                    iee.identificador = "U:U";
                    iee.codBarras = codBarras;
                    cMov.Novo(iee, idEmpresa);

                    cItem.NovoItemEmpAliquotas(idEmpresa, item.id);
                    cMov.Novo(iee, idEmpresa);
                    db.Commit();
                    item.__ie.__preco = null;
                    return item;
                }
                catch (Exception ex)
                {
                    //db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }

        public Item Atualizar(int idCorp, int idEmpresa, Item itemDados, bool retorna)
        {
            setBancoID(idCorp);
            Item item = null;
            lock (db.Ext().Lock())
            {
                try
                {
                    item = cItem.Load(itemDados.id);
                    cItem.Atualiza(item, itemDados);

                    if (item.__fornecedores != null)
                    {
                        cItem.Atualiza(item.id, itemDados.__fornecedores);
                    }

                    if (itemDados.__ie != null)
                    {
                        ItemEmp ie = cItem.LoadItemEmp(item.id, idEmpresa);
                        cItem.Atualiza(ie, itemDados.__ie);

                        if (itemDados.__ie.__aliquotas != null)
                        {
                            ItemEmpAliquotas itemAliquotas =
                                cItem.LoadAliquotas(idEmpresa, item.id);
                            cItem.Atualiza(itemAliquotas, itemDados.__ie.__aliquotas);
                        }
                        if (itemDados.__ie.__preco != null)
                        {
                            ItemEmpPreco precos =
                                cItem.LoadPreco(item.id, 1);
                            cItem.Atualiza(precos, itemDados.__ie.__preco);
                        }
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
                return item;
            }
        }



    }
}
