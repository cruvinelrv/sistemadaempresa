package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.TempMov')]
    public final class TempMov
    {
        public static function get CLASSE():String{return 'TempMov';}
        public static function getCampos():Array{return['cfop','cliente_nome','cliente_cpf','cliente_contato','cliente_endereco_faturamento','cliente_endereco_cobranca','cliente_endereco_entrega','id','idOperacao','idTransacao','idMovMapa','idMovCanceladora','idMovCancelada','idEmp','idCaixa','idCaixaDia','idClienteParceiro','idCliente','idClienteFuncionarioLogado','idClienteFuncionarioVendedor','idClienteTransportador','idClienteEndereco','cooTEF','modeloNF','numeroNF','noMun','foraMun','vlrAcrescimo','vlrItensInicial','vlrItensFinal','vlrTotal','retencaoISSQN','retencaoINSS','dthrMovEmissao','dtLancaCaixa','dtExpiraReserva','dtNF','dtEntSai','anoReferencia','mesReferencia','serieNF','fatura','obs','situacaoDms','tipoDeclaracaoDms','tipoServicoDms','dtMovTicks','dtLancTicks','dtNotaTicks','isReservaDevolvida','isNfePreenchida','isNfeEnviada','isNfsCancelada','__cli','__cliFuncionario','__cliTransp','__cliParceiro','__mItens','__mValores','resumo','tipo','impressao','situacaoNF','__movNfe','__emp']};
        
        public static var campo_cfop:String = 'cfop';
        public static var campo_cliente_nome:String = 'cliente_nome';
        public static var campo_cliente_cpf:String = 'cliente_cpf';
        public static var campo_cliente_contato:String = 'cliente_contato';
        public static var campo_cliente_endereco_faturamento:String = 'cliente_endereco_faturamento';
        public static var campo_cliente_endereco_cobranca:String = 'cliente_endereco_cobranca';
        public static var campo_cliente_endereco_entrega:String = 'cliente_endereco_entrega';
        public static var campo_id:String = 'id';
        public static var campo_idOperacao:String = 'idOperacao';
        public static var campo_idTransacao:String = 'idTransacao';
        public static var campo_idMovMapa:String = 'idMovMapa';
        public static var campo_idMovCanceladora:String = 'idMovCanceladora';
        public static var campo_idMovCancelada:String = 'idMovCancelada';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idCaixa:String = 'idCaixa';
        public static var campo_idCaixaDia:String = 'idCaixaDia';
        public static var campo_idClienteParceiro:String = 'idClienteParceiro';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_idClienteFuncionarioVendedor:String = 'idClienteFuncionarioVendedor';
        public static var campo_idClienteTransportador:String = 'idClienteTransportador';
        public static var campo_idClienteEndereco:String = 'idClienteEndereco';
        public static var campo_cooTEF:String = 'cooTEF';
        public static var campo_modeloNF:String = 'modeloNF';
        public static var campo_numeroNF:String = 'numeroNF';
        public static var campo_noMun:String = 'noMun';
        public static var campo_foraMun:String = 'foraMun';
        public static var campo_vlrAcrescimo:String = 'vlrAcrescimo';
        public static var campo_vlrItensInicial:String = 'vlrItensInicial';
        public static var campo_vlrItensFinal:String = 'vlrItensFinal';
        public static var campo_vlrTotal:String = 'vlrTotal';
        public static var campo_retencaoISSQN:String = 'retencaoISSQN';
        public static var campo_retencaoINSS:String = 'retencaoINSS';
        public static var campo_dthrMovEmissao:String = 'dthrMovEmissao';
        public static var campo_dtLancaCaixa:String = 'dtLancaCaixa';
        public static var campo_dtExpiraReserva:String = 'dtExpiraReserva';
        public static var campo_dtNF:String = 'dtNF';
        public static var campo_dtEntSai:String = 'dtEntSai';
        public static var campo_anoReferencia:String = 'anoReferencia';
        public static var campo_mesReferencia:String = 'mesReferencia';
        public static var campo_serieNF:String = 'serieNF';
        public static var campo_fatura:String = 'fatura';
        public static var campo_obs:String = 'obs';
        public static var campo_situacaoDms:String = 'situacaoDms';
        public static var campo_tipoDeclaracaoDms:String = 'tipoDeclaracaoDms';
        public static var campo_tipoServicoDms:String = 'tipoServicoDms';
        public static var campo_dtMovTicks:String = 'dtMovTicks';
        public static var campo_dtLancTicks:String = 'dtLancTicks';
        public static var campo_dtNotaTicks:String = 'dtNotaTicks';
        public static var campo_isReservaDevolvida:String = 'isReservaDevolvida';
        public static var campo_isNfePreenchida:String = 'isNfePreenchida';
        public static var campo_isNfeEnviada:String = 'isNfeEnviada';
        public static var campo_isNfsCancelada:String = 'isNfsCancelada';
        public static var campo___cli:String = '__cli';
        public static var campo___cliFuncionario:String = '__cliFuncionario';
        public static var campo___cliTransp:String = '__cliTransp';
        public static var campo___cliParceiro:String = '__cliParceiro';
        public static var campo___mItens:String = '__mItens';
        public static var campo___mValores:String = '__mValores';
        public static var campo_resumo:String = 'resumo';
        public static var campo_tipo:String = 'tipo';
        public static var campo_impressao:String = 'impressao';
        public static var campo_situacaoNF:String = 'situacaoNF';
        public static var campo___movNfe:String = '__movNfe';
        public static var campo___emp:String = '__emp';
        public function TempMov(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in TempMov.getCampos()){this[campo]=o[campo];}}
        public function clone():TempMov{return new TempMov(this);}
        public function toString():String
        {
            return '[TempMov '+id+']';
        }
        public var cfop:String = '';
        public var cliente_nome:String = '';
        public var cliente_cpf:String = '';
        public var cliente_contato:String = '';
        public var cliente_endereco_faturamento:String = '';
        public var cliente_endereco_cobranca:String = '';
        public var cliente_endereco_entrega:String = '';
        public var id:Number = 0;
        public var idOperacao:Number = 0;
        public var idTransacao:Number = 0;
        public var idMovMapa:Number = 0;
        public var idMovCanceladora:Number = 0;
        public var idMovCancelada:Number = 0;
        public var idEmp:Number = 0;
        public var idCaixa:Number = 0;
        public var idCaixaDia:Number = 0;
        public var idClienteParceiro:Number = 0;
        public var idCliente:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var idClienteFuncionarioVendedor:Number = 0;
        public var idClienteTransportador:Number = 0;
        public var idClienteEndereco:Number = 0;
        public var cooTEF:Number = 0;
        public var modeloNF:Number = 0;
        public var numeroNF:Number = 0;
        public var noMun:Boolean = false;
        public var foraMun:Boolean = false;
        public var vlrAcrescimo:Number = 0;
        public var vlrItensInicial:Number = 0;
        public var vlrItensFinal:Number = 0;
        public var vlrTotal:Number = 0;
        public var retencaoISSQN:Number = 0;
        public var retencaoINSS:Number = 0;
        public var dthrMovEmissao:String = '';
        public var dtLancaCaixa:String = '';
        public var dtExpiraReserva:String = '';
        public var dtNF:String = '';
        public var dtEntSai:String = '';
        public var anoReferencia:String = '';
        public var mesReferencia:String = '';
        public var serieNF:String = '';
        public var fatura:String = '';
        public var obs:String = '';
        public var situacaoDms:String = '';
        public var tipoDeclaracaoDms:String = '';
        public var tipoServicoDms:String = '';
        public var dtMovTicks:Number = 0;
        public var dtLancTicks:Number = 0;
        public var dtNotaTicks:Number = 0;
        public var isReservaDevolvida:Boolean = false;
        public var isNfePreenchida:Boolean = false;
        public var isNfeEnviada:Boolean = false;
        public var isNfsCancelada:Boolean = false;
        public var __cli:Cliente = null;
        public var __cliFuncionario:Cliente = null;
        public var __cliTransp:Cliente = null;
        public var __cliParceiro:Cliente = null;
        public var __mItens:Array = null;
        public var __mValores:Array = null;
        public var resumo:String = 'entrada';
        public var tipo:String = 'entrada_cancel';
        public var impressao:String = 'sem_impressao';
        public var situacaoNF:String = 'normal';
        public var __movNfe:MovNFE = null;
        public var __emp:Empresa = null;
    }
}
