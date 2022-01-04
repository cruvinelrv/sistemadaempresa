package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_TipoPagamento')]
    public final class Finan_TipoPagamento
    {
        public static function get CLASSE():String{return 'Finan_TipoPagamento';}
        public static function getCampos():Array{return['isHabilitado','ehPrazo','podeAlterarQtdParcelas','podeAlterarJuroParcela','podeAlterarPeriodo','geraContasPagar','geraContasReceber','utilizaSenha','imprimeCarne','__forcaAtualizacao','idEmp','id','idPortador','idTipoDocumento','idGrupoTipoPagamento','idClienteFuncionarioLogado','qtdParcelas','periodo','txJuroParcelamento','txJuroAtraso','txMultaAtraso','pctCustoAdministrativo','pctComissao','nome','senha','grupoTipoPagamento_nome']};
        
        public static var campo_isHabilitado:String = 'isHabilitado';
        public static var campo_ehPrazo:String = 'ehPrazo';
        public static var campo_podeAlterarQtdParcelas:String = 'podeAlterarQtdParcelas';
        public static var campo_podeAlterarJuroParcela:String = 'podeAlterarJuroParcela';
        public static var campo_podeAlterarPeriodo:String = 'podeAlterarPeriodo';
        public static var campo_geraContasPagar:String = 'geraContasPagar';
        public static var campo_geraContasReceber:String = 'geraContasReceber';
        public static var campo_utilizaSenha:String = 'utilizaSenha';
        public static var campo_imprimeCarne:String = 'imprimeCarne';
        public static var campo___forcaAtualizacao:String = '__forcaAtualizacao';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idPortador:String = 'idPortador';
        public static var campo_idTipoDocumento:String = 'idTipoDocumento';
        public static var campo_idGrupoTipoPagamento:String = 'idGrupoTipoPagamento';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_qtdParcelas:String = 'qtdParcelas';
        public static var campo_periodo:String = 'periodo';
        public static var campo_txJuroParcelamento:String = 'txJuroParcelamento';
        public static var campo_txJuroAtraso:String = 'txJuroAtraso';
        public static var campo_txMultaAtraso:String = 'txMultaAtraso';
        public static var campo_pctCustoAdministrativo:String = 'pctCustoAdministrativo';
        public static var campo_pctComissao:String = 'pctComissao';
        public static var campo_nome:String = 'nome';
        public static var campo_senha:String = 'senha';
        public static var campo_grupoTipoPagamento_nome:String = 'grupoTipoPagamento_nome';
        public function Finan_TipoPagamento(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_TipoPagamento.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_TipoPagamento{return new Finan_TipoPagamento(this);}
        public function toString():String
        {
            return nome;
        }
        public var isHabilitado:Boolean = false;
        public var ehPrazo:Boolean = false;
        public var podeAlterarQtdParcelas:Boolean = false;
        public var podeAlterarJuroParcela:Boolean = false;
        public var podeAlterarPeriodo:Boolean = false;
        public var geraContasPagar:Boolean = false;
        public var geraContasReceber:Boolean = false;
        public var utilizaSenha:Boolean = false;
        public var imprimeCarne:Boolean = false;
        public var __forcaAtualizacao:Boolean = false;
        public var idEmp:Number = 0;
        public var id:Number = 0;
        public var idPortador:Number = 0;
        public var idTipoDocumento:Number = 0;
        public var idGrupoTipoPagamento:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var qtdParcelas:Number = 0;
        public var periodo:Number = 0;
        public var txJuroParcelamento:Number = 0;
        public var txJuroAtraso:Number = 0;
        public var txMultaAtraso:Number = 0;
        public var pctCustoAdministrativo:Number = 0;
        public var pctComissao:Number = 0;
        public var nome:String = '';
        public var senha:String = '';
        public var grupoTipoPagamento_nome:String = '';
    }
}
