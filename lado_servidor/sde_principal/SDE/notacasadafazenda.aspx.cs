using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.Entidade;
using System.Globalization;

namespace SDE.code
{
    public partial class notacasadafazenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idCorp = 6;
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IObjectContainer db0 = AppFacade.get.conexaoBanco.get(0);

            IQuery query;

            //busca mov
            TempMov mov = new TempMov();
            query = db.Query();
            query.Constrain(typeof(TempMov));
            IObjectSet rs_mov = query.Execute();
            if (rs_mov.Count == 1)
                mov = rs_mov[0] as TempMov;
            else
                throw new Exception(string.Format("Existem {0} TempMov no banco de dados", rs_mov.Count));

            //busca movnfe
            TempMovNFE mov_nfe = new TempMovNFE();
            query = db.Query();
            query.Constrain(typeof(TempMovNFE));
            IObjectSet rs_mov_nfe = query.Execute();
            if (rs_mov_nfe.Count == 1)
                mov_nfe = rs_mov_nfe[0] as TempMovNFE;
            else
                throw new Exception(string.Format("Existem {0} TempMovNFE no banco de dados", rs_mov_nfe.Count));

            //busca movitem
            List<TempMovItem> mov_item_lista = new List<TempMovItem>();
            query = db.Query();
            query.Constrain(typeof(TempMovItem));
            foreach (TempMovItem mi in query.Execute())
                mov_item_lista.Add(mi);

            //busca cfop
            CFOP cfop = new CFOP();
            query = db0.Query();
            query.Constrain(typeof(CFOP));
            query.Descend("codigo").Constrain(mov.cfop);
            IObjectSet rs_cfop = query.Execute();
            if (rs_cfop.Count > 0)
                cfop = rs_cfop[0] as CFOP;
            else
                throw new Exception(string.Format("Não foi possível buscar o CFOP da venda\nCFOP: {0}", mov.cfop));

            //busca cliente
            Cliente cliente = new Cliente();
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(mov.idCliente);
            IObjectSet rs_cliente = query.Execute();
            if (rs_cliente.Count > 0)
                cliente = rs_cliente[0] as Cliente;
            else
                throw new Exception(string.Format("Não foi possível buscar o cliente\nID: {0}", mov.idCliente));

            //busca endereço cliente
            ClienteEndereco cliente_endereco = new ClienteEndereco();
            query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("id").Constrain(mov.idClienteEndereco);
            IObjectSet rs_cliente_endereco = query.Execute();
            if (rs_cliente_endereco.Count > 0)
                cliente_endereco = rs_cliente_endereco[0] as ClienteEndereco;
            else
                throw new Exception(string.Format("Não foi possível buscar o endereço do cliente\nID: {0}", mov.idClienteEndereco));

            //busca telefone(contato) cliente
            ClienteContato cliente_contato = new ClienteContato();
            query = db.Query();
            query.Constrain(typeof(ClienteContato));
            query.Descend("idCliente").Constrain(cliente.id);
            IObjectSet rs_cliente_contato = query.Execute();
            foreach (ClienteContato ce in rs_cliente_contato)
                if (ce.tipo == SDE.Enumerador.EContatoTipo.fone_fixo)
                    cliente_contato = ce;

            //busca transportador
            Cliente transportador = new Cliente();
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(mov_nfe.idClienteTransp);
            IObjectSet rs_transportador = query.Execute();
            if (rs_transportador.Count > 0)
                transportador = rs_transportador[0] as Cliente;
            else
                throw new Exception(string.Format("Não foi possível buscar o transportador\nID: {0}", mov_nfe.idClienteTransp));

            //busca endereço transportador
            ClienteEndereco transportador_endereco = null;
            query = db.Query();
            query.Constrain(typeof(ClienteEndereco));
            query.Descend("idCliente").Constrain(transportador.id);
            IObjectSet rs_transportador_endereco = query.Execute();
            if (rs_transportador_endereco.Count > 0)
                transportador_endereco = rs_transportador_endereco[0] as ClienteEndereco;
            //else
            //    throw new Exception(string.Format("Não foi possível buscar o endereço do transportador\nID Cliente: {0}", transportador.id));

            //busca veiculo transportador
            ClienteVeiculo transportador_veiculo = null;
            query = db.Query();
            query.Constrain(typeof(ClienteVeiculo));
            query.Descend("id").Constrain(mov_nfe.idVeiculo);
            IObjectSet rs_transportador_veiculo = query.Execute();
            if (rs_transportador_veiculo.Count > 0)
                transportador_veiculo = rs_transportador_veiculo[0] as ClienteVeiculo;

