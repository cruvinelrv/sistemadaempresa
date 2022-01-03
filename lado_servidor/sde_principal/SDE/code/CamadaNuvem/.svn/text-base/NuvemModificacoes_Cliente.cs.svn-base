using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.CamadaControle;
using SDE.Constantes;
using SDE.PDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {

        public void RelatorioCliente_EscrevePdf(int idCorp, int idEmp)
        {
            ConstrutorPDF.RelatorioCliente(idCorp, idEmp);
        }

        public void Cliente_Genericos_Salva(int idCorp, string classe, IList objetos)
        {
            object trueAsObject = true as object;

            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (!classe.StartsWith("Cliente"))
                        throw new Exception("CLASSE '" + classe + "' NÃO SUPORTADA");

                    Type t = Assembly.Load("SDE").GetType(string.Concat("SDE.Entidade.", classe));
                    if (t == null)
                        throw new Exception("CLASSE '" + classe + "' NÃO ENCONTRADA EM 'SDE'");
                    //int proxID = AppFacade.get.reaproveitamento.getIncremento(db, t, objetos.Count);

                    List<string> anota = new List<string>();
                    anota.Add("percorrendo cada objeto");

                    foreach (Object xxx in objetos)
                    {
                        int xxxId = Convert.ToInt32(t.GetField("id").GetValue(xxx));


                        anota.Add("id: " + xxxId.ToString());
                        //primeiro caso, inserção
                        if (xxxId == 0)
                        {
                            int prox = AppFacade.get.reaproveitamento.getIncremento(db, t, 0);
                            t.GetField("id").SetValue(xxx, prox);
                            Utils.filtraCampos(xxx);
                            dbStore(db, xxx);

                            anota.Add("criou " + prox.ToString());
                            continue;
                        }

                        anota.Add("não é inserção");
                        //outros casos, alteração e remoção
                        IList ls = db.Query(t);
                        foreach (Object obj in ls)
                        {
                            int objId = Convert.ToInt32(t.GetField("id").GetValue(obj));

                            if (objId == xxxId)
                            {
                                anota.Add("encontrou correspondencia!");
                                //encontrou correspondencia
                                if ((Boolean)t.GetField("isDeletado").GetValue(xxx))
                                {
                                    anota.Add("é uma remoção");
                                    dbRemove(db, obj);
                                }
                                else
                                {
                                    anota.Add("é uma alteração");
                                    Utils.copiaCamposBasicos(xxx, obj);
                                    dbStore(db, obj);
                                }

                                break;
                            }
                        }
                    }

                    string s = "";
                    foreach (String ss in anota)
                    {
                        s += ss + Environment.NewLine;
                    }
                    //throw new ExcecaoSDE(s);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }//lock-end

        }


        public Cliente Cliente_NovoAltera(int idCorp, Cliente cliente)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Cliente clienteBanco = AppFacade.get.reaproveitamento.Cliente_Load(db, cliente.cpf_cnpj);

                    //throw new Exception(cliente.cpf_cnpj + " nulo: " + (clienteBanco == null) as string);
                    //verifica se já existe
                    //if (clienteBanco != null)
                    //    throw new ExcecaoSDE("já existe um cliente cadastrado com esse cpf");
                    if (cliente.id == 0 && clienteBanco == null)
                    {

                        //atualiza campos
                        //ProximoCampo prox = new ProximoCampo(idCorp);
                        //int proxID = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
                        cliente.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
                        Utils.filtraCampos(cliente);

                        //throw new ExcecaoSDE("preciso validar CPF, NOME, APEL em server-side");

                        //salva
                        dbStore(db, cliente);
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Items["idCorp"] = idCorp;
                        CamadaControle.CCliente cCli = new SDE.CamadaControle.CCliente();
                        //cCli.Atualizar(cliente.id, cliente.__bancarios, _insercoes, _remocoes);
                        cCli.Atualizar(cliente.id, cliente.__contatos, _insercoes, _remocoes);
                        cCli.Atualizar(cliente.id, cliente.__enderecos, _insercoes, _remocoes);
                        cCli.Atualizar(cliente.id, cliente.__familiares, _insercoes, _remocoes);
                        cCli.Atualizar(cliente.id, cliente.__veiculos, _insercoes, _remocoes);



                        //
                        Utils.copiaCamposBasicos(cliente, clienteBanco);
                        dbStore(db, clienteBanco);
                    }

                    dbCommit(db);
                    //Console.Beep(2000, 500);
                    return cliente;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    //Console.Beep(200, 500);
                    return null;
                }
            }//lock-end

        }

        public int Cargo_NovoAltera(int idCorp, int idEmp, int idClienteFuncionarioLogado, Cargo cargo)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (cargo.id == 0)
                    {
                        cargo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cargo), 0);
                        cargo.idEmp = idEmp;
                        cargo.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        dbStore(db, cargo);
                    }
                    else
                    {
                        Cargo cargoBD = null;
                        IQuery query = db.Query();
                        query.Constrain(typeof(Cargo));
                        query.Descend("id").Constrain(cargo.id);
                        cargoBD = query.Execute()[0] as Cargo;
                        Utils.copiaCamposBasicos(cargo, cargoBD);
                        cargoBD.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        dbStore(db, cargoBD);
                    }
                    dbCommit(db);
                    return cargo.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public List<CargoPermissao> CargoPermissoes_NovosAtualiza(int idCorp, List<CargoPermissao> objetos)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (CargoPermissao xxx in objetos)
                    {
                        if (xxx.id == 0)
                        {
                            xxx.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoPermissao), 0);
                            dbStore(db, xxx);
                        }
                        else
                        {
                            IQuery query = db.Query();
                            query.Constrain(typeof(CargoPermissao));
                            query.Descend("id").Constrain(xxx.id);
                            IObjectSet rs = query.Execute();
                            CargoPermissao cargoPermissoes = (CargoPermissao)rs[0];
                            Utils.copiaCamposBasicos(xxx, cargoPermissoes);
                            dbStore(db, cargoPermissoes);
                        }
                    }
                    dbCommit(db);
                    return objetos;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void ClienteFuncionario_SalvaDados(int idCorp, Cliente cliente)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Cliente clienteBD = null;
                    IQuery query = db.Query();
                    query.Constrain(typeof(Cliente));
                    query.Descend("id").Constrain(cliente.id);
                    clienteBD = query.Execute()[0] as Cliente;
                    Utils.copiaCamposBasicos(cliente, clienteBD);
                    dbStore(db, clienteBD);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        public void ClienteFuncionario_Atualiza(int idCorp, int idEmp, Cliente clienteFuncionario,
            ClienteFuncionarioComissionamento clienteFuncionarioComissionamento, List<ClienteFuncionarioPermissao> listClienteFuncionarioPermissoes)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    //BUSCA FUNCIONÁRIO A ALTERAR NO BANCO DE DADOS
                    query = db.Query();
                    query.Constrain(typeof(Cliente));
                    query.Descend("id").Constrain(clienteFuncionario.id);
                    IObjectSet rs_clienteFuncionario = query.Execute();
                    Cliente clienteFuncionario_db = rs_clienteFuncionario[0] as Cliente;

                    //ATUALIZA AS CONFIGURAÇÕES DE COMISSIONAMENTO SE O FUNCIONÁRIO FOR COMISSIONADO
                    if (clienteFuncionario.comissionado)
                    {
                        //FUNCIONÁRIO NÃO ERA COMISSIONADO, SERÁ FEITA UMA NOVA CONFIGURAÇÃO
                        if (clienteFuncionarioComissionamento.id == 0)
                        {
                            clienteFuncionarioComissionamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioComissionamento), 0);
                            clienteFuncionarioComissionamento.idEmp = idEmp;
                            clienteFuncionarioComissionamento.idCliente = clienteFuncionario.id;
                            dbStore(db, clienteFuncionarioComissionamento);
                        }
                        //FUNCIONÁRIO JÁ ERA COMISSIONADO, SERÁ FEITA ATUALIZAÇÃO DAS CONFIGURAÇÕES
                        else
                        {
                            //BUSCA O CLIENTE FUNCIONARIO COMISSIONAMENTO A ALTERAR NO BANCO DE DADOS
                            query = db.Query();
                            query.Constrain(typeof(ClienteFuncionarioComissionamento));
                            query.Descend("id").Constrain(clienteFuncionarioComissionamento.id);
                            IObjectSet rs_clienteFuncionarioComissionamento = query.Execute();
                            ClienteFuncionarioComissionamento clienteFuncionarioComissionamento_db = rs_clienteFuncionarioComissionamento[0] as ClienteFuncionarioComissionamento;

                            //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                            Utils.copiaCamposBasicos(clienteFuncionarioComissionamento, clienteFuncionarioComissionamento_db);
                            dbStore(db, clienteFuncionarioComissionamento_db);
                        }
                    }
                    //O FUNCIONÁRIO ERA COMISSIONADO E PASSARÁ A NÃO SER MAIS, REMOVER AS CONFIGURAÇÕES SOBRE COMISSÃO DO FUNCIONÁRIO
                    if (!clienteFuncionario.comissionado && clienteFuncionario_db.comissionado)
                    {
                        //BUSCA O CLIENTE FUNCIONÁRIO COMISSIONAMENTO A SER REMOVIDO NO BANCO DE DADOS
                        query = db.Query();
                        query.Constrain(typeof(ClienteFuncionarioComissionamento));
                        query.Descend("idCliente").Constrain(clienteFuncionario_db.id);
                        IObjectSet rs_clienteFuncionarioComissionamento = query.Execute();
                        ClienteFuncionarioComissionamento clienteFuncionarioComissionamento_db = rs_clienteFuncionarioComissionamento[0] as ClienteFuncionarioComissionamento;

                        //REMOVE DO BANCO AS CONFIGURAÇÕES DO FUNCIONÁRIO QUE NÃO SERÁ MAIS COMISSIONADO
                        dbRemove(db, clienteFuncionarioComissionamento_db);
                    }

                    //ATUALIZA AS CONFIGURAÇÕES DE ACESSO AO SISTEMA SE O FUNCIONÁRIO FOR UM USUÁRIO
                    if (clienteFuncionario.usuarioSistema)
                    {
                        ClienteFuncionarioUsuario clienteFuncionarioUsuario_db = getClienteFuncionarioUsuarioByClienteFuncionario(clienteFuncionario_db.id, query, db);

                        foreach (ClienteFuncionarioPermissao cfp in listClienteFuncionarioPermissoes)
                        {
                            //É UMA NOVA CONFIGURAÇÃO, SERÁ RELACIONADA AO FUNCIONÁRIO E SALVA
                            if (cfp.id == 0)
                            {
                                cfp.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioPermissao), 0);
                                cfp.idEmp = idEmp;
                                cfp.idClienteFuncionarioUsuario = clienteFuncionarioUsuario_db.id;
                                dbStore(db, cfp);
                            }
                            //CONFIGURAÇÃO EXISTIA, SERÁ FEITA ATUALIZAÇÃO DA MESMA
                            else
                            {
                                //BUSCA O CLIENTE FUNCIONÁRIO PERMISSÃO A SER ALTERADO NO SISTEMA
                                query = db.Query();
                                query.Constrain(typeof(ClienteFuncionarioPermissao));
                                query.Descend("id").Constrain(cfp.id);
                                IObjectSet rs_clienteFuncionarioPermissao = query.Execute();
                                ClienteFuncionarioPermissao clienteFuncionarioPermissao_db = rs_clienteFuncionarioPermissao[0] as ClienteFuncionarioPermissao;

                                //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                                Utils.copiaCamposBasicos(cfp, clienteFuncionarioPermissao_db);
                                dbStore(db, clienteFuncionarioPermissao_db);
                            }
                        }
                    }
                    //O FUNCIONÁRIO POSSUIA ACESSO AO SISTEMA E PASSARÁ A NÃO TER MAIS, REMOVER AS CONFIGURAÇÕES DE ACESSO DO FUNCIONÁRIO
                    if (!clienteFuncionario.usuarioSistema && clienteFuncionario_db.usuarioSistema)
                    {
                        ClienteFuncionarioUsuario clienteFuncionarioUsuario_db = getClienteFuncionarioUsuarioByClienteFuncionario(clienteFuncionario_db.id, query, db);

                        //BUSCA CONFIGURAÇÕES DE ACESSO QUE O FUNCIONÁRIO POSSUIA PARA SEREM REMOVIDOS
                        query = db.Query();
                        query.Constrain(typeof(ClienteFuncionarioPermissao));
                        query.Descend("idClienteFuncionarioUsuario").Constrain(clienteFuncionarioUsuario_db.id);
                        IObjectSet rs_clienteFuncionarioPermissoes = query.Execute();
                        for (int i = 0; i < rs_clienteFuncionarioPermissoes.Count; i++)
                        {
                            //REMOVE DO BANCO AS CONFIGURAÇÕES DO FUNCIONÁRIO QUE NÃO TERÁ MAIS ACESSO AO SISTEMA
                            ClienteFuncionarioPermissao clienteFuncionarioPermissao_db = rs_clienteFuncionarioPermissoes[i] as ClienteFuncionarioPermissao;
                            dbRemove(db, clienteFuncionarioPermissao_db);
                        }
                    }

                    //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                    Utils.copiaCamposBasicos(clienteFuncionario, clienteFuncionario_db);
                    dbStore(db, clienteFuncionario_db);

                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }





        //FUNÇÕES AUXILIARES
        private ClienteFuncionarioUsuario getClienteFuncionarioUsuarioByClienteFuncionario(int clienteFuncionarioId, IQuery query, IObjectContainer db)
        {
            //BUSCA CLIENTE FUNCIONÁRIO USUÁRIO NO BANCO DE DADOS
            query = db.Query();
            query.Constrain(typeof(ClienteFuncionarioUsuario));
            query.Descend("idCliente").Constrain(clienteFuncionarioId);
            IObjectSet rs_clienteFuncionarioUsuario = query.Execute();
            ClienteFuncionarioUsuario clienteFuncionarioUsuario_db = rs_clienteFuncionarioUsuario[0] as ClienteFuncionarioUsuario;
            return clienteFuncionarioUsuario_db;
        }
    }


}