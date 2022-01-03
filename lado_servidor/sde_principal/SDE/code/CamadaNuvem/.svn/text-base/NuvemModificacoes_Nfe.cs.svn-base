using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using SDE.Constantes;
using SDE.CamadaNuvem;
using System.Text;
using Db4objects.Db4o.Query;
using SDE.EntidadeNFE;
using SDE.Enumerador;
using SDE.CamadaControle;
using SDE.CamadaServico;
using System.Xml;
using System.IO;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public string codNumerico;
        public int digVerificador;
        public int dig = 0;
        public int cont = 4;
        public int valorTotal = 0;
        public string valorChave = "";
        //vinicius

        #region GeraXml
        public String[] GerarXml(int idCorp, int idEmp, Mov mov_dados, MovNFE mov_nfe_dados, MovNfeVeiculo mov_nfe_vei_dados, List<MovItem> mItens_dados)
        {
            SuperServico sServ = new SuperServico();

            defineCorp(idCorp);

            mov_dados.dthrMovEmissao = Utils.getAgoraString();

            try
            {
                IQuery query;

                Empresa emp = null;
                query = db.Query();
                query.Constrain(typeof(Empresa));
                query.Descend("id").Constrain(idEmp);
                emp = query.Execute()[0] as Empresa;

                //buscal cliente da empresa (dados da empresa)
                Cliente cliEmp = null;
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(emp.idCliente);
                cliEmp = query.Execute()[0] as Cliente;

                ClienteContato cliEmpFone = new ClienteContato();
                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(emp.idCliente);
                if (query.Execute().Count > 0)
                {
                    foreach (ClienteContato cc in query.Execute())
                    {
                        if (cc.tipo != EContatoTipo.fone_fixo)
                            continue;
                        cliEmpFone = cc;
                        break;
                    }
                }

                ClienteEndereco endCliEmp = null;
                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("id").Constrain(mov_nfe_dados.idEnderecoEmp);
                endCliEmp = query.Execute()[0] as ClienteEndereco;
                if (endCliEmp == null)
                    throw new Exception(string.Format("Empresa {0} não tem Endereco", endCliEmp.id));

                //dados cliente/fornecedor
                Cliente cliForn = null;
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(mov_dados.idCliente);
                cliForn = query.Execute()[0] as Cliente;

                ClienteContato cliFornFone = new ClienteContato();
                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(cliFornFone.id);
                if (query.Execute().Count > 0)
                {
                    foreach (ClienteContato cc in query.Execute())
                    {
                        if (cc.tipo != EContatoTipo.fone_fixo)
                            continue;
                        cliFornFone = cc;
                        break;
                    }
                }

                ClienteEndereco endFornEmp = new ClienteEndereco();
                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("id").Constrain(mov_nfe_dados.idEnderecoCliente);
                if (query.Execute().Count > 0)
                    endFornEmp = query.Execute()[0] as ClienteEndereco;

                MovNFE nfeRF = null;
                if (mov_nfe_dados.finalidadeNFE != ENfeFinalidade.normal)
                {
                    query = db.Query();
                    query.Constrain(typeof(MovNFE));
                    query.Descend("id").Constrain(mov_nfe_dados.idmovReferenciada);
                    nfeRF = query.Execute()[0] as MovNFE;
                }

                ClienteEndereco peRetirada = null;
                if (mov_nfe_dados.idEnderecoRetirada != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteEndereco));
                    query.Descend("id").Constrain(mov_nfe_dados.idEnderecoRetirada);
                    peRetirada = query.Execute()[0] as ClienteEndereco;
                }

                ClienteEndereco peEntrega = null;
                if (mov_nfe_dados.idEnderecoEntrega != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteEndereco));
                    query.Descend("id").Constrain(mov_nfe_dados.idEnderecoEntrega);
                    peEntrega = query.Execute()[0] as ClienteEndereco;
                }

                Cliente cliTransp = null;
                if (mov_nfe_dados.idClienteTransp != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(Cliente));
                    query.Descend("id").Constrain(mov_nfe_dados.idClienteTransp);
                    cliTransp = query.Execute()[0] as Cliente;
                }

                ClienteEndereco endTrans = null;
                if (mov_nfe_dados.idEnderecoTransp != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteEndereco));
                    query.Descend("id").Constrain(mov_nfe_dados.idEnderecoTransp);
                    endTrans = query.Execute()[0] as ClienteEndereco;
                }

                ClienteVeiculo cVeiculo = null;
                if (mov_nfe_dados.idVeiculo != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteVeiculo));
                    query.Descend("id").Constrain(mov_nfe_dados.idVeiculo);
                    cVeiculo = query.Execute()[0] as ClienteVeiculo;
                }


                ClienteVeiculo cReboque = null;
                if (mov_nfe_dados.idReboque != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteVeiculo));
                    query.Descend("id").Constrain(mov_nfe_dados.idReboque);
                    cReboque = query.Execute()[0] as ClienteVeiculo;
                }

                //formula magica Thiago para buscar do Sdeconfig Vinicius 29set10
                String tipoAmb = "";
                query = db.Query();
                query.Constrain(typeof(SdeConfig));
                query.Descend("variavel").Constrain("Empresa.NFe.Ambiente");
                tipoAmb = (query.Execute()[0] as SdeConfig).valor;


                /* //descomentar esse grupo de código para efetivar o CRT e CSOSN da V2 da NFe
                |-----------------------------------------------------------------------|
                | //Pega valor do parâmetro sobre o Código do Regime Tributário         |       
                | String crt = "";                                                      |
                | query = db.Query();                                                   |
                | query.Constrain(typeof(SdeConfig));                                   |
                | query.Descend("variavel").Constrain("Empresa.CRT.Sistema Nacional");  |
                | crt = (query.Execute()[0] as SdeConfig).valor;                        |
                |                                                                       |
                | //Pega o Código de Situação de Operações no Simples Nacional - CSOSN  |
                | String csosn = "";                                                    |
                | query = db.Query();                                                   |
                | query.Constrain(typeof(SdeConfig));                                   |
                | query.Descend("variavel").Constrain("Empresa.CSOSN.Sistema Nacional");|
                | csosn = (query.Execute()[0] as SdeConfig).valor;                      |
                |-----------------------------------------------------------------------|
                */
                //descomentar esse grupo de código para efetivar o CRT e CSOSN da V2 da NFe


                #region EscreveXml

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings oSettings = new XmlWriterSettings();

                oSettings.Encoding = Encoding.UTF8;
                oSettings.CheckCharacters = true;
                oSettings.Indent = true;
                oSettings.IndentChars = "     ";

                XmlWriter writer = XmlWriter.Create(sb, oSettings);

                EscreveItensNfe xmlNFE = new EscreveItensNfe();

                DateTime dtTemp = DateTime.Parse(mov_dados.dthrMovEmissao);
                string ano = dtTemp.ToString("yyyy").Substring(2);
                string mes = dtTemp.ToString("MM");
                string data = ano + mes;

                String codUF = (endCliEmp.cidadeIBGE.Length < 2) ? "00" : endCliEmp.cidadeIBGE.Substring(0, 2);
                String codMov = mov_dados.id.ToString().PadRight(9, '0');

                mov_nfe_dados.chaveAcessoNFE = gerarChaveNFE(codUF, data, cliEmp.cpf_cnpj, "55", mov_nfe_dados.serieNota.ToString(), mov_nfe_dados.numeroNota.ToString(), "");

                xmlNFE.Escreve_Inicio(writer, mov_nfe_dados.chaveAcessoNFE);

                string ufIBGE = "";
                if (mov_dados.resumo == EMovResumo.saida)
                {
                    ufIBGE = (endCliEmp.cidadeIBGE.Length < 2) ? "" : endCliEmp.cidadeIBGE.Substring(0, 2);
                    xmlNFE.Escreve_ide(writer, mov_dados, mov_nfe_dados, nfeRF, ufIBGE, endCliEmp.cidadeIBGE, digVerificador, codNumerico, tipoAmb);
                    xmlNFE.Escreve_emit(writer, cliEmp, endCliEmp, cliEmpFone); //, crt, csosn); //incluir esses parâmetros quando for mudar a versão da NFe p/ 2.0



                    xmlNFE.Escreve_dest(writer, cliForn, cliFornFone, endFornEmp, mov_nfe_dados.clienteIE);

                    if (endCliEmp.id != mov_nfe_dados.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, cliForn.tipo, cliForn.cpf_cnpj, peRetirada);
                    if (endCliEmp.id != mov_nfe_dados.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, cliForn.tipo, cliForn.cpf_cnpj, peEntrega);
                }
                else if (mov_dados.resumo == EMovResumo.entrada)
                {
                    ufIBGE = (endCliEmp.cidadeIBGE.Length < 2) ? "" : endCliEmp.cidadeIBGE.Substring(0, 2);
                    xmlNFE.Escreve_ide(writer, mov_dados, mov_nfe_dados, nfeRF, ufIBGE, endCliEmp.cidadeIBGE, digVerificador, codNumerico, tipoAmb);
                    xmlNFE.Escreve_emit(writer, cliEmp, endCliEmp, cliEmpFone); //, crt, csosn); //incluir esses parâmetros quando for mudar a versão da NFe p/ 2.0
                    xmlNFE.Escreve_dest(writer, cliEmp, cliEmpFone, endCliEmp, mov_nfe_dados.clienteIE);

                    if (endCliEmp.id != mov_nfe_dados.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, cliEmp.tipo, cliEmp.cpf_cnpj, peRetirada);
                    if (endCliEmp.id != mov_nfe_dados.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, cliEmp.tipo, cliEmp.cpf_cnpj, peEntrega);
                }

                int numItem = 0;
                foreach (MovItem mi in mItens_dados)
                {
                    Item item = null;
                    ItemEmpAliquotas iea = null;

                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(mi.idItem);
                    item = query.Execute()[0] as Item;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpAliquotas));
                    query.Descend("idItem").Constrain(item.id);
                    query.Descend("idEmp").Constrain(idEmp);
                    iea = query.Execute()[0] as ItemEmpAliquotas;

                    numItem++;


                    xmlNFE.Escreve_det(writer, mi, mov_dados.tipo, item, iea, numItem, emp, cliEmp);

                }

                xmlNFE.Escreve_total(writer, mov_dados);
                xmlNFE.Escreve_transp(writer, cliTransp, endTrans, cVeiculo, cReboque, mov_nfe_dados);

                if (mov_nfe_dados.infoAdicional.Trim() != "")
                    xmlNFE.Escreve_infAdicXML(writer, mov_nfe_dados.infoAdicional, mov_nfe_dados);
                xmlNFE.Escreve_Fim(writer);
                #endregion

                writer.Flush();
                writer.Close();

                sb.Replace("utf-16", "utf-8");
                string retorno = sb.ToString();



                return new string[] { retorno, mov_nfe_dados.chaveAcessoNFE };
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
                throw;
            }
        }
        #endregion



        private string gerarChaveNFE(string codUF, string dtEmissao, string cnpj, string modelo, string serie, string numDoc, string cod)
        {
            Random rand = new Random();
            codNumerico = rand.Next(999999999).ToString().PadLeft(9, '0');

            cnpj = Utils.apenasNumeros(cnpj);

            while (serie.Length < 3)
            {
                serie = "0" + serie;
            }

            if (numDoc.Length < 9)
            {
                while (numDoc.Length < 9)
                {
                    numDoc = "0" + numDoc;
                }
            }

            digVerificador = gerarDV_NFE(codUF, dtEmissao, cnpj, modelo, serie, numDoc, codNumerico);

            valorChave = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico + digVerificador;

            //salvaChaveNota(valorChave);

            return valorChave;
        }

        private int gerarDV_NFE(string codUF, string dtEmissao, string cnpj, string modelo, string serie, string numDoc, string codNumerico)
        {
            string chave = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico;

            foreach (char c in chave)
            {
                dig = int.Parse(c.ToString());

                valorTotal += dig * cont;
                if (cont != 2)
                    cont--;
                else
                    cont = 9;
            }

            int resto = valorTotal % 11;
            int retorno = 0;

            if (resto != 0 && resto != 1)
                retorno = 11 - resto;

            return retorno;
        }


        /******************* Salva Mov ************************/

        #region Salva Movimentacao NFE
        public void SalvaMovNFE(int idCorp, int idEmp, int idMov, MovNFE movnfeDados)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    ReaproveitamentoCodigo r = AppFacade.get.reaproveitamento;
                    Mov mov = r.Mov_Load(db, idMov);

                    DateTime dtTemp = DateTime.Parse(mov.dthrMovEmissao);
                    string ano = dtTemp.ToString("yyyy").Substring(2);
                    string mes = dtTemp.ToString("MM");
                    string data = ano + mes;

                    ClienteEndereco endEmp = r.Endereco_Load(db, movnfeDados.idEnderecoEmp);
                    Empresa emp = r.Empresa_Load(db, mov.idEmp);
                    Cliente cliEmp = r.Cliente_Load(db, emp.idCliente);

                    String codUF = (endEmp.cidadeIBGE.Length < 2) ? "00" : endEmp.cidadeIBGE.Substring(0, 2);
                    String codMov = mov.id.ToString().PadRight(9, '0');

                    String cpf = cliEmp.cpf_cnpj;


                    String modelo = mov.modeloNF.ToString();
                    //String modelo = "55";

                    String serieNF = mov.serieNF;
                    String numeroNF = mov.numeroNF.ToString();

                    //numeroNota
                    movnfeDados.chaveAcessoNFE = gerarChave(codUF, data, cpf, modelo, serieNF, numeroNF, codMov);
                    //movnfeVeiculos
                    MovNfeVeiculo nfeVeiculoDados = new MovNfeVeiculo();
                    if (movnfeDados.__nfeVeiculo != null)
                        Utils.copiaCamposBasicos(movnfeDados.__nfeVeiculo, nfeVeiculoDados);
                    else
                        nfeVeiculoDados = null;
                    movnfeDados.__nfeVeiculo = null;

                    //busca movNFE
                    MovNFE movNFE = r.MovNFE_Load(db, mov.id);
                    if (movNFE == null)
                    {
                        //Cria nova NFE
                        movNFE = new MovNFE();
                        Utils.copiaCamposBasicos(movnfeDados, movNFE);
                        movnfeDados.id = r.getIncremento(db, typeof(MovNFE), 0);
                        movnfeDados.idEmp = idEmp;
                        mov.isNfePreenchida = true;
                        dbStore(db, mov);
                        dbStore(db, movnfeDados);
                        Utils.copiaCamposBasicos(movnfeDados, movNFE);
                        //movNFE = cMov.NovaNFE(mov, movnfeDados);
                    }
                    else
                    {
                        //Alterar Nfe                        
                        movnfeDados.id = movNFE.id;
                        Utils.copiaCamposBasicos(movnfeDados, movNFE);
                        dbStore(db, movNFE);
                        //cMov.AtualizaNFE(mov, movNFE);
                    }

                    //atualização da nfeVeiculos
                    if (nfeVeiculoDados != null)
                    {
                        //throw new Exception("não nulo");
                        MovNfeVeiculo nfeVeiculo = r.movNfeVeiculo_Load(db, movNFE.id);
                        if (nfeVeiculo == null)
                            nfeVeiculo = new MovNfeVeiculo();
                        Utils.copiaCamposBasicos(nfeVeiculoDados, nfeVeiculo);
                        nfeVeiculo.idMovNFE = movNFE.id;
                        dbStore(db, nfeVeiculo);
                        //cMov.SalvaNFEVeiculo(nfeVeiculo);
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }
        #endregion


        private void verificarObjetos(List<object> objetos)
        {
            foreach (object obj in objetos)
                if (obj != null)
                    Utils.retiraCaracterEspecial(obj);
        }

        /******************* TXT ****************/

        #region Gera TXT
        public String[] GerarTXT(int idCorp, int idEmp, Mov mov_dados, MovNFE mov_nfe_dados, MovNfeVeiculo mov_nfe_vei_dados, List<MovItem> mItens_dados)
        {
            IQuery query;
            defineCorp(idCorp);
            try
            {
                StringBuilder sb = new StringBuilder();

                Mov mov = mov_dados;
                MovNFE mov_nfe = mov_nfe_dados;

                mov.dthrMovEmissao = Utils.getAgoraString();

                #region Pesquisas
                MovNfeVeiculo mov_nfe_vei = null;
                if (mov_nfe.ehVendaVeiculo)
                    mov_nfe_vei = mov_nfe_vei_dados;
                mov.__mItens = mItens_dados;
                List<MovItem> mItens = mov.__mItens;

                //nfe referenciada
                //finalidade ajuste , complementar
                MovNFE mov_nfe_rf = null;
                if (mov_nfe.finalidadeNFE != SDE.Enumerador.ENfeFinalidade.normal)
                {
                    query = db.Query();
                    query.Constrain(typeof(MovNFE));
                    query.Descend("idMov").Constrain(mov_nfe.idmovReferenciada);
                    mov_nfe_rf = query.Execute()[0] as MovNFE;
                }

                //Busca - Itens da Movimentação
                foreach (MovItem mi in mItens)
                {
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    query.Descend("id").Constrain(mi.idItem);
                    mi.__item = query.Execute()[0] as Item;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmp));
                    query.Descend("idItem").Constrain(mi.idItem);
                    if (query.Execute().Count == 1)
                        mi.__item.__ie = query.Execute()[0] as ItemEmp;
                    else
                        foreach (ItemEmp ie in query.Execute())
                            if (ie.idEmp == idEmp)
                                mi.__item.__ie = ie;

                    query = db.Query();
                    query.Constrain(typeof(ItemEmpAliquotas));
                    query.Descend("idItem").Constrain(mi.idItem);
                    if (query.Execute().Count == 1)
                        mi.__item.__ie.__aliquotas = query.Execute()[0] as ItemEmpAliquotas;
                    else
                        foreach (ItemEmpAliquotas iea in query.Execute())
                            if (iea.idItem == mi.idItem)
                                mi.__item.__ie.__aliquotas = iea;
                }

                //Busca Pessoa da Empresa
                query = db.Query();
                query.Constrain(typeof(Empresa));
                query.Descend("id").Constrain(idEmp);
                Empresa emp = query.Execute()[0] as Empresa;
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(emp.idCliente);
                Cliente emp_cliente = query.Execute()[0] as Cliente;
                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(emp.idCliente);
                ClienteContato conEmp = null;
                foreach (ClienteContato cc in query.Execute())
                    if (cc.tipo == EContatoTipo.fone_fixo)
                    {
                        conEmp = cc;
                        continue;
                    }
                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("id").Constrain(mov_nfe.idEnderecoEmp);
                ClienteEndereco endEmp = null;
                if (query.Execute().Count > 0)
                    endEmp = query.Execute()[0] as ClienteEndereco;
                else
                    throw new ExcecaoSDE("Empresa " + emp_cliente.id + " não tem Endereco");

                //Busca Cliente / Fornecedor
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(mov_nfe.idCliente);
                Cliente cli = query.Execute()[0] as Cliente;
                query = db.Query();
                query.Constrain(typeof(ClienteContato));
                query.Descend("idCliente").Constrain(mov_nfe.idCliente);
                ClienteContato conCliForn = null;
                foreach (ClienteContato cc in query.Execute())
                    if (cc.tipo == EContatoTipo.fone_fixo)
                    {
                        conCliForn = cc;
                        continue;
                    }
                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("id").Constrain(mov_nfe.idEnderecoCliente);
                ClienteEndereco endCliForn = null;
                if (query.Execute().Count > 0)
                    endCliForn = query.Execute()[0] as ClienteEndereco;
                else
                    throw new ExcecaoSDE("Cliente " + cli.id + " não tem endereço.");

                //Busca Local Retirada
                ClienteEndereco peRetirada = null;
                if (mov_nfe.idEnderecoRetirada != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteEndereco));
                    query.Descend("id").Constrain(mov_nfe.idEnderecoRetirada);
                    peRetirada = query.Execute()[0] as ClienteEndereco;
                }
                //Busca Local de Entrega
                ClienteEndereco peEntrega = null;
                if (mov_nfe.idEnderecoEntrega != 0)
                {
                    query = db.Query();
                    query.Constrain(typeof(ClienteEndereco));
                    query.Descend("id").Constrain(mov_nfe.idEnderecoEntrega);
                    peEntrega = query.Execute()[0] as ClienteEndereco;
                }
                //Busca Transportador
                Cliente cliTransp = null;
                query = db.Query();
                query.Constrain(typeof(Cliente));
                query.Descend("id").Constrain(mov_nfe.idClienteTransp);
                if (query.Execute().Count > 0)
                    cliTransp = query.Execute()[0] as Cliente;
                ClienteEndereco endTrans = null;
                query = db.Query();
                query.Constrain(typeof(ClienteEndereco));
                query.Descend("id").Constrain(mov_nfe.idEnderecoTransp);
                if (query.Execute().Count > 0)
                    endTrans = query.Execute()[0] as ClienteEndereco;

                //veiculo e reboque
                ClienteVeiculo cVeiculo = null;
                query = db.Query();
                query.Constrain(typeof(ClienteVeiculo));
                query.Descend("id").Constrain(mov_nfe.idVeiculo);
                if (query.Execute().Count > 0)
                    cVeiculo = query.Execute()[0] as ClienteVeiculo;
                ClienteVeiculo cReboque = null;
                query = db.Query();
                query.Constrain(typeof(ClienteVeiculo));
                query.Descend("id").Constrain(mov_nfe.idReboque);
                if (query.Execute().Count > 0)
                    cReboque = query.Execute()[0] as ClienteVeiculo;

                //Busca formas pagamento movimentação
                string obsFormaPagamento = "";
                query = db.Query();
                query.Constrain(typeof(Cx_Lancamento));
                query.Descend("idTransacao").Constrain(mov.idTransacao);
                foreach (Cx_Lancamento cxL in query.Execute())
                    if (cxL.observacoes != null && cxL.observacoes.Trim().Length > 0)
                    {
                        obsFormaPagamento = cxL.observacoes;
                        break;
                    }


                //chave acesso NFE
                DateTime dtTemp = DateTime.Parse(mov.dthrMovEmissao);
                string ano = dtTemp.ToString("yyyy").Substring(2);
                string mes = dtTemp.ToString("MM");
                string data = ano + mes;

                String codUF = (endEmp.cidadeIBGE.Length < 2) ? "00" : endEmp.cidadeIBGE.Substring(0, 2);
                String codMov = mov.id.ToString().PadRight(9, '0');

                String cpf = emp_cliente.cpf_cnpj;

                String modelo = mov.modeloNF.ToString();
                String serieNF = mov.serieNF;
                String numeroNF = mov.numeroNF.ToString();

                mov_nfe.chaveAcessoNFE = gerarChave(codUF, data, cpf, modelo, serieNF, numeroNF, codMov);

                #endregion

                #region Verificar Objetos
                //lista de objetos
                List<object> lista = new List<object>();
                lista.Add(mov);
                lista.Add(mov_nfe);
                lista.Add(emp_cliente);
                lista.Add(cli);
                lista.Add(endEmp);
                lista.Add(endCliForn);
                if (cliTransp != null)
                    lista.Add(cliTransp);
                if (endTrans != null && cliTransp != null)
                    lista.Add(endTrans);
                foreach (MovItem mi in mItens)
                    lista.Add(mi.__item);
                verificarObjetos(lista);

                #endregion

                #region EscreveTXT

                EscreveItensNfeTXT txtNFE = new EscreveItensNfeTXT();
                //gerar chave de Acesso
                //uf =2, dtEmissao=AAMM, cnpj, modelo=55 , 000,   
                //numDoc = 9 , codNumerico = 9
                string codNota = mov_nfe.numeroNota.ToString().PadLeft(9, '0');
                int digitoVerificador = txtNFE.gerarDV(endEmp.ufIBGE, mov.dthrMovEmissao, emp_cliente.cpf_cnpj, "55", mov_nfe.serieNota.ToString().PadLeft(3, '0')
                                                , codNota, codNota);
                string chaveAcesso = txtNFE.gerarChave(endEmp.ufIBGE, mov.dthrMovEmissao, emp_cliente.cpf_cnpj, "55", mov_nfe.serieNota.ToString().PadLeft(3, '0')
                                                , codNota, codNota, digitoVerificador.ToString());

                //inicio do TXT
                txtNFE.Escreve_Inicio(sb, chaveAcesso);
                //emitente, remetente

                string ufIBGE = "";
                if (mov.resumo == EMovResumo.saida)
                {
                    txtNFE.Escreve_ide(sb, mov, mov_nfe, mov_nfe_rf, endEmp.ufIBGE, endEmp.cidadeIBGE);

                    txtNFE.Escreve_emit(sb, emp_cliente, endEmp, conEmp);
                    txtNFE.Escreve_dest(sb, cli, endCliForn, conCliForn);

                    /*
                    // Local de Retirada, Entrega
                    if (endEmp.id != movNfe.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, pesEmp.cpf_cnpj, peRetirada);
                    if (endCliForn.id != movNfe.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, pesEmp.cpf_cnpj, peEntrega);
                    
                    */
                }
                else if (mov.resumo == EMovResumo.entrada)
                {
                    txtNFE.Escreve_ide(sb, mov, mov_nfe, mov_nfe_rf, endCliForn.ufIBGE, endCliForn.cidadeIBGE);
                    txtNFE.Escreve_emit(sb, cli, endCliForn, conCliForn);
                    txtNFE.Escreve_dest(sb, emp.__cliente, endEmp, conEmp);

                    /* 
                    //Local de Retirada, Entrega
                    if (endCliForn.id != movNfe.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, pesCliForn.cpf_cnpj, peRetirada);
                    if (endEmp.id != movNfe.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, pesEmp.cpf_cnpj, peEntrega);
                    */
                }

                //itens da movimentação
                int numItem = 0;
                foreach (MovItem mi in mItens)
                {
                    //throw new Exception("ERRROOOOOOOO");
                    numItem++;
                    txtNFE.Escreve_det(sb, mi, mov, numItem, mov_nfe_vei);
                }

                txtNFE.Escreve_total(sb);
                if (cliTransp != null)
                    txtNFE.Escreve_transp(sb, cliTransp, endTrans, cVeiculo, cReboque, mov_nfe);
                txtNFE.Escreve_fatura(sb, mov_nfe.fatura + ":" + obsFormaPagamento);
                txtNFE.Escreve_infAdic(sb, mov_nfe.infoAdicional);
                //final do arquivo
                txtNFE.Escreve_Fim(sb);


                string retorno = sb.ToString();

                /*
                string sArquivo = string.Format("C:\\NFE_GERADAS\\{0}-nfe.txt", chaveAcesso);
                File.WriteAllText(sArquivo,
                    sb.ToString(), Encoding.UTF8);
                 * */

                #endregion
                return new string[] { retorno, mov_nfe.chaveAcessoNFE };
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
                throw;
            }
        }
        #endregion

      

        /***************** Gera Chave NFe *****************/

        private string gerarChave(string codUF, string dtEmissao, string cnpj, string modelo, string serie, string numDoc, string codNumerico)
        {
            digVerificador = gerarDV(codUF, dtEmissao, cnpj, modelo, serie, numDoc, codNumerico);

            cnpj = Utils.apenasNumeros(cnpj);

            valorChave = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico + digVerificador;

            return valorChave;
        }

        private int gerarDV(string codUF, string dtEmissao, string cnpj, string modelo, string serie, string numDoc, string codNumerico)
        {
            valorChave = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico;

            foreach (char c in valorChave)
            {
                try
                {
                    dig = int.Parse(c.ToString());

                    valorTotal += dig * cont;

                    if (cont != 2)
                        cont--;
                    else
                        cont = 9;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            int resto = valorTotal % 11;
            return 11 - resto;
        }

        public int numNFE(int idCorp, int idEmp)
        {
            defineCorp(idCorp);

            IQuery query;

            int numNF = 0;
            query = db.Query();
            query.Constrain(typeof(MovNFE));
            query.Descend("idEmp").Constrain(idEmp);
            query.Descend("numeroNota").OrderDescending();

            if (query.Execute().Count > 0)
                numNF = (query.Execute()[0] as MovNFE).numeroNota;

            return numNF;
        }

        /*private void salvaChaveNota(string vChave)
        {
            StreamWriter sw = new StreamWriter("C:\\testeChaveNota.txt", true, Encoding.UTF8);

            sw.WriteLine("Valor da Chave: " + vChave);
            sw.Close();
        }*/
    }
}