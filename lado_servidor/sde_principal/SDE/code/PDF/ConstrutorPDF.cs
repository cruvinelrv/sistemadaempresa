using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SDE.Enumerador;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using System.Text;
using System.Globalization;
using System.IO;
using SDE.CamadaNuvem;
using System.Collections;
using System.Drawing.Printing;

namespace SDE.PDF
{
    public partial class ConstrutorPDF
    {
        public static void relatorioMov(int idCorp,int idEmp, int idMov)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            Mov mov = null;
            foreach (Mov xxx in db.Query<Mov>())
                if (xxx.id == idMov)
                {
                    mov = xxx;
                    break;
                }
            //
            List<MovItem> carrinho = new List<MovItem>();
            foreach (MovItem xxx in db.Query<MovItem>())
                if (xxx.idMov == mov.id)
                    carrinho.Add(xxx);
            //
            List<Cx_Lancamento> cxLancamentos = new List<Cx_Lancamento>();
            if (mov.tipo == EMovTipo.saida_venda)
            {
                IQuery q = db.Query();
                q.Constrain(typeof(Cx_Lancamento));
                q.Descend("id").OrderAscending();
                foreach (Cx_Lancamento xxx in q.Execute())
                {
                    if (xxx.idTransacao == mov.idTransacao)
                        cxLancamentos.Add(xxx);
                }
            }
            List<Orcamento_Lancamento> orcamentoLancamentos = new List<Orcamento_Lancamento>();
            if (mov.tipo == EMovTipo.outros_orcamento)
            {
                IQuery q = db.Query();
                q.Constrain(typeof(Orcamento_Lancamento));
                q.Descend("id").OrderAscending();
                foreach (Orcamento_Lancamento xxx in q.Execute())
                {
                    if (xxx.idTransacao == mov.idTransacao)
                        orcamentoLancamentos.Add(xxx);
                }
            }

            //
            Empresa emp = null;
            foreach (Empresa xxx in db.Query<Empresa>())
                if (xxx.id == idEmp)
                {
                    emp = xxx;
                    break;
                }
            //
            Cliente cli_empresa = null;
            foreach (Cliente xxx in db.Query<Cliente>())
                if (xxx.id == emp.idCliente)
                {
                    cli_empresa = xxx;
                    break;
                }
            //
            Cliente cli_funcionario = null;
            foreach (Cliente xxx in db.Query<Cliente>())
                if (xxx.id == mov.idClienteFuncionarioVendedor)
                {
                    cli_funcionario = xxx;
                    break;
                }
            //
            Cliente cli = null;
            foreach (Cliente xxx in db.Query<Cliente>())
                if (xxx.id == mov.idCliente)
                {
                    cli = xxx;
                    break;
                }

            ClienteEndereco cli_endereco = new ClienteEndereco();
            query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("id").Constrain(mov.idClienteEndereco);
            if (query.Execute().Count > 0)
                cli_endereco = query.Execute()[0] as ClienteEndereco;

            List<ClienteEndereco> cli_empresa_enderecos = new List<ClienteEndereco>();
            foreach (ClienteEndereco xxx in db.Query<ClienteEndereco>())
            {
                if (xxx.idCliente == cli_empresa.id)
                    cli_empresa_enderecos.Add(xxx);
            }
            //
            List<ClienteContato> cli_contatos = new List<ClienteContato>();
            List<ClienteContato> cli_empresa_contatos = new List<ClienteContato>();
            foreach (ClienteContato xxx in db.Query<ClienteContato>())
            {
                if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                    if (xxx.idCliente == cli.id)
                        cli_contatos.Add(xxx);
                if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                    if (xxx.idCliente == cli_empresa.id)
                        cli_empresa_contatos.Add(xxx);
            }

            //
            if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                if (cli_empresa_enderecos.Count == 0)
                    cli_empresa_enderecos.Add(new ClienteEndereco());
            if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                if (cli_empresa_contatos.Count == 0)
                    cli_empresa_contatos.Add(new ClienteContato());
            //

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\mov.pdf";

