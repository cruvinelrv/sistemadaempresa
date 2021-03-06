package SDE.CamadaNuvem
{
    import mx.rpc.events.ResultEvent;
    import mx.core.Application;
    import Core.App;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Entidade.*;
    
    public final class NuvemListagem
    {
        
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaNuvem.NuvemListagem');
        private var TOTAL_INVOCACOES:Number = 60;
        private var a:App = null;
        
        public function Inicializa():void
        {
            this.a = App.single;
            this.ListaDB(Item.CLASSE);
            this.ListaDB(ItemEmp.CLASSE);
            this.ListaDB(ItemFornecedor.CLASSE);
            this.ListaDB(ItemEmpAliquotas.CLASSE);
            this.ListaDB(ItemEmpPreco.CLASSE);
            this.ListaDB(ItemEmpEstoque.CLASSE);
            this.ListaDB(Cliente.CLASSE);
            this.ListaDB(ClienteBancario.CLASSE);
            this.ListaDB(ClienteContato.CLASSE);
            this.ListaDB(ClienteEndereco.CLASSE);
            this.ListaDB(ClienteFamiliar.CLASSE);
            this.ListaDB(ClienteVeiculo.CLASSE);
            this.ListaDB(ClienteFuncionarioComissionamento.CLASSE);
            this.ListaDB(ClienteFuncionarioPermissao.CLASSE);
            this.ListaDB(ClienteFuncionarioUsuario.CLASSE);
            this.ListaDB(Cargo.CLASSE);
            this.ListaDB(CargoComissionamento.CLASSE);
            this.ListaDB(CargoPermissao.CLASSE);
            this.ListaDB(TempMov.CLASSE);
            this.ListaDB(TempMovNFE.CLASSE);
            this.ListaDB(TempMovItem.CLASSE);
            this.ListaDB(Cad_Marca.CLASSE);
            this.ListaDB(Cad_Grade.CLASSE);
            this.ListaDB(Cad_Secao.CLASSE);
            this.ListaDB(Empresa.CLASSE);
            this.ListaDB(ContadorTransacao.CLASSE);
            this.ListaDB(ContadorOperacao.CLASSE);
            this.ListaDB(Finan_Titulo.CLASSE);
            this.ListaDB(Finan_TituloItem.CLASSE);
            this.ListaDB(Finan_Lancamento.CLASSE);
            this.ListaDB(Finan_GrupoTipoPagamento.CLASSE);
            this.ListaDB(Finan_CentroCusto.CLASSE);
            this.ListaDB(Finan_TipoDocumento.CLASSE);
            this.ListaDB(Finan_PortadorTipo.CLASSE);
            this.ListaDB(Finan_TipoLancamento.CLASSE);
            this.ListaDB(Finan_Portador.CLASSE);
            this.ListaDB(Finan_Conta.CLASSE);
            this.ListaDB(Finan_TipoPagamento.CLASSE);
            this.ListaDB(Finan_TipoPagamento_Parcela.CLASSE);
            this.ListaDB(Balanco.CLASSE);
            this.ListaDB(BalancoItem.CLASSE);
            this.ListaDB(Cx_Lancamento.CLASSE);
            this.ListaDB(Cx_Diario.CLASSE);
            this.ListaDB(Corporacao.CLASSE);
            this.ListaDB(SdeConfig.CLASSE);
            this.ListaDB(CFOP.CLASSE);
            this.ListaDB(IBGEMunicipio.CLASSE);
            this.ListaDB(IBGEEstado.CLASSE);
            this.ListaDB(LoginEmpresa.CLASSE);
            this.ListaDB(OrdemServico.CLASSE);
            this.ListaDB(OrdemServico_Tipo.CLASSE);
            this.ListaDB(OrdemServico_Item.CLASSE);
            this.ListaDB(OrdemServico_Executor.CLASSE);
            this.ListaDB(Mov.CLASSE);
            this.ListaDB(MovNFE.CLASSE);
            this.ListaDB(MovNfeVeiculo.CLASSE);
            this.ListaDB(MovItem.CLASSE);
            this.ListaDB(MovItemEstoque.CLASSE);
            this.ListaDB(MovValor.CLASSE);
            this.ListaDB(Orcamento_Lancamento.CLASSE);
        }
        public function ListaDB(classe:String, fRetorno:Function=null):void
        {
            if (a.cache['array'+classe]!=null && fRetorno!=null)
            {
                fRetorno(a.cache['array'+classe]);
            }
            else
            {
                ro.Invoca('ListaDB', [a.idCorp, classe],
                    function(retorno:Array):void
                    {
                        a.gerJan.CompletouDownloadUmaTabela(TOTAL_INVOCACOES);
                        a.cache['array'+classe] = retorno;
                        if (fRetorno!=null)
                            fRetorno(a.cache['array'+classe]);
                    }
                );
            }
        }
    }
}
