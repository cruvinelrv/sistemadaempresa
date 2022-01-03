using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SDE.Entidade;
using System.Collections.Generic;
using SDE.CamadaServico;
using SDE.Parametro;
using System.Globalization;

namespace SDE.Web
{
    public partial class notaprefeitura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idCorp = int.Parse(Request.QueryString["idCorp"]);
            int idMov = int.Parse(Request.QueryString["idMov"]);

            SMov sm = new SMov();
            SCorp scp = new SCorp();
            SCliente sc = new SCliente();
            Mov mov = new Mov();
            ClienteEndereco ce = new ClienteEndereco();

            ParamLoadMov plm = new ParamLoadMov() { itens = true, movItens = true, movValores = true };
            ParamLoadCliente plc = new ParamLoadCliente() { enderecos = true };

            /*CABEÇALHO*/
            mov = sm.Load(idCorp, idMov, plm);
            CFOP cfop = scp.Load_CFOP(mov.cfop);
            pCodOp.InnerText = mov.cfop;
            pNaturezaOp.InnerText = cfop.descricao;
            pLocalNoMunicipio.InnerText = (mov.noMun) ? "x" : "";
            pLocalForaMunicipio.InnerText = (mov.foraMun) ? "x" : "";
            DateTime dt = DateTime.Parse(mov.dtNF);
            pDataEmissao.InnerText = dt.ToString("dd/MM/yyyy");

            /*DADOS DO CLIENTE*/
            mov.__cli = sc.Load(idCorp, mov.idCliente, plc);
            ce = sc.LoadEnderecoPorId(mov.idClienteEndereco);
            pCliente.InnerText = mov.__cli.apelido_razsoc;
            pCod.InnerText = mov.__cli.id.ToString();
            pEndereco.InnerText = ce.logradouro;
            pSetor.InnerText = ce.bairro;
            pCidade.InnerText = ce.cidade;
            pUf.InnerText = ce.uf;
            pCep.InnerText = ce.cep;
            pCnpjCpf.InnerText = mov.__cli.cpf_cnpj;
            pInscrMun.InnerText = ce.inscrMun;
            pInscrEst.InnerText = ce.inscr;

            /*FATURA*/
            pFatura.InnerText = mov.fatura;

            /*DADOS DOS SERVIÇOS*/
            Decimal totalBrutoNota = 0;
            Decimal totalFinalNota = 0;
            int nLinhas = mov.__mItens.Count;
            TableRow tr = new TableRow();
            for (int i = 0; i < 9; i++)
            {
                tr = new TableRow();
                if (nLinhas > 0)
                {
                    tr.Cells.Add(new TableCell() { Text = mov.__mItens[i].qtd.ToString(), CssClass = "col1" });
                    tr.Cells.Add(new TableCell() { Text = mov.__mItens[i].__item.unidMed.ToString(), CssClass = "col2" });
                    tr.Cells.Add(new TableCell() { Text = mov.__mItens[i].__item.nome, CssClass = "col3" });
                    tr.Cells.Add(new TableCell() { Text = formatMoney(Decimal.Parse(mov.__mItens[i].vlrUnitVendaFinal.ToString())), CssClass = "col4" });
                    tr.Cells.Add(new TableCell() { Text = formatMoney(Decimal.Parse((mov.__mItens[i].vlrUnitVendaFinal * mov.__mItens[i].qtd).ToString())), CssClass = "col5" });
                    totalBrutoNota += Decimal.Parse((mov.__mItens[i].vlrUnitVendaFinal * mov.__mItens[i].qtd).ToString());
                    nLinhas--;
                }
                else
                    tr.Cells.Add(new TableCell() { Text = ".", CssClass = "col1" });
                tServicos.Rows.Add(tr);
                /*
                //inserir observação sobre o produto aqui
                tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = ".", CssClass = "col1" });
                tServicos.Rows.Add(tr);
                 * */
            }

            tr = new TableRow();
            tr.Cells.Add(new TableCell() { Text = "", CssClass = "col1" });
            tr.Cells.Add(new TableCell() { Text = "", CssClass = "col2" });
            tr.Cells.Add(new TableCell() { Text = "", CssClass = "col3" });
            tr.Cells.Add(new TableCell() { Text = "SUBTOTAL", CssClass = "col4" });
            tr.Cells.Add(new TableCell() { Text = formatMoney(totalBrutoNota), CssClass = "col5" });
            tServicos.Rows.Add(tr);

            //calculos de retenção
            tr = new TableRow();
            if (mov.retencaoISSQN != 0)
            {
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col1" });
                tr.Cells.Add(new TableCell() { Text = "-", CssClass = "col2" });
                tr.Cells.Add(new TableCell() { Text = "RETENCAO ISSQN : " + mov.retencaoISSQN.ToString() + "%", CssClass = "col3" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col4" });
                tr.Cells.Add(new TableCell() { Text = formatMoney(calculaISSQN(totalBrutoNota, Decimal.Parse(mov.retencaoISSQN.ToString()))), CssClass = "col5" });
            } else
                tr.Cells.Add(new TableCell() { Text = ".", CssClass = "col1" });
            tServicos.Rows.Add(tr);

            tr = new TableRow();
            if (mov.retencaoISSQN != 0)
            {
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col1" });
                tr.Cells.Add(new TableCell() { Text = "-", CssClass = "col2" });
                tr.Cells.Add(new TableCell() { Text = "RETENCAO DE INSS : " + mov.retencaoINSS.ToString() + "%", CssClass = "col3" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col4" });
                tr.Cells.Add(new TableCell() { Text = formatMoney(calculaINSS(totalBrutoNota, Decimal.Parse(mov.retencaoINSS.ToString()))), CssClass = "col5" });
            } else
                tr.Cells.Add(new TableCell() { Text = ".", CssClass = "col1" });
            tServicos.Rows.Add(tr);

            //ISSQN = calculaISSQN(totalBrutoNota, ISSQN);
            //INSS = calculaINSS(totalBrutoNota, INSS);
            totalFinalNota = totalBrutoNota + (calculaISSQN(totalBrutoNota, Decimal.Parse(mov.retencaoISSQN.ToString())) + calculaINSS(totalBrutoNota, Decimal.Parse(mov.retencaoINSS.ToString())));

            /*RODAPE*/
            pTotal.InnerText = formatMoney(totalFinalNota);
            pObs.InnerText = mov.obs;
        }

        private Decimal calculaISSQN(Decimal totalBrutoNota, Decimal ISSQN)
        {
            return Decimal.Negate((totalBrutoNota * ISSQN) / 100);
        }

        private Decimal calculaINSS(Decimal totalBrutoNota, Decimal INSS)
        {
            return Decimal.Negate((totalBrutoNota * INSS) / 100);
        }

        private string formatMoney(Decimal d)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:N}", d);
        }
    }
}
