using System;
using System.Collections.Generic;
using System.Collections;
using Db4objects.Db4o;

using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;
namespace SDE.CamadaServico
{
    public class SCliente : SuperServico
    {

        public ClienteEndereco LoadEnderecoPorId(int idEndereco)
        {
            return cCliente.LoadEndereco(idEndereco);
        }

        public Cliente LoadClienteCpfCnpj(int idCorp, string cpf)
        {
            setBancoID(idCorp);
            return cCliente.LoadCpfCnpj(cpf);
        }
        
        public Cliente Load(int idCorp, int idCliente, ParamLoadCliente pl)
        {
            setBancoID(idCorp);
            try
            {
                Cliente cliTemp = cCliente.Load(idCliente);
                if (cliTemp == null)
                    return null;

                Cliente cli = null;
                //se estive preenchido E não ignorando
                //explicitamente ignorando
                if (pl == null || !pl.ignorar)
                {
                    cli = cliTemp;                    
                }
                else
                {
                    cli = new Cliente();
                    cli.id       = cliTemp.id;
                }

                if (pl!=null)
                {
                    if (pl.contatos)
                        cli.__contatos = cCliente.LoadContatos(idCliente);
                    if (pl.enderecos)
                        cli.__enderecos = cCliente.LoadEnderecos(idCliente);
                    if (pl.familiares)
                        cli.__familiares = cCliente.LoadFamiliares(idCliente);
                    if (pl.bancarios)
                        cli.__bancarios = cCliente.LoadBancarios(idCliente);
                    if (pl.veiculos)
                        cli.__veiculos = cCliente.LoadVeiculos(idCliente);
                }
                return cli;
            }
            catch (Exception ex)
            {
                //caso alguma exceção esja gerada, gravamos e lançamos ao usuário final
                throw anotaErro(ex);
            }
        }

        public IList<Cliente> Pesquisa(int idCorp, ParamFiltroCliente pf, ParamLoadCliente pl)
        {
            setBancoID(idCorp);
            pf.texto += pf.cpf;
            IList<Cliente> clientes = cCliente.Pesquisa(pf.fornecedor, pf.funcionario,
                                                        pf.transportador, pf.parceiro,
                                                        pf.texto, pf.tipo);
            IList<Cliente> listaRet = new List<Cliente>();
            foreach (Cliente cli in clientes)
	        {
        		//Verificaçao de preenchemento
                    if (pl != null)
                    {
                        
                        
                        if (pl.contatos)
                            cli.__contatos = cCliente.LoadContatos(cli.id);
                        if (pl.enderecos)
                            cli.__enderecos = cCliente.LoadEnderecos(cli.id);
                        if (pl.familiares)
                            cli.__familiares = cCliente.LoadFamiliares(cli.id);

                        if (pl.bancarios)
                            cli.__bancarios = cCliente.LoadBancarios(cli.id);
                        if (pl.veiculos)
                            cli.__veiculos = cCliente.LoadVeiculos(cli.id);
                       
                    }

                    listaRet.Add(cli); 
	        }
 
            return listaRet;
        }
        /*
        public Cliente Novo(int idCorp, Cliente cDados)
        {
            setBancoID(idCorp);
            Cliente c = null;
            lock (db.Ext().Lock())
            {
                try
                {
                    c = cCliente.LoadCpfCnpj(cDados.cpf_cnpj);
                    if (c == null)
                    {                      
                        c = cCliente.Novo(cDados);
                        db.Commit();
                    }
                    return c;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }

        public Cliente Atualizar(int idCorp, Cliente cliente, bool retornar)
        {
            setBancoID(idCorp);
            Cliente c = null;
            lock (db.Ext().Lock())
            {
                try
                {       
                    c = cCliente.Load(cliente.id);
                    #region Atualiza Cliente
                    cCliente.Atualizar(c, cliente);
                    if (cliente.__contatos != null)
                        cCliente.Atualizar(c.id, cliente.__contatos);
                    if (cliente.__enderecos != null)
                        cCliente.Atualizar(c.id, cliente.__enderecos);
                    if (cliente.__familiares != null)
                        cCliente.Atualizar(c.id, cliente.__familiares);
                    if (cliente.__veiculos != null)
                        cCliente.Atualizar(c.id, cliente.__veiculos);
                    
                    #endregion
                    db.Commit();
                    return (retornar) ? c : null;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
        }
    */
    }
}
