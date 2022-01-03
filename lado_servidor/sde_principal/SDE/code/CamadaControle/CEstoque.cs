using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;

namespace SDE.CamadaControle
{
    public partial class CMov : SuperControle
    {
       
        #region Load


        public ItemEmpEstoque LoadEstoque(int idIEE)
        {
            IList<ItemEmpEstoque> rs = db.Query<ItemEmpEstoque>(
                delegate(ItemEmpEstoque iee)
                {
                    return iee.id == idIEE;
                }
             );
            if (rs.Count != 1)
                throw new Exception(string.Format("Existem {0} estoques com a id = {1}", rs.Count, idIEE));
            return rs[0];
        }
        public ItemEmpEstoque LoadEstoque(int idEmp, int idItem, string identificador)
        {
            IList<ItemEmpEstoque> rs = db.Query<ItemEmpEstoque>(
                delegate(ItemEmpEstoque xxx)
                {
                    return (xxx.idEmp == idEmp &&
                           xxx.idItem == idItem &&
                           xxx.identificador == identificador);
                }
             );
            if (rs.Count > 0)
                return rs[0];

            ItemEmpEstoque iee = new ItemEmpEstoque();
            iee.codBarras = "GERAR";
            iee.idItem = idItem;
            iee.identificador = identificador;
            return Novo(iee, idEmp);

            //    throw new Exception(string.Format("Existem {0} estoques com a idItem = {1}", rs.Count, idItem));
            //return rs[0];
        }

        public ItemEmpEstoque LoadEstoque(string codBarras)
        {
            IList<ItemEmpEstoque> rs = db.Query<ItemEmpEstoque>(
                delegate(ItemEmpEstoque iee)
                {
                    return iee.codBarras == codBarras;
                }
             );
            return (rs.Count == 1) ? rs[0] : null;
        }

        #endregion


        #region Pesquisa

        public IList<ItemEmpEstoque> PesquisaEstoque(int idEmp, IList<int> listaIdEstoque)
        {
            IList<ItemEmpEstoque> rs = db.Query<ItemEmpEstoque>(
                delegate(ItemEmpEstoque iee)
                {
                    return (listaIdEstoque.Contains(iee.id) && idEmp == iee.idEmp);
                }
            );

            return rs;
        }
        public List<ItemEmpEstoque> PesquisaEstoque(int idItem, int idEmp)
        {
            Predicate<ItemEmpEstoque> pesq = null;

            if (idEmp == 0)
            {
                pesq = delegate(ItemEmpEstoque iee) { return (iee.idItem == idItem); };
                idEmp = 1;
            }
            else
                pesq = delegate(ItemEmpEstoque iee) { return (iee.idItem == idItem && iee.idEmp == idEmp); };

            IList<ItemEmpEstoque> rs = db.Query<ItemEmpEstoque>(pesq);

            if (rs.Count == 0)
            {
                ItemEmpEstoque iee = new ItemEmpEstoque()
                {
                    codBarras = "GERAR",
                    idItem = idItem,
                    identificador = "U:U",
                    idEmp = idEmp
                };

                iee = Novo(iee, idEmp);
                return new List<ItemEmpEstoque>() { iee };
            }
            else
            {
                return new List<ItemEmpEstoque>(rs);
            }
        }

        #endregion




        public ItemEmpEstoque Novo(ItemEmpEstoque iee, int idEmp)
        {
            //CorporacaoMax cmax = queryCMax();
            //iee.id = ++cmax.idIEE;
            iee.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpEstoque), 0);
            if (iee.codBarras == "GERAR")
                iee.codBarras = _GeraCodigoBarra(iee.id);
            iee.idEmp = idEmp;
            //db.Store(cmax);
            db.Store(iee);
            return iee;
        }

        private string _GeraCodigoBarra(int idIEE)
        {
            return string.Concat("B", idIEE.ToString().PadLeft(6, '0'));
        }

        public void EstoqueBalanco(ItemEmpEstoque iee, double qtd)
        {
            iee.qtd = qtd;
            db.Store(iee);
        }



    }

}
