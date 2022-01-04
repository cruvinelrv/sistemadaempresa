package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.IBGEMunicipio')]
    public final class IBGEMunicipio
    {
        public static function get CLASSE():String{return 'IBGEMunicipio';}
        public static function getCampos():Array{return['id','nome','codigo','codigoEstado']};
        
        public static var campo_id:String = 'id';
        public static var campo_nome:String = 'nome';
        public static var campo_codigo:String = 'codigo';
        public static var campo_codigoEstado:String = 'codigoEstado';
        public function IBGEMunicipio(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in IBGEMunicipio.getCampos()){this[campo]=o[campo];}}
        public function clone():IBGEMunicipio{return new IBGEMunicipio(this);}
        public function toString():String
        {
            return '[IBGEMunicipio '+id+']';
        }
        public var id:Number = 0;
        public var nome:String = '';
        public var codigo:String = '';
        public var codigoEstado:String = '';
    }
}
