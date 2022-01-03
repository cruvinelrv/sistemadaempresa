using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;


namespace SDE.CamadaServico
{
    public class SRelatorio : SuperServico
    {
        //Dados do Cabecalho dos Relátorios
        public Empresa LoadCabecalho(int idCorporacao, int idEmpresa)
        {
            setBancoID(idCorporacao);
            Empresa e = cEmp.Load(idEmpresa);
            e.__cliente = cCliente.Load(e.idCliente);
            e.__cliente.__enderecos = cCliente.LoadEnderecos(e.idCliente);
            if (e.__cliente.__enderecos.Count < 1)
                e.__cliente.__enderecos.Add(new ClienteEndereco());
            return e;
        }

        public List<Mov> RelNFE(int idCorporacao, int idEmpresa, int idMov)
        {
            setBancoID(idCorporacao);
            Mov mov = cMov.Load(idMov);
            if (mov == null)
                return null;
            //busca cliente
            mov.__cli = cCliente.Load(mov.idCliente);
            mov.__mItens = cMov.LoadMovItens(mov.id);
            mov.__movNfe = cMov.LoadNFE(mov.id);
            //empresa
            mov.__emp = cEmp.Load(mov.idEmp);
            mov.__emp.__cliente = cCliente.Load(mov.__emp.idCliente);
            //load itens 
            foreach (MovItem mi in mov.__mItens)
            {
                mi.__item = cItem.Load(mi.idItem);
                mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);
            }
            //Busca NFE
            mov.__movNfe = cMov.LoadNFE(mov.id);
            if (mov.__movNfe != null)
            {
                //busca transportador
                mov.__movNfe.__transportador = cCliente.Load(mov.__movNfe.idClienteTransp);
                //Busca Endereco
                mov.__movNfe.__ceCliente = cCliente.LoadEndereco(mov.__movNfe.idEnderecoCliente);
                mov.__movNfe.__ceEmpresa = cCliente.LoadEndereco(mov.__movNfe.idEnderecoEmp);
                mov.__movNfe.__ceTransporte = cCliente.LoadEndereco(mov.__movNfe.idEnderecoTransp);
                //Busca Veiculos
                mov.__movNfe.__veiculo = cCliente.LoadVeiculo(mov.__movNfe.idVeiculo);               
                //busca movimentacao referenciada
                if (mov.__movNfe.idmovReferenciada != 0)
                {
                    mov.__movNfe.__nfeRf = cMov.LoadNFE(mov.__movNfe.idmovReferenciada);
                }
            }
            List<Mov> ret = new List<Mov>();
            ret.Add(mov);
            return ret;
        }




        //Relátorio de Resumo Movimentação
        public List<Mov> RelMovResumo(int idCorporacao, ParamFiltroMov pf)
        {
            setBancoID(idCorporacao);
            long dtInicial = DateTime.Parse(pf.dtInicial).Ticks;
            long dtFinal = DateTime.Parse(pf.dtFinal).AddDays(1).Ticks;
            List<Mov> listaMov = cMov.Pesquisa(dtInicial, dtFinal, pf.tipos);
            if (listaMov == null)
                return null;
            foreach (Mov mov in listaMov)
                mov.__mValores = cMov.LoadMovValores(mov.id);
            return listaMov;
        }

