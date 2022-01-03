using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Db4objects.Db4o;
using SDE.Enumerador;
using SDE.Entidade;
using SDE.CamadaServico;
using SDE.CamadaControle;
using System.Reflection;
using SDE.CamadaNuvem;

/* Após o almoço, trocar o componete DropDownList por ListBox, por conhecar melhor as propriedades e eventos.
         <asp:DropDownList ID="ddlCorp" runat="server"  OnSelectedIndexChanged="ddlCorp_SelctedIndex_Click"></asp:DropDownList>
         --> 
 */

namespace SDE.code.Painel
{
    public partial class ConverteUnidMed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
            //Autenticação: Contexão Banco + Chama banco DB4o
            GerenteConectividadeBancoDados bancos = AppFacade.get.conexaoBanco;
            IObjectContainer db0 = bancos.get(0,GerenteConectividadeBancoDados.TipoBanco.cadastros);

            btnGC.Text = "Gera Correção";
            
            ltbEmpresas.Items.Clear();
            foreach (LoginEmpresa le in db0.Query<LoginEmpresa>())
            {
                //ltbEmpresas.Items = Utils.QuickSort.Ordenar(ltbEmpresas.Items);
                ltbEmpresas.Items.Add(
                    new ListItem(le.idCorp + ". " + le.empresa, le.idCorp.ToString())
                    );
            }

        }

        protected void Gera_Correcao_Click(object sender, EventArgs e) 
        {

            // validações!
            
            
            
            /*
            foreach (int i in bancos)
            {
                IObjectContainer bd = bancos.get(1, GerenteConectividadeBancoDados.TipoBanco.cadastros);

            }
            */
        }
    }
}
