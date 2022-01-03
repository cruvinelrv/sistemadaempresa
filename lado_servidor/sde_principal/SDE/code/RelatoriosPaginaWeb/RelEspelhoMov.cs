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
using SDE.Entidade;
using SDE.Enumerador;
using System.Collections.Generic;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Collections;

namespace SDE.RelatoriosPaginaWeb
{
    public class RelEspelhoMov
    {
        public StringBuilder GeraRelEspelhoMov(int idCorp, int idEmp, String dtInicial, String dtFinal, int idCliente, int idVendedor, int idItem, int idMov,
            bool entrada_compra, bool saida_venda, bool outros_orcamento, bool entrada_cancel, bool saida_cancel, bool ambos_ajuste_estoque, bool ambos_balan, bool outros_reserva, bool outros_pedido,
            bool sem_impressao, bool nfe_produto, bool nf_formulario, bool cupom_fiscal, bool orcamento, bool reserva, bool pedido)
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

            List<EMovTipo> listaMovTipoFiltro = ConstroiListMovTipoFiltro(entrada_compra, saida_venda, outros_orcamento, entrada_cancel,
                saida_cancel, ambos_ajuste_estoque, ambos_balan, outros_reserva, outros_pedido);
            List<EMovImpressao> listaMovIMpressaoFiltro = ConstroiListMovImpressaoFiltro(sem_impressao, nfe_produto,
                nf_formulario, cupom_fiscal, orcamento, reserva, pedido);

            IList<Mov> rs_mov = db.Query<Mov>(
                delegate(Mov mov)
                {
                    return (Utils.StringToDateTime(mov.dthrMovEmissao) >= Utils.StringToDateTime(dtInicial) &&
                        Utils.StringToDateTime(mov.dthrMovEmissao) <= Utils.StringToDateTime(dtFinal));
                }
            );
            List<Mov> listaMov = new List<Mov>(rs_mov);


