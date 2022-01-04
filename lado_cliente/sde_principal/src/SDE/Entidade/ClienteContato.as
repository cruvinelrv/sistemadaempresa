package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteContato')]
    public final class ClienteContato
    {
        public static function get CLASSE():String{return 'ClienteContato';}
        public static function getCampos():Array{return['id','idCliente','campo','valor','obs','isDeletado','tipo']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_campo:String = 'campo';
        public static var campo_valor:String = 'valor';
        public static var campo_obs:String = 'obs';
        public static var campo_isDeletado:String = 'isDeletado';
        public static var campo_tipo:String = 'tipo';
        public function ClienteContato(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteContato.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteContato{return new ClienteContato(this);}
        public function toString():String
        {
            return '[ClienteContato '+id+']';
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var campo:String = '';
        public var valor:String = '';
        public var obs:String = '';
        public var isDeletado:Boolean = false;
        public var tipo:String = 'fone_fixo';
    }
}
