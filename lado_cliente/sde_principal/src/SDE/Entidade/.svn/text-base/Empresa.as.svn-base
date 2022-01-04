package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Empresa')]
    public final class Empresa
    {
        public static function get CLASSE():String{return 'Empresa';}
        public static function getCampos():Array{return['id','idCliente','idClienteAdmin','usuario','__cliente','__clienteAdmin','isOptanteSimplesNacional']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_idClienteAdmin:String = 'idClienteAdmin';
        public static var campo_usuario:String = 'usuario';
        public static var campo___cliente:String = '__cliente';
        public static var campo___clienteAdmin:String = '__clienteAdmin';
        public static var campo_isOptanteSimplesNacional:String = 'isOptanteSimplesNacional';
        public function Empresa(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Empresa.getCampos()){this[campo]=o[campo];}}
        public function clone():Empresa{return new Empresa(this);}
        public function toString():String
        {
            return usuario;
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var idClienteAdmin:Number = 0;
        public var usuario:String = '';
        public var __cliente:Cliente = null;
        public var __clienteAdmin:Cliente = null;
        public var isOptanteSimplesNacional:Boolean = false;
    }
}
