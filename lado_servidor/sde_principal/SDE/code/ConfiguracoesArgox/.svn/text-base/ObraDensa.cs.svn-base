using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using System.Text;
using System.Globalization;
using Db4objects.Db4o.Query;
using System.IO;
using Db4objects.Db4o;

namespace SDE.code.ConfiguracoesArgox
{
    public class ObraDensa
    {
        /*
         * ponto superior da etiqueta eixo y: 0380 (valor podendo variar para menos, de acordo com a fonte utilizada, para que não ultrapasse os limites da etiqueta)
         * ponto inferior da etiqueta eixo y: 0010 
         * 
         * ponto inicial da etiqueta 1 eixo x: 0270 
         * ponto inicial da etiqueta 2 eixo x: 0600
         * ponto inicial da etiqueta 3 eixo x: 0930
         * */

        /*NÚMERO MÁXIMO DE CARACTERES POR LINHA: 24*/

        private const string E1_NOME_EMPRESA = "221100002700270";
        private const string E1_CNPJ_EMPRESA = "221100003800240";
        private const string E1_LABEL_VISTA = "221100003800190A VISTA:";
        private const string E1_PRECO_VISTA = "231100002600190";
        private const string E1_PRECO_PRAZO = "221100003800160A PRAZO:";
        private const string E1_CODIGO_BARRAS = "2e1200003400060";
        private const string E1_RF_UNICA = "231100003400010";

        private const string E2_NOME_EMPRESA = "221100002700600";
        private const string E2_CNPJ_EMPRESA = "221100003800570";
        private const string E2_LABEL_VISTA = "221100003800520A VISTA:";
        private const string E2_PRECO_VISTA = "231100002600520";
        private const string E2_PRECO_PRAZO = "221100003800490A PRAZO:";
        private const string E2_CODIGO_BARRAS = "2e1200003400390";
        private const string E2_RF_UNICA = "231100003400340";

        private const string E3_NOME_EMPRESA = "221100002700930";
        private const string E3_CNPJ_EMPRESA = "221100003800900";
        private const string E3_LABEL_VISTA = "221100003800850A VISTA:";
        private const string E3_PRECO_VISTA = "231100002600850";
        private const string E3_PRECO_PRAZO = "221100003800820A PRAZO:";
        private const string E3_CODIGO_BARRAS = "2e1200003400720";
        private const string E3_RF_UNICA = "231100003400670";

        private const string STX = "STX";
        private static string CR = char.ConvertFromUtf32(013);

        public static void EscreveEtiqueta(int idCorp, int idEmp, List<MovItem> listaMovItem, string portaCOM)
        {
            IObjectContainer db = AppFacade.get.conexaoBanco.get(idCorp);
            IQuery query;

            StringBuilder sbScript = new StringBuilder();

            int nEtiqueta = 0;
            int qtd = 0;

            Empresa emp = null;
            query = db.Query();
            query.Constrain(typeof(Empresa));
            query.Descend("id").Constrain(idEmp);
            emp = query.Execute()[0] as Empresa;

            Cliente cliEmp = null;
            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(emp.idCliente);
            cliEmp = query.Execute()[0] as Cliente;

            foreach (MovItem mi in listaMovItem)
            {
                qtd = (int)Math.Ceiling(mi.qtd);

                Item item = null;
                query = db.Query();
                query.Constrain(typeof(Item));
                query.Descend("id").Constrain(mi.idItem);
                item = query.Execute()[0] as Item;

                ItemEmpEstoque iee = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpEstoque));
                query.Descend("id").Constrain(mi.idIEE);
                query.Descend("idEmp").Constrain(idEmp);
                iee = query.Execute()[0] as ItemEmpEstoque;

                ItemEmpPreco iep = null;
                query = db.Query();
                query.Constrain(typeof(ItemEmpPreco));
                query.Descend("idItem").Constrain(item.id);
                query.Descend("idEmp").Constrain(idEmp);
                iep = query.Execute()[0] as ItemEmpPreco;

