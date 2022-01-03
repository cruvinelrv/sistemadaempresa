using System;
using System.Collections.Generic;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;


namespace SDE.CamadaControle
{
    public class CBalanco : SuperControle
    {
        /*
        public Balanco Load(int idBalancoItem)
        {
            IList<Balanco> rs = db.Query<Balanco>(delegate(Balanco b) { return b.id == idBalancoItem; });
            if (rs.Count != 1)
                throw new Exception(string.Format("Existem {0} balanços com a id = {1}", rs.Count, idBalancoItem));
            return rs[0];
        }

        public BalancoItem LoadBalancoItem(int idBalancoItem, int idIEE)
        {
            IList<BalancoItem> rs = db.Query<BalancoItem>(
                delegate(BalancoItem bi)
                {
                    return (bi.idBalancoItem == idBalancoItem &&
                            bi.idIEE == idIEE); 
                }
            );
            return (rs.Count == 1)? rs[0]: null;
        }


        public int Novo()
        {
            //CorporacaoMax cmax = queryCMax();
            Balanco b = new Balanco();
            //b.id = ++cmax.idBalancoItem;
            b.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Balanco), 0);
            b.dtInicio = Utils.getHojeString();
            b.dtInicio_ticks = DateTime.Today.Ticks;
            //db.Store(cmax);
            db.Store(b);
            return b.id;
        }

        public BalancoItem Registra(BalancoItem bi)
        {
            if (bi.id == 0)
            {
                //CorporacaoMax cmax = queryCMax();
                //bi.id = ++cmax.idBalancoItem;
                //db.Store(cmax);
                bi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(BalancoItem), 0);
            }
            Utils.filtraCampos(bi);
            db.Store(bi);
            return bi;
        }

        public void Remove(int idBalancoItem)
        {
            IList<BalancoItem> rs = db.Query<BalancoItem>(delegate(BalancoItem bi) { return bi.id == idBalancoItem; });
            if (rs.Count > 1)
                throw new Exception(string.Format("Existem {0} balanço-itens com a id = {1}", rs.Count, idBalancoItem));
            else if (rs.Count == 1)
                db.Delete(rs[0]);
            //else, quer dizer que nem existe, deve ter sido deletado por outra pessoa
        }

        public IList<Balanco> Lista()
        {
            IList<Balanco> rs = db.Query<Balanco>();
            return rs;
        }

        public IList<BalancoItem> Lista(int idBalancoItem)
        {
            IList<BalancoItem> rs = db.Query<BalancoItem>(delegate(BalancoItem bi) { return bi.idBalancoItem == idBalancoItem; });
            return rs;
        }

        public void Finaliza(Balanco balanco)
        {
            balanco.fechado = true;
            db.Store(balanco);
        }
        */


    }
}
