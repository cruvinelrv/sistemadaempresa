package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ItemEmp')]
    public final class ItemEmp
    {
        public static function get CLASSE():String{return 'ItemEmp';}
        public static function getCampos():Array{return['id','idItem','idEmp','__aliquotas','__preco']};
        
        public static var campo_id:String = 'id';
        public static var campo_idItem:String = 'idItem';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo___aliquotas:String = '__aliquotas';
        public static var campo___preco:String = '__preco';
        public function ItemEmp(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ItemEmp.getCampos()){this[campo]=o[campo];}}
        public function clone():ItemEmp{return new ItemEmp(this);}
        public function toString():String
        {
            return '[ItemEmp '+id+']';
        }
        public var id:Number = 0;
        public var idItem:Number = 0;
        public var idEmp:Number = 0;
        public var __aliquotas:ItemEmpAliquotas = null;
        public var __preco:ItemEmpPreco = null;
    }
}
