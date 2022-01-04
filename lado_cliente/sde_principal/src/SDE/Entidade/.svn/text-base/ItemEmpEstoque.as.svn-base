package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ItemEmpEstoque')]
    public final class ItemEmpEstoque
    {
        public static function get CLASSE():String{return 'ItemEmpEstoque';}
        public static function getCampos():Array{return['id','idEmp','idItem','identificador','codBarras','codBarrasGrade','lote','dtFab','dtVal','qtd','qtdReserva','qtdMin','qtdMax','custo']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idItem:String = 'idItem';
        public static var campo_identificador:String = 'identificador';
        public static var campo_codBarras:String = 'codBarras';
        public static var campo_codBarrasGrade:String = 'codBarrasGrade';
        public static var campo_lote:String = 'lote';
        public static var campo_dtFab:String = 'dtFab';
        public static var campo_dtVal:String = 'dtVal';
        public static var campo_qtd:String = 'qtd';
        public static var campo_qtdReserva:String = 'qtdReserva';
        public static var campo_qtdMin:String = 'qtdMin';
        public static var campo_qtdMax:String = 'qtdMax';
        public static var campo_custo:String = 'custo';
        public function ItemEmpEstoque(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ItemEmpEstoque.getCampos()){this[campo]=o[campo];}}
        public function clone():ItemEmpEstoque{return new ItemEmpEstoque(this);}
        public function toString():String
        {
            return '[ItemEmpEstoque '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idItem:Number = 0;
        public var identificador:String = '';
        public var codBarras:String = '';
        public var codBarrasGrade:String = '';
        public var lote:String = '';
        public var dtFab:String = '';
        public var dtVal:String = '';
        public var qtd:Number = 0;
        public var qtdReserva:Number = 0;
        public var qtdMin:Number = 0;
        public var qtdMax:Number = 0;
        public var custo:Number = 0;
    }
}
