using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.CamadaControle;
using SDE.Constantes;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void SdeConfig_Reseta(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Object o in db.Query<SdeConfig>())
                        db.Delete(o);

                    FieldInfo[] campos = typeof(Constantes.Variaveis_SdeConfig).GetFields();

                    AppFacade.get.reaproveitamento.setIncremento(db, typeof(SdeConfig), 0);
                    int proxID = AppFacade.get.reaproveitamento.getIncremento(db, typeof(SdeConfig), campos.Length);
                    
                    foreach (FieldInfo f in campos)
                    {
                        string[] aaa = (string[])f.GetValue(null);
                        SdeConfig c = new SdeConfig();
                        c.id = proxID++;
                        c.prioridade = 0;
                        c.variavel = aaa[0];
                        c.valor = aaa[1];

                        db.Store(c);
                    }

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public List<SdeConfig> SdeConfig_NovosAtualizacoes(int idCorp, List<SdeConfig> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (SdeConfig xxx in objetos)
                    {
                        if (xxx.id == 0)
                        {
                            //se trata de uma instancia NOVA
                            //ProximoCampo prox = new ProximoCampo(idCorp);
                            xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(SdeConfig), 0);
                            //salva
                            dbStore(db, xxx);
                        }
                        else
                        {
                            //estou alterando uma instancia
                            IQuery q = db.Query();//SELECT *
                            q.Constrain(typeof(SdeConfig));//FROM SdeConfig
                            q.Descend("id").Constrain(xxx.id);//WHERE id = 3
                            IObjectSet r = q.Execute();
                            SdeConfig sdeConf = (SdeConfig)r[0];
                            Utils.copiaCamposBasicos(xxx, sdeConf);
                            //salva
                            dbStore(db, sdeConf);
                        }
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return null;
                }
            }
        }
    }


}