package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_TipoPagamento_Parcela')]
    public final class Finan_TipoPagamento_Parcela
    {
        public static function get CLASSE():String{return 'Finan_TipoPagamento_Parcela';}
        public static function getCampos():Array{return['id','idTipoPagamento','numParcela','dias','porcentagem','taxaJuro','taxaMultaDiaria']};
        
        public static var campo_id:String = 'id';
        public static var campo_idTipoPagamento:String = 'idTipoPagamento';
        public static var campo_numParcela:String = 'numParcela';
        public static var campo_dias:String = 'dias';
        public static var campo_porcentagem:String = 'porcentagem';
        public static var campo_taxaJuro:String = 'taxaJuro';
        public static var campo_taxaMultaDiaria:String = 'taxaMultaDiaria';
        public function Finan_TipoPagamento_Parcela(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_TipoPagamento_Parcela.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_TipoPagamento_Parcela{return new Finan_TipoPagamento_Parcela(this);}
        public function toString():String
        {
            return '[Finan_TipoPagamento_Parcela '+id+']';
        }
        public var id:Number = 0;
        public var idTipoPagamento:Number = 0;
        public var numParcela:Number = 0;
        public var dias:Number = 0;
        public var porcentagem:Number = 0;
        public var taxaJuro:Number = 0;
        public var taxaMultaDiaria:Number = 0;
    }
}
