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
using iTextSharp.text.pdf;
using iTextSharp.text;
using Db4objects.Db4o;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using SDE.Enumerador;
using System.IO;

namespace SDE.RelatoriosPDF
{
    public class RelFichaCliente
    {
        private BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
        private Font font12;
        private Font font10;
        private Font font8;

        public RelFichaCliente()
        {
            font12 = new Font(baseFont, 12, Font.BOLD, Color.WHITE);
            font10 = new Font(baseFont, 10, Font.BOLD, Color.BLACK);
            font8 = new Font(baseFont, 8, Font.BOLD, Color.BLACK);
        }

        public int GeraRelFichaCliente(int idCorp, int idEmp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            query = db.Query();
            query.Constrain(typeof(Empresa));
            query.Descend("id").Constrain(idEmp);
            IObjectSet rs_empresa = query.Execute();
            Empresa empresa_db = rs_empresa[0] as Empresa;

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(empresa_db.idCliente);
            IObjectSet rs_clienteEmpresa = query.Execute();
            Cliente clienteEmpresa_db = rs_clienteEmpresa[0] as Cliente;

            query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("idCliente").Constrain(clienteEmpresa_db.id);
            query.Descend("campo").OrderAscending();
            IObjectSet rs_clienteEmpresaEnderecos = query.Execute();

            query = db.Query();
            query.Constrain(typeof(ClienteContato));
            query.Descend("idCliente").Constrain(clienteEmpresa_db.id);
            query.Descend("campo").OrderAscending();
            IObjectSet rs_clienteEmpresaContatos = query.Execute();

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("nome").OrderAscending();
            IObjectSet rs_clientes = query.Execute();

            String diretorio = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
            String caminhoArquivo = diretorio + ERelatorio.ficha_cliente + ".pdf";

            Document documento = new Document(PageSize.A4);

            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(caminhoArquivo, FileMode.Create));

                HeaderFooter header = new HeaderFooter(new Phrase("Pág.", font8), true);
                header.Alignment = Element.ALIGN_RIGHT;
                documento.Header = header;

                documento.Open();

                GeraCabecalho(documento, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos);

                PdfPTable tabelaFichaClientes = new PdfPTable(1);
                tabelaFichaClientes.WidthPercentage = 100;
                tabelaFichaClientes.SpacingBefore = 10;
                tabelaFichaClientes.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell celulaCabecaItem;
                foreach (Cliente cliente in rs_clientes)
                {
                    IObjectSet rs_clienteContatos = BuscaContatos(db, cliente.id);
                    IObjectSet rs_clienteEnderecos = BuscaEnderecos(db, cliente.id);

                    tabelaFichaClientes.AddCell(GeraCabecalhoTabela(cliente));

                    if (rs_clienteContatos.Count > 0)
                    {
                        celulaCabecaItem = new PdfPCell(new Phrase("CONTATO(S):", font8));
                        celulaCabecaItem.Border = Rectangle.BOTTOM_BORDER;
                        tabelaFichaClientes.AddCell(celulaCabecaItem);
                        tabelaFichaClientes.AddCell(new Phrase(GeraStringContatos(rs_clienteContatos), font8));
                    }

                    if (rs_clienteEnderecos.Count > 0)
                    {
                        celulaCabecaItem = new PdfPCell(new Phrase("ENDEREÇO(S):", font8));
                        celulaCabecaItem.Border = Rectangle.BOTTOM_BORDER;
                        tabelaFichaClientes.AddCell(celulaCabecaItem);
                        foreach (ClienteEndereco clienteEndereco in rs_clienteEnderecos)
                            tabelaFichaClientes.AddCell(new Phrase(GeraStringEndereco(clienteEndereco), font8));
                    }
                }
                documento.Add(tabelaFichaClientes);

