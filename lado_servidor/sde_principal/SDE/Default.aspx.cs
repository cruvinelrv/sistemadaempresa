using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDE.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            MyDB4O.BancoProxy.getFromContext().get(0).Dispose();
            Response.Write("limpou");
             * */
            //Trace.IsEnabled = true;
            AppFacade a = AppFacade.get;
            Object db = a.conexaoBanco.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);


            Response.Write("<h1>ESSA É PAGINA DEFAULT :)</h1>");
            Response.Write("<h1></h1>");
            Response.Write("<h1>19190</h1>");

            Response.Write("<h1>Query String:</h1>");
            foreach (string key in Request.QueryString.Keys)
                Response.Write(string.Format("<cDados><br/>key: {0}<br/>value: {1}</cDados>", key, Request.QueryString[key]));


            /*
            Response.Write("<h1>Headers:</h1>");
            foreach (string key in Request.Headers.Keys)
                Response.Write(string.Format("<p><br/>key: {0}<br/>value: {1}</p>", key, Request.Headers[key]));

            Response.Write("<h1>Params:</h1>");
            foreach (string key in Request.Params.Keys)
                Response.Write(string.Format("<p><br/>key: {0}<br/>value: {1}</p>", key, Request.Params[key]));
            */

        }
    }
}
