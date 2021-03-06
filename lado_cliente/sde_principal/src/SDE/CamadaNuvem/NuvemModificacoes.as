package SDE.CamadaNuvem
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Entidade.*;
    
    public final class NuvemModificacoes
    {
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaNuvem.NuvemModificacoes');
        //ev.result    //CS=Void    //AS=Void
        public function Cargo_NovoAtualizacao(cargo:Cargo, cargoComissionamento:CargoComissionamento, listCargoPermissoes:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Cargo_NovoAtualizacao', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, cargo, cargoComissionamento, listCargoPermissoes], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function RelatorioCliente_EscrevePdf(fRetorno:Function=null):void
        {
            ro.Invoca('RelatorioCliente_EscrevePdf', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Cliente_Genericos_Salva(classe:String, objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Cliente_Genericos_Salva', [Sessao.unica.idCorp, classe, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Cliente    //AS=Cliente
        public function Cliente_NovoAltera(cliente:Cliente, fRetorno:Function=null):void
        {
            ro.Invoca('Cliente_NovoAltera', [Sessao.unica.idCorp, cliente], function(r:Cliente):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function Cargo_NovoAltera(cargo:Cargo, fRetorno:Function=null):void
        {
            ro.Invoca('Cargo_NovoAltera', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, cargo], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=CargoPermissao[]    //AS=Array
        public function CargoPermissoes_NovosAtualiza(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('CargoPermissoes_NovosAtualiza', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ClienteFuncionario_SalvaDados(cliente:Cliente, fRetorno:Function=null):void
        {
            ro.Invoca('ClienteFuncionario_SalvaDados', [Sessao.unica.idCorp, cliente], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ClienteFuncionario_Atualiza(clienteFuncionario:Cliente, clienteFuncionarioComissionamento:ClienteFuncionarioComissionamento, listClienteFuncionarioPermissoes:Array, fRetorno:Function=null):void
        {
            ro.Invoca('ClienteFuncionario_Atualiza', [Sessao.unica.idCorp, Sessao.unica.idEmp, clienteFuncionario, clienteFuncionarioComissionamento, listClienteFuncionarioPermissoes], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=String    //AS=String
        public function EnviaEmail(idMov:Number, idCliente:Number, fromMailAddressData:String, fromNameData:String, fromPasswordData:String, toMailAddressData:String, toNameData:String, subjectData:String, messageData:String, urlData:String, fRetorno:Function=null):void
        {
            ro.Invoca('EnviaEmail', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov, idCliente, fromMailAddressData, fromNameData, fromPasswordData, toMailAddressData, toNameData, subjectData, messageData, urlData], function(r:String):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function SdeConfig_Reseta(fRetorno:Function=null):void
        {
            ro.Invoca('SdeConfig_Reseta', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=SdeConfig[]    //AS=Array
        public function SdeConfig_NovosAtualizacoes(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('SdeConfig_NovosAtualizacoes', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function RelatorioBalanco_Parcial(idBalanco:Number, fRetorno:Function=null):void
        {
            ro.Invoca('RelatorioBalanco_Parcial', [Sessao.unica.idCorp, Sessao.unica.idEmp, idBalanco], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function Balanco_Abre(fRetorno:Function=null):void
        {
            ro.Invoca('Balanco_Abre', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Balanco_Lanca(bi:BalancoItem, fRetorno:Function=null):void
        {
            ro.Invoca('Balanco_Lanca', [Sessao.unica.idCorp, bi], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Balanco_Remove(idBalancoItem:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Balanco_Remove', [Sessao.unica.idCorp, idBalancoItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Balanco_Fecha(idBalanco:Number, boolConcluiCancela:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('Balanco_Fecha', [Sessao.unica.idCorp, Sessao.unica.idEmp, idBalanco, boolConcluiCancela], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function RelatorioOrdemServico(idOrdemServico:Number, fRetorno:Function=null):void
        {
            ro.Invoca('RelatorioOrdemServico', [Sessao.unica.idCorp, Sessao.unica.idEmp, idOrdemServico], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function OrdemServico_Tipo_NovosAtualizacoes(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_Tipo_NovosAtualizacoes', [Sessao.unica.idCorp, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function OrdemServico_NovoAtualizacao(ordemServico_atualizacao:OrdemServico, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_NovoAtualizacao', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, ordemServico_atualizacao], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function OrdemServico_NovoItem(idOrdemServico:Number, osi:OrdemServico_Item, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_NovoItem', [idOrdemServico, osi], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function OrdemServico_AtualizaItem(osi:OrdemServico_Item, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_AtualizaItem', [osi], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function OrdemServico_NovoExecutor(idOrdemServico:Number, idOrdemServicoItem:Number, ose:OrdemServico_Executor, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_NovoExecutor', [idOrdemServico, idOrdemServicoItem, ose], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function OrdemServico_AtualizaExecutor(ose:OrdemServico_Executor, fRetorno:Function=null):void
        {
            ro.Invoca('OrdemServico_AtualizaExecutor', [ose], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_ResetaEstoque(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_ResetaEstoque', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_ResetaPreco(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_ResetaPreco', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_DefineDescontoMaximo(descMax:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_DefineDescontoMaximo', [Sessao.unica.idCorp, descMax], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CertificaIdItemEmpAliquotas(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CertificaIdItemEmpAliquotas', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CertificaItemEmpAliquotas(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CertificaItemEmpAliquotas', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CertificaItemEmpPreco(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CertificaItemEmpPreco', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CertificaItemEmpEstoque(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CertificaItemEmpEstoque', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_defineQuantidadeEstoqueTodos(qtd:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_defineQuantidadeEstoqueTodos', [Sessao.unica.idCorp, Sessao.unica.idEmp, qtd], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_nomeEtiqueta(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_nomeEtiqueta', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_ProdutosDesuso(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_ProdutosDesuso', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_InsereComissaoProdutoMov(comissao:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_InsereComissaoProdutoMov', [Sessao.unica.idCorp, Sessao.unica.idEmp, comissao], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_ItensTodosMaiusculo(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_ItensTodosMaiusculo', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_AtualizacaoBalanco(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_AtualizacaoBalanco', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_LancaQuantidadeProdutosZerados(qtd:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_LancaQuantidadeProdutosZerados', [Sessao.unica.idCorp, Sessao.unica.idEmp, qtd], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CaixaAtualizacao(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CaixaAtualizacao', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CertificaIdEmpCxLancamento(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CertificaIdEmpCxLancamento', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_CaixaSaldoParaValorAbertura(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_CaixaSaldoParaValorAbertura', [Sessao.unica.idCorp, Sessao.unica.idEmp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_VerificaGrade_Grupo_Subgrupo(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_VerificaGrade_Grupo_Subgrupo', [], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_VerificaEspacosVaziosInicioFim(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_VerificaEspacosVaziosInicioFim', [], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_AtualizacaoControleUsuario(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_AtualizacaoControleUsuario', [], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_FuncaoTemporaria(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_FuncaoTemporaria', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Tecnico_ResetaTiposLancamento(fRetorno:Function=null):void
        {
            ro.Invoca('Tecnico_ResetaTiposLancamento', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ListaCasamento_Imprime(idListaCasamento:Number, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_Imprime', [Sessao.unica.idCorp, idListaCasamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function ListaCasamento_Abre(idCliente:Number, dataEvento:String, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_Abre', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, idCliente, dataEvento], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ListaCasamento_Altera(listaAlteracao:OrdemServico, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_Altera', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaAlteracao], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ListaCasamento_LancaItem(listaItem:OrdemServico_Item, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_LancaItem', [Sessao.unica.idCorp, listaItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ListaCasamento_RemoveItem(idListaItem:Number, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_RemoveItem', [Sessao.unica.idCorp, idListaItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ListaCasamento_Fecha(idLista:Number, fRetorno:Function=null):void
        {
            ro.Invoca('ListaCasamento_Fecha', [Sessao.unica.idCorp, idLista], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Estoque_InventarioEscrevePdf(porGrupo:Boolean, tipoPreco:String, pctSobreValor:Number, dataInventario:String, cabecalhoInventario:String, mostraZerados:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('Estoque_InventarioEscrevePdf', [Sessao.unica.idCorp, Sessao.unica.idEmp, porGrupo, tipoPreco, pctSobreValor, dataInventario, cabecalhoInventario, mostraZerados], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaEntrada(mov:Mov, listIEP:Array, fRetorno:Function=null):void
        {
            ro.Invoca('NovaEntrada', [Sessao.unica.idCorp, Sessao.unica.idEmp, mov, listIEP], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function AlteraEntrada(movOriginal:Mov, movAlterada:Mov, listIEP:Array, fRetorno:Function=null):void
        {
            ro.Invoca('AlteraEntrada', [Sessao.unica.idCorp, Sessao.unica.idEmp, movOriginal, movAlterada, listIEP], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Finan_Portador[]    //AS=Array
        public function Finan_Portador_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_Portador_Novos', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Finan_PortadorTipo[]    //AS=Array
        public function Finan_PortadorTipo_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_PortadorTipo_Novos', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Finan_CentroCusto[]    //AS=Array
        public function Finan_CentroCusto_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_CentroCusto_Novos', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_GrupoTipoPagamento_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_GrupoTipoPagamento_Novos', [Sessao.unica.idCorp, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Finan_TipoDocumento[]    //AS=Array
        public function Finan_TipoDocumento_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_TipoDocumento_Novos', [Sessao.unica.idCorp, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_TipoLancamento_Novos(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_TipoLancamento_Novos', [Sessao.unica.idCorp, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Finan_Conta[]    //AS=Array
        public function Finan_Conta_NovosAtualizacoes(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_Conta_NovosAtualizacoes', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, objetos], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_TipoPagamento_NovosAtualizacoes(objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_TipoPagamento_NovosAtualizacoes', [Sessao.unica.idCorp, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_LancaCapitalTotal(valor:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_LancaCapitalTotal', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, valor], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_Lancamento_Transferencia(lancamento:Finan_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_Lancamento_Transferencia', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, lancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_lancamento_DebitoCreditoVista(lancamento:Finan_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_lancamento_DebitoCreditoVista', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, lancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_lancamento_RecebimentoRota(lancamento:Finan_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_lancamento_RecebimentoRota', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, lancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ChequeNovo(finanTitulo:Finan_Titulo, cliente:Cliente, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ChequeNovo', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, finanTitulo, cliente], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ChequeAltera(finanTitulo:Finan_Titulo, cliente:Cliente, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ChequeAltera', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, finanTitulo, cliente], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ChequeBaixa(finanTitulo:Finan_Titulo, finanConta:Finan_Conta, clieteDestino:Cliente, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ChequeBaixa', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, finanTitulo, finanConta, clieteDestino], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ChequeCompensa(finanTitulo:Finan_Titulo, finanConta:Finan_Conta, isDevolucao:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ChequeCompensa', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, finanTitulo, finanConta, isDevolucao], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_TituloBaixa(listaFinanTituloItem:Array, finanConta:Finan_Conta, finanTipoPagamento:Finan_TipoPagamento, idCentroCusto:Number, idTipoLancamento:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_TituloBaixa', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, listaFinanTituloItem, finanConta, finanTipoPagamento, idCentroCusto, idTipoLancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Caixa_RetiradaPagamento(cxLancamento:Cx_Lancamento, finanLancamento:Finan_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Caixa_RetiradaPagamento', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, cxLancamento, finanLancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Caixa_TransferenciaConta(cxLancamento:Cx_Lancamento, finanLancamento:Finan_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Caixa_TransferenciaConta', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, cxLancamento, finanLancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Caixa_Entrada(cxLancamento:Cx_Lancamento, fRetorno:Function=null):void
        {
            ro.Invoca('Caixa_Entrada', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, cxLancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Caixa_Abertura(data:String, valorAbertura:Number, abertoPeloSistema:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('Caixa_Abertura', [Sessao.unica.idCorp, Sessao.unica.idEmp, data, valorAbertura, abertoPeloSistema], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Finan_TituloItem    //AS=Finan_TituloItem
        public function Finan_DuplicataIpressa(finanTituloItem:Finan_TituloItem, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_DuplicataIpressa', [Sessao.unica.idCorp, finanTituloItem], function(r:Finan_TituloItem):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ContasPagar_Novo(finanTitulo:Finan_Titulo, listaFinanTituloItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ContasPagar_Novo', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, finanTitulo, listaFinanTituloItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_ContasPagar_Baixa(listaFinanTituloItem:Array, finanConta:Finan_Conta, tipoPagamento:String, idCentroCusto:Number, idTipoLancamento:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_ContasPagar_Baixa', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, listaFinanTituloItem, finanConta, tipoPagamento, idCentroCusto, idTipoLancamento], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finan_DuplicataEscrevePdf(listaFinanTitulo:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Finan_DuplicataEscrevePdf', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaFinanTitulo], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarXml(mov_dados:Mov, mov_nfe_dados:MovNFE, mov_nfe_vei_dados:MovNfeVeiculo, mItens_dados:Array, fRetorno:Function=null):void
        {
            ro.Invoca('GerarXml', [Sessao.unica.idCorp, Sessao.unica.idEmp, mov_dados, mov_nfe_dados, mov_nfe_vei_dados, mItens_dados], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function SalvaMovNFE(idMov:Number, movnfeDados:MovNFE, fRetorno:Function=null):void
        {
            ro.Invoca('SalvaMovNFE', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov, movnfeDados], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarTXT(mov_dados:Mov, mov_nfe_dados:MovNFE, mov_nfe_vei_dados:MovNfeVeiculo, mItens_dados:Array, fRetorno:Function=null):void
        {
            ro.Invoca('GerarTXT', [Sessao.unica.idCorp, Sessao.unica.idEmp, mov_dados, mov_nfe_dados, mov_nfe_vei_dados, mItens_dados], function(r:Array):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function numNFE(fRetorno:Function=null):void
        {
            ro.Invoca('numNFE', [Sessao.unica.idCorp, Sessao.unica.idEmp], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Usuario_NovoAtualizacao(clienteFuncionarioUsuario:ClienteFuncionarioUsuario, listClienteFuncionarioPermissoes:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Usuario_NovoAtualizacao', [Sessao.unica.idCorp, Sessao.unica.idEmp, clienteFuncionarioUsuario, listClienteFuncionarioPermissoes], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Temp_Salva(mov:Mov, movNfe:MovNFE, listaMovItens:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Temp_Salva', [Sessao.unica.idCorp, Sessao.unica.idClienteFuncionarioLogado, mov, movNfe, listaMovItens], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Temp_Deleta(fRetorno:Function=null):void
        {
            ro.Invoca('Temp_Deleta', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function ItemNovo(item:Item, iep:ItemEmpPreco, fRetorno:Function=null):void
        {
            ro.Invoca('ItemNovo', [Sessao.unica.idCorp, Sessao.unica.idEmp, item, iep], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function ItemAtualiza(item:Item, iep:ItemEmpPreco, iea:ItemEmpAliquotas, fRetorno:Function=null):void
        {
            ro.Invoca('ItemAtualiza', [Sessao.unica.idCorp, item, iep, iea], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemPrecoAjusteSecao(idSecao:Number, porcentagemAjuste:Number, fRetorno:Function=null):void
        {
            ro.Invoca('ItemPrecoAjusteSecao', [Sessao.unica.idCorp, Sessao.unica.idEmp, idSecao, porcentagemAjuste], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemPrecoAjusteMarca(idMarca:Number, porcentagemAjuste:Number, fRetorno:Function=null):void
        {
            ro.Invoca('ItemPrecoAjusteMarca', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMarca, porcentagemAjuste], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function EscreveEtiquetaPorMovEMB(idMov:Number, portaCOM:String, fRetorno:Function=null):void
        {
            ro.Invoca('EscreveEtiquetaPorMovEMB', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov, portaCOM], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function EscreveEtiquetaPorListaEMB(listaMovItem:Array, portaCOM:String, fRetorno:Function=null):void
        {
            ro.Invoca('EscreveEtiquetaPorListaEMB', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem, portaCOM], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function EscreveEtiquetaPorMovObraDensa(idMov:Number, portaCOM:String, fRetorno:Function=null):void
        {
            ro.Invoca('EscreveEtiquetaPorMovObraDensa', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov, portaCOM], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function EscreveEtiquetaPorListaObraDensa(listaMovItem:Array, portaCOM:String, fRetorno:Function=null):void
        {
            ro.Invoca('EscreveEtiquetaPorListaObraDensa', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem, portaCOM], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorMovSETE(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorMovSETE', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorListaSETE(listaMovItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorListaSETE', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorMovAntonietaCasa(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorMovAntonietaCasa', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorListaAntonietaCasa(listaMovItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorListaAntonietaCasa', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorMovWembley(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorMovWembley', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorListaWembley(listaMovItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorListaWembley', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorMovCostaAzul(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorMovCostaAzul', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorListaCostaAzul(listaMovItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorListaCostaAzul', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorMovModaMorena(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorMovModaMorena', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function escreveEtiquetaPorListaModaMorena(listaMovItem:Array, fRetorno:Function=null):void
        {
            ro.Invoca('escreveEtiquetaPorListaModaMorena', [Sessao.unica.idCorp, Sessao.unica.idEmp, listaMovItem], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function RelListaTelefone(fRetorno:Function=null):void
        {
            ro.Invoca('RelListaTelefone', [Sessao.unica.idCorp, Sessao.unica.idEmp], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function RelFichaCliente(fRetorno:Function=null):void
        {
            ro.Invoca('RelFichaCliente', [Sessao.unica.idCorp, Sessao.unica.idEmp], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function RelListaPrecos(idMarca:Number, fRetorno:Function=null):void
        {
            ro.Invoca('RelListaPrecos', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMarca], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function RelListagemBalanco(campoOrdenacao:String, fRetorno:Function=null):void
        {
            ro.Invoca('RelListagemBalanco', [Sessao.unica.idCorp, Sessao.unica.idEmp, campoOrdenacao], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function RelListaProdutoTributacao(idMarca:Number, fRetorno:Function=null):void
        {
            ro.Invoca('RelListaProdutoTributacao', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMarca], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Cad_Generico_Novos(classe:String, objetos:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Cad_Generico_Novos', [Sessao.unica.idCorp, classe, objetos], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Cad_Generico_Reseta(fRetorno:Function=null):void
        {
            ro.Invoca('Cad_Generico_Reseta', [Sessao.unica.idCorp], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function MovEscrevePdf(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('MovEscrevePdf', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function Mov_Salva(mov:Mov, carrinho:Array, valores:Array, ordemServico:OrdemServico, fRetorno:Function=null):void
        {
            ro.Invoca('Mov_Salva', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, mov, carrinho, valores, ordemServico], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Int32    //AS=Number
        public function Mov_Salva_EntradaRetorno(mov:Mov, carrinho:Array, valores:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Mov_Salva_EntradaRetorno', [Sessao.unica.idCorp, Sessao.unica.idEmp, Sessao.unica.idClienteFuncionarioLogado, mov, carrinho, valores], function(r:Number):void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{if(fRetorno!=null)fRetorno(r);});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Mov_Cancela(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Mov_Cancela', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov, Sessao.unica.idClienteFuncionarioLogado], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function RealizaAjusteEstoque(mov:Mov, fRetorno:Function=null):void
        {
            ro.Invoca('RealizaAjusteEstoque', [Sessao.unica.idCorp, Sessao.unica.idEmp, mov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function Cad_Secao_Atualizacoes(listaAtualizacao:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Cad_Secao_Atualizacoes', [Sessao.unica.idCorp, listaAtualizacao], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
        //ev.result    //CS=Void    //AS=Void
        public function GeraCarne(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('GeraCarne', [Sessao.unica.idCorp, idMov], function():void{Sessao.unica.nuvens.notificacoes.Lista_Notificacoes(function():void{fRetorno();});});
        }
    }
}