                documento.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private void GeraCabecalho(Document documento, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos)
        {
            PdfPTable tabelaCabecalho = new PdfPTable(2);
            tabelaCabecalho.WidthPercentage = 100;
            tabelaCabecalho.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell celulaCabecalho;

            celulaCabecalho = new PdfPCell(new Phrase((clienteEmpresa_db.apelido_razsoc.Trim() == String.Empty) ? clienteEmpresa_db.nome : clienteEmpresa_db.apelido_razsoc, font10));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            celulaCabecalho = new PdfPCell(new Phrase(Utils.formata_cpf_cnpj(clienteEmpresa_db.cpf_cnpj), font10));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            if (rs_clienteEmpresaEnderecos.Count > 0)
            {
                ClienteEndereco clienteEndereco = rs_clienteEmpresaEnderecos[0] as ClienteEndereco;
                celulaCabecalho = new PdfPCell(new Phrase("Endereço: " + GeraStringEndereco(clienteEndereco), font10));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }
            else
            {
                celulaCabecalho = new PdfPCell(new Phrase("Endereço: -", font10));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }

            if (rs_clienteEmpresaContatos.Count > 0)
            {
                celulaCabecalho = new PdfPCell(new Phrase(String.Format("Contato(s): {0}", GeraStringContatos(rs_clienteEmpresaContatos)), font10));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }
            else
            {
                celulaCabecalho = new PdfPCell(new Phrase("Contatos(s): -", font10));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }

            celulaCabecalho = new PdfPCell(new Phrase("FICHA DE CLIENTES", font12));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            celulaCabecalho.HorizontalAlignment = Element.ALIGN_CENTER;
            celulaCabecalho.BackgroundColor = Color.BLACK;
            celulaCabecalho.Colspan = 2;
            tabelaCabecalho.AddCell(celulaCabecalho);

            documento.Add(tabelaCabecalho);
        }

        private PdfPTable GeraCabecalhoTabela(Cliente cliente)
        {
            PdfPTable tabelaCabecalho = new PdfPTable(3);
            tabelaCabecalho.WidthPercentage = 100;
            tabelaCabecalho.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.DefaultCell.BackgroundColor = Color.LIGHT_GRAY;
            tabelaCabecalho.SetWidths(new int[] { 50, 25, 25 });

            tabelaCabecalho.AddCell(new Phrase(cliente.nome, font8));
            tabelaCabecalho.AddCell(new Phrase(Utils.formata_cpf_cnpj(cliente.cpf_cnpj), font8));
            tabelaCabecalho.AddCell(new Phrase((cliente.rg == null || cliente.rg.Trim() == string.Empty) ? "" : "RG: " + cliente.rg, font8));

            return tabelaCabecalho;
        }

        private String GeraStringContatos(IObjectSet rs_clienteContatos)
        {
            int contador = 0;
            String strContatos = "";
            foreach (ClienteContato clienteContato in rs_clienteContatos)
            {
                strContatos += String.Format("{0}: {1}", clienteContato.campo, clienteContato.valor);
                contador++;
                if (contador < rs_clienteContatos.Count)
                    strContatos += " / ";
            }
            return strContatos;
        }

        private String GeraStringEndereco(ClienteEndereco clienteEndereco)
        {
            return String.Format("{0} {1}, {2}-{3} CEP:{4} {5}", clienteEndereco.logradouro, (clienteEndereco.numero == 0) ? "" : clienteEndereco.numero.ToString(), clienteEndereco.cidade, clienteEndereco.uf, clienteEndereco.cep, (clienteEndereco.inscr == null || clienteEndereco.inscr.Trim() == string.Empty) ? "" : "I.E.:" + clienteEndereco.inscr);
        }

        private IObjectSet BuscaContatos(IObjectContainer db, int idCliente)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(ClienteContato));
            query.Descend("idCliente").Constrain(idCliente);
            query.Descend("tipo").OrderAscending();
            return query.Execute();
        }

        private IObjectSet BuscaEnderecos(IObjectContainer db, int idCliente)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("idCliente").Constrain(idCliente);
            return query.Execute();
        }
    }
}
