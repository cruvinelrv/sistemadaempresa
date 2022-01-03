using System;
using System.Collections;
using System.Collections.Generic;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SDE.CamadaServico
{
    public class SCorp : SuperServico
    {

        public CFOP Load_CFOP(string codigoCFOP)
        {
            setBancoID(0);
            return cCorp.LoadCFOP(codigoCFOP);
        }
        /*
        public IList Lista_CFOP()
        {
            IQuery q = db0.Query();
            q.Constrain(typeof(CFOP));
            q.Descend("id").OrderAscending();
            IList r = q.Execute();
            return r;
        }

        public IList Lista_IBGE_Municipios()
        {
            IQuery q = db0.Query();
            q.Constrain(typeof(IBGEMunicipio));
            q.Descend("nome").OrderAscending();
            IList r = q.Execute();
            return r;
        }

        public IList Lista_IBGE_Estados()
        {
            IQuery q = db0.Query();
            q.Constrain(typeof(IBGEEstado));
            q.Descend("nome").OrderAscending();
            IList r = q.Execute();
            return r;
        }
        */
        public Empresa LoadEmpresaCompleta(int idCorp, int idEmp)
        {
            setBancoID(idCorp);
            Empresa emp = cEmp.Load(idEmp);

            emp.__cliente = cCliente.Load(emp.idCliente);
            emp.__cliente.__enderecos = cCliente.LoadEnderecos(emp.idCliente);
            emp.__cliente.__familiares = cCliente.LoadFamiliares(emp.idCliente);
            emp.__cliente.__contatos = cCliente.LoadContatos(emp.idCliente);
            return emp;
        }
        /*
        public CorporacaoListas CorpListas(int idCorp)
        {
            setBancoID(idCorp);
            return cCorp.Listas();
        }
         * 
        public EmpresaListas EmpListas(int idCorp)
        {
            setBancoID(idCorp);
            return cEmp.Listas();
        }

        public void AtualizarEmpListas(int idCorp, int idClienteFuncionarioLogado, EmpresaListas empListasDados)
        {
            if (empListasDados == null)
                return;
            setBancoID(idCorp);
            EmpresaListas empL = cEmp.Listas();
            try
            {

                if (empListasDados.centroCustos != null)
                    cEmp.AtualizaCentrosCusto(idClienteFuncionarioLogado, empL.centroCustos, empListasDados.centroCustos);
                if (empListasDados.planosConta != null)
                    cEmp.AtualizaPlanoContas(idClienteFuncionarioLogado, empL.planosConta, empListasDados.planosConta);

                if (empListasDados.contas != null)
                    cEmp.AtualizaContas(idClienteFuncionarioLogado, empL.contas, empListasDados.contas);
                if (empListasDados.portadores != null)
                    cEmp.AtualizaPortadores(idClienteFuncionarioLogado, empL.portadores, empListasDados.portadores);

                if (empListasDados.tiposDocumento != null)
                    cEmp.AtualizaTiposDocumento(idClienteFuncionarioLogado, empL.tiposDocumento, empListasDados.tiposDocumento);
                if (empListasDados.formas != null)
                    cEmp.AtualizaTiposPagamento(idClienteFuncionarioLogado, empL.formas, empListasDados.formas);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }

        }
        /*
        public void AtualizarCorpListas(int idCorp, CorporacaoListas corpListasDados)
        {
            if (corpListasDados == null)
                return;
            setBancoID(idCorp);
            CorporacaoListas corpLista = cCorp.Listas();
            try
            {
                if (corpListasDados.marcas != null)
                    cCorp.AtualizaMarcas(corpLista.marcas, corpListasDados.marcas);
                if (corpListasDados.categorias != null)
                    cCorp.AtualizaSecoes(corpLista.categorias, corpListasDados.categorias);
                if (corpListasDados.grades != null)
                    cCorp.AtualizaGrade(corpLista.grades, corpListasDados.grades);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }

        }
        */
        public Empresa LoadEmpresa(int idCorp, int idEmp)
        {
            setBancoID(idCorp);
            return cEmp.Load(idEmp);
        }
        /*
        public CaixaEmpDia teste(CaixaEmpDia recebe)
        {
            CaixaEmpDia c = new CaixaEmpDia();
            c.__totais = new Dictionary<string, double>();
            c.__totais.Add(EValorEspecie.cheque_a_vista.ToString(), 20);
            c.__totais.Add(EValorEspecie.reserva.ToString(), 30);
            c.__totais.Add(EValorEspecie.dinheiro.ToString(), 40);
            c.dtDia = "recebi!";
            return c;
        }
        */

        //http://cleberbox/sde1/DownloadNFE.asmx?WSDL
        //br.com.sistemadaempresa.DownloadNFE

       // var url:String = "http://sde.sistemadaempresa.com.br/notaprefeitura.aspx?idCorp="+Sessao.unica.idCorp+"&idMov="+idMov;


    }
}
