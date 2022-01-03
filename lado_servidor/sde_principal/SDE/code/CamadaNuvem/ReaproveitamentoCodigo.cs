using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDE.Entidade;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SDE.Atributos;
using SDE.Constantes;
using System.Reflection;
using SDE.CamadaNuvem;

public class ReaproveitamentoCodigo
{
    public ReaproveitamentoCodigo(AppFacade app)
    {

    }



    public void GeraLancamentoFinanceiro(SuperNuvem nuvem,
        int idTransacao, int idOperacao,
        int idContaDestino, int idContaOrigem,
        string nomeLancamento, double valorLancamento
        )
    {
        Finan_Lancamento lancamento = new Finan_Lancamento();
        lancamento.idTransacao = idTransacao;
        lancamento.idOperacao = idOperacao;
        lancamento.nome = nomeLancamento;

        Finan_Lancamento lancaDebito = new Finan_Lancamento();
        Finan_Lancamento lancaCredito = new Finan_Lancamento();
        Utils.copiaCamposBasicos(lancamento, lancaDebito);
        Utils.copiaCamposBasicos(lancamento, lancaCredito);
        //

        //credito
        lancaCredito.idContaOrigem = idContaOrigem;
        lancaCredito.idContaDestino = idContaDestino;
        lancaCredito.valorLancado = valorLancamento;
        //debito
        lancaDebito.idContaDestino = lancaCredito.idContaOrigem;
        lancaDebito.idContaOrigem = lancaCredito.idContaDestino;
        lancaDebito.valorLancado = -lancaCredito.valorLancado;
        //id
        lancaDebito.id = this.getIncremento(nuvem.db, typeof(Finan_Lancamento), 1);
        lancaCredito.id = lancaDebito.id + 1;
        //

        Finan_Conta contaDebitar = null, contaCreditar = null;
        foreach (Finan_Conta c in nuvem.db.Query<Finan_Conta>())
        {
            if (c.id == lancaCredito.idContaOrigem)
                contaDebitar = c;
            else if (c.id == lancaCredito.idContaDestino)
                contaCreditar = c;
        }

        contaDebitar.saldoAtual = Math.Round(contaDebitar.saldoAtual, 2);
        contaCreditar.saldoAtual = Math.Round(contaCreditar.saldoAtual, 2);

        //contaDebitar
        lancaDebito.isCredito = false;
        lancaDebito.saldoAnterior = contaDebitar.saldoAtual;
        contaDebitar.saldoAnterior = contaDebitar.saldoAtual;
        contaDebitar.saldoAtual += lancaDebito.valorLancado;
        lancaDebito.saldoAtual = contaDebitar.saldoAtual;

        //contaCreditar
        lancaCredito.isCredito = true;
        lancaCredito.saldoAnterior = contaCreditar.saldoAtual;
        contaCreditar.saldoAnterior = contaCreditar.saldoAtual;
        contaCreditar.saldoAtual += lancaCredito.valorLancado;
        lancaCredito.saldoAtual = contaCreditar.saldoAtual;

        nuvem.dbStore(nuvem.db, contaDebitar, contaCreditar, lancaCredito, lancaDebito);


    }











    public void AtualizaConfiguracoes(IObjectContainer dbX)
    {
        IList<SdeConfig> camposMeus = dbX.Query<SdeConfig>();

        foreach (FieldInfo f in typeof(Variaveis_SdeConfig).GetFields())
        {
            string[] aaa = (string[])f.GetValue(null);

            String campo = aaa[0];
            String valorInicial = aaa[1];
            bool existe = false;
            //SdeConfig c = null;
            foreach (SdeConfig c in camposMeus)
                if (c.variavel == campo)
                {
                    existe = true;
                    break;
                }
            //
            if (!existe)
            {

                SdeConfig c = new SdeConfig();
                c.variavel = campo;
                c.valor = valorInicial;
                c.prioridade = 0;//0==corp,1==emp,10==usuario-logado
                //
                c.id = getIncremento(dbX, typeof(SdeConfig), 0);
                dbX.Store(c);
            }
        }
    }

    public int getIncrementoOperacao(IObjectContainer dbX, int idEmp, int idClienteFuncionarioLogado)
    {
        ContadorOperacao objInterno = new ContadorOperacao();
        objInterno.id = getIncremento(dbX, objInterno.GetType(), 0);
        objInterno.dthr = Utils.getAgoraString();
        objInterno.idEmp = idEmp;
        objInterno.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
        dbX.Store(objInterno);
        return objInterno.id;
    }

