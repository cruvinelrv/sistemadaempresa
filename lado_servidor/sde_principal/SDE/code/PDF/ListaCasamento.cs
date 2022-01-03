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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace SDE.PDF
{
    public class ListaCasamento
    {
        private BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.EMBEDDED);
        private BaseFont baseFontCabecalho = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\CygnetRound.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        private Font font18;
        private Font font12;
        private Font font10;
        private Font font8;

        public ListaCasamento()
        {
            font18 = new Font(baseFontCabecalho, 18, Font.BOLD, Color.BLACK);
            font12 = new Font(baseFontCabecalho, 12, Font.BOLD, Color.BLACK);
            font10 = new Font(baseFont, 10, Font.BOLD, Color.BLACK);
            font8 = new Font(baseFont, 8, Font.BOLD, Color.BLACK);
        }

        public void ConstroiListaCasamento(int idCorp, int idListaCasamento)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            query = db.Query();
            query.Constrain(typeof(OrdemServico));
            query.Descend("id").Constrain(idListaCasamento);
            IObjectSet rs_listaCasamento = query.Execute();
            OrdemServico listaCasamento_db = rs_listaCasamento[0] as OrdemServico;

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(listaCasamento_db.idCliente);
            IObjectSet rs_cliente = query.Execute();
            Cliente cliente_db = rs_cliente[0] as Cliente;

            query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("idCliente").Constrain(cliente_db.id);
            IObjectSet rs_clienteEndereco = query.Execute();

            query = db.Query();
            query.Constrain(typeof(ClienteFamiliar));
            query.Descend("idCliente").Constrain(cliente_db.id);
            query.Descend("key").Constrain("NOIVO");
            IObjectSet rs_noivo = query.Execute();
            ClienteFamiliar clienteFamiliar_db = (rs_noivo.Count == 0) ? null : rs_noivo[0] as ClienteFamiliar;

            //começa a busca por itens da lista de casamento
            query = db.Query();
            query.Constrain(typeof(OrdemServico_Item));
            //linha abaixo busca na tabela Ordem_Servico_Item as ids que estao na Ordem_Servico
            query.Descend("idOrdemServico").Constrain(listaCasamento_db.id);
            //organiza a lista de itens da tabela Ordem_Servico_Item
            query.Descend("item_nome").OrderAscending();
            IObjectSet rs_listaItens = query.Execute();

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            String filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\listaCasamento.pdf";
            Document documento = new Document(PageSize.A4);
            PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(filePath, FileMode.Create));

            documento.Open();

            try
            {
                int pagina = 1;
                int numeroItens = 0;

                PdfPCell celulaItem;

                escreveCabecalho(documento, cliente_db, clienteFamiliar_db, rs_clienteEndereco, listaCasamento_db.dthrInicio, pagina);
                PdfPTable tabelaItens = criaNovaTabelaItens();
                foreach (OrdemServico_Item item in rs_listaItens)
                {
                    if (numeroItens == 44)
                    {
                        pagina++;
                        numeroItens = 0;
                        documento.Add(tabelaItens);
                        documento.NewPage();
                        escreveCabecalho(documento, cliente_db, clienteFamiliar_db, rs_clienteEndereco, listaCasamento_db.dthrInicio, pagina);
                        tabelaItens = criaNovaTabelaItens();
                    }

                    celulaItem = new PdfPCell(new Phrase(item.idItem.ToString(), font10));
                    celulaItem.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    celulaItem.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                    celulaItem.BorderWidthLeft = 0;
                    celulaItem.BorderWidthRight = 0;
                    celulaItem.BorderWidthTop = 0;
                    tabelaItens.AddCell(celulaItem);

                    celulaItem = new PdfPCell(new Phrase(item.item_nome, font10));
                    celulaItem.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    celulaItem.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                    celulaItem.BorderWidthLeft = 0;
                    celulaItem.BorderWidthRight = 0;
                    celulaItem.BorderWidthTop = 0;
                    tabelaItens.AddCell(celulaItem);

                    celulaItem = new PdfPCell(new Phrase("R$", font10));
                    celulaItem.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    celulaItem.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                    celulaItem.BorderWidthLeft = 0;
                    celulaItem.BorderWidthRight = 0;
                    celulaItem.BorderWidthTop = 0;
                    tabelaItens.AddCell(celulaItem);

                    celulaItem = new PdfPCell(new Phrase(Utils.formatMoney(Convert.ToDecimal(item.vlrUnitVendaInicial), false), font10));
                    celulaItem.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    celulaItem.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                    celulaItem.BorderWidthLeft = 0;
                    celulaItem.BorderWidthRight = 0;
                    celulaItem.BorderWidthTop = 0;
                    tabelaItens.AddCell(celulaItem);

                    numeroItens++;
                }
                documento.Add(tabelaItens);
                documento.Close();
            }
            catch (Exception ex)
            {
                string mensagem = ex.Message;
                documento.Close();
                throw new Exception("ERRO: " + mensagem);
            }
        }

        private void escreveCabecalho(Document documento, Cliente cliente, ClienteFamiliar clienteFamiliar, IObjectSet rs_clienteEnderecos, string dataEvento, int pagina)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            PdfPCell celulaCabecalho;
            PdfPTable tabelaCabecalho = new PdfPTable(1);
            tabelaCabecalho.WidthPercentage = 100;
            tabelaCabecalho.DefaultCell.Border = Rectangle.NO_BORDER;

            celulaCabecalho = new PdfPCell(new Phrase("Lista de Casamento", font18));
            celulaCabecalho.PaddingLeft = 140;
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            celulaCabecalho = new PdfPCell(new Phrase(String.Format("Data do Evento:{0}", string.Format("{0:D}", Utils.StringToDateTime(dataEvento))), font12));
            celulaCabecalho.PaddingLeft = 140;
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            celulaCabecalho = new PdfPCell(new Phrase(String.Format("Noiva: {0}", cultureInfo.TextInfo.ToTitleCase(cliente.nome.ToLower())), font12));
            celulaCabecalho.PaddingLeft = 140;
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            celulaCabecalho = new PdfPCell(new Phrase(cultureInfo.TextInfo.ToTitleCase(String.Format("Noivo: {0}", (clienteFamiliar == null) ? "" : clienteFamiliar.nome.ToLower())), font12));
            celulaCabecalho.PaddingLeft = 140;
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            if (rs_clienteEnderecos.Count > 0)
            {
                ClienteEndereco clienteEndereco = rs_clienteEnderecos[0] as ClienteEndereco;
                celulaCabecalho = new PdfPCell(new Phrase(cultureInfo.TextInfo.ToTitleCase(String.Format("Endereço: {0} {1}, {2}-{3} cep:{4}", clienteEndereco.logradouro.ToLower(), clienteEndereco.numero.ToString(), clienteEndereco.cidade.ToLower(), clienteEndereco.uf, clienteEndereco.cep)), font12));
                celulaCabecalho.PaddingLeft = 140;
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }
            else
            {
                celulaCabecalho = new PdfPCell(new Phrase("Endereço: -", font12));
                celulaCabecalho.PaddingLeft = 140;
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }

            celulaCabecalho = new PdfPCell(new Phrase(""));
            celulaCabecalho.FixedHeight = 35;
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            documento.Add(tabelaCabecalho);
        }

        private PdfPTable criaNovaTabelaItens()
        {
            PdfPTable tabelaRetorno = new PdfPTable(4);
            tabelaRetorno.SetWidths(new int[] {10, 70, 5, 15 });
            tabelaRetorno.WidthPercentage = 75;
            tabelaRetorno.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            tabelaRetorno.DefaultCell.BorderWidthLeft = 0;
            tabelaRetorno.DefaultCell.BorderWidthRight = 0;
            tabelaRetorno.DefaultCell.BorderWidthTop = 0;
            tabelaRetorno.AddCell(new Phrase("Cod.", font10));
            tabelaRetorno.AddCell(new Phrase("Item", font10));
            tabelaRetorno.AddCell(new Phrase(""));
            tabelaRetorno.AddCell(new Phrase("Preço", font10));
            return tabelaRetorno;
        }
    }
}
