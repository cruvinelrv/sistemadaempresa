using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDE.Entidade;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Text;
using System.Globalization;
using SDE.Enumerador;
using System.Reflection;
using System.Collections;

namespace SDE
{
    public partial class devtesting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                sb.Append("<table class='etiqueta'>");
                sb.Append("<thead/>");

                /*
                sb.AppendFormat("<tr><td>{0}</td></tr>", "000003 - CAMISA TRICOLINE M/L");
                sb.AppendFormat("<tr><td>{0}</td></tr>", "Ref: 1187");
                sb.AppendFormat("<tr><td>{0}</td></tr>", "A Vista: R$42,00. A Prazo: 4x R$11,29");
                sb.AppendFormat("<tr><td>{0}<td></tr>", "Entrada, 30, 60 e 90 dias");
                sb.AppendFormat("<tr><td>{0}</td></tr>", "Taxa de Juros 7,51% ao mês");
                 * */

                sb.AppendFormat("<tr><td>{0}</td></tr>", "INICIO2");
                sb.AppendFormat("<tr><td>{0}</td></tr>", ".");
                sb.AppendFormat("<tr><td>{0}</td></tr>", ".");
                sb.AppendFormat("<tr><td>{0}<td></tr>", ".");
                sb.AppendFormat("<tr><td>{0}</td></tr>", "FIM2");

                sb.Append("</table>");
            }

            Literal1.Text = sb.ToString();
        }
    }
}
