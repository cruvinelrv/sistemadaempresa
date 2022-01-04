package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_TipoLancamento')]
    public final class Finan_TipoLancamento
    {
        public static function get CLASSE():String{return 'Finan_TipoLancamento';}
        public static function getCampos():Array{return['idEmp','id','idClienteFuncionarioLogado','codigoTipoLancamento','codigoGrupoLancamento','nomeGrupoLancamento','nomeTipoLancamento','codigo']};
        
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_codigoTipoLancamento:String = 'codigoTipoLancamento';
        public static var campo_codigoGrupoLancamento:String = 'codigoGrupoLancamento';
        public static var campo_nomeGrupoLancamento:String = 'nomeGrupoLancamento';
        public static var campo_nomeTipoLancamento:String = 'nomeTipoLancamento';
        public static var campo_codigo:String = 'codigo';
        public function Finan_TipoLancamento(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_TipoLancamento.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_TipoLancamento{return new Finan_TipoLancamento(this);}
        public function toString():String
        {
            return nomeTipoLancamento;
        }
        public var idEmp:Number = 0;
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var codigoTipoLancamento:Number = 0;
        public var codigoGrupoLancamento:Number = 0;
        public var nomeGrupoLancamento:String = '';
        public var nomeTipoLancamento:String = '';
        public var codigo:String = '';
    }
}
