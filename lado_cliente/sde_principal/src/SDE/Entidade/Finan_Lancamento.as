package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_Lancamento')]
    public final class Finan_Lancamento
    {
        public static function get CLASSE():String{return 'Finan_Lancamento';}
        public static function getCampos():Array{return['id','idOperacao','idTransacao','idEmp','idClienteFuncionarioLogado','idContaOrigem','idContaDestino','idCentroCusto','idTipoLancamento','idTipoPagamento','dtFluxoCaixa','dtLancamento','nome','tipoLancamento_nome','historico','valorLancado','saldoAtual','saldoAnterior','isCredito','isRecebimentoRota','idFinan_LancamentoOrigem','qtdEstimada','qtdRecebida','valorBruto','valorDespesas','valorLiquido','porcentagemComissao','valorComissao']};
        
        public static var campo_id:String = 'id';
        public static var campo_idOperacao:String = 'idOperacao';
        public static var campo_idTransacao:String = 'idTransacao';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_idContaOrigem:String = 'idContaOrigem';
        public static var campo_idContaDestino:String = 'idContaDestino';
        public static var campo_idCentroCusto:String = 'idCentroCusto';
        public static var campo_idTipoLancamento:String = 'idTipoLancamento';
        public static var campo_idTipoPagamento:String = 'idTipoPagamento';
        public static var campo_dtFluxoCaixa:String = 'dtFluxoCaixa';
        public static var campo_dtLancamento:String = 'dtLancamento';
        public static var campo_nome:String = 'nome';
        public static var campo_tipoLancamento_nome:String = 'tipoLancamento_nome';
        public static var campo_historico:String = 'historico';
        public static var campo_valorLancado:String = 'valorLancado';
        public static var campo_saldoAtual:String = 'saldoAtual';
        public static var campo_saldoAnterior:String = 'saldoAnterior';
        public static var campo_isCredito:String = 'isCredito';
        public static var campo_isRecebimentoRota:String = 'isRecebimentoRota';
        public static var campo_idFinan_LancamentoOrigem:String = 'idFinan_LancamentoOrigem';
        public static var campo_qtdEstimada:String = 'qtdEstimada';
        public static var campo_qtdRecebida:String = 'qtdRecebida';
        public static var campo_valorBruto:String = 'valorBruto';
        public static var campo_valorDespesas:String = 'valorDespesas';
        public static var campo_valorLiquido:String = 'valorLiquido';
        public static var campo_porcentagemComissao:String = 'porcentagemComissao';
        public static var campo_valorComissao:String = 'valorComissao';
        public function Finan_Lancamento(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_Lancamento.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_Lancamento{return new Finan_Lancamento(this);}
        public function toString():String
        {
            return nome;
        }
        public var id:Number = 0;
        public var idOperacao:Number = 0;
        public var idTransacao:Number = 0;
        public var idEmp:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var idContaOrigem:Number = 0;
        public var idContaDestino:Number = 0;
        public var idCentroCusto:Number = 0;
        public var idTipoLancamento:Number = 0;
        public var idTipoPagamento:Number = 0;
        public var dtFluxoCaixa:String = '';
        public var dtLancamento:String = '';
        public var nome:String = '';
        public var tipoLancamento_nome:String = '';
        public var historico:String = '';
        public var valorLancado:Number = 0;
        public var saldoAtual:Number = 0;
        public var saldoAnterior:Number = 0;
        public var isCredito:Boolean = false;
        public var isRecebimentoRota:Boolean = false;
        public var idFinan_LancamentoOrigem:Number = 0;
        public var qtdEstimada:Number = 0;
        public var qtdRecebida:Number = 0;
        public var valorBruto:Number = 0;
        public var valorDespesas:Number = 0;
        public var valorLiquido:Number = 0;
        public var porcentagemComissao:Number = 0;
        public var valorComissao:Number = 0;
    }
}
