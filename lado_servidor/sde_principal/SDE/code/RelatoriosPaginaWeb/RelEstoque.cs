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

namespace SDE.RelatoriosPaginaWeb
{
    public class RelEstoque
    {
        public StringBuilder GeraRelEstoque(int idCorp, int idEmp, Boolean exibeGrade, Boolean exibeEstoqueZerado)
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
                query.Constrain(typeof(Item));
                query.Descend("grupo").OrderAscending();
                query.Descend("nome").OrderAscending();
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

                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, exibeGrade);
                if (exibeGrade)
                    GeraCorpoRelComGrade(idEmp, stringBuilderRel, db, dicionarioItemProGrupo, exibeEstoqueZerado);
                else
                    GeraCorpoRelSemGrade(idEmp, stringBuilderRel, db, dicionarioItemProGrupo, exibeEstoqueZerado);

                return stringBuilderRel;
            }
            catch (Exception ex)
            {
                stringBuilderRel = new StringBuilder(string.Format("<h2>{0}\n{1}</h2>", ex.Message, ex.StackTrace));
                return stringBuilderRel;
                //return null;
            }
        }

        private void GeraCorpoRelSemGrade(int idEmp, StringBuilder stringBuilderRel, IObjectContainer db, Dictionary<String, List<Item>> dicionarioItemProGrupo, Boolean exibeEstoqueZerado)
        {
            IQuery query;
            Double quantidadeTotal = 0;

            for (int i = 0; i < dicionarioItemProGrupo.Count; i++)
            {
                String grupo = dicionarioItemProGrupo.ElementAt(i).Key;
                List<Item> listaItens = dicionarioItemProGrupo.ElementAt(i).Value;

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<td style='color:white; background-color:black;'>Grupo: {0}</td>", grupo);
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:5%;'>Cód.</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:44%;'>Item</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:13%;'>Rf. Única</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:13%;'>Rf. Aux.</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Unid. Med.</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:5%;'>Qtd</th>");
                stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Preço Venda</th>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("<tbody>");

                Double quantidadeTotalGrupo = 0;
                foreach (Item item in listaItens)
                {
                    if (item.desuso)
                        continue;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpEstoque));
                    query.Descend("idEmp").Constrain(idEmp);
                    query.Descend("idItem").Constrain(item.id);
                    IObjectSet rs_itemEmpEstoques = query.Execute();

                    Double quantidadeItem = 0;
                    foreach (ItemEmpEstoque itemEmpEstoque in rs_itemEmpEstoques)
                    {
                        quantidadeItem += itemEmpEstoque.qtd;
                    }
                    if (!exibeEstoqueZerado)
                    {
                        if (quantidadeItem <= 0)
                            continue;
                    }

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpPreco));
                    query.Descend("idEmp").Constrain(idEmp);
                    query.Descend("idItem").Constrain(item.id);
                    IObjectSet rs_itemEmpPreco = query.Execute();
                    ItemEmpPreco itemEmpPreco_db = rs_itemEmpPreco[0] as ItemEmpPreco;
                    
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.id);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.rfUnica);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.rfAuxiliar);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.unidMed);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", quantidadeItem);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(itemEmpPreco_db.venda), true));
                    stringBuilderRel.Append("</tr>");

                    quantidadeTotalGrupo += quantidadeItem;
                }
                stringBuilderRel.Append("</tbody>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<td style='background-color:#87CEEB;'>Qtd Estoque do Grupo '{0}': {1}</td>", grupo, quantidadeTotalGrupo);
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                quantidadeTotal += quantidadeTotalGrupo;
            }

            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<td style='background-color:#EEDD82;'>Total de itens em estoque: {0}</td>", quantidadeTotal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</thead>");
            stringBuilderRel.Append("</table>");
        }

        private void GeraCorpoRelComGrade(int idEmp, StringBuilder stringBuilderRel, IObjectContainer db, Dictionary<String, List<Item>> dicionarioItemProGrupo, Boolean exibeEstoqueZerado)
        {
            IQuery query;

            for (int i = 0; i < dicionarioItemProGrupo.Count; i++)
            {
                String grupo = dicionarioItemProGrupo.ElementAt(i).Key;
                List<Item> listaItens = dicionarioItemProGrupo.ElementAt(i).Value;

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<td style='color:white; background-color:black;'>Grupo: {0}</td>", grupo);
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                foreach (Item item in listaItens)
                {
                    if (item.desuso)
                        continue;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpEstoque));
                    query.Descend("idEmp").Constrain(idEmp);
                    query.Descend("idItem").Constrain(item.id);
                    IObjectSet rs_itemEmpEstoques = query.Execute();

                    Double quantidadeItem = 0;
                    foreach (ItemEmpEstoque itemEmpEstoque in rs_itemEmpEstoques)
                    {
                        quantidadeItem += itemEmpEstoque.qtd;
                    }
                    if (!exibeEstoqueZerado)
                    {
                        if (quantidadeItem <= 0)
                            continue;
                    }

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='background-color:gray; width:50%;'>Item: {0}</th>", item.nome);
                    stringBuilderRel.AppendFormat("<th style='background-color:gray; width:20%;'>Cód. Único: {0}</th>", item.rfUnica);
                    stringBuilderRel.AppendFormat("<th style='background-color:gray; width:20%;'>Cód. Auxiliar: {0}</th>", item.rfAuxiliar);
                    stringBuilderRel.AppendFormat("<th style='background-color:gray; width:10%;'>Total Item: {0}</th>", quantidadeItem);
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_iee'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:90%;'>Grade/Identificador</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Quantidade</th>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("<tbody>");

                    foreach (ItemEmpEstoque itemEmpEstoque in rs_itemEmpEstoques)
                    {
                        if (!exibeEstoqueZerado)
                        {
                            if (itemEmpEstoque.qtd == 0)
                                continue;
                        }

                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", itemEmpEstoque.identificador);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", itemEmpEstoque.qtd);
                        stringBuilderRel.Append("</tr>");
                    }
                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");
                }
            }
        }

        private void GeraCabecalho(StringBuilder stringBuilderRel, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos, Boolean exibeGrade)
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", (exibeGrade) ? "RELATÓRIO DE ESTOQUE COM GRADE" : "RELATÓRIO DE ESTOQUE SEM GRADE");
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
    }
}