        //Movimentação por Id
        public List<Mov> RelMovId(int idCorporacao, ParamFiltroMov pf)
        {
            setBancoID(idCorporacao);
            Mov mov = cMov.Load(pf.idMov);
            List<Mov> lista = new List<Mov>();
            if (mov == null)
                return lista;
            
            lista.Add(mov);
            foreach (Mov mv in lista)
            {
                //funcionario
                mv.__cliFuncionario = cCliente.Load(mv.idClienteFuncionarioLogado);
                //cliente
                mv.__cli = cCliente.Load(mv.idCliente);
                mv.__cli.__contatos = cCliente.LoadContatos(mv.__cli.id);
                mv.__cli.__enderecos = cCliente.LoadEnderecos(mv.__cli.id);
                //valores
                mv.__mValores = cMov.LoadMovValores(mv.id);
                //itens
                mv.__mItens = cMov.LoadMovItens(mv.id);
                foreach (MovItem mi in mv.__mItens)
                {
                    mi.__item = cItem.Load(mi.idItem);
                    mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);
                }
            }
            return lista;
        }

        //Relátorio movimentação Diaria
        public List<Mov> RelMovDiario(int idCorporacao, ParamFiltroMov pf)
        {
            setBancoID(idCorporacao);
            long dtInicial = DateTime.Parse(pf.dtInicial).Ticks;
            long dtFinal = DateTime.Parse(pf.dtFinal).AddDays(1).Ticks;
            List<Mov> listaMov = cMov.Pesquisa(dtInicial, dtFinal, pf.tipos);
            foreach (Mov mov in listaMov)
            {
                //Funcionario
                mov.__cliFuncionario = cCliente.Load(mov.idClienteFuncionarioLogado);
                //cliente
                mov.__cli = cCliente.Load(mov.idCliente);
                mov.__cli.__contatos = cCliente.LoadContatos(mov.__cli.id);
                mov.__cli.__enderecos = cCliente.LoadEnderecos(mov.__cli.id);
                //valores
                mov.__mValores = cMov.LoadMovValores(mov.id);
                //itens
                mov.__mItens = cMov.LoadMovItens(mov.id);
                foreach (MovItem mi in mov.__mItens)
                {
                    mi.__item = cItem.Load(mi.idItem);
                    mi.__mIEstoques = cMov.ListMovItemEstoque(mi.id);
                }
            }
            return listaMov;
        }

        //Relátorio de Clientes
        public IList<Cliente> RelClientes(int idCorporacao, ParamFiltroCliente pf)
        {
            setBancoID(idCorporacao);
            IList<Cliente> clientes = cCliente.Pesquisa(pf.fornecedor, pf.funcionario,
                                                        pf.transportador, pf.parceiro,
                                                        pf.texto, pf.tipo);
            return clientes;
        }

        //Relátorio de Estoques
        public IList<Item> RelEstoque(int idCorporacao, int idEmpresa, ParamFiltroItem pf)
        {
            setBancoID(idCorporacao);
            IList listaItem = cItem.lista();
            IList<Item> retorno = new List<Item>();
            foreach (Item it in listaItem)
            {
                it.__estoques = cMov.PesquisaEstoque(it.id, idEmpresa);
                retorno.Add(it);

                if (it.__estoques == null)
                    throw new Exception("nulo:" + it.id);
            }
            return retorno;
            //return listaItem;
        }

        public IList<Item> Etiquetas(int idCorporacao, int idEmpresa, IList<int> listaIdEstoque)
        {
            setBancoID(idCorporacao);
            IList<ItemEmpEstoque> estoques = cMov.PesquisaEstoque(idEmpresa, listaIdEstoque);
            IList<Item> lista = new List<Item>();

            foreach (ItemEmpEstoque iee in estoques)
            {
                Item it = cItem.Load(iee.idItem);
                it.__estoques = new List<ItemEmpEstoque>() { iee };
                it.__ie = cItem.LoadItemEmp(iee.idItem, idEmpresa);
                it.__ie.__preco = cItem.LoadPreco(iee.idItem, idEmpresa);

                lista.Add(it);
            }
            return lista;
        }

        public IList<Item> EtiquetasIdMov(int idCorporacao, int idEmpresa, int idMov)
        {
            setBancoID(idCorporacao);
            IList<Item> ret = new List<Item>();
            //busca movimentação
            Mov mv = cMov.Load(idMov);
            if (mv == null)
                throw new Exception("Movimentação Inexistente");
            mv.__mItens = cMov.LoadMovItens(idMov);
            
            foreach (MovItem mi in mv.__mItens)
            {
                Item it =  cItem.Load(mi.idItem);
                it.__ie = cItem.LoadItemEmp(it.id, idEmpresa);
                it.__ie.__preco = cItem.LoadPreco( it.id, idEmpresa);

                it.__estoques = new List<ItemEmpEstoque>();
                List<MovItemEstoque> movEstoques = cMov.ListMovItemEstoque(mi.id);
                foreach (MovItemEstoque mie in movEstoques)
                {  
                    ItemEmpEstoque iep = null;
                    if (mie.idIEE != 0)
                        iep = cMov.LoadEstoque(mie.idIEE) ;
                    else
                        iep = cMov.LoadEstoque(mv.idEmp, mi.idItem, mie.identificador);
                    it.__estoques.Add(iep);
                    for (int i = 0; i < mie.qtd; i++)
                        ret.Add(it);
                }
            }
            return ret;
        }

    }
}
