package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Cx_Diario')]
    public final class Cx_Diario
    {
        public static function get CLASSE():String{return 'Cx_Diario';}
        public static function getCampos():Array{return['id','idEmp','data','valorAbertura','valorFechamento','totalEntradas','totalSaidas','saldo','situacao']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_data:String = 'data';
        public static var campo_valorAbertura:String = 'valorAbertura';
        public static var campo_valorFechamento:String = 'valorFechamento';
        public static var campo_totalEntradas:String = 'totalEntradas';
        public static var campo_totalSaidas:String = 'totalSaidas';
        public static var campo_saldo:String = 'saldo';
        public static var campo_situacao:String = 'situacao';
        public function Cx_Diario(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Cx_Diario.getCampos()){this[campo]=o[campo];}}
        public function clone():Cx_Diario{return new Cx_Diario(this);}
        public function toString():String
        {
            return '[Cx_Diario '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var data:String = '';
        public var valorAbertura:Number = 0;
        public var valorFechamento:Number = 0;
        public var totalEntradas:Number = 0;
        public var totalSaidas:Number = 0;
        public var saldo:Number = 0;
        public var situacao:String = 'aberto_pelo_sistema';
    }
}
