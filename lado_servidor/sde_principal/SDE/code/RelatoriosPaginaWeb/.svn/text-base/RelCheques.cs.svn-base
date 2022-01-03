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
    public class RelCheques
    {
        Double totalGeral;

        public StringBuilder GeraRelCheques(int idCorp, int idEmp, String dtInicial, String dtFinal,
            bool cheques_a_receber, bool cheques_baixados, bool cheques_compensados, bool cheques_devolvidos)
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
            query.Constrain(typeof(Finan_Titulo));
            query.Descend("tipo").Constrain(ETipoTitulo.cheque_a_receber);
            IObjectSet rs_finanTitulo = query.Execute();

            List<Finan_Titulo> listaChequesReceber = new List<Finan_Titulo>();
            Dictionary<int, List<Finan_Titulo>> dicionarioChequesBaixadosPorFinanConta = new Dictionary<int,List<Finan_Titulo>>();
            Dictionary<int, List<Finan_Titulo>> dicionarioChequesCompensadosPorFinanConta = new Dictionary<int, List<Finan_Titulo>>();
            Dictionary<int, List<Finan_Titulo>> dicionarioChequesDevolvidosPorFinanConta1 = new Dictionary<int, List<Finan_Titulo>>();
            Dictionary<int, List<Finan_Titulo>> dicionarioChequesDevolvidosPorFinanConta2 = new Dictionary<int, List<Finan_Titulo>>();

            foreach (Finan_Titulo finanTitulo in rs_finanTitulo)
            {
                if (cheques_a_receber)
                {
                    if (!finanTitulo.isAlterado && !finanTitulo.isBaixado && !finanTitulo.isCompensado &&
                        Utils.StringToDateTime(finanTitulo.dtPagamento) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanTitulo.dtPagamento) <= Utils.StringToDateTime(dtFinal))
                        listaChequesReceber.Add(finanTitulo);
                }
                if (cheques_baixados)
                {
                    if (finanTitulo.isBaixado && !finanTitulo.isAlterado && finanTitulo.dtBaixa != String.Empty &&
                        Utils.StringToDateTime(finanTitulo.dtBaixa) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanTitulo.dtBaixa) <= Utils.StringToDateTime(dtFinal))
                        AdicionaDicionario(dicionarioChequesBaixadosPorFinanConta, finanTitulo);
                }
                if (cheques_compensados)
                {
                    if (finanTitulo.isBaixado && finanTitulo.isCompensado && !finanTitulo.isAlterado && finanTitulo.dtCompensacao != String.Empty &&
                        Utils.StringToDateTime(finanTitulo.dtCompensacao) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanTitulo.dtCompensacao) <= Utils.StringToDateTime(dtFinal))
                        AdicionaDicionario(dicionarioChequesCompensadosPorFinanConta, finanTitulo);
                }
                if (cheques_devolvidos)
                {
                    if (finanTitulo.isBaixado && !finanTitulo.isAlterado && finanTitulo.isDevolvido1 && !finanTitulo.isDevolvido2 &&
                        Utils.StringToDateTime(finanTitulo.dtDevolucao1) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanTitulo.dtDevolucao1) <= Utils.StringToDateTime(dtFinal))
                        AdicionaDicionario(dicionarioChequesDevolvidosPorFinanConta1, finanTitulo);
                    if (finanTitulo.isBaixado && !finanTitulo.isAlterado && finanTitulo.isDevolvido1 && finanTitulo.isDevolvido2 &&
                        Utils.StringToDateTime(finanTitulo.dtDevolucao2) >= Utils.StringToDateTime(dtInicial) && Utils.StringToDateTime(finanTitulo.dtDevolucao2) <= Utils.StringToDateTime(dtFinal))
                        AdicionaDicionario(dicionarioChequesDevolvidosPorFinanConta2, finanTitulo);
                }
            }

            try
            {
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                if (cheques_a_receber)
                {
                    GeraTitulo(stringBuilderRel, listaChequesReceber.Count > 0, true, false, false, false);
                    if (listaChequesReceber.Count > 0)
                    {
                        GeraTableHeader(stringBuilderRel, db, true, false, false, 0);
                        GeraTableBody(stringBuilderRel, db, listaChequesReceber);
                    }
                }
                if (cheques_baixados)
                {
                    totalGeral = 0;
                    GeraTitulo(stringBuilderRel, dicionarioChequesBaixadosPorFinanConta.Keys.Count > 0, false, true, false, false);
                    foreach (int idConta in dicionarioChequesBaixadosPorFinanConta.Keys)
                    {
                        GeraTableHeader(stringBuilderRel, db, false, false, false, idConta);
                        GeraTableBody(stringBuilderRel, db, dicionarioChequesBaixadosPorFinanConta, idConta, true, false, false, false);
                    }
                    if (dicionarioChequesBaixadosPorFinanConta.Keys.Count > 0)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.AppendFormat("<tr><th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGeral), true));
                        stringBuilderRel.Append("</table>");
                    }
                }
                if (cheques_compensados)
                {
                    totalGeral = 0;
                    GeraTitulo(stringBuilderRel, dicionarioChequesCompensadosPorFinanConta.Keys.Count > 0, false, false, true, false);
                    foreach (int idConta in dicionarioChequesCompensadosPorFinanConta.Keys)
                    {
                        GeraTableHeader(stringBuilderRel, db, false, false, false, idConta);
                        GeraTableBody(stringBuilderRel, db, dicionarioChequesCompensadosPorFinanConta, idConta, false, true, false, false);
                    }
                    if (dicionarioChequesCompensadosPorFinanConta.Keys.Count > 0)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.AppendFormat("<tr><th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGeral), true));
                        stringBuilderRel.Append("</table>");
                    }
                }
                if (cheques_devolvidos)
                {
                    totalGeral = 0;
                    GeraTitulo(stringBuilderRel, (dicionarioChequesDevolvidosPorFinanConta1.Keys.Count > 0 || dicionarioChequesDevolvidosPorFinanConta2.Keys.Count > 0), false, false, false, true);
                    foreach (int idConta in dicionarioChequesDevolvidosPorFinanConta1.Keys)
                    {
                        GeraTableHeader(stringBuilderRel, db, false, true, false, idConta);
                        GeraTableBody(stringBuilderRel, db, dicionarioChequesDevolvidosPorFinanConta1, idConta, false, false, true, false);
                    }
                    if (dicionarioChequesDevolvidosPorFinanConta1.Keys.Count > 0)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.AppendFormat("<tr><th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGeral), true));
                        stringBuilderRel.Append("</table>");
                    }

                    totalGeral = 0;
                    foreach (int idConta in dicionarioChequesDevolvidosPorFinanConta2.Keys)
                    {
                        GeraTableHeader(stringBuilderRel, db, false, false, true, idConta);
                        GeraTableBody(stringBuilderRel, db, dicionarioChequesDevolvidosPorFinanConta2, idConta, false, false, false, true);
                    }
                    if (dicionarioChequesDevolvidosPorFinanConta2.Keys.Count > 0)
                    {
                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.AppendFormat("<tr><th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGeral), true));
                        stringBuilderRel.Append("</table>");
                    }
                }

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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "RELATÓRIO DE CHEQUES");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");
        }

        private void GeraTitulo(StringBuilder stringBuilderRel, bool possuiItens, bool cheques_a_receber, bool cheques_baixados, bool cheques_compensados, bool cheques_devolvidos)
        {
            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");

            if (cheques_a_receber)
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:black;'>{0}</th>", (possuiItens) ? "CHEQUES A RECEBER" : "NÃO EXISTEM CHEQUES A RECEBER NO PERÍODO");
            if (cheques_baixados)
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:black;'>{0}</th>", (possuiItens) ? "CHEQUES BAIXADOS" : "NÃO EXISTEM CHEQUES BAIXADOS NO PERÍODO");
            if (cheques_compensados)
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:black;'>{0}</th>", (possuiItens) ? "CHEQUES COMPENSADOS" : "NÃO EXISTEM CHEQUES BAIXADOS NO PERÍODO");
            if (cheques_devolvidos)
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:black;'>{0}</th>", (possuiItens) ? "CHEQUES DEVOLVIDOS" : "NÃO EXISTEM CHEQUES DEVOLVIDOS NO PERÍODO");

            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");
        }

        private void GeraTableHeader(StringBuilder stringBuilderRel, IObjectContainer db, bool cheques_a_receber, bool cheques_devolvidos1, bool cheques_devolvidos2, int idConta)
        {
            if (!cheques_a_receber)
            {
                IQuery query = db.Query();
                query.Constrain(typeof(Finan_Conta));
                query.Descend("id").Constrain(idConta);
                IObjectSet rs_finanConta = query.Execute();
                Finan_Conta finanConta_db = rs_finanConta[0] as Finan_Conta;

                stringBuilderRel.Append("<table class='tabela_head'>");
                stringBuilderRel.Append("<thead/>");
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:gray; width:50%'>Conta: {0}</th>", finanConta_db.nome);
                stringBuilderRel.AppendFormat("<th style='color:white; background-color:gray; width:30%'>Tipo: {0}</th>", finanConta_db.tipo);
                if (cheques_devolvidos1)
                    stringBuilderRel.Append("<th style='color:white; background-color:gray; width:20%'>1ª Devolução</th>");
                if (cheques_devolvidos2)
                    stringBuilderRel.Append("<th style='color:white; background-color:gray; width:20%'>2ª Devolução</th>");
                stringBuilderRel.Append("</tr>");
                stringBuilderRel.Append("</table>");
            }

            stringBuilderRel.Append("<table class='tabela_mov'>");
            stringBuilderRel.Append("<thead>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:40%;'>Emitente</th>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:30%;'>Fornecedor</th>");
            stringBuilderRel.AppendFormat("<th scoupe='colune' style='width:10%;'>{0}</th>", (cheques_a_receber) ? "Bom para o dia" : "Data");
            stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Nº Cheque</th>");
            stringBuilderRel.Append("<th scoupe='colune' style='width:10%;'>Valor</th>");
            stringBuilderRel.Append("</thead>");
            stringBuilderRel.Append("<tbody>");
        }

        private void GeraTableBody(StringBuilder stringBuilderRel, IObjectContainer db, Dictionary<int, List<Finan_Titulo>> dicionarioFinanTitulo, int idConta, bool cheques_baixados, bool cheques_compensados, bool cheques_devolvidos_1, bool cheques_devolvidos_2)
        {
            Double totalGrupo = 0;

            if (cheques_baixados)
            {
                foreach (Finan_Titulo finanTitulo in dicionarioFinanTitulo[idConta].OrderBy(ft => Utils.StringToDateTime(ft.dtBaixa)))
                {
                    Cliente clientePagar_db = getClientePagar(db, finanTitulo);
                    Cliente clienteFornecedorCheque_db = getClienteFornecedorCheque(db, finanTitulo, clientePagar_db);

                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", clientePagar_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", (clienteFornecedorCheque_db == clientePagar_db) ? "O MESMO" : clienteFornecedorCheque_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.dtBaixa);
                    stringBuilderRel.AppendFormat("<td>{0} baixado:{1} alterado:{1}</td>", finanTitulo.numCheque, finanTitulo.isBaixado, finanTitulo.isAlterado);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTitulo.valorCobrado.ToString()), true));
                    stringBuilderRel.Append("</tr>");

                    totalGrupo += finanTitulo.valorCobrado;
                }
            }
            if (cheques_compensados)
            {
                foreach (Finan_Titulo finanTitulo in dicionarioFinanTitulo[idConta].OrderBy(ft => Utils.StringToDateTime(ft.dtCompensacao)))
                {
                    Cliente clientePagar_db = getClientePagar(db, finanTitulo);
                    Cliente clienteFornecedorCheque_db = getClienteFornecedorCheque(db, finanTitulo, clientePagar_db);

                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", clientePagar_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", (clienteFornecedorCheque_db == clientePagar_db) ? "O MESMO" : clienteFornecedorCheque_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.dtCompensacao);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.numCheque);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTitulo.valorCobrado.ToString()), true));
                    stringBuilderRel.Append("</tr>");

                    totalGrupo += finanTitulo.valorCobrado;
                }
            }
            if (cheques_devolvidos_1)
            {
                foreach (Finan_Titulo finanTitulo in dicionarioFinanTitulo[idConta].OrderBy(ft => Utils.StringToDateTime(ft.dtDevolucao1)))
                {
                    Cliente clientePagar_db = getClientePagar(db, finanTitulo);
                    Cliente clienteFornecedorCheque_db = getClienteFornecedorCheque(db, finanTitulo, clientePagar_db);

                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", clientePagar_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", (clienteFornecedorCheque_db == clientePagar_db) ? "O MESMO" : clienteFornecedorCheque_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.dtDevolucao1);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.numCheque);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTitulo.valorCobrado.ToString()), true));
                    stringBuilderRel.Append("</tr>");

                    totalGrupo += finanTitulo.valorCobrado;
                }
            }
            if (cheques_devolvidos_2)
            {
                foreach (Finan_Titulo finanTitulo in dicionarioFinanTitulo[idConta].OrderBy(ft => Utils.StringToDateTime(ft.dtDevolucao2)))
                {
                    Cliente clientePagar_db = getClientePagar(db, finanTitulo);
                    Cliente clienteFornecedorCheque_db = getClienteFornecedorCheque(db, finanTitulo, clientePagar_db);

                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td>{0}</td>", clientePagar_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", (clienteFornecedorCheque_db == clientePagar_db) ? "O MESMO" : clienteFornecedorCheque_db.nome);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.dtDevolucao2);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.numCheque);
                    stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTitulo.valorCobrado.ToString()), true));
                    stringBuilderRel.Append("</tr>");

                    totalGrupo += finanTitulo.valorCobrado;
                }
            }
            stringBuilderRel.Append("</tbody>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#87CEEB;'>TOTAL GRUPO: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGrupo), true));
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");

            totalGeral += totalGrupo;
        }

        private void GeraTableBody(StringBuilder stringBuilderRel, IObjectContainer db, List<Finan_Titulo> listaFinanTitulo)
        {
            Double totalGrupo = 0;

            foreach (Finan_Titulo finanTitulo in listaFinanTitulo)
            {
                Cliente clientePagar_db = getClientePagar(db, finanTitulo);
                Cliente clienteFornecedorCheque_db = getClienteFornecedorCheque(db, finanTitulo, clientePagar_db);

                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<td>{0}</td>", clientePagar_db.nome);
                stringBuilderRel.AppendFormat("<td>{0}</td>", (clienteFornecedorCheque_db == clientePagar_db) ? "O MESMO" : clienteFornecedorCheque_db.nome);
                stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.dtPagamento);
                stringBuilderRel.AppendFormat("<td>{0}</td>", finanTitulo.numCheque);
                stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTitulo.valorCobrado.ToString()), true));
                stringBuilderRel.Append("</tr>");

                totalGrupo += finanTitulo.valorCobrado;
            }
            stringBuilderRel.Append("</tbody>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead/>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='font-weight:bold; width:33%; background-color:#EEDD82;'>TOTAL GERAL: {0}</th></tr>", Utils.formatMoney(Convert.ToDecimal(totalGrupo), true));
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

        private void AdicionaDicionario(Dictionary<int, List<Finan_Titulo>> dicionarioCheques, Finan_Titulo finanTitulo)
        {
            if (!dicionarioCheques.ContainsKey(finanTitulo.idContaDestino))
                dicionarioCheques.Add(finanTitulo.idContaDestino, new List<Finan_Titulo>());
            dicionarioCheques[finanTitulo.idContaDestino].Add(finanTitulo);
        }

        private Cliente getClientePagar(IObjectContainer db, Finan_Titulo finanTitulo)
        {
            IQuery query;

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(finanTitulo.idClienteAPagar);
            IObjectSet rs_clientePagar = query.Execute();
            Cliente clientePagar_db = rs_clientePagar[0] as Cliente;
            return clientePagar_db;
        }

        private Cliente getClienteFornecedorCheque(IObjectContainer db, Finan_Titulo finanTitulo, Cliente clientePagar_db)
        {
            IQuery query;

            Cliente clienteFornecedorCheque_db;
            if (finanTitulo.idClienteAPagar == finanTitulo.idFornecedorCheque)
                clienteFornecedorCheque_db = clientePagar_db;
            else
            {
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(finanTitulo.idFornecedorCheque);
                IObjectSet rs_clienteFornecedorCheque = query.Execute();
                clienteFornecedorCheque_db = rs_clienteFornecedorCheque[0] as Cliente;
            }
            return clienteFornecedorCheque_db;
        }
    }
}
