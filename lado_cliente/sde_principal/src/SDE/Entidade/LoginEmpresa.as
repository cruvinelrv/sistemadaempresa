package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.LoginEmpresa')]
    public final class LoginEmpresa
    {
        public static function get CLASSE():String{return 'LoginEmpresa';}
        public static function getCampos():Array{return['id','idCorp','idEmp','empresa']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCorp:String = 'idCorp';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_empresa:String = 'empresa';
        public function LoginEmpresa(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in LoginEmpresa.getCampos()){this[campo]=o[campo];}}
        public function clone():LoginEmpresa{return new LoginEmpresa(this);}
        public function toString():String
        {
            return '[LoginEmpresa '+id+']';
        }
        public var id:Number = 0;
        public var idCorp:Number = 0;
        public var idEmp:Number = 0;
        public var empresa:String = '';
    }
}
