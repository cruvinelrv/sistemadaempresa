package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.MovItemEstoque')]
    public final class MovItemEstoque
    {
        public static function get CLASSE():String{return 'MovItemEstoque';}
        public static function getCampos():Array{return['id','idMovItem','idIEE','qtd','identificador','lote','codBarrasGrade','dtFab','dtVal','__iee']};
        
        public static var campo_id:String = 'id';
        public static var campo_idMovItem:String = 'idMovItem';
        public static var campo_idIEE:String = 'idIEE';
        public static var campo_qtd:String = 'qtd';
        public static var campo_identificador:String = 'identificador';
        public static var campo_lote:String = 'lote';
        public static var campo_codBarrasGrade:String = 'codBarrasGrade';
        public static var campo_dtFab:String = 'dtFab';
        public static var campo_dtVal:String = 'dtVal';
        public static var campo___iee:String = '__iee';
        public function MovItemEstoque(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in MovItemEstoque.getCampos()){this[campo]=o[campo];}}
        public function clone():MovItemEstoque{return new MovItemEstoque(this);}
        public function toString():String
        {
            return '[MovItemEstoque '+id+']';
        }
        public var id:Number = 0;
        public var idMovItem:Number = 0;
        public var idIEE:Number = 0;
        public var qtd:Number = 0;
        public var identificador:String = '';
        public var lote:String = '';
        public var codBarrasGrade:String = '';
        public var dtFab:String = '';
        public var dtVal:String = '';
        public var __iee:ItemEmpEstoque = null;
    }
}
