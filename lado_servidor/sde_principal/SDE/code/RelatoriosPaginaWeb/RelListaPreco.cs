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
    public class RelListaPreco
    {
        public StringBuilder GeraRelListaPreco(int idCorp, int idEmp, int idMarca)
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

            query = db.Query();
            query.Constrain(typeof(Item));
            if (idMarca > 0)
                query.Descend("idMarca").Constrain(idMarca);
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

            try
            {
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos);

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
                    stringBuilderRel.Append("<th scoupe='colune' style='width:55%;'>Item</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Rf. Un.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Rf. Aux.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Unid. Med.</th>");
                    stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Preço Venda</th>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("<tbody>");

                    foreach (Item item in listaItens)
                    {
                        if (item.desuso)
                            continue;

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
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(itemEmpPreco_db.venda), true));
                        stringBuilderRel.Append("</tr>");
                    }
                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");
                }

                return stringBuilderRel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void GeraCabecalho(StringBuilder stringBuilderRel, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos)
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "LISTA DE PREÇO");
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

        internal StringBuilder GeraRelListaPreco(int idCorp, int idEmp)
        {
            throw new NotImplementedException();
        }
    }
}
