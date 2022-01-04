package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.CFOP')]
    public final class CFOP
    {
        public static function get CLASSE():String{return 'CFOP';}
        public static function getCampos():Array{return['id','codigo','descricao']};
        
        public static var campo_id:String = 'id';
        public static var campo_codigo:String = 'codigo';
        public static var campo_descricao:String = 'descricao';
        public function CFOP(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in CFOP.getCampos()){this[campo]=o[campo];}}
        public function clone():CFOP{return new CFOP(this);}
        public function toString():String
        {
            return '[CFOP '+id+']';
        }
        public var id:Number = 0;
        public var codigo:String = '';
        public var descricao:String = '';
    }
}