    public int getIncrementoTransacao(IObjectContainer dbX, int idEmp, int idClienteFuncionarioLogado)
    {
        ContadorTransacao objInterno = new ContadorTransacao();
        objInterno.id = getIncremento(dbX, objInterno.GetType(), 0);
        objInterno.dthr = Utils.getAgoraString();
        objInterno.idEmp = idEmp;
        objInterno.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
        dbX.Store(objInterno);
        return objInterno.id;
    }

    public int getIncremento(IObjectContainer dbX, Type tipo, int passos)
    {
        //pega o atributo da classe
        object[] atributosClasse = tipo.GetCustomAttributes(false);
        if (atributosClasse.Length == 0)
            throw new Exception("classe " + tipo.FullName + " não possui atributos");
        AtEnt attr = atributosClasse[0] as AtEnt;



        //pega a instancia correta
        String entidade = tipo.FullName;
        Incremento incremento = null;
        IList<Incremento> incrementos = dbX.Query<Incremento>();
        foreach (Incremento i in incrementos)
        {
            if (i.entidade == entidade)
                incremento = i;
        }




        //caso não exista, cria
        if (incremento==null)
            incremento = new Incremento() { entidade = entidade, id = incrementos.Count+1 };
        
        
        
        //pega o próximo
        incremento.valorUltimaID++;
        int proximo = incremento.valorUltimaID;
        


        //caso passos > 0, incrementa um numero maior de passos durante esta transação
        incremento.valorUltimaID += passos;
        dbX.Store(incremento);

        //dbX.Commit(); NÃO PODE FAZER COMMIT POIS ESTAMOS NO BANCO DE CADASTROS
        return proximo;
    }

    public void setIncremento(IObjectContainer dbX, Type tipo, int valor)
    {
        //pega o atributo da classe
        object[] atributosClasse = tipo.GetCustomAttributes(false);
        if (atributosClasse.Length == 0)
            throw new Exception("classe " + tipo.FullName + " não possui atributos");
        AtEnt attr = atributosClasse[0] as AtEnt;

        //pega o banco correto
        /*
        if (attr.banco == EnumBanco.bancoZero)
            idCorp = 0;
        IObjectContainer dbX = AppFacade.get.conexaoBanco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
        */
        //pega a instancia correta
        String entidade = tipo.FullName;


        Incremento incremento = null;
        IList<Incremento> incrementos = dbX.Query<Incremento>();
        foreach (Incremento i in incrementos)
        {
            if (i.entidade == entidade)
                incremento = i;
        }
        //caso não exista, cria
        if (incremento == null)
            incremento = new Incremento() { entidade = entidade, id = incrementos.Count + 1 };
        //define o valor
        incremento.valorUltimaID = valor;


        dbX.Store(incremento);
        //dbX.Commit(); NÃO PODE FAZER COMMIT POIS ESTAMOS NO BANCO DE CADASTROS
    }




