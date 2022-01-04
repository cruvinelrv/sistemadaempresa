package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Balanco')]
    public final class Balanco
    {
        public static function get CLASSE():String{return 'Balanco';}
        public static function getCampos():Array{return['id','idOperacao','idTransacao','nome','dthrInicio','dthrFim','situacao']};
        
        public static var campo_id:String = 'id';
        public static var campo_idOperacao:String = 'idOperacao';
        public static var campo_idTransacao:String = 'idTransacao';
        public static var campo_nome:String = 'nome';
        public static var campo_dthrInicio:String = 'dthrInicio';
        public static var campo_dthrFim:String = 'dthrFim';
        public static var campo_situacao:String = 'situacao';
        public function Balanco(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Balanco.getCampos()){this[campo]=o[campo];}}
        public function clone():Balanco{return new Balanco(this);}
        public function toString():String
        {
            return '[Balanco '+id+']';
        }
        public var id:Number = 0;
        public var idOperacao:Number = 0;
        public var idTransacao:Number = 0;
        public var nome:String = '';
        public var dthrInicio:String = '';
        public var dthrFim:String = '';
        public var situacao:String = 'em_andamento';
    }
}
