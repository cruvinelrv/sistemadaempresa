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
using Db4objects.Db4o.Query;
using SDE.Entidade;
using System.IO;
using SDE.Enumerador;

namespace SDE.RelatoriosPDF
{
    public class RelListaTelefone
    {
        private BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
        private Font font12;
        private Font font10;
        private Font font8;

        public RelListaTelefone()
        {
            font12 = new Font(baseFont, 12, Font.BOLD, Color.WHITE);
            font10 = new Font(baseFont, 10, Font.BOLD, Color.BLACK);
            font8 = new Font(baseFont, 8, Font.BOLD, Color.BLACK);
        }

        public int GeraReListaTelefone(int idCorp, int idEmp)
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
            String caminhoArquivo = diretorio + ERelatorio.lista_telefone + ".pdf";

            Document documento = new Document(PageSize.A4);
            
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(caminhoArquivo, FileMode.Create));
                
                HeaderFooter header = new HeaderFooter(new Phrase("Pág.", font8), true);
                header.Alignment = Element.ALIGN_RIGHT;
                documento.Header = header;

                documento.Open();

                GeraCabecalho(documento, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos);

                PdfPCell celulaTabelaClientes;

                PdfPTable tabelaClientes = new PdfPTable(2);
                tabelaClientes.WidthPercentage = 100;
                tabelaClientes.SpacingBefore = 10;
                tabelaClientes.SetWidths(new int[] { 60, 40 });

                celulaTabelaClientes = new PdfPCell(new Phrase("Cliente", font10));
                tabelaClientes.AddCell(celulaTabelaClientes);
                celulaTabelaClientes = new PdfPCell(new Phrase("Telefone(s)", font10));
                tabelaClientes.AddCell(celulaTabelaClientes);

                foreach (Cliente cliente in rs_clientes)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteContato));
                    query.Descend("idCliente").Constrain(cliente.id);
                    IObjectSet rs_clienteContatos = query.Execute();

                    celulaTabelaClientes = new PdfPCell(new Phrase(cliente.nome, font10));
                    tabelaClientes.AddCell(celulaTabelaClientes);
                    celulaTabelaClientes = new PdfPCell(new Phrase(GeraStringTelefones(rs_clienteContatos), font10));
                    tabelaClientes.AddCell(celulaTabelaClientes);
                }

                documento.Add(tabelaClientes);

                documento.Close();
                return 1;
            }
            catch (Exception)
            {
                documento.Close();
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
                celulaCabecalho = new PdfPCell(new Phrase(String.Format("Endereço: {0}, {1}-{2} CEP:{3} I.E.:{4}", clienteEndereco.logradouro, clienteEndereco.cidade, clienteEndereco.uf, clienteEndereco.cep, clienteEndereco.inscr), font10));
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

            celulaCabecalho = new PdfPCell(new Phrase("RELATÓRIO DE CLIENTES", font12));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            celulaCabecalho.HorizontalAlignment = Element.ALIGN_CENTER;
            celulaCabecalho.BackgroundColor = Color.BLACK;
            celulaCabecalho.Colspan = 2;
            tabelaCabecalho.AddCell(celulaCabecalho);

            documento.Add(tabelaCabecalho);
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

        private String GeraStringTelefones(IObjectSet rs_clienteContatos)
        {
            int contador = 0;
            String strTelefones = "";
            foreach (ClienteContato clienteContato in rs_clienteContatos)
            {
                contador++;
                if (clienteContato.tipo == EContatoTipo.fone_fixo || clienteContato.tipo == EContatoTipo.celular || clienteContato.tipo == EContatoTipo.fax)
                    strTelefones += clienteContato.valor;
                else
                    continue;
                if (contador < rs_clienteContatos.Count)
                    strTelefones += " / ";
            }
            return strTelefones;
        }
    }
}
