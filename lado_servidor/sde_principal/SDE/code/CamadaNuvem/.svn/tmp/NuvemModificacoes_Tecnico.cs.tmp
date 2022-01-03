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
using System.Text.RegularExpressions;
using System.IO;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {

        public void Tecnico_ResetaEstoque(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (ItemEmpEstoque xxx in db.Query<ItemEmpEstoque>())
                    {
                        xxx.qtd = 0;
                        xxx.qtdMax = 0;
                        xxx.qtdMin = 0;
                        xxx.qtdReserva = 0;
                        dbStore(db, xxx);
                    }
                    //
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void Tecnico_ResetaPreco(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (ItemEmpPreco xxx in db.Query<ItemEmpPreco>())
                    {
                        xxx.compra = 0;
                        xxx.custo = 0;
                        xxx.descontoMaximo = 0;
                        xxx.margemLucro = 0;
                        xxx.pctComissao = 0;
                        xxx.venda = 0;
                        xxx.atacado = 0;
                        dbStore(db, xxx);
                    }
                    //
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void Tecnico_DefineDescontoMaximo(int idCorp, double descMax)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (ItemEmpPreco xxx in db.Query<ItemEmpPreco>())
                    {
                        xxx.descontoMaximo = descMax;
                        dbStore(db, xxx);
                    }
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void Tecnico_CertificaIdItemEmpAliquotas(int idCorp, int idEmp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (ItemEmpAliquotas iea in db.Query<ItemEmpAliquotas>())
                    {
                        if (iea.id == 0)
                        {
                            iea.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpAliquotas), 0);
                            dbStore(db, iea);
                        }
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

        public void Tecnico_CertificaItemEmpAliquotas(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    bool contemIea;
                    foreach (Empresa empresa in db.Query<Empresa>())
                    {
                        foreach (Item item in db.Query<Item>())
                        {
                            contemIea = false;
                            IQuery query = db.Query();
                            query.Constrain(typeof(ItemEmpAliquotas));
                            query.Descend("idItem").Constrain(item.id);
                            foreach (ItemEmpAliquotas xxx in query.Execute())
                            {
                                if (xxx.idEmp != empresa.id)
                                    continue;
                                contemIea = true;
                                break;
                            }

                            if (!contemIea)
                            {
                                ItemEmpAliquotas iea = new ItemEmpAliquotas()
                                {
                                    id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpAliquotas), 0),
                                    idEmp = empresa.id,
                                    idItem = item.id
                                };

                                foreach (FieldInfo f in typeof(ItemEmpAliquotas).GetFields())
                                {
                                    if (f.Name.StartsWith("icmsAliq"))
                                        f.SetValue(iea, 17);
                                    else if (f.Name.StartsWith("icmsCST"))
                                        f.SetValue(iea, "000");
                                }
                                iea.pisCST = "08";    //operacao sem insidencia
                                iea.cofinsCST = "08"; //operacao sem insidencia
                                iea.ipiCST = "52";    //saída    sem insidencia
                                iea.ipiCodEnquad = "999";

                                dbStore(db, iea);
                            }
                        }
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

        public void Tecnico_CertificaItemEmpPreco(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    bool contemIep;
                    foreach (Empresa empresa in db.Query<Empresa>())
                    {
                        foreach (Item item in db.Query<Item>())
                        {
                            contemIep = false;
                            IQuery query = db.Query();
                            query.Constrain(typeof(ItemEmpPreco));
                            query.Descend("idItem").Constrain(item.id);
                            foreach (ItemEmpPreco xxx in query.Execute())
                            {
                                if (xxx.idEmp != empresa.id)
                                    continue;
                                contemIep = true;
                                break;
                            }

                            if (!contemIep)
                            {
                                ItemEmpPreco iep = new ItemEmpPreco()
                                {
                                    id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpPreco), 0),
                                    idEmp = empresa.id,
                                    idItem = item.id,
                                };
                                dbStore(db, iep);
                            }
                        }
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

        public void Tecnico_CertificaItemEmpEstoque(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    bool contemIee;
                    foreach (Empresa empresa in db.Query<Empresa>())
                    {
                        foreach (Item item in db.Query<Item>())
                        {
                            contemIee = false;
                            IQuery query = db.Query();
                            query.Constrain(typeof(ItemEmpEstoque));
                            query.Descend("idItem").Constrain(item.id);
                            foreach (ItemEmpEstoque xxx in query.Execute())
                            {
                                if (xxx.idEmp != empresa.id)
                                    continue;
                                contemIee = true;
                                break;
                            }

                            if (!contemIee)
                            {
                                int id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ItemEmpEstoque), 0);
                                ItemEmpEstoque iee = new ItemEmpEstoque()
                                {
                                    id = id,
                                    idEmp = empresa.id,
                                    idItem = item.id,
                                    identificador = "U:U",
                                    codBarras = String.Concat("B", id.ToString().PadLeft(6, '0')),
                                    qtd = 0,
                                };
                                dbStore(db, iee);
                            }
                        }
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

        public void Tecnico_defineQuantidadeEstoqueTodos(int idCorp, int idEmp, double qtd)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;
                    query = db.Query();
                    query.Constrain(typeof(ItemEmpEstoque));
                    query.Descend("idEmp").Constrain(idEmp);

                    foreach (ItemEmpEstoque iee in query.Execute())
                    {
                        query = db.Query();
                        query.Constrain(typeof(Item));
                        query.Descend("id").Constrain(iee.idItem);

                        if (!(query.Execute()[0] as Item).desuso)
                            iee.qtd = qtd;
                        dbStore(db, iee);
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

        public void Tecnico_nomeEtiqueta(int idCorp)
        {
            if (idCorp != 68 && idCorp != 44 && idCorp != 53 && idCorp != 56 && idCorp != 20 && idCorp != 64 && idCorp != 76)
                throw new Exception("Empresa não possui configuração de etiqueta");
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Item item in db.Query<Item>())
                    {
                        if (item.nome.Length > Utils.numeroCaracteresEtiqueta(idCorp))
                            item.nomeEtiqueta = item.nome.Substring(0, Utils.numeroCaracteresEtiqueta(idCorp));
                        else
                            item.nomeEtiqueta = item.nome;
                        dbStore(db, item);
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

        public void Tecnico_ProdutosDesuso(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Item item in db.Query<Item>())
                    {
                        item.desuso = true;
                        dbStore(db, item);
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

        public void Tecnico_InsereComissaoProdutoMov(int idCorp, int idEmp, double comissao)
        {
            IQuery query;

            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Item item in db.Query<Item>())
                    {
                        ItemEmpPreco itemEmpPreco = null;
                        query = db.Query();
                        query.Constrain(typeof(ItemEmpPreco));
                        query.Descend("idItem").Constrain(item.id);
                        query.Descend("idEmp").Constrain(idEmp);
                        IObjectSet setterIep = query.Execute();
                        if (setterIep.Count == 0)
                            continue;
                        itemEmpPreco = setterIep[0] as ItemEmpPreco;
                        itemEmpPreco.pctComissao = comissao;
                        dbStore(db, itemEmpPreco);

                        /*
                        if (item.id == 25)
                            itemEmpPreco.pctComissao = 1;
                        else
                            itemEmpPreco.pctComissao = 1.8;
                         * */
                    }

                    foreach (Mov mov in db.Query<Mov>())
                    {
                        query = db.Query();
                        query.Constrain(typeof(MovItem));
                        query.Descend("idMov").Constrain(mov.id);

                        foreach (MovItem movItem in query.Execute())
                        {
                            if (movItem.idItem > 0)
                            {
                                ItemEmpPreco itemEmpPreco = null;
                                query = db.Query();
                                query.Constrain(typeof(ItemEmpPreco));
                                query.Descend("idItem").Constrain(movItem.idItem);
                                itemEmpPreco = query.Execute()[0] as ItemEmpPreco;

                                movItem.pctComissaoPreco = itemEmpPreco.pctComissao;

                                dbStore(db, movItem);
                            }
                        }
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

        public void Tecnico_ItensTodosMaiusculo(int idCorp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Item item in db.Query<Item>())
                    {
                        item.nome = item.nome.ToUpper();
                        dbStore(db, item);
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

        public void Tecnico_AtualizacaoBalanco(int idCorp, int idEmp)
        {
            defineCorp(idCorp);
            int contador = 0;
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    foreach (Balanco balanco in db.Query<Balanco>())
                    {
                        query = db.Query();
                        query.Constrain(typeof(BalancoItem));
                        query.Descend("idBalanco").Constrain(balanco.id);
                        foreach (BalancoItem balancoItem in query.Execute())
                        {
                            Item item = null;
                            query = db.Query();
                            query.Constrain(typeof(Item));
                            query.Descend("id").Constrain(balancoItem.idItem);
                            item = query.Execute()[0] as Item;

                            ItemEmpPreco itemEmpPreco = null;
                            query = db.Query();
                            query.Constrain(typeof(ItemEmpPreco));
                            query.Descend("idItem").Constrain(balancoItem.idItem);
                            itemEmpPreco = query.Execute()[0] as ItemEmpPreco;

                            balancoItem.rfUnica = item.rfUnica;
                            balancoItem.rfAuxiliar = item.rfAuxiliar;
                            balancoItem.custo = itemEmpPreco.custo;
                            balancoItem.compra = itemEmpPreco.compra;
                            balancoItem.venda = itemEmpPreco.venda;

                            dbStore(db, balancoItem);
                            contador++;
                        }
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

        public void Tecnico_LancaQuantidadeProdutosZerados(int idCorp, int idEmp, double qtd)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query = db.Query();
                    query.Constrain(typeof(ItemEmpEstoque));
                    query.Descend("idEmp").Constrain(idEmp);
                    foreach (ItemEmpEstoque iee in query.Execute())
                    {
                        if (iee.qtd == 0)
                        {
                            iee.qtd = qtd;
                            dbStore(db, iee);
                        }
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

        public void Tecnico_CaixaAtualizacao(int idCorp, int idEmp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Cx_Diario cxD in db.Query<Cx_Diario>())
                    {
                        if (cxD.idEmp == idEmp)
                        {
                            AtualizaValoresCaixa(cxD, idEmp, db);
                            cxD.situacao = ECxDiarioSituacao.aberto;
                            cxD.valorAbertura = cxD.saldo;
                            dbStore(db, cxD);
                        }
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

        public void Tecnico_CertificaIdEmpCxLancamento(int idCorp, int idEmp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Cx_Lancamento cxL in db.Query<Cx_Lancamento>())
                    {
                        cxL.idEmp = idEmp;
                        dbStore(db, cxL);
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

        public void Tecnico_CaixaSaldoParaValorAbertura(int idCorp, int idEmp)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    foreach (Cx_Diario cxD in db.Query<Cx_Diario>())
                    {
                        if (StringToDateTime(cxD.data) < StringToDateTime("19/04/2010") && cxD.idEmp == idEmp)
                        {
                            cxD.valorAbertura = cxD.saldo;
                            dbStore(db, cxD);
                        }
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

        public void Tecnico_VerificaGrade_Grupo_Subgrupo()
        {
            IQuery query;
            int max_id_corp = db0.Query<Max>()[0].idCorporacao;
            for (int idCorp = 1; idCorp <= max_id_corp; idCorp++)
            {
                defineCorp(idCorp);
                //db = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);

                string STR_GERAL = "GERAL"; //"Geral"; 

                bool[] requisitos = new bool[3];

                foreach (Cad_Secao cs in db.Query<Cad_Secao>())
                {
                    if (cs.secao != null && cs.secao.ToUpper() == STR_GERAL)
                        cs.secao = cs.secao.ToUpper();
                    if (cs.grupo != null && cs.grupo.ToUpper() == STR_GERAL)
                        cs.grupo = cs.grupo.ToUpper();
                    if (cs.subgrupo != null && cs.subgrupo.ToUpper() == STR_GERAL)
                        cs.subgrupo = cs.subgrupo.ToUpper();

                    if (!requisitos[0] && cs.secao != null && cs.secao == STR_GERAL)
                        requisitos[0] = true;
                    if (!requisitos[1] && cs.secao != null && cs.secao == STR_GERAL && cs.grupo != null && cs.grupo == STR_GERAL)
                        requisitos[1] = true;
                    if (!requisitos[2] && cs.secao != null && cs.secao == STR_GERAL && cs.grupo != null && cs.grupo == STR_GERAL && cs.subgrupo != null && cs.subgrupo == STR_GERAL)
                        requisitos[2] = true;
                    dbStore(db, cs);
                }

                if (!requisitos[0])
                    dbStore(db, new Cad_Secao() { id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cad_Secao), 0), idClienteFuncionarioLogado = 1, secao = STR_GERAL });

                if (!requisitos[1])
                    dbStore(db, new Cad_Secao() { id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cad_Secao), 0), idClienteFuncionarioLogado = 1, secao = STR_GERAL, grupo = STR_GERAL });

                if (!requisitos[2])
                    dbStore(db, new Cad_Secao() { id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cad_Secao), 0), idClienteFuncionarioLogado = 1, secao = STR_GERAL, grupo = STR_GERAL, subgrupo = STR_GERAL });

                dbCommit(db);
            }
        }

        public void Tecnico_VerificaEspacosVaziosInicioFim()
        {
            IQuery query;
            int max_id_corp = db0.Query<Max>()[0].idCorporacao;
            for (int idCorp = 1; idCorp < max_id_corp; idCorp++)
            {
                defineCorp(idCorp);

                Assembly assembly = Assembly.Load("SDE");
                Type[] tipos = assembly.GetTypes();

                foreach (Type tipo in tipos)
                {
                    if (!(tipo.Namespace == "SDE.Entidade") || tipo.Name.StartsWith("Super"))
                        continue;

                    query = db.Query();
                    query.Constrain(tipo);
                    IObjectSet rs_objeto = query.Execute();

                    foreach (object obj in rs_objeto)
                    {
                        foreach (FieldInfo f in obj.GetType().GetFields())
                        {
                            if (f.GetValue(obj) != null && f.FieldType == typeof(String) && !f.Name.StartsWith("__"))
                            {
                                string valorAntigo = f.GetValue(obj).ToString();
                                string valorNovo = valorAntigo.TrimStart().TrimEnd();
                                f.SetValue(obj, valorNovo);
                            }
                        }
                    }
                }
            }
        }
        
        public void Tecnico_AtualizacaoControleUsuario()
        {
            //string diretorio_bd = @"C:\dev\sistemadaempresa\ambiente_desenvolvimento\marcos\banco_de_dados\Banco\";
            string diretorio_bd = @"C:\banco_dados_db4o\sde\Banco\";

            for (int i = 1; i < 100; i++)
            {
                //se banco não existe na base de dados passa para próxima corporação
                if (!File.Exists(diretorio_bd + i.ToString() + ".dbf"))
                    continue;

                defineCorp(i);

                lock (db.Ext().Lock())
                {
                    IQuery query;

                    try
                    {
                        //remove todos usuário e permissoes da empresa. necessário no caso de rodas este método mais de uma vez
                        foreach (ClienteFuncionarioUsuario cfu in db.Query<ClienteFuncionarioUsuario>())
                            dbRemove(db, cfu);
                        foreach (ClienteFuncionarioPermissao cfp in db.Query<ClienteFuncionarioPermissao>())
                            dbRemove(db, cfp);

                        //resetando id's
                        query = db.Query();
                        query.Constrain(typeof(Incremento));
                        query.Descend("entidade").Constrain("SDE.Entidade.ClienteFuncionarioUsuario");
                        IObjectSet rs_incremento1 = query.Execute();
                        if (rs_incremento1.Count > 0)
                        {
                            Incremento inceremento1 = rs_incremento1[0] as Incremento;
                            inceremento1.valorUltimaID = 0;
                            dbStore(db, inceremento1);
                        }

                        query = db.Query();
                        query.Constrain(typeof(Incremento));
                        query.Descend("entidade").Constrain("SDE.Entidade.ClienteFuncionarioPermissao");
                        IObjectSet rs_incremento2 = query.Execute();
                        if (rs_incremento2.Count > 0)
                        {
                            Incremento incremento2 = rs_incremento2[0] as Incremento;
                            incremento2.valorUltimaID = 0;
                            dbStore(db, incremento2);
                        }

                        bool existe = false;
                        Cliente vinicius = null;
                        foreach (Cliente cliente in db.Query<Cliente>())
                        {
                            //dizendo que nenhum funcionário é usuário do sistema
                            cliente.usuarioSistema = false;
                            cliente.comissionado = false;

                            //se cliente for comissionado, recebendo uma ou mais opções de comissionamento, é criado um novo objeto
                            //que contenha as configurações dessa comissão. a configuração de comissionamento não mais será no
                            //cadastro de cliente e sim em um objeto separado que contenha a id do cliente
                            if (cliente.calculaMaoDeObra || cliente.calculaMaoDeObraGarantia || cliente.calculaMaoDeObraGeral ||
                                cliente.calculaMaoDeObraGeralGarantia || cliente.calculaMontanteTotal || cliente.calculaProdutos ||
                                cliente.calculaProdutosEmGarantia)
                            {
                                ClienteFuncionarioComissionamento clienteFuncionarioComissionamento = new ClienteFuncionarioComissionamento();
                                clienteFuncionarioComissionamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioComissionamento), 0);
                                clienteFuncionarioComissionamento.idEmp = 1;
                                clienteFuncionarioComissionamento.idCliente = cliente.id;

                                clienteFuncionarioComissionamento.calculaMaoDeObra = cliente.calculaMaoDeObra;
                                clienteFuncionarioComissionamento.calculaMaoDeObraGarantia = cliente.calculaMaoDeObraGarantia;
                                clienteFuncionarioComissionamento.calculaMaoDeObraGeral = cliente.calculaMaoDeObraGeral;
                                clienteFuncionarioComissionamento.calculaMaoDeObraGeralGarantia = cliente.calculaMaoDeObraGeralGarantia;
                                clienteFuncionarioComissionamento.calculaProdutos = cliente.calculaProdutos;
                                clienteFuncionarioComissionamento.calculaProdutosEmGarantia = cliente.calculaProdutosEmGarantia;
                                clienteFuncionarioComissionamento.calculaMontanteTotal = cliente.calculaMontanteTotal;

                                clienteFuncionarioComissionamento.comissaoMaoDeObra = cliente.comissaoMaoDeObra;
                                clienteFuncionarioComissionamento.comissaoMaoDeObraGarantia = cliente.comissaoMaoDeObraGarantia;
                                clienteFuncionarioComissionamento.comissaoMaoDeObraGeral = cliente.comissaoMaoDeObraGeral;
                                clienteFuncionarioComissionamento.comissaoMaoDeObraGeralGarantia = cliente.comissaoMaoDeObraGeralGarantia;
                                clienteFuncionarioComissionamento.comissaoProdutos = cliente.comissaoProdutos;
                                clienteFuncionarioComissionamento.comissaoProdutosEmGarantia = cliente.comissaoProdutosEmGarantia;
                                clienteFuncionarioComissionamento.comissaoMontanteTotal = cliente.comissaoMontanteTotal;

                                //informa no cadastro de cliente que o mesmo é comissionado
                                cliente.comissionado = true;

                                dbStore(db, clienteFuncionarioComissionamento);
                            }

                            //se funcionário possuir login é criado um objeto de usuário para o mesmo e tambem serão
                            //criados objetos que receberão as permissoes de sistema do usuário
                            if (cliente.loginUsuario != null && cliente.loginSenha != null)
                                if (cliente.loginUsuario.Trim() != String.Empty && cliente.loginUsuario.Trim() != String.Empty)
                                {
                                    ClienteFuncionarioUsuario clienteFuncionarioUsuario = new ClienteFuncionarioUsuario();
                                    clienteFuncionarioUsuario.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0);
                                    clienteFuncionarioUsuario.idCliente = cliente.id;
                                    clienteFuncionarioUsuario.idEmp = 1;
                                    clienteFuncionarioUsuario.login = cliente.loginUsuario;
                                    clienteFuncionarioUsuario.senha = cliente.loginSenha;
                                    clienteFuncionarioUsuario.usuarioTecnico = false;

                                    foreach (SdeConfig sdeConfig in db.Query<SdeConfig>())
                                    {
                                        if (!sdeConfig.variavel.StartsWith("Menu"))
                                            continue;
                                        if (sdeConfig.valor == "0")
                                            continue;

                                        ClienteFuncionarioPermissao clienteFuncionarioPermissao = new ClienteFuncionarioPermissao();
                                        clienteFuncionarioPermissao.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioPermissao), 0);
                                        clienteFuncionarioPermissao.idClienteFuncionarioUsuario = clienteFuncionarioUsuario.id;
                                        clienteFuncionarioPermissao.idEmp = 1;
                                        clienteFuncionarioPermissao.variavel = sdeConfig.variavel;
                                        clienteFuncionarioPermissao.valor = sdeConfig.valor;

                                        dbStore(db, clienteFuncionarioPermissao);
                                    }

                                    //informa no cadastro de cliente que o mesmo é usuáro do sistema
                                    cliente.usuarioSistema = true;
                                    cliente.ehFuncionario = true;

                                    dbStore(db, clienteFuncionarioUsuario);
                                }

                            //percorreu o cliente, vou salvar o objeto mesmo não havendo alterações
                            dbStore(db, cliente);

                            if (cliente.cpf_cnpj == "71082069191")
                            {
                                existe = true;
                                vinicius = cliente;
                            }
                        }

                        if (!existe)
                        {
                            vinicius = new Cliente()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0),
                                ehFuncionario = true,
                                ehFornecedor = true,
                                cpf_cnpj = "71082069191",
                                rg = "4210001",
                                rgUF = "GO",
                                dtNasc = "30/11/1981",
                                nome = "VINICIUS MACHADO CRUVINEL",
                                apelido_razsoc = "VINICIUS MULTISOFT"
                            };

                            ClienteContato ccComercial = new ClienteContato()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                                idCliente = vinicius.id,
                                tipo = EContatoTipo.fone_fixo,
                                campo = "COMERCIAL",
                                valor = "64-3621-4579"
                            };

                            ClienteContato ccCelular1 = new ClienteContato() 
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                                idCliente = vinicius.id,
                                tipo = EContatoTipo.celular,
                                campo = "CELULAR1",
                                valor = "64-8406-2571"
                            };

                            ClienteContato ccCelular2 = new ClienteContato()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                                idCliente = vinicius.id,
                                tipo = EContatoTipo.celular,
                                campo = "CELULAR2",
                                valor = "64-8128-8526"
                            };

                            ClienteContato ccEmail = new ClienteContato()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                                idCliente = vinicius.id,
                                tipo = EContatoTipo.email,
                                campo = "EMAIL",
                                valor = "MULTISOFT@YMAIL.COM"
                            };

                            ClienteEndereco endereco = new ClienteEndereco()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteEndereco), 0),
                                idCliente = vinicius.id,
                                tipo = EEnderecoTipo.residencial_comercial,
                                campo = "COMERCIAL",
                                logradouro = "RUA ITAGIBA G. JAYME",
                                bairro = "CENTRO",
                                cep = "75901180",
                                cidade = "RIO VERDE",
                                cidadeIBGE = "5218805",
                                uf = "GO",
                                ufIBGE = "52",
                                numero = 1896
                            };

                            ClienteFuncionarioUsuario cfu = new ClienteFuncionarioUsuario() 
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0),
                                idCliente = vinicius.id,
                                idEmp = 1,
                                login = "VINICIUS",
                                senha = "1710",
                                usuarioTecnico = true
                            };

                            dbStore(db, vinicius, ccComercial, ccCelular1, ccCelular2, ccEmail, endereco, cfu);
                        }
                        else
                        {
                            ClienteFuncionarioUsuario cfu = new ClienteFuncionarioUsuario()
                            {
                                id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0),
                                idCliente = vinicius.id,
                                idEmp = 1,
                                login = "VINICIUS",
                                senha = "1710",
                                usuarioTecnico = true
                            };
                        }

                        foreach (Cargo cargo in db.Query<Cargo>())
                        {
                            //se cargo for comissionado, recebendo uma ou mais opções de comissionamento, é criado um novo objeto
                            //que contenha as configurações dessa comissão. a configuração de comissionamento não mais será no
                            //cadastro do cargo e sim em um objeto separado que contenha a id do cargo
                            if (cargo.calculaMaoDeObra || cargo.calculaMaoDeObraGarantia || cargo.calculaMaoDeObraGeral ||
                                cargo.calculaMaoDeObraGeralGarantia || cargo.calculaMontanteTotal ||
                                cargo.calculaProdutos || cargo.calculaProdutosEmGarantia)
                            {
                                CargoComissionamento cargoComissionamento = new CargoComissionamento();
                                cargoComissionamento.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(CargoComissionamento), 0);
                                cargoComissionamento.idCargo = cargo.id;
                                cargoComissionamento.idEmp = 1;

                                cargoComissionamento.calculaMaoDeObra = cargo.calculaMaoDeObra;
                                cargoComissionamento.calculaMaoDeObraGarantia = cargo.calculaMaoDeObraGarantia;
                                cargoComissionamento.calculaMaoDeObraGeral = cargo.calculaMaoDeObraGeral;
                                cargoComissionamento.calculaMaoDeObraGeralGarantia = cargo.calculaMaoDeObraGeralGarantia;
                                cargoComissionamento.calculaMontanteTotal = cargo.calculaMontanteTotal;
                                cargoComissionamento.calculaProdutos = cargo.calculaProdutos;
                                cargoComissionamento.calculaProdutosEmGarantia = cargo.calculaProdutosEmGarantia;

                                cargoComissionamento.comissaoMaoDeObra = cargo.comissaoMaoDeObra;
                                cargoComissionamento.comissaoMaoDeObraGarantia = cargo.comissaoMaoDeObraGarantia;
                                cargoComissionamento.comissaoMaoDeObraGeral = cargo.comissaoMaoDeObraGeral;
                                cargoComissionamento.comissaoMaoDeObraGeralGarantia = cargo.comissaoMaoDeObraGeralGarantia;
                                cargoComissionamento.comissaoMontanteTotal = cargo.comissaoMontanteTotal;
                                cargoComissionamento.comissaoProdutos = cargo.comissaoProdutos;
                                cargoComissionamento.comissaoProdutosEmGarantia = cargo.comissaoProdutosEmGarantia;

                                //informa no cadastro de cargo que o mesmo possui configurações de comissionamento
                                cargo.comissionado = true;

                                dbStore(db, cargoComissionamento);
                            }

                            dbStore(db, cargo);
                        }
                    }
                    catch (Exception ex)
                    {
                        dbRollback(ex, db);
                        throw new Exception(ex.StackTrace);
                    }
                }
            }
        }

        public void Tecnico_FuncaoTemporaria(int idCorp, int idEmp, int idClienteFuncionarioLogado)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    if (idCorp != 76)
                        throw new Exception("Função temporária não suportada por esta empresa");
                    IQuery query;

                    #region Verifica valores Finan_Lancamento Antonieta Casa

                    query = db.Query();
                    query.Constrain(typeof(Finan_Lancamento));
                    IObjectSet rs_finanLancamento = query.Execute();

                    foreach (Finan_Lancamento finanLancamento in rs_finanLancamento)
                    {
                        if (finanLancamento.historico == null || finanLancamento.historico == string.Empty)
                            continue;

                        query = db.Query();
                        query.Constrain(typeof(Finan_Titulo));
                        query.Descend("idOperacao").Constrain(finanLancamento.idOperacao);
                        IObjectSet rs_finanTitulo = query.Execute();
                        if (rs_finanTitulo.Count == 0)
                            continue;
                        Finan_Titulo finanTitulo_db = (Finan_Titulo)rs_finanTitulo[0];

                        query = db.Query();
                        query.Constrain(typeof(Finan_TituloItem));
                        query.Descend("idTitulo").Constrain(finanTitulo_db.id);
                        IObjectSet rs_finanTituloItem = query.Execute();
                        if (rs_finanTituloItem.Count == 1)
                            continue;

                        int index_parcela = Convert.ToInt32(finanLancamento.historico.Substring(28, 1)) - 1;
                        double valor_cobrado = ((Finan_TituloItem)rs_finanTituloItem[index_parcela]).valorCobrado;
                        finanLancamento.valorBruto = valor_cobrado;
                        finanLancamento.valorLancado = valor_cobrado;

                        dbStore(db, finanLancamento);
                    }
                    dbCommit(db);

                    #endregion

                    #region Preenche IPI CNPJ
                    /*
                    query = db.Query();
                    query.Constrain(typeof(ItemEmpAliquotas));
                    IObjectSet rs_itemEmpAliquotas = query.Execute();

                    foreach (ItemEmpAliquotas iea in rs_itemEmpAliquotas)
                    {
                        iea.ipiCNPJ = (iea.ipiCNPJ == null) ? "" : iea.ipiCNPJ;
                        dbStore(db, iea);
                    }
                    dbCommit(db);
                    */
                    #endregion

                    #region Correção saldo de rota BILHARBOL

                    /*
                    query = db.Query();
                    query.Constrain(typeof(Finan_Lancamento));
                    query.Descend("id").OrderAscending();
                    IObjectSet rs_finanLancamento = query.Execute();

                    Dictionary<int, Finan_Conta> dicionarioContaPorId = new Dictionary<int, Finan_Conta>();
                    List<Finan_Conta> listaFinanConta = new List<Finan_Conta>();

                    foreach (Finan_Lancamento finanLancamento in rs_finanLancamento)
                    {
                        query = db.Query();
                        query.Constrain(typeof(Finan_Conta));
                        query.Descend("id").Constrain(finanLancamento.idContaDestino);
                        IObjectSet rs_finanConta = query.Execute();
                        Finan_Conta finanConta_db = rs_finanConta[0] as Finan_Conta;

                        if (!listaFinanConta.Contains(finanConta_db))
                        {
                            finanConta_db.saldoInicial = 0;
                            finanConta_db.saldoAtual = 0;
                            finanConta_db.saldoAnterior = 0;
                            listaFinanConta.Add(finanConta_db);
                        }

                        finanLancamento.saldoAnterior = finanConta_db.saldoAtual;
                        finanConta_db.saldoAnterior = finanConta_db.saldoAtual;
                        finanConta_db.saldoAtual += finanLancamento.valorLancado;
                        finanLancamento.saldoAtual = finanConta_db.saldoAtual;

                        dbStore(db, finanConta_db, finanLancamento);
                    }

                    dbCommit(db);
                     * */

                    #endregion

                    #region CORREÇÃO ENTRADAS DE ESTOQUE EMB2

                    /*
                    int numeroNF = 1019;

                    IList<Mov> rs_mov = db.Query<Mov>(
                        delegate(Mov mov)
                        {
                            return (Utils.StringToDateTime(mov.dthrMovEmissao) >= Utils.StringToDateTime("16/08/2010") && Utils.StringToDateTime(mov.dthrMovEmissao) <= Utils.StringToDateTime("18/08/2010") &&
                                mov.tipo == EMovTipo.entrada_cancel && mov.idMovCancelada == 0 && mov.numeroNF == numeroNF);
                        }
                        );

                    foreach (Mov mov in rs_mov)
                    {
                        int idOperacao = AppFacade.get.reaproveitamento.getIncrementoOperacao(db, idEmp, idClienteFuncionarioLogado);
                        int idTransacao = AppFacade.get.reaproveitamento.getIncrementoTransacao(db, idEmp, idClienteFuncionarioLogado);

                        Mov mov_novo = new Mov();
                        Utils.copiaCamposBasicos(mov, mov_novo);
                        mov_novo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                        mov_novo.idMovCanceladora = 0;
                        mov_novo.idOperacao = idOperacao;
                        mov_novo.idTransacao = idTransacao;
                        mov_novo.numeroNF = ++numeroNF;
                        mov_novo.tipo = EMovTipo.entrada_compra;

                        query = db.Query();
                        query.Constrain(typeof(MovItem));
                        query.Descend("idMov").Constrain(mov.id);
                        IObjectSet rs_movItem = query.Execute();

                        foreach (MovItem movItem in rs_movItem)
                        {
                            MovItem movItem_novo = new MovItem();
                            Utils.copiaCamposBasicos(movItem, movItem_novo);
                            movItem_novo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                            movItem_novo.idMov = mov_novo.id;
                            movItem_novo.idOperacao = idOperacao;
                            movItem_novo.idTransacao = idTransacao;

                            query = db.Query();
                            query.Constrain(typeof(ItemEmpEstoque));
                            query.Descend("id").Constrain(movItem.idIEE);
                            IObjectSet rs_itemEmpEstoque = query.Execute();
                            ItemEmpEstoque itemEmpEstoque_db = rs_itemEmpEstoque[0] as ItemEmpEstoque;

                            if (itemEmpEstoque_db.qtd < 0)
                                itemEmpEstoque_db.qtd = 0;

                            itemEmpEstoque_db.qtd += movItem.qtd;

                            dbStore(db, movItem_novo, itemEmpEstoque_db);
                        }

                        dbStore(db, mov_novo);
                    }

                    dbCommit(db);
                    /*

                    #endregion

                    #region Retirar Contas Pagar com valor 0 (Sete)
                    /*
                    query = db.Query();
                    query.Constrain(typeof(Finan_Titulo));
                    query.Descend("idEmp").Constrain(idEmp);
                    query.Descend("tipo").Constrain(ETipoTitulo.titulo_a_pagar);
                    IObjectSet rs_ListaTitulo = query.Execute();

                    foreach (Finan_Titulo finanTitulo in rs_ListaTitulo)
                    {
                        query = db.Query();
                        query.Constrain(typeof(Finan_TituloItem));
                        query.Descend("idTitulo").Constrain(finanTitulo.id);
                        IObjectSet rs_listaFinanTituloItem = query.Execute();

                        Boolean removeTitulo = false;
                        foreach (Finan_TituloItem finanTituloItem in rs_listaFinanTituloItem)
                        {
                            if (finanTituloItem.valorCobrado != 0)
                                continue;
                            removeTitulo = true;

                            dbRemove(db, finanTituloItem);
                        }

                        if (removeTitulo)
                            dbRemove(db, finanTitulo);
                    }

                    dbCommit(db);
                    */
                    #endregion

                    #region Copiar preços da emp 2 para 1 quando o preço de venda na emp 1 for 0 (Espaço Mamão Bebê)
                    /*
                    query = db.Query();
                    query.Constrain(typeof(Item));
                    IObjectSet rs_listaItem = query.Execute();

                    foreach (Item item in rs_listaItem)
                    {
                        query = db.Query();
                        query.Constrain(typeof(ItemEmpPreco));
                        query.Descend("idItem").Constrain(item.id);
                        query.Descend("idEmp").Constrain(1);
                        IObjectSet rs_itemEmpPreco1 = query.Execute();
                        ItemEmpPreco itemEmpPreco1 = rs_itemEmpPreco1[0] as ItemEmpPreco;

                        if (itemEmpPreco1.venda == 0)
                        {
                            query = db.Query();
                            query.Constrain(typeof(ItemEmpPreco));
                            query.Descend("idItem").Constrain(item.id);
                            query.Descend("idEmp").Constrain(2);
                            IObjectSet rs_itemEmpPreco2 = query.Execute();
                            
                            if (rs_itemEmpPreco2.Count == 0)
                                continue;

                            ItemEmpPreco itemEmpPreco2 = rs_itemEmpPreco2[0] as ItemEmpPreco;
                            itemEmpPreco1.venda = itemEmpPreco2.venda;
                            dbStore(db, itemEmpPreco1);
                        }
                    }

                    dbCommit(db);
                    */
                    #endregion

                    #region Ajuste de preço de venda de entrada de entoque de acordo com cadastro (Antonieta Casa)

                    /*
                    query = db.Query();
                    query.Constrain(typeof(Mov));
                    IObjectSet rs_listaMov = query.Execute();

                    foreach (Mov mov in rs_listaMov)
                    {
                        if (mov.tipo == EMovTipo.entrada_compra)
                        {
                            query = db.Query();
                            query.Constrain(typeof(MovItem));
                            query.Descend("idMov").Constrain(mov.id);
                            IObjectSet rs_listaMovItem = query.Execute();

                            foreach (MovItem movItem in rs_listaMovItem)
                            {
                                if (movItem.vlrUnitVendaInicial == Convert.ToDouble(Decimal.Zero) && movItem.vlrUnitVendaFinal == Convert.ToDouble(Decimal.Zero))
                                {
                                    query = db.Query();
                                    query.Constrain(typeof(ItemEmpPreco));
                                    query.Descend("idEmp").Constrain(idEmp);
                                    query.Descend("idItem").Constrain(movItem.idItem);
                                    IObjectSet rs_itemEmpPreco = query.Execute();
                                    ItemEmpPreco itemEmpPreco = rs_itemEmpPreco[0] as ItemEmpPreco;

                                    movItem.vlrUnitVendaInicial = itemEmpPreco.venda;
                                    movItem.vlrUnitVendaFinal = itemEmpPreco.venda;
                                    dbStore(db, movItem);
                                }
                            }
                        }
                    }
                    dbCommit(db);
                    */
                    #endregion

                    #region Copiar configurações de login da empresa 1 para empresa 2
                    /*
                    query = db.Query();
                    query.Constrain(typeof(Empresa));
                    query.Descend("id").Constrain(2);
                    IObjectSet rs_empresa2 = query.Execute();
                    if (rs_empresa2.Count == 0)
                        throw new Exception("Corporação não possue empresa 2");

                    foreach (ClienteFuncionarioUsuario cfu in db.Query<ClienteFuncionarioUsuario>())
                    {
                        ClienteFuncionarioUsuario clienteFuncionarioUsuario = new ClienteFuncionarioUsuario();
                        Utils.copiaCamposBasicos(cfu, clienteFuncionarioUsuario);
                        clienteFuncionarioUsuario.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0);
                        clienteFuncionarioUsuario.idEmp = 2;
                    }
                    */
                    #endregion

                    #region Correção quantidade estoque de acordo com entrada (Luz Forte)

                    /*
                    //Todo o estoque é zerado para posteriormente serem lançadas as quantidades nas entradas
                    foreach (ItemEmpEstoque iee in db.Query<ItemEmpEstoque>())
                    {
                        iee.qtd = 0;
                        dbStore(db, iee);
                    }

                    foreach (Mov mov in db.Query<Mov>())
                    {
                        if (mov.tipo == EMovTipo.entrada_compra)
                        {
                            query = db.Query();
                            query.Constrain(typeof(MovItem));
                            query.Descend("idMov").Constrain(mov.id);
                            IObjectSet rs_movItem = query.Execute();

                            foreach (MovItem movItem in rs_movItem)
                            {
                                query = db.Query();
                                query.Constrain(typeof(ItemEmpEstoque));
                                query.Descend("idItem").Constrain(movItem.idItem);
                                query.Descend("idEmp").Constrain(idEmp);
                                query.Descend("identificador").Constrain(movItem.estoque_identificador);
                                IObjectSet rs_iee = query.Execute();
                                ItemEmpEstoque iee = rs_iee[0] as ItemEmpEstoque;

                                iee.qtd += movItem.qtd;
                                dbStore(db, iee);
                            }
                        }
                    }
                    */

                    #endregion

                    #region Copia rfAux para rfUnica

                    /*
                    foreach (Item item in db.Query<Item>())
                    {
                        item.rfUnica = item.rfAuxiliar;
                        dbStore(db, item);
                    }

                    dbCommit(db);
                     * */

                    #endregion

                    #region Comissão Pintando o 7

                    /*

                    double pctComissao = 3;
                    DateTime dataInicial = new DateTime(2010, 5, 1);

                    foreach (Mov mov in db.Query<Mov>())
                    {
                        if (mov.dthrMovEmissao != null)
                        {
                            if (StringToDateTime(mov.dthrMovEmissao) >= dataInicial)
                            {
                                query = db.Query();
                                query.Constrain(typeof(MovItem));
                                query.Descend("idMov").Constrain(mov.id);
                                IObjectSet rs_movItem = query.Execute();

                                foreach (MovItem mi in rs_movItem)
                                {
                                    if (mov.tipo == EMovTipo.saida_venda || mov.tipo == EMovTipo.outros_pedido)
                                    {
                                        mi.pctComissaoPreco = pctComissao;
                                        mi.vlrComissaoPreco = (mi.vlrUnitVendaFinalQtd * pctComissao) / 100;
                                    }
                                    else
                                    {
                                        mi.pctComissaoPreco = 0;
                                        mi.vlrComissaoPreco = 0;
                                    }
                                    dbStore(db, mi);
                                }
                            }
                        }
                    }
                    dbCommit(db);

                     * */
                    #endregion

                    #region ajuste de preço

                    /*
                    foreach (ItemEmpPreco iep in db.Query<ItemEmpPreco>())
                    {
                        if (iep.custo == 0)
                        {
                            iep.custo = iep.compra;
                            dbStore(db, iep);
                        }
                    }
                     * */

                    #endregion

                    #region Cancela Balanço
                    
                    /*
                    IQuery query;

                    Balanco balanco = null;
                    query = db.Query();
                    query.Constrain(typeof(Balanco));
                    query.Descend("id").Constrain(2);
                    balanco = query.Execute()[0] as Balanco;

                    Mov mov = null;
                    query = db.Query();
                    query.Constrain(typeof(Mov));
                    query.Descend("idOperacao").Constrain(balanco.idOperacao);
                    mov = query.Execute()[0] as Mov;

                    Mov movCanceladora = new Mov();
                    Utils.copiaCamposBasicos(mov, movCanceladora);
                    movCanceladora.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    movCanceladora.dthrMovEmissao = Utils.getAgoraString();
                    movCanceladora.dtLancaCaixa = Utils.getAgoraString();
                    movCanceladora.dtMovTicks = Utils.getAgoraTicks();
                    movCanceladora.dtLancTicks = Utils.getAgoraTicks();

                    movCanceladora.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
                    movCanceladora.idMovCancelada = mov.id;
                    mov.idMovCanceladora = movCanceladora.id;

                    mov.tipo = EMovTipo.entrada_cancel;
                    dbStore(db, mov);
                    movCanceladora.resumo = EMovResumo.entrada;
                    movCanceladora.tipo = EMovTipo.entrada_cancel;

                    balanco.situacao = EBalancoSituacao.em_andamento;

                    dbStore(db, balanco);
                    dbStore(db, mov, movCanceladora);

                    foreach (ItemEmpEstoque iee in db.Query<ItemEmpEstoque>())
                    {
                        iee.qtd = 0;
                        dbStore(db, iee);
                    }

                    dbCommit(db);
                     * */
                    
                    #endregion

                    #region Outra Função

                    /*
                    IQuery query;

                    Dictionary<int, List<Cx_Lancamento>> dictCxL = new Dictionary<int, List<Cx_Lancamento>>();

                    query = db.Query();
                    query.Constrain(typeof(Cx_Lancamento));
                    query.Descend("dtPagamento").OrderAscending();

                    foreach (Cx_Lancamento cxL in query.Execute())
                    {
                        query = db.Query();
                        query.Constrain(typeof(Mov));
                        query.Descend("idOperacao").Constrain(cxL.idOperacao);
                        Mov mov = query.Execute()[0] as Mov;

                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(mov.idCliente);
                        Cliente cliente = query.Execute()[0] as Cliente;

                        if (cxL.tipoPagamento_geraContasReceber && cliente.id != 1)
                        {
                            if (dictCxL.ContainsKey(cxL.idTransacao))
                                dictCxL[cxL.idTransacao].Add(cxL);
                            else
                            {
                                dictCxL.Add(cxL.idTransacao, new List<Cx_Lancamento>());
                                dictCxL[cxL.idTransacao].Add(cxL);
                            }
                        }
                    }

                    foreach (int idTransacao in dictCxL.Keys)
                    {
                        List<Cx_Lancamento> CxLValor = dictCxL[idTransacao];
                        Cx_Lancamento cxLPadrao = CxLValor[0];

                        Finan_Titulo finanTitulo = new Finan_Titulo();
                        finanTitulo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_Titulo), 0);

                        double valorTotalTitulo = 0;
                        int parcela = 1;
                        foreach (Cx_Lancamento cxL in CxLValor)
                        {
                            foreach (Cx_Lancamento item in db.Query<Cx_Lancamento>())
                            {
                                if (item.id == cxL.id)
                                {
                                    Finan_TituloItem finanTituloItem = new Finan_TituloItem();
                                    finanTituloItem.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TituloItem), 0);
                                    finanTituloItem.idTitulo = finanTitulo.id;
                                    finanTituloItem.idEmp = idEmp;
                                    finanTituloItem.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                                    finanTituloItem.idGrupoTipoPagamento = item.idGrupoTipoPagamento;
                                    finanTituloItem.idTipoPagamento = item.idTipoPagamento;
                                    finanTituloItem.dtLancamento = item.dthr;
                                    finanTituloItem.dtPagamento = item.dtPagamento;
                                    finanTituloItem.valorCobrado = item.valorCobrado;
                                    finanTituloItem.parcela = parcela;
                                    finanTituloItem.situacao = ETituloSituacao.em_aberto;
                                    finanTituloItem.identificador = defineNumeroTituloItem(finanTituloItem.id.ToString(), parcela.ToString());

                                    item.tipo = ECxLancamentoTipo.venda;

                                    dbStore(db, finanTituloItem, item);

                                    valorTotalTitulo += item.valorCobrado;
                                    parcela++;
                                }
                            }
                        }

                        Mov mov = null;
                        query = db.Query();
                        query.Constrain(typeof(Mov));
                        query.Descend("idTransacao").Constrain(finanTitulo.idTransacao);
                        if (query.Execute().Count == 0)
                            throw new Exception("Mov não encontrado. ID TRANSAÇÃO: " + finanTitulo.idTransacao);
                        else
                            mov = query.Execute()[0] as Mov;

                        int idCliente = 0;
                        query = db.Query();
                        query.Constrain(typeof(Cliente));
                        query.Descend("id").Constrain(mov.idCliente);
                        if (query.Execute().Count == 0)
                            throw new Exception("Cliente não encontrado. ID CLIENTE: " + mov.idCliente);
                        else
                            idCliente = (query.Execute()[0] as Cliente).id;

                        finanTitulo.idOperacao = cxLPadrao.idOperacao;
                        finanTitulo.idTransacao = cxLPadrao.idTransacao;
                        finanTitulo.idClienteAPagar = idCliente;
                        finanTitulo.dtLancamento = cxLPadrao.dthr;
                        finanTitulo.valorCobrado = valorTotalTitulo;
                        finanTitulo.valorOriginal = valorTotalTitulo;
                        finanTitulo.grupoTipoPagamento_nome = cxLPadrao.grupoTipoPagamento_nome;
                        finanTitulo.tipoDocumento_nome = cxLPadrao.tipoPagamento_nome;
                        finanTitulo.tipoPagamento_geraContasPagar = cxLPadrao.tipoPagamento_geraContasPagar;
                        finanTitulo.tipoPagamento_geraContasReceber = cxLPadrao.tipoPagamento_geraContasReceber;
                        finanTitulo.tipo = ETipoTitulo.titulo_a_receber;
                        finanTitulo.situacao = ETituloSituacao.em_aberto;
                        finanTitulo.identificador = defineNumeroTitulo(finanTitulo.id.ToString());
                        dbStore(db, finanTitulo);
                    }
                    dbCommit(db);
                    */

                    #endregion

                    #region Add User

                    /*
                    Cliente vinicius = new Cliente()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0),
                        ehFuncionario = true,
                        ehFornecedor = true,
                        cpf_cnpj = "71082069191",
                        rg = "4210001",
                        rgUF = "GO",
                        dtNasc = "30/11/1981",
                        nome = "VINICIUS MACHADO CRUVINEL",
                        apelido_razsoc = "VINICIUS MULTISOFT"
                    };

                    ClienteContato ccComercial = new ClienteContato()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                        idCliente = vinicius.id,
                        tipo = EContatoTipo.fone_fixo,
                        campo = "COMERCIAL",
                        valor = "64-3621-4579"
                    };

                    ClienteContato ccCelular1 = new ClienteContato()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                        idCliente = vinicius.id,
                        tipo = EContatoTipo.celular,
                        campo = "CELULAR1",
                        valor = "64-8406-2571"
                    };

                    ClienteContato ccCelular2 = new ClienteContato()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                        idCliente = vinicius.id,
                        tipo = EContatoTipo.celular,
                        campo = "CELULAR2",
                        valor = "64-8128-8526"
                    };

                    ClienteContato ccEmail = new ClienteContato()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0),
                        idCliente = vinicius.id,
                        tipo = EContatoTipo.email,
                        campo = "EMAIL",
                        valor = "MULTISOFT@YMAIL.COM"
                    };

                    ClienteEndereco endereco = new ClienteEndereco()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteEndereco), 0),
                        idCliente = vinicius.id,
                        tipo = EEnderecoTipo.residencial_comercial,
                        campo = "COMERCIAL",
                        logradouro = "RUA JOAQUIM FONSECA",
                        bairro = "MORADA DO SOL",
                        cep = "75908730",
                        cidade = "RIO VERDE",
                        cidadeIBGE = "5218805",
                        uf = "GO",
                        ufIBGE = "52",
                        numero = 521
                    };

                    ClienteFuncionarioUsuario cfu = new ClienteFuncionarioUsuario()
                    {
                        id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFuncionarioUsuario), 0),
                        idCliente = vinicius.id,
                        idEmp = 1,
                        login = "VINICIUS",
                        senha = "1710",
                        usuarioTecnico = true
                    };

                    dbStore(db, vinicius, ccComercial, ccCelular1, ccCelular2, ccEmail, endereco, cfu);
                    dbCommit();
                    */
                    #endregion
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    throw;
                }
            }
        }

        private string defineNumeroTituloItem(string numero, string parcela)
        {
            while (numero.Length < 7)
                numero = "0" + numero;

            return "VD" + numero + "-" + parcela;
        }

        private string defineNumeroTitulo(string numero)
        {
            while (numero.Length < 7)
                numero = "0" + numero;

            return "VD" + numero;
        }

        private DateTime StringToDateTime(string dataString)
        {
            if (dataString.Length == 10)
                return DateTime.Parse(dataString);
            else
                return DateTime.Parse(dataString.Substring(0, 10));
        }

        public void Tecnico_ResetaTiposLancamento(int idCorp)
        {
            //Variaveis_SdeConfig.fin
        }

        private void AtualizaValoresCaixa(Cx_Diario cxD, int idEmp, IObjectContainer db)
        {
            string dataAtual = cxD.data;
            IQuery query;
            Dictionary<string, List<Cx_Lancamento>> dictLancamentos = new Dictionary<string, List<Cx_Lancamento>>();
            List<Cx_Lancamento> listaCxLRecebimentos = new List<Cx_Lancamento>();
            List<Cx_Lancamento> listaCxLRetiradas = new List<Cx_Lancamento>();
            List<Cx_Lancamento> listaCxLEntradas = new List<Cx_Lancamento>();

            foreach (Cx_Lancamento cxL in db.Query<Cx_Lancamento>())
            {
                if (cxL.dthr != null && cxL.dthr.StartsWith(dataAtual))
                {
                    string nomeGrupo = (cxL.grupoTipoPagamento_nome == null) ? "OUTROS" : cxL.grupoTipoPagamento_nome;
                    if (!dictLancamentos.ContainsKey(nomeGrupo))
                        dictLancamentos.Add(nomeGrupo, new List<Cx_Lancamento>());
                    dictLancamentos[nomeGrupo].Add(cxL);
                }
            }

            double totalEntradas = 0;

            foreach (List<Cx_Lancamento> listaCxL in dictLancamentos.Values)
            {
                string grupo = (listaCxL[0] as Cx_Lancamento).grupoTipoPagamento_nome;
                double valorCobrado = 0;
                foreach (Cx_Lancamento cxL in listaCxL)
                {
                    Mov mov = null;
                    query = db.Query();
                    query = db.Query();
                    query.Constrain(typeof(Mov));
                    query.Descend("idOperacao").Constrain(cxL.idOperacao);
                    if (query.Execute().Count > 0)
                    {
                        mov = query.Execute()[0] as Mov;
                        if (mov.idMovCanceladora == 0 && mov.tipo == EMovTipo.saida_venda)
                        {
                            //valorCobrado += cxL.valorRecebido;
                            valorCobrado += cxL.valorCobrado;
                        }
                    }
                    if (cxL.tipo == ECxLancamentoTipo.recebimento && cxL.situacao == ECxLancamentoSituacao.lancado)
                        listaCxLRecebimentos.Add(cxL);
                    if (cxL.tipo == ECxLancamentoTipo.retirada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        listaCxLRetiradas.Add(cxL);
                    if (cxL.tipo == ECxLancamentoTipo.entrada && cxL.situacao == ECxLancamentoSituacao.lancado)
                        listaCxLEntradas.Add(cxL);
                }

                if (valorCobrado > 0)
                {
                    if (grupo == "DINHEIRO")
                        totalEntradas += valorCobrado;
                    if (grupo == "A VISTA")
                        totalEntradas += valorCobrado;
                }

            }

            foreach (Cx_Lancamento cxL in listaCxLRecebimentos)
                totalEntradas += cxL.valorRecebido;

            foreach (Cx_Lancamento cxL in listaCxLEntradas)
                totalEntradas += cxL.valorRecebido;

            double totalRetiradas = 0;
            foreach (Cx_Lancamento cxL in listaCxLRetiradas)
                totalRetiradas += cxL.valorCobrado;

            cxD.totalEntradas = totalEntradas;
            cxD.totalSaidas = totalRetiradas;
        }

    }


}