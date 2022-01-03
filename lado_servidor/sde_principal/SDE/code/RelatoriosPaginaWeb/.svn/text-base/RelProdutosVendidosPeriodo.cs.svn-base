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
using System.Collections.Generic;
using SDE.Enumerador;

namespace SDE.RelatoriosPaginaWeb
{
    public class RelProdutosVendidosPeriodo
    {
        public StringBuilder GeraRelProdutosVendidosPeriodo(int idCorp, int idEmp, int idItem, String dtInicial, String dtFinal)
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

                IList<Mov> rs_mov = db.Query<Mov>(
                    delegate(Mov mov)
                    {
                        return (Utils.StringToDateTime(mov.dthrMovEmissao) >= Utils.StringToDateTime(dtInicial) &&
                            Utils.StringToDateTime(mov.dthrMovEmissao) <= Utils.StringToDateTime(dtFinal) &&
                            (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido || mov.tipo == EMovTipo.nfs_prefeitura));
                    }
                );

                Dictionary<int, string[]> dicionarioItensPorItemEmpEstoque = new Dictionary<int, string[]>();//0:Id 1:Nome 2:Rf.U 3:Rf.A 4:Grade 5:Quatidade 6:Valor
                foreach (Mov mov in rs_mov)
                {
                    query = db.Query();
                    query.Constrain(typeof(MovItem));
                    query.Descend("idMov").Constrain(mov.id);
                    IObjectSet rs_movItem = query.Execute();

                    double quantidadeUnit;
                    double valorUnit;
                    foreach (MovItem movItem in rs_movItem)
                    {
                        if (idItem != 0 && idItem != movItem.idItem)
                            continue;

                        Item item = AppFacade.get.reaproveitamento.Item_Load(db, movItem.idItem);

                        if (!dicionarioItensPorItemEmpEstoque.ContainsKey(movItem.idIEE))
                            dicionarioItensPorItemEmpEstoque.Add(movItem.idIEE, new string[] { item.id.ToString(), item.nome, item.rfUnica, item.rfAuxiliar, movItem.estoque_identificador, "0", "0" });

                        quantidadeUnit = Convert.ToDouble(dicionarioItensPorItemEmpEstoque[movItem.idIEE][5]);
                        valorUnit = Convert.ToDouble(dicionarioItensPorItemEmpEstoque[movItem.idIEE][6]);

                        dicionarioItensPorItemEmpEstoque[movItem.idIEE][5] = (quantidadeUnit + movItem.qtd).ToString();
                        dicionarioItensPorItemEmpEstoque[movItem.idIEE][6] = (valorUnit + movItem.vlrUnitVendaFinalQtd).ToString();
                    }
                }
                List<string[]> listaOrdenadaPorItemNome = OrdenaLista(dicionarioItensPorItemEmpEstoque);

                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                if (listaOrdenadaPorItemNome.Count > 0)
                {
                    double totalQuantidade = 0;
                    double totalValor = 0;

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("<th scope='col' style='width:5%;'>Cód.</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:40%;'>Item</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Cód. Única</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Cód. Aux.</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Grade/Ident.</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:7%;'>Quantidade</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Vlr. Total</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("<tbody>");


                    foreach (string[] valor in listaOrdenadaPorItemNome.OrderBy(item => item[1]))
                    {
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[0]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[1]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[2]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[3]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[4]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", valor[5]);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(valor[6]), true));
                        stringBuilderRel.Append("</tr>");

                        totalQuantidade += Convert.ToDouble(valor[5]);
                        totalValor += Convert.ToDouble(valor[6]);
                    }
                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:50%; background-color:#EEDD82;'>QUANTIDADE VENDIDA: {0}</th>", totalQuantidade);
                    stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:50%; background-color:#EEDD82;'>VALOR TOTAL: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalValor), true));
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "PRODUTOS VENDIDOS NO PERÍODO");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
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

        private List<string[]> OrdenaLista(Dictionary<int, string[]> dicionarioItensPorItemEmpEstoque)
        {
            List<string[]> listaRetorno = new List<string[]>();
            foreach (int chave in dicionarioItensPorItemEmpEstoque.Keys)
                listaRetorno.Add(dicionarioItensPorItemEmpEstoque[chave]);
            return listaRetorno;
        }

        private int GetIndexMovItem(List<MovItem> listaMovItem, int idItemEmpEstoque)
        {
            foreach (MovItem movItem in listaMovItem)
            {
                if (movItem.idIEE != idItemEmpEstoque)
                    continue;
                return listaMovItem.IndexOf(movItem);
            }
            return -1;
        }
    }
}
