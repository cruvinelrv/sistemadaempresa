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
using SDE.Entidade;
using System.Collections.Generic;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void Usuario_NovoAtualizacao(int idCorp, int idEmp,
            ClienteFuncionarioUsuario clienteFuncionarioUsuario, List<ClienteFuncionarioPermissao> listClienteFuncionarioPermissoes)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    //NOVO CADASTRO
                    if (clienteFuncionarioUsuario.id == 0)
                    {
                        clienteFuncionarioUsuario.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0);
                        clienteFuncionarioUsuario.idEmp = idEmp;

                        //BUSCA O CLIENTE FUNCIONARIO
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(clienteFuncionarioUsuario.idCliente);
                        IObjectSet rs_clienteFuncionario = query.Execute();
                        Cliente clienteFuncionario = rs_clienteFuncionario[0] as Cliente;
                        
                        //INFORMA QUE O FUNCIONÁRIO É UM USUÁRIO DO SISTEMA A SALVA
                        clienteFuncionario.usuarioSistema = true;
                        dbStore(db, clienteFuncionario);

                        //SALVA AS CONFIGURAÇÕES DE ACESSO DO USUÁRIO
                        foreach (ClienteFuncionarioPermissao clienteFuncionarioPermissao in listClienteFuncionarioPermissoes)
                        {
                            clienteFuncionarioPermissao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioPermissao), 0);
                            clienteFuncionarioPermissao.idEmp = idEmp;
                            clienteFuncionarioPermissao.idClienteFuncionarioUsuario = clienteFuncionarioUsuario.id;
                            dbStore(db, clienteFuncionarioPermissao);
                        }

                        dbStore(db, clienteFuncionarioUsuario);
                    }
                    //ATUALIZAÇÃO DE CADASTRO EXISTENTE
                    else
                    {
                        //BUSCA O USUÁRIO A ALTERAR NO BANCO DE DADOS
                        query = db.Query();
                        query.Constrain(typeof(ClienteFuncionarioUsuario));
                        query.Descend("id").Constrain(clienteFuncionarioUsuario.id);
                        query.Descend("idEmp").Constrain(idEmp);
                        IObjectSet rs_clienteFuncionarioUsuario = query.Execute();
                        ClienteFuncionarioUsuario clienteFuncionarioUsuario_db = rs_clienteFuncionarioUsuario[0] as ClienteFuncionarioUsuario;

                        //SERÁ FEITA A ATUALIZAÇÃO SOBRE AS CONFIGURAÇÕES DE ACESSO DO USUÁRIO
                        foreach (ClienteFuncionarioPermissao clienteFuncionarioPermissao in listClienteFuncionarioPermissoes)
                        {
                            //É UMA NOVA CONFIGURAÇÃO, SERÁ RELACIONADA AO USUARIO E SALVA
                            if (clienteFuncionarioPermissao.id == 0)
                            {
                                clienteFuncionarioPermissao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioPermissao), 0);
                                clienteFuncionarioPermissao.idEmp = idEmp;
                                clienteFuncionarioPermissao.idClienteFuncionarioUsuario = clienteFuncionarioUsuario_db.id;
                                dbStore(db, clienteFuncionarioPermissao);
                            }
                            //CONFIGURAÇÃO EXISTIA, SERÁ FEITA A ATUALIZAÇÃO DA MESMA
                            else
                            {
                                //BUSCA O CLIENTE FUNCIONARIO PERMISSÃO A ALTERAR NO BANCO DE DADOS
                                query = db.Query();
                                query.Constrain(typeof(ClienteFuncionarioPermissao));
                                query.Descend("id").Constrain(clienteFuncionarioPermissao.id);
                                query.Descend("idEmp").Constrain(idEmp);
                                IObjectSet rs_clienteFuncionarioPermissao = query.Execute();
                                ClienteFuncionarioPermissao clienteFuncionarioPermissao_db = rs_clienteFuncionarioPermissao[0] as ClienteFuncionarioPermissao;

                                //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                                Utils.copiaCamposBasicos(clienteFuncionarioPermissao, clienteFuncionarioPermissao_db);
                                dbStore(db, clienteFuncionarioPermissao_db);
                            }
                        }
                        Utils.copiaCamposBasicos(clienteFuncionarioUsuario, clienteFuncionarioUsuario_db);
                        dbStore(db, clienteFuncionarioUsuario_db);
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }
    }
}
