using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Text;
using SDE.Entidade;

namespace SDE.code.ConfiguracoesArgox
{
    public class EspacoMamaeBebe
    {
        /*
         * ponto superior da etiqueta eixo y: 0190 (valor podendo variar para menos, de acordo com a fonte utilizada, para que não ultrapasse os limites da etiqueta)
         * ponto inferior da etiqueta eixo y: 0010
         * 
         * ponto inicial da etiqueta 1 eixo x: 0010
         * ponto inicial da etiqueta 2 eixo x: 0370
         * ponto inicial da etiqueta 3 eixo x: 0730
         * */

        private const string E1_NOME_EMPRESA = "111100001800080";
        private const string E1_CODIGO_PRODUTO = "111100001600010";
        private const string E1_NOME_PRODUTO = "111100001600120";
        private const string E1_GRADE_PRODUTO = "111100001400010GRADE:";
        private const string E1_CODIGO_MARCA = "111100001200010CMARCA:";
        private const string E1_NOME_MARCA = "111100001200120MARCA:";
        private const string E1_RF_UNICA = "111100000900010RF:";
        private const string E1_PRECO = "121100000900170";
        private const string E1_CODIGO_BARRAS = "1E5206000000030";

        private const string E2_NOME_EMPRESA = "111100001800440";
        private const string E2_CODIGO_PRODUTO = "111100001600370";
        private const string E2_NOME_PRODUTO = "111100001600480";
        private const string E2_GRADE_PRODUTO = "111100001400370GRADE:";
        private const string E2_CODIGO_MARCA = "111100001200370CMARCA:";
        private const string E2_NOME_MARCA = "111100001200480MARCA:";
        private const string E2_RF_UNICA = "111100000900370RF:";
        private const string E2_PRECO = "121100000900530";
        private const string E2_CODIGO_BARRAS = "1E5206000000390";

        private const string E3_NOME_EMPRESA = "111100001800800";
        private const string E3_CODIGO_PRODUTO = "111100001600730";
        private const string E3_NOME_PRODUTO = "111100001600840";
        private const string E3_GRADE_PRODUTO = "111100001400730GRADE:";
        private const string E3_CODIGO_MARCA = "111100001200730CMARCA:";
        private const string E3_NOME_MARCA = "111100001200840MARCA:";
        private const string E3_RF_UNICA = "111100000900730RF:";
        private const string E3_PRECO = "121100000900890";
        private const string E3_CODIGO_BARRAS = "1E5206000000750";

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
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 1)
                        {
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, 0);

                            int qtdResto = (int)Math.Ceiling(mi.qtd) - 1;

