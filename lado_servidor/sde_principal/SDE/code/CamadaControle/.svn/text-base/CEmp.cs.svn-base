using System;
using System.Collections.Generic;
using SDE.Entidade;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;


namespace SDE.CamadaControle
{
    public class CEmp : SuperControle
    {

        #region Novo
        /*
        public EmpresaParam salvaParametro(EmpresaParam parametro)
        {

            db.Store(parametro);
            return parametro;

        }
        */
        public Empresa NovaEmpresa(Empresa emp)
        {
            //CorporacaoMax cMax = queryCMax();
            Utils.filtraCampos(emp);
            //emp.id = ++cMax.idEmpresa;
            emp.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Empresa), 0);
            db.Store(emp);
            //db.Store(cMax);
            return emp;
        }

        /*
        public Login NovoLogin(int idCorp, Login lancaDebito)
        {
            Max m = queryMax();
            Utils.filtraCampos(lancaDebito);
            lancaDebito.id = ++m.idLogin;
            lancaDebito.telas.idLogin = lancaDebito.id;
            lancaDebito.idCorp = idCorp;
            lancaDebito.telas.idCorp = idCorp;

            db0.Store(m);
            db0.Store(lancaDebito);
            return lancaDebito;
        }
        */
        public System.Web.UI.WebControls.Login NovoLogin(int idCorp, System.Web.UI.WebControls.Login l)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Load

        public IList<Cliente> LoadClienteUsuario(string usuario, string senha)
        {
            IList<Cliente> ret = db.Query<Cliente>(delegate(Cliente xxx) { return xxx.loginUsuario == usuario && xxx.loginSenha == senha; });
            return ret;
        }



        public Empresa Load(int idEmp)
        {
            IList<Empresa> rs = db.Query<Empresa>(
                  delegate(Empresa emp)
                  {
                      return (emp.id == idEmp);
                  }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }
        public Empresa Load(string usuario)
        {
            IList<Empresa> rs = db.Query<Empresa>(
                  delegate(Empresa emp)
                  {
                      return (emp.usuario == usuario);
                  }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }
        /*
        public EmpresaParam LoadParam(int idEmp)
        {
            IList<EmpresaParam> rs = db.Query<EmpresaParam>(
                delegate(EmpresaParam param)
                {
                    return (param.idEmp == idEmp);
                }
            );
            return (rs.Count == 1)?rs[0]:null;
        }
        public Login LoadLogin(string empresa, string usuario)
        {
            IList<Login> rs = db0.Query<Login>(
                  delegate(Login login)
                  {
                      return (login.empresa == empresa && login.usuario == usuario);
                  }
            );
            if (rs.Count > 1)
                throw new Exception(  string.Format("Foram detectados um total de {0} usuários iguais para {1} - {2} operação cancelada", rs.Count, empresa, usuario) );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public Login LoadLogin(string empresa, string usuario, string senha)
        {
            IList<Login> rs = db0.Query<Login>(
                  delegate(Login login)
                  {
                      return (login.empresa == empresa && login.usuario == usuario && login.senha == senha);
                  }
            );
            if (rs.Count > 1)
                throw new Exception("Foi detectado algum bloqueio em seu usuário, por favor, contate-nos reportando o problema. (64) 3621.4579");
            return (rs.Count == 1) ? rs[0] : null;
        }
        */

        #endregion

        #region Ataulizacao

        public void AtualizaCentrosCusto(int idClienteFuncionarioLogado, List<Finan_CentroCusto> centros, List<Finan_CentroCusto> centrosDados)
        {
            foreach (Finan_CentroCusto xxx in centrosDados)
            {
                bool existe = false;
                foreach (Finan_CentroCusto yyy in centros)
                {
                    if (yyy.id == xxx.id)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    centros.Add(xxx);
                }
            }
            db.Store(centros);
        }
        /*
        public void AtualizaPlanoContas(int idClienteFuncionarioLogado, List<Finan_PlanoConta> contas, List<Finan_PlanoConta> contasDados)
        {
            foreach (Finan_PlanoConta xxx in contasDados)
            {
                bool existe = false;
                foreach (Finan_PlanoConta yyy in contas)
                {
                    if (yyy.cod == xxx.cod)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    contas.Add(xxx);
                }
            }
            db.Store(contas);
        }
         * */
        public void AtualizaContas(int idClienteFuncionarioLogado, List<Finan_Conta> contas, List<Finan_Conta> contasDados)
        {
            foreach (Finan_Conta xxx in contasDados)
            {
                bool existe = false;
                foreach (Finan_Conta yyy in contas)
                {
                    if (yyy.id == xxx.id)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    contas.Add(xxx);
                }
            }
            db.Store(contas);
        }
        public void AtualizaPortadores(int idClienteFuncionarioLogado, List<Finan_Portador> portadores, List<Finan_Portador> portadoresDados)
        {
            foreach (Finan_Portador xxx in portadoresDados)
            {
                bool existe = false;
                foreach (Finan_Portador yyy in portadores)
                {
                    if (yyy.id == xxx.id)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    portadores.Add(xxx);
                }
            }
            db.Store(portadores);
        }
        public void AtualizaTiposPagamento(int idClienteFuncionarioLogado, List<Finan_TipoPagamento> tiposPagamento, List<Finan_TipoPagamento> tiposPagamentoDados)
        {
            foreach (Finan_TipoPagamento xxx in tiposPagamentoDados)
            {
                bool existe = false;
                foreach (Finan_TipoPagamento yyy in tiposPagamento)
                {
                    if (yyy.id == xxx.id)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    tiposPagamento.Add(xxx);
                }
            }
            db.Store(tiposPagamento);
        }
        public void AtualizaTiposDocumento(int idClienteFuncionarioLogado, List<Finan_TipoDocumento> tiposDocumento, List<Finan_TipoDocumento> tiposDocumentoDados)
        {
            foreach (Finan_TipoDocumento xxx in tiposDocumentoDados)
            {
                bool existe = false;
                foreach (Finan_TipoDocumento yyy in tiposDocumento)
                {
                    if (yyy.id == xxx.id)
                        existe = true;
                }
                if (!existe)
                {
                    xxx.idClienteFuncionarioLogado = idClienteFuncionarioLogado;
                    tiposDocumento.Add(xxx);
                }
            }
            db.Store(tiposDocumento);
        }
        /*
        public void AtualizaLoginTelas(LoginTelas lt, LoginTelas ltDados)
        {
            Utils.copiaCamposBasicos(ltDados, lt);
            db0.Store(lt);
        }
        public IList<Login> ListaLogin(string empresa)
        {
            IList<Login> rs = db0.Query<Login>(
                  delegate(Login login)
                  {
                      return (login.empresa == empresa);
                  }
            );
            return rs;
        }
         * */
        #endregion


    }
}
