using System;
using System.Collections;
using System.Collections.Generic;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;

namespace SDE.CamadaServico
{
    public class SAtualizacao : SuperServico
    {

        public IEnumerable PegaAtualizacoes(int idCorp)
        {
            defineCorp(idCorp);
            try
            {
                //lock (db.Ext().Lock())
                //db.Commit();
                return null;

            }
            catch (Exception ex)
            {
                throw anotaErro(ex);
            }
            
        }




    }
}
