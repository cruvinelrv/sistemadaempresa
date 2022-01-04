package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Max')]
    public final class Max
    {
        public static function get CLASSE():String{return 'Max';}
        public static function getCampos():Array{return['idLogin','idCorporacao','idCFOP','idEstado','idMunicipio','idLoginEmpresa']};
        
        public static var campo_idLogin:String = 'idLogin';
        public static var campo_idCorporacao:String = 'idCorporacao';
        public static var campo_idCFOP:String = 'idCFOP';
        public static var campo_idEstado:String = 'idEstado';
        public static var campo_idMunicipio:String = 'idMunicipio';
        public static var campo_idLoginEmpresa:String = 'idLoginEmpresa';
        public function clone():* { return new Max(this); }
        public function injeta(o:*):void{for each (var campo:String in Max.getCampos()){this[campo]=o[campo];}}        public function Max(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var idLogin:Number = 0;
        public var idCorporacao:Number = 0;
        public var idCFOP:Number = 0;
        public var idEstado:Number = 0;
        public var idMunicipio:Number = 0;
        public var idLoginEmpresa:Number = 0;
    }
}
