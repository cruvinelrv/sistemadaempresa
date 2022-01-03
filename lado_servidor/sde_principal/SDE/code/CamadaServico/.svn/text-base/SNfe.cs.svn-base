using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.CamadaServico;
using SDE.EntidadeNFE;
using SDE.Entidade;
using SDE.Enumerador;

using System.Diagnostics;

using System.IO;
using System.Xml;

namespace SDE.CamadaServico
{
    public class SNfe : SuperServico
    {

        public MovNFE LoadIdMov(int idCorp, int idMov)
        {
            setBancoID(idCorp);
            return cMov.LoadNFE(idMov);
        }

        public MovNFE LoadNumeroNota(int idCorp, int numeroNota)
        {
            setBancoID(idCorp);
            return cMov.LoadNFENumeroNota(numeroNota);
        }

        public bool VerificaExisteNFE(int idCorp, int numeroNota)
        {
            setBancoID(idCorp);
            MovNFE nfe = cMov.LoadNFENumeroNota(numeroNota);
            return (nfe == null)
                ? false
                : true;
        }

        public void SalvaMovNFE(int idCorp, int idMov, MovNFE movnfeDados)
        {
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Mov mov = cMov.Load(idMov);

                    DateTime dtTemp = DateTime.Parse(mov.dthrMovEmissao);
                    string ano = dtTemp.ToString("yyyy").Substring(2);
                    string mes = dtTemp.ToString("MM");
                    string data = ano + mes;

                    ClienteEndereco endEmp = cCliente.LoadEndereco(movnfeDados.idEnderecoEmp);
                    Empresa emp = cEmp.Load(mov.idEmp);
                    Cliente cliEmp = cCliente.Load(emp.idCliente);

                    String codUF = (endEmp.cidadeIBGE.Length < 2) ? "00" : endEmp.cidadeIBGE.Substring(0, 2);
                    String codMov = mov.id.ToString().PadRight(9, '0');

                    String cpf = cliEmp.cpf_cnpj;

                    String modelo = mov.modeloNF.ToString();
                    String serieNF = mov.serieNF;
                    String numeroNF = mov.numeroNF.ToString();

                    //numeroNota
                    
                    movnfeDados.chaveAcessoNFE = cMov.gerarChave(codUF, data, cpf, modelo, serieNF, numeroNF, codMov);
                    //movnfeVeiculos
                    MovNfeVeiculo nfeVeiculoDados = new MovNfeVeiculo();
                    if (movnfeDados.__nfeVeiculo != null)
                        Utils.copiaCamposBasicos(movnfeDados.__nfeVeiculo, nfeVeiculoDados);
                    else
                        nfeVeiculoDados = null;
                    movnfeDados.__nfeVeiculo = null;

                    //busca movNFE
                    MovNFE movNFE = cMov.LoadNFE(mov.id);
                    if (movNFE == null)
                    {
                        //Cria nova NFE
                        movNFE = new MovNFE();
                        Utils.copiaCamposBasicos(movnfeDados, movNFE);
                        movNFE = cMov.NovaNFE(mov, movnfeDados);
                    }
                    else
                    {
                        //Alterar Nfe                        
                        movnfeDados.id = movNFE.id;
                        Utils.copiaCamposBasicos(movnfeDados, movNFE);
                        cMov.AtualizaNFE(mov, movNFE);
                    }

                    //atualização da nfeVeiculos
                    if (nfeVeiculoDados != null)
                    {
                        //throw new Exception("não nulo");
                        MovNfeVeiculo nfeVeiculo = cMov.LoadNFEVeiculo(movNFE.id);
                        if (nfeVeiculo == null)
                            nfeVeiculo = new MovNfeVeiculo();
                        Utils.copiaCamposBasicos(nfeVeiculoDados, nfeVeiculo);
                        nfeVeiculo.idMovNFE = movNFE.id;
                        cMov.SalvaNFEVeiculo(nfeVeiculo);
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

        public void DefineMovNFE_Enviada(int idCorp, int idMov)
        {
            setBancoID(idCorp);
            try
            {
                Mov mov = cMov.Load(idMov);
                cMov.SalvaNFE_Enviada(idMov);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
        }

        public string GetUltimaInfoAdicional(int idCorp)
        {
            setBancoID(idCorp);
            MovNFE nfe = cMov.LoadUltimaNFE();
            
            return (nfe==null)
                ? ""
                : nfe.infoAdicional;
        }
        public int GetProximoNumeroNFE(int idCorp)
        {
            setBancoID(idCorp);
            MovNFE nfeUltima = cMov.LoadUltimaNFE();
            if(nfeUltima == null)
                return 1;

            int ultimoNumero = nfeUltima.numeroNota;
            bool existeNumeroNota = true;
            while (existeNumeroNota)
            {
                ultimoNumero++;
                MovNFE nfe = cMov.LoadNFENumeroNota(ultimoNumero);
                if (nfe == null)
                    break;
            }
            return ultimoNumero + 1;
        }

        public String[] GerarXml(int idCorp, int idMov)
        {
            setBancoID(idCorp);
            try
            {
                #region Pesquisas
                //Busca Movimentação
                Mov mov = cMov.Load(idMov);
                MovNFE movNfe = cMov.LoadNFE(mov.id);
                List<MovItem> mItens = cMov.LoadMovItens(mov.id);
                List<MovValor> mValores = cMov.LoadMovValores(mov.id);
                //nfe referenciada
                //finalidade ajuste , complementar
                MovNFE nfeRF = null;
                if (movNfe.finalidadeNFE != ENfeFinalidade.normal)
                {
                    nfeRF = cMov.LoadNFE(movNfe.idmovReferenciada);
                }


                //Busca - Itens da Movimentação
                foreach (MovItem mi in mItens)
                {
                    mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);

                    mi.__item = cItem.Load(mi.idItem);

                    mi.__item.__ie = cItem.LoadItemEmp(mi.idItem, mov.idEmp);
                    mi.__item.__ie.__aliquotas = cItem.LoadAliquotas(mov.idEmp, mi.idItem);
                }

                //Busca Pessoa da Empresa
                Empresa emp = cEmp.Load(mov.idEmp);
                Cliente cliEmp = cCliente.Load(emp.idCliente);
                ClienteEndereco endEmp = cCliente.LoadEndereco(movNfe.idEnderecoEmp);
                if (endEmp == null)
                    throw new Exception(string.Format("Empresa {0} não tem Endereco", cliEmp.id));

                //Busca Cliente / Fornecedor
                Cliente cli = cCliente.Load(mov.idCliente);
                ClienteEndereco endCliForn = cCliente.LoadEndereco(movNfe.idEnderecoCliente);
                if (endCliForn == null)
                    throw new Exception(string.Format("Cliente {0} não tem endereço.", cli.id));

                //Busca Local Retirada
                ClienteEndereco peRetirada = null;
                if (movNfe.idEnderecoRetirada != 0)
                    peRetirada = cCliente.LoadEndereco(movNfe.idEnderecoRetirada);
                //Busca Local de Entrega
                ClienteEndereco peEntrega = null;
                if (movNfe.idEnderecoEntrega != 0)
                    peEntrega = cCliente.LoadEndereco(movNfe.idEnderecoEntrega);

                //Busca Transportador
                Cliente cliTransp = cCliente.Load(movNfe.idClienteTransp);
                ClienteEndereco endTrans = cCliente.LoadEndereco(movNfe.idEnderecoTransp);                
                /*
                if (endTrans == null )
                    throw new Exception("Transportador não tem endereço");
                */
                //veiculo e reboque
                ClienteVeiculo cVeiculo = cCliente.LoadVeiculo(movNfe.idVeiculo);
                ClienteVeiculo cReboque = cCliente.LoadVeiculo(movNfe.idReboque);
                #endregion
                //
                #region EscreveXml

                StringBuilder sb = new StringBuilder();

                //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
                XmlWriterSettings oSettings = new XmlWriterSettings();
                oSettings.Indent = true;
                oSettings.IndentChars = "     ";
                //
                XmlWriter writer = XmlWriter.Create(sb, oSettings);
                EscreveItensNfe xmlNFE = new EscreveItensNfe();

                DateTime dtTemp = DateTime.Parse(mov.dthrMovEmissao);
                string ano = dtTemp.ToString("yyyy").Substring(2);
                string mes = dtTemp.ToString("MM");
                string data = ano+mes;

                String codUF = (endEmp.cidadeIBGE.Length < 2) ? "00" : endEmp.cidadeIBGE.Substring(0, 2);
                String codMov = mov.id.ToString().PadRight(9, '0');
                //String chaveAcesso = cMov.gerarChave( codUF, data, cliEmp.__pessoa.cpf_cnpj, mov.modeloNF.ToString(), mov.serieNF, mov.numeroNF.ToString() , codMov);

                //movNfe.chaveAcessoNFE

                //inicio do XML
                xmlNFE.Escreve_Inicio(writer, movNfe.chaveAcessoNFE);
                //emitente, remetente
                //emitente, remetente
                string ufIBGE = "";
                /*if (mov.resumo == EMovResumo.saida)
                {
                    ufIBGE = (endEmp.cidadeIBGE.Length < 2) ? "" : endEmp.cidadeIBGE.Substring(0, 2);
                    //xmlNFE.Escreve_ide(writer, mov, movNfe, nfeRF, ufIBGE, endEmp.cidadeIBGE, codMov);
                    xmlNFE.Escreve_emit(writer, cliEmp, endEmp);


                    xmlNFE.Escreve_dest(writer, cli, endCliForn, movNfe.clienteIE);
                    // Local de Retirada da Empresa, Entrega 
                    if (endEmp.id != movNfe.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, cliEmp.tipo, cliEmp.cpf_cnpj, peRetirada);
                    if (endCliForn.id != movNfe.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, cli.tipo, cli.cpf_cnpj, peEntrega);
                }
                else if (mov.resumo == EMovResumo.entrada)
                {
                    ufIBGE = (endCliForn.cidadeIBGE.Length < 2) ? "" : endCliForn.cidadeIBGE.Substring(0, 2);
                    //xmlNFE.Escreve_ide(writer, mov, movNfe, nfeRF, ufIBGE, endCliForn.cidadeIBGE, codMov);
                    xmlNFE.Escreve_emit(writer, cli, endCliForn);
                    xmlNFE.Escreve_dest(writer, cliEmp, endEmp, movNfe.clienteIE);
                    // Local de Retirada do Cliente, Entrega
                    if (endCliForn.id != movNfe.idEnderecoRetirada && peRetirada != null)
                        xmlNFE.Escreve_retirada(writer, cli.tipo, cli.cpf_cnpj, peRetirada);
                    if (endEmp.id != movNfe.idEnderecoEntrega && peEntrega != null)
                        xmlNFE.Escreve_entrega(writer, cliEmp.tipo, cliEmp.cpf_cnpj, peEntrega);
                }*/

                //itens da movimentação
                int numItem = 0;
                foreach (MovItem mi in mItens)
                {
                    numItem++;
                    //xmlNFE.Escreve_det(writer, mi, mov.tipo, numItem);
                }
                xmlNFE.Escreve_total(writer, mov);
                xmlNFE.Escreve_transp(writer, cliTransp, endTrans, cVeiculo, cReboque, movNfe);

                if (movNfe.infoAdicional.Trim()!="")
                    xmlNFE.Escreve_infAdicXML(writer, movNfe.infoAdicional, movNfe);
                //final do arquivo
                xmlNFE.Escreve_Fim(writer);
                #endregion

                writer.Flush();
                writer.Close();
                string retorno = sb.ToString();
                return new string[] { retorno, movNfe.chaveAcessoNFE };
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
        }



        public String[] GerarTXT(int idCorp, int idMov)
        {
            setBancoID(idCorp);
            try
            {
                StringBuilder sb = new StringBuilder();
                #region Pesquisas
                //Busca Movimentação
                Mov mov = cMov.Load(idMov);
                MovNFE movNfe = cMov.LoadNFE(mov.id);
                //busca nfeVeiculo
                MovNfeVeiculo nfeVeiculo = null;
                if (movNfe.ehVendaVeiculo)
                    nfeVeiculo = cMov.LoadNFEVeiculo(movNfe.id);

                List<MovItem> mItens = cMov.LoadMovItens(mov.id);
                //List<MovValor> mValores = cMov.LoadMovValores(mov.id);

                //nfe referenciada
                //finalidade ajuste , complementar
                MovNFE nfeRF = null;
                if (movNfe.finalidadeNFE != ENfeFinalidade.normal)
                {
                    nfeRF = cMov.LoadNFE(movNfe.idmovReferenciada);
                }

                //Busca - Itens da Movimentação
                foreach (MovItem mi in mItens)
                {
                    mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);

                    mi.__item = cItem.Load(mi.idItem);
                    mi.__item.__ie = cItem.LoadItemEmp(mi.idItem, mov.idEmp);
                    mi.__item.__ie.__aliquotas = cItem.LoadAliquotas(mov.idEmp, mi.idItem);
                }

                //Busca Pessoa da Empresa
                Empresa emp = cEmp.Load(mov.idEmp);
                emp.__cliente = cCliente.Load(emp.idCliente);
                List<ClienteContato> listConEmp = cCliente.LoadContatos(emp.idCliente);
                ClienteContato conEmp = null;
                foreach (ClienteContato cc in listConEmp)
                    if (cc.tipo == EContatoTipo.fone_fixo)
                    {
                        conEmp = cc;
                        continue;
                    }
                ClienteEndereco endEmp = cCliente.LoadEndereco(movNfe.idEnderecoEmp);
                if (endEmp == null)
                    throw new ExcecaoSDE("Empresa " + emp.__cliente.id + " não tem Endereco");

                //Busca Cliente / Fornecedor
                Cliente cli = cCliente.Load(mov.idCliente);
                List<ClienteContato> listConCliForn = cCliente.LoadContatos(mov.idCliente);
                ClienteContato conCliForn = null;
                foreach (ClienteContato cc in listConCliForn)
                    if (cc.tipo == EContatoTipo.fone_fixo)
                    {
                        conCliForn = cc;
                        continue;
                    }
                ClienteEndereco endCliForn = cCliente.LoadEndereco(movNfe.idEnderecoCliente);
                if (endCliForn == null)
                    throw new ExcecaoSDE("Cliente " + cli.id + " não tem endereço.");

                //Busca Local Retirada
                ClienteEndereco peRetirada = null;
                if (movNfe.idEnderecoRetirada != 0)
                    peRetirada = cCliente.LoadEndereco(movNfe.idEnderecoRetirada);
                //Busca Local de Entrega
                ClienteEndereco peEntrega = null;
                if (movNfe.idEnderecoEntrega != 0)
                    peEntrega = cCliente.LoadEndereco(movNfe.idEnderecoEntrega);

                //Busca Transportador
                Cliente cliTransp = cCliente.Load(movNfe.idClienteTransp);
                ClienteEndereco endTrans = cCliente.LoadEndereco(movNfe.idEnderecoTransp);
                //veiculo e reboque
                ClienteVeiculo cVeiculo = cCliente.LoadVeiculo(movNfe.idVeiculo);
                ClienteVeiculo cReboque = cCliente.LoadVeiculo(movNfe.idReboque);
                #endregion
                //
                #region Verificar Objetos
                //lista de objetos
                List<object> lista = new List<object>();
                lista.Add(mov);
                lista.Add(movNfe);
                lista.Add(emp.__cliente);
                lista.Add(cli);
                lista.Add(endEmp);
                lista.Add(endCliForn);
                if (cliTransp != null)
                    lista.Add(cliTransp);
                if(endTrans !=  null && cliTransp != null)
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
                string codNota = movNfe.numeroNota.ToString().PadLeft(9, '0');
                int digitoVerificador = txtNFE.gerarDV(endEmp.ufIBGE, mov.dthrMovEmissao, emp.__cliente.cpf_cnpj, "55", movNfe.serieNota.ToString().PadLeft(3, '0')
                                                , codNota, codNota);
                string chaveAcesso = txtNFE.gerarChave(endEmp.ufIBGE, mov.dthrMovEmissao, emp.__cliente.cpf_cnpj, "55", movNfe.serieNota.ToString().PadLeft(3, '0')
                                                , codNota, codNota, digitoVerificador.ToString());

                //inicio do TXT
                txtNFE.Escreve_Inicio(sb, chaveAcesso);
                //emitente, remetente

                string ufIBGE = "";
                if (mov.resumo == EMovResumo.saida || mov.tipo == EMovTipo.entrada_devolucao)
                {
                    txtNFE.Escreve_ide(sb, mov, movNfe, nfeRF, endEmp.ufIBGE, endEmp.cidadeIBGE);

                    txtNFE.Escreve_emit(sb, emp.__cliente, endEmp, conEmp);
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
                    txtNFE.Escreve_ide(sb, mov, movNfe, nfeRF, endCliForn.ufIBGE, endCliForn.cidadeIBGE);
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
                    txtNFE.Escreve_det(sb, mi, mov, numItem, nfeVeiculo);
                }

                txtNFE.Escreve_total(sb);
                if(cliTransp != null)
                    txtNFE.Escreve_transp(sb, cliTransp, endTrans, cVeiculo, cReboque, movNfe);
                txtNFE.Escreve_fatura(sb, movNfe.fatura);
                txtNFE.Escreve_infAdic(sb, movNfe.infoAdicional);
                //final do arquivo
                txtNFE.Escreve_Fim(sb);


                string retorno = sb.ToString();

                /*
                string sArquivo = string.Format("C:\\NFE_GERADAS\\{0}-nfe.txt", chaveAcesso);
                File.WriteAllText(sArquivo,
                    sb.ToString(), Encoding.UTF8);
                 * */
                #endregion
                return new string[] { retorno, movNfe.chaveAcessoNFE };
            }
            catch (ExcecaoSDE)
            {
                throw;
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
        }


        private void verificarObjetos(List<object> objetos)
        {
            foreach (object obj in objetos)
                if(obj != null)
                    Utils.retiraCaracterEspecial(obj);
        }

    }
}
