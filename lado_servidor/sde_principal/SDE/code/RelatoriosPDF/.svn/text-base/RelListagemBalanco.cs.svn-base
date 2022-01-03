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
using System.Collections.Generic;
using SDE.Entidade;
using System.IO;
using SDE.Enumerador;

namespace SDE.RelatoriosPDF
{
    public class RelListagemBalanco
    {
        private BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
        private Font font12;
        private Font font10;
        private Font font8;

        public RelListagemBalanco()
        {
            font12 = new Font(baseFont, 12, Font.BOLD, Color.WHITE);
            font10 = new Font(baseFont, 10, Font.BOLD, Color.WHITE);
            font8 = new Font(baseFont, 8, Font.BOLD, Color.BLACK);
        }

        public int GeraRelListagemBalanco(int idCorp, int idEmp, string campoOrdenacao)
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
            query.Constrain(typeof(Item));
            query.Descend("grupo").OrderAscending();
            query.Descend(campoOrdenacao).OrderAscending();
            IObjectSet rs_itens = query.Execute();

            Dictionary<String, List<Item>> dicionarioItemProGrupo = new Dictionary<string, List<Item>>();
            dicionarioItemProGrupo.Add("INDEFINIDO", new List<Item>());

            foreach (Item item in rs_itens)
            {
                if (item.grupo == null || item.grupo.Trim() == String.Empty)
                    dicionarioItemProGrupo["INDEFINIDO"].Add(item);
                else
                {
                    if (!dicionarioItemProGrupo.ContainsKey(item.grupo))
                        dicionarioItemProGrupo.Add(item.grupo, new List<Item>());
                    dicionarioItemProGrupo[item.grupo].Add(item);
                }
            }
            if (dicionarioItemProGrupo["INDEFINIDO"].Count == 0)
                dicionarioItemProGrupo.Remove("INDEFINIDO");

            String diretorio = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
            String caminhoArquivo = diretorio + ERelatorio.listagem_balanco + ".pdf";

            Document documento = new Document(PageSize.A4);
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(caminhoArquivo, FileMode.Create));

                HeaderFooter header = new HeaderFooter(new Phrase("Pág.", font8), true);
                header.Alignment = Element.ALIGN_RIGHT;
                documento.Header = header;

                documento.Open();

                GeraCabecalho(documento, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos);

                PdfPCell celulaTabelaListagem;

                for (int i = 0; i < dicionarioItemProGrupo.Count; i++)
                {
                    String grupo = dicionarioItemProGrupo.ElementAt(i).Key;
                    List<Item> listaItens = dicionarioItemProGrupo.ElementAt(i).Value;

                    PdfPTable tabelaListagem = GeraNovaTabelaGrupo(grupo);

                    foreach (Item item in listaItens)
                    {
                        if (item.desuso)
                            continue;

                        celulaTabelaListagem = new PdfPCell(new Phrase(item.id.ToString(), font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                        celulaTabelaListagem = new PdfPCell(new Phrase(item.nome, font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                        celulaTabelaListagem = new PdfPCell(new Phrase(item.rfUnica, font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                        celulaTabelaListagem = new PdfPCell(new Phrase(item.rfAuxiliar, font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                        celulaTabelaListagem = new PdfPCell(new Phrase(item.unidMed.ToString(), font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                        celulaTabelaListagem = new PdfPCell(new Phrase("", font8));
                        tabelaListagem.AddCell(celulaTabelaListagem);
                    }
                    celulaTabelaListagem = new PdfPCell(new Phrase(String.Format("Quantidade Total do Grupo '{0}':", grupo), font8));
                    celulaTabelaListagem.Colspan = 6;
                    tabelaListagem.AddCell(celulaTabelaListagem);

                    documento.Add(tabelaListagem);
                }

                PdfPTable tabelaTotal = new PdfPTable(1);
                tabelaTotal.WidthPercentage = 100;
                tabelaTotal.DefaultCell.Border = 0;
                tabelaTotal.SpacingBefore = 10;
                tabelaTotal.AddCell(new Phrase("Total de Itens em Estoque: __________________", font8));
                documento.Add(tabelaTotal);

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

            celulaCabecalho = new PdfPCell(new Phrase((clienteEmpresa_db.apelido_razsoc.Trim() == String.Empty) ? clienteEmpresa_db.nome : clienteEmpresa_db.apelido_razsoc, font8));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            celulaCabecalho = new PdfPCell(new Phrase(Utils.formata_cpf_cnpj(clienteEmpresa_db.cpf_cnpj), font8));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.AddCell(celulaCabecalho);

            if (rs_clienteEmpresaEnderecos.Count > 0)
            {
                ClienteEndereco clienteEndereco = rs_clienteEmpresaEnderecos[0] as ClienteEndereco;
                celulaCabecalho = new PdfPCell(new Phrase(String.Format("Endereço: {0}, {1}-{2} CEP:{3} I.E.:{4}", clienteEndereco.logradouro, clienteEndereco.cidade, clienteEndereco.uf, clienteEndereco.cep, clienteEndereco.inscr), font8));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }
            else
            {
                celulaCabecalho = new PdfPCell(new Phrase("Endereço: -", font8));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }

            if (rs_clienteEmpresaContatos.Count > 0)
            {
                celulaCabecalho = new PdfPCell(new Phrase(String.Format("Contato(s): {0}", GeraStringContatos(rs_clienteEmpresaContatos)), font8));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }
            else
            {
                celulaCabecalho = new PdfPCell(new Phrase("Contatos(s): -", font8));
                celulaCabecalho.Border = Rectangle.NO_BORDER;
                celulaCabecalho.Colspan = 2;
                tabelaCabecalho.AddCell(celulaCabecalho);
            }

            celulaCabecalho = new PdfPCell(new Phrase("LISTAGEM PARA BALANÇO", font12));
            celulaCabecalho.Border = Rectangle.NO_BORDER;
            celulaCabecalho.HorizontalAlignment = Element.ALIGN_CENTER;
            celulaCabecalho.BackgroundColor = Color.BLACK;
            celulaCabecalho.Colspan = 2;
            tabelaCabecalho.AddCell(celulaCabecalho);

            documento.Add(tabelaCabecalho);
        }

        private PdfPTable GeraNovaTabelaGrupo(string grupo)
        {
            PdfPCell celulaTabelaPrecos;

            PdfPTable tabelaPrecos = new PdfPTable(6);
            tabelaPrecos.WidthPercentage = 100;
            tabelaPrecos.SpacingBefore = 10;
            tabelaPrecos.SetWidths(new int[] { 8, 47, 10, 13, 10, 12 });

            celulaTabelaPrecos = new PdfPCell(new Phrase(String.Format("GRUPO: {0}", grupo), font10));
            celulaTabelaPrecos.BackgroundColor = Color.BLACK;
            celulaTabelaPrecos.Colspan = 6;
            tabelaPrecos.AddCell(celulaTabelaPrecos);

            celulaTabelaPrecos = new PdfPCell(new Phrase("Cód.", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);
            celulaTabelaPrecos = new PdfPCell(new Phrase("Item", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);
            celulaTabelaPrecos = new PdfPCell(new Phrase("Rf. Un.", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);
            celulaTabelaPrecos = new PdfPCell(new Phrase("Rf. Aux.", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);
            celulaTabelaPrecos = new PdfPCell(new Phrase("Un.", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);
            celulaTabelaPrecos = new PdfPCell(new Phrase("Quantidade", font8));
            tabelaPrecos.AddCell(celulaTabelaPrecos);

            return tabelaPrecos;
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
    }
}
