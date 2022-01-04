package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Incremento')]
    public final class Incremento
    {
        public static function get CLASSE():String{return 'Incremento';}
        public static function getCampos():Array{return['id','valorUltimaID','entidade']};
        
        public static var campo_id:String = 'id';
        public static var campo_valorUltimaID:String = 'valorUltimaID';
        public static var campo_entidade:String = 'entidade';
        public function clone():* { return new Incremento(this); }
        public function injeta(o:*):void{for each (var campo:String in Incremento.getCampos()){this[campo]=o[campo];}}        public function Incremento(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var id:Number = 0;
        public var valorUltimaID:Number = 0;
        public var entidade:String = '';
    }
}
