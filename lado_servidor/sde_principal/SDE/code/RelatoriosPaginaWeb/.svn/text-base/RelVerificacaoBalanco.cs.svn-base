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
using SDE.code;

namespace SDE.RelatoriosPaginaWeb
{
    public class RelVerificacaoBalanco
    {
        public StringBuilder GeraRelVerificacaoBalanco(int idCorp, int idEmp, int idBalanco, bool somenteDivergencias)
        {
            StringBuilder stringBuilderRel = new StringBuilder();
            
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

            IQuery query;


            Balanco balanco = null;
            query = db.Query();
            query.Constrain(typeof(Balanco));
            query.Descend("id").Constrain(idBalanco);
            balanco = query.Execute()[0] as Balanco;

            Dictionary<int, List<BalancoItem>> dictBalancoItem = new Dictionary<int, List<BalancoItem>>();

            try
            {
                foreach (BalancoItem bi in db.Query<BalancoItem>().OrderBy(item => item.item_nome))
                {
                    if (bi.idBalanco == balanco.id)
                    {
                        if (dictBalancoItem.ContainsKey(bi.idItem))
                        {
                            dictBalancoItem[bi.idItem].Add(bi);
                        }
                        else
                        {
                            dictBalancoItem.Add(bi.idItem, new List<BalancoItem>());
                            dictBalancoItem[bi.idItem].Add(bi);
                        }
                    }
                }

                Empresa emp = null;
                foreach (Empresa xxx in db.Query<Empresa>())
                    if (xxx.id == idEmp)
                    {
                        emp = xxx;
                        break;
                    }
                //
                Cliente cli_emp = null;
                foreach (Cliente xxx in db.Query<Cliente>())
                    if (xxx.id == emp.idCliente)
                    {
                        cli_emp = xxx;
                        break;
                    }
                //
                List<ClienteEndereco> cli_empresa_enderecos = new List<ClienteEndereco>();
                foreach (ClienteEndereco xxx in db.Query<ClienteEndereco>())
                    if (xxx.idCliente == cli_emp.id)
                        cli_empresa_enderecos.Add(xxx);
                //
                List<ClienteContato> cli_empresa_contatos = new List<ClienteContato>();
                foreach (ClienteContato xxx in db.Query<ClienteContato>())
                    if (xxx.idCliente == cli_emp.id)
                        cli_empresa_contatos.Add(xxx);
                //
                if (cli_empresa_enderecos.Count == 0)
                    cli_empresa_enderecos.Add(new ClienteEndereco());
                if (cli_empresa_contatos.Count == 0)
                    cli_empresa_contatos.Add(new ClienteContato());

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>{0}</th>", (cli_emp.apelido_razsoc == "") ? cli_emp.nome : cli_emp.apelido_razsoc);
                stringBuilderRel.AppendFormat("<th>{0}</th>", Utils.formata_cpf_cnpj(cli_emp.cpf_cnpj));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Cidade: {0}-{1}</th>", cli_empresa_enderecos[0].cidade, cli_empresa_enderecos[0].uf);
                stringBuilderRel.AppendFormat("<th>Inscrição Estadual: {0}</th>", cli_empresa_enderecos[0].inscr);
                stringBuilderRel.Append("</tr>");
                escreveContatos(stringBuilderRel, cli_empresa_contatos);
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_ass'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='fontsize: 16px;'>{0}</th>", "VERIFICAÇÃO DE BALANÇO");
                stringBuilderRel.AppendFormat("<th style='fontsize: 16px;'>Data do Fechamento do Balanço: {0}</th>", balanco.dthrFim);
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                double qtdAnteriorTotal = 0;
                double qtdLancadaTotal = 0;

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<th scoupe='colune'>Cód.</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Item</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Rf. Única</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Fr. Aux.</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Grade/Ident.</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Quantidade Anterior</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Quantidade Lancada</th>");
                stringBuilderRel.Append("<th scoupe='colune'>Saldo</th>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("<tbody>");

                for (int i = 0; i < dictBalancoItem.Count; i++)
                {
                    List<BalancoItem> itensBalanco = dictBalancoItem.ElementAt(i).Value;
                    BalancoItem balancoItem = itensBalanco.ElementAt(0);

                    Item item = null;
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(balancoItem.idItem);
                    item = query.Execute()[0] as Item;

                    double qtdAnterior = itensBalanco[0].qtdAnterior;
                    double qtdLancada = 0;

                    foreach (BalancoItem bi in itensBalanco)
                        qtdLancada += bi.qtdLancada;

                    balancoItem.qtdAnterior = qtdAnterior;
                    balancoItem.qtdLancada = qtdLancada;

                    if (somenteDivergencias)
                        if (balancoItem.qtdAnterior == balancoItem.qtdLancada)
                            continue;

                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.id);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.rfUnica);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", item.rfAuxiliar);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", balancoItem.estoque_identificador);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", qtdAnterior);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", qtdLancada);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", qtdLancada - qtdAnterior);
                    stringBuilderRel.Append("</tr>");

                    qtdAnteriorTotal += qtdAnterior;
                    qtdLancadaTotal += qtdLancada;
                }
                //tags dados anteriores
                stringBuilderRel.Append("</tbody>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Quantidade Anterior Total: {0}</tr>", qtdAnteriorTotal);
                stringBuilderRel.AppendFormat("<th>Quantidade Lançada Total: {0}</th>", qtdLancadaTotal);
                stringBuilderRel.AppendFormat("<th>Saldo Total: {0}</th>", (qtdLancadaTotal - qtdAnteriorTotal));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");
                stringBuilderRel.Append("</table>");

                return stringBuilderRel;
            }
            catch (Exception ex)
            {
               
                throw; // new Exception(ex.ToString());
            }    
        }


        private void escreveContatos(StringBuilder stringBuilderRel, IList<ClienteContato> contatos)
        {
            string cContatos = "";
            stringBuilderRel.Append("<tr>");
            for (int i = 0; i < contatos.Count; i++)
            {
                ClienteContato cc = contatos[i];
                cContatos += string.Format("{0}: {1},   ", cc.campo, cc.valor);
            }
            stringBuilderRel.AppendFormat("<th style='font-size: 12px;'>Contato(s): {0}</th>", cContatos);
            stringBuilderRel.Append("</tr>");
        }
    }
}
