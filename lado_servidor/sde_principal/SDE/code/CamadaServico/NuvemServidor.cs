using System;
using System.Collections.Generic;
using System.Collections;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SDE.CamadaServico
{
    public partial class NuvemServidor : SuperServico
    {
        /*
        public IList Item_Lista_Item(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(Item));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Item_Lista_Preco(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(ItemEmpPreco));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Item_Lista_Aliquota(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(ItemEmpAliquotas));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Item_Lista_Estoque(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(ItemEmpAliquotas));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        //
        public IList Mov_Lista_Mov(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(Mov));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Mov_Lista_MovItem(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(MovItem));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        //
        public IList Corp_Lista_Login(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(Login));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        /**/
        public IList Corp_Lista_Empresa(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(Empresa));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        //
        public IList Cliente_Lista_Cliente(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(Cliente));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Cliente_Lista_Contato(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(ClienteContato));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }
        public IList Cliente_Lista_Endereco(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            IQuery q = db.Query();
            q.Constrain(typeof(ClienteEndereco));
            q.Descend("id").OrderAscending();
            IList rs = q.Execute();
            return rs;
        }

    }

    public partial class NuvemServidor : SuperServico
    {

            /*
        public Item Item_Novo(int idCorp, int idEmpresa, Item item, string codBarras)
        {
            defineCorp(idCorp);
            if (item.id > 0)
                throw new Exception("ITEM JÁ CADASTRADO");
            if (item.rfUnica != "GERAR" && cItem.Load(item.rfUnica) != null)
                throw new Exception("REFERÊNCIA JÁ CADASTRADA");
            try
            {
                lock (db.Ext().Lock())
                {
                    Item itemDB = cItem.Novo(item);

                    item.id = itemDB.id;
                    item.__ie.idItem = itemDB.id;
                    item.__ie.idEmp = idEmpresa;

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

                    
                    //cItem.Atualiza(
                        cItem.LoadAliq(item.id, idEmpresa, EItemAliq.entDentroUF),
                        item.__ie.__alEntDentro
                        );
                    *
                    db.Commit();
                }
                item.__ie.__preco = null;
                cAtualizacao.Anota(item.id, item);
            }
            catch (Exception ex)
            {
                db.Rollback();
                anotaErro(ex);
            }
            return item;
        }

        public Item Item_Atualizar(int idCorp, int idEmpresa, Item itemDados, bool retorna)
        {
            defineCorp(idCorp);
            Item item = null;
            try
            {
                lock (db.Ext().Lock())
                {
                    item = cItem.Load(itemDados.id);
                    cItem.Atualiza(item, itemDados);

                    //itemDados.__fornecedores
                    if (item.__fornecedores != null)
                    {
                        cItem.Atualiza(item.id, itemDados.__fornecedores);
                    }

                    if (itemDados.__ie != null)
                    {
                        ItemEmp ie = cItem.LoadItemEmp(item.id, idEmpresa);
                        cItem.Atualiza(ie, itemDados.__ie);
                        /
                        if (itemDados.__ie.__alEntDentro != null)
                        {
                            ItemEmpAliq itemAliq =
                                cItem.LoadAliq(item.id, idEmpresa, itemDados.__ie.__alEntDentro.tipoAliq);
                            cItem.Atualiza(itemAliq, itemDados.__ie.__alEntDentro);
                        }
                        *
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
            }
            catch (Exception ex)
            {
                anotaErro(ex);
            }
            return item;
        }
             * */

    }
}