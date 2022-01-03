using System;
using System.Collections.Generic;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using System.Xml;
using System.Text;
using Db4objects.Db4o.Query;

namespace SDE.CamadaServico
{
    public class SMov : SuperServico
    {
        #region Load

        public List<Mov> LoadMovEntrada_NumeroNota(int idCorp, int idCliente, int numNota, EMovResumo resumo)
        {
            setBancoID(idCorp);
            return cMov.LoadMovEntrada_NumeroNota(idCliente, numNota, resumo);
        }

        public List<Mov> LoadMovNumeroNota(int idCorp, int idCliente, int numNota, EMovResumo resumo)
        {
            setBancoID(idCorp);
            return cMov.LoadMovNumeroNota(idCliente, numNota, resumo);
        }
        
        public object[] LoadItemEstoque(int idCorp, int idEmpresa, string barras)
        {
            setBancoID(idCorp);
            ItemEmpEstoque iee = cMov.LoadEstoque(barras);
            if (iee == null)
                return null;
            ItemEmpPreco iep = cItem.LoadPreco(iee.idItem, idEmpresa);
            Item it = cItem.Load(iee.idItem);
            object[] rs = new object[] { it, iee, iep };
            return rs;
        }

        public Mov Load(int idCorp, int idMov, ParamLoadMov pl)
        {
            setBancoID(idCorp);
            Mov mov;
            mov = cMov.Load(idMov);
            if (mov == null)
                return null;
            if (pl != null)
            {
                if (pl.movItens)
                {
                    mov.__mItens = cMov.LoadMovItens(idMov);
                    if (pl.itens)
                        foreach (MovItem mi in mov.__mItens)
                        {
                            mi.__item = cItem.Load(mi.idItem);
                            mi.__item.__ie = cItem.LoadItemEmp(mi.idItem, mov.idEmp);
                            mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);
                        }
                }
                if (pl.movValores)
                    mov.__mValores = cMov.LoadMovValores(idMov);
            }
            return mov;
        }
        #endregion 

        #region Pesquisa
        public List<Mov> Pesquisa(int idCorp, ParamLoadMov pl, ParamFiltroMov pf)
        {
            setBancoID(idCorp);
            List<Mov> lista = null;
            //Pesquisa pela idMov
            if (pf.idMov != 0)
            {
                lista = new List<Mov>();
                Mov m = cMov.Load(pf.idMov);
                if (m != null)
                    lista.Add(m);
            }
            else
            {
                long dtInicial = Utils.DateParseBR(pf.dtInicial).Ticks;
                long dtFinal = Utils.DateParseBR(pf.dtFinal).AddDays(1).Ticks;
                lista = cMov.Pesquisa(dtInicial, dtFinal, pf.tipos);
            }
            if (pl != null)
            {
                foreach (Mov mov in lista)
                {
                    if (pl.movItens)
                        mov.__mItens = cMov.LoadMovItens(mov.id);
                    if (pl.movValores)
                        mov.__mValores = cMov.LoadMovValores(mov.id);
                    if (pl.clientes)
                        mov.__cli = cCliente.Load(mov.idCliente);
                }
            }
            return lista;
        }
        #endregion


