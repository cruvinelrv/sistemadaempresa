package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.OrdemServico')]
    public final class OrdemServico
    {
        public static function get CLASSE():String{return 'OrdemServico';}
        public static function getCampos():Array{return['id','idOperacao','idTransacao','idEmp','idCliente','idClienteFuncionarioLogado','idOrdemServicoTipo','idMovFinalizacao','numOS','descricao','obs','cliente_nome','cliente_cpf','cliente_contato','cliente_endereco_cobranca','veiculo','placa','kilometragem','numMotor','maquina','implAgricola','equipamento','numSerie','servico','defeitoReclamado','defeiroConstatado','isContratado','vlrItensInicial','vlrItensFinal','vlrAcrescimo','vlrTotal','dthrLancamento','dthrInicio','dthrPrevisao','dthrConclusao','dtOrdemServicoTicks','dtInicioTicks','dtPrevisaoTicks','dtConclusaoTicks','status','__oSItens']};
        
        public static var campo_id:String = 'id';
        public static var campo_idOperacao:String = 'idOperacao';
        public static var campo_idTransacao:String = 'idTransacao';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_idOrdemServicoTipo:String = 'idOrdemServicoTipo';
        public static var campo_idMovFinalizacao:String = 'idMovFinalizacao';
        public static var campo_numOS:String = 'numOS';
        public static var campo_descricao:String = 'descricao';
        public static var campo_obs:String = 'obs';
        public static var campo_cliente_nome:String = 'cliente_nome';
        public static var campo_cliente_cpf:String = 'cliente_cpf';
        public static var campo_cliente_contato:String = 'cliente_contato';
        public static var campo_cliente_endereco_cobranca:String = 'cliente_endereco_cobranca';
        public static var campo_veiculo:String = 'veiculo';
        public static var campo_placa:String = 'placa';
        public static var campo_kilometragem:String = 'kilometragem';
        public static var campo_numMotor:String = 'numMotor';
        public static var campo_maquina:String = 'maquina';
        public static var campo_implAgricola:String = 'implAgricola';
        public static var campo_equipamento:String = 'equipamento';
        public static var campo_numSerie:String = 'numSerie';
        public static var campo_servico:String = 'servico';
        public static var campo_defeitoReclamado:String = 'defeitoReclamado';
        public static var campo_defeiroConstatado:String = 'defeiroConstatado';
        public static var campo_isContratado:String = 'isContratado';
        public static var campo_vlrItensInicial:String = 'vlrItensInicial';
        public static var campo_vlrItensFinal:String = 'vlrItensFinal';
        public static var campo_vlrAcrescimo:String = 'vlrAcrescimo';
        public static var campo_vlrTotal:String = 'vlrTotal';
        public static var campo_dthrLancamento:String = 'dthrLancamento';
        public static var campo_dthrInicio:String = 'dthrInicio';
        public static var campo_dthrPrevisao:String = 'dthrPrevisao';
        public static var campo_dthrConclusao:String = 'dthrConclusao';
        public static var campo_dtOrdemServicoTicks:String = 'dtOrdemServicoTicks';
        public static var campo_dtInicioTicks:String = 'dtInicioTicks';
        public static var campo_dtPrevisaoTicks:String = 'dtPrevisaoTicks';
        public static var campo_dtConclusaoTicks:String = 'dtConclusaoTicks';
        public static var campo_status:String = 'status';
        public static var campo___oSItens:String = '__oSItens';
        public function OrdemServico(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in OrdemServico.getCampos()){this[campo]=o[campo];}}
        public function clone():OrdemServico{return new OrdemServico(this);}
        public function toString():String
        {
            return '[OrdemServico '+id+']';
        }
        public var id:Number = 0;
        public var idOperacao:Number = 0;
        public var idTransacao:Number = 0;
        public var idEmp:Number = 0;
        public var idCliente:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var idOrdemServicoTipo:Number = 0;
        public var idMovFinalizacao:Number = 0;
        public var numOS:String = '';
        public var descricao:String = '';
        public var obs:String = '';
        public var cliente_nome:String = '';
        public var cliente_cpf:String = '';
        public var cliente_contato:String = '';
        public var cliente_endereco_cobranca:String = '';
        public var veiculo:String = '';
        public var placa:String = '';
        public var kilometragem:String = '';
        public var numMotor:String = '';
        public var maquina:String = '';
        public var implAgricola:String = '';
        public var equipamento:String = '';
        public var numSerie:String = '';
        public var servico:String = '';
        public var defeitoReclamado:String = '';
        public var defeiroConstatado:String = '';
        public var isContratado:Boolean = false;
        public var vlrItensInicial:Number = 0;
        public var vlrItensFinal:Number = 0;
        public var vlrAcrescimo:Number = 0;
        public var vlrTotal:Number = 0;
        public var dthrLancamento:String = '';
        public var dthrInicio:String = '';
        public var dthrPrevisao:String = '';
        public var dthrConclusao:String = '';
        public var dtOrdemServicoTicks:Number = 0;
        public var dtInicioTicks:Number = 0;
        public var dtPrevisaoTicks:Number = 0;
        public var dtConclusaoTicks:Number = 0;
        public var status:String = 'nao_iniciada';
        public var __oSItens:Array = null;
    }
}
