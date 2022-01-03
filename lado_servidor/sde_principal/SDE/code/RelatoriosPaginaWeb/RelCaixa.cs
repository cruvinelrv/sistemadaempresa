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

/* ATENÇÃO: ESTE CÓDIGO NÃO FOI OTIMIZADO, ESTA É UMA TRANSCRIÇÃO DO CODIGO ANTIGO PARA ESTA CLASSE
 * FAZER POSTERIORMENTE ESTE RELATÓRIO QUE ATENDA AO NOVO CAIXA QUANDO ESTE ESTIVER COMPLETO
 * O CODIGO TRANSCRITO ESTARA SINALISADO COMO TAL.
 * */

namespace SDE.RelatoriosPaginaWeb
{
    public class RelCaixa
    {
        public StringBuilder GeraRelCaixa(int idCorp, int idEmp, String dtCaixa, bool corporativo)
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

            //CODIGO TRANSCRITO
            Dictionary<string, List<Cx_Lancamento>> dictLancamentos = new Dictionary<string, List<Cx_Lancamento>>();
            List<Mov> listaMov = new List<Mov>();
            List<Mov> listaMovRecebimentos = new List<Mov>();
            List<Cx_Lancamento> listaCxLRecebimentos = new List<Cx_Lancamento>();
            List<Cx_Lancamento> listaCxLRetiradas = new List<Cx_Lancamento>();
            List<Cx_Lancamento> listaCxLEntradas = new List<Cx_Lancamento>();

            double saldoInicial = GetSaldoInicial(db, idEmp, dtCaixa);
            if (saldoInicial < 0.001)
                saldoInicial = 0;

