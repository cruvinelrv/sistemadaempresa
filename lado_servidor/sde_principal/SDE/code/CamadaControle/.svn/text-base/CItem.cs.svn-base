using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using SDE.Entidade;
using SDE.Enumerador;

using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using SDE.Parametro;

namespace SDE.CamadaControle
{
    public class CItem : SuperControle
    {
        #region Load


        public ItemEmpAliquotas LoadAliquotas(int idEmp, int idItem)
        {

            IList<ItemEmpAliquotas> rs = db.Query<ItemEmpAliquotas>(
                delegate(ItemEmpAliquotas xxx)
                {
                    return (xxx.idItem == idItem
                        && xxx.idEmp == idEmp);
                }
            );
            if (rs.Count > 0)
                return rs[0];


            //se nao existir aliquota
            ItemEmpAliquotas a = new ItemEmpAliquotas() 
            {
                idEmp = idEmp,
                idItem = idItem,
            };

            foreach (FieldInfo f in typeof(ItemEmpAliquotas).GetFields())
            {
                if (f.Name.StartsWith("icmsAliq"))
                    f.SetValue(a, 17);
                else if (f.Name.StartsWith("icmsCST"))
                    f.SetValue(a, "000");
            }
            a.pisCST = "08";    //operacao sem insidencia
            a.cofinsCST = "08"; //operacao sem insidencia
            a.ipiCST = "52";    //saída    sem insidencia
            a.ipiCodEnquad = "999";

            db.Store(a);
            return a;
        }

        public Item Load(int idItem)
        {
            IList<Item> rs = db.Query<Item>(
                  delegate(Item item)
                  {
                      return (item.id == idItem);
                  }
            );
            return (rs.Count > 0) ? rs[0] : null;
        }
        public Item Load(string rfUnica)
        {
            IList<Item> rs = db.Query<Item>(
                delegate(Item item)
                {
                    return item.rfUnica == rfUnica;
                }
            );
            return (rs.Count > 0) ? rs[0] : null;
        }

        public ItemEmpPreco LoadPreco(int idItem, int idEmp)
        {
            IList<ItemEmpPreco> rs = db.Query<ItemEmpPreco>(
                delegate(ItemEmpPreco xxx)
                {
                    return (xxx.idItem == idItem && xxx.idEmp == idEmp);
                }
            );
            if (rs.Count > 0)
                return rs[0];

            //CorporacaoMax cMax = queryCMax();
            ItemEmpPreco iep = new ItemEmpPreco();
            //iep.id = ++cMax.idIEP;
            iep.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpPreco), 0);
            iep.idItem = idItem;
            iep.idEmp = idEmp;
            db.Store(iep);
            return iep;
        }

        public ItemEmp LoadItemEmp(int idItem, int idEmp)
        {
            IList<ItemEmp> rs = db.Query<ItemEmp>(
                delegate(ItemEmp xxx)
                {
                    return (xxx.idItem == idItem && xxx.idEmp == idEmp);
                }
            );
            if (rs.Count > 0)
                return rs[0];

            ItemEmp ie = new ItemEmp();
            ie.idItem = idItem;
            ie.idEmp = idEmp;
            db.Store(ie);
            return ie;
        }

        public List<ItemFornecedor> ListaFornecedores(int idItem)
        {
            IList<ItemFornecedor> rs = db.Query<ItemFornecedor>(
                delegate(ItemFornecedor xxx)
                {
                    return (xxx.idItem == idItem);
                }
            );
            return new List<ItemFornecedor>(rs);
        }

        #endregion

        #region Pesquisa

        public IList lista()
        {
            IQuery q = db.Query();
            q.Constrain(typeof(Item));
            q.Descend("nome").OrderAscending();
            IList rs = q.Execute();

            return rs;
        }

        public IList<Item> Pesquisa(string texto, string secao, string grupo, string subgrupo, ParamFiltroItem pf)
        {

            IList<Item> rs = db.Query<Item>(
                  delegate(Item it)
                  {
                      if (Utils.ehNumero(texto))
                          return (it.id.ToString() == texto || it.rfAuxiliar.Contains(texto));
                      if (!Utils.verifica(
                           texto, it.nome, it.rfUnica, it.rfAuxiliar, it.marca, it.secao, it.grupo)
                          )
                      {
                          return false;
                      }

                      if (it.desuso)
                          return false;
                    
                      if ((pf.produto && pf.servico) || (!pf.produto && !pf.servico))
                          return true;
                      else if (pf.produto)
                          return it.tipo == EItemTipo.produto;
                      else if (pf.servico)
                          return it.tipo == EItemTipo.servico;
                      return true;
                  }
            );
            return rs;

        }

        public IList<Item> Pesquisa(IList<int> listaIdItem)
        {
            IList<Item> rs = db.Query<Item>(
                  delegate(Item item)
                  {
                      return (listaIdItem.Contains(item.id));
                  }
            );
            return rs;
        }

        #endregion

        #region Novo
        public Item Novo(Item itemDados)
        {
            //CorporacaoMax cMax = queryCMax();
            Item item = new Item();
            Utils.copiaCamposBasicos(itemDados, item);
            //item.id = ++cMax.idItem;
            item.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Item), 0);
            if (item.rfUnica == "GERAR")
                item.rfUnica = string.Concat("#", item.id);
            db.Store(item);
            //db.Store(cMax);
            return item;
        }
        public ItemEmpAliquotas NovoItemEmpAliquotas(int idEmp, int idItem)
        {
            ItemEmpAliquotas iea = new ItemEmpAliquotas() { idEmp = idEmp, idItem = idItem };
            iea.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpAliquotas), 0);

            foreach (FieldInfo f in typeof(ItemEmpAliquotas).GetFields())
            {
                if (f.Name.StartsWith("icmsAliq"))
                    f.SetValue(iea, 17);
                else if (f.Name.StartsWith("icmsCST"))
                    f.SetValue(iea, "000");
            }
            iea.pisCST = "08";    //operacao sem incidência
            iea.cofinsCST = "08"; //operacao sem incidência
            iea.ipiCST = "52";    //saída    sem incidência
            iea.ipiCodEnquad = "999";
            db.Store(iea);

            return iea;
        }
        #endregion

        #region Atualiza

        public void Atualiza(Item item, Item itemDados)
        {
            Utils.copiaCamposBasicos(itemDados, item);
            db.Store(item);
        }
        public void Atualiza(ItemEmpAliquotas aliquotas, ItemEmpAliquotas aliquotasDados)
        {
            Utils.copiaCamposBasicos(aliquotasDados, aliquotas);
            db.Store(aliquotas);
        }
        public void Atualiza(ItemEmpPreco preco, ItemEmpPreco precoDados)
        {
            Utils.copiaCamposBasicos(precoDados, preco);
            db.Store(preco);
        }
        public void Atualiza(ItemEmp ie, ItemEmp ieDados)
        {
            Utils.copiaCamposBasicos(ieDados, ie);
            db.Store(ie);
        }
        public void Atualiza(int idItem, List<ItemFornecedor> ifDados)
        {
           
        }
        #endregion

    }
}
