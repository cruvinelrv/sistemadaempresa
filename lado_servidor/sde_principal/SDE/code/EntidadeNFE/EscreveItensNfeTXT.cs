using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.CamadaServico;
using SDE.Entidade;
using SDE.Enumerador;

using System.IO;
using System.Xml;

namespace SDE.EntidadeNFE
{
    public class EscreveItensNfeTXT
    {
        private const string nomePais = "BRASIL";
        private const int codPais = 1058;

        private double vlrTotalBC = 0;
        private double vlrTotalICMS = 0;
        private double vlrTotalBCST = 0;
        private double vlrTotalIcmsST = 0;
        private double vlrTotalProduto = 0;
        private double vlrTotalNF = 0;

        private double vlrTotalFrete = 0;
        private double vlrTotalSeguro = 0;
        private double vlrTotalDesc = 0;

        private double vlrTotalII = 0;
        private double vlrTotalIPI = 0;
        private double vlrTotalPIS = 0;
        private double vlrTotalCOFINS = 0;
        private double vlrTotalOutros = 0;


        public void Escreve_Inicio(StringBuilder sb, string chave)
        {
            sb.AppendLine("NOTAFISCAL|1");
            sb.Append("A|");
            sb.Append("1.10|"); //layoute
            sb.AppendLine("NFe" + chave + "|"); //id    
        }

        public void Escreve_Fim(StringBuilder sb)
        {
            /*
            writer.WriteEndElement();   //infNFe
            writer.WriteEndElement();   //NFe
            writer.WriteEndDocument(); //documento
            
            writer.Flush();
            writer.Close();
            */
        }


        //ITEM B identificacao da NFE
        public void Escreve_ide(StringBuilder sb, Mov mov, MovNFE nfe, MovNFE nfeRF, string codUF, string codMunicipio)
        {
            string tipoNF = string.Empty;
            string naturezaOP = string.Empty;
            int formaPgto = 0;
            int finalidade = 0;
            int ambiente = 2;

            //tipo da nota fiscal
            //0-entrada   1-saida
            if (mov.resumo == EMovResumo.entrada)
                tipoNF = "0";
            else
                tipoNF = "1";

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

            //ambiente nfe 1-producao, 2-homologacao
            //ambiente = 2;
            
            if (nfe.ambienteNFE == ENfeAmbiente.producao)
                ambiente = 1;
            else
                ambiente = 2; 
            

            sb.Append("B|");
            sb.Append(codUF + "|");//codigo da Uf do remetente
            sb.Append(nfe.codNumericoNFE.ToString() + "|"); //_______codigo da chave de acesso
            sb.Append(naturezaOP.ToString() + "|");//natureza da operação (venda, compra, devolucao)
            sb.Append(formaPgto.ToString() + "|");//indicador pagamaneto //0-a vista, 1-a prazo, 2-outros
            sb.Append("55|");

            string numSerie =  (nfe.serieNota != null) ? nfe.serieNota.ToString().PadLeft(3,'0'): "000";
            sb.Append(numSerie+"|");//serie do documento fiscal

            string numNota = nfe.numeroNota.ToString().PadLeft(9, '0');
            sb.Append(numNota + "|");//numero do documento Fiscal
            sb.Append(formataDataNFe(mov.dthrMovEmissao) + "|");
            if(nfe.dtSaiEnt != null && nfe.dtSaiEnt!= string.Empty)
                sb.Append(formataDataNFe(nfe.dtSaiEnt) + "|");
            else
                sb.Append(formataDataNFe(mov.dthrMovEmissao) + "|");
            sb.Append(tipoNF + "|");//0 - entrada , 1- saida
            sb.Append(codMunicipio + "|"); //codido de municipio do fator gerador

            sb.Append("1|");//tipo impressao  1-Retrato 2-Paisagem
            sb.Append("1|");//1-normal, 2- Contigencia, 3-Contigencia, 4-Contigencia, 5-Contigencia
            sb.Append("|");//_______Digito verificador da chave de acesso
            sb.Append(ambiente.ToString() + "|");//_____identificação do ambiente; 1- Produção, 2- homologação //só homologacao
            sb.Append(finalidade.ToString() + "|");//____finalidade da NFE 1-normal 2-complementar  3-ajuste
            sb.AppendLine("0|");//Processo de emissão, 0-aplicativo contribuinte, 1-avulsa pelo fisco, 2-atraves do fisco, 3-
            // sb.AppendLine("10|");//___versão do aplicativo emissor da nfe



            //determiana nfe Referenciada , quando finalidade for ajuste ou complemento
            if (nfe.finalidadeNFE != ENfeFinalidade.normal && nfeRF != null)
            {
                sb.Append("B13|");
                sb.AppendLine(nfeRF.chaveAcessoNFE.ToString() + "|");
            }

        }

