using System;
using System.Collections;
using System.Collections.Generic;
using SDE.Entidade;
using SDE.Parametro;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;
using SDE.Enumerador;


namespace SDE.CamadaControle
{
    public class CCliente : SuperControle
    {

        #region Load

        public List<ClienteBancario> LoadBancarios(int idCliente)
        {
            IList<ClienteBancario> rs = db.Query<ClienteBancario>(
                delegate(ClienteBancario cb)
                {
                    return (cb.idCliente == idCliente);
                }
            );
            return new List<ClienteBancario>(rs);
        }
        
        public List<ClienteContato> LoadContatos(int idCliente)
        {
            IList<ClienteContato> rs = db.Query<ClienteContato>(
                delegate(ClienteContato xxx)
                {
                    return (xxx.idCliente == idCliente);
                }
            );
            return new List<ClienteContato>(rs);
        }

        public ClienteEndereco LoadEndereco(int idEndereco)
        {
            IList<ClienteEndereco> rs = db.Query<ClienteEndereco>(
                delegate(ClienteEndereco ce)
                {
                    return (ce.id == idEndereco);
                }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public List<ClienteEndereco> LoadEnderecos(int idCliente)
        {
            IList<ClienteEndereco> rs = db.Query<ClienteEndereco>(
                delegate(ClienteEndereco xxx)
                {
                    return (xxx.idCliente == idCliente);
                }    
            );
            return new List<ClienteEndereco>(rs);
        }

        public List<ClienteFamiliar> LoadFamiliares(int idCliente)
        {
            IList<ClienteFamiliar> rs = db.Query<ClienteFamiliar>(
                delegate(ClienteFamiliar xxx)
                {
                    return (xxx.idCliente == idCliente);
                }
            );
            return new List<ClienteFamiliar>(rs);
        }

        public List<ClienteVeiculo> LoadVeiculos(int idCliente)
        {
            IList<ClienteVeiculo> rs = db.Query<ClienteVeiculo>(
                delegate(ClienteVeiculo xxx)
                {
                    return (xxx.idCliente == idCliente);
                }
            );
            return new List<ClienteVeiculo>(rs);
        }

        public Cliente Load(int idCliente)
        {
            IList<Cliente> rs = db.Query<Cliente>(
                delegate(Cliente cli)
                {
                    return (cli.id == idCliente);
                }
            );
            if (rs.Count == 1)
                return rs[0];
            return null;
        }

        public Cliente LoadCpfCnpj(string cpfCnpj)
        {

            IList<Cliente> rs = db.Query<Cliente>(
                delegate(Cliente cli)
                {
                    return (cli.cpf_cnpj == cpfCnpj);
                }
            );
            if (rs.Count == 1)
                return rs[0];
            return null;
        }

        public ClienteVeiculo LoadVeiculo(int idVeiculo)
        {
            IList<ClienteVeiculo> rs = db.Query<ClienteVeiculo>(
                delegate(ClienteVeiculo cv)
                {
                    return (cv.id == idVeiculo);
                }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        #endregion

        #region Pesquisa
        public IList<Cliente> Pesquisa(bool fornecedor, bool funcionario, bool tranportador, bool parceiro ,
                            string texto, EPesTipo tipo)
        {
            //  "se" estamos pesquisando exclusivamente funcionarios,
            //      esta pessoa não se trata de um, portanto retornamos "false"
            //  caso contrário
            //      estamos pesquisando um cliente comum, e devemos retornar "true"
            bool estamos_fazendo_uma_pesquisa_exclusiva = (fornecedor || funcionario || parceiro || tranportador);
            IList<Cliente> rs = db.Query<Cliente>(
                delegate(Cliente cli)
                {
                    if (!Utils.verifica(texto, cli.nome, cli.cpf_cnpj, cli.apelido_razsoc))
                        return false;
                    if (tipo != EPesTipo.nao_informado &&  cli.tipo != tipo)
                        return false;
                    
                    if (!estamos_fazendo_uma_pesquisa_exclusiva)
                        return true;
                    if (fornecedor && cli.ehFornecedor)
                        return true;
                    if (funcionario && cli.ehFuncionario)
                        return true;
                    if (tranportador && cli.ehTransportador)
                        return true;
                    if (parceiro && cli.ehParceiro)
                        return true;
                    return false;
                }
            );
            return rs;
        }
        #endregion

        #region Novo
        public Cliente Novo(Cliente cDados)
        {
            //CorporacaoMax cmax = queryCMax();
            Cliente c = new Cliente();
            //c.id = ++cmax.idCliente;
            c.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
            c.nome = cDados.nome;
            c.cpf_cnpj = cDados.cpf_cnpj;
            c.apelido_razsoc = cDados.apelido_razsoc;
            c.tipo = cDados.tipo;
            
            DateTime dt = DateTime.Today;
            c.dtRegistro = Utils.getDateString(dt);
            c.dtRegistroTicks = dt.Ticks;
            c.dtNasc = "01/01/1900";
            c.dtNascTicks = DateTime.Parse("01/01/1900").Ticks;

            db.Store(c);
            //db.Store(cmax);
            return c;
        }
        #endregion

        #region Atualizar

        public void Atualizar(Cliente c, Cliente cDados)
        {
            Utils.copiaCamposBasicos(cDados, c);
            db.Store(c);
        }

        /*
        public void Atualizar(int idCliente, IList<ClienteBancario> objetos)
        {
            if (objetos == null || objetos.Count == 0)
                return;
            //CorporacaoMax cm = queryCMax();
            IList<ClienteBancario> bancariosBD = LoadBancarios(idCliente);
            IList<ClienteBancario> bancarios = new List<ClienteBancario>(bancariosBD);
            //damos uma id àqueles que tem id zero, 
            //e os adicionamos na lista principal, para nao atualizarmos duas vezes
            foreach (ClienteBancario cb in objetos)
                if (cb.id == 0)
                {
                    //cb.id = ++cm.idClienteBancario;
                    cb.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteBancario), 0);
                    cb.idCliente = idCliente;
                    bancarios.Add(cb);
                }
            //criamos dicionarios para agilizar a iteração
            Dictionary<int, ClienteBancario> dictDados = new Dictionary<int, ClienteBancario>();
            Dictionary<int, ClienteBancario> dict = new Dictionary<int, ClienteBancario>();
            foreach (ClienteBancario cb in objetos)
                dictDados.Add(cb.id, cb);
            foreach (ClienteBancario cb in bancarios)
                dict.Add(cb.id, cb);
            //espera-se que as quantidades sejam iguais
            if (dict.Count != dictDados.Count)
                throw new Exception(string.Format("quantidades diferentes em CBancarios {0} , dados: {1}", dict.Count, dictDados.Count));
            //transpomos todos os dados
            foreach (int idCB in dictDados.Keys)
            {
                ClienteBancario bc = dict[idCB];
                ClienteBancario bcd = dictDados[idCB];
                //aqueles que deverem ser deletados, serão aqui mesmo
                //não há necessidade de retirarmos da lista, pois a lista será descartável
                if (bcd.isDeletado)
                    db0.Delete(bc);
                else
                {
                    Utils.copiaCamposBasicos(bcd, bc);
                    db0.Store(bc);
                }
            }
        }
        */
        public void Atualizar(int idCliente, IList<ClienteContato> objetos, ArrayList _insercoes, ArrayList _remocoes)
        {
            if (objetos == null || objetos.Count == 0)
                return;
            //CorporacaoMax cm = queryCMax();
            IList<ClienteContato> contatosBD = LoadContatos(idCliente);
            IList<ClienteContato> contatos = new List<ClienteContato>(contatosBD);
            //damos uma id àqueles que tem id zero, 
            //e os adicionamos na lista principal, para nao atualizarmos duas vezes
            foreach (ClienteContato ccDados in objetos)
                if (ccDados.id == 0)
                {
                    //ccDados.id = ++cm.idClienteContato;
                    ccDados.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0);
                    ccDados.idCliente = idCliente;
                    contatos.Add(ccDados);
                }
            //criamos dicionarios para agilizar a iteração
            Dictionary<int, ClienteContato> dictDados = new Dictionary<int, ClienteContato>();
            Dictionary<int, ClienteContato> dict = new Dictionary<int, ClienteContato>();
            foreach (ClienteContato cc in objetos)
                dictDados.Add(cc.id, cc);
            foreach (ClienteContato cc in contatos)
                dict.Add(cc.id, cc);
            //espera-se que as quantidades sejam iguais
            if (dict.Count != dictDados.Count)
                throw new Exception(string.Format("quantidades diferentes em PContatos {0} , dados: {1}", dict.Count, dictDados.Count));
            //transpomos todos os dados
            foreach (int idCC in dictDados.Keys)
            {
                ClienteContato cc = dict[idCC];
                ClienteContato ccd = dictDados[idCC];
                //aqueles que deverem ser deletados, serão aqui mesmo
                //não há necessidade de retirarmos da lista, pois a lista será descartável
                if (ccd.isDeletado)
                {
                    db.Delete(cc);
                    _remocoes.Add(cc);
                }
                else
                {
                    Utils.copiaCamposBasicos(ccd, cc);
                    db.Store(cc);
                    _insercoes.Add(cc);
                }
            }
            //db.Store(cm);
        }
        public void Atualizar(int idCliente, IList<ClienteEndereco> objetos, ArrayList _insercoes, ArrayList _remocoes)
        {
            if (objetos == null || objetos.Count == 0)
                return;
            //CorporacaoMax cm = queryCMax();
            IList<ClienteEndereco> enderecosBD = LoadEnderecos(idCliente);
            IList<ClienteEndereco> enderecos = new List<ClienteEndereco>(enderecosBD);
            //damos uma id àqueles que tem id zero, 
            //e os adicionamos na lista principal, para nao atualizarmos duas vezes
            foreach (ClienteEndereco ce in objetos)
            {
                if (ce.id == 0)
                {
                    //ce.id = ++cm.idClienteEndereco;
                    ce.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteEndereco), 0);
                    ce.idCliente = idCliente;
                    enderecos.Add(ce);
                }
            }
            //criamos dicionarios para agilizar a iteração
            Dictionary<int, ClienteEndereco> dictDados = new Dictionary<int, ClienteEndereco>();
            Dictionary<int, ClienteEndereco> dict = new Dictionary<int, ClienteEndereco>();
            foreach (ClienteEndereco ce in objetos)
                dictDados.Add(ce.id, ce);
            foreach (ClienteEndereco ce in enderecos)
                dict.Add(ce.id, ce);
            //espera-se que as quantidades sejam iguais
            if (dict.Count != dictDados.Count)
                throw new Exception(string.Format("quantidades diferentes em PEnderecos {0} , dados: {1}", dict.Count, dictDados.Count));
            //transpomos todos os dados
            foreach (int idCE in dictDados.Keys)
            {
                ClienteEndereco ce = dict[idCE];
                ClienteEndereco ced = dictDados[idCE];
                //aqueles que deverem ser deletados, serão aqui mesmo
                //não há necessidade de retirarmos da lista, pois a lista será descartável
                if (ced.isDeletado)
                {
                    db.Delete(ce);
                    _remocoes.Add(ce);
                }
                else
                {
                    Utils.copiaCamposBasicos(ced, ce);
                    db.Store(ce);
                    _insercoes.Add(ce);
                }
            }
            //db.Store(cm);
        }
        public void Atualizar(int idCliente, IList<ClienteFamiliar> objetos, ArrayList _insercoes, ArrayList _remocoes)
        {
            if (objetos == null || objetos.Count == 0)
                return;
            //CorporacaoMax cm = queryCMax();
            IList<ClienteFamiliar> familiaresBD = LoadFamiliares(idCliente);
            IList<ClienteFamiliar> familiares = new List<ClienteFamiliar>(familiaresBD);
            //damos uma id àqueles que tem id zero, 
            //e os adicionamos na lista principal, para nao atualizarmos duas vezes
            foreach (ClienteFamiliar cf in objetos)
                if (cf.id == 0)
                {
                    //cf.id = ++cm.idClienteFamiliar;
                    cf.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteFamiliar), 0);
                    cf.idCliente = idCliente;
                    familiares.Add(cf);
                }

