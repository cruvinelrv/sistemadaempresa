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
using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using SDE.PDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public void ListaCasamento_Imprime(int idCorp, int idListaCasamento)
        {
            ListaCasamento listaCasamento = new ListaCasamento();
            listaCasamento.ConstroiListaCasamento(idCorp, idListaCasamento);
        }

        public int ListaCasamento_Abre(int idCorp, int idEmp, int idClienteFuncionarioLogado, int idCliente, string dataEvento)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query;

                    query = db.Query();
                    query.Constrain(typeof(Cliente));
                    query.Descend("id").Constrain(idCliente);
                    IObjectSet rs_cliente = query.Execute();
                    Cliente cliente = rs_cliente[0] as Cliente;

                    ReaproveitamentoCodigo reaproveitamento = AppFacade.get.reaproveitamento;
                    OrdemServico novaListaCasamento = new OrdemServico();
                    novaListaCasamento.id = reaproveitamento.getIncremento(db, typeof(OrdemServico), 0);
                    novaListaCasamento.idEmp = idEmp;
                    novaListaCasamento.idCliente = idCliente;
                    novaListaCasamento.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    novaListaCasamento.dthrInicio = dataEvento;
                    novaListaCasamento.status = SDE.Enumerador.EOrdemServicoStatus.em_andamento;
                    novaListaCasamento.cliente_nome = cliente.nome;
                    novaListaCasamento.cliente_cpf = cliente.cpf_cnpj;

                    dbStore(db, novaListaCasamento);
                    dbCommit(db);
                    return novaListaCasamento.id;
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                    return 0;
                }
            }
        }

        public void ListaCasamento_Altera(int idCorp, int idEmp, OrdemServico listaAlteracao)
        {
            defineCorp(idCorp);
            try
            {
                IQuery query = db.Query();
                query.Constrain(typeof(OrdemServico));
                query.Descend("id").Constrain(listaAlteracao.id);
                IObjectSet rs_lista = query.Execute();
                OrdemServico lista_db = (OrdemServico)rs_lista[0];

                Utils.copiaCamposBasicos(listaAlteracao, lista_db);
                dbStore(db, lista_db);
                dbCommit(db);
            }
            catch (Exception ex)
            {
                dbRollback(ex, db);
            }
        }

        public void ListaCasamento_LancaItem(int idCorp, OrdemServico_Item listaItem)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    listaItem.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(OrdemServico_Item), 0);
                    dbStore(db, listaItem);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void ListaCasamento_RemoveItem(int idCorp, int idListaItem)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query = db.Query();
                    query.Constrain(typeof(OrdemServico_Item));
                    query.Descend("id").Constrain(idListaItem);
                    IObjectSet rs_listaItem = query.Execute();
                    OrdemServico_Item listaItem_db = rs_listaItem[0] as OrdemServico_Item;
                    dbRemove(db, listaItem_db);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }

        public void ListaCasamento_Fecha(int idCorp, int idLista)
        {
            defineCorp(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    IQuery query = db.Query();
                    query.Constrain(typeof(OrdemServico));
                    query.Descend("id").Constrain(idLista);
                    IObjectSet rs_listaCasamento = query.Execute();
                    OrdemServico listaCasamento_db = rs_listaCasamento[0] as OrdemServico;
                    listaCasamento_db.status = SDE.Enumerador.EOrdemServicoStatus.finalizada;
                    dbStore(db, listaCasamento_db);
                    dbCommit(db);
                }
                catch (Exception ex)
                {
                    dbRollback(ex, db);
                }
            }
        }
    }
}
