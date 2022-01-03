using System;
using System.Collections.Generic;
using System.Collections;

using SDE.Atributos;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SDE.CamadaNuvem
{
    public class NuvemListagem : SuperNuvem
    {
        public IList ListaDB(int idCorp, string classe)
        {
            try
            {
                //valores padrão
                IObjectContainer dbX = null;
                string fieldNameOrdenador = null;
                //
                Type t = Type.GetType(string.Concat("SDE.Entidade.", classe));
                object[] attribs = t.GetCustomAttributes(typeof(AtEnt), false);

                if (attribs.Length > 0)
                {
                    AtEnt a = (AtEnt)attribs[0];
                    fieldNameOrdenador = a.primaryKey;
                    if (a.banco == EnumBanco.bancoZero)
                        dbX = db0;
                    else
                    {
                        defineCorp(idCorp);
                        dbX = db;
                    }
                }
                else
                {
                    defineCorp(idCorp);
                    fieldNameOrdenador = "id";
                    dbX = db;
                }

                IList ls = AppFacade.get.reaproveitamento.Lista(dbX, t, fieldNameOrdenador);

                //string s = "classe: "+classe+" | ls: "+ls.Count+" | contains0: "+classes_db0.Contains(classe).ToString()+" | fieldnameOrdenador: "+fieldNameOrdenador+Environment.NewLine;
                //System.IO.File.AppendAllText(@"c:\inetpub\web\saas.txt", s);
                return ls;
            }
            catch
            {

                throw new Exception("classe: "+classe);
            }
        }
    }

}