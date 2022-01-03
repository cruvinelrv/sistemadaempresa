using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SDE.Entidade;
using SDE.CamadaServico;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

using System.Threading;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using SDE.Atributos;
using SDE.CamadaControle;

namespace SDE.MetodoCorretivo
{
    public class MetodoCorretivo
    {

        Dictionary<int, string> sl = new Dictionary<int, string>();
        List<String> passos = new List<String>();

        public void metodoCorretivo()
        {
            transpoeMovNFE();
        }

        void anota(int idCorp, string valor)
        {
            //lock (tranca)
            {
                sl[idCorp] = valor;
                StringBuilder sb = new StringBuilder();
                foreach (int key in sl.Keys)
                {
                    sb.AppendLine("corp: " + key + ": " + sl[key]);
                }
                //File.WriteAllText("C:\\Inetpub\\Web\\lalala.txt", sb.ToString());
            }
        }

        void anota2(string passo)
        {
            string s = DateTime.Now.ToString("HH:mm:ss fffffff") + "  -  " + passo + "\r\n";
            passos.Add(s);
            StringBuilder sb = new StringBuilder();
            foreach (string valor in passos)
            {
                sb.AppendLine("passos" + valor);
            }
            //File.WriteAllText("C:\\Inetpub\\Web\\lelele.txt", sb.ToString());
        }

        void transpoeMovNFE()
        {
            return;
            IObjectContainer db0 = AppFacade.get.conexaoBanco.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            anota2("iniciando - conversao pessoa para cliente");
            Max max = db0.Query<Max>()[0];


            //Cliente cConsumidor = new Cliente() { id = 1, cpf_cnpj = "00000000000", nome = "CLIENTE CONSUMIDOR", apelido_razsoc = "CLIENTE CONSUMIDOR", dtNasc = "01/01/1900", tipo = SDE.Enumerador.EPesTipo.Fisica, loginUsuario = "ADMIN", loginSenha = "ADMIN" };


            for (int idCorp = 1; idCorp <= max.idCorporacao; idCorp++)
            {
                object[] dados = new object[] { idCorp };
                thread_transpoeUmaCorporacao(dados);
            }
            
        }


        public void thread_transpoeUmaCorporacao(object data)
        {
            
            object[] dados = (object[])data;
            int idCorp = Convert.ToInt32(dados[0]);
            anota(idCorp, ":(");
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);


            try
            {




                anota(idCorp, ":)");
                if (idCorp == 2) anota2("Finalizado");
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("excep! {0} - {1} / {2}", idCorp, ex.StackTrace, ex.Message));
                //anota2(string.Format("excep! {0} - {1} / {2}", idCorp, ex.StackTrace, ex.Message));
            }
        }

    }
}
