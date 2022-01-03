using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db4objects.Db4o;

namespace SDE.CamadaNuvem
{
    public abstract class SuperNuvem
    {
        protected IObjectContainer db0 { get; private set; }
        protected int idCorp { get; private set; }

        public SuperNuvem()
        {
            db0 = AppFacade.get.conexaoBanco.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            _insercoes = new ArrayList();
            _remocoes = new ArrayList();
	    }

        protected void defineCorp(int idCorp)
        {
            this.idCorp = idCorp;
            db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
        }


        protected ArrayList _insercoes = null;
        protected ArrayList _remocoes = null;




        protected void dbRemove(IObjectContainer dbX, object objeto)
        {
            dbX.Delete(objeto);
            _remocoes.Add(objeto);
        }

        public void dbStore(IObjectContainer dbX, params object[] objetos)
        {
            foreach (Object o in objetos)
            {
                dbX.Store(o);
                _insercoes.Add(o);
            }
        }
        protected void dbStore(IObjectContainer dbX, object objeto)
        {
            dbX.Store(objeto);
            _insercoes.Add(objeto);
        }
        protected void dbCommit(params IObjectContainer[] dbList)
        {
            foreach (IObjectContainer dbX in dbList)
                dbX.Commit();
            AppFacade.get.cacheDados.LancaNotificacoes(idCorp, _insercoes, _remocoes);
        }
        protected void dbRollback(Exception ex, params IObjectContainer[] dbList)
        {
            AppFacade.get.conexaoBanco.geraLog(idCorp, ex);
            foreach (IObjectContainer dbX in dbList)
                dbX.Rollback();
            throw new Exception(string.Format("[erro anotado {2}] {0} {1}", ex.Message, ex.StackTrace, idCorp), ex);
        }

        private IObjectContainer _db = null;

        public IObjectContainer db
        {
            private set { _db = value; }
            get
            {
                if (_db == null)
                    throw new Exception("O sistema não pôde identificar sua corporação");
                else
                    return _db;
            }
        }
    }
}
