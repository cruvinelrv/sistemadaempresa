using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Db4objects.Db4o;
using SDE.CamadaServico;
using SDE.Entidade;
using System.Threading;
using System.Text;

namespace SDE
{
    public partial class injecao : System.Web.UI.Page
    {

        private static void anota(string s)
        {
            AppFacade.get.msg = string.Format("<br/>{0} <br/> {1}", DateTime.Now.ToString("HH:mm:ss"), s);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblLog.Text = AppFacade.get.msg;
            

            if (cmbArquivos.Items.Count > 0)
                return;

            cmbArquivos.Items.Add("escolha...");

            string pasta = Request.PhysicalApplicationPath + "App_Data\\";
            string[] arqs = Directory.GetFiles(pasta, "*.db");
            foreach (string arq in arqs)
            {
                cmbArquivos.Items.Add(new ListItem(arq.Replace(pasta, ""), arq));
            }

        }

        protected void btnInjetar_Click(object sender, EventArgs e)
        {
            int idCorp = int.Parse(txtCorp.Text);
            if (idCorp <= 0)
                return;
            Context.Items["idCorp"] = idCorp;
            if (cmbArquivos.SelectedIndex == 0)
                return;
            string arq = cmbArquivos.SelectedValue;
            using (IObjectContainer dbFONTE = Db4oFactory.OpenFile(arq))
            {
                importaItens(dbFONTE, idCorp);
            }
        }

        protected void importaItens(IObjectContainer dbFONTE, int idCorp)
        {
            IList<Item> itens = dbFONTE.Query<Item>();

            try
            {
                
                //ThreadStart ts = new ThreadStart(delegate()
                {

                    SItem sItem = new SItem();
                    int contador = 0;
                    foreach (Item itemFONTE in itens)
                    {

                        contador++;
                        //sb.AppendLine(contador.ToString());

                        anota("invocando novo " + contador);
                        Item it = sItem.Novo(idCorp, 1, itemFONTE, "");
                        itemFONTE.id = it.id;
                        anota("invocando atualizar " + contador);
                        it = sItem.Atualizar(idCorp, 1, itemFONTE, true);
                    }
                    anota("terminei " + contador);
                }
                //);
                //new Thread(ts).Start();
            }
            catch (Exception ex)
            {

                anota(ex.Message+" - "+ex.StackTrace);
                throw;
            }
            
            

        }


    }
}