        //Item C - Emitente
        public void Escreve_emit(StringBuilder sb, Cliente cli, ClienteEndereco ce, ClienteContato cc)
        {
            string cnpj_cpf = cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            sb.Append("C|");
            sb.Append(cli.apelido_razsoc+ "|");//razao social
            sb.Append(cli.nome + "|");//nome fantasia

            sb.Append(ce.inscr + "|");
            sb.Append("|");//inscricao estadual do Substituto tributario
            //campos de nfe conjugada, prestação de serviço e peças
            sb.Append("|"); //IM
            sb.AppendLine("|");//CNAE

            
            if (cli.cpf_cnpj.Length == 14)
            {
                sb.Append("C02|");
                sb.AppendLine(cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "") + "|");                
            }
            else
            {
                sb.Append("C02a|");
                sb.AppendLine(cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "") + "|");
            }

            //inicio TAG endereco
            sb.Append("C05|");

            sb.Append(ce.logradouro + "|");
            sb.Append(ce.numero.ToString() + "|");
            sb.Append(ce.complemento + "|");
            sb.Append(ce.bairro + "|");
            sb.Append(ce.cidadeIBGE + "|");
            sb.Append(ce.cidade + "|");
            sb.Append(ce.uf + "|");
            sb.Append(ce.cep.Replace("-", "").Replace(".", "") + "|");
            sb.Append(codPais.ToString() + "|");
            sb.Append(nomePais + "|");
            //telefone
            sb.AppendLine((cc != null) ? cc.valor.Replace("(", "").Replace(")", "").Replace("-", "") + "|" : "|");

