using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDE.Outros;
using SDE.CamadaControle;
using Db4objects.Db4o;

namespace SDE.CamadaServico
{
    public class STecnico : SuperServico
    {

        public void ClientesRemoveTodos(int idCorp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ClienteRemoverTudo();
            }
            db.Commit();
        }
        public void ClientesResetaTodos(int idCorp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ClientesResetaTodos();
            }
            db.Commit();
        }

        public void ItemRemoverTudo(int idCorp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ItemRemoverTudo();
            }
            db.Commit();
        }

        public void ItemRedefineCodUnicoComIdItem(int idCorp, bool absolutamenteTodos)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ItemRedefineCodUnicoComIdItem(absolutamenteTodos);
            }
            db.Commit();
        }

        public void EstoqueCorrecaoGradeBarras(int idCorp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.EstoqueCorrecaoGradeBarras();
            }
            db.Commit();
        }
        /*
        public void ItemJogaCodAuxParaCodUnico(int bancoID)
        {
            setBancoID(bancoID);
            cTecnico.ItemJogaCodAuxParaCodUnico();
            Commit(bancoID);
        }
        public void ResetaCorpListas(int idCorp, int idEmp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ResetaCorpListas();
            }
            db.Commit();
        }
        */
        public void ResetaEmpListas(int idCorp, int idEmp)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                cTecnico.ResetaEmpListas(idEmp);
            }
            db.Commit();
        }

    }
}
