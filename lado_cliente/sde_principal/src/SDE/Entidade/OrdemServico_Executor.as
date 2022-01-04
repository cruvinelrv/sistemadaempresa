package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.OrdemServico_Executor')]
    public final class OrdemServico_Executor
    {
        public static function get CLASSE():String{return 'OrdemServico_Executor';}
        public static function getCampos():Array{return['id','idOrdemServico','idOrdemServicoItem','idClienteExecutor','dthrInicio','dthrPrevisao','dthrConclusao','dtInicioTicks','dtPrevisaoTicks','dtConclusaoTicks','status','__removaMe','__executor']};
        
        public static var campo_id:String = 'id';
        public static var campo_idOrdemServico:String = 'idOrdemServico';
        public static var campo_idOrdemServicoItem:String = 'idOrdemServicoItem';
        public static var campo_idClienteExecutor:String = 'idClienteExecutor';
        public static var campo_dthrInicio:String = 'dthrInicio';
        public static var campo_dthrPrevisao:String = 'dthrPrevisao';
        public static var campo_dthrConclusao:String = 'dthrConclusao';
        public static var campo_dtInicioTicks:String = 'dtInicioTicks';
        public static var campo_dtPrevisaoTicks:String = 'dtPrevisaoTicks';
        public static var campo_dtConclusaoTicks:String = 'dtConclusaoTicks';
        public static var campo_status:String = 'status';
        public static var campo___removaMe:String = '__removaMe';
        public static var campo___executor:String = '__executor';
        public function OrdemServico_Executor(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in OrdemServico_Executor.getCampos()){this[campo]=o[campo];}}
        public function clone():OrdemServico_Executor{return new OrdemServico_Executor(this);}
        public function toString():String
        {
            return '[OrdemServico_Executor '+id+']';
        }
        public var id:Number = 0;
        public var idOrdemServico:Number = 0;
        public var idOrdemServicoItem:Number = 0;
        public var idClienteExecutor:Number = 0;
        public var dthrInicio:String = '';
        public var dthrPrevisao:String = '';
        public var dthrConclusao:String = '';
        public var dtInicioTicks:Number = 0;
        public var dtPrevisaoTicks:Number = 0;
        public var dtConclusaoTicks:Number = 0;
        public var status:String = 'nao_iniciada';
        public var __removaMe:Boolean = false;
        public var __executor:Cliente = null;
    }
}
