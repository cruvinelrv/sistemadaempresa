package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamFiltroMov')]
    public final class ParamFiltroMov
    {
        public static function get CLASSE():String{return 'ParamFiltroMov';}
        public static function getCampos():Array{return['dtInicial','dtFinal','tipos','idCliente','idMov']};
        
        public static var campo_dtInicial:String = 'dtInicial';
        public static var campo_dtFinal:String = 'dtFinal';
        public static var campo_tipos:String = 'tipos';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_idMov:String = 'idMov';
        public function clone():ParamFiltroMov { return new ParamFiltroMov(this); }
        public function ParamFiltroMov(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var dtInicial:String = '';
        public var dtFinal:String = '';
        public var tipos:String = '';
        public var idCliente:Number = 0;
        public var idMov:Number = 0;
    }
}