            //preenche bloco1
            pNumeroNf.InnerText = mov_nfe.numeroNota.ToString();
            pSaida.InnerText = (mov.resumo == SDE.Enumerador.EMovResumo.saida) ? "X" : "";
            pEntrada.InnerText = (mov.resumo == SDE.Enumerador.EMovResumo.entrada) ? "X" : "";
            pNaturezaOp.InnerText = cfop.descricao.Substring(0, 26);
            pCFOP.InnerText = cfop.codigo;
            //pInscrEstSubTrib.InnerText = ????

            pNomeRazSocialDestRemet.InnerText = cliente.nome;
            pCnpjCpfDestRemet.InnerText = cliente.cpf_cnpj;
            pEnderecoDestRemet.InnerText = cliente_endereco.logradouro + " " + cliente_endereco.numero + " " + cliente_endereco.complemento;
            pBairroDistritoDestRemet.InnerText = cliente_endereco.bairro;
            pCepDestRemet.InnerText = cliente_endereco.cep;
            pMunicipioDestRemet.InnerText = cliente_endereco.cidade;
            pFoneFaxDestRemet.InnerText = cliente_contato.valor;
            pUfDestRemet.InnerText = cliente_endereco.uf;
            pInsrcEstadualDestRemet.InnerText = cliente_endereco.inscr;
            pFatura.InnerText = mov_nfe.fatura;

            DateTime dtEmissao = DateTime.Parse(mov.dthrMovEmissao);
            DateTime dtSaidaEntrada = DateTime.Parse(mov_nfe.dtSaiEnt);
            pDataEmissao.InnerText = dtEmissao.ToString("dd/MM/yyyy");
            pDataSaidaEntrada.InnerText = dtSaidaEntrada.ToString("dd/MM/yyyy");
            pHoraSaida.InnerText = mov_nfe.horaSaida;

            //preenche TABLE
            Double totaSemDescontos = 0;
            Double totalComDescontos = 0;
            Double totalProdutos = 0;
            Double baseCalculoICMS = 0;
            Double valorICMS = 0;
            Double baseCauculoICMSSubstituicao = 0;
            Double valorICMSSubstituição = 0;
            TableRow tr;
            int cont = 0;
            int nLinhas = mov_item_lista.Count;
            for (int i = 0; i < 21; i++)
            {
                tr = new TableRow();
                if (nLinhas > 0)
                {
                    TempMovItem mi = mov_item_lista[i];
                    tr.Cells.Add(new TableCell() { Text = mi.idItem.ToString(), CssClass = "col1" });
                    tr.Cells.Add(new TableCell() { Text = mi.item_nome, CssClass = "col2" });
                    tr.Cells.Add(new TableCell() { Text = mi.icmsCst, CssClass = "col3" });
                    tr.Cells.Add(new TableCell() { Text = mi.unid_med, CssClass = "col4" });
                    tr.Cells.Add(new TableCell() { Text = mi.qtd.ToString(), CssClass = "col5" });
                    tr.Cells.Add(new TableCell() { Text = formatMoney(Decimal.Parse(mi.vlrUnitVendaFinal.ToString()), true), CssClass = "col6" });
                    tr.Cells.Add(new TableCell() { Text = formatMoney(Decimal.Parse(mi.vlrUnitVendaFinalQtd.ToString()), true), CssClass = "col7" });
                    tr.Cells.Add(new TableCell() { Text = mi.icmsAliq.ToString(), CssClass = "col8" });

                    if (mi.icmsCst == "060")
                    {
                        baseCauculoICMSSubstituicao += mi.vlrUnitVendaFinalQtd;
                        valorICMSSubstituição += (mi.vlrUnitVendaFinalQtd * mi.icmsAliqPadrao) / 100;
                    }
                    else if (mi.icmsCst == "020")
                    {
                        double baseCalculoReduzida = mi.vlrUnitVendaFinalQtd - ((mi.vlrUnitVendaFinalQtd * mi.icmsAliq) / 100);
                        baseCalculoICMS += baseCalculoReduzida;
                        valorICMS += (baseCalculoReduzida * mi.icmsAliqPadrao) / 100;
                    }
                    else if (mi.icmsCst != "040")
                    {
                        baseCalculoICMS += mi.vlrUnitVendaFinalQtd;
                        valorICMS += (mi.vlrUnitVendaFinalQtd * mi.icmsAliqPadrao) / 100;
                    }

                    totaSemDescontos += mi.vlrUnitVendaInicial * mi.qtd;
                    totalComDescontos += mi.vlrUnitVendaFinal * mi.qtd;
                    nLinhas--;
                }
                else
                    tr.Cells.Add(new TableCell() { Text = ".", CssClass = "col1" });

                tProdutos.Rows.Add(tr);
                cont++;
            }

