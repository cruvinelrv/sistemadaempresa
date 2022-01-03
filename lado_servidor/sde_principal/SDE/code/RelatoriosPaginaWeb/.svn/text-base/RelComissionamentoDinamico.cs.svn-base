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
using System.Collections;
using System.Collections.Generic;
using SDE.Enumerador;

namespace SDE.RelatoriosPaginaWeb
{
    public class RelComissionamentoDinamico
    {
        double comissaoProdutosVendidosFuncionarioTotal = 0;
        double comissaoProdutosEmGarantiaFuncionarioTotal = 0;
        double comissaoMaoDeObraFuncionarioTotal = 0;
        double comissaoMaoDeObraGeralFuncionarioTotal = 0;
        double comissaoMaoDeObraGarantiaFuncionarioTotal = 0;
        double comissaoMaoDeObraGeralGarantiaFuncionarioTotal = 0;
        double comissaoMontanteTotalFuncionarioTotal = 0;
        double comissaoSobreProdutosFuncionarioTotal = 0;
        double comissaoSobreTipoPagamentoFuncionarioTotal = 0;

        bool imprimeProdutos;
        bool imprimeProdutosGarantia;
        bool imprimeMaoObra;
        bool imprimeMaoObraGeral;
        bool imprimeMaoObraGarantia;
        bool imprimeMaoObraGeralGarantia;
        bool imprimeMontanteTotal;
        bool imprimeTipoPagamento;
        bool imprimeVendas;