            /*
            sb.AppendLine(
                (ce.fone != null)
                ? ce.fone.Replace("(", "").Replace(")", "").Replace("-", "") + "|"
                : "|"
                );
             * */
            //final TAG Emitente
        }
        
        //Item D -  destinatario
        public void Escreve_dest(StringBuilder sb, Cliente cli, ClienteEndereco ce, ClienteContato cc)
        {
            //Inicio TAG Destinatario
            string cnpj_cpf = cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            sb.Append("E|");

            if(cnpj_cpf.Length == 14)
                sb.Append(cli.apelido_razsoc + "|");
            else
                sb.Append(cli.nome + "|");
            //inscricao Estadual IE
            if (ce.inscr == null || ce.inscr == "ISENTO" || ce.inscr == string.Empty)
                sb.AppendLine("ISENTO|");
            else
                sb.AppendLine(ce.inscr + "|");

            
            if (cli.cpf_cnpj.Length == 14)
            {
                sb.Append("E02|");
                sb.AppendLine(cnpj_cpf + "|");
            }
            else
            {
                sb.Append("E03|");
                sb.AppendLine(cnpj_cpf + "|");
            }

            //Inicio TAG Endereco
            sb.Append("E05|");

            sb.Append(ce.logradouro + "|");
            sb.Append(ce.numero.ToString() + "|");
            sb.Append(ce.complemento + "|");
            sb.Append(ce.bairro + "|");
            sb.Append(ce.cidadeIBGE + "|");
            sb.Append(ce.cidade + "|");
            sb.Append(ce.uf + "|");
            sb.Append(ce.cep.Replace("-", "").Replace(".", "") + "|");
            sb.Append(codPais.ToString() + "|");
            sb.Append(nomePais + "|");
            //telefone
            sb.AppendLine((cc != null) ? cc.valor.Replace("(", "").Replace(")", "").Replace("-", "") + "|" : "|");

            //sb.AppendLine((ce.fone != null) ? ce.fone.Replace("(", "").Replace(")", "").Replace("-", "") + "|" : "|");
        }
        /*
        //Item F - identificação do local de retirada
        public void Escreve_retirada(StringBuilder sb, string cnpj, PessoaEndereco pe)
        {
            //incio TAG local REtirada
            writer.WriteStartElement("retirada");
            writer.WriteElementString("CNPJ", cnpj);
            writer.WriteElementString("xLgr", pe.logradouro);
            writer.WriteElementString("nro", pe.numero.ToString());
            writer.WriteElementString("xCpl", pe.complemento);
            writer.WriteElementString("xBairro", pe.bairro);
            writer.WriteElementString("cMun", pe.cidadeIBGE);
            writer.WriteElementString("xMun", pe.cidade);
            writer.WriteElementString("UF", pe.uf);
            writer.WriteEndElement();
            //fim TAG local Retirada
        }

        //Item G - identificação do local de entrega
        public void Escreve_entrega(StringBuilder sb, string cnpj, PessoaEndereco pe)
        {
            //inicio TAG local de Entrega
            writer.WriteStartElement("entrega");
            writer.WriteElementString("CNPJ", cnpj);
            writer.WriteElementString("xLgr", pe.logradouro);
            writer.WriteElementString("nro", pe.numero.ToString());
            writer.WriteElementString("xCpl", pe.complemento);
            writer.WriteElementString("xBairro", pe.bairro);
            writer.WriteElementString("cMun", pe.cidadeIBGE);
            writer.WriteElementString("xMun", pe.cidade);
            writer.WriteElementString("UF", pe.uf);
            writer.WriteEndElement();
            //fim TAG local de Entrega
        }
        */
        //Item H - Detalhamento de produto e serviços
        //Item I - Produtos e serviços da NFE
        public void Escreve_det(StringBuilder sb, MovItem mi, Mov mov, int numItem, MovNfeVeiculo nfeVeiculo)
        {
            //Item 
            Item it = mi.__item;
            //itemEmpAliq
            ItemEmpAliquotas itAliq = it.__ie.__aliquotas;
            double vlrUnit = 0;
            if (mov.resumo == EMovResumo.saida || mov.tipo == EMovTipo.entrada_devolucao)
                vlrUnit = mi.vlrUnitVendaFinal;
            else               
                vlrUnit = mi.vlrUnitCompra;

            //determina nacional
            int origem = (int)it.origem;




            //valores do movItem
            double valorTotalItem = vlrUnit * mi.qtd;
            double valorIcms = 0;
            double valorIPI = 0;
            double valorPIS = 0;
            double valorCOFINS = 0;

            //inicio TAG Detelhamento
            sb.Append("H|");
            //colocar obs no item 1
            if (numItem == 1)
            {
                sb.Append(numItem.ToString() + "|");
                //sb.Append(it.complAplic.Replace("\r",""));
                sb.Append(mi.estoque_identificador.Replace("\r", ""));
            }
            else
            {
                sb.Append(numItem.ToString());
            }
            sb.AppendLine("|");

            //inicio TAG produto
            sb.Append("I|");
            sb.Append(it.id.ToString() + "|");
            sb.Append("|"); //CEAN
            sb.Append(it.nome +" "+ mi.estoque_identificador + "|");
            sb.Append("|"); //NCM
            sb.Append("|"); //EXTIPI
            sb.Append("|"); //GENERO
            sb.Append(mi.cfop.ToString() + "|");
            sb.Append(it.unidMed.ToString() + "|");
            sb.Append(formataValor(mi.qtd, 4) + "|");
            sb.Append(formataValor(vlrUnit, 4) + "|");
            sb.Append(formataValor(valorTotalItem, 2) + "|");
            sb.Append("|");
            sb.Append(it.unidMed.ToString() + "|");
            sb.Append(formataValor(mi.qtd, 4) + "|");
            sb.Append(formataValor(vlrUnit, 4) + "|");

            //valores frete , seguro, desc
            if (mi.vlrFrete != 0)
                sb.Append(formataValor(mi.vlrFrete, 2) + "|");
            else
                sb.Append("|");

            if (mi.vlrSeguro != 0)
                sb.Append(formataValor(mi.vlrSeguro, 2) + "|");
            else
                sb.Append("|");

            //if (mi.vlrDesc > 0)
              //  sb.AppendLine(formataValor(mi.vlrDesc, 2) + "|");     RETIREI DESCONTO
            //else
                sb.AppendLine("|");
            //fim TAG produto
            //inicio TAG impostos
            #region nfe Veiculo

            if (nfeVeiculo != null)
            {
                //exemplo
                //J|0|JSAAL43A682107359|01|AMARELA|450|42|167|251|001|2|J404-107375|0|0||2008|2007|1|17|1|1|1|0|
                //inicio da TAG Veiculos novos
                sb.Append("J|");
                //inicio da TAG icmsTot
                sb.Append(nfeVeiculo.tipoOperacao + "|");
                sb.Append(nfeVeiculo.chassi + "|");
                sb.Append(nfeVeiculo.codCor + "|");
                sb.Append(nfeVeiculo.desCor + "|");
                sb.Append(nfeVeiculo.potencia + "|");
                sb.Append(nfeVeiculo.cm3 + "|");
                sb.Append(formataValor(nfeVeiculo.pesoL, 2) + "|");
                sb.Append(formataValor(nfeVeiculo.pesoB, 2) + "|");
                sb.Append(nfeVeiculo.serie + "|");
                sb.Append(nfeVeiculo.tipoCombustivel + "|");
                sb.Append(nfeVeiculo.numeroMotor + "|");
                sb.Append(nfeVeiculo.cmkg + "|");
                sb.Append(nfeVeiculo.distanciaEixos + "|");
                sb.Append("|");//renavan
                sb.Append(nfeVeiculo.anoModel + "|");
                sb.Append(nfeVeiculo.anoFab + "|");
                sb.Append(nfeVeiculo.tipoPintura + "|");
                sb.Append(nfeVeiculo.tipoVeiculo + "|");
                sb.Append(nfeVeiculo.especie + "|");
                sb.Append(nfeVeiculo.condicaoVIN + "|");
                sb.Append(nfeVeiculo.condicaoVeic + "|");
                if (nfeVeiculo.condicaoVIN == 2)//se nacional, escreve marca e modelo
                    sb.Append(nfeVeiculo.codMarcaModelo+"||");
                else
                    sb.Append("0");
                sb.AppendLine();
            }

            #endregion



            #region ICMS
            //inicio da TAG ICMS
            sb.AppendLine("M|");
            sb.AppendLine("N|");


            //verificacao do icms          
            mi.icmsCst = (mi.icmsCst == null || mi.icmsCst == "") ? "000" : mi.icmsCst;
            if (mi.icmsCst.Equals("000"))
            {
                vlrTotalICMS += mi.vlrICMS;
                vlrTotalBC += mi.bcICMS;

                sb.Append("N02|");
                sb.Append(origem.ToString() + "|");//0-nacional, 1-Estrangeiro importação direta, 2-Estrangeiro no mercado interno
                sb.Append("00|");
                sb.Append("3|");//0-MArgem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                sb.Append(formataValor(mi.bcICMS, 2) + "|");
                sb.Append(formataValor(mi.icmsAliq, 2) + "|");
                sb.AppendLine(formataValor(mi.vlrICMS, 2) + "|");
            }
            else if (mi.icmsCst.Equals("020"))
            {
                vlrTotalICMS += mi.vlrICMS;
                vlrTotalBC += mi.bcICMS;
                double pReducao = (mi.icmsAliq * 100) / mi.icmsAliqPadrao;

                sb.Append("N03|");
                sb.Append(origem.ToString() + "|");//0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                sb.Append("20|");
                sb.Append("3|");//0-Margem Valor agregado(%), 1-pauta(valor), 2-preco tabelado(valor), 3 -valor operacao
                sb.Append(formataValor(pReducao, 2) + "|");//percentual de redução da BC
                sb.Append(formataValor(mi.bcICMS, 2) + "|");
                sb.Append(formataValor(mi.icmsAliq, 2) + "|");
                sb.AppendLine(formataValor(mi.vlrICMS, 2) + "|");

            }
            else if (mi.icmsCst.Equals("040")) //isento, nao Tributavel
            {
                sb.Append("N06|");
                sb.Append(origem.ToString() + "|");// 0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                sb.AppendLine("40|"); // 40-isento, 41-não tributado, 50-suspensão

            }
            else if (mi.icmsCst.Equals("060"))//substituição tributaria
            {
                vlrTotalIcmsST += mi.vlrIcmsSubstTrib;
                vlrTotalBCST += mi.bcIcmsSubstTrib;

                sb.Append("N08|");
                sb.Append(origem.ToString() + "|");// 0-nacional, 1-Estrangeiro improtação direta, 2-Estrangeiro no mercado interno
                sb.Append("60|");//substituicao tributaria
                sb.Append(formataValor(mi.bcIcmsSubstTrib, 2) + "|");
                sb.AppendLine(formataValor(mi.vlrIcmsSubstTrib, 2) + "|");
            }
            //fim TAG ICMS
            #endregion

            #region IPI -  verificar codigo
            //inicio da TAG IPI
            sb.Append("O|");
            if (itAliq.ipiClasseEnquad == null || itAliq.ipiClasseEnquad.Length < 5)
                sb.Append("|");
            else
                sb.Append(itAliq.ipiClasseEnquad + "|");

            string cnpjProduto = "";
            if(itAliq.ipiCNPJ != null)
                itAliq.ipiCNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
            sb.Append(cnpjProduto + "|");//cnpj Produtor

            string codSelo = "";
            if (itAliq.ipiCodSelo != null)
                codSelo = itAliq.ipiCodSelo;
            sb.Append(codSelo + "|");//codigo do selo

            string qtdSelo = "";
            if (itAliq.ipiQtdSelo != null)
                qtdSelo = itAliq.ipiQtdSelo.ToString();
            sb.Append(qtdSelo + "|");//quantidade do selo

            //verificação do codEnquadramento
            if (itAliq.ipiCodEnquad == null || itAliq.ipiCodEnquad.Length != 3)
                sb.AppendLine("999|");
            else
                sb.AppendLine(itAliq.ipiCodEnquad + "|");

            if (mi.ipiCst == null){
                if (mov.resumo == EMovResumo.entrada)
                    mi.ipiCst = "02";  //entrada isenta
                else
                    mi.ipiCst = "52";  //saida isenta
            }
            //00-entrada com recuperação de credito, 49-outras entradas, 50-saida tributada,  99-outras saidas
            if (mi.ipiCst == "00" || mi.ipiCst == "49" || mi.ipiCst == "50" || mi.ipiCst == "99")
            {
                sb.Append("O07|");
                sb.Append(mi.ipiCst + "|");
                //informar campos (O10 O13 O14) se IPI for cobrado por aliquota
                if (itAliq.ipiTipoCalculo == ECalculoIpiTipo.percentual)
                {
                    vlrTotalIPI += mi.vlrIPI;
                    sb.AppendLine(formataValor(mi.vlrIPI, 2) + "|");

                    sb.Append("O10|");
                    sb.Append(formataValor(mi.bcIPI, 2) + "|");//valor BC
                    sb.AppendLine(formataValor(mi.ipiAliq, 2) + "|");//pesrcentual da aliquota

                }
                //informar campos (O11 O12 O14) se IPI for cobrado por unidade
                else
                {
                    vlrTotalIPI += mi.vlrIPI;
                    sb.AppendLine(formataValor(mi.vlrIPI, 2) + "|");

                    sb.Append("O11|");
                    sb.Append(formataValor(mi.qtd, 4) + "|"); //quantidade de produto
                    sb.AppendLine(formataValor(mi.ipiAliq, 4) + "|");//valor por unidade

                }
            }
            //01-entrada tributada zero, 02-entrada isenta, 03- entrada nao-tributada, 04-entrada imune, entrada com suspensão
            //51-saida tributada zero, 52-saida isenta, 53 saida não tributada, 54-saida imune, 55- saida com suspensão
            else
            {
                sb.Append("O08|");
                if(mi.ipiCst == null || mi.ipiCst == string.Empty)
                    sb.AppendLine("01|");
                else
                    sb.AppendLine(mi.ipiCst + "|");
            }
            //fim da TAG IPI
            #endregion

            #region PIS
            //inicio TAG Pis
            sb.AppendLine("Q|");
            if (mi.pisCst == null)
                mi.pisCst = "04";

            //CST 01, 02 - tributado pela aliquota
            if (mi.pisCst == "01" || mi.pisCst == "02")
            {
                vlrTotalPIS += mi.vlrPis;
                sb.Append("Q02|");

                sb.Append(mi.pisCst + "|");
                sb.Append(formataValor(mi.bcPIS, 2) + "|");
                sb.Append(formataValor(mi.pisAliq, 2) + "|");
                sb.AppendLine(formataValor(mi.vlrPis, 2) + "|");
            }
            //Cst 03  -  tributado pela quantidade
            else if (mi.pisCst == "03")
            {
                valorPIS = mi.qtd * mi.pisAliq;
                vlrTotalPIS += valorPIS;

                sb.Append("Q03|");
                sb.Append(mi.pisCst + "|");
                sb.Append(formataValor(mi.qtd, 4) + "|");//quantidade de produto vendido
                sb.Append(formataValor(mi.pisAliq, 4) + "|");//aliquota do PIS (em reais)
                sb.AppendLine(formataValor(mi.vlrPis, 2) + "|");

            }
            //PIS isento
            //CST 04-aliquota zero, 06- aliquota zero, 07-Isenta, 08-sem incidencia de contribuicao, 09-suspensa de contri
            else
            {
                sb.Append("Q04|");
                if (mi.pisCst ==  null || mi.pisCst == string.Empty)
                    sb.AppendLine("04|");
                else
                    sb.AppendLine(mi.pisCst + "|");
            }

            //fim TAG PIS
            //inicio TAG COFINS
            #endregion

            #region COFINS
            sb.AppendLine("S|");
            if (mi.cofinsCst == null)
                mi.cofinsCst = "04";
            //CST 01 - aliquota,  02 - aliquota
            if (mi.cofinsCst == "01" || mi.cofinsCst == "02")
            {
                vlrTotalCOFINS += mi.vlrCofins;
                sb.Append("S02|");

                sb.Append(mi.cofinsCst + "|");
                sb.Append(formataValor(mi.bcCOFINS, 2) + "|");
                sb.Append(formataValor(mi.cofinsAliq, 2) + "|");
                sb.AppendLine(formataValor(mi.vlrCofins, 2) + "|");
            }
            //CST 03 - tributavel pela Quantidade
            else if (mi.cofinsCst == "03")
            {
                vlrTotalCOFINS += mi.vlrCofins;
                sb.Append("S03|");
                sb.Append(mi.cofinsCst + "|");
                sb.Append(formataValor(mi.qtd, 4) + "|"); //quantidade de PRODUTO
                sb.Append(formataValor(mi.cofinsAliq, 4) + "|"); //aliquota do COFINS (em reais)
                sb.AppendLine(formataValor(mi.vlrCofins, 2) + "|");
            }
            //COFINS isento
            //CST 04-aliquota zero, 06- aliquota zero, 07-Isenta, 08-sem incidencia de contribuicao, 09-suspensa de contri
            // else if (mi.cofinsCst == "04" || mi.cofinsCst == "06" || mi.cofinsCst == "07"
            //            || mi.cofinsCst == "08" || mi.cofinsCst == "09")
            else
            {
                sb.Append("S04|");
                if(mi.cofinsCst == null || mi.cofinsCst == string.Empty)
                    sb.AppendLine("04|");
                else
                    sb.AppendLine(mi.cofinsCst + "|");
            }

            //fim TAG COFINS

            #endregion


            //fim TAG imposto

            //fim TAG Detalhamento

            //valores totais da nota fiscal
            vlrTotalProduto += valorTotalItem;
            vlrTotalFrete += mi.vlrFrete;
            //if (mi.vlrDesc > 0)
              //  vlrTotalDesc += mi.vlrDesc;  RETIREI DESCONTO                         //AKI
            vlrTotalSeguro += mi.vlrSeguro;
        }

        //Item W  - Totais da NFE
        public void Escreve_total(StringBuilder sb)
        {
            vlrTotalNF = vlrTotalProduto + vlrTotalFrete + vlrTotalSeguro
                         + vlrTotalOutros - vlrTotalDesc;

            //inicio da TAG total
            sb.AppendLine("W|");
            sb.Append("W02|");
            //inicio da TAG icmsTot
            sb.Append(formataValor(vlrTotalBC, 2) + "|"); //base de calculo icms
            sb.Append(formataValor(vlrTotalICMS, 2) + "|");//valor do icms
            sb.Append(formataValor(vlrTotalBCST, 2) + "|");//base de calculo Substituição tributaria
            sb.Append(formataValor(vlrTotalIcmsST, 2) + "|");//valor do icms substituição tributaria
            sb.Append(formataValor(vlrTotalProduto, 2) + "|");
            sb.Append(formataValor(vlrTotalFrete, 2) + "|");
            sb.Append(formataValor(vlrTotalSeguro, 2) + "|");
            sb.Append(formataValor(vlrTotalDesc, 2) + "|");
            sb.Append(formataValor(vlrTotalII, 2) + "|");
            sb.Append(formataValor(vlrTotalIPI, 2) + "|");
            sb.Append(formataValor(vlrTotalPIS, 2) + "|");
            sb.Append(formataValor(vlrTotalCOFINS, 2) + "|");
            sb.Append(formataValor(vlrTotalOutros, 2) + "|");
            sb.AppendLine(formataValor(vlrTotalNF, 2) + "|");
        }

        //Item X -  Transporte
        public void Escreve_transp(StringBuilder sb, Cliente cli, ClienteEndereco ce, ClienteVeiculo veiculo, ClienteVeiculo reboque, MovNFE nfe)
        {            
            string cnpf = cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            
            int tipoTransp = 0;
            if (nfe.tipoTranspNFE == ENfeTipoTransporte.emitente)
                tipoTransp = 0;
            else
                tipoTransp = 1;


            sb.Append("X|");
            sb.AppendLine(tipoTransp.ToString()+"|");//modalidade do frete (0 - por conta emitente, 1 - por conta do destinatario)
            //inicio da TAG Transportadora
            sb.Append("X03|");
            sb.Append(cli.nome + "|");
            //inscricao Estadual IE
            if (ce == null || ce.inscr == null || ce.inscr == "ISENTO" || ce.inscr == string.Empty)
                sb.Append("ISENTO|");
            else
                sb.Append(ce.inscr + "|");

            if (ce != null)
            {
                string endCompleto = string.Format("{0} {1} {2}", ce.logradouro, ce.numero, ce.bairro);
                sb.Append(endCompleto + "|");
                sb.Append(ce.uf + "|");
                sb.AppendLine(ce.cidade + "|");
            }
            
            if (cli.cpf_cnpj.Length == 14)
            {
                //cnpj
                sb.Append("X04|");
                sb.AppendLine(cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "") + "|");
            }
            else
            {
                //cpf
                sb.Append("X05|");
                sb.AppendLine(cli.cpf_cnpj.Replace(".", "").Replace("/", "").Replace("-", "") + "|");
            }
            //fim TAG Transportadora
            //Inicio TAG veicTransp
            if (veiculo != null)
            {
                sb.Append("X18|");
                sb.Append(veiculo.placaNumero.Replace("-", "") + "|");
                sb.Append(veiculo.placaUF + "|");
                sb.AppendLine(veiculo.regNacTranspCarga + "|");//Registro nacional de transporte de carga
            }
            // fim da TAG veicTransp
            //inicio TAG reboque
            if (reboque != null)
            {
                sb.Append("X22|");
                sb.Append(reboque.placaNumero.Replace("-", "") + "|");
                sb.Append(reboque.placaUF + "|");
                sb.AppendLine(reboque.regNacTranspCarga + "|");//Registro nacional de transporte de carga
            }
            //fim da TAG reboque
            //inicio da TAG volume
            sb.Append("X26|");
            sb.Append(nfe.volQuantidade.ToString() + "|");//quantidade
            sb.Append(nfe.volEspecie + "|");//especie das mercadorias
            sb.Append(nfe.volMarca + "|");//marca das mercadorias
            sb.Append(nfe.volNumeracao + "|");//numeração do volume transportado
            sb.Append(formataValor(nfe.volPesoLiquido, 3) + "|");//peso liquido
            sb.AppendLine(Utils.apenasNumeros(nfe.volPesoBruto.ToString()) + "|");//peso bruto
            //inicio da TAG lacres
            sb.Append("X33|");
            sb.AppendLine("|");
        }

        //item Y, fatura
        public void Escreve_fatura(StringBuilder sb, string fatura)
        {
            if (fatura == null || fatura.Trim().Length == 0)
                return;
            else if (fatura.Length > 60)
                fatura = fatura.Substring(0, 60);
            sb.AppendLine("Y|");
            sb.Append("Y02|");
            sb.Append(fatura);
            sb.AppendLine("|");
        }
        public void Escreve_infAdic(StringBuilder sb, string adicionais)
        {
            if (adicionais == null || adicionais.Trim().Length == 0)
                return;

            adicionais = adicionais.Replace("\n", "").Replace("\r", "");

            sb.Append("Z|"); //infAdic
            sb.Append("|");//fisco
            sb.AppendLine(adicionais + "|");//contribuinte
        }

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

            string ano = dt.Year.ToString().PadLeft(4, '0');
            string mes = dt.Month.ToString().PadLeft(2, '0');
            string dia = dt.Day.ToString().PadLeft(2, '0');

            return string.Format("{0}-{1}-{2}", ano, mes, dia);
        }

        public string gerarChave(string codUF, string dtEmissao, string cnpj,
            string modelo, string serie, string numDoc, string codNumerico, string digVerificador)
        {
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            DateTime dt = DateTime.Parse(dtEmissao);
            string dtvalor = dt.Year.ToString().Substring(2, 2) + dt.Month.ToString().PadLeft(2, '0');

            string valor = codUF + dtvalor  + cnpj + modelo + serie + numDoc + codNumerico + digVerificador;

            return valor;
        }



        public int gerarDV(string codUF, string dtEmissao, string cnpj,
            string modelo, string serie, string numDoc, string codNumerico)
        {

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            DateTime dt = DateTime.Parse(dtEmissao);
            string dtvalor = dt.Year.ToString().Substring(2,2) + dt.Month.ToString().PadLeft(2, '0');

            string valor = codUF + dtvalor + cnpj + modelo + serie + numDoc + codNumerico;

            int cont = 4;
            int valorTotal = 0;
            foreach (char c in valor)
            {
                int dig = int.Parse(c.ToString());
                valorTotal += dig * cont;
                if (cont != 2)
                    cont--;
                else
                    cont = 9;

            }
            int resto = valorTotal % 11;
            if (resto == 0 || resto == 1)
                return 0;
            else
                return 11 - resto;
        }


    }
}