                            if (qtdResto == 3)
                            {
                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveFim(sbScript, 0);
                                nEtiqueta = 0;
                            }
                            else if (qtdResto > 3)
                            {
                                int nRepeticoes = Convert.ToInt32(qtdResto / 3);
                                int nResto = qtdResto % 3;

                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveFim(sbScript, nRepeticoes);

                                if (nRepeticoes > 1)
                                {
                                    while (nRepeticoes > 1)
                                    {
                                        EscreveInicio(sbScript);
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveFim(sbScript, nRepeticoes);
                                        nRepeticoes--;
                                    }
                                }

                                if (nResto > 0)
                                {
                                    EscreveInicio(sbScript);

                                    if (nResto == 1)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        nEtiqueta = 1;
                                    }
                                    else if (nResto == 2)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
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
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (qtdResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
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
                            EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 2)
                        {
                            EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, 0);

                            int qtdResto = (int)Math.Ceiling(mi.qtd) - 2;

                            if (qtdResto == 3)
                            {
                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveFim(sbScript, 0);
                                nEtiqueta = 0;
                            }
                            else if (qtdResto > 3)
                            {
                                int nRepeticoes = Convert.ToInt32(qtdResto / 3);
                                int nResto = qtdResto % 3;

                                EscreveInicio(sbScript);
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveFim(sbScript, nRepeticoes);

                                if (nRepeticoes > 1)
                                {
                                    while (nRepeticoes > 1)
                                    {
                                        EscreveInicio(sbScript);
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveFim(sbScript, nRepeticoes);
                                        nRepeticoes--;
                                    }
                                }

                                if (nResto > 0)
                                {
                                    EscreveInicio(sbScript);

                                    if (nResto == 1)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        nEtiqueta = 1;
                                    }
                                    else if (nResto == 2)
                                    {
                                        EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                        EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                            item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
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
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (qtdResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    nEtiqueta = 2;
                                }
                            }
                        }
                        else if (mi.qtd < 2)
                        {
                            EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            nEtiqueta = 2;
                        }
                    }
                    else if (nEtiqueta == 0)
                    {
                        //tratar etiqueta do começo
                        if (mi.qtd == 3)
                        {
                            EscreveInicio(sbScript);
                            EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, 0);
                            nEtiqueta = 0;
                        }
                        else if (mi.qtd > 3)
                        {
                            int qtdItens = (int)Math.Ceiling(mi.qtd);
                            int nRepeticoes = Convert.ToInt32(qtdItens / 3);
                            int nResto = qtdItens % 3;

                            EscreveInicio(sbScript);
                            EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                            EscreveFim(sbScript, nRepeticoes);

                            if (nRepeticoes > 1)
                            {
                                while (nRepeticoes > 1)
                                {
                                    EscreveInicio(sbScript);
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveEtiqueta3(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveFim(sbScript, nRepeticoes);
                                    nRepeticoes--;
                                }
                            }

                            if (nResto > 0)
                            {
                                EscreveInicio(sbScript);

                                if (nResto == 1)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    nEtiqueta = 1;
                                }
                                else if (nResto == 2)
                                {
                                    EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                    EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                        item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
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
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                nEtiqueta = 1;
                            }
                            else if (mi.qtd == 2)
                            {
                                EscreveEtiqueta1(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                EscreveEtiqueta2(sbScript, cliEmp.nome, item.id.ToString(), item.nomeEtiqueta, iee.identificador, item.rfUnica,
                                    item.idMarca.ToString(), (item.marca.Length > 11) ? item.marca.Substring(0, 11) : item.marca, Utils.formatMoney(Decimal.Parse(iep.venda.ToString()), true), iee.codBarras);
                                nEtiqueta = 2;
                            }
                        }
                    }
                }
            }

            if (nEtiqueta > 0)
                EscreveFim(sbScript, 0);

            //Grava os arquivos .bat e script no servidor
            string DIRETORIO = @"C:\Inetpub\wwwroot\ftp\documentos\argox\" + idCorp + @"\";
            string ARQUIVO_BAT = @"C:\Inetpub\wwwroot\ftp\documentos\argox\" + idCorp + @"\enviaImpressao.bat";
            string ARQUIVO_TXT = @"C:\Inetpub\wwwroot\ftp\documentos\argox\" + idCorp + @"\confImpressao.txt";
            string STX = char.ConvertFromUtf32(002);

            string confImpressao = sbScript.ToString().Replace("STX", STX);
            string acript_bat = @"COPY C:\SDE\confImpressao.txt " + portaCOM;

            if (!Directory.Exists(DIRETORIO))
                Directory.CreateDirectory(DIRETORIO);

            File.WriteAllText(ARQUIVO_BAT, acript_bat);
            File.WriteAllText(ARQUIVO_TXT, confImpressao);
        }

        private static void EscreveInicio(StringBuilder sbScript)
        {
            sbScript.Append(STX + "m" + CR); // define unidade de medida em milímetros
            sbScript.Append(STX + "L" + CR); // entra em modo de formatação da etiqueta
            sbScript.Append("H14"); // fixa temperatura para 14
            sbScript.Append("D11" + CR); //tamanho padrão para pixel
        }

        private static void EscreveEtiqueta1(
            StringBuilder sbScript, string nomeEmpresa, string codigoProduto, string nomeProduto, string gradeProduto,
            string rfUnica, string codigoMarca, string nomeMarca, string preco, string codigoBarras)
        {
            sbScript.AppendFormat(E1_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sbScript.AppendFormat(E1_CODIGO_PRODUTO + "{0}" + CR, codigoProduto);
            sbScript.AppendFormat(E1_NOME_PRODUTO + "{0}" + CR, nomeProduto);
            sbScript.AppendFormat(E1_GRADE_PRODUTO + "{0}" + CR, gradeProduto);
            sbScript.AppendFormat(E1_RF_UNICA + "{0}" + CR, rfUnica);
            sbScript.AppendFormat(E1_CODIGO_MARCA + "{0}" + CR, codigoMarca);
            sbScript.AppendFormat(E1_NOME_MARCA + "{0}" + CR, nomeMarca);
            sbScript.AppendFormat(E1_PRECO + "{0}" + CR, preco);
            sbScript.AppendFormat(E1_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
        }

        private static void EscreveEtiqueta2(
            StringBuilder sbScript, string nomeEmpresa, string codigoProduto, string nomeProduto, string gradeProduto,
            string rfUnica, string codigoMarca, string nomeMarca, string preco, string codigoBarras)
        {
            sbScript.AppendFormat(E2_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sbScript.AppendFormat(E2_CODIGO_PRODUTO + "{0}" + CR, codigoProduto);
            sbScript.AppendFormat(E2_NOME_PRODUTO + "{0}" + CR, nomeProduto);
            sbScript.AppendFormat(E2_GRADE_PRODUTO + "{0}" + CR, gradeProduto);
            sbScript.AppendFormat(E2_RF_UNICA + "{0}" + CR, rfUnica);
            sbScript.AppendFormat(E2_CODIGO_MARCA + "{0}" + CR, codigoMarca);
            sbScript.AppendFormat(E2_NOME_MARCA + "{0}" + CR, nomeMarca);
            sbScript.AppendFormat(E2_PRECO + "{0}" + CR, preco);
            sbScript.AppendFormat(E2_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
        }

        private static void EscreveEtiqueta3(
            StringBuilder sbScript, string nomeEmpresa, string codigoProduto, string nomeProduto, string gradeProduto,
            string rfUnica, string codigoMarca, string nomeMarca, string preco, string codigoBarras)
        {
            sbScript.AppendFormat(E3_NOME_EMPRESA + "{0}" + CR, nomeEmpresa);
            sbScript.AppendFormat(E3_CODIGO_PRODUTO + "{0}" + CR, codigoProduto);
            sbScript.AppendFormat(E3_NOME_PRODUTO + "{0}" + CR, nomeProduto);
            sbScript.AppendFormat(E3_GRADE_PRODUTO + "{0}" + CR, gradeProduto);
            sbScript.AppendFormat(E3_RF_UNICA + "{0}" + CR, rfUnica);
            sbScript.AppendFormat(E3_CODIGO_MARCA + "{0}" + CR, codigoMarca);
            sbScript.AppendFormat(E3_NOME_MARCA + "{0}" + CR, nomeMarca);
            sbScript.AppendFormat(E3_PRECO + "{0}" + CR, preco);
            sbScript.AppendFormat(E3_CODIGO_BARRAS + "B{0}" + CR, codigoBarras);
        }

        private static void EscreveFim(StringBuilder sbScript, int nRepeticoes)
        {
            sbScript.Append("E" + CR); //fim do modo de formatação e imprime
            sbScript.Append(STX + "Q" + CR); // limpa a memória da impressora
        }
    }
}