        public StringBuilder GeraRelComissionamentoDinamico(int idCorp, int idEmp, String dtInicial, String dtFinal, int idFuncionario, bool exibeMovimentacao)
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
                        (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido) && mov.idClienteFuncionarioVendedor != 1);
                }
                );
            List<Mov> listaMov = new List<Mov>(rs_mov);

            Dictionary<Cliente, List<Mov>> dicionarioMovPorFuncionario = new Dictionary<Cliente, List<Mov>>();
            foreach (Mov mov in listaMov.OrderBy(item => Utils.StringToDateTime(item.dthrMovEmissao)))
            {
                if (idFuncionario != 0 && idFuncionario != mov.idClienteFuncionarioVendedor)
                    continue;
                if (mov.idClienteFuncionarioVendedor == 1)
                    continue;

                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(mov.idClienteFuncionarioVendedor);
                IObjectSet rs_clienteFuncionarioVendedor = query.Execute();
                Cliente clienteFuncionarioVendedor_db = rs_clienteFuncionarioVendedor[0] as Cliente;

                if (!dicionarioMovPorFuncionario.ContainsKey(clienteFuncionarioVendedor_db))
                    dicionarioMovPorFuncionario.Add(clienteFuncionarioVendedor_db, new List<Mov>());
                dicionarioMovPorFuncionario[clienteFuncionarioVendedor_db].Add(mov);
            }

            try
            {
                GeraCabecalho(stringBuilderRel, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal);

                foreach (Cliente chave in dicionarioMovPorFuncionario.Keys.OrderBy(item => item.nome))
                {
                    List<Mov> listaMovPorFuncionario = dicionarioMovPorFuncionario[chave];


                    ClienteFuncionarioComissionamento clienteFuncionarioComissionamento_db = new ClienteFuncionarioComissionamento();
                    query = db.Query();
                    query.Constrain(typeof(ClienteFuncionarioComissionamento));
                    query.Descend("idCliente").Constrain(chave.id);
                    IObjectSet rs_clienteFuncionarioComissionamento = query.Execute();
                    if (rs_clienteFuncionarioComissionamento.Count > 0)
                        clienteFuncionarioComissionamento_db = rs_clienteFuncionarioComissionamento[0] as ClienteFuncionarioComissionamento;

                    comissaoProdutosVendidosFuncionarioTotal = 0;
                    comissaoProdutosEmGarantiaFuncionarioTotal = 0;
                    comissaoMaoDeObraFuncionarioTotal = 0;
                    comissaoMaoDeObraGeralFuncionarioTotal = 0;
                    comissaoMaoDeObraGarantiaFuncionarioTotal = 0;
                    comissaoMaoDeObraGeralGarantiaFuncionarioTotal = 0;
                    comissaoMontanteTotalFuncionarioTotal = 0;
                    comissaoSobreProdutosFuncionarioTotal = 0;
                    comissaoSobreTipoPagamentoFuncionarioTotal = 0;

                    imprimeProdutos = false;
                    imprimeProdutosGarantia = false;
                    imprimeMaoObra = false;
                    imprimeMaoObraGeral = false;
                    imprimeMaoObraGarantia = false;
                    imprimeMaoObraGeralGarantia = false;
                    imprimeMontanteTotal = false;
                    imprimeTipoPagamento = false;
                    imprimeVendas = false;

                    
                    //validações tipos de relatórios para comissões!
                    StringBuilder stringBuilderProdutos = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>PRODUTOS</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderProdutos.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderProdutosGarantia = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>PRODUTOS EM GARANTIA</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderProdutosGarantia.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderMaoObra = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>MÃO DE OBRA</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderMaoObra.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderMaoObraGeral = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>MÃO DE OBRA GERAL</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderMaoObraGeral.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderMaoObraGarantia = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>MÃO DE OBRA GARANTIA</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderMaoObraGarantia.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderMaoObraGeralGarantia = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>MÃO DE OBRA GERAL GARANTIA</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderMaoObraGeralGarantia.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderMontanteTotal = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>MONTANTE TOTAL</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderMontanteTotal.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderTipoPagamento = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>TIPO PAGAMENTO</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderTipoPagamento.Append(GeraTableHeader(null, exibeMovimentacao));
                    
                    StringBuilder stringBuilderVendas = new StringBuilder("<table class='tabela_head'><thead/><tr><th style='color:white; background-color:gray;'>VENDAS</th></tr></table>");
                    if (!exibeMovimentacao) 
                        stringBuilderVendas.Append(GeraTableHeader(null, exibeMovimentacao));

                    
                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<td style='color:white; background-color:black;'>FUNCIONÁRIO(A): {0}</td>", chave.nome);
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");

                    foreach (Mov mov in listaMovPorFuncionario)
                    {
                        query = db.Query();
                        query.Constrain(typeof(MovItem));
                        query.Descend("idMov").Constrain(mov.id);
                        IObjectSet rs_movItem = query.Execute();

                        if (exibeMovimentacao)
                        {
                            stringBuilderProdutos.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderProdutosGarantia.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderMaoObra.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderMaoObraGeral.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderMaoObraGarantia.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderMaoObraGeralGarantia.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderMontanteTotal.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderTipoPagamento.Append(GeraTableHeader(mov, exibeMovimentacao));
                            stringBuilderVendas.Append(GeraTableHeader(mov, exibeMovimentacao));
                        }

                        foreach (MovItem movItem in rs_movItem)
                        {
                            //COMISSÃO PRODUTOS
                            if (clienteFuncionarioComissionamento_db.calculaProdutos)
                            {
                                AdicionaComissaoProduto(stringBuilderProdutos, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                            }

                            //COMISSÃO PRODUTOS EM GARANTIA
                            if (clienteFuncionarioComissionamento_db.calculaProdutosEmGarantia)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(movItem.idItem);
                                IObjectSet rs_item = query.Execute();
                                Item item_db = rs_item[0] as Item;

                                if (item_db.tipo == EItemTipo.servico)
                                {
                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico));
                                    query.Descend("idTransacao").Constrain(mov.idTransacao);
                                    IObjectSet rs_ordemServico = query.Execute();
                                    OrdemServico ordemServico_db = rs_ordemServico[0] as OrdemServico;

                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico_Item));
                                    query.Descend("idOrdemServico").Constrain(ordemServico_db.id);
                                    IObjectSet rs_ordemServicoItem = query.Execute();

                                    foreach (OrdemServico_Item ordemServicoItem in rs_ordemServicoItem)
                                    {
                                        if (ordemServicoItem.tipoItem != "G")
                                            continue;
                                        if (ordemServicoItem.idItem != movItem.idItem)
                                            continue;

                                        AdicionaComissaoProdutoGarantia(stringBuilderProdutosGarantia, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                                    }
                                }
                            }

                            //COMISSAO MÃO DE OBRA
                            if (clienteFuncionarioComissionamento_db.calculaMaoDeObra)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(movItem.idItem);
                                IObjectSet rs_item = query.Execute();
                                Item item_db = rs_item[0] as Item;

                                if (item_db.tipo == EItemTipo.servico)
                                    AdicionaComissaoMaoObra(stringBuilderMaoObra, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                            }

                            //COMISSÃO MAO DE OBRA GERAL
                            if (clienteFuncionarioComissionamento_db.calculaMaoDeObraGeral)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(movItem.idItem);
                                IObjectSet rs_item = query.Execute();
                                Item item_db = rs_item[0] as Item;

                                if (item_db.tipo == EItemTipo.servico)
                                    AdicionaComissaoMaoObraGeral(stringBuilderMaoObraGeral, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                            }

                            //COMISSÃO MÃO DE OBRA GARANTIA
                            if (clienteFuncionarioComissionamento_db.calculaMaoDeObraGarantia)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(movItem.idItem);
                                IObjectSet rs_item = query.Execute();
                                Item item_db = rs_item[0] as Item;

                                if (item_db.tipo == EItemTipo.servico)
                                {
                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico));
                                    query.Descend("idTransacao").Constrain(mov.idTransacao);
                                    IObjectSet rs_ordemServico = query.Execute();
                                    OrdemServico ordemServico_db = rs_ordemServico[0] as OrdemServico;

                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico_Item));
                                    query.Descend("idOrdemServico").Constrain(ordemServico_db.id);
                                    IObjectSet rs_ordemServicoItem = query.Execute();

                                    foreach (OrdemServico_Item ordemServicoItem in rs_ordemServicoItem)
                                    {
                                        if (ordemServicoItem.tipoItem != "G")
                                            continue;
                                        if (ordemServicoItem.idItem != movItem.idItem)
                                            continue;

                                        AdicionaComissaoMaoObraGarantia(stringBuilderMaoObraGarantia, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                                    }
                                }
                            }

                            //COMISSÃO MÃO DE OBRA GERAL GARANTIA
                            if (clienteFuncionarioComissionamento_db.calculaMaoDeObraGeralGarantia)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Item));
                                query.Descend("id").Constrain(movItem.idItem);
                                IObjectSet rs_item = query.Execute();
                                Item item_db = rs_item[0] as Item;

                                if (item_db.tipo == EItemTipo.servico)
                                {
                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico));
                                    query.Descend("idTransacao").Constrain(mov.idTransacao);
                                    IObjectSet rs_ordemServico = query.Execute();
                                    OrdemServico ordemServico_db = rs_ordemServico[0] as OrdemServico;

                                    query = db.Query();
                                    query.Constrain(typeof(OrdemServico_Item));
                                    query.Descend("idOrdemServico").Constrain(ordemServico_db.id);
                                    IObjectSet rs_ordemServicoItem = query.Execute();

                                    foreach (OrdemServico_Item ordemServicoItem in rs_ordemServicoItem)
                                    {
                                        if (ordemServicoItem.tipoItem != "G")
                                            continue;
                                        if (ordemServicoItem.idItem != movItem.idItem)
                                            continue;

                                        AdicionaComissaoMaoObraGeralGarantia(stringBuilderMaoObraGarantia, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                                    }
                                }
                            }

                            //COMISSÃO MONTANTE TOTAL
                            if (clienteFuncionarioComissionamento_db.calculaMontanteTotal)
                            {
                                AdicionaComissaoMontanteTotal(stringBuilderMontanteTotal, db, mov, clienteFuncionarioComissionamento_db, movItem, exibeMovimentacao);
                            }

                            //COMISSÃO TIPO PAGAMENTO
                            bool imprimeEste = false;
                            List<Finan_TipoPagamento> listaFinanTipoPagamento = new List<Finan_TipoPagamento>();

                            query = db.Query();
                            query.Constrain(typeof(Cx_Lancamento));
                            query.Descend("idTransacao").Constrain(mov.idTransacao);
                            IObjectSet rs_cxLancamento = query.Execute();

                            foreach (Cx_Lancamento cxLancamento in rs_cxLancamento)
                            {
                                query = db.Query();
                                query.Constrain(typeof(Finan_TipoPagamento));
                                query.Descend("id").Constrain(cxLancamento.idTipoPagamento);
                                IObjectSet rs_finanTipoPagamento = query.Execute();

                                if (rs_finanTipoPagamento.Count == 0)
                                    continue;

                                Finan_TipoPagamento finanTipoPagamento = rs_finanTipoPagamento[0] as Finan_TipoPagamento;

                                if (finanTipoPagamento.pctComissao == 0)
                                    continue;

                                if (!listaFinanTipoPagamento.Contains(finanTipoPagamento))
                                    listaFinanTipoPagamento.Add(finanTipoPagamento);

                                imprimeEste = true;
                            }
                            if (imprimeEste)
                                AdicionaComissaoTipoPagamento(stringBuilderTipoPagamento, db, mov, chave, movItem, listaFinanTipoPagamento, exibeMovimentacao);
                            
                            //COMISSÃO VENDAS
                            AdicionaComissaoVenda(stringBuilderVendas, db, mov, chave, movItem, exibeMovimentacao);

                        }

                        if (exibeMovimentacao && imprimeProdutos)
                        {
                            stringBuilderProdutos.Append("</tbody>");
                            stringBuilderProdutos.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeProdutosGarantia)
                        {
                            stringBuilderProdutosGarantia.Append("</tbody>");
                            stringBuilderProdutosGarantia.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeMaoObra)
                        {
                            stringBuilderMaoObra.Append("</tbody>");
                            stringBuilderMaoObra.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeMaoObraGeral)
                        {
                            stringBuilderMaoObraGeral.Append("</tbody>");
                            stringBuilderMaoObraGeral.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeMaoObraGeralGarantia)
                        {
                            stringBuilderMaoObraGeralGarantia.Append("</tbody>");
                            stringBuilderMaoObraGeralGarantia.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeMontanteTotal)
                        {
                            stringBuilderMontanteTotal.Append("</tbody>");
                            stringBuilderMontanteTotal.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeTipoPagamento)
                        {
                            stringBuilderTipoPagamento.Append("</tbody>");
                            stringBuilderTipoPagamento.Append("</table>");
                        }
                        if (exibeMovimentacao && imprimeVendas)
                        {
                            stringBuilderVendas.Append("</tbody>");
                            stringBuilderVendas.Append("</table>");
                        }
                    }

                    if (imprimeProdutos)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderProdutos.Append("</tbody>");
                            stringBuilderProdutos.Append("</table>");
                        }

                        stringBuilderProdutos.Append("<table class='tabela_head'>");
                        stringBuilderProdutos.Append("<thead/>");
                        stringBuilderProdutos.Append("<tr>");
                        stringBuilderProdutos.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Produtos: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoSobreProdutosFuncionarioTotal), true));
                        stringBuilderProdutos.Append("</tr>");
                        stringBuilderProdutos.Append("</table>");

                        stringBuilderRel.Append(stringBuilderProdutos);
                    }
                    if (imprimeProdutosGarantia)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderProdutosGarantia.Append("</tbody>");
                            stringBuilderProdutosGarantia.Append("</table>");
                        }

                        stringBuilderProdutosGarantia.Append("<table class='tabela_head'>");
                        stringBuilderProdutosGarantia.Append("<thead/>");
                        stringBuilderProdutosGarantia.Append("<tr>");
                        stringBuilderProdutosGarantia.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Produtos em Garantia: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoProdutosEmGarantiaFuncionarioTotal), true));
                        stringBuilderProdutosGarantia.Append("</tr>");
                        stringBuilderProdutosGarantia.Append("</table>");

                        stringBuilderRel.Append(stringBuilderProdutosGarantia);
                    }
                    if (imprimeMaoObra)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderMaoObra.Append("</tbody>");
                            stringBuilderMaoObra.Append("</table>");
                        }

                        stringBuilderMaoObra.Append("<table class='tabela_head'>");
                        stringBuilderMaoObra.Append("<thead/>");
                        stringBuilderMaoObra.Append("<tr>");
                        stringBuilderMaoObra.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Mão de Obra: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoMaoDeObraFuncionarioTotal), true));
                        stringBuilderMaoObra.Append("</tr>");
                        stringBuilderMaoObra.Append("</table>");

                        stringBuilderRel.Append(stringBuilderMaoObra);
                    }
                    if (imprimeMaoObraGeral)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderMaoObraGeral.Append("</tbody>");
                            stringBuilderMaoObraGeral.Append("</table>");
                        }

                        stringBuilderMaoObraGeral.Append("<table class='tabela_head'>");
                        stringBuilderMaoObraGeral.Append("<thead/>");
                        stringBuilderMaoObraGeral.Append("<tr>");
                        stringBuilderMaoObraGeral.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Mão de Obra Geral: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoMaoDeObraGeralFuncionarioTotal), true));
                        stringBuilderMaoObraGeral.Append("</tr>");
                        stringBuilderMaoObraGeral.Append("</table>");

                        stringBuilderRel.Append(stringBuilderMaoObraGeral);
                    }
                    if (imprimeMaoObraGarantia)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderMaoObraGarantia.Append("</tbody>");
                            stringBuilderMaoObraGarantia.Append("</table>");
                        }

                        stringBuilderMaoObraGarantia.Append("<table class='tabela_head'>");
                        stringBuilderMaoObraGarantia.Append("<thead/>");
                        stringBuilderMaoObraGarantia.Append("<tr>");
                        stringBuilderMaoObraGarantia.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Mão de Obra em Garantia: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoMaoDeObraGarantiaFuncionarioTotal), true));
                        stringBuilderMaoObraGarantia.Append("</tr>");
                        stringBuilderMaoObraGarantia.Append("</table>");

                        stringBuilderRel.Append(stringBuilderMaoObraGarantia);
                    }
                    if (imprimeMaoObraGeralGarantia)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderMaoObraGeralGarantia.Append("</tbody>");
                            stringBuilderMaoObraGeralGarantia.Append("</table>");
                        }

                        stringBuilderMaoObraGeralGarantia.Append("<table class='tabela_head'>");
                        stringBuilderMaoObraGeralGarantia.Append("<thead/>");
                        stringBuilderMaoObraGeralGarantia.Append("<tr>");
                        stringBuilderMaoObraGeralGarantia.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Mão de Obra em Garantia: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoMaoDeObraGeralGarantiaFuncionarioTotal), true));
                        stringBuilderMaoObraGeralGarantia.Append("</tr>");
                        stringBuilderMaoObraGeralGarantia.Append("</table>");

                        stringBuilderRel.Append(stringBuilderMaoObraGeralGarantia);
                    }
                    if (imprimeMontanteTotal)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderMontanteTotal.Append("</tbody>");
                            stringBuilderMontanteTotal.Append("</table>");
                        }

                        stringBuilderMontanteTotal.Append("<table class='tabela_head'>");
                        stringBuilderMontanteTotal.Append("<thead/>");
                        stringBuilderMontanteTotal.Append("<tr>");
                        stringBuilderMontanteTotal.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Montante Total: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoMontanteTotalFuncionarioTotal), true));
                        stringBuilderMontanteTotal.Append("</tr>");
                        stringBuilderMontanteTotal.Append("</table>");

                        stringBuilderRel.Append(stringBuilderMontanteTotal);
                    }
                    if (imprimeTipoPagamento)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderTipoPagamento.Append("</tbody>");
                            stringBuilderTipoPagamento.Append("</table>");
                        }

                        stringBuilderTipoPagamento.Append("<table class='tabela_head'>");
                        stringBuilderTipoPagamento.Append("<thead/>");
                        stringBuilderTipoPagamento.Append("<tr>");
                        stringBuilderTipoPagamento.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Forma de Pagamento: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoSobreTipoPagamentoFuncionarioTotal), true));
                        stringBuilderTipoPagamento.Append("</tr>");
                        stringBuilderTipoPagamento.Append("</table>");

                        stringBuilderRel.Append(stringBuilderTipoPagamento);
                    }
                    if (imprimeVendas)
                    {
                        if (!exibeMovimentacao)
                        {
                            stringBuilderVendas.Append("</tbody>");
                            stringBuilderVendas.Append("</table>");
                        }

                        stringBuilderVendas.Append("<table class='tabela_head'>");
                        stringBuilderVendas.Append("<thead/>");
                        stringBuilderVendas.Append("<tr>");
                        stringBuilderVendas.AppendFormat("<th style='background-color:#87CEEB;'>Total Sobre Vendas: {0}</th>", Utils.formatMoney(Convert.ToDecimal(comissaoSobreProdutosFuncionarioTotal), true));
                        stringBuilderVendas.Append("</tr>");
                        stringBuilderVendas.Append("</table>");

                        stringBuilderRel.Append(stringBuilderVendas);
                    }

                    stringBuilderRel.Append("<table class='tabela_head'>");
                    stringBuilderRel.Append("<thead/>");
                    stringBuilderRel.Append("<tr>");
                    stringBuilderRel.AppendFormat("<th style='background-color:#EEDD82;'>Comissão Total Funcionário: {0}</th>", Utils.formatMoney(Convert.ToDecimal(
                        comissaoMontanteTotalFuncionarioTotal + comissaoMaoDeObraFuncionarioTotal + comissaoMaoDeObraGarantiaFuncionarioTotal +
                    comissaoProdutosEmGarantiaFuncionarioTotal + comissaoProdutosVendidosFuncionarioTotal + comissaoSobreProdutosFuncionarioTotal), true));
                    stringBuilderRel.Append("</tr>");
                    stringBuilderRel.Append("</table>");
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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "COMISSIONAMENTO");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");
        }

        private StringBuilder GeraTableHeader(Mov mov, bool exibeMovimentacao)
        {
            StringBuilder stringBuilderRetorno = new StringBuilder();

            if (exibeMovimentacao)
            {
                stringBuilderRetorno.Append("<table class='tabela_mov'>");
                stringBuilderRetorno.Append("<thead/>");
                stringBuilderRetorno.Append("<tr>");
                stringBuilderRetorno.AppendFormat("<th style='font-weight:bold;'>Movimentação: {0} / Data: {1}</th>", mov.id, mov.dthrMovEmissao);
                stringBuilderRetorno.Append("</tr>");
                stringBuilderRetorno.Append("</table>");
            }

            stringBuilderRetorno.Append("<table class='tabela_mov'>");
            stringBuilderRetorno.Append("<thead>");
            stringBuilderRetorno.Append("<th scoupe='colune' style='width:70%;'>Item</th>");
            stringBuilderRetorno.Append("<th scoupe='colune' style='width:10%;'>Valor</th>");
            stringBuilderRetorno.Append("<th scoupe='colune' style='width:10%;'>Comissão(%)</th>");
            stringBuilderRetorno.Append("<th scoupe='colune' style='width:10%;'>Comissão(R$)</th>");
            stringBuilderRetorno.Append("</thead>");
            stringBuilderRetorno.Append("<tbody>");

            return stringBuilderRetorno;
        }

        private void AdicionaComissaoProduto(StringBuilder stringBuilderProdutos, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderProdutos.Append("<tr>");
            stringBuilderProdutos.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderProdutos.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderProdutos.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoProdutos), false));
            stringBuilderProdutos.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoProdutos) / 100), true));
            stringBuilderProdutos.Append("</tr>");
            comissaoProdutosVendidosFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoProdutos) / 100;

            imprimeProdutos = true;
        }

        private void AdicionaComissaoProdutoGarantia(StringBuilder stringBuilderProdutoGarantia, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderProdutoGarantia.Append("<tr>");
            stringBuilderProdutoGarantia.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderProdutoGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderProdutoGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoProdutosEmGarantia), false));
            stringBuilderProdutoGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoProdutosEmGarantia) / 100), true));
            stringBuilderProdutoGarantia.Append("</tr>");
            comissaoProdutosEmGarantiaFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoProdutosEmGarantia) / 100;

            imprimeProdutosGarantia = true;
        }

        private void AdicionaComissaoMaoObra(StringBuilder stringBuilderMaoObra, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderMaoObra.Append("<tr>");
            stringBuilderMaoObra.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderMaoObra.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderMaoObra.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoMaoDeObra), false));
            stringBuilderMaoObra.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObra) / 100), true));
            stringBuilderMaoObra.Append("</tr>");
            comissaoMaoDeObraFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObra) / 100;

            imprimeMaoObra = true;
        }

        private void AdicionaComissaoMaoObraGeral(StringBuilder stringBuilderMaoObraGeral, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderMaoObraGeral.Append("<tr>");
            stringBuilderMaoObraGeral.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderMaoObraGeral.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderMaoObraGeral.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoMaoDeObraGeral), false));
            stringBuilderMaoObraGeral.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGeral) / 100), true));
            stringBuilderMaoObraGeral.Append("</tr>");
            comissaoMaoDeObraGeralFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGeral) / 100;

            imprimeMaoObraGeral = true;
        }

        private void AdicionaComissaoMaoObraGarantia(StringBuilder stringBuilderMaoObraGarantia, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderMaoObraGarantia.Append("<tr>");
            stringBuilderMaoObraGarantia.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderMaoObraGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderMaoObraGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoMaoDeObraGarantia), false));
            stringBuilderMaoObraGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGarantia) / 100), true));
            stringBuilderMaoObraGarantia.Append("</tr>");
            comissaoMaoDeObraGarantiaFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGarantia) / 100;

            imprimeMaoObraGeral = true;
        }

        private void AdicionaComissaoMaoObraGeralGarantia(StringBuilder stringBuilderMaoObraGeralGarantia, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderMaoObraGeralGarantia.Append("<tr>");
            stringBuilderMaoObraGeralGarantia.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderMaoObraGeralGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderMaoObraGeralGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoMaoDeObraGeralGarantia), false));
            stringBuilderMaoObraGeralGarantia.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGeralGarantia) / 100), true));
            stringBuilderMaoObraGeralGarantia.Append("</tr>");
            comissaoMaoDeObraGeralGarantiaFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMaoDeObraGeralGarantia) / 100;

            imprimeMaoObraGeralGarantia = true;
        }

        private void AdicionaComissaoMontanteTotal(StringBuilder stringBuilderMontanteTotal, IObjectContainer db, Mov mov, ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, MovItem movItem, bool exibeMovimentacao)
        {
            stringBuilderMontanteTotal.Append("<tr>");
            stringBuilderMontanteTotal.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderMontanteTotal.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderMontanteTotal.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(clienteFuncionarioComissionamento.comissaoMontanteTotal), false));
            stringBuilderMontanteTotal.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMontanteTotal) / 100), true));
            stringBuilderMontanteTotal.Append("</tr>");
            comissaoMontanteTotalFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * clienteFuncionarioComissionamento.comissaoMontanteTotal) / 100;

            imprimeMontanteTotal = true;
        }

        private void AdicionaComissaoTipoPagamento(StringBuilder stringBuilderTipoPagamento, IObjectContainer db, Mov mov, Cliente vendedor, MovItem movItem, List<Finan_TipoPagamento> listaFinanTipoPagamento, bool exibeMovimentacao)
        {
            foreach (Finan_TipoPagamento finanTipoPagamento in listaFinanTipoPagamento)
            {
                stringBuilderTipoPagamento.Append("<tr>");
                stringBuilderTipoPagamento.AppendFormat("<td>{0}</td>", movItem.item_nome);
                stringBuilderTipoPagamento.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
                stringBuilderTipoPagamento.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(finanTipoPagamento.pctComissao), false));
                stringBuilderTipoPagamento.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * finanTipoPagamento.pctComissao) / 100), true));
                stringBuilderTipoPagamento.Append("</tr>");
                comissaoSobreTipoPagamentoFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * finanTipoPagamento.pctComissao) / 100;
            }

            imprimeTipoPagamento = true;
        }

        private void AdicionaComissaoVenda(StringBuilder stringBuilderComissaoVenda, IObjectContainer db, Mov mov, Cliente vendedor, MovItem movItem, bool exibeMovimentacao)
        {
            if (vendedor.comissaoProdutos == 0)
                return;           
            //if (movItem.pctComissaoPreco == 0)
            //return;

            stringBuilderComissaoVenda.Append("<tr>");
            stringBuilderComissaoVenda.AppendFormat("<td>{0}</td>", movItem.item_nome);
            stringBuilderComissaoVenda.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
            stringBuilderComissaoVenda.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.pctComissaoPreco), false));
            stringBuilderComissaoVenda.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal((movItem.vlrUnitVendaFinalQtd * movItem.pctComissaoPreco) / 100), true));
            stringBuilderComissaoVenda.Append("</tr>");
            comissaoSobreProdutosFuncionarioTotal += (movItem.vlrUnitVendaFinalQtd * vendedor.comissaoProdutos) / 100;

            imprimeVendas = true;
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
