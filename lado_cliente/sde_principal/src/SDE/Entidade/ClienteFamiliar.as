package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteFamiliar')]
    public final class ClienteFamiliar
    {
        public static function get CLASSE():String{return 'ClienteFamiliar';}
        public static function getCampos():Array{return['id','idCliente','key','nome','data','fone','obs','ehDependente','ehAutorizado','isDeletado']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_key:String = 'key';
        public static var campo_nome:String = 'nome';
        public static var campo_data:String = 'data';
        public static var campo_fone:String = 'fone';
        public static var campo_obs:String = 'obs';
        public static var campo_ehDependente:String = 'ehDependente';
        public static var campo_ehAutorizado:String = 'ehAutorizado';
        public static var campo_isDeletado:String = 'isDeletado';
        public function ClienteFamiliar(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteFamiliar.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteFamiliar{return new ClienteFamiliar(this);}
        public function toString():String
        {
            return '[ClienteFamiliar '+id+']';
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var key:String = '';
        public var nome:String = '';
        public var data:String = '';
        public var fone:String = '';
        public var obs:String = '';
        public var ehDependente:Boolean = false;
        public var ehAutorizado:Boolean = false;
        public var isDeletado:Boolean = false;
    }
}
