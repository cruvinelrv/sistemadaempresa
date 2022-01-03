using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SDE.Outros;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;


namespace SDE.CamadaControle
{
    public class CBanco
    {
        public Objeto Load(IObjectContainer db, long uuID)
        {
            object objSDE = db.Ext().GetByID(uuID);
            db.Activate(objSDE, 1);

            return (objSDE == null)
                ? null
                : Load(db, uuID, objSDE);
        }

        private Objeto Load(IObjectContainer db, long uuID, object __obj)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Type t = __obj.GetType();
            Objeto obj = new Objeto();
            obj.campos = new List<object>();
            obj.classe = t.Name;
            obj.uuID = uuID;
            foreach (FieldInfo f in t.GetFields())
            {
                Objeto o = new Objeto();
                o.classe = f.FieldType.Name;
                o.campo = f.Name;
                //Verifica se Field é Collection ou Objeto de Multisoft.Sistema

                if (!(f.FieldType.FullName.Contains("System.Collections")) /*&& !(f.FieldType.FullName.Contains("fib"))*/)
                {
                    o.valor = f.GetValue(__obj);
                    o.primitivo = true;
                }
                o.uuID = db.Ext().GetID(f.GetValue(__obj));
                obj.campos.Add(o);
            }
            return obj;
        }

        public IList Lista(IObjectContainer db, string tipo)
        {
            IList ret = new ArrayList();

            Assembly asm = Assembly.GetExecutingAssembly();
            Type t = asm.GetType(tipo);
            IList lsSDE = db.Query(t);

            foreach (Object objSDE in lsSDE)
            {
                db.Activate(objSDE, 1);

                Objeto obj = new Objeto();
                obj.classe = objSDE.GetType().Name;
                obj.uuID = db.Ext().GetID(objSDE);
                obj.valor = Load(db, obj.uuID, objSDE);
                ret.Add(obj);
            }

            return ret;
        }

        public void EditaCampo(IObjectContainer db, long uuID, string campo, object valor)
        {
            Object oInstancia = db.Ext().GetByID(uuID);
            db.Activate(oInstancia, 1);
            FieldInfo f = oInstancia.GetType().GetField(campo);

            if (f.FieldType == typeof(Boolean) || (valor as string == "true") || (valor as string == "false"))
                valor = Convert.ToBoolean(valor);
            if (f.FieldType == typeof(Double))
                valor = Convert.ToDouble(valor);
            if (f.FieldType == typeof(Int32))
                valor = Convert.ToInt32(valor);
            if (f.FieldType == typeof(Int64))
                valor = Convert.ToInt64(valor);

            f.SetValue(oInstancia, valor);

            db.Store(oInstancia);
            db.Commit();
        }

    }
}
