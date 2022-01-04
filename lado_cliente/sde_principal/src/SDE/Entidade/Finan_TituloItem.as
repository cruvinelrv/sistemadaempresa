package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_TituloItem')]
    public final class Finan_TituloItem
    {
        public static function get CLASSE():String{return 'Finan_TituloItem';}
        public static function getCampos():Array{return['idEmp','id','idClienteFuncionarioLogado','idContadorOperacao','idTituloMapa','idTipoPagamento','idGrupoTipoPagamento','idTitulo','parcela','texto','dtLancamento','dtPagamento','descontoValidade','condEspeciais','obs','identificador','valorCobrado','descontoPct','descontoVlr','situacao']};
        
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_idContadorOperacao:String = 'idContadorOperacao';
        public static var campo_idTituloMapa:String = 'idTituloMapa';
        public static var campo_idTipoPagamento:String = 'idTipoPagamento';
        public static var campo_idGrupoTipoPagamento:String = 'idGrupoTipoPagamento';
        public static var campo_idTitulo:String = 'idTitulo';
        public static var campo_parcela:String = 'parcela';
        public static var campo_texto:String = 'texto';
        public static var campo_dtLancamento:String = 'dtLancamento';
        public static var campo_dtPagamento:String = 'dtPagamento';
        public static var campo_descontoValidade:String = 'descontoValidade';
        public static var campo_condEspeciais:String = 'condEspeciais';
        public static var campo_obs:String = 'obs';
        public static var campo_identificador:String = 'identificador';
        public static var campo_valorCobrado:String = 'valorCobrado';
        public static var campo_descontoPct:String = 'descontoPct';
        public static var campo_descontoVlr:String = 'descontoVlr';
        public static var campo_situacao:String = 'situacao';
        public function Finan_TituloItem(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_TituloItem.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_TituloItem{return new Finan_TituloItem(this);}
        public function toString():String
        {
            return '[Finan_TituloItem '+id+']';
        }
        public var idEmp:Number = 0;
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var idContadorOperacao:Number = 0;
        public var idTituloMapa:Number = 0;
        public var idTipoPagamento:Number = 0;
        public var idGrupoTipoPagamento:Number = 0;
        public var idTitulo:Number = 0;
        public var parcela:Number = 0;
        public var texto:String = '';
        public var dtLancamento:String = '';
        public var dtPagamento:String = '';
        public var descontoValidade:String = '';
        public var condEspeciais:String = '';
        public var obs:String = '';
        public var identificador:String = '';
        public var valorCobrado:Number = 0;
        public var descontoPct:Number = 0;
        public var descontoVlr:Number = 0;
        public var situacao:String = 'em_aberto';
    }
}
