package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamFiltroTipoPagamento')]
    public final class ParamFiltroTipoPagamento
    {
        public static function get CLASSE():String{return 'ParamFiltroTipoPagamento';}
        public static function getCampos():Array{return['parcelas','portador','tipoDocumento']};
        
        public static var campo_parcelas:String = 'parcelas';
        public static var campo_portador:String = 'portador';
        public static var campo_tipoDocumento:String = 'tipoDocumento';
        public function clone():ParamFiltroTipoPagamento { return new ParamFiltroTipoPagamento(this); }
        public function ParamFiltroTipoPagamento(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var parcelas:Boolean = false;
        public var portador:Boolean = false;
        public var tipoDocumento:Boolean = false;
    }
}
