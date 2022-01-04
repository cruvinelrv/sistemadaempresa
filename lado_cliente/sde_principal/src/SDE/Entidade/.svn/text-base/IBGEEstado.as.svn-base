package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.IBGEEstado')]
    public final class IBGEEstado
    {
        public static function get CLASSE():String{return 'IBGEEstado';}
        public static function getCampos():Array{return['id','nome','codigo','sigla']};
        
        public static var campo_id:String = 'id';
        public static var campo_nome:String = 'nome';
        public static var campo_codigo:String = 'codigo';
        public static var campo_sigla:String = 'sigla';
        public function IBGEEstado(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in IBGEEstado.getCampos()){this[campo]=o[campo];}}
        public function clone():IBGEEstado{return new IBGEEstado(this);}
        public function toString():String
        {
            return '[IBGEEstado '+id+']';
        }
        public var id:Number = 0;
        public var nome:String = '';
        public var codigo:String = '';
        public var sigla:String = '';
    }
}
