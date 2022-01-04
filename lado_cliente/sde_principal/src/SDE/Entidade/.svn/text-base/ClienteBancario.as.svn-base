package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteBancario')]
    public final class ClienteBancario
    {
        public static function get CLASSE():String{return 'ClienteBancario';}
        public static function getCampos():Array{return['id','idCliente','isDeletado']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_isDeletado:String = 'isDeletado';
        public function ClienteBancario(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteBancario.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteBancario{return new ClienteBancario(this);}
        public function toString():String
        {
            return '[ClienteBancario '+id+']';
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var isDeletado:Boolean = false;
    }
}
