using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SDE.CamadaServico;

namespace SDE.Web
{
    /// <summary>
    /// Summary description for DownloadNFE
    /// </summary>
    [WebService(Namespace = "http://sde.sistemadaempresa.com.br/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DownloadNFE : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] getNFE(int idCorp, int idMov)
        {
            SNfe snfe = new SNfe();
            string[] r = snfe.GerarTXT(idCorp, idMov);
            return r;
        }
    }
}
