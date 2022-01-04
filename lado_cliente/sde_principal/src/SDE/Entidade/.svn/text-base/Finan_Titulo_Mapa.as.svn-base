package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_Titulo_Mapa')]
    public final class Finan_Titulo_Mapa
    {
        public static function get CLASSE():String{return 'Finan_Titulo_Mapa';}
        public static function getCampos():Array{return['id']};
        
        public static var campo_id:String = 'id';
        public function clone():* { return new Finan_Titulo_Mapa(this); }
        public function injeta(o:*):void{for each (var campo:String in Finan_Titulo_Mapa.getCampos()){this[campo]=o[campo];}}        public function Finan_Titulo_Mapa(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var id:Number = 0;
    }
}