            foreach (Cx_Lancamento cxL in db.Query<Cx_Lancamento>().OrderBy(item => item.id))
            {
                if (!corporativo)
                    if (cxL.idEmp != idEmp)
                        continue;
                if (cxL.dthr != null && cxL.dthr.StartsWith(dtCaixa))
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
            query.Descend("data").Constrain(dtCaixa);
            if (query.Execute().Count == 0)
                cxD = new Cx_Diario();
            else
            {
                foreach (Cx_Diario xxx in query.Execute())
                {
                    if (!corporativo)
                        if (xxx.idEmp != idEmp)
                            continue;
                    cxD = xxx;
                }
            }
            //FIM CODIGO TRANSCRITO

            try
            {
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtCaixa);

                //CODIGO TRANSCRITO

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>SALDO INICIAL DO CAIXA: {0}</th>", Utils.formatMoney(Convert.ToDecimal(saldoInicial), true));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th style='font-size: 12px; font-weight:bold'>RESUMO DE MOVIMENTAÇÕES</th>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                stringBuilderRel.Append("<th scope='col' style='width:80%;'>Valor</th>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");

                double totalDinheiro = 0;
                double totalResumo = 0;
                for (int i = 0; i < dictLancamentos.Count; i++)
                {
                    string grupo = dictLancamentos.ElementAt(i).Key;
                    List<Cx_Lancamento> listaLancamentos = dictLancamentos.ElementAt(i).Value;

                    double valorCobrado = 0;
                    foreach (Cx_Lancamento cxL in listaLancamentos)
                    {
                        if (cxL.tipo == ECxLancamentoTipo.venda)
                        {
                            Mov mov = null;
                            query = db.Query();
                            query.Constrain(typeof(Mov));
                            query.Descend("idOperacao").Constrain(cxL.idOperacao);
                            if (query.Execute().Count > 0)
                            {
                                mov = query.Execute()[0] as Mov;
                                if (!corporativo)
                                    if (mov.idEmp != idEmp)
                                        continue;
                                if (mov.idMovCanceladora == 0 && (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido))
                                {
                                    valorCobrado += cxL.valorCobrado;
                                    if (cxL.tipo == ECxLancamentoTipo.venda)
                                        if (!listaMov.Contains(mov))
                                            listaMov.Add(mov);
                                }
                            }
                        }
                    }

                    if (valorCobrado > 0)
                    {
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", grupo);
                        stringBuilderRel.AppendFormat("<td>R$ {0}</td>", Utils.formatMoney(Convert.ToDecimal(valorCobrado), false));
                        stringBuilderRel.Append("</tr>");

                        totalResumo += valorCobrado;

                        if (grupo == "DINHEIRO")
                            totalDinheiro += valorCobrado;
                        if (grupo == "A VISTA")
                            totalDinheiro += valorCobrado;
                    }
                }
                stringBuilderRel.Append("</tbody>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RESUMO: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalResumo), true));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th style='font-size: 12px; font-weight:bold'>RESUMO DE ENTRADAS E RECEBIMENTOS</th>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.Append("<th scope='col' style='width:80%;'>Resumo</th>");
                stringBuilderRel.Append("<th scope='col' style='width:80%;'>Valor</th>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</thead>");

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
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", grupo);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(valorResumo), true));
                        stringBuilderRel.Append("</tr>");
                    }
                }
                stringBuilderRel.Append("</tbody>");
                stringBuilderRel.Append("</table>");

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RESUMO: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalResumo), true));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                if (listaMov.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_ass'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "MOVIMENTAÇÕES");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th scope='col'>Cod</th>");
                    stringBuilderRel.Append("<th scope='col'>Cliente</th>");
                    stringBuilderRel.Append("<th scope='col'>Vendedor</th>");
                    stringBuilderRel.Append("<th scope='col'>Data/Hora</th>");
                    stringBuilderRel.Append("<th scope='col'>Forma Pgto.</th>");
                    stringBuilderRel.Append("<th scope='col'>Valor Bruto</th>");
                    stringBuilderRel.Append("<th scope='col'>Valor Desconto</th>");
                    stringBuilderRel.Append("<th scope='col'>Valor Liquido</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");

                    foreach (Mov mov in listaMov.OrderBy(item => Utils.StringToDateTime(item.dthrMovEmissao)))
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

                        Cliente funcionario = null;
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(mov.idClienteFuncionarioVendedor);
                        funcionario = query.Execute()[0] as Cliente;

                        mov.vlrAcrescimo = -mov.vlrAcrescimo;

                        double vlrItensInicial = (mov.vlrItensInicial < 0.001) ? 0 : mov.vlrItensInicial;
                        double vlrAcrescimo = (mov.vlrAcrescimo < 0.001) ? 0 : mov.vlrAcrescimo;
                        double vlrItensFinal = (mov.vlrItensFinal < 0.001) ? 0 : mov.vlrItensFinal;

                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", mov.id);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", mov.cliente_nome);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", funcionario.nome);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", mov.dthrMovEmissao);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", formaPagamento);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(vlrItensInicial), true));
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(vlrAcrescimo), true));
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(vlrItensFinal), true));
                        stringBuilderRel.Append("</tr>");
                    }

                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");
                }

                double totalRecebimentos = 0;
                if (listaCxLRecebimentos.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_ass'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "RECEBIMENTOS");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th scope='col' style='width:5%%;'>Cod</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:25%;'>Cliente</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");

                    foreach (Cx_Lancamento cxL in listaCxLRecebimentos.OrderBy(item => Utils.StringToDateTime(item.dthr)))
                    {
                        Mov mov = null;
                        query = db.Query();
                        query.Constrain(typeof(Mov));
                        query.Descend("idTransacao").Constrain(cxL.idTransacao);
                        mov = query.Execute()[0] as Mov;

                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.id);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", mov.cliente_nome);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.dthr);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(cxL.valorRecebido), true));
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        stringBuilderRel.Append("</tr>");

                        totalRecebimentos += cxL.valorRecebido;
                    }

                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RECEBIMENTOS: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalRecebimentos), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
                }

                double totalEntradas = 0;
                if (listaCxLEntradas.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_ass'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "ENTRADAS");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th scope='col' style='width:5%;'>Cod</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");

                    foreach (Cx_Lancamento cxL in listaCxLEntradas)
                    {
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.id);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.dthr);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(cxL.valorRecebido), true));
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        stringBuilderRel.Append("</tr>");

                        totalEntradas += cxL.valorRecebido;
                    }

                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL ENTRADAS: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalEntradas), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
                }

                double totalRetiradas = 0;
                if (listaCxLRetiradas.Count > 0)
                {
                    stringBuilderRel.Append("<table class='tabela_ass'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "RETIRADAS");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.Append("<th scope='col' style='width:5%;'>Cod</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:25%;'>Centro Custo</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:18%;'>Data/Hora</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:12%;'>Valor</th>");
                    stringBuilderRel.Append("<th scope='col' style='width:40%;'>Histórico</th>");
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</thead>");

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

                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.id);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", finanTipoLancamento.nomeTipoLancamento);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.dthr);
                        stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(cxL.valorCobrado), true));
                        stringBuilderRel.AppendFormat("<td>{0}</td>", cxL.observacoes);
                        stringBuilderRel.Append("</tr>");

                        totalRetiradas += cxL.valorCobrado;
                    }

                    stringBuilderRel.Append("</tbody>");
                    stringBuilderRel.Append("</table>");

                    stringBuilderRel.Append("<table class='tabela_mov'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='font-size: 12px; font-weight:bold'>TOTAL RETIRADAS: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalRetiradas), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
                }

                totalDinheiro += saldoInicial;

                double totalEmEspecie = (totalDinheiro - totalRetiradas < 0.001) ? 0 : totalDinheiro - totalRetiradas;

                stringBuilderRel.Append("<table class='tabela_mov'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='font-size:12px; font-weight:bold'>TOTAL EM ESPECIE: {0}</th>", Utils.formatMoney(Convert.ToDecimal(totalEmEspecie), true));
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");

                //FIM CODIGO TRANSCRITO

                return stringBuilderRel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void GeraCabecalho(StringBuilder stringBuilderRel, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos, String dtCaixa)
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "RELATÓRIO DE CAIXA");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>DATA:{0}</th>", dtCaixa);
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

        private double GetSaldoInicial(IObjectContainer db, int idEmp, string dataAtual)
        {
            IQuery query = db.Query();
            query.Constrain(typeof(Cx_Diario));
            query.Descend("data").Constrain(dataAtual);
            query.Descend("idEmp").Constrain(idEmp);
            Cx_Diario cxD = query.Execute()[0] as Cx_Diario;
            return cxD.valorAbertura;
        }
    }
}
