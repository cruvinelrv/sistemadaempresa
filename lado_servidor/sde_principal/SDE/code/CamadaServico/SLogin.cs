using System;
using System.Collections.Generic;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SDE.CamadaServico
{
    public interface ITeste { void metodo1(); string prop1 { get; set; } string prop2 { get; } string prop3 { set; } }
    public class SLogin : SuperServico
    {
        /*
        public void AtualizaLoginTelas(string emp, string usu, LoginTelas loginTelas)
        {
            try
            {
                Login login = cEmp.LoadLogin(emp, usu);
                cEmp.AtualizaLoginTelas(login.telas, loginTelas);
                db0.Commit();
            }
            catch (Exception ex)
            {
                db0.Rollback();
                throw anotaErro(ex);
            }

        }
        */
        public string ValidaVersao(string versao)
        {
            return EModo.producao.ToString();
        }

        public int[] FazLogin2(string emp, string usu, string sen)
        {
            IQuery q = db0.Query();
            q.Constrain(typeof(LoginEmpresa));
            q.Descend("empresa").Constrain(emp);
            IObjectSet rs = q.Execute();
            if (rs.Count == 0)
                return null;
            else if (rs.Count > 1)
                throw new Exception("existem "+rs.Count+" logins-empresa para "+emp);

            LoginEmpresa lEmp = (LoginEmpresa)rs[0];
            setBancoID(lEmp.idCorp);

            IList<Cliente> clientes = cEmp.LoadClienteUsuario(usu, sen);

            if (clientes.Count == 0)
                return null;
            else if (clientes.Count > 1)
                throw new Exception("existem " + rs.Count + " logins-empresa para " + emp);
            Cliente cli = clientes[0];



            lock (db.Ext().Lock())
            {
                AppFacade.get.reaproveitamento.AtualizaConfiguracoes(db);
                db.Commit();
            }



            int[] ret = new int[] { lEmp.idCorp, lEmp.idEmp, cli.id };
            return ret;
        }
        public int[] FazLogin(string empresa, string usuario, string senha)
        {
            IQuery query;

            query = db0.Query();
            query.Constrain(typeof(LoginEmpresa));
            query.Descend("empresa").Constrain(empresa);
            IObjectSet rs_loginEmpresa = query.Execute();
            if (rs_loginEmpresa.Count == 0)
                return null;
            else if (rs_loginEmpresa.Count > 1)
                throw new Exception("Existem " + rs_loginEmpresa.Count + " logins-empresa para " + empresa);

            LoginEmpresa loginEmpresa = rs_loginEmpresa[0] as LoginEmpresa;
            setBancoID(loginEmpresa.idCorp);

            query = db.Query();
            query.Constrain(typeof(ClienteFuncionarioUsuario));
            query.Descend("login").Constrain(usuario);
            IObjectSet rs_clienteFuncionarioUsuario = query.Execute();

            if (rs_clienteFuncionarioUsuario.Count == 0)
                return null;
            else if (rs_clienteFuncionarioUsuario.Count > 1)
                throw new Exception("Existem " + rs_clienteFuncionarioUsuario.Count + "logins-empresa para " + empresa);
            ClienteFuncionarioUsuario clienteFuncionarioUsuario = rs_clienteFuncionarioUsuario[0] as ClienteFuncionarioUsuario;

            if (clienteFuncionarioUsuario.idEmp != loginEmpresa.idEmp)
                return null;

            if (senha == String.Empty)
            {
                if (clienteFuncionarioUsuario.senha != String.Empty)
                    return null;
            }
            else
            {
                if (senha != clienteFuncionarioUsuario.senha)
                    return null;
            }

            query = db.Query();
            query.Constrain(typeof(Cliente));
            query.Descend("id").Constrain(clienteFuncionarioUsuario.idCliente);
            IObjectSet rs_cliente = query.Execute();
            Cliente cliente = rs_cliente[0] as Cliente;

            lock (db.Ext().Lock())
            {
                AppFacade.get.reaproveitamento.AtualizaConfiguracoes(db);
                db.Commit();
            }

            int[] retorno = new int[] { loginEmpresa.idCorp, loginEmpresa.idEmp, cliente.id,
                (clienteFuncionarioUsuario.usuarioTecnico) ? 1 : 0,
                (clienteFuncionarioUsuario.senha == String.Empty) ? 1 : 0 };
            return retorno;
        }

        public void SalvaSenha(int corpId, int empId, int clienteId, string senha)
        {
            setBancoID(corpId);

            IQuery query = db.Query();
            query.Constrain(typeof(ClienteFuncionarioUsuario));
            query.Descend("idEmp").Constrain(empId);
            query.Descend("idCliente").Constrain(clienteId);
            IObjectSet rs_clienteFuncionarioUsuario = query.Execute();
            ClienteFuncionarioUsuario clienteFuncionarioUsuario_db = rs_clienteFuncionarioUsuario[0] as ClienteFuncionarioUsuario;
            clienteFuncionarioUsuario_db.senha = senha;
            db.Store(clienteFuncionarioUsuario_db);
            db.Commit();
        }

        /*
        public Login FazLogin(string emp, string usu, string sen)
        {
            try
            {
                Login login = cEmp.LoadLogin(emp, usu, sen);
                if (login == null)
                    return null;

                setBancoID(login.idCorp);
                cEmp = null;
                login.__cliente = cCliente.Load(login.idCliente);

                login.__emp = cEmp.Load(login.idEmp);

                int idCliente = login.__emp.idCliente;
                login.__emp.__cliente = cCliente.Load( idCliente );


                return login;
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
        }
        */
        public void GuardaCEP(string cep, string tipo_logr, string logr, string bairro, string cidade, string uf)
        {

        }


    }
}
