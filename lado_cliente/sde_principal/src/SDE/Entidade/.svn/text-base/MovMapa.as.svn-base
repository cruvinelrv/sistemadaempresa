package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.MovMapa')]
    public final class MovMapa
    {
        public static function get CLASSE():String{return 'MovMapa';}
        public static function getCampos():Array{return['id']};
        
        public static var campo_id:String = 'id';
        public function clone():* { return new MovMapa(this); }
        public function injeta(o:*):void{for each (var campo:String in MovMapa.getCampos()){this[campo]=o[campo];}}        public function MovMapa(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var id:Number = 0;
    }
}