            if (totaSemDescontos > totalComDescontos)
            {
                Double desconto = totaSemDescontos - totalComDescontos;
                tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col1" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col2" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col3" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col4" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col5" });
                tr.Cells.Add(new TableCell() { Text = "DESCONTO", CssClass = "col6" });
                tr.Cells.Add(new TableCell() { Text = formatMoney(Decimal.Parse(desconto.ToString()), true), CssClass = "col7" });
                tr.Cells.Add(new TableCell() { Text = "", CssClass = "col8" });

                tProdutos.Rows.Add(tr);

                totalProdutos = totaSemDescontos - desconto;
            }
            else
                totalProdutos = totaSemDescontos;

            //preenche bloco2
            Double totalNota = totalProdutos + (mov_nfe.valorFrete + mov_nfe.valorSeguro + mov_nfe.valorOutrasDespesas);
            pBaseCalculoIcms.InnerText = formatMoney(Decimal.Parse(baseCalculoICMS.ToString()), true);
            pValorIcms.InnerText = formatMoney(Decimal.Parse(valorICMS.ToString()), true);
            pBaseCalculoIcmsSub.InnerText = formatMoney(Decimal.Parse(baseCauculoICMSSubstituicao.ToString()), true);
            pValorIcmsSub.InnerText = formatMoney(Decimal.Parse(valorICMSSubstituição.ToString()), true);
            pValorTotaProdutos.InnerText = formatMoney(Decimal.Parse(totalProdutos.ToString()), true);
            pValorFrete.InnerText = formatMoney(Decimal.Parse(mov_nfe.valorFrete.ToString()), true);
            pValorSeguro.InnerText = formatMoney(Decimal.Parse(mov_nfe.valorSeguro.ToString()), true);
            pOutrasDespAcessorias.InnerText = formatMoney(Decimal.Parse(mov_nfe.valorOutrasDespesas.ToString()), true);
            pValorToralNota.InnerText = formatMoney(Decimal.Parse(totalNota.ToString()), true);

            if (transportador != null)
            {
                pNomeRazSocialTransp.InnerText = transportador.nome;
                pCpfCnpjTransp.InnerText = transportador.cpf_cnpj;
            }
            if (transportador_endereco != null)
            {
                pEnderecoTransp.InnerText = transportador_endereco.logradouro + " " + transportador_endereco.numero + "" + transportador_endereco.complemento;
                pMunicipioTrans.InnerText = transportador_endereco.cidade;
                pUfTransp.InnerText = transportador_endereco.uf;
                pInsrEstadualTransp.InnerText = transportador_endereco.inscr;
            }
            if (transportador_veiculo != null)
            {
                pPlacaVeiculoTransp.InnerText = transportador_veiculo.placaNumero;
                pUfVeiculoTransp.InnerText = transportador_veiculo.placaUF;
            }
            pFretePorContaTransp.InnerText = (mov_nfe.tipoTranspNFE == SDE.Enumerador.ENfeTipoTransporte.emitente) ? "1" : "2";
            pQuantidadeTransp.InnerText = mov_nfe.volQuantidade.ToString();
            pEspecieTransp.InnerText = mov_nfe.volEspecie;
            pMarcaTrans.InnerText = mov_nfe.volMarca;
            pNumeroTransp.InnerText = mov_nfe.volNumeracao;
            pPesoBrutoTransp.InnerText = mov_nfe.volPesoBruto.ToString();
            pPesoLiquidoTransp.InnerText = mov_nfe.volPesoLiquido.ToString();

            pDadosAdicionais.InnerText = mov_nfe.infoAdicional;

            pNumeroNfRodape.InnerText = mov_nfe.numeroNota.ToString();
        }

        private string formatMoney(Decimal d, Boolean mostra_cifra)
        {
            if (mostra_cifra)
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:C}", d);
            else
                return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:N}", d);
        }
    }
}
