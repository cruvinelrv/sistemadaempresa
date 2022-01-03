using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SDE.Outros
{
    public class Atualizacao
    {
        public string classe;
        public int idObj, idAtualizacao;//, idClienteFuncionarioLogado;
        public object obj;
        public bool ehInsercao;
    }
}
