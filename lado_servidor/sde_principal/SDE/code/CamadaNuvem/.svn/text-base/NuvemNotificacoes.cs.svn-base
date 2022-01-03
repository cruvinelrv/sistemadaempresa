using System;
using System.Collections.Generic;
using System.Collections;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Outros;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SDE.CamadaNuvem
{
    public class NuvemNotificacoes : SuperNuvem
    {
        public List<Atualizacao> Lista_Notificacoes(int idCorp, int ultima)
        {
            List<Atualizacao> ls = AppFacade.get.cacheDados.getAtualizacoes(idCorp, ultima);
            return ls;
        }
    }

}