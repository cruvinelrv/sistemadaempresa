using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDE.Web
{
    public partial class detalhes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["transpondo-cDados-listas"] == null)
                return;

            SortedList sl = (SortedList)Application["transpondo-cDados-listas"];
            List<String> passos = (List<String>)Application["transpondo-cDados-passos"];
            form1.InnerText = "";

            StringBuilder sb = new StringBuilder();
            /**/
            foreach (int idCorp in sl.Keys)
            {
                form1.InnerHtml += string.Format( "{0} -- {1}<br/>", idCorp, sl[idCorp] );
            }
            /**/
            foreach (string passo in passos)
            {
                form1.InnerHtml += string.Format("{0}<br/>", passo);
            }
            /**/
            
        }
    }
}
