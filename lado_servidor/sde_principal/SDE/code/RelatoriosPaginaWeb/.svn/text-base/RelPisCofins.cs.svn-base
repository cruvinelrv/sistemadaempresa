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
    public class RelPisCofins
    {
        public StringBuilder GeraRelPisCofins(int idCorp, int idEmp, String dtInicial, String dtFinal)
        {
            StringBuilder stringBuilderRel = new StringBuilder();

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

            IList<Mov> rs_mov = db.Query<Mov>(
                delegate(Mov mov)
                {
                    return (Utils.StringToDateTime(mov.dthrMovEmissao) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(mov.dthrMovEmissao) <= Utils.StringToDateTime(dtFinal) &&
                        (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido || mov.tipo == EMovTipo.nfs_prefeitura));
                }
                );
            IList<Mov> listaMov = new List<Mov>(rs_mov);

            Dictionary<String, List<MovItem>> dicionarioItemPorGrupo = new Dictionary<string, List<MovItem>>();
            dicionarioItemPorGrupo.Add("INDEFINIDO", new List<MovItem>());

            foreach (Mov mov in listaMov.OrderBy(item => item.id).Reverse())
            {
                query = db.Query();
                query.Constrain(typeof(MovItem));
                query.Descend("idMov").Constrain(mov.id);
                query.Descend("item_nome").OrderAscending();
                IObjectSet rs_movItem = query.Execute();

                foreach (MovItem movItem in rs_movItem)
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(movItem.idItem);
                    IObjectSet rs_item = query.Execute();
                    Item item = rs_item[0] as Item;

                    if (item.grupo == null || item.grupo.Trim() == String.Empty)
                        dicionarioItemPorGrupo["INDEFINIDO"].Add(movItem);
                    else
                    {
                        if (!dicionarioItemPorGrupo.ContainsKey(item.grupo))
                            dicionarioItemPorGrupo.Add(item.grupo, new List<MovItem>());
                        dicionarioItemPorGrupo[item.grupo].Add(movItem);
                    }
                }
            }
            if (dicionarioItemPorGrupo["INDEFINIDO"].Count == 0)
                dicionarioItemPorGrupo.Remove("INDEFINIDO");

            try
            {
                Double totalGeral = 0;
                Double totalCOFINS = 0;
                Double totalPIS = 0;

                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                for (int i = 0; i < dicionarioItemPorGrupo.Count; i++)
                {
                    String grupo = dicionarioItemPorGrupo.ElementAt(i).Key;
                    List<MovItem> listaMovItem = dicionarioItemPorGrupo.ElementAt(i).Value;

                    Double totalGrupo = 0;
                    Double totalGrupoCOFINS = 0;
                    Double totalGrupoPIS = 0;

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td style='background-color:black; color:white;'>Grupo: {0}</td>", grupo);
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<th scoupe='colune' style='font-size:10px;'>Cód.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='font-size:10px;'>Item</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>UN</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>CSTP</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>CSTC</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>PIS</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>COFINS</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>Vlr. Un.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>Qtd. V.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='text-align:center;'>Vlr. Total</th>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("<tbody>");

                    foreach (MovItem movItem in listaMovItem)
                    {
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td style='font-size:10px;'>{0}</td>", movItem.idItem);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; width: 280px;'>{0}</td>", movItem.item_nome);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", movItem.unid_med);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", movItem.pisCst);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", movItem.cofinsCst);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}% / {1}</td>", movItem.pisAliq, Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * movItem.pisAliq) / 100), false));
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}% / {1}</td>", movItem.cofinsAliq, Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * movItem.cofinsAliq) / 100), false));
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinal), false));
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", movItem.qtd);
                        stringBuilderRel.AppendFormat("<td style='font-size:10px; text-align:center;'>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), false));
                        stringBuilderRel.Append("</tr>");

                        totalGrupo += movItem.vlrUnitVendaFinalQtd;
                        totalGrupoCOFINS += (movItem.vlrUnitVendaFinalQtd * movItem.cofinsAliq) / 100;
                        totalGrupoPIS += (movItem.vlrUnitVendaFinalQtd * movItem.pisAliq) / 100;
                    }

                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");

                    totalGeral += totalGrupo;
                    totalCOFINS += totalGrupoCOFINS;
                    totalPIS += totalGrupoPIS;

                    stringBuilderRel.Append("<table class='tabela_ass'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td style='background-color:#87CEEB;'>Sub-Total Vendas: {0}</td>", Utils.formatMoney(Convert.ToDecimal(totalGrupo), true));
                    stringBuilderRel.AppendFormat("<td style='background-color:#87CEEB;'>Sub-Total PIS: {0}</td>", Utils.formatMoney(Convert.ToDecimal(totalGrupoPIS), true));
                    stringBuilderRel.AppendFormat("<td style='background-color:#87CEEB;'>Sub-Total COFINS: {0}</td>", Utils.formatMoney(Convert.ToDecimal(totalGrupoCOFINS), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("</table>");
                }

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='background-color:#EEDD82;'>Total Vendas: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalGeral), true));
                stringBuilderRel.AppendFormat("<th style='background-color:#EEDD82;'>Total PIS: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalPIS), true));
                stringBuilderRel.AppendFormat("<th style='background-color:#EEDD82;'>Total COFINS: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalCOFINS), true));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                GeraLegenda(stringBuilderRel);

                return stringBuilderRel;
            }
            catch (Exception)
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "RELATÓRIO PIS/COFINS");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");
        }

        private void GeraLegenda(StringBuilder stringBuilderRel)
        {
            stringBuilderRel.Append("<table class='tabela_mov'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr><th style='border-bottom: 2px solid; font-size:14px;'>Legenda CSTP(CST-PIS) / CSTC(CST-COFINS)</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>01: Operação Tributável - Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo/Não Cumulativo)</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>02: Operação Tributável - Base de Calculo = Valor da Operação (Alíquota Diferenciada)</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>03: Operação Tributável - Base de Calculo = Quantidade Vendida x Alíquota por Unidade de Produto</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>04: Operação Tributável - Tributação Monofásica - (Alíquota Zero)</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>05: Operação Tributável (substituição tributária)</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>06: Operação Tributável – Alíquota zero</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>07: Operação Isenta da contribuição</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>08: Operação Sem Incidência da contribuição</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>09: Operação com suspensão da contribuição</th></tr>");
            stringBuilderRel.Append("<tr><th style='font-size:11px;'>99: Outras Operações</th></tr>");
            stringBuilderRel.Append("</table>");
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