            Document myPdfDoc = new Document(PageSize.A4);
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));
            Font baseFont = new Font();
            baseFont.Size = 8;

            Paragraph linha = new Paragraph("\n", baseFont);

            myPdfDoc.Open();

            try
            {
                PdfPTable tableOrcamento = new PdfPTable(1);
                tableOrcamento.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableOrcamento.DefaultCell.Border = 0;
                tableOrcamento.AddCell(new Phrase(defineTipoImpressao(mov.impressao)));
                myPdfDoc.Add(tableOrcamento);

                Paragraph p1 = new Paragraph("NAO E VALIDO COMO DOCUMENTO FISCAL", baseFont);
                myPdfDoc.Add(p1);
                Paragraph p2 = new Paragraph("NAO ACOBERTA MERCADORIA PARA DEVOLUCAO OU TROCA", baseFont);
                myPdfDoc.Add(p2);

                PdfPTable t1 = new PdfPTable(2);
                t1.WidthPercentage = 100;
                t1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                t1.DefaultCell.Border = 0;
                t1.AddCell(new Phrase((cli_empresa.apelido_razsoc == "") ? cli_empresa.nome : cli_empresa.apelido_razsoc, baseFont));
                t1.AddCell(new Phrase(formata_cpf_cnpj(cli_empresa.cpf_cnpj, true), baseFont));
                myPdfDoc.Add(t1);

                PdfPTable t2 = new PdfPTable(2);
                t2.WidthPercentage = 100;
                t2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                t2.DefaultCell.Border = 0;

                if (cli_empresa_enderecos.Count == 0)
                {
                    cli_empresa_enderecos.Add(new ClienteEndereco());
                }

                t2.AddCell(new Phrase(String.Format("Cidade: {0}-{1}", cli_empresa_enderecos[0].cidade, cli_empresa_enderecos[0].uf), baseFont));
                t2.AddCell(new Phrase(String.Format("Inscrição Estadual: {0}", cli_empresa_enderecos[0].inscr), baseFont));
                
                myPdfDoc.Add(t2);

                string cContatos = "";
                for (int i = 0; i < cli_empresa_contatos.Count; i++)
                {
                    ClienteContato cc = cli_empresa_contatos[i];
                    cContatos += string.Format("{0}: {1},   ", cc.campo, cc.valor);
                }
                Paragraph p3 = new Paragraph(String.Format("Contato(s): {0}", cContatos), baseFont);
                myPdfDoc.Add(p3);

                PdfPTable t3 = new PdfPTable(3);
                t3.WidthPercentage = 100;
                t3.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                t3.DefaultCell.Border = 0;
                t3.AddCell(new Phrase(String.Format("Número: {0}", mov.id.ToString()), baseFont));
                t3.AddCell(new Phrase(String.Format("Data: {0}", mov.dthrMovEmissao.Substring(0, 10)), baseFont));
                t3.AddCell(new Phrase(String.Format("Vendedor: {0}", cli_funcionario.nome), baseFont));
                myPdfDoc.Add(t3);

                PdfPTable t4 = new PdfPTable(3);
                t4.WidthPercentage = 100;
                t4.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                t4.DefaultCell.Border = 0;
                if (mov.idCliente != 1)
                    t4.AddCell(new Phrase(String.Format("Cliente: {0} - {1}", cli.id, (cli.tipo == EPesTipo.Juridica) ? (cli.apelido_razsoc == "") ? cli.nome : cli.apelido_razsoc : cli.nome), baseFont));
                else
                    t4.AddCell(new Phrase(String.Format("Cliente: {0} - {1}", mov.idCliente, mov.cliente_nome), baseFont));

                if (mov.idCliente != 1)
                {
                    cContatos = "";
                    for (int i = 0; i < cli_contatos.Count; i++)
                    {
                        ClienteContato cc = cli_contatos[i];
                        cContatos += string.Format("{0}: {1},   ", cc.campo, cc.valor);
                    }
                    t4.AddCell(new Phrase(String.Format("Contato(s): {0}", cContatos), baseFont));
                }
                else
                {
                    t4.AddCell(new Phrase(String.Format("Contato(s): {0}", mov.cliente_contato), baseFont));
                }

                t4.AddCell(new Phrase(formata_cpf_cnpj(mov.cliente_cpf, true), baseFont));
                t4.AddCell(new Phrase(String.Format("CEP: {0}", cli_endereco.cep), baseFont));
                t4.AddCell(new Phrase("", baseFont));
                t4.AddCell(new Phrase(String.Format("Inscr. Est.: {0}", cli_endereco.inscr), baseFont));
                myPdfDoc.Add(t4);

                Paragraph p4 = null; ;
                if (mov.idCliente != 1)
                {
                    p4 = new Paragraph(string.Format("Endereço: {0}, {1} {2}, {3} {4}-{5}",
                    cli_endereco.logradouro,
                    (cli_endereco.numero == 0) ? "" : cli_endereco.numero.ToString(),
                    cli_endereco.complemento,
                    cli_endereco.bairro,
                    cli_endereco.cidade,
                    cli_endereco.uf)
                    , baseFont);
                }
                else
                    p4 = new Paragraph(string.Format("Endereço: {0}", mov.cliente_endereco_faturamento), baseFont);
                myPdfDoc.Add(p4);

                myPdfDoc.Add(linha);

                int[] tableWidths = { 5, 10, 12, 30, 12, 8, 13, 5, 10, 10 };

                PdfPTable tableHeader = new PdfPTable(10);
                tableHeader.SetWidths(tableWidths);
                tableHeader.WidthPercentage = 100;
                tableHeader.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHeader.DefaultCell.Bottom = 0;
                tableHeader.AddCell(new Phrase("Cod.", baseFont));
                tableHeader.AddCell(new Phrase("Cod. Aux.", baseFont));
                tableHeader.AddCell(new Phrase("Cod. Un.", baseFont));
                tableHeader.AddCell(new Phrase("Descrição", baseFont));
                tableHeader.AddCell(new Phrase("Grade/Ident.", baseFont));
                tableHeader.AddCell(new Phrase("Locação", baseFont));
                tableHeader.AddCell(new Phrase("Unid. Medida", baseFont));
                tableHeader.AddCell(new Phrase("Qtd.", baseFont));
                tableHeader.AddCell(new Phrase("Prc. Unit.", baseFont));
                tableHeader.AddCell(new Phrase("Prc. Total", baseFont));
                myPdfDoc.Add(tableHeader);

                PdfPCell cellContent;

                foreach (MovItem mi in carrinho)
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(mi.idItem);
                    Item item = query.Execute()[0] as Item;

                    string locacao = "";
                    if (item.locacao1 != "") locacao += item.locacao1;
                    if (item.locacao2 != "") if (locacao != "") locacao += "/" + item.locacao2; else locacao += item.locacao2;
                    if (item.locacao3 != "") if (locacao != "") locacao += "/" + item.locacao3; else locacao += item.locacao3;
                    if (item.locacao4 != "") if (locacao != "") locacao += "/" + item.locacao4; else locacao += item.locacao4;
                    if (item.locacao5 != "") if (locacao != "") locacao += "/" + item.locacao5; else locacao += item.locacao5;
                    if (item.locacao6 != "") if (locacao != "") locacao += "/" + item.locacao6; else locacao += item.locacao6;
                    if (item.locacao7 != "") if (locacao != "") locacao += "/" + item.locacao7; else locacao += item.locacao7;
                    if (item.locacao8 != "") if (locacao != "") locacao += "/" + item.locacao8; else locacao += item.locacao8;
                    if (item.locacao9 != "") if (locacao != "") locacao += "/" + item.locacao9; else locacao += item.locacao9;
                    if (item.locacao10 != "") if (locacao != "") locacao += "/" + item.locacao10; else locacao += item.locacao10;

                    PdfPTable tableContent = new PdfPTable(10);
                    tableContent.SetWidths(tableWidths);
                    tableContent.WidthPercentage = 100;
                    tableContent.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    cellContent = new PdfPCell(new Phrase(mi.idItem.ToString(), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.rf_unica, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.rf_auxiliar, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.item_nome, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.estoque_identificador, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(locacao, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.unid_med, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(mi.qtd.ToString(), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(formatMoney(Decimal.Parse(mi.vlrUnitVendaFinal.ToString()), false), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(formatMoney(Decimal.Parse((mi.vlrUnitVendaFinal * mi.qtd).ToString()), false), baseFont));
                    tableContent.AddCell(cellContent);

                    myPdfDoc.Add(tableContent);
                }

                PdfPTable tableFormaPagamento = new PdfPTable(1);
                tableFormaPagamento.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableFormaPagamento.DefaultCell.Border = 0;

                if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                {
                    t2.AddCell(new Phrase(String.Format("Cidade: {0}-{1}", cli_empresa_enderecos[0].cidade, cli_empresa_enderecos[0].uf), baseFont));
                    t2.AddCell(new Phrase(String.Format("Inscrição Estadual: {0}", cli_empresa_enderecos[0].inscr), baseFont));
                }
                else
                {
                    t2.AddCell(new Phrase(String.Format("Cidade:"), baseFont));
                    t2.AddCell(new Phrase(String.Format("Inscrição Estadual:"), baseFont));
                }

                if (mov.tipo != EMovTipo.outros_orcamento && mov.tipo != EMovTipo.outros_pedido)
                    tableFormaPagamento.AddCell(new Phrase("Forma de Pagamento", baseFont));
                
                myPdfDoc.Add(tableFormaPagamento);

                //myPdfDoc.Add(linha);

                if (cxLancamentos.Count > 0 || orcamentoLancamentos.Count > 0)
                {
                    PdfPTable tableHeaderFormaPagamento = new PdfPTable(3);
                    tableHeaderFormaPagamento.WidthPercentage = 100;
                    tableHeaderFormaPagamento.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableHeaderFormaPagamento.AddCell(new Phrase("Forma de Pagamento", baseFont));
                    tableHeaderFormaPagamento.AddCell(new Phrase("Data Vencimento", baseFont));
                    tableHeaderFormaPagamento.AddCell(new Phrase("Valor", baseFont));
                    myPdfDoc.Add(tableHeaderFormaPagamento);

                    if (mov.tipo == EMovTipo.saida_venda)
                    {
                        foreach (Cx_Lancamento cxl in cxLancamentos)
                        {
                            PdfPTable tableContentFormaPagamento = new PdfPTable(3);
                            tableContentFormaPagamento.WidthPercentage = 100;
                            tableContentFormaPagamento.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                            cellContent = new PdfPCell(new Phrase(cxl.tipoPagamento_nome, baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            cellContent = new PdfPCell(new Phrase(cxl.dtPagamento, baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            cellContent = new PdfPCell(new Phrase(formatMoney(Convert.ToDecimal(cxl.valorCobrado), false), baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            myPdfDoc.Add(tableContentFormaPagamento);
                        }
                    }
                    else if (mov.tipo == EMovTipo.outros_orcamento)
                    {
                        foreach (Orcamento_Lancamento orcamentoL in orcamentoLancamentos)
                        {
                            PdfPTable tableContentFormaPagamento = new PdfPTable(3);
                            tableContentFormaPagamento.WidthPercentage = 100;
                            tableContentFormaPagamento.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                            cellContent = new PdfPCell(new Phrase(orcamentoL.tipoPagamento_nome, baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            cellContent = new PdfPCell(new Phrase(orcamentoL.dtPagamento, baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            cellContent = new PdfPCell(new Phrase(formatMoney(Convert.ToDecimal(orcamentoL.valorCobrado), false), baseFont));
                            tableContentFormaPagamento.AddCell(cellContent);

                            myPdfDoc.Add(tableContentFormaPagamento);
                        }
                    }
                }

                double pctDesconto = 0;
                if (mov.vlrItensInicial != 0)
                    pctDesconto = (mov.vlrItensInicial - mov.vlrItensFinal) * 100 / mov.vlrItensInicial;
                if (pctDesconto < 0)
                    pctDesconto = 0;
                double valorDesconto = mov.vlrItensInicial - mov.vlrItensFinal;
                if (valorDesconto < 0)
                    valorDesconto = 0;

                PdfPTable t5 = new PdfPTable(4);
                t5.WidthPercentage = 100;
                t5.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                t5.DefaultCell.Border = 0;
                if (mov.vlrItensInicial < mov.vlrItensFinal)
                    t5.AddCell(new Phrase(String.Format("Val. Prod.: {0}", formatMoney(Decimal.Parse(mov.vlrItensFinal.ToString()), false)), baseFont));
                else
                    t5.AddCell(new Phrase(String.Format("Val. Prod.: {0}", formatMoney(Decimal.Parse(mov.vlrItensInicial.ToString()), false)), baseFont));
                t5.AddCell(new Phrase(String.Format("Desc.(%): {0}%", Math.Round(pctDesconto, 2).ToString()), baseFont));
                t5.AddCell(new Phrase(String.Format("Desc.(R$): {0}", formatMoney(Decimal.Parse(valorDesconto.ToString()), false)), baseFont));
                t5.AddCell(new Phrase(String.Format("Val. Total c/ Desc.: {0}", formatMoney(Decimal.Parse(mov.vlrItensFinal.ToString()), false)), baseFont));
                myPdfDoc.Add(t5);

                Paragraph p5 = new Paragraph(String.Format("OBS: {0}", mov.obs), baseFont);
                myPdfDoc.Add(p5);

                if (mov.tipo == EMovTipo.outros_orcamento)
                {
                    Paragraph pOrcmt1 = new Paragraph(String.Format("Validade: {0} DIAS", mov.validadeDias), baseFont);
                    myPdfDoc.Add(pOrcmt1);

                    if (mov.entrega != "" && mov.entrega != null)
                    {
                        Paragraph pOrcmt2 = new Paragraph(String.Format("Entrega: {0}", mov.entrega), baseFont);
                        myPdfDoc.Add(pOrcmt2);
                    }

                    if (mov.frete != "" && mov.frete != null)
                    {
                        Paragraph pOrcmt3 = new Paragraph(String.Format("Frete: {0}", mov.frete), baseFont);
                        myPdfDoc.Add(pOrcmt3);
                    }

                    if (mov.impostos != "" && mov.impostos != null)
                    {
                        Paragraph pOrcmt4 = new Paragraph(String.Format("Impostos: {0}", mov.impostos), baseFont);
                        myPdfDoc.Add(pOrcmt4);
                    }
                }

                PdfPTable tableAssinatura = new PdfPTable(1);
                tableAssinatura.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableAssinatura.DefaultCell.Border = 0;
                tableAssinatura.AddCell("______________________________________________________");
                tableAssinatura.AddCell(new Phrase(mov.cliente_nome, baseFont));
                myPdfDoc.Add(tableAssinatura);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            myPdfDoc.Close();
        }

        public static void RelatorioCliente(int idCorp, int idEmp)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            //
            Empresa emp = null;
            foreach (Empresa xxx in db.Query<Empresa>())
                if (xxx.id == idEmp)
                {
                    emp = xxx;
                    break;
                }
            //
            Cliente cli_empresa = null;
            foreach (Cliente xxx in db.Query<Cliente>())
                if (xxx.id == emp.idCliente)
                {
                    cli_empresa = xxx;
                    break;
                }

            List<ClienteEndereco> cli_empresa_enderecos = new List<ClienteEndereco>();
            foreach (ClienteEndereco xxx in db.Query<ClienteEndereco>())
                if (xxx.idCliente == cli_empresa.id)
                    cli_empresa_enderecos.Add(xxx);
            //
            List<ClienteContato> cli_empresa_contatos = new List<ClienteContato>();
            foreach (ClienteContato xxx in db.Query<ClienteContato>())
                if (xxx.idCliente == cli_empresa.id)
                    cli_empresa_contatos.Add(xxx);

            //
            if (cli_empresa_enderecos.Count == 0)
                cli_empresa_enderecos.Add(new ClienteEndereco());
            if (cli_empresa_contatos.Count == 0)
                cli_empresa_contatos.Add(new ClienteContato());
            //

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\relCliente.pdf";

            Document myPdfDoc = new Document(PageSize.A4);
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));
            Font baseFont = new Font();
            baseFont.Size = 8;

            Paragraph linha = new Paragraph("\n", baseFont);

            myPdfDoc.Open();

            PdfPTable t1 = new PdfPTable(2);
            t1.WidthPercentage = 100;
            t1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            t1.DefaultCell.Border = 0;
            t1.AddCell(new Phrase((cli_empresa.apelido_razsoc == "") ? cli_empresa.nome : cli_empresa.apelido_razsoc, baseFont));
            t1.AddCell(new Phrase(formata_cpf_cnpj(cli_empresa.cpf_cnpj, true), baseFont));
            myPdfDoc.Add(t1);

            PdfPTable t2 = new PdfPTable(2);
            t2.WidthPercentage = 100;
            t2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            t2.DefaultCell.Border = 0;
            t2.AddCell(new Phrase(String.Format("Cidade: {0}-{1}", cli_empresa_enderecos[0].cidade, cli_empresa_enderecos[0].uf), baseFont));
            t2.AddCell(new Phrase(String.Format("Inscrição Estadual: {0}", cli_empresa_enderecos[0].inscr), baseFont));
            myPdfDoc.Add(t2);

            myPdfDoc.Add(linha);

            string cContatos = "";
            for (int i = 0; i < cli_empresa_contatos.Count; i++)
            {
                ClienteContato cc = cli_empresa_contatos[i];
                cContatos += string.Format("{0}: {1},   ", cc.campo, cc.valor);
            }
            Paragraph p3 = new Paragraph(String.Format("Contato(s): {0}", cContatos), baseFont);
            myPdfDoc.Add(p3);

            int[] tableWidths = { 10, 45, 45};

            PdfPTable tableHeader = new PdfPTable(3);
            tableHeader.SetWidths(tableWidths);
            tableHeader.WidthPercentage = 100;
            tableHeader.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tableHeader.DefaultCell.Bottom = 0;
            tableHeader.AddCell(new Phrase("Cod.", baseFont));
            tableHeader.AddCell(new Phrase("Cliente", baseFont));
            tableHeader.AddCell(new Phrase("Telefone(s)", baseFont));
            myPdfDoc.Add(tableHeader);

            PdfPCell cellContent;

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("nome").OrderAscending();
            foreach (Cliente cliente in query.Execute())
            {
                int contador = 1;
                string telefones = "";
                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(cliente.id);
                foreach (ClienteContato clienteContato in query.Execute())
                    if (clienteContato.tipo == EContatoTipo.fone_fixo || clienteContato.tipo == EContatoTipo.celular || clienteContato.tipo == EContatoTipo.fax)
                    {
                        if (contador == query.Execute().Count)
                            telefones += clienteContato.valor;
                        else
                            telefones += clienteContato.valor + " ,";
                    }
                PdfPTable tableContent = new PdfPTable(3);
                tableContent.SetWidths(tableWidths);
                tableContent.WidthPercentage = 100;
                tableContent.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                cellContent = new PdfPCell(new Phrase(cliente.id.ToString(), baseFont));
                tableContent.AddCell(cellContent);

                cellContent = new PdfPCell(new Phrase(cliente.nome, baseFont));
                tableContent.AddCell(cellContent);

                cellContent = new PdfPCell(new Phrase(telefones, baseFont));
                tableContent.AddCell(cellContent);

                myPdfDoc.Add(tableContent);
            }

            myPdfDoc.Close();
        }

        public static void RelatorioParcialBalanco(int idCorp, int idEmp, int idBalanco)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            Empresa empresa = null;
            query = db.Query();
            query.Constrain(typeof(Empresa));
            query.Descend("id").Constrain(idEmp);
            if (query.Execute().Count == 0)
                throw new Exception("Empresa não encontrada.\nId Empresa: " + idEmp);
            else
                empresa = query.Execute()[0] as Empresa;

            Cliente cliente_empresa = null;
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(empresa.idCliente);
            if (query.Execute().Count == 0)
                throw new Exception("Cliente empresa não encontrado.\nId Cliente Empresa: " + empresa.idCliente);
            else
                cliente_empresa = query.Execute()[0] as Cliente;

            Balanco balanco = null;
            query = db.Query();
            query.Constrain(typeof(Balanco));
            query.Descend("id").Constrain(idBalanco);
            if (query.Execute().Count == 0)
                throw new Exception("Balanço não encontrado. Id balanço: " + idBalanco);
            else
                balanco = query.Execute()[0] as Balanco;

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\relParcialBalanco.pdf";

            Document myPdfDoc = new Document(PageSize.A4.Rotate());
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));
            Font baseFont = new Font();
            baseFont.Size = 8;

            Paragraph linha = new Paragraph("\n", baseFont);

            try
            {
                myPdfDoc.Open();

                Paragraph titulo = new Paragraph("Relatório Parcial de Balanço");
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.Font.Size = 16;
                myPdfDoc.Add(titulo);

                Paragraph pempresa = new Paragraph("Empresa: " + cliente_empresa.nome);
                myPdfDoc.Add(pempresa);

                Paragraph dataBalanco = new Paragraph("Balanço iniciado em: " + balanco.dthrInicio);
                myPdfDoc.Add(dataBalanco);

                myPdfDoc.Add(linha);

                int[] tableWidths = { 40, 10, 10, 10, 5, 5, 10, 10 };

                PdfPTable tableHeader = new PdfPTable(8);
                tableHeader.SetWidths(tableWidths);
                tableHeader.WidthPercentage = 100;
                tableHeader.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHeader.DefaultCell.Bottom = 0;
                tableHeader.AddCell(new Phrase("Item", baseFont));
                tableHeader.AddCell(new Phrase("Rf. Un.", baseFont));
                tableHeader.AddCell(new Phrase("Rf. Aux.", baseFont));
                tableHeader.AddCell(new Phrase("Ident.", baseFont));
                tableHeader.AddCell(new Phrase("Qtd. Ant.", baseFont));
                tableHeader.AddCell(new Phrase("Qtd. Lan.", baseFont));
                tableHeader.AddCell(new Phrase("Preço Custo", baseFont));
                tableHeader.AddCell(new Phrase("Preço Venda", baseFont));
                myPdfDoc.Add(tableHeader);

                PdfPCell cellContent;

                PdfPTable tableContent = new PdfPTable(8);
                tableContent.SetWidths(tableWidths);
                tableContent.WidthPercentage = 100;
                tableContent.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                double valorTotalCusto = 0;
                double valorTotalVenda = 0;
                double quantidadeTotalAnterior = 0;
                double quantidadeTotalLancada = 0;

                query = db.Query();
                query.Constrain(typeof(BalancoItem));
                query.Descend("idBalanco").Constrain(idBalanco);
                query.Descend("id").OrderAscending();
                foreach (BalancoItem balancoItem in query.Execute())
                {
                    //Item item = db.Query<Item>(delegate(Item it) { return it.id == balancoItem.idItem; })[0];
                    //ItemEmpPreco itemEmpPreco = db.Query<ItemEmpPreco>(delegate(ItemEmpPreco iep) { return iep.idItem == item.id; })[0];

                    //IObjectSet resultItem = db.QueryByExample(new Item() { id = balancoItem.idItem });
                    //Item item = resultItem[0] as Item;

                    //IObjectSet resultItemEmpPreco = db.QueryByExample(new ItemEmpPreco() { idItem = item.id });
                    //ItemEmpPreco itemEmpPreco = resultItemEmpPreco[0] as ItemEmpPreco;

                    cellContent = new PdfPCell(new Phrase(balancoItem.item_nome, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(balancoItem.rfUnica, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(balancoItem.rfAuxiliar, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(balancoItem.estoque_identificador, baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(balancoItem.qtdAnterior.ToString(), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(balancoItem.qtdLancada.ToString(), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(formatMoney(decimal.Parse(balancoItem.custo.ToString()), true), baseFont));
                    tableContent.AddCell(cellContent);

                    cellContent = new PdfPCell(new Phrase(formatMoney(decimal.Parse(balancoItem.venda.ToString()), true), baseFont));
                    tableContent.AddCell(cellContent);

                    valorTotalCusto += balancoItem.custo * balancoItem.qtdLancada;
                    valorTotalVenda += balancoItem.venda * balancoItem.qtdLancada;
                    quantidadeTotalAnterior += balancoItem.qtdAnterior;
                    quantidadeTotalLancada += balancoItem.qtdLancada;
                }

                myPdfDoc.Add(tableContent);

                Paragraph valores = new Paragraph("Valores:");
                valores.Font.Size = 12;
                valores.Font.IsBold();
                myPdfDoc.Add(valores);

                Paragraph valorCusto = new Paragraph("Custo Total: " + formatMoney(decimal.Parse(valorTotalCusto.ToString()), true), baseFont);
                myPdfDoc.Add(valorCusto);

                Paragraph valorVenda = new Paragraph("Venda Total: " + formatMoney(decimal.Parse(valorTotalVenda.ToString()), true), baseFont);
                myPdfDoc.Add(valorVenda);

                myPdfDoc.Close();
            }
            catch (Exception ex)
            {
                myPdfDoc.Close();
                throw new Exception(ex.ToString()+"\n"+ex.StackTrace);
            }
        }

        public static void RelatorioOrdemServico(int idCorp, int idEmp, int idOrdemServico)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;
            Document documento = new Document();

            try
            {
                query = db.Query();
                query.Constrain(typeof(OrdemServico));
                query.Descend("id").Constrain(idOrdemServico);
                IObjectSet rs_ordemServico = query.Execute();
                OrdemServico ordemServico_db = rs_ordemServico[0] as OrdemServico;

                query = db.Query();
                query.Constrain(typeof(OrdemServico_Item));
                query.Descend("idOrdemServico").Constrain(ordemServico_db.id);
                IObjectSet rs_ordemServicoItens = query.Execute();

                query = db.Query();
                query.Constrain(typeof(OrdemServico_Tipo));
                query.Descend("id").Constrain(ordemServico_db.idOrdemServicoTipo);
                IObjectSet rs_ordemServicoTipo = query.Execute();
                OrdemServico_Tipo ordemServicoTipo_db = rs_ordemServicoTipo[0] as OrdemServico_Tipo;

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
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(ordemServico_db.idClienteFuncionarioLogado);
                IObjectSet rs_clienteFuncionario = query.Execute();
                Cliente clienteFuncionario_db = rs_clienteFuncionario[0] as Cliente;

                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(ordemServico_db.idCliente);
                IObjectSet rs_cliente = query.Execute();
                Cliente cliente_db = rs_cliente[0] as Cliente;

                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("idCliente").Constrain(clienteEmpresa_db.id);
                IObjectSet rs_clienteEmpresaEnderecos = query.Execute();

                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(clienteEmpresa_db.id);
                IObjectSet rs_clienteEmpresaContatos = query.Execute();

                if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                    Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\");

                string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\OrdemServico.pdf";

                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                iTextSharp.text.Font font10 = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL, Color.BLACK);
                iTextSharp.text.Font font8 = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

                PdfPTable separadorHorizontal = new PdfPTable(1);
                separadorHorizontal.WidthPercentage = 100;
                separadorHorizontal.DefaultCell.Border = 0;
                PdfPCell cell = new PdfPCell();
                cell.BorderWidthLeft = 0;
                cell.BorderWidthTop = 0;
                cell.BorderWidthRight = 0;
                cell.BorderWidthBottom = 1;
                separadorHorizontal.AddCell(cell);

                Rectangle FORMULARIO = new Rectangle(612f, 777f);
                documento = new Document(FORMULARIO);
                PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(filePath, FileMode.Create));

                PdfPTable tabela_titulo = new PdfPTable(1);
                tabela_titulo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabela_titulo.DefaultCell.Border = 0;
                tabela_titulo.AddCell(new Phrase("ORDEM DE SERVIÇO"));

                PdfPTable tabela_dadosEmpresa = new PdfPTable(2);
                tabela_dadosEmpresa.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela_dadosEmpresa.DefaultCell.Border = 0;
                tabela_dadosEmpresa.WidthPercentage = 100;
                tabela_dadosEmpresa.AddCell(new Phrase((clienteEmpresa_db.apelido_razsoc == "") ? clienteEmpresa_db.nome : clienteEmpresa_db.apelido_razsoc, font8));
                tabela_dadosEmpresa.AddCell(new Phrase(formata_cpf_cnpj(clienteEmpresa_db.cpf_cnpj, true), font8));
                if (rs_clienteEmpresaEnderecos.Count > 0)
                    tabela_dadosEmpresa.AddCell(new Phrase(String.Format("Cidade: {0}-{1}", (rs_clienteEmpresaEnderecos[0] as ClienteEndereco).cidade, (rs_clienteEmpresaEnderecos[0] as ClienteEndereco).uf), font8));
                else
                    tabela_dadosEmpresa.AddCell(new Phrase(String.Format("Cidade: "), font8));
                if (rs_clienteEmpresaEnderecos.Count > 0)
                    tabela_dadosEmpresa.AddCell(new Phrase(String.Format("Inscrição Estadual: {0}", (rs_clienteEmpresaEnderecos[0] as ClienteEndereco).inscr), font8));
                else
                    tabela_dadosEmpresa.AddCell(new Phrase(String.Format("Inscrição Estadual: "), font8));

                String strClienteEmpresaContatos = "";
                foreach (ClienteContato clienteContato in rs_clienteEmpresaContatos)
                    strClienteEmpresaContatos += String.Format("{0}: {1}, ", clienteContato.campo, clienteContato.valor);
                Paragraph paragrafo_clienteEmpresaContatos = new Paragraph(String.Format("Contato(s): {0}", strClienteEmpresaContatos), font8);

                Paragraph paragrafo_numeroOrdemServico = new Paragraph(String.Format("Nº Serviço: {0}", ordemServico_db.id), font8);

                PdfPTable tabela_dadosOrdemServico = new PdfPTable(3);
                tabela_dadosOrdemServico.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela_dadosOrdemServico.DefaultCell.Border = 0;
                tabela_dadosOrdemServico.WidthPercentage = 100;
                tabela_dadosOrdemServico.AddCell(new Phrase(String.Format("Data início: {0}", ordemServico_db.dthrLancamento.Substring(0, 10)), font8));
                tabela_dadosOrdemServico.AddCell(new Phrase(String.Format("Data previsão: {0}", ordemServico_db.dthrPrevisao.Substring(0, 10)), font8));
                tabela_dadosOrdemServico.AddCell(new Phrase(String.Format("Vendedor: {0}", clienteFuncionario_db.nome), font8));
                tabela_dadosOrdemServico.AddCell(new Phrase(String.Format("Cliente: {0}-{1}", ordemServico_db.idCliente, ordemServico_db.cliente_nome), font8));
                tabela_dadosOrdemServico.AddCell(new Phrase(String.Format("Contato: {0}", ordemServico_db.cliente_contato), font8));
                tabela_dadosOrdemServico.AddCell(new Phrase(formata_cpf_cnpj(ordemServico_db.cliente_cpf, true), font8));

                Paragraph paragrafo_enderecoCliente = new Paragraph(String.Format("Endereço: {0}", ordemServico_db.cliente_endereco_cobranca), font8);

                PdfPTable tabela_itensTipoOrdemServico = new PdfPTable(2);
                tabela_itensTipoOrdemServico.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela_itensTipoOrdemServico.DefaultCell.Border = 0;
                tabela_itensTipoOrdemServico.WidthPercentage = 100;
                int contador = 0;
                if (ordemServicoTipo_db.veiculo)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Veículo: {0}", ordemServico_db.veiculo), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.placa)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Placa: {0}", ordemServico_db.placa), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.kilometragem)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Kilometragem: {0}", ordemServico_db.kilometragem), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.numMotor)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Nº do Motor: {0}", ordemServico_db.numMotor), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.maquina)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Máquina: {0}", ordemServico_db.maquina), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.implAgricola)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Impl. Agrícola: {0}", ordemServico_db.implAgricola), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.equipamento)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Equipamento: {0}", ordemServico_db.equipamento), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.numSerie)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Nº Série: {0}", ordemServico_db.numSerie), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.servico)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Serviço: {0}", ordemServico_db.servico), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.defeitoReclamado)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Defeito Reclamado: {0}", ordemServico_db.defeitoReclamado), font8));
                    contador++;
                }
                if (ordemServicoTipo_db.defeitoConstatado)
                {
                    tabela_itensTipoOrdemServico.AddCell(new Phrase(String.Format("Defeito Contatado: {0}", ordemServico_db.defeiroConstatado), font8));
                    contador++;
                }
                if (contador % 2 != 0)
                {
                    cell.Border = 0;
                    tabela_itensTipoOrdemServico.AddCell(cell);
                }

                float[] largurasTabela = new float[] { 45, 5, 10, 10, 25, 5 };
                PdfPTable tabela_itensOrdemServico = new PdfPTable(6);
                tabela_itensOrdemServico.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela_itensOrdemServico.WidthPercentage = 100;
                tabela_itensOrdemServico.SetWidths(largurasTabela);
                tabela_itensOrdemServico.AddCell(new Phrase("Item", font8));
                tabela_itensOrdemServico.AddCell(new Phrase("Qtd.", font8));
                tabela_itensOrdemServico.AddCell(new Phrase("Vlr. Unit.", font8));
                tabela_itensOrdemServico.AddCell(new Phrase("Vlr. Total", font8));
                tabela_itensOrdemServico.AddCell(new Phrase("Executores", font8));
                tabela_itensOrdemServico.AddCell(new Phrase("Tipo", font8));

                Dictionary<string, double> dicionarioValorPorItem = new Dictionary<String, Double>();
                foreach (OrdemServico_Item ordemServicoItem in rs_ordemServicoItens)
                {
                    String ordemServicoExecutores = "";
                    query = db.Query();
                    query.Constrain(typeof(OrdemServico_Executor));
                    query.Descend("idOrdemServicoItem").Constrain(ordemServicoItem.id);
                    IObjectSet rs_ordemServicoExecutores = query.Execute();
                    foreach (OrdemServico_Executor ordemServicoExecutor in rs_ordemServicoExecutores)
                    {
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(ordemServicoExecutor.idClienteExecutor);
                        IObjectSet rs_clienteExecutor = query.Execute();
                        ordemServicoExecutores += String.Format("{0}\n", (rs_clienteExecutor[0] as Cliente).nome);
                    }

                    tabela_itensOrdemServico.AddCell(new Phrase(ordemServicoItem.item_nome, font8));
                    tabela_itensOrdemServico.AddCell(new Phrase(ordemServicoItem.qtd.ToString(), font8));
                    tabela_itensOrdemServico.AddCell(new Phrase(Utils.formatMoney(Convert.ToDecimal(ordemServicoItem.vlrUnitVendaFinal), true), font8));
                    tabela_itensOrdemServico.AddCell(new Phrase(Utils.formatMoney(Convert.ToDecimal(ordemServicoItem.vlrUnitVendaFinalQtd), true), font8));
                    tabela_itensOrdemServico.AddCell(new Phrase(ordemServicoExecutores, font8));
                    tabela_itensOrdemServico.AddCell(new Phrase(ordemServicoItem.tipoItem, font8));

                    if (dicionarioValorPorItem.ContainsKey(ordemServicoItem.tipoItem))
                        dicionarioValorPorItem[ordemServicoItem.tipoItem] += ordemServicoItem.vlrUnitVendaFinalQtd;
                    else
                        dicionarioValorPorItem.Add(ordemServicoItem.tipoItem, ordemServicoItem.vlrUnitVendaFinalQtd);
                }

                Double totalOrdemServico = 0;
                PdfPTable tabela_totais = new PdfPTable(1);
                tabela_totais.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela_totais.DefaultCell.Border = 0;
                tabela_totais.WidthPercentage = 100;
                foreach (String chave in dicionarioValorPorItem.Keys)
                {
                    tabela_totais.AddCell(new Phrase(String.Format("Total de {0}: {1}", defineTipoOrdemServico(chave), Utils.formatMoney(Convert.ToDecimal(dicionarioValorPorItem[chave]), true)), font8));
                    totalOrdemServico += dicionarioValorPorItem[chave];
                }
                tabela_totais.AddCell(new Phrase(String.Format("Total da Ordem de Serviço: {0}", Utils.formatMoney(Convert.ToDecimal(totalOrdemServico), true)), font8));

                PdfPTable tabela_assinaturaCliente = new PdfPTable(1);
                tabela_assinaturaCliente.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabela_assinaturaCliente.DefaultCell.Border = 0;
                tabela_assinaturaCliente.AddCell("______________________________________________________");
                tabela_assinaturaCliente.AddCell(new Phrase(cliente_db.nome, font8));

                documento.Open();

                documento.Add(tabela_titulo);
                documento.Add(tabela_dadosEmpresa);
                documento.Add(paragrafo_clienteEmpresaContatos);
                documento.Add(separadorHorizontal);
                documento.Add(paragrafo_numeroOrdemServico);
                documento.Add(tabela_dadosOrdemServico);
                documento.Add(paragrafo_enderecoCliente);
                documento.Add(separadorHorizontal);
                documento.Add(tabela_itensTipoOrdemServico);
                documento.Add(tabela_itensOrdemServico);
                documento.Add(tabela_totais);
                documento.Add(tabela_assinaturaCliente);

                documento.Close();
            }
            catch (Exception ex)
            {
                documento.Close();
                throw new Exception("ERRO: " + ex.StackTrace);
            }
        }

        #region Inventário

        public static void relatorioInventario(int idCorp, int idEmp, bool porGrupo, string tipoPreco,
            double pctSobreValor, string dataInventario, string textoCabecalho, bool mostraZerados)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            //pesquisas

            Empresa empresa = null;
            query = db.Query();
            query.Constrain(typeof(Empresa));
            query.Descend("id").Constrain(1);
            if (query.Execute().Count == 0)
                throw new Exception("Empresa não encontrada. ID: " + 1);
            else
                empresa = query.Execute()[0] as Empresa;

            Cliente clienteEmpresa = null;
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(empresa.idCliente);
            if (query.Execute().Count == 0)
                throw new Exception("Cliente (Empresa) não encontrado. ID: " + empresa.idCliente);
            else
                clienteEmpresa = query.Execute()[0] as Cliente;

            ClienteEndereco clienteEndereco = null;
            foreach (ClienteEndereco ce in db.Query<ClienteEndereco>())
            {
                if (ce.idCliente != clienteEmpresa.id)
                    continue;
                clienteEndereco = ce;
                break;
            }
            if (clienteEndereco == null)
                throw new Exception("Cliente Endereço (Empresa) não encontrado. ID CLIENTE: " + clienteEmpresa.id);


            ////Contrução PDF

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\inventario.pdf";

            Font baseFont = new Font();
            baseFont.Size = 8;
            Document myPdfDoc = new Document(PageSize.A4.Rotate());
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();

            int paginaAtual = 1;
            int contadorLinhas = 0;

            double valorTotalInventario = 0;

            myPdfDoc.Add(escreveCabecalho(textoCabecalho, clienteEmpresa.apelido_razsoc, clienteEmpresa.cpf_cnpj, clienteEndereco.inscr, dataInventario, paginaAtual, valorTotalInventario, tipoPreco));

            List<Item> listaItem = new List<Item>();
            query = db.Query();
            query.Constrain(typeof(Item));
            query.Descend("tipo").Constrain(EItemTipo.produto);
            foreach (Item item in query.Execute())
                listaItem.Add(item);
            foreach (Item item in listaItem.OrderBy(xxx => xxx.nome))
            {
                double qtdItens = 0;
                double preco = 0;
                double precoTotal = 0;

                foreach (ItemEmpEstoque iee in db.Query<ItemEmpEstoque>())
                    if (iee.idItem == item.id)
                        qtdItens += iee.qtd;

                if (qtdItens == 0 && !mostraZerados)
                    continue;

                foreach (ItemEmpPreco iep in db.Query<ItemEmpPreco>())
                {
                    if (iep.idItem != item.id)
                        continue;
                    if (tipoPreco == "Venda")
                        preco = iep.venda;
                    else if (tipoPreco == "Compra")
                        preco = iep.compra;
                    else if (tipoPreco == "Custo")
                        preco = iep.custo;
                    break;
                }

                preco = defineValor(preco, pctSobreValor);
                precoTotal = preco * qtdItens;


                if (contadorLinhas == 36)
                {
                    PdfPTable tableSaldoTransporte = new PdfPTable(1);
                    tableSaldoTransporte.WidthPercentage = 100;

                    PdfPCell cellSaldoTransporte = new PdfPCell(new Phrase("Subtotal Geral (A Transportar): " + formatMoney(Decimal.Parse(valorTotalInventario.ToString()), true), baseFont));
                    cellSaldoTransporte.HorizontalAlignment = Element.ALIGN_RIGHT;

                    tableSaldoTransporte.AddCell(cellSaldoTransporte);
                    myPdfDoc.Add(tableSaldoTransporte);

                    paginaAtual++;
                    myPdfWriter.NewPage();
                    myPdfDoc.Add(escreveCabecalho(textoCabecalho, clienteEmpresa.apelido_razsoc, clienteEmpresa.cpf_cnpj, clienteEndereco.inscr, dataInventario, paginaAtual, valorTotalInventario, tipoPreco));
                    contadorLinhas = 0;
                }

                int[] tableWidths = { 5, 10, 50, 5, 10, 10, 10 };
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(tableWidths);

                table.AddCell(new Phrase(item.id.ToString(), baseFont));
                table.AddCell(new Phrase(item.rfUnica, baseFont));
                table.AddCell(new Phrase((item.nome.Length > 75) ? item.nome.Substring(0, 75) : item.nome, baseFont));
                table.AddCell(new Phrase(item.unidMed.ToString(), baseFont));
                table.AddCell(new Phrase(qtdItens.ToString(), baseFont));
                table.AddCell(new Phrase(formatMoney(Decimal.Parse(preco.ToString()), true), baseFont));
                table.AddCell(new Phrase(formatMoney(Decimal.Parse(precoTotal.ToString()), true), baseFont));

                myPdfDoc.Add(table);

                valorTotalInventario += precoTotal;
                contadorLinhas++;
            }

            PdfPTable tableTotalGeral = new PdfPTable(1);
            tableTotalGeral.WidthPercentage = 100;

            PdfPCell cellTotalGeral = new PdfPCell(new Phrase("Total Geral: " + formatMoney(Decimal.Parse(valorTotalInventario.ToString()), true), baseFont));
            cellTotalGeral.HorizontalAlignment = Element.ALIGN_RIGHT;

            tableTotalGeral.AddCell(cellTotalGeral);
            myPdfDoc.Add(tableTotalGeral);

            myPdfDoc.Close();
        }

        private static double defineValor(double valor, double pctSobreValor)
        {
            if (pctSobreValor > 0)
                return (valor * pctSobreValor) / 100;
            else
                return valor;
        }

        private static PdfPTable escreveCabecalho(string textoCabecalho, string nomeEmpresa, string cpfCnpj, string inscrEstadual, string dataEstoque, int pagina, double saldo, string tipoPreco)
        {
            Font baseFont = new Font();
            baseFont.Size = 8;
            int[] tableWidths;
            PdfPCell cell;

            PdfPTable tableCabecalho = new PdfPTable(1);
            tableCabecalho.WidthPercentage = 100;
            tableCabecalho.DefaultCell.Padding = 0;

            tableWidths = new int[] {100};
            PdfPTable tableCabecalho01 = new PdfPTable(1);
            tableCabecalho01.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase(textoCabecalho, baseFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tableCabecalho01.AddCell(cell);

            PdfPTable tableCabecalho02 = new PdfPTable(10);
            tableCabecalho02.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase("Empresa:", baseFont));
            cell.Colspan = 2;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase(nomeEmpresa, baseFont));
            cell.Colspan = 3;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase("CPF/CNPJ:", baseFont));
            cell.Colspan = 2;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase(formata_cpf_cnpj(cpfCnpj, false), baseFont));
            cell.Colspan = 3;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase("Inscrição Estadual:", baseFont));
            cell.Colspan = 2;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase(inscrEstadual, baseFont));
            cell.Colspan = 3;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase("Estoques Existentes em:", baseFont));
            cell.Colspan = 2;
            tableCabecalho02.AddCell(cell);

            cell = new PdfPCell(new Phrase(dataEstoque, baseFont));
            cell.Colspan = 3;
            tableCabecalho02.AddCell(cell);

            PdfPTable tableCabecalho03 = new PdfPTable(1);
            tableCabecalho03.WidthPercentage = 100;
            tableCabecalho03.DefaultCell.BorderWidth = 0;

            cell = new PdfPCell(new Phrase("Página " + pagina, baseFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableCabecalho03.AddCell(cell);

            cell = new PdfPCell(new Phrase("Saldo de Transporte:" + formatMoney(Decimal.Parse(saldo.ToString()), true), baseFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableCabecalho03.AddCell(cell);

            tableWidths = new int[] { 5, 10, 50, 5, 10, 10, 10 };
            PdfPTable tableCabecalho04 = new PdfPTable(7);
            tableCabecalho04.WidthPercentage = 100;
            tableCabecalho04.SetWidths(tableWidths);
            tableCabecalho04.AddCell(new Phrase("CODIGO", baseFont));
            tableCabecalho04.AddCell(new Phrase("FABRICANTE", baseFont));
            tableCabecalho04.AddCell(new Phrase("DESCRIÇÃO", baseFont));
            tableCabecalho04.AddCell(new Phrase("UND", baseFont));
            tableCabecalho04.AddCell(new Phrase("ESTOQUE ATUAL", baseFont));
            tableCabecalho04.AddCell(new Phrase("VALOR (" + tipoPreco.Substring(0, 1) + ")", baseFont));
            tableCabecalho04.AddCell(new Phrase("VALOR TOTAL", baseFont));

            tableCabecalho.AddCell(tableCabecalho01);
            tableCabecalho.AddCell(tableCabecalho02);
            tableCabecalho.AddCell(tableCabecalho03);
            tableCabecalho.AddCell(tableCabecalho04);
            
            return tableCabecalho;
        }

        #endregion

        public static void duplicataAcoRio(int idCorp, int idEmp, List<Finan_TituloItem> listaFinanTituloItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            //Consultas

            Empresa emp = null;
            query = db.Query();
            query.Constrain(typeof(Empresa));
            query.Descend("id").Constrain(idEmp);
            if (query.Execute().Count == 0)
                throw new Exception("Empresa não encontrada. ID: " + idEmp);
            else
                emp = query.Execute()[0] as Empresa;

            Cliente cliEmp = null;
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(emp.idCliente);
            if (query.Execute().Count == 0)
                throw new Exception("Cliente Empresa não encontrado. ID: " + emp.idCliente);
            else
                cliEmp = query.Execute()[0] as Cliente;

            ClienteEndereco cliEmpEndereco = null;
            foreach (ClienteEndereco ce in db.Query<ClienteEndereco>())
            {
                if (!(ce.idCliente == cliEmp.id) || !(ce.tipo == EEnderecoTipo.residencial_comercial))
                    continue;
                cliEmpEndereco = ce;
                break;
            }
            if (cliEmpEndereco == null)
                throw new Exception("Endereço da Empresa não encontrada (Empresa). ID Cliente: " + cliEmp.id);

            ClienteContato cliEmpContato = null;
            foreach (ClienteContato cc in db.Query<ClienteContato>())
            {
                if (!(cc.idCliente == cliEmp.id) || !(cc.tipo == EContatoTipo.fone_fixo))
                    continue;
                cliEmpContato = cc;
                break;
            }
            if (cliEmpContato == null)
                throw new Exception("Contato da Empresa não encontrado (Empresa). ID Cliente: " + cliEmp.id);

            ////Contrução PDF

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\duplicata.pdf";

            Document myPdfDoc = new Document(PageSize.A4);
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            Font baseFont8 = new Font();
            baseFont8.Size = 8;

            Font baseFont6 = new Font();
            baseFont6.Size = 6;
            
            PdfPCell cell;

            Paragraph linha = new Paragraph("\n", baseFont8);

            PdfPCell colspanCell2 = new PdfPCell();
            colspanCell2.BorderWidthBottom = 0;
            colspanCell2.BorderWidthTop = 0;
            colspanCell2.Colspan = 2;

            myPdfDoc.Open();

            foreach (Finan_TituloItem fti in listaFinanTituloItem)
            {
                //consultas

                Finan_Titulo finanTitulo = null;
                query = db.Query();
                query.Constrain(typeof(Finan_Titulo));
                query.Descend("id").Constrain(listaFinanTituloItem[0].idTitulo);
                if (query.Execute().Count == 0)
                    throw new Exception("Titulo não encontrado. ID: " + fti.idTitulo);
                else
                    finanTitulo = query.Execute()[0] as Finan_Titulo;

                Mov mov = null;
                query = db.Query();
                query.Constrain(typeof(Mov));
                query.Descend("idTransacao").Constrain(finanTitulo.idTransacao);
                if (query.Execute().Count == 0)
                    throw new Exception("Movimentação feretente ai título não encontrada. ID: " + finanTitulo.idTransacao);
                else
                    mov = query.Execute()[0] as Mov;

                Cliente cliente = null;
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(mov.idCliente);
                if (query.Execute().Count == 0)
                    throw new Exception("Cliente não encontrado. ID: " + mov.idCliente);
                else
                    cliente = query.Execute()[0] as Cliente;

                ClienteEndereco clienteEnd = null;
                foreach (ClienteEndereco ce in db.Query<ClienteEndereco>())
                {
                    if (!(cliente.id == ce.idCliente))
                        continue;
                    clienteEnd = ce;
                    break;
                }
                if (clienteEnd == null)
                    throw new Exception("ClienteEndereco não encontrado. ID Cliente: " + cliente.id);

                ClienteContato clienteCont = null;
                foreach (ClienteContato cc in db.Query<ClienteContato>())
                {
                    if (!(cliente.id == cc.idCliente) || !(cc.tipo == EContatoTipo.fone_fixo))
                        continue;
                    clienteCont = cc;
                    break;
                }
                if (clienteCont == null)
                    clienteCont = new ClienteContato();

                //inicio PDF

                int[] tableWidths;

                tableWidths = new int[] { 70, 30 };
                PdfPTable tableHead = new PdfPTable(2);
                tableHead.DefaultCell.Padding = 0;
                tableHead.SetWidths(tableWidths);
                tableHead.WidthPercentage = 100;

                tableWidths = new int[] { 100 };
                PdfPTable tableHead_Cell01 = new PdfPTable(1);
                tableHead_Cell01.SetWidths(tableWidths);
                tableHead_Cell01.DefaultCell.BorderWidth = 0;
                tableHead_Cell01.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHead_Cell01.AddCell(new Phrase(cliEmp.nome, baseFont8));
                tableHead_Cell01.AddCell(new Phrase(cliEmpEndereco.logradouro + "-" + cliEmpEndereco.bairro, baseFont8));
                tableHead_Cell01.AddCell(new Phrase(cliEmpEndereco.cidade + "-" + cliEmpEndereco.uf +
                    " CEP: " + cliEmpEndereco.cep + " Fone: " + cliEmpContato.valor, baseFont8));

                tableWidths = new int[] { 50, 50 };
                PdfPTable tableHead_Cell02 = new PdfPTable(2);
                tableHead_Cell02.SetWidths(tableWidths);
                tableHead_Cell02.DefaultCell.BorderWidth = 0;
                tableHead_Cell02.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tableHead_Cell02.AddCell(new Phrase("CNPJ:", baseFont8));
                tableHead_Cell02.AddCell(new Phrase(formata_cpf_cnpj(cliEmp.cpf_cnpj, false), baseFont8));
                tableHead_Cell02.AddCell(new Phrase("INSC. ESTADUAL:", baseFont8));
                tableHead_Cell02.AddCell(new Phrase(cliEmpEndereco.inscr, baseFont8));
                tableHead_Cell02.AddCell(new Phrase("DATA DE EMISSÃO:", baseFont8));
                tableHead_Cell02.AddCell(new Phrase(Utils.getAgoraString(), baseFont8));

                tableHead.AddCell(tableHead_Cell01);
                tableHead.AddCell(tableHead_Cell02);

                //tableWidths = new int[] { 100 };
                PdfPTable tableInfoTitulo = new PdfPTable(8);
                tableInfoTitulo.WidthPercentage = 100;
                tableInfoTitulo.DefaultCell.BorderWidth = 0;

                cell = new PdfPCell(new Phrase("FATURA", baseFont8));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("DUPLICATA", baseFont8));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("VENCIMENTO", baseFont8));
                cell.Colspan = 2;
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("Para uso da Instituição Financeira", baseFont8));
                cell.Colspan = 2;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderWidthBottom = 0;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("NUMERO", baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("VALOR R$", baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("NR. DE ORDEM", baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase("VALOR R$", baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase(fti.identificador, baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase(formatMoney(Decimal.Parse(fti.valorCobrado.ToString()), true), baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase(finanTitulo.identificador, baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase(formatMoney(Decimal.Parse(finanTitulo.valorCobrado.ToString()), true), baseFont8));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                cell = new PdfPCell(new Phrase(fti.dtPagamento, baseFont8));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableInfoTitulo.AddCell(cell);

                //tableWidths = new int[] { 100 };
                PdfPTable tableInfoDesconto = new PdfPTable(8);
                tableInfoDesconto.WidthPercentage = 100;
                tableInfoDesconto.DefaultCell.BorderWidth = 0;

                tableInfoDesconto.AddCell(colspanCell2);

                cell = new PdfPCell(new
                Phrase("Desconto de: " + fti.descontoPct + "% sobre " + formatMoney(Decimal.Parse(fti.descontoVlr.ToString()), true) + " Até: " + fti.descontoValidade, baseFont8));
                cell.Colspan = 4;
                cell.BorderWidthBottom = 0;
                tableInfoDesconto.AddCell(cell);

                tableInfoDesconto.AddCell(colspanCell2);
                tableInfoDesconto.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Cond. Especiais: " + fti.condEspeciais, baseFont8));
                cell.Colspan = 4;
                cell.BorderWidthTop = 0;
                tableInfoDesconto.AddCell(cell);

                cell = new PdfPCell();
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                tableInfoDesconto.AddCell(cell);

                //tableWidths = new int[] { 100 };
                PdfPTable tableInfoSacado = new PdfPTable(8);
                tableInfoSacado.WidthPercentage = 100;
                tableInfoSacado.DefaultCell.BorderWidth = 0;

                tableInfoSacado.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Nome do Sacado: " + cliente.nome, baseFont8));
                cell.Colspan = 6;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                tableInfoSacado.AddCell(cell);

                tableInfoSacado.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Endereço: " + clienteEnd.logradouro, baseFont8));
                cell.Colspan = 4;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthRight = 0;
                tableInfoSacado.AddCell(cell);

                cell = new PdfPCell(new Phrase("Telefone: " + clienteCont.valor, baseFont8));
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                tableInfoSacado.AddCell(cell);

                tableInfoSacado.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Municipio: " + clienteEnd.cidade, baseFont8));
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthRight = 0;
                tableInfoSacado.AddCell(cell);

                cell = new PdfPCell(new Phrase("Estado: " + clienteEnd.uf, baseFont8));
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                cell.BorderWidthRight = 0;
                tableInfoSacado.AddCell(cell);

                cell = new PdfPCell(new Phrase("CEP: " + clienteEnd.cep, baseFont8));
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                tableInfoSacado.AddCell(cell);

                tableInfoSacado.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Local de Pagamento: " + clienteEnd.cidade + "-" + clienteEnd.uf, baseFont8));
                cell.Colspan = 6;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                tableInfoSacado.AddCell(cell);

                tableInfoSacado.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("CPF/CNPJ: " + formata_cpf_cnpj(cliente.cpf_cnpj, false), baseFont8));
                cell.Colspan = 3;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthRight = 0;
                tableInfoSacado.AddCell(cell);

                cell = new PdfPCell(new Phrase("Insc. Est.:" + clienteEnd.inscr, baseFont8));
                cell.Colspan = 3;
                cell.BorderWidthTop = 0;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                tableInfoSacado.AddCell(cell);

                //tableWidths = new int[] { 100 };
                PdfPTable tableValor = new PdfPTable(8);
                tableValor.WidthPercentage = 100;

                tableValor.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Valor por Extenso: " + ConverteValorPorExtenso.MoedaPorExtenso(fti.valorCobrado), baseFont8));
                cell.Colspan = 6;
                tableValor.AddCell(cell);

                //tableWidths = new int[] { 100 };
                PdfPTable tableRodape = new PdfPTable(8);
                tableRodape.WidthPercentage = 100;

                tableRodape.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Reconheço(emos) a exatidão desta duplicata de VENDA MERCANTIL na importancia acima que pagarei(emos) a "+
                    "____________________________________________________________________________ "+
                    "ou a sua ordem na praça e vencimentos indicados.", baseFont8));
                cell.Colspan = 6;
                tableRodape.AddCell(cell);

                tableRodape.AddCell(colspanCell2);

                cell = new PdfPCell(new Phrase("Em: ____/____/________", baseFont8));
                cell.Colspan = 2;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthRight = 0;
                tableRodape.AddCell(cell);

                cell = new PdfPCell(new Phrase("_________________________________", baseFont8));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderWidthBottom = 0;
                cell.BorderWidthLeft = 0;
                tableRodape.AddCell(cell);

                cell = new PdfPCell();
                cell.Colspan = 2;
                cell.BorderWidthTop = 0;
                tableRodape.AddCell(cell);

                PdfPCell nCell = new PdfPCell();
                nCell.Colspan = 2;
                nCell.BorderWidthTop = 0;
                nCell.BorderWidthRight = 0;
                tableRodape.AddCell(nCell);

                cell = new PdfPCell(new Phrase("Assinatura do Sacado", baseFont8));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                tableRodape.AddCell(cell);

                tableWidths = new int[] { 100 };
                PdfPTable tablePrincipal = new PdfPTable(1);
                tablePrincipal.WidthPercentage = 100;
                tablePrincipal.DefaultCell.Padding = 0;
                tablePrincipal.SetWidths(tableWidths);
                tablePrincipal.DefaultCell.BorderWidth = 0;

                tablePrincipal.AddCell(tableHead);
                tablePrincipal.AddCell(tableInfoTitulo);
                tablePrincipal.AddCell(tableInfoDesconto);
                tablePrincipal.AddCell(tableInfoSacado);
                tablePrincipal.AddCell(tableValor);
                tablePrincipal.AddCell(tableRodape);

                myPdfDoc.Add(tablePrincipal);

                myPdfDoc.Add(linha);
                myPdfDoc.Add(linha);
            }

            myPdfDoc.Close();
        }

        #region Etiquetas

        #region Pintando 7

        public static void EtiquetaSETE(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;

            ////Contrução PDF
            
            Font baseFont = new Font();
            baseFont.Size = 8;

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\"+idCorp+@"\etiqueta.pdf";

            Document myPdfDoc = new Document(PageSize.A4, 7, 0, 6, 0);

            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();
            PdfContentByte cb = myPdfWriter.DirectContent;

            float[] tableWidths = { 20, 20, float.Parse("19,5"), float.Parse("19,5"), 21 };
            PdfPTable tablePrincipal = new PdfPTable(5);
            tablePrincipal.WidthPercentage = 100;
            tablePrincipal.SetWidths(tableWidths);
            tablePrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tablePrincipal.DefaultCell.PaddingRight = -1;
            tablePrincipal.DefaultCell.PaddingBottom = float.Parse("7,5");

            int qtdEtiquetas = 0;

            foreach (MovItem movItem in listaMovItem)
            {
                Item item = null;
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("Item não encontrado. ID: " + movItem.idItem);
                else
                    item = query.Execute()[0] as Item;

                ItemEmpEstoque itemEmpEstoque = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpEstoque));
                query.Descend("idItem").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpEstoque. ID ITEM: " + movItem.idItem);
                else
                    foreach (ItemEmpEstoque iee in query.Execute())
                        if (iee.idEmp == idEmp)
                            itemEmpEstoque = iee;

                ItemEmpPreco itemEmpPreco = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpPreco));
                query.Descend("idItem").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpPreço não encontrado. ID ITEM: " + movItem.idItem);
                else
                    foreach (ItemEmpPreco iep in query.Execute())
                        if (iep.idEmp == idEmp)
                            itemEmpPreco = iep;

                Barcode39 codigoBarras = new Barcode39();
                codigoBarras.Code = itemEmpEstoque.codBarras;
                codigoBarras.StartStopText = false;
                codigoBarras.BarHeight = 33;
                codigoBarras.N = 2;
                Image imagemCodigoBarras = codigoBarras.CreateImageWithBarcode(cb, null, null);

                double qtd = movItem.qtd;

                while (qtd > 0)
                {
                    PdfPTable tableEtiqueta = new PdfPTable(1);
                    tableEtiqueta.WidthPercentage = 100;
                    tableEtiqueta.DefaultCell.Padding = 0;
                    tableEtiqueta.DefaultCell.Border = Rectangle.NO_BORDER;

                    PdfPCell cellCodigoBarras = new PdfPCell(new Phrase(new Chunk(imagemCodigoBarras, 0, 0)));
                    cellCodigoBarras.Border = Rectangle.NO_BORDER;
                    tableEtiqueta.AddCell(cellCodigoBarras);
                    tableEtiqueta.AddCell(new Phrase(formatMoney(Decimal.Parse(itemEmpPreco.venda.ToString()), true), baseFont));
                    tableEtiqueta.AddCell(new Phrase(item.rfUnica + "-" + ((item.nomeEtiqueta.Length > 15) ? item.nomeEtiqueta.Substring(0, 15) : item.nomeEtiqueta), baseFont));

                    tablePrincipal.AddCell(tableEtiqueta);
                    qtd--;
                    qtdEtiquetas++;
                }
            }

            int resto;
            int qtdResto = qtdEtiquetas % 5;
            if (qtdResto > 0)
                resto = 5 - qtdResto;
            else if (qtdEtiquetas < 5)
                resto = 5 - qtdEtiquetas;
            else
                resto = qtdEtiquetas % 5;

            while (resto > 0)
            {
                PdfPCell cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                tablePrincipal.AddCell(cell);
                resto--;
            }

            myPdfDoc.Add(tablePrincipal);

            myPdfDoc.Close();
        }

        #endregion

        #region Wembley
        // Modelo Etiqueta PIMACO A4 248 | Proporções: 17,00 mm x 31,00 mm

        public static void EtiquetaWembley(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;

            ////Contrução PDF

            Font baseFont = new Font();
            baseFont.Size = 12;
            baseFont.IsBold();

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\etiqueta.pdf";

            Document myPdfDoc = new Document(PageSize.A4, 19.44f, 19.44f, 35.84f, 0);

            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();

            PdfPTable tablePrincipal = new PdfPTable(6);
            tablePrincipal.WidthPercentage = 100;
            tablePrincipal.DefaultCell.PaddingTop = 6.76f;
            tablePrincipal.DefaultCell.PaddingBottom = 5.6f;
            tablePrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            int qtdEtiquetas = 0;

            foreach (MovItem movItem in listaMovItem)
            {
                Item item = null;
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("Item não encontrado. ID: " + movItem.idItem);
                else
                    item = query.Execute()[0] as Item;

                ItemEmpPreco itemEmpPreco = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpPreco));
                query.Descend("idItem").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpPreço não encontrado. ID ITEM: " + movItem.idItem);
                else
                    foreach (ItemEmpPreco iep in query.Execute())
                        if (iep.idEmp == idEmp)
                            itemEmpPreco = iep;

                double qtd = movItem.qtd;

                while (qtd > 0)
                {
                    PdfPTable tableEtiqueta = new PdfPTable(1);
                    tableEtiqueta.WidthPercentage = 100;
                    tableEtiqueta.DefaultCell.Padding = 0;
                    tableEtiqueta.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableEtiqueta.DefaultCell.PaddingLeft = 13.68f;

                    tableEtiqueta.AddCell(new Phrase(item.rfUnica, baseFont));
                    tableEtiqueta.AddCell(new Phrase(item.rfAuxiliar, baseFont));
                    tableEtiqueta.AddCell(new Phrase(formatMoney(Decimal.Parse(itemEmpPreco.venda.ToString()), true), baseFont));

                    tablePrincipal.AddCell(tableEtiqueta);
                    qtd--;
                    qtdEtiquetas++;
                }
            }

            int resto;
            if (qtdEtiquetas < 6)
                resto = 6 - qtdEtiquetas;
            else
                resto = qtdEtiquetas % 6;

            while (resto > 0)
            {
                PdfPCell cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                tablePrincipal.AddCell(cell);
                resto--;
            }

            myPdfDoc.Add(tablePrincipal);

            myPdfDoc.Close();
        }

        #endregion

        #region Moda Morena

        private static PdfPTable tablePrincipal = null;
        public static void EtiquetaModaMorena(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            iTextSharp.text.Rectangle PIMATAB_8936 = new iTextSharp.text.Rectangle(252f, 875.52f);
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font baseFont10 = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD, Color.BLACK);

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\etiqueta.pdf";

            Document myPdfDoc = new Document(PIMATAB_8936, 0, 0, 8, 0);
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();

            try
            {
                iniciaTablePrincipal();

                double qtd = 0;
                foreach (MovItem mi in listaMovItem)
                    qtd += mi.qtd;
                double impressas = 0;
                int totalImpressas = 0;

                foreach (MovItem mi in listaMovItem)
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(mi.idItem);
                    IObjectSet rs_Item = query.Execute();

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpPreco));
                    query.Descend("idItem").Constrain(mi.idItem);
                    IObjectSet rs_iep = query.Execute();

                    Item item = rs_Item[0] as Item;
                    ItemEmpPreco iep = rs_iep[0] as ItemEmpPreco;

                    string codigoItem = item.id.ToString();
                    while (codigoItem.Length < 7)
                        codigoItem = "0" + codigoItem;

                    string nomeItem = (item.nomeEtiqueta.Length > 33) ? item.nomeEtiqueta.Substring(0, 33) : item.nomeEtiqueta;

                    for (int i = 0; i < mi.qtd; i++)
                    {
                        PdfPTable tableEtiqueta = new PdfPTable(1);
                        tableEtiqueta.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        tableEtiqueta.DefaultCell.PaddingTop = 0f;
                        tableEtiqueta.DefaultCell.PaddingBottom = 0f;
                        tableEtiqueta.AddCell(new Phrase(string.Format("{0}-{1}", codigoItem, nomeItem), baseFont10));
                        tableEtiqueta.AddCell(new Phrase(string.Format("Ref: {0}", item.rfUnica), baseFont10));
                        tableEtiqueta.AddCell(new Phrase(string.Format("A Vista: {0}", Utils.formatMoney(Convert.ToDecimal(iep.venda), true)), baseFont10));
                        tableEtiqueta.AddCell(new Phrase(string.Format("A Prazo: 4x {0}", Utils.formatMoney(Convert.ToDecimal((((iep.venda * 7.51) / 100) + iep.venda) / 4), true)), baseFont10));
                        tableEtiqueta.AddCell(new Phrase("Entrada, 30, 60 e 90 dias", baseFont10));
                        tableEtiqueta.AddCell(new Phrase("Taxa de Juros 7,51% ao mês", baseFont10));
                        tablePrincipal.AddCell(tableEtiqueta);
                        impressas++;
                        totalImpressas++;

                        if (impressas == 8 && totalImpressas < qtd)
                        {
                            myPdfDoc.Add(tablePrincipal);
                            iniciaTablePrincipal();
                            impressas = 0;
                        }
                    }
                }

                myPdfDoc.Add(tablePrincipal);
                myPdfDoc.Close();
            }
            catch (Exception ex)
            {
                myPdfDoc.Close();
                throw new Exception("Ocorreu um erro durante a construção do documento: " + ex.Message);
            }
        }

        private static void iniciaTablePrincipal()
        {
            tablePrincipal = new PdfPTable(1);
            tablePrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tablePrincipal.DefaultCell.PaddingTop = 0f;
            tablePrincipal.DefaultCell.PaddingBottom = 47.5f;
        }

        #endregion

        #region Antonieta Casa

        public static void EtiquetaAntonietaCasa(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            Font font6 = new Font(baseFont, 6, Font.BOLD, Color.BLACK);
            Font font8 = new Font(baseFont, 8, Font.BOLD, Color.BLACK);
            Paragraph linha = new Paragraph("\n", new Font(baseFont, 2, Font.NORMAL, Color.BLACK));

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            String filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\etiqueta.pdf";

            Document documento = new Document(PageSize.A4, 8, 6, 32, 30);
            PdfWriter pdfWriter = PdfWriter.GetInstance(documento, new FileStream(filePath, FileMode.Create));

            documento.Open();
            PdfContentByte contentByte = pdfWriter.DirectContent;

            try
            {
                PdfPTable tabelaPrincipal = new PdfPTable(5);
                tabelaPrincipal.WidthPercentage = 100;
                tabelaPrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int quantidadeEtiquetas = 0;

                foreach (MovItem movItem in listaMovItem)
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(movItem.idItem);
                    IObjectSet rs_item = query.Execute();
                    Item item = rs_item[0] as Item;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpPreco));
                    query.Descend("idItem").Constrain(movItem.idItem);
                    query.Descend("idEmp").Constrain(idEmp);
                    IObjectSet rs_itemEmpPreco = query.Execute();
                    ItemEmpPreco itemEmpPreco = rs_itemEmpPreco[0] as ItemEmpPreco;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpEstoque));
                    query.Descend("id").Constrain(movItem.idIEE);
                    IObjectSet rs_itemEmpEstoque = query.Execute();
                    ItemEmpEstoque itemEmpEstoque = rs_itemEmpEstoque[0] as ItemEmpEstoque;

                    Barcode39 codigoBarras = new Barcode39();
                    codigoBarras.Code = itemEmpEstoque.codBarras;
                    codigoBarras.StartStopText = false;
                    codigoBarras.BarHeight = 13.7f;
                    codigoBarras.N = 2;
                    codigoBarras.Font = baseFont;
                    Image imagemCodigoBarras = codigoBarras.CreateImageWithBarcode(contentByte, null, null);

                    double quantidade = movItem.qtd;

                    while (quantidade > 0)
                    {
                        PdfPCell celulaEtiqueta = new PdfPCell();
                        celulaEtiqueta.PaddingLeft = 8;
                        celulaEtiqueta.Border = Rectangle.NO_BORDER;

                        celulaEtiqueta.AddElement(new Phrase(item.nomeEtiqueta, font6));
                        celulaEtiqueta.AddElement(new Phrase(itemEmpEstoque.identificador, font6));
                        celulaEtiqueta.AddElement(linha);
                        celulaEtiqueta.AddElement(new Phrase(new Chunk(imagemCodigoBarras, 0, 0)));
                        celulaEtiqueta.AddElement(new Phrase(Utils.formatMoney(Convert.ToDecimal(itemEmpPreco.venda), true), font8));

                        tabelaPrincipal.AddCell(celulaEtiqueta);
                        quantidade--;
                        quantidadeEtiquetas++;
                    }
                }

                int resto;
                int quantidadeResto = quantidadeEtiquetas % 5;
                if (quantidadeResto > 0)
                    resto = 5 - quantidadeResto;
                else if (quantidadeEtiquetas < 5)
                    resto = 5 - quantidadeEtiquetas;
                else
                    resto = quantidadeEtiquetas % 5;

                while (resto > 0)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.Border = Rectangle.NO_BORDER;
                    tabelaPrincipal.AddCell(cell);
                    resto--;
                }

                documento.Add(tabelaPrincipal);

                documento.Close();
            }
            catch (Exception ex)
            {
                documento.Close();
                throw new Exception("ERRO: " + ex.Message);
            }
        }

        #endregion

        #region Costa Azul

        public static void EtiquetaCostaAzul(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font baseFont10 = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font baseFont14 = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font baseFont18 = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD, Color.BLACK);

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\etiqueta.pdf";

            Document myPdfDoc = new Document(PageSize.A4, 20, 20, 20, 5);
            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();

            float[] tableWidths = { float.Parse("33.33"), float.Parse("33.33"), float.Parse("33.33") };
            PdfPTable tabelaPrincipal = new PdfPTable(3);
            tabelaPrincipal.SetWidths(tableWidths);
            tabelaPrincipal.WidthPercentage = 100;
            tabelaPrincipal.DefaultCell.PaddingLeft = 10;
            tabelaPrincipal.DefaultCell.PaddingBottom = float.Parse("6,2");
            tabelaPrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            int qtdEtiquetas = 0;
            foreach (MovItem mi in listaMovItem)
            {
                Item item = null;
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(mi.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("Item não encontrado. ID: " + mi.idItem);
                else
                    item = query.Execute()[0] as Item;

                ItemEmpPreco itemEmpPreco = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpPreco));
                query.Descend("idItem").Constrain(mi.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpPreço não encontrado. ID ITEM: " + mi.idItem);
                else
                    foreach (ItemEmpPreco iep in query.Execute())
                        if (iep.idEmp == idEmp)
                            itemEmpPreco = iep;

                double qtd = mi.qtd;

                while (qtd > 0)
                {
                    PdfPCell cellItemNome;
                    if (item.nomeEtiqueta.Length <= 27)
                        cellItemNome = new PdfPCell(new Phrase(string.Format("{0}", item.nomeEtiqueta + "          ."), baseFont10));
                    else
                        cellItemNome = new PdfPCell(new Phrase(string.Format("{0}", item.nomeEtiqueta), baseFont10));
                    cellItemNome.Border = 0;
                    PdfPTable tabelaEtiqueta = new PdfPTable(1);
                    tabelaEtiqueta.WidthPercentage = 100;
                    tabelaEtiqueta.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tabelaEtiqueta.AddCell(new Phrase(string.Format("Cod:{0}", item.id), baseFont18));
                    tabelaEtiqueta.AddCell(cellItemNome);
                    tabelaEtiqueta.AddCell(new Phrase(string.Format("{0}", Utils.formatMoney(Decimal.Parse(itemEmpPreco.venda.ToString()), true)), baseFont14));
                    tabelaPrincipal.AddCell(tabelaEtiqueta);
                    qtd--;
                    qtdEtiquetas++;
                }
            }

            int resto;
            int qtdresto = qtdEtiquetas % 3;
            if (qtdresto > 0)
                resto = 3 - qtdresto;
            else if (qtdEtiquetas < 3)
                resto = 3 - qtdEtiquetas;
            else
                resto = qtdEtiquetas % 3;

            while (resto > 0)
            {
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tabelaPrincipal.AddCell(cell);
                resto--;
            }

            myPdfDoc.Add(tabelaPrincipal);

            myPdfDoc.Close();
        }

        #endregion

        #region BST Bijuterias
        // Modelo Etiqueta PIMACO A4 248 | Proporções: 17,00 mm x 31,00 mm

        public static void EtiquetaBST(int idCorp, int idEmp, List<MovItem> listaMovItem)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;

            ////Contrução PDF

            Font baseFont = new Font();
            baseFont.Size = 10;
            baseFont.IsBold();

            if (!Directory.Exists(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\"))
                Directory.CreateDirectory(@"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"");

            string filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\etiqueta.pdf";

            Document myPdfDoc = new Document(PageSize.A4, 19.44f, 19.44f, 35.84f, 0);

            PdfWriter myPdfWriter = PdfWriter.GetInstance(myPdfDoc, new FileStream(filePath, FileMode.Create));

            myPdfDoc.Open();
            PdfContentByte cb = myPdfWriter.DirectContent;

            PdfPTable tablePrincipal = new PdfPTable(6);
            tablePrincipal.WidthPercentage = 100;
            tablePrincipal.DefaultCell.PaddingTop = 5.76f;
            tablePrincipal.DefaultCell.PaddingBottom = 5.6f;
            tablePrincipal.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            int qtdEtiquetas = 0;

            foreach (MovItem movItem in listaMovItem)
            {
                Item item = null;
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("Item não encontrado. ID: " + movItem.idItem);
                else
                    item = query.Execute()[0] as Item;


                ItemEmpEstoque itemEmpEstoque = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpEstoque));
                query.Descend("idItem").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpEstoque. ID ITEM: " + movItem.idItem);
                else
                    foreach (ItemEmpEstoque iee in query.Execute())
                        if (iee.idEmp == idEmp)
                            itemEmpEstoque = iee;


                ItemEmpPreco itemEmpPreco = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpPreco));
                query.Descend("idItem").Constrain(movItem.idItem);
                if (query.Execute().Count == 0)
                    throw new Exception("ItemEmpPreço não encontrado. ID ITEM: " + movItem.idItem);
                else
                    foreach (ItemEmpPreco iep in query.Execute())
                        if (iep.idEmp == idEmp)
                            itemEmpPreco = iep;


                Barcode39 codigoBarras = new Barcode39();
                codigoBarras.Code = itemEmpEstoque.codBarras;
                codigoBarras.StartStopText = false;
                codigoBarras.BarHeight = 13;
                codigoBarras.N = 2;
                Image imagemCodigoBarras = codigoBarras.CreateImageWithBarcode(cb, null, null);
                
                double qtd = movItem.qtd;
                string bst = "BST";
                
                while (qtd > 0)
                {
                    PdfPTable tableEtiqueta = new PdfPTable(1);
                    tableEtiqueta.WidthPercentage = 100;
                    tableEtiqueta.DefaultCell.Padding = 0;
                    tableEtiqueta.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableEtiqueta.DefaultCell.PaddingLeft = 14.00f;

                    tableEtiqueta.AddCell(new Phrase(item.rfUnica + " - " + formatMoney(Decimal.Parse(itemEmpPreco.venda.ToString()), true), baseFont));

                    //incremento Código de Barras do Produto.
                    PdfPCell cellCodigoBarras = new PdfPCell(new Phrase(new Chunk(imagemCodigoBarras, 0, 0)));
                    cellCodigoBarras.Border = Rectangle.NO_BORDER;
                    tableEtiqueta.AddCell(cellCodigoBarras);
                    
                    //tableEtiqueta.AddCell(new Phrase(formatMoney(Decimal.Parse(itemEmpPreco.venda.ToString()), true), baseFont));

                    

                    /* // incremento código principal caso não tenha código auxiliar
                    if (item.rfAuxiliar == "")
                        item.rfAuxiliar = item.rfUnica; //"BST";
                    */
                    
                    tablePrincipal.AddCell(tableEtiqueta);
                    qtd--;
                    qtdEtiquetas++;
                }
            }

            int resto;
            if (qtdEtiquetas < 6)
                resto = 6 - qtdEtiquetas;
            else
                resto = qtdEtiquetas % 6;

            while (resto > 0)
            {
                PdfPCell cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                tablePrincipal.AddCell(cell);
                resto--;
            }

            myPdfDoc.Add(tablePrincipal);

            myPdfDoc.Close();
        }
        #endregion

        #endregion

        #region Utils

        private static string formata_cpf_cnpj(string cpf_cnpj, bool retornaTipo)
        {
            StringBuilder dado = new StringBuilder();
            string mascara = "";
            string tipo = "";

            if (cpf_cnpj.Length == 11)
            {
                mascara = "###.###.###-##";
                tipo = "CPF";
            }
            else
            {
                mascara = "##.###.###/####-##";
                tipo = "CNPJ";
            }

            foreach (char c in cpf_cnpj)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = mascara.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0; )
            {
                if (mascara[--indMascara] == '#')
                    indCampo--;
            }

            StringBuilder saida = new StringBuilder();
            for (; indMascara < mascara.Length; indMascara++)
                saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);

            if (retornaTipo)
                return tipo + ": " + saida.ToString();
            else
                return saida.ToString();
        }
        private static string formatMoney(Decimal d, Boolean mostra_cifra)
        {
            if (mostra_cifra)
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:C}", d);
            else
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:N}", d);
        }
        private static string defineTipoImpressao(EMovImpressao impressao)
        {
            string retorno = "";
            if (impressao == EMovImpressao.reserva)
                retorno = "RESERVA DE ESTOQUE";
            else if (impressao == EMovImpressao.orcamento)
                retorno = "ORÇAMENTO";
            else if (impressao == EMovImpressao.pedido)
                retorno = "DAV - DOCUMENTO AUXILIAR DE VENDA";
            else
                retorno = "VENDA";
            return retorno;
        }
        private static string defineTipoOrdemServico(string tipo)
        {
            switch (tipo)
            {
                case "S":
                    return "Serviço";
                case "P":
                    return "Produto";
                case "G":
                    return "Garantia";
                case "ST":
                    return "Serviço de Terceiros";
                case "PT":
                    return "Produto de Terceiros";
                case "PU":
                    return "Produto Utilizado";
                case "MP":
                    return "Materia Prima";
                default:
                    return "Tipo não tratado";
            }
        }

        #endregion
    }
}
