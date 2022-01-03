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
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.Entidade;
using SDE.Enumerador;
using System.Collections.Generic;

namespace SDE.RelatoriosPaginaWeb
{
    public class RelTitulosReceberPagar
    {
        Double totalTitulosReceber = 0;
        Double totalTitulosRecebido = 0;
        Double totalTitulosPagar = 0;
        Double totalTitulosPago = 0;

        public StringBuilder GeraRelTitulosReceberPagar(int idCorp, int idEmp, String dtInicial, String dtFinal,
            int idCliente, int idPortador, bool titulo_a_pagar, bool titulo_a_receber, bool em_aberto, bool lancado)
        {
            StringBuilder stringBuilderRel = new StringBuilder();

            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            try
            {
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
                query.Constrain(typeof(Finan_Titulo));
                IObjectSet rs_finanTitulo = query.Execute();

                List<ETipoTitulo> listaTipoTituloFiltro = ConstroiListTipoTituloFiltro(titulo_a_pagar, titulo_a_receber);
                List<ETituloSituacao> listaSituacaoTituloFiltro = ConstroiListSituacaoTituloFiltro(em_aberto, lancado);

                Dictionary<Cliente, List<Finan_TituloItem>> dicionarioFinanTituloPorTituloReceber = new Dictionary<Cliente, List<Finan_TituloItem>>();
                Dictionary<Cliente, List<Finan_TituloItem>> dicionarioFinanTituloPorTituloPagar = new Dictionary<Cliente, List<Finan_TituloItem>>();

                foreach (Finan_Titulo finanTitulo in rs_finanTitulo)
                {
                    if (idCliente != 0 && (idCliente != finanTitulo.idClienteAPagar && idCliente != finanTitulo.idClienteAReceber))
                        continue;
                    if (finanTitulo.idClienteAPagar == 1 || finanTitulo.idClienteAReceber == 1)
                        continue;
                    if (!listaTipoTituloFiltro.Contains(finanTitulo.tipo))
                        continue;

                    query = db.Query();
                    query.Constrain(typeof(Finan_TituloItem));
                    query.Descend("idTitulo").Constrain(finanTitulo.id);
                    IObjectSet rs_finanTituloItem = query.Execute();
                    foreach (Finan_TituloItem finanTituloItem in rs_finanTituloItem)
                    {
                        if (!(Utils.StringToDateTime(finanTituloItem.dtPagamento) >= Utils.StringToDateTime(dtInicial)) || !(Utils.StringToDateTime(finanTituloItem.dtPagamento) <= Utils.StringToDateTime(dtFinal)))
                            continue;
                        if (!listaSituacaoTituloFiltro.Contains(finanTituloItem.situacao))
                            continue;

                        if (idPortador != 0)
                        {
                            query = db.Query();
                            query.Constrain(typeof(Finan_TipoPagamento));
                            query.Descend("id").Constrain(finanTituloItem.idTipoPagamento);
                            IObjectSet rs_finanTipoPagamento = query.Execute();
                            if (rs_clienteEmpresa.Count == 0)
                                continue;
                            Finan_TipoPagamento finanTipoPagamento_db = rs_finanTipoPagamento[0] as Finan_TipoPagamento;
                            if (idPortador != finanTipoPagamento_db.idPortador)
                                continue;
                        }

                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        IObjectSet rs_cliente;
                        Cliente cliente_db;

                        if (finanTitulo.tipo == ETipoTitulo.titulo_a_receber)
                        {
                            query.Descend("id").Constrain(finanTitulo.idClienteAPagar);
                            rs_cliente = query.Execute();
                            cliente_db = rs_cliente[0] as Cliente;
                            if (!dicionarioFinanTituloPorTituloReceber.ContainsKey(cliente_db))
                                dicionarioFinanTituloPorTituloReceber.Add(cliente_db, new List<Finan_TituloItem>());
                            dicionarioFinanTituloPorTituloReceber[cliente_db].Add(finanTituloItem);
                        }
                        if (finanTitulo.tipo == ETipoTitulo.titulo_a_pagar)
                        {
                            query.Descend("id").Constrain(finanTitulo.idClienteAReceber);
                            rs_cliente = query.Execute();
                            cliente_db = rs_cliente[0] as Cliente;
                            if (!dicionarioFinanTituloPorTituloPagar.ContainsKey(cliente_db))
                                dicionarioFinanTituloPorTituloPagar.Add(cliente_db, new List<Finan_TituloItem>());
                            dicionarioFinanTituloPorTituloPagar[cliente_db].Add(finanTituloItem);
                        }
                    }
                }
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                if (dicionarioFinanTituloPorTituloReceber.Keys.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th style='color:white; background-color:black;'>TITULOS A RECEBER</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    foreach (Cliente cliente in dicionarioFinanTituloPorTituloReceber.Keys.OrderBy(key => key.nome))
                    {
                        GeraTableHeader(stringBuilderRel, cliente, ETipoTitulo.titulo_a_receber);
                        GeraTableBody(stringBuilderRel, dicionarioFinanTituloPorTituloReceber, cliente, ETipoTitulo.titulo_a_receber);
                    }

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL A RECEBER: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalTitulosReceber), true));
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:67%; background-color:#EEDD82;'>TOTAL GERAL RECEBIDO: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalTitulosRecebido), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
                }
                else
                    if (titulo_a_receber)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.Append("<th style='color:white; background-color:black;'>NÃO EXISTEM TITULOS A RECEBER NO PERÍODO</th>");
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("</table>");
                    }

                if (dicionarioFinanTituloPorTituloPagar.Keys.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th style='color:white; background-color:black;'>TITULOS A PAGAR</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    foreach (Cliente cliente in dicionarioFinanTituloPorTituloPagar.Keys.OrderBy(key => key.nome))
                    {
                        GeraTableHeader(stringBuilderRel, cliente, ETipoTitulo.titulo_a_pagar);
                        GeraTableBody(stringBuilderRel, dicionarioFinanTituloPorTituloPagar, cliente, ETipoTitulo.titulo_a_pagar);
                    }

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL A PAGAR: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalTitulosPagar), true));
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:67%; background-color:#EEDD82;'>TOTAL GERAL PAGO: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalTitulosPago), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
                }
                else
                    if (titulo_a_pagar)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.Append("<th style='color:white; background-color:black;'>NÃO EXISTEM TITULOS A PAGAR NO PERÍODO</th>");
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("</table>");
                    }

                return stringBuilderRel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void GeraCabecalho(StringBuilder stringBuilderRel, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos, String dtInicial, String dtFinal)
        {
            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead>");

            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th>{0}</th>", (clienteEmpresa_db.apelido_razsoc.Trim() == String.Empty) ? clienteEmpresa_db.nome : clienteEmpresa_db.apelido_razsoc);
            stringBuilderRel.AppendFormat("<th>{0}</th>", Utils.formata_cpf_cnpj(clienteEmpresa_db.cpf_cnpj));
            stringBuilderRel.Append("</tr>");

            if (rs_clienteEmpresaEnderecos.Count > 0)
            {
                ClienteEndereco clienteEndereco = rs_clienteEmpresaEnderecos[0] as ClienteEndereco;
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Endereço:{0}, {1}-{2} CEP:{3} I.E.:{4}</th>", clienteEndereco.logradouro, clienteEndereco.cidade, clienteEndereco.uf, clienteEndereco.cep, clienteEndereco.inscr);
                stringBuilderRel.Append("<tr>");
            }
            else
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th>Endereço: -</th>");
                stringBuilderRel.Append("</tr>");
            }

            if (rs_clienteEmpresaContatos.Count > 0)
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Contato(s): {0}</th>", GeraStringContatos(rs_clienteEmpresaContatos));
                stringBuilderRel.Append("</tr>");
            }
            else
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th>Contato(s): -</th>");
                stringBuilderRel.Append("</tr>");
            }

            stringBuilderRel.Append("</thead>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_ass'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "TÍTULOS A PAGAR / RECEBER");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");
        }

        private static void GeraTableHeader(StringBuilder stringBuilderRel, Cliente cliente, ETipoTitulo eTipoTitulo)
        {
            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='color:white; background-color:gray;'>Cliente: {0}</th>", cliente.nome);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_mov'>");
            stringBuilderRel.Append("<thead>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:12%;'>Nº do Documento</th>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:12%;'>Vencimento</th>");
            stringBuilderRel.AppendFormat("<th scoupe='colune' style='width:12%;'>{0}</th>", (eTipoTitulo == ETipoTitulo.titulo_a_receber) ? "Valor a Receber" : "Valor a Pagar");
            stringBuilderRel.Append("<th scoupe='colune' style='width:54%;'>Observações</th>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Situação</th>");
            stringBuilderRel.Append("</thead>");
            stringBuilderRel.Append("<tbody>");
        }

        private void GeraTableBody(StringBuilder stringBuilderRel, Dictionary<Cliente, List<Finan_TituloItem>> dicionarioFinanTitulo, Cliente cliente, ETipoTitulo eTipoTitulo)
        {
            Double totalTitulosPorCliente = 0;
            Double totalTitulosEmAbertoPorCliente = 0;
            Double totalTitulosLancadoPorCliente = 0;

            foreach (Finan_TituloItem finanTituloItem in dicionarioFinanTitulo[cliente].OrderBy(value => Utils.StringToDateTime(value.dtPagamento)))
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<td>{0}</td>", finanTituloItem.identificador);
                stringBuilderRel.AppendFormat("<td>{0}</td>", finanTituloItem.dtPagamento);
                stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTituloItem.valorCobrado), true));
                stringBuilderRel.AppendFormat("<td>{0}</td>", finanTituloItem.obs);
                stringBuilderRel.AppendFormat("<td>{0}</td>", StringSituacaoTitulo(finanTituloItem.situacao, eTipoTitulo));
                stringBuilderRel.Append("</tr>");

                totalTitulosPorCliente += finanTituloItem.valorCobrado;
                if (finanTituloItem.situacao == ETituloSituacao.em_aberto) totalTitulosEmAbertoPorCliente += finanTituloItem.valorCobrado;
                if (finanTituloItem.situacao == ETituloSituacao.lancado) totalTitulosLancadoPorCliente += finanTituloItem.valorCobrado;
            }
            stringBuilderRel.Append("</tbody>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#87CEEB;'>{0}: {1}</th>", (eTipoTitulo == ETipoTitulo.titulo_a_receber) ? "TOTAL A RECEBER" : "TOTAL A PAGAR", Utils.formatMoney(Convert.ToDecimal(totalTitulosEmAbertoPorCliente), true));
            stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#87CEEB;'>{0}: {1}</th>", (eTipoTitulo == ETipoTitulo.titulo_a_receber) ? "TOTAL RECEBIDO" : "TOTAL PAGO", Utils.formatMoney(Convert.ToDecimal(totalTitulosLancadoPorCliente), true));
            stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#87CEEB;'>TOTAL: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalTitulosPorCliente), true));
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");

            if (eTipoTitulo == ETipoTitulo.titulo_a_receber)
            {
                totalTitulosReceber += totalTitulosEmAbertoPorCliente;
                totalTitulosRecebido += totalTitulosLancadoPorCliente;
            }
            else
            {
                totalTitulosPagar += totalTitulosEmAbertoPorCliente;
                totalTitulosPago += totalTitulosLancadoPorCliente;
            }
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

        private List<ETipoTitulo> ConstroiListTipoTituloFiltro(bool titulo_a_pagar, bool titulo_a_receber)
        {
            List<ETipoTitulo> listaRetorno = new List<ETipoTitulo>();

            if (titulo_a_pagar == true) listaRetorno.Add(ETipoTitulo.titulo_a_pagar);
            if (titulo_a_receber == true) listaRetorno.Add(ETipoTitulo.titulo_a_receber);

            return listaRetorno;
        }

        private List<ETituloSituacao> ConstroiListSituacaoTituloFiltro(bool em_aberto, bool lancado)
        {
            List<ETituloSituacao> listaRetorno = new List<ETituloSituacao>();

            if (em_aberto == true) listaRetorno.Add(ETituloSituacao.em_aberto);
            if (lancado == true) listaRetorno.Add(ETituloSituacao.lancado);

            return listaRetorno;
        }

        private String StringSituacaoTitulo(ETituloSituacao eTituloSituacao, ETipoTitulo eTipoTitulo)
        {
            String strRetorno = "";

            if (eTituloSituacao == ETituloSituacao.em_aberto) strRetorno = "Em Aberto";
            if (eTituloSituacao == ETituloSituacao.lancado) strRetorno = (eTipoTitulo == ETipoTitulo.titulo_a_pagar) ? "Pago" : "Recebido";

            return strRetorno;
        }
    }
}
