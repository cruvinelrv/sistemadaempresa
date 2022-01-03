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
    public class RelExtratoContaCorerenteCaixa
    {
        public StringBuilder GeraRelExtratoContaCorrenteCaixa(int idCorp, int idEmp,
            int idConta, int idCentroCusto, int idPlanoConta, String dtInicial, String dtFinal)
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

            IList<Finan_Lancamento> rs_finanLancamento = db.Query<Finan_Lancamento>(
                delegate(Finan_Lancamento finanLancamento)
                {
                    return (Utils.StringToDateTime(finanLancamento.dtLancamento) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanLancamento.dtLancamento) <= Utils.StringToDateTime(dtFinal));
                }
                );
            List<Finan_Lancamento> listaFinanLancamento = new List<Finan_Lancamento>(rs_finanLancamento);

            Dictionary<Finan_Conta, List<Finan_Lancamento>> dicionarioLancamentosPorConta = new Dictionary<Finan_Conta, List<Finan_Lancamento>>();

            foreach (Finan_Lancamento finanLancamento in listaFinanLancamento.OrderBy(item => item.id))
            {
                if (finanLancamento.dtLancamento != null && Utils.StringToDateTime(finanLancamento.dtLancamento) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanLancamento.dtLancamento) <= Utils.StringToDateTime(dtFinal))
                {
                    if (idConta != 0 && idConta != finanLancamento.idContaDestino)
                        continue;
                    if (idCentroCusto != 0 && idCentroCusto != finanLancamento.idCentroCusto)
                        continue;
                    if (idPlanoConta != 0 && idPlanoConta != finanLancamento.idTipoLancamento)
                        continue;

                    query = db.Query();
                    query.Constrain(typeof(Finan_Conta));
                    query.Descend("id").Constrain(finanLancamento.idContaDestino);
                    IObjectSet rs_finanConta = query.Execute();
                    if (rs_finanConta.Count == 0)
                        continue;
                    Finan_Conta finanConta_db = rs_finanConta[0] as Finan_Conta;

                    if (!dicionarioLancamentosPorConta.ContainsKey(finanConta_db))
                        dicionarioLancamentosPorConta.Add(finanConta_db, new List<Finan_Lancamento>());
                    dicionarioLancamentosPorConta[finanConta_db].Add(finanLancamento);
                }
            }

            try
            {
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                for (int i = 0; i < dicionarioLancamentosPorConta.Count; i++)
                {
                    Finan_Conta finanConta = dicionarioLancamentosPorConta.ElementAt(i).Key;
                    List<Finan_Lancamento> listaFinanLancamentoPorConta = dicionarioLancamentosPorConta.ElementAt(i).Value;

                    Dictionary<Finan_CentroCusto, List<Finan_Lancamento>> dicionarioLancamentosPorCentroCusto= new Dictionary<Finan_CentroCusto, List<Finan_Lancamento>>();

                    foreach (Finan_Lancamento finanLancamento in listaFinanLancamentoPorConta)
                    {
                        query = db.Query();
                        query.Constrain(typeof(Finan_CentroCusto));
                        query.Descend("id").Constrain(finanLancamento.idCentroCusto);
                        IObjectSet rs_finanCentroCusto = query.Execute();
                        if (rs_finanCentroCusto.Count == 0)
                            continue;
                        Finan_CentroCusto finanCentroCusto_db = rs_finanCentroCusto[0] as Finan_CentroCusto;

                        if (!dicionarioLancamentosPorCentroCusto.ContainsKey(finanCentroCusto_db))
                            dicionarioLancamentosPorCentroCusto.Add(finanCentroCusto_db, new List<Finan_Lancamento>());
                        dicionarioLancamentosPorCentroCusto[finanCentroCusto_db].Add(finanLancamento);
                    }

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr style='color:white; background-color:black;'>");
                    stringBuilderRel.AppendFormat("<th style='font-size:14px; width:50%;'>Conta: {0}</th>", finanConta.nome);
                    stringBuilderRel.AppendFormat("<th style='font-size:14px; width:50%;'>Tipo: {0}</th>", finanConta.tipo);
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");
                    stringBuilderRel.Append("</table>");

                    for (int j = 0; j < dicionarioLancamentosPorCentroCusto.Count; j++)
                    {
                        Finan_CentroCusto finanCentroCusto = dicionarioLancamentosPorCentroCusto.ElementAt(j).Key;
                        List<Finan_Lancamento> listaFinanLancamentoPorCentroCusto = dicionarioLancamentosPorCentroCusto.ElementAt(j).Value;

                        stringBuilderRel.Append("<table class='tabela_mov'>");
                        stringBuilderRel.Append("<thead>");
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<th style='border-bottom: 2px solid; font-size:14px; color:white; background-color:gray;'>Centro de Custo: {0}-{1}</th>", finanCentroCusto.id, finanCentroCusto.nome);
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("</thead>");
                        stringBuilderRel.Append("</table>");

                        stringBuilderRel.Append("<table class='tabela_mov'>");
                        stringBuilderRel.Append("<thead>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:5%;'>Conta</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:9%;'>Documento</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Data Lancmt.</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:15%;'>Tipo</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:25%;'>Histórico</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:12%;'>Debito</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:12%;'>Credito</th>");
                        stringBuilderRel.Append("<th scoupe='colune' style='width:12%;'>Saldo</th>");
                        stringBuilderRel.Append("</thead>");
                        stringBuilderRel.Append("<tbody>");

                        Double saldoPorCentroCusto = listaFinanLancamentoPorCentroCusto.ElementAt(0).saldoAnterior;

                        foreach (Finan_Lancamento finanLancamento in listaFinanLancamentoPorCentroCusto)
                        {
                            query = db.Query();
                            query.Constrain(typeof(Finan_TipoLancamento));
                            query.Descend("id").Constrain(finanLancamento.idTipoLancamento);
                            IObjectSet rs_tipoLancamento = query.Execute();
                            if (rs_tipoLancamento.Count == 0)
                                continue;
                            Finan_TipoLancamento finanTipoLancamento_db = rs_tipoLancamento[0] as Finan_TipoLancamento;

                            saldoPorCentroCusto = (finanLancamento.isCredito) ? saldoPorCentroCusto + ((finanLancamento.valorLancado < 0) ? finanLancamento.valorLancado * -1 : finanLancamento.valorLancado) : saldoPorCentroCusto - ((finanLancamento.valorLancado < 0) ? finanLancamento.valorLancado * -1 : finanLancamento.valorLancado);

                            

                            stringBuilderRel.Append("<tr>");
                            stringBuilderRel.AppendFormat("<td>{0}</td>", finanTipoLancamento_db.codigo);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", finanLancamento.nome);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", finanLancamento.dtLancamento);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", finanTipoLancamento_db.nomeTipoLancamento);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", finanLancamento.historico);
                            if (finanLancamento.isCredito)
                            {
                                stringBuilderRel.AppendFormat("<td>{0}</td>", "-");
                                stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanLancamento.valorLancado), true));
                            }
                            else
                            {
                                stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanLancamento.valorLancado), true));
                                stringBuilderRel.AppendFormat("<td>{0}</td>", "-");
                            }
                            stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(saldoPorCentroCusto), true));
                            stringBuilderRel.Append("</tr>");
                        }
                    }
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "EXTRATO CONTA CORRENTE CAIXA");
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
    }
}
