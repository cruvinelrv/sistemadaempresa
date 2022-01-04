package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.BalancoItem')]
    public final class BalancoItem
    {
        public static function get CLASSE():String{return 'BalancoItem';}
        public static function getCampos():Array{return['id','idOperacao','idTransacao','idBalanco','idIEE','idItem','qtdLancada','qtdAnterior','custo','compra','venda','item_nome','estoque_identificador','rfUnica','rfAuxiliar']};
        
        public static var campo_id:String = 'id';
        public static var campo_idOperacao:String = 'idOperacao';
        public static var campo_idTransacao:String = 'idTransacao';
        public static var campo_idBalanco:String = 'idBalanco';
        public static var campo_idIEE:String = 'idIEE';
        public static var campo_idItem:String = 'idItem';
        public static var campo_qtdLancada:String = 'qtdLancada';
        public static var campo_qtdAnterior:String = 'qtdAnterior';
        public static var campo_custo:String = 'custo';
        public static var campo_compra:String = 'compra';
        public static var campo_venda:String = 'venda';
        public static var campo_item_nome:String = 'item_nome';
        public static var campo_estoque_identificador:String = 'estoque_identificador';
        public static var campo_rfUnica:String = 'rfUnica';
        public static var campo_rfAuxiliar:String = 'rfAuxiliar';
        public function BalancoItem(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in BalancoItem.getCampos()){this[campo]=o[campo];}}
        public function clone():BalancoItem{return new BalancoItem(this);}
        public function toString():String
        {
            return '[BalancoItem '+id+']';
        }
        public var id:Number = 0;
        public var idOperacao:Number = 0;
        public var idTransacao:Number = 0;
        public var idBalanco:Number = 0;
        public var idIEE:Number = 0;
        public var idItem:Number = 0;
        public var qtdLancada:Number = 0;
        public var qtdAnterior:Number = 0;
        public var custo:Number = 0;
        public var compra:Number = 0;
        public var venda:Number = 0;
        public var item_nome:String = '';
        public var estoque_identificador:String = '';
        public var rfUnica:String = '';
        public var rfAuxiliar:String = '';
    }
}
