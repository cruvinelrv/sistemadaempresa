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
using SDE.RelatoriosPDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public int RelListaTelefone(int idCorp, int idEmp)
        {
            RelListaTelefone rel = new RelListaTelefone();
            return rel.GeraReListaTelefone(idCorp, idEmp);
        }

        public int RelFichaCliente(int idCorp, int idEmp)
        {
            RelFichaCliente rel = new RelFichaCliente();
            return rel.GeraRelFichaCliente(idCorp, idEmp);
        }

        public int RelListaPrecos(int idCorp, int idEmp, int idMarca)
        {
            RelListaPreco rel = new RelListaPreco();
            return rel.GeraRelListaPreco(idCorp, idEmp, idMarca);
        }

        public int RelListagemBalanco(int idCorp, int idEmp, string campoOrdenacao)
        {
            RelListagemBalanco rel = new RelListagemBalanco();
            return rel.GeraRelListagemBalanco(idCorp, idEmp, campoOrdenacao);
        }
        public int RelListaProdutoTributacao(int idCorp, int idEmp, int idMarca)
        {
            RelListaProdutoTributacao rel = new RelListaProdutoTributacao();
            return rel.GeraRelListaProdutoTributacao(idCorp, idEmp, idMarca);
        }
    }
} 
