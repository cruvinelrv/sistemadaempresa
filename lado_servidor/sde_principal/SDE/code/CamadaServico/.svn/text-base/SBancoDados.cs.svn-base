using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDE.Outros;
using SDE.CamadaControle;
using Db4objects.Db4o;

namespace SDE.CamadaServico
{
    public class SBancoDados : SuperServico
    {
        private CBanco _cBanco;
        protected CBanco cBanco { get { if (_cBanco == null)_cBanco = new CBanco(); return _cBanco; } }

        public void MetodoCorretivo()
        {
            new SDE.MetodoCorretivo.MetodoCorretivo().metodoCorretivo();
        }

        public void GeraExcecao(int index, string texto)
        {
            try
            {
                throw new Exception(texto);
            }
            catch (Exception ex)
            {
                throw anotaErro(ex);
            }
        }

        public Objeto Load(int index, long uuID)
        {
            CBanco controle = new CBanco();
            IObjectContainer db = AppFacade.get.conexaoBanco.get(index, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            return controle.Load(db, uuID);
        }

        public IList ListaClasses()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            List<string> lista = new List<string>();
            foreach (Type t in asm.GetTypes())
                if (t.Namespace == "SDE.Entidade" && !t.Name.StartsWith("__"))
                    lista.Add(t.FullName);

            string[] ss = new string[lista.Count];
            lista.CopyTo(ss);
            Array.Sort(ss);

            return ss;
        }

        public IList Lista(int index, string tipo)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(index, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            return cBanco.Lista(db, tipo);
        }

        public void EditaCampo(int index, long uuID, string campo, object valor)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(index, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            cBanco.EditaCampo(db, uuID, campo, valor);
        }

        public void Deleta(int index, long uuID)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(index, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            object instancia = db.Ext().GetByID(uuID);
            if (instancia != null)
            {
                db.Delete(instancia);
                db.Commit();
            }
        }

        public void removeTodasInstanciasClasse(int index, string classe)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(index, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            Assembly a = Assembly.Load("SDE");
            Type tipo = a.GetType(classe);//referenciar as dlls neste projeto
            foreach (Object o in db.Query(tipo))
                db.Delete(o);
            db.Commit();
        }
    }
}
