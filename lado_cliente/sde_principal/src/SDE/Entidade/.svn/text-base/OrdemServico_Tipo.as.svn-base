package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.OrdemServico_Tipo')]
    public final class OrdemServico_Tipo
    {
        public static function get CLASSE():String{return 'OrdemServico_Tipo';}
        public static function getCampos():Array{return['id','idClienteFuncionarioLogado','nome','veiculo','placa','kilometragem','numMotor','maquina','implAgricola','equipamento','numSerie','servico','defeitoReclamado','defeitoConstatado']};
        
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_nome:String = 'nome';
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
        public static var campo_defeitoConstatado:String = 'defeitoConstatado';
        public function OrdemServico_Tipo(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in OrdemServico_Tipo.getCampos()){this[campo]=o[campo];}}
        public function clone():OrdemServico_Tipo{return new OrdemServico_Tipo(this);}
        public function toString():String
        {
            return '[OrdemServico_Tipo '+id+']';
        }
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var nome:String = '';
        public var veiculo:Boolean = false;
        public var placa:Boolean = false;
        public var kilometragem:Boolean = false;
        public var numMotor:Boolean = false;
        public var maquina:Boolean = false;
        public var implAgricola:Boolean = false;
        public var equipamento:Boolean = false;
        public var numSerie:Boolean = false;
        public var servico:Boolean = false;
        public var defeitoReclamado:Boolean = false;
        public var defeitoConstatado:Boolean = false;
    }
}