            try
            {
                GeraCabecalho(stringBuilderRel, db, clienteEmpresa_db, rs_clienteEmpresaEnderecos, rs_clienteEmpresaContatos, dtInicial, dtFinal,
                    idCliente, idVendedor, idItem, idMov, listaMovTipoFiltro, listaMovIMpressaoFiltro);

                foreach (Mov mov in listaMov.OrderBy(item => Utils.StringToDateTime(item.dthrMovEmissao)))
                {
                    if (listaMovTipoFiltro.Contains(mov.tipo) && listaMovIMpressaoFiltro.Contains(mov.impressao))
                    {
                        if (mov.idMovCanceladora != 0)
                            continue;
                        if (idCliente != 0 && idCliente != mov.idCliente)
                            continue;
                        if (idVendedor != 0 && idVendedor != mov.idClienteFuncionarioVendedor)
                            continue;
                        if (idMov != 0 && idMov != mov.id)
                            continue;

                        query = db.Query();
                        query.Constrain(typeof(MovItem));
                        query.Descend("idMov").Constrain(mov.id);
                        query.Descend("item_nome").OrderAscending();
                        IObjectSet rs_movItem = query.Execute();
                        if (idItem != 0)
                        {
                            bool itemRelacionado = false;
                            foreach (MovItem movItem in rs_movItem)
                            {
                                if (movItem.idItem != idItem)
                                    continue;
                                itemRelacionado = true;
                            }
                            if (!itemRelacionado)
                                continue;
                        }

                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead>");
                        stringBuilderRel.Append("<tr style='background-color:black; font-weight:bold; color:white;'>");
                        stringBuilderRel.AppendFormat("<td style='width:10%;'>Cód.: {0}</td>", mov.id);
                        stringBuilderRel.AppendFormat("<td style='width:30%;'>Data/Hora: {0}</td>", mov.dthrMovEmissao);
                        stringBuilderRel.AppendFormat("<td style='width:30%;'>Tipo Movimentação: {0}</td>", StringMovTipo(mov.tipo));
                        stringBuilderRel.AppendFormat("<td style='width:30%;'>Tipo Impressão: {0}</td>", StringMovImpressao(mov.impressao));
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("</thead>");
                        stringBuilderRel.Append("</table>");

                        stringBuilderRel.Append("<table class='tabela_mov'>");
                        stringBuilderRel.Append("<thead>");
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("<th scope='col' style='width:5%;'>Cód.</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:40%;'>Item</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:5%;'>Quantidade</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:10%;'>Grade/Ident.</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:10%;'>Vlr.Unit.</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:10%;'>Vlr. Bruto</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:10%;'>Vlr Desc.</th>");
                        stringBuilderRel.Append("<th scope='col' style='width:10%;'>Vlr. Final</th>");
                        stringBuilderRel.Append("</tr>");
                        stringBuilderRel.Append("</thead>");
                        stringBuilderRel.Append("<tbody>");

                        Double valorTotalMov = 0;
                        foreach (MovItem movItem in rs_movItem)
                        {
                            if (idItem != 0 && idItem != movItem.idItem)
                                continue;

                            stringBuilderRel.Append("<tr>");
                            stringBuilderRel.AppendFormat("<td style='width:20;'>{0}</td>", movItem.idItem);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", movItem.item_nome);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", movItem.qtd);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", movItem.estoque_identificador);
                            stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaInicial), true));
                            stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaInicial * movItem.qtd), true));
                            stringBuilderRel.AppendFormat("<td>{0}</td>", Utils.formatMoney(Convert.ToDecimal(movItem.vlrDesc), true));
                            stringBuilderRel.AppendFormat("<td>{0}</td>", (mov.tipo == EMovTipo.entrada_compra || mov.tipo == EMovTipo.entrada_cancel) ? Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaInicial * movItem.qtd), true) : Utils.formatMoney(Convert.ToDecimal(movItem.vlrUnitVendaFinalQtd), true));
                            stringBuilderRel.Append("</tr>");

                            valorTotalMov += (mov.tipo == EMovTipo.entrada_compra || mov.tipo == EMovTipo.entrada_cancel) ? movItem.vlrUnitVendaInicial * movItem.qtd : movItem.vlrUnitVendaFinalQtd;
                        }

                        stringBuilderRel.Append("</tbody>");
                        stringBuilderRel.Append("</table>");

                        stringBuilderRel.Append("<table class='tabela_head'>");
                        stringBuilderRel.Append("<thead/>");
                        stringBuilderRel.Append("<tr>");
                        stringBuilderRel.AppendFormat("<td style='background-color:#87CEEB;'>Total da Movimentação: {0}</td>", Utils.formatMoney(Convert.ToDecimal(valorTotalMov), true));
                        stringBuilderRel.Append("</tr>");
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

        private void GeraCabecalho(StringBuilder stringBuilderRel, IObjectContainer db, Cliente clienteEmpresa_db, IObjectSet rs_clienteEmpresaEnderecos, IObjectSet rs_clienteEmpresaContatos, String dtInicial, String dtFinal,
            int idCliente, int idVendedor, int idItem, int idMov, List<EMovTipo> listaMovTipoFiltro, List<EMovImpressao> listaMovIMpressaoFiltro)
        {
            IQuery query;

            Cliente cliente_db = null;
            if (idCliente > 0)
            {
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(idCliente);
                IObjectSet rs_cliente = query.Execute();
                cliente_db = rs_cliente[0] as Cliente;
            }

            Cliente vendedor_db = null;
            if (idVendedor > 0)
            {
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(idVendedor);
                IObjectSet rs_vendedor = query.Execute();
                vendedor_db = rs_vendedor[0] as Cliente;
            }

            Item item_db = null;
            if (idItem > 0)
            {
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(idItem);
                IObjectSet rs_item = query.Execute();
                item_db = rs_item[0] as Item;
            }

            Mov mov_db = null;
            if (idMov > 0)
            {
                query = db.Query();
                query.Constrain(typeof(Mov));
                query.Descend("id").Constrain(idMov);
                IObjectSet rs_mov = query.Execute();
                mov_db = rs_mov[0] as Mov;
            }

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
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>{0}</th>", "ESPELHO DE MOVIMENTAÇÕES");
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th style='fontsize: 12px;'>PERÍODO: {0} A {1}</th>", dtInicial, dtFinal);
            stringBuilderRel.Append("</tr>");
            stringBuilderRel.Append("</table>");

            stringBuilderRel.Append("<table class='tabela_head'>");
            stringBuilderRel.Append("<thead>");

            if (cliente_db != null)
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Cliente/Fornecedor: {0}</th>", cliente_db.nome);
                stringBuilderRel.Append("</tr>");
            }

            if (vendedor_db != null)
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Vendedor: {0}</th>", vendedor_db.nome);
                stringBuilderRel.Append("</tr>");
            }

            if (item_db != null)
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Produto: {0}</th>", item_db.nome);
                stringBuilderRel.Append("</tr>");
            }

            if (mov_db != null)
            {
                stringBuilderRel.Append("<tr>");
                stringBuilderRel.AppendFormat("<th>Movimentação: {0} / {1} / {2}</th>", mov_db.cliente_nome, mov_db.dthrMovEmissao, Utils.formatMoney(Convert.ToDecimal(mov_db.vlrTotal), true));
                stringBuilderRel.Append("</tr>");
            }

            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th>Tipo(s) Movimentação: {0}", GeraStringMovTipoFiltro(listaMovTipoFiltro));
            stringBuilderRel.Append("</tr>");

            stringBuilderRel.Append("<tr>");
            stringBuilderRel.AppendFormat("<th>Tipo(s) Impressão: {0}", GeraStringMovImpressaoFiltro(listaMovIMpressaoFiltro));
            stringBuilderRel.Append("</tr>");

            stringBuilderRel.Append("</thead>");
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

        private String GeraStringMovTipoFiltro(List<EMovTipo> listaMovTipoFiltro)
        {
            String strRetorno = "";
            int contador = 0;

            foreach (EMovTipo eMovTipo in listaMovTipoFiltro)
            {
                contador++;
                strRetorno += StringMovTipo(eMovTipo);

                strRetorno += (contador < listaMovTipoFiltro.Count) ? ", " : ".";
            }

            return strRetorno;
        }

        private String GeraStringMovImpressaoFiltro(List<EMovImpressao> listaMovIMpressaoFiltro)
        {
            String strRetorno = "";
            int contador = 0;

            foreach (EMovImpressao eMovImpressao in listaMovIMpressaoFiltro)
            {
                contador++;
                strRetorno += StringMovImpressao(eMovImpressao);

                strRetorno += (contador < listaMovIMpressaoFiltro.Count) ? ", " : ".";
            }

            return strRetorno;
        }

        private List<EMovTipo> ConstroiListMovTipoFiltro(bool entrada_compra, bool saida_venda, bool outros_orcamento, bool entrada_cancel, bool saida_cancel, bool ambos_ajuste_estoque, bool ambos_balan, bool outros_reserva, bool outros_pedido)
        {
            List<EMovTipo> listaRetorno = new List<EMovTipo>();

            if (entrada_compra == true) listaRetorno.Add(EMovTipo.entrada_compra);
            if (saida_venda == true) listaRetorno.Add(EMovTipo.saida_venda);
            if (outros_orcamento == true) listaRetorno.Add(EMovTipo.outros_orcamento);
            if (entrada_cancel == true) listaRetorno.Add(EMovTipo.entrada_cancel);
            if (saida_cancel == true) listaRetorno.Add(EMovTipo.saida_cancel);
            if (ambos_ajuste_estoque == true) listaRetorno.Add(EMovTipo.ambos_ajuste_estoque);
            if (ambos_balan == true) listaRetorno.Add(EMovTipo.ambos_balan);
            if (outros_reserva == true) listaRetorno.Add(EMovTipo.outros_reserva);
            if (outros_pedido == true) listaRetorno.Add(EMovTipo.outros_pedido);

            return listaRetorno;
        }

        private List<EMovImpressao> ConstroiListMovImpressaoFiltro(bool sem_impressao, bool nfe_produto, bool nf_formulario, bool cupom_fiscal, bool orcamento, bool reserva, bool pedido)
        {
            List<EMovImpressao> listaRetorno = new List<EMovImpressao>();

            if (sem_impressao == true) listaRetorno.Add(EMovImpressao.sem_impressao);
            if (nfe_produto == true) listaRetorno.Add(EMovImpressao.nfe_produto);
            if (nf_formulario == true) listaRetorno.Add(EMovImpressao.nf_formulario);
            if (cupom_fiscal == true) listaRetorno.Add(EMovImpressao.cupom_fiscal);
            if (orcamento == true) listaRetorno.Add(EMovImpressao.orcamento);
            if (reserva == true) listaRetorno.Add(EMovImpressao.reserva);
            if (pedido == true) listaRetorno.Add(EMovImpressao.pedido);

            return listaRetorno;
        }

        private String StringMovTipo(EMovTipo eMvovTipo)
        {
            String strRetorno = "";

            if (eMvovTipo == EMovTipo.entrada_compra) strRetorno = "Compra";
            if (eMvovTipo == EMovTipo.saida_venda) strRetorno = "Venda";
            if (eMvovTipo == EMovTipo.outros_orcamento) strRetorno = "Orçamento";
            if (eMvovTipo == EMovTipo.entrada_cancel) strRetorno = "Compra Cencelada";
            if (eMvovTipo == EMovTipo.saida_cancel) strRetorno = "Venda Cancelada";
            if (eMvovTipo == EMovTipo.ambos_ajuste_estoque) strRetorno = "Ajuste de Estoque";
            if (eMvovTipo == EMovTipo.ambos_balan) strRetorno = "Balanço";
            if (eMvovTipo == EMovTipo.outros_reserva) strRetorno = "Reserva";
            if (eMvovTipo == EMovTipo.outros_pedido) strRetorno = "Pedido";

            return strRetorno;
        }

        private String StringMovImpressao(EMovImpressao eMovImpressao)
        {
            String strRetorno = "";

            if (eMovImpressao == EMovImpressao.sem_impressao) strRetorno = "Sem Impressão";
            if (eMovImpressao == EMovImpressao.nfe_produto) strRetorno = "NFe";
            if (eMovImpressao == EMovImpressao.nf_formulario) strRetorno = "NF";
            if (eMovImpressao == EMovImpressao.cupom_fiscal) strRetorno = "Cupom Fiscal";
            if (eMovImpressao == EMovImpressao.orcamento) strRetorno = "Orçamento";
            if (eMovImpressao == EMovImpressao.reserva) strRetorno = "Reserva";
            if (eMovImpressao == EMovImpressao.pedido) strRetorno = "Pedido";

            return strRetorno;
        }
    }
}
