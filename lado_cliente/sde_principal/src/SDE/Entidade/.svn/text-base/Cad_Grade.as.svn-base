package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Cad_Grade')]
    public final class Cad_Grade
    {
        public static function get CLASSE():String{return 'Cad_Grade';}
        public static function getCampos():Array{return['id','idClienteFuncionarioLogado','mae','mae_rf','filha','filha_rf','__orderBy']};
        
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_mae:String = 'mae';
        public static var campo_mae_rf:String = 'mae_rf';
        public static var campo_filha:String = 'filha';
        public static var campo_filha_rf:String = 'filha_rf';
        public static var campo___orderBy:String = '__orderBy';
        public function Cad_Grade(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Cad_Grade.getCampos()){this[campo]=o[campo];}}
        public function clone():Cad_Grade{return new Cad_Grade(this);}
        public function toString():String
        {
            return '[Cad_Grade '+id+']';
        }
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var mae:String = '';
        public var mae_rf:String = '';
        public var filha:String = '';
        public var filha_rf:String = '';
        public var __orderBy:String = '';
    }
}
