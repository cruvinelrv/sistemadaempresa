package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamLoadMov')]
    public final class ParamLoadMov
    {
        public static function get CLASSE():String{return 'ParamLoadMov';}
        public static function getCampos():Array{return['ignora','movItens','movValores','itens','clientes']};
        
        public static var campo_ignora:String = 'ignora';
        public static var campo_movItens:String = 'movItens';
        public static var campo_movValores:String = 'movValores';
        public static var campo_itens:String = 'itens';
        public static var campo_clientes:String = 'clientes';
        public function clone():ParamLoadMov { return new ParamLoadMov(this); }
        public function ParamLoadMov(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var ignora:Boolean = false;
        public var movItens:Boolean = false;
        public var movValores:Boolean = false;
        public var itens:Boolean = false;
        public var clientes:Boolean = false;
    }
}