            //criamos dicionarios para agilizar a iteração
            Dictionary<int, ClienteFamiliar> dictDados = new Dictionary<int, ClienteFamiliar>();
            Dictionary<int, ClienteFamiliar> dict = new Dictionary<int, ClienteFamiliar>();
            foreach (ClienteFamiliar cf in objetos)
                dictDados.Add(cf.id, cf);
            foreach (ClienteFamiliar cf in familiares)
                dict.Add(cf.id, cf);
            //espera-se que as quantidades sejam iguais
            if (dict.Count != dictDados.Count)
                throw new Exception(string.Format("quantidades diferentes em PAmigos {0} , dados: {1}", dict.Count, dictDados.Count));
            //transpomos todos os dados
            foreach (int idCF in dictDados.Keys)
            {
                ClienteFamiliar cf = dict[idCF];
                ClienteFamiliar cfd = dictDados[idCF];
                //aqueles que deverem ser deletados, serão aqui mesmo
                //não há necessidade de retirarmos da lista, pois a lista será descartável
                if (cfd.isDeletado)
                {
                    db.Delete(cf);
                    _remocoes.Add(cf);
                }
                else
                {
                    Utils.copiaCamposBasicos(cfd, cf);
                    db.Store(cf);
                    _insercoes.Add(cf);
                }
            }
            //db.Store(cm);
        }
        public void Atualizar(int idCliente, IList<ClienteVeiculo> objetos, ArrayList _insercoes, ArrayList _remocoes)
        {
            if (objetos == null || objetos.Count == 0)
                return;
            //CorporacaoMax cm = queryCMax();
            IList<ClienteVeiculo> veiculosBD = LoadVeiculos(idCliente);
            IList<ClienteVeiculo> veiculos = new List<ClienteVeiculo>(veiculosBD);
            //damos uma id àqueles que tem id zero, 
            //e os adicionamos na lista principal, para nao atualizarmos duas vezes
            foreach (ClienteVeiculo cv in objetos)
            {
                if (cv.id == 0)
                {
                    //cv.id = ++cm.idClienteVeiculo;
                    cv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteVeiculo), 0);
                    cv.idCliente = idCliente;
                    veiculos.Add(cv);
                }
            }

            //criamos dicionarios para agilizar a iteração
            Dictionary<int, ClienteVeiculo> dictDados = new Dictionary<int, ClienteVeiculo>();
            Dictionary<int, ClienteVeiculo> dict = new Dictionary<int, ClienteVeiculo>();
            foreach (ClienteVeiculo cv in objetos)
                dictDados.Add(cv.id, cv);
            foreach (ClienteVeiculo cv in veiculos)
                dict.Add(cv.id, cv);
            //espera-se que as quantidades sejam iguais
            if (dict.Count != dictDados.Count)
                throw new Exception(string.Format("quantidades diferentes em CVeiculos {0} , dados: {1}", dict.Count, dictDados.Count));
            //transpomos todos os dados
            foreach (int idCV in dictDados.Keys)
            {
                ClienteVeiculo cv = dict[idCV];
                ClienteVeiculo cvd = dictDados[idCV];
                //aqueles que deverem ser deletados, serão aqui mesmo
                //não há necessidade de retirarmos da lista, pois a lista será descartável
                if (cvd.isDeletado)
                {
                    db.Delete(cv);
                    _remocoes.Add(cv);
                }
                else
                {
                    Utils.copiaCamposBasicos(cvd, cv);
                    db.Store(cv);
                    _insercoes.Add(cv);
                }
            }
            //db.Store(cm);
        }

        #endregion

    }
}
