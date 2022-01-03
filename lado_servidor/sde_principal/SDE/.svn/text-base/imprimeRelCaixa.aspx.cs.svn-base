using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using System.Globalization;
using System.Reflection;

namespace SDE
{
    public partial class imprimeRelCaixa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            body.Attributes["class"] = "carregando";
            Timer1.Interval = 500;
            Timer1.Enabled = true;

            Page.EnableViewState = false;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            escreveRelatorio();
            body.Attributes["class"] = "";
        }

        private void escreveRelatorio()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                int idCorp = int.Parse(Request.QueryString["idCorp"]);
                int idEmp = int.Parse(Request.QueryString["idEmp"]);
                string dataBase = Request.QueryString["dataBase"];
                bool ehCorporativo = Boolean.Parse(Request.QueryString["ehCorporativo"]);

                IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);

                Dictionary<string, List<Cx_Lancamento>> dictLancamentos = new Dictionary<string, List<Cx_Lancamento>>();
                List<Mov> listaMov = new List<Mov>();
                List<Mov> listaMovRecebimentos = new List<Mov>();
                List<Cx_Lancamento> listaCxLRecebimentos = new List<Cx_Lancamento>();
                List<Cx_Lancamento> listaCxLRetiradas = new List<Cx_Lancamento>();
                List<Cx_Lancamento> listaCxLEntradas = new List<Cx_Lancamento>();

                IQuery query;

                double saldoInicial = getSaldoInicial(db, idEmp, dataBase);
                if (saldoInicial < 0.001)
                    saldoInicial = 0;

                foreach (Cx_Lancamento cxL in db.Query<Cx_Lancamento>().OrderBy(item => item.id))
                {
                    if (!ehCorporativo)
                        if (cxL.idEmp != idEmp)
                            continue;
                    if (cxL.dthr != null && cxL.dthr.StartsWith(dataBase))
                    {
                        if (cxL.idGrupoTipoPagamento == 0 && cxL.tipo != ECxLancamentoTipo.entrada)
                            continue;

                        Finan_GrupoTipoPagamento finanGrupoTipoPagamento = new Finan_GrupoTipoPagamento();
                        query = db.Query();
                        query.Constrain(typeof(Finan_GrupoTipoPagamento));
                        query.Descend("id").Constrain(cxL.idGrupoTipoPagamento);
                        query.Descend("idEmp").Constrain(idEmp);
                        IObjectSet rs_finanGrupoTipoPagamento = query.Execute();
                        if (rs_finanGrupoTipoPagamento.Count > 0)
                            finanGrupoTipoPagamento = rs_finanGrupoTipoPagamento[0] as Finan_GrupoTipoPagamento;
                        string nomeGrupo = (finanGrupoTipoPagamento.nome == String.Empty || finanGrupoTipoPagamento.nome == null) ? "OUTROS" : finanGrupoTipoPagamento.nome;
                        if (!dictLancamentos.ContainsKey(nomeGrupo))
                            dictLancamentos.Add(nomeGrupo, new List<Cx_Lancamento>());
                        dictLancamentos[nomeGrupo].Add(cxL);
                    }
                }

                Cx_Diario cxD = null;
                query = db.Query();
                query.Constrain(typeof(Cx_Diario));
                query.Descend("data").Constrain(dataBase);
                if (query.Execute().Count == 0)
                    cxD = new Cx_Diario();
                else
                {
                    foreach (Cx_Diario xxx in query.Execute())
                    {
                        if (!ehCorporativo)
                            if (xxx.idEmp != idEmp)
                                continue;
                        cxD = xxx;
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

                sb.Append("<table class='tabela_head'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.AppendFormat("<th>{0}</th>", (cli_emp.apelido_razsoc == "") ? cli_emp.nome : cli_emp.apelido_razsoc);
                sb.AppendFormat("<th>{0}</th>", formata_cpf_cnpj(cli_emp.cpf_cnpj));
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.AppendFormat("<th>Cidade: {0}-{1}</th>", cli_empresa_enderecos[0].cidade, cli_empresa_enderecos[0].uf);
                sb.AppendFormat("<th>Inscrição Estadual: {0}</th>", cli_empresa_enderecos[0].inscr);
                sb.Append("</tr>");
                contatosEmpresa(sb, cli_empresa_contatos);
                sb.Append("</thead>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_ass'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.AppendFormat("<th style='font-size: 14px;'>{0}</th>", "RESUMO DE CAIXA");
                sb.AppendFormat("<th style='font-size: 14px;'>Data: {0}</th>", dataBase);
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.AppendFormat("<th style='font-size: 14px; font-weight:bold'>SALDO INICIAL DO CAIXA: {0}</th>", formatMoney(Decimal.Parse(saldoInicial.ToString()), true));
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.Append("<th style='font-size: 14px; font-weight:bold'>RESUMO DE MOVIMENTAÇÕES</th>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                double totalDinheiro = 0;
                double totalResumo = 0;
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorCobrado = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        /*
                        if (cxL.tipo == ECxLancamentoTipo.recebimento && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            //listaCxLRecebimentos.Add(cxL);
                            //totalRecebimentosResumo += cxL.valorRecebido;
                        }
                        else if (cxL.tipo == ECxLancamentoTipo.retirada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            //listaCxLRetiradas.Add(cxL);
                            //totalRetiradasResumo += cxL.valorOriginal;
                        }
                        else if (cxL.tipo == ECxLancamentoTipo.entrada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            //listaCxLEntradas.Add(cxL);
                            //totalEntradasResumo += cxL.valorOriginal;
                        }
                         * */

                        if (cxL.tipo == ECxLancamentoTipo.venda)
                        {
                            Mov mov = null;
                            query = db.Query();
                            query.Constrain(typeof(Mov));
                            query.Descend("idOperacao").Constrain(cxL.idOperacao);
                            if (query.Execute().Count > 0)
                            {
                                mov = query.Execute()[0] as Mov;
                                if (!ehCorporativo)
                                    if (mov.idEmp != idEmp)
                                        continue;
                                if (mov.idMovCanceladora == 0 && (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido))
                                {
                                    //valorCobrado += cxL.valorRecebido;
                                    valorCobrado += cxL.valorCobrado;
                                    if (cxL.tipo == ECxLancamentoTipo.venda)
                                        if (!listaMov.Contains(mov))
                                            listaMov.Add(mov);
                                    //if (cxL.tipo == ECxLancamentoTipo.recebimento && cxL.situacao == ECxLancamentoSituacao.lancado)
                                    //listaMovRecebimentos.Add(mov);
                                }
                            }
                        }
                    }

                    if (valorCobrado > 0)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", grupo);
                        sb.AppendFormat("<td>R$ {0}</td>", formatMoney(Convert.ToDecimal(valorCobrado), false));
                        sb.Append("</tr>");

                        totalResumo += valorCobrado;

                        if (grupo == "DINHEIRO")
                            totalDinheiro += valorCobrado;
                        if (grupo == "A VISTA")
                            totalDinheiro += valorCobrado;
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.AppendFormat("<th style='font-size: 14px; font-weight:bold'>TOTAL RESUMO: {0}</th>", formatMoney(Convert.ToDecimal(totalResumo), true));
                sb.Append("</tr>");
                sb.Append("</table>");

                /*
                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorTotal = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        if (cxL.tipo == ECxLancamentoTipo.recebimento && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLRecebimentos.Add(cxL);
                            valorTotal += cxL.valorRecebido;
                        }
                    }

                    if (valorTotal > 0)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", grupo);
                        sb.AppendFormat("<td>R$ {0}</td>", formatMoney(Convert.ToDecimal(valorTotal), false));
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorTotal = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        if (cxL.tipo == ECxLancamentoTipo.retirada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLRetiradas.Add(cxL);
                            valorTotal += cxL.valorOriginal;
                        }
                    }

                    if (valorTotal > 0)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", grupo);
                        sb.AppendFormat("<td>R$ {0}</td>", formatMoney(Convert.ToDecimal(valorTotal), false));
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorTotal = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        if (cxL.tipo == ECxLancamentoTipo.entrada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLEntradas.Add(cxL);
                            valorTotal += cxL.valorOriginal;
                        }
                    }

                    if (valorTotal > 0)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", grupo);
                        sb.AppendFormat("<td>R$ {0}</td>", formatMoney(Convert.ToDecimal(valorTotal), false));
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                * */

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.Append("<th style='font-size: 14px; font-weight:bold'>RESUMO DE ENTRADAS E RECEBIMENTOS</th>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                totalResumo = 0;
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorResumo = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        if (cxL.tipo == ECxLancamentoTipo.recebimento && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLRecebimentos.Add(cxL);
                            valorResumo += cxL.valorRecebido;
                            if (grupo == "A VISTA" || grupo == "DINHEIRO")
                                totalDinheiro += cxL.valorRecebido;
                        }
                        else if (cxL.tipo == ECxLancamentoTipo.retirada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLRetiradas.Add(cxL);
                            //valorResumo -= cxL.valorOriginal;
                        }
                        else if (cxL.tipo == ECxLancamentoTipo.entrada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        {
                            listaCxLEntradas.Add(cxL);
                            valorResumo += cxL.valorOriginal;
                            if (grupo == "A VISTA" || grupo == "DINHEIRO" || grupo == "OUTROS")
                                totalDinheiro += cxL.valorRecebido;
                        }
                    }

                    totalResumo += valorResumo;

                    if (valorResumo != 0)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", grupo);
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Convert.ToDecimal(valorResumo), true));
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("<tr>");
                sb.AppendFormat("<th style='font-size: 14px; font-weight:bold'>TOTAL RESUMO: {0}</th>", formatMoney(Convert.ToDecimal(totalResumo), true));
                sb.Append("</tr>");
                sb.Append("</table>");

                /*
                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                sb.Append("<th scope='col' style='width:80%;'>Valor</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                sb.Append("<tr>");
                sb.Append("<td>RECEBIMENTOS</td>");
                sb.AppendFormat("<td>{0}</td>", Utils.formatMoney(Decimal.Parse(totalRecebimentosResumo.ToString()), true));
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>ENTRADAS</td>");
                sb.AppendFormat("<td>{0}</td>", Utils.formatMoney(Decimal.Parse(totalEntradasResumo.ToString()), true));
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>RETIRADAS</td>");
                sb.AppendFormat("<td>{0}</td>", Utils.formatMoney(Decimal.Parse(totalRetiradasResumo.ToString()), true));
                sb.Append("</tr>");

                sb.Append("</tbody>");
                sb.Append("</table>");
                 * */

                if (listaMov.Count > 0)
                {
                    sb.Append("<table class='tabela_ass'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='fontsize: 14px;'>{0}</th>", "MOVIMENTAÇÕES");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col'>Cod</th>");
                    sb.Append("<th scope='col'>Cliente</th>");
                    sb.Append("<th scope='col'>Vendedor</th>");
                    sb.Append("<th scope='col'>Data/Hora</th>");
                    sb.Append("<th scope='col'>Forma Pgto.</th>");
                    sb.Append("<th scope='col'>Valor Bruto</th>");
                    sb.Append("<th scope='col'>Valor Desconto</th>");
                    sb.Append("<th scope='col'>Valor Liquido</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    foreach (Mov mov in listaMov.OrderBy(item => StringToDateTime(item.dthrMovEmissao)))
                    {
                        List<string> listaFormaPagamento = new List<string>();
                        query = db.Query();
                        query.Constrain(typeof(Cx_Lancamento));
                        query.Descend("idTransacao").Constrain(mov.idTransacao);
                        foreach (Cx_Lancamento cxL in query.Execute())
                            if (!listaFormaPagamento.Contains(cxL.tipoPagamento_nome))
                                listaFormaPagamento.Add(cxL.tipoPagamento_nome);

                        string formaPagamento = "";

                        for (int i = 0; i < listaFormaPagamento.Count; i++)
                        {
                            if (i == 0)
                                formaPagamento += listaFormaPagamento[i];
                            else
                                formaPagamento += "/" + listaFormaPagamento[i];
                        }

                        /*
                        foreach (string pg in listaFormaPagamento)
                            formaPagamento += pg + "/";
                         * */

                        Cliente funcionario = null;
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(mov.idClienteFuncionarioVendedor);
                        funcionario = query.Execute()[0] as Cliente;

                        mov.vlrAcrescimo = -mov.vlrAcrescimo;

                        double vlrItensInicial = (mov.vlrItensInicial < 0.001) ? 0 :  mov.vlrItensInicial;
                        double vlrAcrescimo = (mov.vlrAcrescimo < 0.001) ? 0 : mov.vlrAcrescimo;
                        double vlrItensFinal = (mov.vlrItensFinal < 0.001) ? 0 : mov.vlrItensFinal;

                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", mov.id);
                        sb.AppendFormat("<td>{0}</td>", mov.cliente_nome);
                        sb.AppendFormat("<td>{0}</td>", funcionario.nome);
                        sb.AppendFormat("<td>{0}</td>", mov.dthrMovEmissao);
                        sb.AppendFormat("<td>{0}</td>", formaPagamento);
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(vlrItensInicial.ToString()), true));
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(vlrAcrescimo.ToString()), true));
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(vlrItensFinal.ToString()), true));
                        sb.Append("</tr>");
                    }

                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }

                double totalRecebimentos = 0;
                if (listaCxLRecebimentos.Count > 0)
                {
                    sb.Append("<table class='tabela_ass'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='fontsize: 14px;'>{0}</th>", "RECEBIMENTOS");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col' style='width:5%%;'>Cod</th>");
                    sb.Append("<th scope='col' style='width:25%;'>Cliente</th>");
                    sb.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    sb.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    sb.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    foreach (Cx_Lancamento cxL in listaCxLRecebimentos.OrderBy(item => StringToDateTime(item.dthr)))
                    {
                        Mov mov = null;
                        query = db.Query();
                        query.Constrain(typeof(Mov));
                        query.Descend("idTransacao").Constrain(cxL.idTransacao);
                        mov = query.Execute()[0] as Mov;

                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", cxL.id);
                        sb.AppendFormat("<td>{0}</td>", mov.cliente_nome);
                        sb.AppendFormat("<td>{0}</td>", cxL.dthr);
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(cxL.valorRecebido.ToString()), true));
                        sb.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        sb.Append("</tr>");

                        //totalDinheiro += cxL.valorRecebido;
                        totalRecebimentos += cxL.valorRecebido;
                    }

                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RECEBIMENTOS: {0}</th>", formatMoney(Convert.ToDecimal(totalRecebimentos), true));
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                double totalEntradas = 0;
                if (listaCxLEntradas.Count > 0)
                {
                    sb.Append("<table class='tabela_ass'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='fontsize: 14px;'>{0}</th>", "ENTRADAS");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col' style='width:5%;'>Cod</th>");
                    sb.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    sb.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    sb.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    foreach (Cx_Lancamento cxL in listaCxLEntradas)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", cxL.id);
                        sb.AppendFormat("<td>{0}</td>", cxL.dthr);
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(cxL.valorRecebido.ToString()), true));
                        sb.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        sb.Append("</tr>");

                        //totalDinheiro += cxL.valorRecebido;
                        totalEntradas += cxL.valorRecebido;
                    }

                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL ENTRADAS: {0}</th>", formatMoney(Convert.ToDecimal(totalEntradas), true));
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                double totalRetiradas = 0;
                if (listaCxLRetiradas.Count > 0)
                {
                    sb.Append("<table class='tabela_ass'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='fontsize: 14px;'>{0}</th>", "RETIRADAS");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col' style='width:5%;'>Cod</th>");
                    sb.Append("<th scope='col' style='width:25%;'>Centro Custo</th>");
                    sb.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    sb.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    sb.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    foreach (Cx_Lancamento cxL in listaCxLRetiradas)
                    {
                        Finan_Lancamento finanLancamento = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_Lancamento));
                        query.Descend("idTransacao").Constrain(cxL.idTransacao);
                        finanLancamento = query.Execute()[0] as Finan_Lancamento;

                        Finan_TipoLancamento finanTipoLancamento = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_TipoLancamento));
                        query.Descend("id").Constrain(finanLancamento.idTipoLancamento);
                        finanTipoLancamento = query.Execute()[0] as Finan_TipoLancamento;

                        /*
                        Finan_CentroCusto finanCentroCusto = null;
                        query = db.Query();
                        query.Constrain(typeof(Finan_CentroCusto));
                        query.Descend("id").Constrain(finanLancamento.idCentroCusto);
                        finanCentroCusto = query.Execute()[0] as Finan_CentroCusto;
                         * */

                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", cxL.id);
                        sb.AppendFormat("<td>{0}</td>", finanTipoLancamento.nomeTipoLancamento);
                        sb.AppendFormat("<td>{0}</td>", cxL.dthr);
                        sb.AppendFormat("<td>{0}</td>", formatMoney(Decimal.Parse(cxL.valorCobrado.ToString()), true));
                        sb.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        sb.Append("</tr>");

                        totalRetiradas += cxL.valorCobrado;
                    }

                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    sb.Append("<table class='tabela_mov'>");
                    sb.Append("<thead/>");
                    sb.Append("<tr>");
                    sb.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RETIRADAS: {0}</th>", formatMoney(Convert.ToDecimal(totalRetiradas), true));
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }

                totalDinheiro += saldoInicial;

                double totalEmEspecie = (totalDinheiro - totalRetiradas < 0.001) ? 0 : totalDinheiro - totalRetiradas;

                sb.Append("<table class='tabela_mov'>");
                sb.Append("<thead/>");
                sb.Append("</tr>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.AppendFormat("<th style='font-size:14px; font-weight:bold'>TOTAL EM ESPECIE: {0}</th>", formatMoney(Decimal.Parse((totalEmEspecie).ToString()), true));
                sb.Append("</tr>");
                sb.Append("</table>");

                Literal1.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO: "+ ex +"\n"+ ex.StackTrace);
            }
        }

        #region Utils

        private string formata_cpf_cnpj(string cpf_cnpj)
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

            return tipo + ": " + saida.ToString();
        }
        private void contatosEmpresa(StringBuilder sb, IList<ClienteContato> contatos)
        {
            string cContatos = "";
            sb.Append("<tr>");
            for (int i = 0; i < contatos.Count; i++)
            {
                ClienteContato cc = contatos[i];
                cContatos += string.Format("{0}: {1},   ", cc.campo, cc.valor);
            }
            sb.AppendFormat("<th>Contato(s): {0}</th>", cContatos);
            sb.Append("</tr>");
        }
        private string formatMoney(Decimal d, Boolean mostra_cifra)
        {
            if (mostra_cifra)
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:C}", d);
            else
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:N}", d);
        }
        private DateTime StringToDateTime(string dataString)
        {
            if (dataString.Length == 10)
                return DateTime.Parse(dataString);
            else
                return DateTime.Parse(dataString.Substring(0, 10));
        }
        private double getSaldoInicial(IObjectContainer db, int idEmp, string dataAtual)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Cx_Diario));
            query.Descend("data").Constrain(dataAtual);
            query.Descend("idEmp").Constrain(idEmp);
            Cx_Diario cxD = query.Execute()[0] as Cx_Diario;
            return cxD.valorAbertura;
        }

        #endregion
    }
}