        public int NovaMovEntrada(int idCorp, Mov mov)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (mov.__mItens == null)
                        throw new Exception("Movimentação sem Itens");
                    mov.id = cMov.Entrada(mov);
                    db.Commit();
                    return mov.id;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }


        public int NovaMovEntradaDevolucao(int idCorp, Mov mov)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (mov.__mItens == null)
                        throw new Exception("Movimentação sem Itens");
                    mov.cfop = mov.__mItens[0].cfop.ToString();
                    if (mov.tipo == EMovTipo.entrada_compra ||
                        mov.tipo == EMovTipo.entrada_devolucao)
                    {
                        mov.resumo = EMovResumo.entrada;
                        mov.id = cMov.Entrada(mov);
                    }

                    db.Commit();
                    return mov.id;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }




        public int NovaMovPDV(int idCorp, Mov mov)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {                
                    CalculaImpostos(mov.idEmp, mov, mov.__mItens);
                    if (mov.__mItens == null)
                        throw new Exception("Movimentação sem Itens");
                    mov.cfop = mov.__mItens[0].cfop.ToString();
                    if (mov.tipo == EMovTipo.saida_venda ||
                        mov.tipo == EMovTipo.saida_condi)
                    {
                        mov.resumo = EMovResumo.saida;
                        mov.id = cMov.Venda(mov);
                    }
                    if (mov.tipo == EMovTipo.outros_reserva ||
                        mov.tipo == EMovTipo.outros_orcamento)
                    {
                        mov.resumo = EMovResumo.outros;
                        mov.id = cMov.Orcamento(mov);
                    }
                    db.Commit();
                    return mov.id;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }

        public int NovaMovServico(int idCorp, Mov mov)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (mov.__mItens == null)
                        throw new Exception("Movimentação sem Itens");
                    mov.id = cMov.Servico(mov);
                    db.Commit();
                    return mov.id;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }

        public string GeraDms(int idCorp, int idEmp, int idMov)
        {
            StringBuilder sb = new StringBuilder();

            ParamLoadMov pl = new ParamLoadMov();
            pl.movItens = true;
            Mov mov = this.Load(idCorp, idMov, pl);
            Empresa emp = cEmp.Load(idEmp);
            Cliente empCli = cCliente.Load(emp.idCliente);
            Cliente cli = cCliente.Load(mov.idCliente);
            Item it;

            DateTime dt = DateTime.Parse(mov.dthrMovEmissao);
            string valorDaNotaFiscal = string.Format("{0:0.00}", mov.vlrItensFinal).Replace(",", ".");
            string baseDeCalculo;

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            XmlWriterSettings oSettings = new XmlWriterSettings();
            oSettings.Indent = true;
            oSettings.Encoding = Encoding.ASCII;
            oSettings.IndentChars = "     ";

            XmlWriter writer = XmlWriter.Create(sb, oSettings);

            writer.WriteStartDocument();
            writer.WriteStartElement("declaracaoDeServicoDms");    //declaracaoDeServicoDms
            //contribuinte
            writer.WriteStartElement("contribuinte");
            writer.WriteElementString("cgc", empCli.cpf_cnpj);
            writer.WriteEndElement();
            //serviço
            writer.WriteStartElement("servicoDms");
            writer.WriteElementString("anoDeReferencia", mov.anoReferencia);
            writer.WriteElementString("mesDeReferencia", mov.mesReferencia);
            writer.WriteElementString("tipo", mov.tipoServicoDms);
            writer.WriteElementString("tipoDeDeclaracao", mov.tipoDeclaracaoDms);
            writer.WriteElementString("situacao", mov.situacaoDms);
            writer.WriteEndElement(); //declaracaoDeServicoDms
            //itensDeServico
            writer.WriteStartElement("itensDeServico");
            foreach (MovItem mi in mov.__mItens)
            {
                it = cItem.Load(mi.idItem);
                baseDeCalculo = string.Format("{0:0.00}", mi.vlrUnitVendaFinal).Replace(",", ".");
                writer.WriteStartElement("itemDeServico");
                writer.WriteElementString("cnpjPrestadorTomador", cli.cpf_cnpj);
                writer.WriteElementString("razaoSocialDePrestadorTomadorNaoCadastrado", cli.apelido_razsoc);
                writer.WriteElementString("numeroDaNotaFiscal", mov.numeroNF.ToString());
                writer.WriteElementString("serieDaNotaFiscal", mov.serieNF);
                writer.WriteElementString("dataDaNotaFiscal", dt.ToString("yyyy-MM-dd"));
                writer.WriteElementString("historicoDaNotaFiscal", it.nome);
                writer.WriteElementString("valorDaNotaFiscal", valorDaNotaFiscal);
                writer.WriteElementString("baseDeCalculo", baseDeCalculo);
                writer.WriteElementString("aliquota", mi.bcISS.ToString());
                writer.WriteElementString("recolhidoPeloTomador", (mi.recolhidoPeloTomador) ? "1" : "0");
                writer.WriteElementString("notaFiscalCancelada",(mov.isNfsCancelada)?"1":"0");
                writer.WriteEndElement();
            }
            writer.WriteEndElement(); //itensDeServico
            writer.WriteEndDocument(); //documento
            writer.Flush();
            writer.Close();

            string s = sb.ToString();

            return s;
        }

        #region Cancelamento
        public void CancelaMov(int idCorp, int idMov, int idClienteFuncionarioLogado)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //busca movimentação a cancelar
                    Mov movDados = cMov.Load(idMov);
                    if (movDados == null)
                        throw new Exception("Movimentação inexistente");
                    if (movDados.idMovCanceladora > 0)
                        throw new Exception("Movimentação já cancelada");
                    List<MovItem> movItens = cMov.LoadMovItens(idMov);
                    List<MovValor> movValores = cMov.LoadMovValores(idMov);
                    //movimentação de cancelamento
                    Mov mov = new Mov();
                    Utils.copiaCamposBasicos(movDados, mov);
                    mov.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    mov.dthrMovEmissao = Utils.getAgoraString();
                    mov.dtLancaCaixa = Utils.getAgoraString();
                    mov.dtMovTicks = Utils.getAgoraTicks();
                    mov.dtLancTicks = Utils.getAgoraTicks();
                    mov.__mItens = new List<MovItem>();
                    mov.__mValores = new List<MovValor>();
                    //copiar movitens
                    foreach (MovItem miDados in movItens)
                    {
                        miDados.__mIEstoques = cMov.ListMovItemEstoque(miDados.id);
                        MovItem mi = new MovItem();
                        Utils.copiaCamposBasicos(miDados, mi);
                        mi.__mIEstoques = new List<MovItemEstoque>();
                        mi.__mIEstoques = cMov.ListMovItemEstoque(miDados.id);

                        //Copiar MovItemEstoque
                        foreach (MovItemEstoque mieDados in miDados.__mIEstoques)
                        {
                            MovItemEstoque mie = new MovItemEstoque();
                            Utils.copiaCamposBasicos(mieDados, mie);
                            mi.__mIEstoques.Add(mie);
                        }
                        mov.__mItens.Add(mi);
                    }
                    //copiar movValores
                    foreach (MovValor mvDados in movValores)
                    {
                        MovValor mv = new MovValor();
                        Utils.copiaCamposBasicos(mvDados, mv);
                        mov.__mValores.Add(mv);
                    }

                    switch (movDados.resumo)
                    {
                        case EMovResumo.saida:
                            mov.resumo = EMovResumo.saida;
                            mov.tipo = EMovTipo.saida_cancel;
                            cMov.CancelaSaida(movDados, mov);
                            break;
                        case EMovResumo.entrada:
                            mov.resumo = EMovResumo.entrada;
                            mov.tipo = EMovTipo.entrada_cancel;
                            cMov.CancelaEntrada(movDados, mov);
                            break;
                        case EMovResumo.outros:
                            mov.resumo = EMovResumo.outros;
                            if (movDados.tipo == EMovTipo.outros_reserva)
                            {
                                mov.tipo = EMovTipo.outros_cancel;
                                cMov.CancelaReserva(movDados, mov);
                            }
                            break;
                        case EMovResumo.ambos:
                            mov.resumo = EMovResumo.ambos;
                            if (movDados.tipo == EMovTipo.ambos_balan)
                            {
                                mov.tipo = EMovTipo.ambos_cancel;
                                cMov.CancelaBalanco(movDados, mov);
                            }
                            break;
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }
        #endregion

        #region Calculo imposto
        public void CalculaImpostos(int idEmp, Mov mov, List<MovItem> movItens)
        {
            Empresa emp = cEmp.Load(idEmp);
            foreach (MovItem mi in movItens)
            {
                if (mi.__mIEstoques == null)
                    mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);

                Item it = cItem.Load(mi.idItem);
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    //Precos
                    ItemEmpPreco itPreco = cItem.LoadPreco(mi.idItem, idEmp);
                    //Aliquotas
                    ItemEmpAliquotas itAliq = cItem.LoadAliquotas(idEmp, mi.idItem);
                    //determinção do valor unitario

                    double vlrUnit = 0;
                    double vlrTotalProduto = 0;
                    if (mov.resumo == EMovResumo.saida)
                    {
                        vlrUnit = mi.vlrUnitVendaFinal;
                        mi.icmsCst = itAliq.icmsCST_SD;
                        mi.icmsAliq = itAliq.icmsAliq_SD;
                        mi.icmsCst = itAliq.icmsCST_SD;
                    }
                    else if (mov.resumo == EMovResumo.entrada)
                    {
                        vlrUnit = mi.vlrUnitCompra;
                        mi.icmsCst = itAliq.icmsCST_ED;
                        mi.icmsAliq = itAliq.icmsAliq_ED;
                        mi.icmsAliqPadrao = itAliq.icmsAliqPadrao_ED;
                    }
                    vlrTotalProduto = vlrUnit * mie.qtd;

                    if (!emp.isOptanteSimplesNacional)
                    {
                        //calculo do icms
                        switch (mi.icmsCst)
                        {
                            case "000": //tributado normal
                                mi.bcICMS = vlrTotalProduto;
                                mi.vlrICMS = vlrTotalProduto * (mi.icmsAliq / 100);
                                break;
                            case "020": //tributado com reducao
                                mi.bcICMS = vlrTotalProduto;
                                mi.vlrICMS = vlrTotalProduto * (mi.icmsAliq / 100);
                                break;
                            case "040"://isento
                                mi.vlrIcmsIsento = vlrTotalProduto * (mi.icmsAliq / 100);
                                break;
                            case "060"://substituicao tributaria
                                mi.bcIcmsSubstTrib = vlrTotalProduto;
                                mi.vlrIcmsSubstTrib = vlrTotalProduto * (mi.icmsAliq / 100);
                                break;
                            default:
                                break;
                        }
                    }
                    //calculo do IPI
                    mi.ipiCst = itAliq.ipiCST;
                    mi.ipiAliq = itAliq.ipiAliq;
                    if (itAliq.ipiCST == "00" || itAliq.ipiCST == "49" ||
                        itAliq.ipiCST == "50" || itAliq.ipiCST == "99")
                    {
                        mi.bcIPI = vlrTotalProduto;
                        //IPI por aliquota
                        if (itAliq.ipiTipoCalculo == ECalculoIpiTipo.percentual)
                            mi.vlrIPI = vlrTotalProduto * (itAliq.ipiAliq / 100);
                        //IPI por quantidade
                        else
                            mi.vlrIPI = itAliq.ipiAliq * mi.qtd;
                    }

                    //calculo do PIS
                    //tributacao por aliquota
                    mi.pisAliq = itAliq.pisAliq;
                    mi.pisCst = itAliq.pisCST;
                    if (itAliq.pisCST == "01" || itAliq.pisCST == "02")
                    {
                        mi.bcPIS = vlrTotalProduto;
                        mi.vlrPis = vlrTotalProduto * (itAliq.pisAliq / 100);
                    }
                    //tributacao por quantidade
                    else if (itAliq.pisCST == "03")
                    {
                        //BC quantidade de produtos
                        mi.bcPIS = mie.qtd;
                        mi.vlrPis = itAliq.pisAliq * mie.qtd;
                    }

                    //calculo do COFINS
                    //cst 01 , 02 por aliquota
                    mi.cofinsAliq = itAliq.cofinsAliq;
                    mi.cofinsCst = itAliq.cofinsCST;
                    //tributação por aliquota
                    if (itAliq.cofinsCST == "01" || itAliq.cofinsCST == "02")
                    {
                        mi.bcCOFINS = vlrTotalProduto;
                        mi.vlrCofins = vlrTotalProduto * (itAliq.cofinsAliq / 100);
                    }
                    //tributacao por quantidade
                    else if (itAliq.cofinsCST == "03")
                    {
                        //BC quantidade de Produtos
                        mi.bcCOFINS = mie.qtd;
                        mi.vlrCofins = itAliq.cofinsAliq * mie.qtd;
                    }

                }
            }
        }
        #endregion



    }
}