    public Item Item_Load(IObjectContainer db, int idItem)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(Item));
        query.Descend("id").Constrain(idItem);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return (Item)rs[0];
        if (rs.Count == 0)
            return null;
        
        throw new Exception(
            string.Format("Existem {0} itens cadastrados para o id {1}", rs.Count, idItem)
            );
    }

    public ItemEmpEstoque Item_LoadEstoque(IObjectContainer db, int idEmpEstoque, bool aceitaZeroResultados)
    {
        IQuery q = db.Query();
        q.Constrain(typeof(ItemEmpEstoque));
        q.Descend("id").Constrain(idEmpEstoque);
        IObjectSet rs = q.Execute();
        if (rs.Count == 1)
            return (ItemEmpEstoque)rs[0];
        if (rs.Count == 0 && aceitaZeroResultados)
            return null;
        //
        throw new Exception(
            string.Format("Existem {0} estoques cadastrados para o id IEE {1}", rs.Count, idEmpEstoque)
            );
    }

    public IList Lista(IObjectContainer dbX, Type t, string fieldNameOrdenador)//
    {
        //Type t = typeof(SDE.Entidade.CFOP);
        IQuery q = dbX.Query();
        q.Constrain(t);
        q.Descend(fieldNameOrdenador).OrderAscending();
        IList rs = q.Execute();
        return rs;
    }

    public Cliente Cliente_Load(IObjectContainer db, string cpf_cnpj)
    {
        //if (AppFacade.get.isAmbienteTeste)
        //    return null;
        //
        IQuery q = db.Query();
        q.Constrain(typeof(Cliente));
        q.Descend("cpf_cnpj").Constrain(cpf_cnpj);
        IObjectSet rs = q.Execute();
        if (rs.Count == 1)
            return (Cliente)rs[0];
        if (rs.Count == 0)
            return null;
        //
        throw new Exception(
            string.Format("Existem {0} clientes cadastrados para o cpf/cnpj {1}", rs.Count, cpf_cnpj)
            );
    }
    public Cliente Cliente_Load(IObjectContainer db, int idCliente)
    {
        IQuery q = db.Query();
        q.Constrain(typeof(Cliente));
        q.Descend("id").Constrain(idCliente);
        IObjectSet rs = q.Execute();
        if (rs.Count == 1)
            return (Cliente)rs[0];
        if (rs.Count == 0)
            return null;
        //
        throw new Exception(
            string.Format("Existem {0} clientes cadastrados para o id {1}", rs.Count, idCliente)
            );
    }

    public Mov Mov_Load(IObjectContainer db, int idMov)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(Mov));
        query.Descend("id").Constrain(idMov);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return rs[0] as Mov;
        if (rs.Count == 0)
            return null;
        throw new Exception(
            string.Format("Existem {0} movimentações cadastrados para o id{1}", rs.Count, idMov)
            );
    }

    public ClienteEndereco Endereco_Load(IObjectContainer db, int idEndereco)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(ClienteEndereco));
        query.Descend("id").Constrain(idEndereco);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return rs[0] as ClienteEndereco;
        if (rs.Count == 0)
            return null;
        throw new Exception(string.Format("Existem {0} enderecos cadastrados para o id {1}",rs.Count, idEndereco));
    }

    public Empresa Empresa_Load(IObjectContainer db, int idEmpresa)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(Empresa));
        query.Descend("id").Constrain(idEmpresa);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return rs[0] as Empresa;
        if (rs.Count == 0)
            return null;
        throw new Exception(string.Format("Existem {0} empresas cadastrados para o id {1}", rs.Count, idEmpresa));
    }

    public MovNFE MovNFE_Load(IObjectContainer db, int idMov)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(MovNFE));
        query.Descend("idMov").Constrain(idMov);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return rs[0] as MovNFE;
        if (rs.Count == 0)
            return null;
        throw new Exception(string.Format("Existem {0} MovNFE cadastradas para o id {1}", rs.Count, idMov));
    }

    public MovNfeVeiculo movNfeVeiculo_Load(IObjectContainer db, int idMovNFE)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(MovNfeVeiculo));
        query.Descend("idMovNFE").Constrain(idMovNFE);
        IObjectSet rs = query.Execute();
        if (rs.Count == 1)
            return rs[0] as MovNfeVeiculo;
        if (rs.Count == 0)
            return null;
        throw new Exception(string.Format("Existem {0} MovNfeVeiculo cadastradas para o id {1}", rs.Count, idMovNFE));
    }

    public List<MovItem> movItem_List(IObjectContainer db, int idMov)
    {
        List<MovItem> listaRetorno = new List<MovItem>();
        IQuery query = db.Query();
        query.Constrain(typeof(MovItem));
        query.Descend("idMov").Constrain(idMov);
        foreach (MovItem mi in query.Execute())
            listaRetorno.Add(mi);
        return listaRetorno;
    }

    public bool caixaAberto(IObjectContainer db, int idEmp, string data)
    {
        IQuery query = db.Query();
        query.Constrain(typeof(Cx_Diario));
        query.Descend("idEmp").Constrain(idEmp);
        query.Descend("data").Constrain(data);
        if (query.Execute().Count > 0)
            return true;
        else
            return false;
    }

    public double getValorSaldoAtualCaixaPorData(IObjectContainer db, int idEmp, string data)
    {
        Cx_Diario cxD = null;
        DateTime dataSelecionada = DateTime.Parse(data);
        List<Cx_Diario> listaCxD = new List<Cx_Diario>();
        IQuery query = db.Query();
        query.Constrain(typeof(Cx_Diario));
        query.Descend("idEmp").Constrain(idEmp);
        //não posso utilizar o id como critério de sequência pois é possível fazer aberturas retroativas
        //query.Descend("id").OrderDescending();
        foreach (Cx_Diario xxx in query.Execute())
            if (DateTime.Parse(xxx.data) < dataSelecionada)
                listaCxD.Add(xxx);

        if (listaCxD.Count == 0)
            return 0;

        IEnumerable<Cx_Diario> enumerableCxD = listaCxD.OrderBy(item => Utils.StringToDateTime(item.data));
        cxD = enumerableCxD.Last();
        return (cxD.valorAbertura + cxD.totalEntradas) - cxD.totalSaidas;
    }
}