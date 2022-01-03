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
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.Entidade;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SDE.PDF
{
    public partial class ConstrutorPDF
    {
        public static void GeraCarne(int idCorp, int IdMov)
        {
            if (idCorp != 10 && idCorp != 71)
                throw new Exception("O gerador de carnê SDE não está devidamente configurado para esta empresa " + idCorp.ToString());

            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\carne.pdf";

            query = db.Query();
            query.Constrain(typeof(Mov));
            query.Descend("id").Constrain(IdMov);
            IObjectSet rs_mov = query.Execute();
            Mov mov = rs_mov[0] as Mov;

            query = db.Query();
            query.Constrain(typeof(Finan_Titulo));
            query.Descend("idTransacao").Constrain(mov.idTransacao);
            IObjectSet rs_ft = query.Execute();
            Finan_Titulo ft = rs_ft[0] as Finan_Titulo;

            query = db.Query();
            query.Constrain(typeof(Finan_TituloItem));
            query.Descend("idTitulo").Constrain(ft.id);
            query.Descend("parcela").OrderAscending();
            IObjectSet rs_fti = query.Execute();

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(mov.idClienteFuncionarioVendedor);
            IObjectSet nome_vendedor = query.Execute();
            Cliente nome_vend = nome_vendedor[0] as Cliente;


            Document document = new Document(PageSize.A4, 30, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();

            try
            {
                BaseFont bfHelvica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                iTextSharp.text.Font helvica = new iTextSharp.text.Font(bfHelvica, 6, iTextSharp.text.Font.COURIER, Color.BLACK);
                iTextSharp.text.Font helvicaPq = new iTextSharp.text.Font(bfHelvica, 4, iTextSharp.text.Font.COURIER, Color.BLACK);
                iTextSharp.text.Font helvicaB = new iTextSharp.text.Font(bfHelvica, 8, iTextSharp.text.Font.BOLD, Color.BLACK);
                iTextSharp.text.Font helvicaU = new iTextSharp.text.Font(bfHelvica, 8, iTextSharp.text.Font.COURIER, Color.BLACK);
                iTextSharp.text.Font helvicaBranco = new iTextSharp.text.Font(bfHelvica, 2, iTextSharp.text.Font.BOLD, Color.BLACK);
                iTextSharp.text.Font helvicaMargemInt = new iTextSharp.text.Font(bfHelvica, 4, iTextSharp.text.Font.COURIER, Color.GRAY);
                iTextSharp.text.Font helvicaPontilhado = new iTextSharp.text.Font(bfHelvica, 8, iTextSharp.text.Font.COURIER, Color.GRAY);
                int x = 1;

                foreach (Finan_TituloItem xFti in rs_fti)
                {
                    if (x == 5)
                    {
                        x = 1;
                    }
                    if (x == 1)
                    {
                        PdfPTable tb = new PdfPTable(1);
                        PdfPCell cell = new PdfPCell(new Phrase(" ", helvicaMargemInt));
                        cell.Border = 0;
                        tb.AddCell(cell);
                        cell = new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", helvicaPontilhado));
                        cell.Border = 0;
                        tb.AddCell(cell);
                        cell = new PdfPCell(new Phrase(" ", helvicaMargemInt));
                        cell.Border = 0;
                        tb.AddCell(cell);
                        tb.WidthPercentage = 100;
                        document.Add(tb);
                    }
                    PdfPTable pptPrincipal = new PdfPTable(2);

                    //Divisão Tabela Principal
                    float[] colPrincipal = new float[2];
                    colPrincipal[0] = 45;
                    colPrincipal[1] = 55;

                    pptPrincipal.SetTotalWidth(colPrincipal);

                    pptPrincipal.WidthPercentage = 100;

                    //Canhoto
                    PdfPTable pptCanhotoLogo = new PdfPTable(3);

                    float[] colCanhoto = new float[3];
                    colCanhoto[0] = 10;
                    colCanhoto[1] = 30;
                    colCanhoto[2] = 50;

                    pptCanhotoLogo.SetTotalWidth(colCanhoto);
                    pptCanhotoLogo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    pptCanhotoLogo.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;

                    PdfPCell cellLogoVencDtv = new PdfPCell();
                    cellLogoVencDtv.Border = 0;
                    cellLogoVencDtv.Colspan = 2;
                    cellLogoVencDtv.Rowspan = 4;

                    //cellLogoVencDtv.AddElement(iTextSharp.text.Image.GetInstance("C:\\dev\\sistemadaempresa\\ambiente_desenvolvimento\\flaviano\\lado_servidor\\sde_flaviano\\SDE\\img\\logo_clientes\\44.JPG"));
                    cellLogoVencDtv.AddElement(iTextSharp.text.Image.GetInstance(@"C:\Inetpub\wwwroot\app\sistemadaempresa_sde\img\logo_clientes\" + idCorp + ".jpg"));
                    pptCanhotoLogo.AddCell(cellLogoVencDtv);
                    Phrase phVencimento = new Phrase("VENCIMENTO", helvica);
                    pptCanhotoLogo.AddCell(phVencimento);
                    Phrase phVencimentoValue = new Phrase(xFti.dtPagamento, helvicaB);
                    pptCanhotoLogo.AddCell(phVencimentoValue);
                    Phrase phDataVenda = new Phrase("DATA VENDA", helvica);
                    pptCanhotoLogo.AddCell(phDataVenda);
                    Phrase phDataVendaValue = new Phrase(xFti.dtLancamento.Substring(0, 10), helvicaB);
                    pptCanhotoLogo.AddCell(phDataVendaValue);

                    Phrase phFoneFax;
                    String Fone="";

                    if (idCorp == 10)
                        Fone = "(64) 3613-0783/3621-7871";
                    else
                        if (idCorp == 71)
                            Fone = "";

                    phFoneFax = new Phrase("fone/fax", helvica);
                    phFoneFax = new Phrase("fone", helvica);

                    //O contato da empresa não está dinâmico... funcionará somente para a Autêntica modas
                    pptCanhotoLogo.AddCell(phFoneFax);
                    
                    Phrase phFoneFaxValue = new Phrase(Fone, helvica);
                    pptCanhotoLogo.AddCell(phFoneFaxValue);
                    Phrase phEspacoBranco = new Phrase(" ", helvicaBranco);
                    pptCanhotoLogo.AddCell(phEspacoBranco);


                    PdfPTable pptValPrestPag = new PdfPTable(3);
                    pptValPrestPag.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    pptValPrestPag.WidthPercentage = 100;

                    Phrase phValPrestacaoC = new Phrase("VALOR DA PRESTAÇÃO", helvica);
                    pptValPrestPag.AddCell(phValPrestacaoC);

                    Phrase phPrestacaoC = new Phrase("PRESTAÇÃO", helvica);
                    pptValPrestPag.AddCell(phPrestacaoC);

                    Phrase phDataPagamento = new Phrase("DATA DO PAGAMENTO", helvica);
                    pptValPrestPag.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;
                    pptValPrestPag.AddCell(phDataPagamento);

                    Phrase phPrestacaoValueC = new Phrase(Utils.formatMoney(Convert.ToDecimal(xFti.valorCobrado), false), helvicaB);
                    pptValPrestPag.AddCell(phPrestacaoValueC);

                    Phrase phNPrestacaoValueC = new Phrase(xFti.parcela.ToString() + "/" + rs_fti.Count.ToString(), helvicaB);
                    pptValPrestPag.AddCell(phNPrestacaoValueC);

                    Phrase phDataPagamentoValue = new Phrase("________________", helvicaU);
                    pptValPrestPag.AddCell(phDataPagamentoValue);

                    PdfPCell cellValPrestPag = new PdfPCell();
                    cellValPrestPag.Border = 0;
                    cellValPrestPag.Colspan = 3;
                    cellValPrestPag.Rowspan = 1;

                    cellValPrestPag.AddElement(pptValPrestPag);

                    pptCanhotoLogo.AddCell(cellValPrestPag);

                    PdfPCell cellCliente = new PdfPCell(new Phrase("CLIENTE", helvica));
                    cellCliente.Border = 0;
                    cellCliente.Colspan = 3;
                    cellCliente.Rowspan = 1;
                    pptCanhotoLogo.AddCell(cellCliente);

                    PdfPCell cellClienteValue;

                    if (mov.cliente_nome.Length > 52)
                    {
                        cellClienteValue = new PdfPCell(new Phrase(mov.cliente_nome.Substring(0, 51), helvicaB));
                    }
                    else
                    {
                        cellClienteValue = new PdfPCell(new Phrase(mov.cliente_nome, helvicaB));
                    }
                    cellClienteValue.Border = 0;
                    cellClienteValue.Colspan = 3;
                    cellClienteValue.Rowspan = 1;
                    pptCanhotoLogo.AddCell(cellClienteValue);

                    pptCanhotoLogo.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    PdfPCell cellContratoVendPgto = new PdfPCell();
                    cellContratoVendPgto.Border = 0;
                    cellContratoVendPgto.Colspan = 3;
                    cellContratoVendPgto.Rowspan = 1;

                    PdfPTable pptContratoVendPgto = new PdfPTable(3);
                    pptContratoVendPgto.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    colCanhoto[0] = 15;
                    colCanhoto[1] = 60;
                    colCanhoto[2] = 30;

                    pptContratoVendPgto.SetTotalWidth(colCanhoto);
                    pptContratoVendPgto.WidthPercentage = 100;

                    Phrase phVendaC = new Phrase("VENDA", helvica);
                    pptContratoVendPgto.AddCell(phVendaC);
                    Phrase phVendedorC = new Phrase("VENDEDOR", helvica);
                    pptContratoVendPgto.AddCell(phVendedorC);
                    Phrase phValorPagtoC = new Phrase("VALOR PAGAMENTO", helvica);
                    pptContratoVendPgto.AddCell(phValorPagtoC);

                    //pptContratoVendPgto.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;

                    Phrase phContratoCValue = new Phrase(IdMov.ToString(), helvicaB);
                    pptContratoVendPgto.AddCell(phContratoCValue);

                    Phrase phVendedorCValue;
                    if (nome_vend.nome.Length > 28)
                    {
                        phVendedorCValue = new Phrase(nome_vend.nome.Substring(0, 27), helvicaB);
                    }
                    else
                    {
                        phVendedorCValue = new Phrase(nome_vend.nome, helvicaB);
                    }
                    pptContratoVendPgto.AddCell(phVendedorCValue);
                    Phrase phValorPagtoCValue = new Phrase("______________", helvicaU);
                    pptContratoVendPgto.AddCell(phValorPagtoCValue);

                    cellContratoVendPgto.AddElement(pptContratoVendPgto);

                    pptCanhotoLogo.AddCell(cellContratoVendPgto);

                    pptCanhotoLogo.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    PdfPCell cellObservacao = new PdfPCell(new Phrase("OBSERVAÇÃO", helvica));
                    cellObservacao.Border = 0;
                    cellObservacao.Colspan = 3;
                    cellObservacao.Rowspan = 1;
                    pptCanhotoLogo.AddCell(cellObservacao);


                    PdfPCell cellObservacaoValue;
                    if (xFti.obs == null)
                        cellObservacaoValue = new PdfPCell(new Phrase("", helvicaB));
                    else
                        cellObservacaoValue = new PdfPCell(new Phrase(xFti.obs, helvicaB));
                    cellObservacaoValue.Border = 0;
                    cellObservacaoValue.Colspan = 3;
                    cellObservacaoValue.Rowspan = 1;
                    pptCanhotoLogo.AddCell(cellObservacaoValue);

                    //Folha
                    PdfPTable pptFolha = new PdfPTable(3);
                    pptFolha.DefaultCell.Border = 0;

                    Phrase phLoja = new Phrase("CÓD. LOJA", helvica);
                    pptFolha.AddCell(phLoja);
                    Phrase phVenda = new Phrase("VENDA", helvica);
                    pptFolha.AddCell(phVenda);
                    Phrase phPrestacao = new Phrase("PRESTAÇÃO", helvica);
                    pptFolha.AddCell(phPrestacao);

                    Phrase phLojaValue = new Phrase(mov.idEmp.ToString(), helvicaB);
                    pptFolha.AddCell(phLojaValue);
                    Phrase phContratoValue = new Phrase(mov.id.ToString(), helvicaB);
                    pptFolha.AddCell(phContratoValue);
                    Phrase phPrestacaoValue = new Phrase(xFti.parcela.ToString() + "/" + rs_fti.Count.ToString(), helvicaB);
                    pptFolha.AddCell(phPrestacaoValue);

                    PdfPCell cellVendedorDataVenda = new PdfPCell();
                    cellVendedorDataVenda.Border = 0;
                    cellVendedorDataVenda.Colspan = 3;
                    cellVendedorDataVenda.Rowspan = 1;

                    PdfPTable pptVendedorDataVenda = new PdfPTable(2);
                    pptVendedorDataVenda.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    pptVendedorDataVenda.WidthPercentage = 100;

                    pptVendedorDataVenda.AddCell(phEspacoBranco);
                    pptVendedorDataVenda.AddCell(phEspacoBranco);

                    Phrase phVendedor = new Phrase("VENDEDOR", helvica);
                    pptVendedorDataVenda.AddCell(phVendedor);

                    Phrase phDataVendaF = new Phrase("DATA DA VENDA", helvica);
                    pptVendedorDataVenda.AddCell(phDataVendaF);



                    pptVendedorDataVenda.AddCell(phVendedorCValue);
                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
                    Phrase phDataVendaValueF = new Phrase(xFti.dtLancamento.Substring(0, 10), helvicaB);//ruthy
                    pptVendedorDataVenda.AddCell(phDataVendaValueF);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    Phrase phTotalCompra = new Phrase("TOTAL DA COMPRA", helvica);
                    pptVendedorDataVenda.AddCell(phTotalCompra);
                    Phrase phVencimentoF = new Phrase("VENCIMENTO", helvica);
                    pptVendedorDataVenda.AddCell(phVencimentoF);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;

                    Phrase phTotalCompraValue = new Phrase(Utils.formatMoney(Convert.ToDecimal(ft.valorCobrado), false), helvicaB);
                    pptVendedorDataVenda.AddCell(phTotalCompraValue);
                    Phrase phVencimentoFValue = new Phrase(xFti.dtPagamento, helvicaB);
                    pptVendedorDataVenda.AddCell(phVencimentoFValue);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    Phrase phValorPrestacao = new Phrase("VALOR DA PRESTAÇÃO", helvica);
                    pptVendedorDataVenda.AddCell(phValorPrestacao);
                    Phrase phDataPag = new Phrase("DATA DO PAGAMENTO", helvica);
                    pptVendedorDataVenda.AddCell(phDataPag);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;

                    Phrase phValorPrestacaoValue = new Phrase(Utils.formatMoney(Convert.ToDecimal(xFti.valorCobrado), false), helvicaB);
                    pptVendedorDataVenda.AddCell(phValorPrestacaoValue);
                    pptVendedorDataVenda.AddCell(phEspacoBranco);
                    pptVendedorDataVenda.AddCell(phEspacoBranco);
                    Phrase phDataPagValue = new Phrase("__________________", helvicaU);
                    pptVendedorDataVenda.AddCell(phDataPagValue);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    Phrase phAcrescimoDesconto = new Phrase("ACRÉSCIMO/DESCONTO", helvica);
                    pptVendedorDataVenda.AddCell(phAcrescimoDesconto);
                    Phrase phValorPagto = new Phrase("VALOR PAGAMENTO", helvica);
                    pptVendedorDataVenda.AddCell(phValorPagto);

                    pptVendedorDataVenda.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;

                    pptVendedorDataVenda.AddCell(phEspacoBranco);
                    pptVendedorDataVenda.AddCell(phEspacoBranco);

                    Phrase phAcrescimoDescontoValue = new Phrase("__________________", helvicaU);
                    pptVendedorDataVenda.AddCell(phAcrescimoDescontoValue);

                    Phrase phValorPagtoValue = new Phrase("__________________", helvicaU);
                    pptVendedorDataVenda.AddCell(phDataPagValue);

                    cellVendedorDataVenda.AddElement(pptVendedorDataVenda);

                    pptFolha.AddCell(cellVendedorDataVenda);

                    PdfPCell cellClienteF = new PdfPCell(new Phrase("NOME DO CLIENTE", helvica));
                    cellClienteF.Border = 0;
                    cellClienteF.Colspan = 3;
                    cellClienteF.Rowspan = 1;
                    pptFolha.AddCell(cellClienteF);

                    cellClienteValue.Border = 0;
                    cellClienteValue.Colspan = 3;
                    cellClienteValue.Rowspan = 1;
                    pptFolha.AddCell(cellClienteValue);

                    pptFolha.DefaultCell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;

                    //Principal
                    pptPrincipal.AddCell(pptCanhotoLogo);
                    pptPrincipal.AddCell(pptFolha);

                    document.Add(pptPrincipal);

                    PdfPTable tb2 = new PdfPTable(1);
                    PdfPCell cell2 = new PdfPCell(new Phrase(" ", helvicaMargemInt));
                    cell2.Border = 0;
                    tb2.AddCell(cell2);
                    cell2 = new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", helvicaPontilhado));
                    cell2.Border = 0;
                    tb2.AddCell(cell2);
                    cell2 = new PdfPCell(new Phrase(" ", helvicaMargemInt));
                    cell2.Border = 0;
                    tb2.AddCell(cell2);
                    tb2.WidthPercentage = 100;
                    document.Add(tb2);

                    x++;
                }
                document.Close();
            }
            catch (Exception ex)
            {
                document.Close();
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
