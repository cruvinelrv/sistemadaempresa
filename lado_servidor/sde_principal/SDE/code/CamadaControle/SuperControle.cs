using System;
using System.Collections.Generic;
using Db4objects.Db4o;
using SDE.Entidade;

namespace SDE.CamadaControle
{
    public class SuperControle 
    {
        public SuperControle()
        {
            this.idCorp = Convert.ToInt32(System.Web.HttpContext.Current.Items["idCorp"]);
            this.banco = AppFacade.get.conexaoBanco;
            this.db0 = banco.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            this.db = banco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
        }
        protected GerenteConectividadeBancoDados banco = null;
        protected IObjectContainer db = null;
        protected IObjectContainer db0 = null;
        int _idCorp;
        
        protected int idCorp
        {
            get { return _idCorp; }
            private set
            {
                _idCorp = value;
            }
        }
        protected Max queryMax()
        {
            return db0.Query<Max>()[0];
        }
        /*
        protected CorporacaoMax queryCMax()
        {
            IList<CorporacaoMax> rs = db.Query<CorporacaoMax>();
            return rs[0];
        }
         * */

    }
}
