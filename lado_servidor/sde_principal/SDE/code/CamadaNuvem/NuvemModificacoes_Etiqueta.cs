using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using System.Text;
using System.Globalization;
using System.IO;
using SDE.PDF;
using SDE.code.ConfiguracoesArgox;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        //coisa coisado

        #region Espaço Mamãe Bebê

        public void EscreveEtiquetaPorMovEMB(int idCorp, int idEmp, int idMov, string portaCOM)
        {
            defineCorp(idCorp);
            IQuery query;

            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);

            List<MovItem> listaMovItem = new List<MovItem>();
            foreach (MovItem mi in query.Execute())
                listaMovItem.Add(mi);
            EspacoMamaeBebe.EscreveEtiqueta(idCorp, idEmp, listaMovItem, portaCOM);
        }

        public void EscreveEtiquetaPorListaEMB(int idCorp, int idEmp, List<MovItem> listaMovItem, string portaCOM)
        {
            EspacoMamaeBebe.EscreveEtiqueta(idCorp, idEmp, listaMovItem, portaCOM);
        }

        #endregion

        #region Obra Densa

        public void EscreveEtiquetaPorMovObraDensa(int idCorp, int idEmp, int idMov, string portaCOM)
        {
            defineCorp(idCorp);
            IQuery query;

            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);

            List<MovItem> listaMovItem = new List<MovItem>();
            foreach (MovItem mi in query.Execute())
                listaMovItem.Add(mi);
            ObraDensa.EscreveEtiqueta(idCorp, idEmp, listaMovItem, portaCOM);
        }

        public void EscreveEtiquetaPorListaObraDensa(int idCorp, int idEmp, List<MovItem> listaMovItem, string portaCOM)
        {
            ObraDensa.EscreveEtiqueta(idCorp, idEmp, listaMovItem, portaCOM);
        }

        #endregion

        #region Pintando 7

        public void escreveEtiquetaPorMovSETE(int idCorp, int idEmp, int idMov)
        {
            defineCorp(idCorp);
            IQuery query;

            List<MovItem> listaMovItem = new List<MovItem>();
            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);
            foreach (MovItem movItem in query.Execute())
                listaMovItem.Add(movItem);

            ConstrutorPDF.EtiquetaSETE(idCorp, idEmp, listaMovItem);
        }

        public void escreveEtiquetaPorListaSETE(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            ConstrutorPDF.EtiquetaSETE(idCorp, idEmp, listaMovItem);
        }

        #endregion

        #region Antonieta Casa

        public void escreveEtiquetaPorMovAntonietaCasa(int idCorp, int idEmp, int idMov)
        {
            defineCorp(idCorp);
            IQuery query;

            List<MovItem> listaMovItem = new List<MovItem>();
            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);
            foreach (MovItem movItem in query.Execute())
                listaMovItem.Add(movItem);

            ConstrutorPDF.EtiquetaAntonietaCasa(idCorp, idEmp, listaMovItem);
        }

        public void escreveEtiquetaPorListaAntonietaCasa(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            ConstrutorPDF.EtiquetaAntonietaCasa(idCorp, idEmp, listaMovItem);
        }

        #endregion

        #region Wembley

        public void escreveEtiquetaPorMovWembley(int idCorp, int idEmp, int idMov)
        {
            defineCorp(idCorp);
            IQuery query;

            List<MovItem> listaMovItem = new List<MovItem>();
            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);
            foreach (MovItem movItem in query.Execute())
                listaMovItem.Add(movItem);

            ConstrutorPDF.EtiquetaWembley(idCorp, idEmp, listaMovItem);
        }

        public void escreveEtiquetaPorListaWembley(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            ConstrutorPDF.EtiquetaWembley(idCorp, idEmp, listaMovItem);
        }

        #endregion

        #region Costa Azul

        public void escreveEtiquetaPorMovCostaAzul(int idCorp, int idEmp, int idMov)
        {
            defineCorp(idCorp);
            IQuery query;

            List<MovItem> listaMovItem = new List<MovItem>();
            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);
            foreach (MovItem movItem in query.Execute())
                listaMovItem.Add(movItem);

            ConstrutorPDF.EtiquetaCostaAzul(idCorp, idEmp, listaMovItem);
        }

        public void escreveEtiquetaPorListaCostaAzul(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            ConstrutorPDF.EtiquetaCostaAzul(idCorp, idEmp, listaMovItem);
        }

        #endregion

        #region Moda Morena

        public void escreveEtiquetaPorMovModaMorena(int idCorp, int idEmp, int idMov)
        {
            defineCorp(idCorp);
            IQuery query;

            List<MovItem> listaMovItem = new List<MovItem>();
            query = db.Query();
            query.Constrain(typeof(MovItem));
            query.Descend("idMov").Constrain(idMov);
            foreach (MovItem movItem in query.Execute())
                listaMovItem.Add(movItem);

            ConstrutorPDF.EtiquetaModaMorena(idCorp, idEmp, listaMovItem);
        }

        public void escreveEtiquetaPorListaModaMorena(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            ConstrutorPDF.EtiquetaModaMorena(idCorp, idEmp, listaMovItem);
        }

        #endregion
    }
}
