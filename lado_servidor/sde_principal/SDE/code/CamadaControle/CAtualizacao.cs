using System;
using System.Collections.Generic;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;


namespace SDE.CamadaControle
{
    public class CAtualizacao : SuperControle
    {
        public CAtualizacao(int idCorp):base(idCorp) { }
        public void Anota(int idObj, object obj)
        {
            throw new NotImplementedException();
            //AppFacade.get.getAnotacoesAtualizacoes(idCorp).AdicionaAsNotificacoes(idObj, obj);
        }

    }
}
