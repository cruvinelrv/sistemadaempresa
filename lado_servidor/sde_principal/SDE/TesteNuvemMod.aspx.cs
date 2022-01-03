using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SDE.CamadaNuvem;
using SDE.Entidade;
using System.Reflection;


namespace SDE
{
    public partial class TesteNuvemMod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            c.nome = "Teste Incremento " + DateTime.Now.ToShortTimeString();
            c.apelido_razsoc = "Teste Incremento " + DateTime.Now.ToShortTimeString();
            c.cpf_cnpj = "00407672133";

            NuvemModificacoes nMod = new NuvemModificacoes();
            c = nMod.Cliente_Novo(3, c);

            foreach (FieldInfo f in typeof(Cliente).GetFields())
            {
                object val = f.GetValue(c);
                if (val == null)
                    continue;
                string s = string.Format("<p><strong>{0}: {1}</strong></p>", f.Name, val);
                Response.Write(s);
            }

        }
    }
}
