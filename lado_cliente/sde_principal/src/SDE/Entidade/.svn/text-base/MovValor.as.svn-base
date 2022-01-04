package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.MovValor')]
    public final class MovValor
    {
        public static function get CLASSE():String{return 'MovValor';}
        public static function getCampos():Array{return['id','idMov','idCaixa','idCaixaDia','qtdParcelas','valor','dtPrimeiraParcela','dtPrimeiraParcelaTicks','especie']};
        
        public static var campo_id:String = 'id';
        public static var campo_idMov:String = 'idMov';
        public static var campo_idCaixa:String = 'idCaixa';
        public static var campo_idCaixaDia:String = 'idCaixaDia';
        public static var campo_qtdParcelas:String = 'qtdParcelas';
        public static var campo_valor:String = 'valor';
        public static var campo_dtPrimeiraParcela:String = 'dtPrimeiraParcela';
        public static var campo_dtPrimeiraParcelaTicks:String = 'dtPrimeiraParcelaTicks';
        public static var campo_especie:String = 'especie';
        public function MovValor(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in MovValor.getCampos()){this[campo]=o[campo];}}
        public function clone():MovValor{return new MovValor(this);}
        public function toString():String
        {
            return '[MovValor '+id+']';
        }
        public var id:Number = 0;
        public var idMov:Number = 0;
        public var idCaixa:Number = 0;
        public var idCaixaDia:Number = 0;
        public var qtdParcelas:Number = 0;
        public var valor:Number = 0;
        public var dtPrimeiraParcela:String = '';
        public var dtPrimeiraParcelaTicks:Number = 0;
        public var especie:String = 'nao_informado';
    }
}
