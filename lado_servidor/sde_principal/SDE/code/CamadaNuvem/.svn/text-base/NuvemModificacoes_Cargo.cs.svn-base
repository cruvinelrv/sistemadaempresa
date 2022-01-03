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
        public void Cargo_NovoAtualizacao(int idCorp, int idEmp, int idClienteFuncionarioLogado,
            Cargo cargo, CargoComissionamento cargoComissionamento, List<CargoPermissao> listCargoPermissoes)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //NOVO CADASTRO
                    if (cargo.id == 0)
                    {
                        cargo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cargo), 0);
                        cargo.idEmp = idEmp;
                        cargo.idClienteFuncionarioLogado = idClienteFuncionarioLogado;

                        //SALVA AS CONFIGURAÇÕES GENÉRICAS DE COMISSIONAMENTO SE O CARGO FOR COMISSIONADO
                        if (cargo.comissionado)
                        {
                            cargoComissionamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoComissionamento), 0);
                            cargoComissionamento.idEmp = idEmp;
                            cargoComissionamento.idCargo = cargo.id;
                            dbStore(db, cargoComissionamento);
                        }

                        //SALVA AS CONFIGURAÇÕES GENÉRICAS DE ACESSO AO SISTEMA SE O CARGO TIVER ACESSO
                        if (cargo.acessaSistema)
                        {
                            foreach (CargoPermissao cargoPermissao in listCargoPermissoes)
                            {
                                cargoPermissao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoPermissao), 0);
                                cargoPermissao.idEmp = idEmp;
                                cargoPermissao.idCargo = cargo.id;
                                dbStore(db, cargoPermissao);
                            }
                        }

                        dbStore(db, cargo);
                    }
                    //ATUALIZAÇÃO DE CADASTRO EXISTENTE
                    else
                    {
                        IQuery query;

                        //BUSCA O CARGO A ALTERAR NO BANCO DE DADOS
                        query = db.Query();
                        query.Constrain(typeof(Cargo));
                        query.Descend("id").Constrain(cargo.id);
                        IObjectSet rs_cargo = query.Execute();
                        Cargo cargo_db = rs_cargo[0] as Cargo;

                        //ATUALIZA AS CONFIGURAÇÕES GENÉRICAS DE COMISSIONAMENTO SE O CARGO FOR COMISSIONADO
                        if (cargo.comissionado)
                        {
                            //CARGO NÃO ERA COMISSIONADO, SERÁ FEITA UMA NOVA CONFIGURAÇÃO
                            if (cargoComissionamento.id == 0)
                            {
                                cargoComissionamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoComissionamento), 0);
                                cargoComissionamento.idEmp = idEmp;
                                cargoComissionamento.idCargo = cargo_db.id;
                                dbStore(db, cargoComissionamento);
                            }
                            //CARGO JÁ ERA COMISSIONADO, SERÁ FEITA A ATUALIZAÇÃO DAS CONFIGURAÇÕES
                            else
                            {
                                //BUSCA O CARGO COMISSIONAMENTO A ALTERAR NO BANCO DE DADOS
                                query = db.Query();
                                query.Constrain(typeof(CargoComissionamento));
                                query.Descend("id").Constrain(cargoComissionamento.id);
                                IObjectSet rs_cargoComissionamento = query.Execute();
                                CargoComissionamento cargoComissionamento_db = rs_cargoComissionamento[0] as CargoComissionamento;

                                //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                                Utils.copiaCamposBasicos(cargoComissionamento, cargoComissionamento_db);
                                dbStore(db, cargoComissionamento_db);
                            }
                        }
                        //O CARGO ERA COMISSIONADO E PASSARÁ A NÃO SER MAIS, REMOVER AS CONFIGURAÇÕES SOBRE COMISSÃO DO CARGO
                        if (!cargo.comissionado && cargo_db.comissionado)
                        { 
                            //BUSCA O CARGO COMISSIONAMENTO A REMOVER NO BANCO DE DADOS
                            query = db.Query();
                            query.Constrain(typeof(CargoComissionamento));
                            query.Descend("idCargo").Constrain(cargo_db.id);
                            IObjectSet rs_cargoComissionamento = query.Execute();
                            CargoComissionamento cargoComissionamento_db = rs_cargoComissionamento[0] as CargoComissionamento;

                            //REMOVE DO BANCO AS CONFIGURAÇÕES DO CARGO QUE NÃO SERÁ MAIS COMISSIONADO
                            dbRemove(db, cargoComissionamento_db);
                        }

                        //ATUALIZA AS CONFIGURAÇÕES GENÉRICAS DE ACESSO AO SISTEMA SE O CARGO TIVER ACESSO
                        if (cargo.acessaSistema)
                        {
                            foreach (CargoPermissao cargoPermissao in listCargoPermissoes)
                            {
                                //É UMA NOVA CONFIGURAÇÃO, SERÁ RELACIONADA AO CARGO E SALVA
                                if (cargoPermissao.id == 0)
                                {
                                    cargoPermissao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoPermissao), 0);
                                    cargoPermissao.idEmp = idEmp;
                                    cargoPermissao.idCargo = cargo_db.id;
                                    dbStore(db, cargoPermissao);
                                }
                                //CONFIGURAÇÃO EXISTIA, SERÁ FEITA A ATUALIZAÇÃO DA MESMA
                                else
                                {
                                    //BUSCA O CARGO PERMISSÃO A ALTERAR NO BANCO DE DADOS
                                    query = db.Query();
                                    query.Constrain(typeof(CargoPermissao));
                                    query.Descend("id").Constrain(cargoPermissao.id);
                                    IObjectSet rs_cargoPermissao = query.Execute();
                                    CargoPermissao cargoPermissao_db = rs_cargoPermissao[0] as CargoPermissao;

                                    //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                                    Utils.copiaCamposBasicos(cargoPermissao, cargoPermissao_db);
                                    dbStore(db, cargoPermissao_db);
                                }
                            }
                        }
                        //O CARGO POSSUIA ACESSO AO SISTEMA E PASSARÁ A NÃO TER MAIS, REMOVER AS CONFIGURAÇÕES DE ACESSO DO CARGO
                        if (!cargo.acessaSistema && cargo_db.acessaSistema)
                        {
                            //BUSCA AS CONFIGURAÇÕES DE ACESSO QUE O CARGO POSSUIA PARA SEREM REMOVIDAS
                            query = db.Query();
                            query.Constrain(typeof(CargoPermissao));
                            query.Descend("idCargo").Constrain(cargo_db.id);
                            IObjectSet rs_cargoPermissoes = query.Execute();
                            for (int i = 0; i < rs_cargoPermissoes.Count; i++)
                            {
                                //REMOVE DO BANCO AS CONFIGURAÇÕES DO CARGO QUE NÃO TERÁ MAIS ACESSO AO SISTEMA
                                CargoPermissao cargoPermissao_db = rs_cargoPermissoes[i] as CargoPermissao;
                                dbRemove(db, cargoPermissao_db);
                            }
                        }

                        //REPLICA AS INFORMAÇÕES INSERIDAS PELO USUÁRIO NO OBJETO A SER SALVO E SALVA O MESMO
                        Utils.copiaCamposBasicos(cargo, cargo_db);
                        cargo_db.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                        dbStore(db, cargo_db);
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
