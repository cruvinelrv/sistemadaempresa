using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.CamadaServico;
using SDE.Entidade;
using SDE.Enumerador;
using SDE.Constantes;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SDE.EntidadeNFE
{
    public class EscreveItensNfe
    {
        private const string nomePais = "BRASIL";
        private const int codPais = 1058;

        public double
            vlrTotalBC = 0, vlrTotalICMS = 0, vlrTotalBCST = 0, vlrTotalIcmsST = 0, vlrTotalProduto = 0, vlrTotalNF = 0, vlrTotalFrete = 0,
            vlrTotalSeguro = 0, vlrTotalDesc = 0, vlrTotalII = 0, vlrTotalIPI = 0, vlrTotalPIS = 0, vlrTotalCOFINS = 0, vlrTotalOutros = 0,
            acumulaVlrTotalBC = 0, acumulaVlrTotalICMS = 0, acumulaVlrTotalBCST = 0, acumulaVlrTotalIcmsST = 0, acumulaVlrTotalProduto = 0,
            acumulaVlrTotalNF = 0, acumulaVlrTotalFrete = 0, acumulaVlrTotalSeguro = 0, acumulaTotalDesc = 0, acumulaVlrTotalII = 0,
            acumulaVlrTotalIPI = 0, acumulaVlrTotalPIS = 0, acumulaVlrTotalCOFINS = 0, acumulaVlrTotalOutros = 0;


        private string tpnf = "";
        private string fatura = "";



        public void Escreve_Inicio(XmlWriter writer, String chaveAcesso)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("NFe", "http://www.portalfiscal.inf.br/nfe");
            writer.WriteStartElement("infNFe");
            writer.WriteAttributeString("Id", "NFe" + chaveAcesso);
            writer.WriteAttributeString("versao", "1.10");
        }

        //ITEM B identificacao da NFE
        public void Escreve_ide(XmlWriter writer, Mov mov, MovNFE nfe, MovNFE nfeRF, string codUF, string codMunicipio, int digVerificador, string codigoNumerico, string tipoAmbiente)
        {
            string tipoNF = string.Empty;
            string naturezaOP = string.Empty;
            int formaPgto = 0;
            int finalidade = 0;

            //tipo da nota fiscal
            //0-entrada   1-saida
            if (mov.resumo == EMovResumo.entrada)
                tipoNF = "0";
            else
                tipoNF = "1";

            //natureza da operação           
            naturezaOP = nfe.cfop;

            //Forma Pagamento
            if (nfe.formaPgtoNFE == ENfeFormaPgto.vista)
                formaPgto = 0;
            else if (nfe.formaPgtoNFE == ENfeFormaPgto.prazo)
                formaPgto = 1;
            else
                formaPgto = 2;

            //finalidade   1-normal 2-complementar  3-ajuste
            if (nfe.finalidadeNFE == ENfeFinalidade.normal)
                finalidade = 1;
            else if (nfe.finalidadeNFE == ENfeFinalidade.complementar)
                finalidade = 2;
            else
                finalidade = 3;

            if (tipoAmbiente == "1")
                tipoAmbiente = "2";
            if (tipoAmbiente == "0")
                tipoAmbiente = "1";




            writer.WriteStartElement("ide"); //identificação
            writer.WriteElementString("cUF", codUF);//codigo da Uf do remetente
            writer.WriteElementString("cNF", codigoNumerico); //codigo da chave de acesso
            writer.WriteElementString("natOp", naturezaOP.ToString());//natureza da operação (venda, compra, devolucao)
            writer.WriteElementString("indPag", formaPgto.ToString());//indicador pagamaneto //0-a vista, 1-a prazo, 2-outros
            writer.WriteElementString("mod", "55");
            writer.WriteElementString("serie", nfe.serieNota.ToString());//serie do documento fiscal
            writer.WriteElementString("nNF", nfe.numeroNota.ToString());//numero do documento Fiscal
            writer.WriteElementString("dEmi", formataDataNFe(mov.dthrMovEmissao));
            writer.WriteElementString("dSaiEnt", formataDataNFe(mov.dthrMovEmissao));
            writer.WriteElementString("tpNF", tipoNF);//0 - entrada , 1- saida
            writer.WriteElementString("cMunFG", codMunicipio.PadRight(7, '0')); //codido de municipio do fator gerador

            tpnf = tipoNF;
            //determiana nfe Referenciada , quando finalidade for ajuste ou complemento
            if (nfe.finalidadeNFE != ENfeFinalidade.normal && nfeRF != null)
            {
                writer.WriteStartElement("NFref"); //nota Fiscal RF
                writer.WriteElementString("refNFe", nfeRF.chaveAcessoNFE.ToString());
                writer.WriteEndElement(); //nota Fiscal RF
            }

            writer.WriteElementString("tpImp", "1");//tipo impressao  1-Retrato 2-Paisagem
            writer.WriteElementString("tpEmis", "1");//1-normal, 2- Contigencia, 3-Contigencia, 4-Contigencia, 5-Contigencia
            writer.WriteElementString("cDV", digVerificador.ToString());//_______Digito verificador da chave de acesso
            writer.WriteElementString("tpAmb", tipoAmbiente);//_____identificação do ambiente; 1- Produção, 2- homologação //só homologacao
            writer.WriteElementString("finNFe", finalidade.ToString());//____finalidade da NFE 1-normal 2-complementar  3-ajuste
            writer.WriteElementString("procEmi", "0");//Processo de emissão, 0-aplicativo contribuinte, 1-avulsa pelo fisco, 2-atraves do fisco, 3-
            writer.WriteElementString("verProc", "UNINFE2.0");//___versão do aplicativo emissor da nfe
            writer.WriteEndElement(); //ide
        }

        //Item C - Emitente

        public void Escreve_emit(XmlWriter writer, Cliente cli, ClienteEndereco ce, ClienteContato cc) // ,string crt, string csosn) // add parâmetros para V.2 da NFe
        {
            Utils.retiraCaracterEspecial(cli);
            Utils.retiraCaracterEspecial(ce);
            Utils.retiraCaracterEspecial(cc);

            //Inicio TAG Emitente
            writer.WriteStartElement("emit");

            if (cli.tipo == EPesTipo.Fisica)
                writer.WriteElementString("CPF", Utils.apenasNumeros(cli.cpf_cnpj));
            else
                writer.WriteElementString("CNPJ", Utils.apenasNumeros(cli.cpf_cnpj));

            writer.WriteElementString("xNome", cli.apelido_razsoc);
            writer.WriteElementString("xFant", cli.nome);



            //inicio TAG endereco
            writer.WriteStartElement("enderEmit");

            writer.WriteElementString("xLgr", ce.logradouro);
            writer.WriteElementString("nro", ce.numero.ToString());

            if (ce.complemento.Trim() != String.Empty)
            {
                writer.WriteElementString("xCpl", ce.complemento);
            }

            writer.WriteElementString("xBairro", ce.bairro);
            writer.WriteElementString("cMun", ce.cidadeIBGE.PadRight(7, '0'));
            writer.WriteElementString("xMun", ce.cidade);
            writer.WriteElementString("UF", ce.uf);

            if (ce.tipo == EEnderecoTipo.residencial_comercial)
                writer.WriteElementString("CEP", Utils.apenasNumeros(ce.cep));

            writer.WriteElementString("cPais", codPais.ToString());
            writer.WriteElementString("xPais", nomePais);
            //if (ce.fone.Trim() != String.Empty)

            if (cc != null && cc.valor != null && cc.valor != "")
            {
                String s = Utils.apenasNumeros(cc.valor);
                if (cc.tipo == EContatoTipo.fone_fixo && s.Length == 10)
                    writer.WriteElementString("fone", s);
            }



            //writer.WriteElementString("fone", Utils.apenasNumeros(cc.valor));

            writer.WriteEndElement();
            //final TAG endereco

            if (ce.inscr == String.Empty || ce.inscr == "ISENTO")
                writer.WriteElementString("IE", "ISENTO");
            else
                writer.WriteElementString("IE", ce.inscr);




            /* //descomentar esse gurpo de código para validar o CRT e CSOSN da Versão 2.0 da Nfe.    
            |------------------------------------------------------------------------------------------------------------------|
            | //Aqui colocarei TAG CRT + CSOSN (Caso Simples Nacional {1}) | Caso CRT 3 --> Evidencia que não vá fazer nada!   |
            | if (crt == "1" || crt == "2")                                                                                    |
            | {                                                                                                                |
            |     writer.WriteElementString("CRT", crt);                                                                       |
            |     writer.WriteElementString("CSOSN", csosn);                                                                   |
            | }                                                                                                                |
            | else if (crt == "3")                                                                                             |
            |     writer.WriteElementString("CRT", crt);                                                                       |
            |                                                                                                                  |
            |                                                                                                                  |
            |------------------------------------------------------------------------------------------------------------------|
            */
            //descomentar esse gurpo de código para validar o CRT e CSOSN da Versão 2.0 da Nfe.    

            //writer.WriteElementString("IEST", "0");//inscricao estadual do Substituto tributario
            //campos de nfe conjugada, prestação de serviço e peças
            //writer.WriteElementString("IM", "");
            //writer.WriteElementString("CNAE", "4611700");
            writer.WriteEndElement();
            //final TAG Emitente
        }



        //Item D -  destinatario
        public void Escreve_dest(XmlWriter writer, Cliente cli, ClienteContato cliContato, ClienteEndereco ce, string clienteIE)
        {
            Utils.retiraCaracterEspecial(cli);
            Utils.retiraCaracterEspecial(ce);

            //Inicio TAG Destinatario
            writer.WriteStartElement("dest");
            if (cli.tipo == EPesTipo.Fisica)
            {
                writer.WriteElementString("CPF", Utils.apenasNumeros(cli.cpf_cnpj));
                writer.WriteElementString("xNome", cli.nome);
            }
            else
            {
                writer.WriteElementString("CNPJ", Utils.apenasNumeros(cli.cpf_cnpj));
                writer.WriteElementString("xNome", cli.apelido_razsoc);
            }


            //Inicio TAG Endereco
            writer.WriteStartElement("enderDest");
            writer.WriteElementString("xLgr", ce.logradouro);
            writer.WriteElementString("nro", ce.numero.ToString());
            if (ce.complemento.Trim() != String.Empty)
                writer.WriteElementString("xCpl", ce.complemento);
            writer.WriteElementString("xBairro", ce.bairro);
            writer.WriteElementString("cMun", ce.cidadeIBGE.PadRight(7, '0'));
            writer.WriteElementString("xMun", ce.cidade);
            writer.WriteElementString("UF", ce.uf);
            writer.WriteElementString("CEP", Utils.apenasNumeros(ce.cep));
            writer.WriteElementString("cPais", codPais.ToString());
            writer.WriteElementString("xPais", nomePais);

            if (cliContato != null && cliContato.valor != null && cliContato.valor != "")
            {
                String s = Utils.apenasNumeros(cliContato.valor);
                if (cliContato.tipo == EContatoTipo.fone_fixo && s.Length == 10)
                    writer.WriteElementString("fone", s);
            }

            writer.WriteEndElement();
            //Fim TAG Endereco

            if (ce.inscr == String.Empty || ce.inscr == "ISENTO")
                writer.WriteElementString("IE", "ISENTO");
            else
                writer.WriteElementString("IE", ce.inscr);

            writer.WriteEndElement();
            //Fim da TAG Destinatario
        }

        //Item E - identificação do local de retirada
        public void Escreve_retirada(XmlWriter writer, EPesTipo tipo, string cnpj, ClienteEndereco ce)
        {
            Utils.retiraCaracterEspecial(ce);

            //incio TAG local Retirada
            writer.WriteStartElement("retirada");
            if (tipo == EPesTipo.Fisica)
            {
                writer.WriteElementString("CPF", Utils.apenasNumeros(cnpj));
            }
            else
                writer.WriteElementString("CNPJ", cnpj);
            writer.WriteElementString("xLgr", ce.logradouro);
            writer.WriteElementString("nro", ce.numero.ToString());
            if (ce.complemento != String.Empty)
                writer.WriteElementString("xCpl", ce.complemento);
            writer.WriteElementString("xBairro", ce.bairro);
            writer.WriteElementString("cMun", ce.cidadeIBGE.PadRight(7, '0'));
            writer.WriteElementString("xMun", ce.cidade);
            writer.WriteElementString("UF", ce.uf);
            writer.WriteEndElement();
            //fim TAG local Retirada
        }

        //Item F - identificação do local de entrega
        public void Escreve_entrega(XmlWriter writer, EPesTipo tipo, string cnpj, ClienteEndereco ce)
        {
            Utils.retiraCaracterEspecial(ce);

            //inicio TAG local de Entrega
            if (tpnf == "0")
            {
                writer.WriteStartElement("entrega");

                writer.WriteElementString("CNPJ", cnpj);
                writer.WriteElementString("xLgr", ce.logradouro);
                writer.WriteElementString("nro", ce.numero.ToString());

                if (ce.complemento != String.Empty)
                    writer.WriteElementString("xCpl", ce.complemento);

                writer.WriteElementString("xBairro", ce.bairro);
                writer.WriteElementString("cMun", ce.cidadeIBGE.PadRight(7, '0'));
                writer.WriteElementString("xMun", ce.cidade);
                writer.WriteElementString("UF", ce.uf);
                writer.WriteEndElement();
            }
            //fim TAG local de Entrega
        }

        //Item G - Detalhamento de produto e serviços
        //Item H - Produtos e serviços da NFE
        public void Escreve_det(XmlWriter writer, MovItem mi, EMovTipo tipo, Item item, ItemEmpAliquotas iea, int numItem, Empresa emp, Cliente idClientEmp)
        {
            //Item 
            Item it = item;
            //itemEmpAliq


            ItemEmpAliquotas itAliq = iea;
            double vlrUnit = 0;
            if (tipo == EMovTipo.entrada_compra)
                vlrUnit = mi.vlrUnitCompra;
            else
                vlrUnit = mi.vlrUnitVendaFinal;

            //detremina nacional
            int origem = 0;
            if (it.origem == EItemOrigem.nacional)
                origem = 0;
            else if (it.origem == EItemOrigem.internacional)
                origem = 1;
            else
                origem = 2;


            //valores do movItem
            double valorTotalItem = vlrUnit * mi.qtd;
            double valorIcms = 0;
            double valorIPI = 0;
            double valorPIS = 0;
            double valorCOFINS = 0;

            //inicio TAG Detelhamento
            writer.WriteStartElement("det");
            writer.WriteAttributeString("nItem", numItem.ToString());
            //inicio TAG produto
            writer.WriteStartElement("prod");
            writer.WriteElementString("cProd", it.id.ToString());
            writer.WriteElementString("cEAN", "");
            writer.WriteElementString("xProd", it.nome);
            writer.WriteElementString("CFOP", mi.cfop.ToString());
            writer.WriteElementString("uCom", it.unidMed.ToString());
            writer.WriteElementString("qCom", formataValor(mi.qtd, 4));
            writer.WriteElementString("vUnCom", formataValor(vlrUnit, 4));
            writer.WriteElementString("vProd", formataValor(valorTotalItem, 2));
            writer.WriteElementString("cEANTrib", "");
            writer.WriteElementString("uTrib", it.unidMed.ToString());
            writer.WriteElementString("qTrib", formataValor(mi.qtd, 4));
            writer.WriteElementString("vUnTrib", formataValor(vlrUnit, 4));
            if (mi.vlrFrete > 0)
                writer.WriteElementString("vFrete", formataValor(mi.vlrFrete, 2));
            if (mi.vlrSeguro > 0)
                writer.WriteElementString("vSeg", formataValor(mi.vlrSeguro, 2));

            double desconto = mi.qtd * (mi.vlrUnitVendaInicial - mi.vlrUnitVendaFinal);

            if (desconto > 0)
                writer.WriteElementString("vDesc", formataValor(desconto, 2));
            writer.WriteEndElement();
            //fim TAG produto
            //inicio TAG impostos
            writer.WriteStartElement("imposto");

            #region ICMS
            //inicio da TAG ICMS
            writer.WriteStartElement("ICMS");


            //verificacao do icms  
            mi.icmsCst = (mi.icmsCst == null || mi.icmsCst == "") ? "000" : mi.icmsCst;
            if (mi.icmsCst.Equals("000") || mi.icmsCst == String.Empty)
            {
                vlrTotalProduto = valorTotalItem;
                vlrTotalICMS = mi.vlrICMS;
                vlrTotalBC = mi.bcICMS;

                if (emp.idCliente == idClientEmp.id)
                {
                    if (!emp.isOptanteSimplesNacional)
                    {
                        mi.bcICMS = vlrTotalProduto;
                        mi.vlrICMS = vlrTotalProduto * (mi.icmsAliq / 100);



                        writer.WriteStartElement("ICMS00"); //ICMS00
                        writer.WriteElementString("orig", origem.ToString());//0-nacional, 1-Estrangeiro importação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "00");
                        writer.WriteElementString("modBC", "3");//0-MArgem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                        writer.WriteElementString("vBC", formataValor(mi.bcICMS, 2));
                        writer.WriteElementString("pICMS", formataValor(mi.icmsAliq, 2));
                        writer.WriteElementString("vICMS", formataValor(mi.vlrICMS, 2));
                        writer.WriteEndElement();//fim ICMS00
                    }
                    else
                    {
                        writer.WriteStartElement("ICMS00"); //ICMS00
                        writer.WriteElementString("orig", origem.ToString());//0-nacional, 1-Estrangeiro importação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "00");
                        writer.WriteElementString("modBC", "3");//0-MArgem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                        writer.WriteElementString("vBC", formataValor(mi.bcICMS, 2));
                        writer.WriteElementString("pICMS", formataValor(mi.icmsAliq, 2));
                        writer.WriteElementString("vICMS", formataValor(mi.vlrICMS, 2));
                        writer.WriteEndElement();//fim ICMS00
                    }
                }

            }
            else if (mi.icmsCst.Equals("020"))
            {
                vlrTotalProduto = valorTotalItem;
                vlrTotalICMS = mi.vlrICMS;
                vlrTotalBC = mi.bcICMS;
                double pReducao = 0;

                if (emp.idCliente == idClientEmp.id)
                {
                    if (!emp.isOptanteSimplesNacional)
                    {
                        pReducao = 100 - 100 * ((mi.icmsAliq / 100) / (mi.icmsAliqPadrao / 100));
                        mi.bcICMS = vlrTotalProduto - ((vlrTotalProduto * pReducao) / 100);
                        mi.vlrICMS = (mi.bcICMS * mi.icmsAliqPadrao) / 100;

                        writer.WriteStartElement("ICMS20");//ICMS20
                        writer.WriteElementString("orig", origem.ToString());//0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "20");
                        writer.WriteElementString("modBC", "3");//0-Margem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                        writer.WriteElementString("pRedBC", formataValor(pReducao, 2));//percentual de redução da BC
                        writer.WriteElementString("vBC", formataValor(mi.bcICMS, 2));
                        writer.WriteElementString("pICMS", formataValor(mi.icmsAliq, 2));
                        writer.WriteElementString("vICMS", formataValor(mi.vlrICMS, 2));
                        writer.WriteEndElement();//fim ICMS20
                    }
                    else
                    {
                        writer.WriteStartElement("ICMS20");//ICMS20
                        writer.WriteElementString("orig", origem.ToString());//0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "20");
                        writer.WriteElementString("modBC", "3");//0-Margem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                        writer.WriteElementString("pRedBC", formataValor(pReducao, 2));//percentual de redução da BC
                        writer.WriteElementString("vBC", formataValor(mi.bcICMS, 2));
                        writer.WriteElementString("pICMS", formataValor(mi.icmsAliq, 2));
                        writer.WriteElementString("vICMS", formataValor(mi.vlrICMS, 2));
                        writer.WriteEndElement();//fim ICMS20
                    }
                }
            }
            else if (mi.icmsCst.Equals("040") || mi.icmsCst.Equals("041") || mi.icmsCst.Equals("050")) //isento, nao Tributavel
            {
                vlrTotalICMS += mi.vlrICMS;
                vlrTotalBC += mi.bcICMS;

                writer.WriteStartElement("ICMS40"); //ICMS40
                writer.WriteElementString("orig", origem.ToString());// 0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                if (mi.icmsCst.Equals("040"))
                    writer.WriteElementString("CST", "40"); // 40-isento, 41-não tributado, 50-suspensão
                else if (mi.icmsCst.Equals("041"))
                    writer.WriteElementString("CST", "41"); // 40-isento, 41-não tributado, 50-suspensão
                else
                    writer.WriteElementString("CST", "50"); // 40-isento, 41-não tributado, 50-suspensão
                writer.WriteEndElement();//fim ICMS40

            }
            else if (mi.icmsCst.Equals("060"))//substituição tributaria
            {
                vlrTotalIcmsST = mi.vlrIcmsSubstTrib;
                vlrTotalBCST = mi.bcIcmsSubstTrib;

                if (emp.idCliente == idClientEmp.id)
                {
                    if (!emp.isOptanteSimplesNacional)
                    {
                        mi.bcIcmsSubstTrib = vlrTotalProduto;
                        mi.vlrIcmsSubstTrib = vlrTotalProduto * (mi.icmsAliq / 100);

                        writer.WriteStartElement("ICMS60"); //ICMS60
                        writer.WriteElementString("orig", origem.ToString());// 0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "60");//substituicao tributaria
                        writer.WriteElementString("vBCST", formataValor(mi.bcIcmsSubstTrib, 2));
                        writer.WriteElementString("vICMSST", formataValor(mi.vlrIcmsSubstTrib, 2));
                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement("ICMS60"); //ICMS60
                        writer.WriteElementString("orig", origem.ToString());// 0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                        writer.WriteElementString("CST", "60");//substituicao tributaria
                        writer.WriteElementString("vBCST", formataValor(mi.bcIcmsSubstTrib, 2));
                        writer.WriteElementString("vICMSST", formataValor(mi.vlrIcmsSubstTrib, 2));
                        writer.WriteEndElement();
                    }
                }
            }
            writer.WriteEndElement();

            //fim TAG ICMS

            #endregion

            #region IPI -  verificar codigo
            //inicio da TAG IPI
            if (itAliq.ipiAliq != 0)
            {
                writer.WriteStartElement("IPI");
                if (itAliq.ipiClasseEnquad.Length == 5)
                    writer.WriteElementString("clEnq", itAliq.ipiClasseEnquad);
                writer.WriteElementString("CNPJProd", itAliq.ipiCNPJ);//cnpj Produtor
                writer.WriteElementString("cSelo", itAliq.ipiCodSelo);//codigo do selo
                if (itAliq.ipiQtdSelo != 0)
                    writer.WriteElementString("qSelo", itAliq.ipiQtdSelo.ToString());//quantidade do selo
                if (itAliq.ipiCodEnquad.Length == 3)
                    writer.WriteElementString("cEnq", itAliq.ipiCodEnquad);
                else
                    writer.WriteElementString("cEnq", "999");
                //00-entrada com recuperação de credito, 49-outras entradas, 50-saida tributada,  99-outras saidas
                if (mi.ipiCst == "00" || mi.ipiCst == "49" || mi.ipiCst == "50" || mi.ipiCst == "99")
                {
                    writer.WriteStartElement("IPITrib");
                    writer.WriteElementString("CST", mi.ipiCst);
                    //informar campos (O10 O13 O14) se IPI for cobrado por aliquota
                    if (itAliq.ipiTipoCalculo == ECalculoIpiTipo.percentual)
                    {
                        vlrTotalIPI = mi.vlrIPI;
                        writer.WriteElementString("vBC", formataValor(mi.bcIPI, 2));//valor BC
                        writer.WriteElementString("pIPI", formataValor(mi.ipiAliq, 2));//pesrcentual da aliquota
                        writer.WriteElementString("vIPI", formataValor(mi.vlrIPI, 2));
                    }
                    //informar campos (O11 O12 O14) se IPI for cobrado por unidade
                    else
                    {
                        vlrTotalIPI += mi.vlrIPI;

                        writer.WriteElementString("qUnid", formataValor(mi.qtd, 4)); //quantidade de produto
                        writer.WriteElementString("vUnid", formataValor(mi.ipiAliq, 4));//valor por unidade
                        writer.WriteElementString("vIPI", formataValor(mi.vlrIPI, 2));//valor total do IPI
                    }
                    writer.WriteEndElement();
                }
                //01-entrada tributada zero, 02-entrada isenta, 03- entrada nao-tributada, 04-entrada imune, entrada com suspensão
                //51-saida tributada zero, 52-saida isenta, 53 saida não tributada, 54-saida imune, 55- saida com suspensão
                else
                {
                    writer.WriteStartElement("IPINT");
                    writer.WriteElementString("CST", mi.ipiCst);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            //fim da TAG IPI
            #endregion

            #region PIS
            //inicio TAG Pis
            writer.WriteStartElement("PIS");
            if (mi.vlrPis > 0)
            {
                //CST 01, 02 - tributado pela aliquota
                if (mi.pisCst == "01" || mi.pisCst == "02")
                {
                    vlrTotalPIS += mi.vlrPis;
                    writer.WriteStartElement("PISAliq"); //PISAliq
                    writer.WriteElementString("CST", mi.pisCst);
                    writer.WriteElementString("vBC", formataValor(mi.bcPIS, 2));
                    writer.WriteElementString("pPIS", formataValor(mi.pisAliq, 2));
                    writer.WriteElementString("vPIS", formataValor(mi.vlrPis, 2));
                    writer.WriteEndElement(); //PISAliq
                }
                //Cst 03  -  tributado pela quantidade
                else if (mi.pisCst == "03")
                {
                    valorPIS = mi.qtd * mi.pisAliq;
                    vlrTotalPIS = valorPIS;

                    writer.WriteStartElement("PISQtde"); //PISQtd
                    writer.WriteElementString("CST", mi.pisCst);
                    writer.WriteElementString("qBCProd", formataValor(mi.qtd, 4));//quantidade de produto vendido
                    writer.WriteElementString("vAliqProd", formataValor(mi.pisAliq, 4));//aliquota do PIS (em reais)
                    writer.WriteElementString("vPIS", formataValor(mi.vlrPis, 2));
                    writer.WriteEndElement(); //PISQtd
                }
                //PIS isento
                //CST 04-aliquota zero, 06- aliquota zero, 07-Isenta, 08-sem incidencia de contribuicao, 09-suspensa de contri
                else if (mi.pisCst == "04" || mi.pisCst == "06" || mi.pisCst == "07" || mi.pisCst == "08" || mi.pisCst == "09")
                {
                    writer.WriteStartElement("PISNT"); //PISNT
                    writer.WriteElementString("CST", mi.pisCst);
                    writer.WriteEndElement(); //PISNT
                }
            }
            else
            {
                writer.WriteStartElement("PISNT"); //PISNT
                writer.WriteElementString("CST", "08");//08: não inscidencia de tributação
                writer.WriteEndElement(); //PISNT
            }

            writer.WriteEndElement();
            //fim TAG PIS
            #endregion



            //inicio TAG COFINS
            #region COFINS
            writer.WriteStartElement("COFINS");
            if (mi.vlrCofins > 0)
            {
                //CST 01 - aliquota,  02 - aliquota
                if (mi.cofinsCst == "01" || mi.cofinsCst == "02")
                {
                    vlrTotalCOFINS += mi.vlrCofins;

                    writer.WriteStartElement("COFINSAliq"); //COFINSAliq
                    writer.WriteElementString("CST", mi.cofinsCst);
                    writer.WriteElementString("vBC", formataValor(mi.bcCOFINS, 2));
                    writer.WriteElementString("pCOFINS", formataValor(mi.cofinsAliq, 2));
                    writer.WriteElementString("vCOFINS", formataValor(mi.vlrCofins, 2));
                    writer.WriteEndElement(); //COFINSAliq
                }
                //CST 03 - tributavel pela Quantidade
                else if (mi.cofinsCst == "03")
                {
                    vlrTotalCOFINS += mi.vlrCofins;

                    writer.WriteStartElement("COFINSQtde"); //COFINSQtde
                    writer.WriteElementString("CST", mi.cofinsCst);
                    writer.WriteElementString("qBCProd", formataValor(mi.qtd, 4)); //quantidade de PRODUTO
                    writer.WriteElementString("vAliqProd", formataValor(mi.cofinsAliq, 4)); //aliquota do COFINS (em reais)
                    writer.WriteElementString("vCOFINS", formataValor(mi.vlrCofins, 2));
                    writer.WriteEndElement(); //COFINSQtde
                }
                //COFINS isento
                //CST 04-aliquota zero, 06- aliquota zero, 07-Isenta, 08-sem incidencia de contribuicao, 09-suspensa de contri
                else if (mi.cofinsCst == "04" || mi.cofinsCst == "06" || mi.cofinsCst == "07"
                            || mi.cofinsCst == "08" || mi.cofinsCst == "09")
                {
                    writer.WriteStartElement("COFINSNT"); //PISNT
                    //writer.WriteElementString("CST", mi.cofinsCst);
                    writer.WriteElementString("CST", "08");
                    writer.WriteEndElement(); //PISNT
                }

            }
            else
            {
                writer.WriteStartElement("COFINSNT"); //PISNT
                writer.WriteElementString("CST", "08");//08: não inscidencia de tributação
                writer.WriteEndElement(); //PISNT
            }
            writer.WriteEndElement();
            //fim TAG COFINS
            #endregion

            writer.WriteEndElement();
            //fim TAG imposto
            writer.WriteEndElement();
            //fim TAG Detalhamento

            //valores totais da nota fiscal
            vlrTotalProduto = valorTotalItem;
            vlrTotalBC = mi.bcICMS;
            vlrTotalFrete = mi.vlrFrete;
            vlrTotalDesc = desconto;
            vlrTotalSeguro = mi.vlrSeguro;
            vlrTotalICMS = mi.vlrICMS;

            acumulaVlrTotalProduto += vlrTotalProduto;
            vlrTotalNF += vlrTotalProduto + vlrTotalFrete + vlrTotalSeguro + vlrTotalOutros + vlrTotalIPI;
            acumulaVlrTotalBC += vlrTotalBC;
            acumulaVlrTotalBCST += vlrTotalBCST;
            acumulaVlrTotalICMS += vlrTotalICMS;
            acumulaVlrTotalIcmsST += vlrTotalIcmsST;

        }

        //Item W  - Totais da NFE
        public void Escreve_total(XmlWriter writer1, Mov mov_dados) //Empresa emp, Cliente idClienteEmp, 
        {
            //inicio da TAG total
            writer1.WriteStartElement("total");

            //DESCONTOS
            vlrTotalDesc = mov_dados.vlrAcrescimo;
            vlrTotalNF = vlrTotalNF - vlrTotalDesc;


            //inicio da TAG icmsTot
            writer1.WriteStartElement("ICMSTot");
                writer1.WriteElementString("vBC", formataValor(acumulaVlrTotalBC, 2)); //base de calculo icms
                writer1.WriteElementString("vICMS", formataValor(acumulaVlrTotalICMS, 2));//valor do icms
                writer1.WriteElementString("vBCST", formataValor(acumulaVlrTotalBCST, 2));//base de calculo Substituição tributaria
                writer1.WriteElementString("vST", formataValor(acumulaVlrTotalIcmsST, 2));//valor do icms substituição tributaria
                writer1.WriteElementString("vProd", formataValor(acumulaVlrTotalProduto, 2));
                writer1.WriteElementString("vFrete", formataValor(vlrTotalFrete, 2));
                writer1.WriteElementString("vSeg", formataValor(vlrTotalSeguro, 2));

                //ruthy desconto
                writer1.WriteElementString("vDesc", formataValor(vlrTotalDesc, 2));

                writer1.WriteElementString("vII", formataValor(vlrTotalII, 2));
                writer1.WriteElementString("vIPI", formataValor(vlrTotalIPI, 2));
                writer1.WriteElementString("vPIS", formataValor(vlrTotalPIS, 2));
                writer1.WriteElementString("vCOFINS", formataValor(vlrTotalCOFINS, 2));
                writer1.WriteElementString("vOutro", formataValor(vlrTotalOutros, 2));

                writer1.WriteElementString("vNF", formataValor(vlrTotalNF, 2));
            //fim da TAG icmsTot
            writer1.WriteEndElement();
            //fim da TAG total
        writer1.WriteEndElement();
        }
        // }

        //Item X -  Transporte
        public void Escreve_transp(XmlWriter writer, Cliente cli, ClienteEndereco ce, ClienteVeiculo veiculo, ClienteVeiculo reboque, MovNFE nfe)
        {
            Utils.retiraCaracterEspecial(cli);
            Utils.retiraCaracterEspecial(ce);


            string endCompleto = string.Format("{0} {1} {2}", ce.logradouro, ce.numero, ce.bairro);

            int tipoTransp = 0;

            if (nfe.tipoTranspNFE == ENfeTipoTransporte.emitente)
                tipoTransp = 0;
            if (nfe.tipoTranspNFE == ENfeTipoTransporte.destinatario)
                tipoTransp = 1;

            writer.WriteStartElement("transp");//transp
                writer.WriteElementString("modFrete", tipoTransp.ToString());//modalidade do frete (0 - por conta emitente, 1 - por conta do destinatario)
                //inicio da TAG Transportadora

                writer.WriteStartElement("transporta");
                if (cli.tipo == EPesTipo.Juridica)
                {
                    writer.WriteElementString("CNPJ", cli.cpf_cnpj);
                    writer.WriteElementString("xNome", cli.apelido_razsoc);
                }
                else
                    if (cli.tipo == EPesTipo.Fisica)
                    {
                        writer.WriteElementString("CPF", cli.cpf_cnpj);
                        writer.WriteElementString("xNome", cli.nome);
                    }

                if (tipoTransp == 9)
                {
                    writer.WriteStartElement("transporta");
                    writer.WriteElementString("CNPJ", cli.cpf_cnpj);
                    writer.WriteElementString("xNome", cli.nome);
                }
                if (ce.inscr == string.Empty || ce.inscr == "ISENTO")
                    writer.WriteElementString("IE", "ISENTO");
                else
                    writer.WriteElementString("IE", ce.inscr);

                writer.WriteElementString("xEnder", endCompleto);
                writer.WriteElementString("xMun", ce.cidade);
                writer.WriteElementString("UF", ce.uf);
            writer.WriteEndElement();
            //fim TAG Transportadora
            
            //Inicio TAG veicTransp
            if (veiculo != null)
            {
                writer.WriteStartElement("veicTransp");
                    writer.WriteElementString("placa", veiculo.placaNumero);
                    writer.WriteElementString("UF", veiculo.placaUF);
                    if (veiculo.regNacTranspCarga != string.Empty)
                        writer.WriteElementString("RNTC", veiculo.regNacTranspCarga);//Registro nacional de transporte de carga
                writer.WriteEndElement();
            }
            // fim da TAG veicTransp
            //inicio TAG reboque
            if (reboque != null)
            {
                writer.WriteStartElement("reboque");
                    writer.WriteElementString("placa", reboque.placaNumero);
                    writer.WriteElementString("UF", reboque.placaUF);
                    if (reboque.regNacTranspCarga != string.Empty)
                        writer.WriteElementString("RNTC", reboque.regNacTranspCarga);//Registro nacional de transporte de carga
                writer.WriteEndElement();
            }
            //fim da TAG reboque

            //inicio da TAG volume
            writer.WriteStartElement("vol");
                writer.WriteElementString("qVol", nfe.volQuantidade.ToString());//quantidade
                if (nfe.volEspecie != string.Empty)
                    writer.WriteElementString("esp", nfe.volEspecie);//especie das mercadorias
                if (nfe.volMarca != string.Empty)
                    writer.WriteElementString("marca", nfe.volMarca);//marca das mercadorias
                if (nfe.volNumeracao != string.Empty)
                    writer.WriteElementString("nVol", nfe.volNumeracao);//numeração do volume transportado
                if (nfe.volPesoLiquido != 0)
                    writer.WriteElementString("pesoL", formataValor(nfe.volPesoLiquido, 3));//peso liquido
                if (nfe.volPesoBruto != 0)
                    writer.WriteElementString("pesoB", formataValor(nfe.volPesoBruto, 3));//peso bruto

                //inicio da TAG lacres
                /*
                writer.WriteStartElement("lacres");
                writer.WriteElementString("nLacre", "");
                writer.WriteEndElement();
                * */
                //fim da TAG lacres
            writer.WriteEndElement(); //vol
        writer.WriteEndElement(); //transp
        }

        public void Escreve_Fim(XmlWriter writer)
        {
            writer.WriteEndElement();   //infNFe
            writer.WriteEndElement();   //NFe
            writer.WriteEndDocument(); //documento
        }

        public void Escreve_infAdicXML(XmlWriter writer, string adicionais, MovNFE nfe)
        {
            if (nfe.fatura != "")
                Utils.retiraCaracterEspecial(nfe);

            adicionais += " - " + "FORMA DE PAGAMENTO: " + nfe.formaPgtoNFE.ToString().ToUpper() + fatura;

            Utils.retiraCaracterEspecial(adicionais);
            Utils.verificaCaracterEspecial(adicionais);


            writer.WriteStartElement("infAdic");
                writer.WriteElementString("infAdFisco", adicionais);
            writer.WriteEndElement();
        }
        /*
                string valor = f.GetValue(obj).ToString().ToUpper();
                valor = Regex.Replace(valor, "%", " PORCENTO");
                valor = Regex.Replace(valor, "R$", "VALOR:");
                valor = Regex.Replace(valor, "$", "VALOR:");
                valor = Regex.Replace(valor, "#", "");
                valor = Regex.Replace(valor, "&", "");
                valor = Regex.Replace(valor, "¨", "");
                f.SetValue(obj, valor);
            }
        }*/


        private string formataValor(double valor, int decimais)
        {
            string ret = "";
            if (decimais == 4)
                ret = string.Format("{0:0.0000}", valor).Replace(",", ".");
            if (decimais == 3)
                ret = string.Format("{0:0.000}", valor).Replace(",", ".");
            if (decimais == 2)
                ret = string.Format("{0:0.00}", valor).Replace(",", ".");
            return ret;
        }

        private string formataDataNFe(string data)
        {
            if (data.Length < 8)
                return "";

            DateTime dt = DateTime.Parse(data);
            string ret = "";
            ret = dt.ToString("yyyy-MM-dd");
            return ret;
        }

        /*private void cancelaNota(XmlWriter writer, string just, string data, string chave, string amb,
                                string codUF, string cnpj, string modelo, string serie, string numNFe)
        {
            string AAMM = "";
            DateTime dt = DateTime.Parse(data);
            AAMM = dt.ToString("YYMM");

            writer.WriteStartElement("cancNFe");
                writer.WriteStartElement("infCanc Id="+'"'+chave);
                    writer.WriteElementString("tpAmb",amb);
                    writer.WriteElementString("xServ","CANCELAR");
                    writer.WriteElementString("chNFe",chave);
                    writer.WriteElementString("nProt",gerarProt());
                    writer.WriteElementString("xJust",just);
                writer.WriteEndElement();
            writer.WriteEndElement();


            /*<infCanc Id="ID35080699999090910270550000000000011234567890"> 
                <tpAmb>2</tpAmb> 
                <xServ>CANCELAR</xServ> 
                <chNFe>35080699999090910270550000000000011234567890</chNFe> 
                <nProt>135080000000001</nProt> 
                <xJust>Teste do WS de Cancelamento</xJust> 
                <tpEmis>1</tpEmis>                          (OPCIONAL) 
            </infCanc>*/
        /*}

        private string gerarProt(string entidade, string uf, string dataANO2dig)
        {
            //entidade 1-SEFAZ 2-Receita Federal
            string protocolo = "";



            return protocolo;
        }*/
    }
}