                if (mi.qtd > 0)
                {
                    if (nEtiqueta == 2)
                    {
                        //continuar etiqueta em aberto, restando 1 etiqueta
                        if (mi.qtd == 1)
                        {
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 1)
                        {
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, 0);

                            int qtdResto = (int)Math.Ceiling(mi.qtd) - 1;

                            if (qtdResto == 3)
                            {
                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveFim(sbScript, 0);
                                nEtiqueta = 0;
                            }
                            else if (qtdResto > 3)
                            {
                                int nRepeticoes = Convert.ToInt32(qtdResto / 3);
                                int nResto = qtdResto % 3;

                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveFim(sbScript, nRepeticoes);

                                if (nRepeticoes > 1)
                                {
                                    while (nRepeticoes > 1)
                                    {
                                        EscreveInicio(sbScript);
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveFim(sbScript, nRepeticoes);
                                        nRepeticoes--;
                                    }
                                }

                                if (nResto > 0)
                                {
                                    EscreveInicio(sbScript);

                                    if (nResto == 1)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        nEtiqueta = 1;
                                    }
                                    else if (nResto == 2)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        nEtiqueta = 2;
                                    }
                                }
                                else
                                    nEtiqueta = 0;
                            }
                            else if (qtdResto < 3)
                            {
                                EscreveInicio(sbScript);

                                if (qtdResto == 1)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (qtdResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 2;
                                }
                            }
                        }
                    }
                    else if (nEtiqueta == 1)
                    {
                        //continuar etiqueta em aberto, restando 2 etiquetas
                        if (mi.qtd == 2)
                        {
                            EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 2)
                        {
                            EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, 0);

                            int qtdResto = (int)Math.Ceiling(mi.qtd) - 2;

                            if (qtdResto == 3)
                            {
                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveFim(sbScript, 0);
                                nEtiqueta = 0;
                            }
                            else if (qtdResto > 3)
                            {
                                int nRepeticoes = Convert.ToInt32(qtdResto / 3);
                                int nResto = qtdResto % 3;

                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveFim(sbScript, nRepeticoes);

                                if (nRepeticoes > 1)
                                {
                                    while (nRepeticoes > 1)
                                    {
                                        EscreveInicio(sbScript);
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveFim(sbScript, nRepeticoes);
                                        nRepeticoes--;
                                    }
                                }

                                if (nResto > 0)
                                {
                                    EscreveInicio(sbScript);

                                    if (nResto == 1)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        nEtiqueta = 1;
                                    }
                                    else if (nResto == 2)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                        nEtiqueta = 2;
                                    }
                                }
                                else
                                    nEtiqueta = 0;
                            }
                            else if (qtdResto < 3)
                            {
                                EscreveInicio(sbScript);

                                if (qtdResto == 1)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (qtdResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 2;
                                }
                            }
                        }
                        else if (mi.qtd < 2)
                        {
                            EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            nEtiqueta = 2;
                        }
                    }
                    else if (nEtiqueta == 0)
                    {
                        //tratar etiqueta do começo
                        if (mi.qtd == 3)
                        {
                            EscreveInicio(sbScript);
                            EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 3)
                        {
                            int qtdItens = (int)Math.Ceiling(mi.qtd);
                            int nRepeticoes = Convert.ToInt32(qtdItens / 3);
                            int nResto = qtdItens % 3;

                            EscreveInicio(sbScript);
                            EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                            EscreveFim(sbScript, nRepeticoes);

                            if (nRepeticoes > 1)
                            {
                                while (nRepeticoes > 1)
                                {
                                    EscreveInicio(sbScript);
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveEtiqueta3(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveFim(sbScript, nRepeticoes);
                                    nRepeticoes--;
                                }
                            }

                            if (nResto > 0)
                            {
                                EscreveInicio(sbScript);

                                if (nResto == 1)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (nResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                    nEtiqueta = 2;
                                }
                            }
                            else
                                nEtiqueta = 0;
                        }
                        else if (mi.qtd < 3)
                        {
                            EscreveInicio(sbScript);

                            if (mi.qtd == 1)
                            {
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                nEtiqueta = 1;
                            }
                            else if (mi.qtd == 2)
                            {
                                EscreveEtiqueta1(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, Utils.formata_cpf_cnpj(cliEmp.cpf_cnpj), item.rfUnica, iep.venda, iee.codBarras);
                                nEtiqueta = 2;
                            }
                        }
                    }
                }
            }

            if (nEtiqueta > 0)
                EscreveFim(sbScript, 0);

            //Grava os arquivos .bat e script no servidor
            string DIRETORIO = @"C:\Inetpub\wwwroot\ftp\documentos\argox\" + idCorp+@"\";
            string ARQUIVO_BAT = @"C:\Inetpub\wwwroot\ftp\documentos\argox\"+idCorp+@"\enviaImpressao.bat";
            string ARQUIVO_TXT = @"C:\Inetpub\wwwroot\ftp\documentos\argox\"+idCorp+@"\confImpressao.txt";
            string STX = char.ConvertFromUtf32(002);

            string confImpressao = sbScript.ToString().Replace("STX", STX);
            string acript_bat = @"COPY C:\SDE\confImpressao.txt " + portaCOM;

            if (!Directory.Exists(DIRETORIO))
                Directory.CreateDirectory(DIRETORIO);

            File.WriteAllText(ARQUIVO_BAT, acript_bat);
            File.WriteAllText(ARQUIVO_TXT, confImpressao);
        }

        private static void EscreveInicio(StringBuilder sb)
        {
            sb.Append(STX + "m" + CR); // define unidade de medida em milímetros
            sb.Append(STX + "L" + CR); // entra em modo de formatação da etiqueta
            sb.Append("H14"); // fixa temperatura para 14
            sb.Append("D11" + CR); //tamanho padrão para pixel
        }

        private static void EscreveEtiqueta1(
            StringBuilder sb, string nomeEmpresa, string cnpjEmpresa, string rfUnica, double preco, string codigoBarras)
        {
            sb.AppendFormat(E1_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sb.AppendFormat(E1_CNPJ_EMPRESA + "{0}" + CR, cnpjEmpresa);
            sb.Append(E1_LABEL_VISTA + CR);
            sb.AppendFormat(E1_PRECO_VISTA + "{0}" + CR, Utils.formatMoney(Decimal.Parse(preco.ToString()), true));
            sb.AppendFormat(E1_PRECO_PRAZO + "3x {0}" + CR, Utils.formatMoney(Math.Round(((decimal)preco / 3), 2), true));
            sb.AppendFormat(E1_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
            sb.AppendFormat(E1_RF_UNICA + "{0}" + CR, rfUnica);
        }

        private static void EscreveEtiqueta2(
            StringBuilder sb, string nomeEmpresa, string cnpjEmpresa, string rfUnica, double preco, string codigoBarras)
        {
            sb.AppendFormat(E2_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sb.AppendFormat(E2_CNPJ_EMPRESA + "{0}" + CR, cnpjEmpresa);
            sb.Append(E2_LABEL_VISTA + CR);
            sb.AppendFormat(E2_PRECO_VISTA + "{0}" + CR, Utils.formatMoney(Decimal.Parse(preco.ToString()), true));
            sb.AppendFormat(E2_PRECO_PRAZO + "3x {0}" + CR, Utils.formatMoney(Math.Round(((decimal)preco / 3), 2), true));
            sb.AppendFormat(E2_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
            sb.AppendFormat(E2_RF_UNICA + "{0}" + CR, rfUnica);
        }

        private static void EscreveEtiqueta3(
            StringBuilder sb, string nomeEmpresa, string cnpjEmpresa, string rfUnica, double preco, string codigoBarras)
        {
            sb.AppendFormat(E3_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sb.AppendFormat(E3_CNPJ_EMPRESA + "{0}" + CR, cnpjEmpresa);
            sb.Append(E3_LABEL_VISTA + CR);
            sb.AppendFormat(E3_PRECO_VISTA + "{0}" + CR, Utils.formatMoney(Decimal.Parse(preco.ToString()), true));
            sb.AppendFormat(E3_PRECO_PRAZO + "3x {0}" + CR, Utils.formatMoney(Math.Round(((decimal)preco / 3), 2), true));
            sb.AppendFormat(E3_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
            sb.AppendFormat(E3_RF_UNICA + "{0}" + CR, rfUnica);
        }

        private static void EscreveFim(StringBuilder sbScript, int nRepeticoes)
        {
            sbScript.Append("E" + CR); //fim do modo de formatação e imprime
            sbScript.Append(STX + "Q" + CR); // limpa a memória da impressora
        }

    }
}
