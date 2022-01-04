package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Corporacao')]
    public final class Corporacao
    {
        public static function get CLASSE():String{return 'Corporacao';}
        public static function getCampos():Array{return['id','nome']};
        
        public static var campo_id:String = 'id';
        public static var campo_nome:String = 'nome';
        public function Corporacao(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Corporacao.getCampos()){this[campo]=o[campo];}}
        public function clone():Corporacao{return new Corporacao(this);}
        public function toString():String
        {
            return '[Corporacao '+id+']';
        }
        public var id:Number = 0;
        public var nome:String = '';
    }
}
