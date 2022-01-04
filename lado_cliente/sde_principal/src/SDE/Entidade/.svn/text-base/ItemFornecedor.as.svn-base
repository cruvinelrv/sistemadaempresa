package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ItemFornecedor')]
    public final class ItemFornecedor
    {
        public static function get CLASSE():String{return 'ItemFornecedor';}
        public static function getCampos():Array{return['id','idItem','idCliente','isDeletado','nome']};
        
        public static var campo_id:String = 'id';
        public static var campo_idItem:String = 'idItem';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_isDeletado:String = 'isDeletado';
        public static var campo_nome:String = 'nome';
        public function ItemFornecedor(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ItemFornecedor.getCampos()){this[campo]=o[campo];}}
        public function clone():ItemFornecedor{return new ItemFornecedor(this);}
        public function toString():String
        {
            return '[ItemFornecedor '+id+']';
        }
        public var id:Number = 0;
        public var idItem:Number = 0;
        public var idCliente:Number = 0;
        public var isDeletado:Boolean = false;
        public var nome:String = '';
    }
}
