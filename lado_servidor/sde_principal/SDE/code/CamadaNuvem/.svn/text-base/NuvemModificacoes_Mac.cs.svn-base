using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using SDE.Entidade;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void getMac(int idCorp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            SdeConfig mac = new SdeConfig();
            String strMac = "";
            
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Name == "Conexão local")
                {
                    PhysicalAddress address = adapter.GetPhysicalAddress();
                    strMac = address.ToString();
                }
            }
            query = db.Query();
            query.Constrain(typeof(SdeConfig));
            query.Descend("variavel").Constrain("Empresa.Endereço.MAC");
            IObjectSet rs_mac = query.Execute();

            if (rs_mac.Count > 0)
            {
                mac = rs_mac[0] as SdeConfig;
            }

            if (rs_mac.Count == 0)
            {
                
            }

        }
    }
}