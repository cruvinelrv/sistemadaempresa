package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ItemEmpPreco')]
    public final class ItemEmpPreco
    {
        public static function get CLASSE():String{return 'ItemEmpPreco';}
        public static function getCampos():Array{return['id','idEmp','idItem','compra','custo','venda','margemLucro','pctComissao','descontoMaximo','atacado']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idItem:String = 'idItem';
        public static var campo_compra:String = 'compra';
        public static var campo_custo:String = 'custo';
        public static var campo_venda:String = 'venda';
        public static var campo_margemLucro:String = 'margemLucro';
        public static var campo_pctComissao:String = 'pctComissao';
        public static var campo_descontoMaximo:String = 'descontoMaximo';
        public static var campo_atacado:String = 'atacado';
        public function ItemEmpPreco(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ItemEmpPreco.getCampos()){this[campo]=o[campo];}}
        public function clone():ItemEmpPreco{return new ItemEmpPreco(this);}
        public function toString():String
        {
            return '[ItemEmpPreco '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idItem:Number = 0;
        public var compra:Number = 0;
        public var custo:Number = 0;
        public var venda:Number = 0;
        public var margemLucro:Number = 0;
        public var pctComissao:Number = 0;
        public var descontoMaximo:Number = 0;
        public var atacado:Number = 0;
    }
}
