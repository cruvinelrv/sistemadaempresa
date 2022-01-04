package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteFuncionarioUsuario')]
    public final class ClienteFuncionarioUsuario
    {
        public static function get CLASSE():String{return 'ClienteFuncionarioUsuario';}
        public static function getCampos():Array{return['id','idEmp','idCliente','usuarioTecnico','login','senha']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_usuarioTecnico:String = 'usuarioTecnico';
        public static var campo_login:String = 'login';
        public static var campo_senha:String = 'senha';
        public function ClienteFuncionarioUsuario(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteFuncionarioUsuario.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteFuncionarioUsuario{return new ClienteFuncionarioUsuario(this);}
        public function toString():String
        {
            return '[ClienteFuncionarioUsuario '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idCliente:Number = 0;
        public var usuarioTecnico:Boolean = false;
        public var login:String = '';
        public var senha:String = '';
    }
}
