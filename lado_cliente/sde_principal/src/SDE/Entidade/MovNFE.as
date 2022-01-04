package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.MovNFE')]
    public final class MovNFE
    {
        public static function get CLASSE():String{return 'MovNFE';}
        public static function getCampos():Array{return['dtSaiEnt','cfop','infoAdicional','chaveAcessoNFE','clienteIE','fatura','codNumericoNFE','idMov','id','idmovReferenciada','numeroNota','serieNota','ambienteNFE','finalidadeNFE','formaPgtoNFE','idEmp','idEnderecoEmp','idCliente','idEnderecoCliente','idEnderecoEntrega','idEnderecoRetirada','idClienteTransp','idEnderecoTransp','idVeiculo','idReboque','tipoTranspNFE','volQuantidade','volPesoLiquido','volPesoBruto','valorFrete','valorSeguro','valorOutrasDespesas','volEspecie','volMarca','volNumeracao','horaSaida','__transportador','__veiculo','__reboque','__ceCliente','__ceEmpresa','__ceTransporte','__nfeRf','ehVendaVeiculo','__nfeVeiculo']};
        
        public static var campo_dtSaiEnt:String = 'dtSaiEnt';
        public static var campo_cfop:String = 'cfop';
        public static var campo_infoAdicional:String = 'infoAdicional';
        public static var campo_chaveAcessoNFE:String = 'chaveAcessoNFE';
        public static var campo_clienteIE:String = 'clienteIE';
        public static var campo_fatura:String = 'fatura';
        public static var campo_codNumericoNFE:String = 'codNumericoNFE';
        public static var campo_idMov:String = 'idMov';
        public static var campo_id:String = 'id';
        public static var campo_idmovReferenciada:String = 'idmovReferenciada';
        public static var campo_numeroNota:String = 'numeroNota';
        public static var campo_serieNota:String = 'serieNota';
        public static var campo_ambienteNFE:String = 'ambienteNFE';
        public static var campo_finalidadeNFE:String = 'finalidadeNFE';
        public static var campo_formaPgtoNFE:String = 'formaPgtoNFE';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idEnderecoEmp:String = 'idEnderecoEmp';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_idEnderecoCliente:String = 'idEnderecoCliente';
        public static var campo_idEnderecoEntrega:String = 'idEnderecoEntrega';
        public static var campo_idEnderecoRetirada:String = 'idEnderecoRetirada';
        public static var campo_idClienteTransp:String = 'idClienteTransp';
        public static var campo_idEnderecoTransp:String = 'idEnderecoTransp';
        public static var campo_idVeiculo:String = 'idVeiculo';
        public static var campo_idReboque:String = 'idReboque';
        public static var campo_tipoTranspNFE:String = 'tipoTranspNFE';
        public static var campo_volQuantidade:String = 'volQuantidade';
        public static var campo_volPesoLiquido:String = 'volPesoLiquido';
        public static var campo_volPesoBruto:String = 'volPesoBruto';
        public static var campo_valorFrete:String = 'valorFrete';
        public static var campo_valorSeguro:String = 'valorSeguro';
        public static var campo_valorOutrasDespesas:String = 'valorOutrasDespesas';
        public static var campo_volEspecie:String = 'volEspecie';
        public static var campo_volMarca:String = 'volMarca';
        public static var campo_volNumeracao:String = 'volNumeracao';
        public static var campo_horaSaida:String = 'horaSaida';
        public static var campo___transportador:String = '__transportador';
        public static var campo___veiculo:String = '__veiculo';
        public static var campo___reboque:String = '__reboque';
        public static var campo___ceCliente:String = '__ceCliente';
        public static var campo___ceEmpresa:String = '__ceEmpresa';
        public static var campo___ceTransporte:String = '__ceTransporte';
        public static var campo___nfeRf:String = '__nfeRf';
        public static var campo_ehVendaVeiculo:String = 'ehVendaVeiculo';
        public static var campo___nfeVeiculo:String = '__nfeVeiculo';
        public function MovNFE(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in MovNFE.getCampos()){this[campo]=o[campo];}}
        public function clone():MovNFE{return new MovNFE(this);}
        public function toString():String
        {
            return '[MovNFE '+id+']';
        }
        public var dtSaiEnt:String = '';
        public var cfop:String = '';
        public var infoAdicional:String = '';
        public var chaveAcessoNFE:String = '';
        public var clienteIE:String = '';
        public var fatura:String = '';
        public var codNumericoNFE:Number = 0;
        public var idMov:Number = 0;
        public var id:Number = 0;
        public var idmovReferenciada:Number = 0;
        public var numeroNota:Number = 0;
        public var serieNota:Number = 0;
        public var ambienteNFE:String = 'producao';
        public var finalidadeNFE:String = 'normal';
        public var formaPgtoNFE:String = 'vista';
        public var idEmp:Number = 0;
        public var idEnderecoEmp:Number = 0;
        public var idCliente:Number = 0;
        public var idEnderecoCliente:Number = 0;
        public var idEnderecoEntrega:Number = 0;
        public var idEnderecoRetirada:Number = 0;
        public var idClienteTransp:Number = 0;
        public var idEnderecoTransp:Number = 0;
        public var idVeiculo:Number = 0;
        public var idReboque:Number = 0;
        public var tipoTranspNFE:String = 'emitente';
        public var volQuantidade:Number = 0;
        public var volPesoLiquido:Number = 0;
        public var volPesoBruto:Number = 0;
        public var valorFrete:Number = 0;
        public var valorSeguro:Number = 0;
        public var valorOutrasDespesas:Number = 0;
        public var volEspecie:String = '';
        public var volMarca:String = '';
        public var volNumeracao:String = '';
        public var horaSaida:String = '';
        public var __transportador:Cliente = null;
        public var __veiculo:ClienteVeiculo = null;
        public var __reboque:ClienteVeiculo = null;
        public var __ceCliente:ClienteEndereco = null;
        public var __ceEmpresa:ClienteEndereco = null;
        public var __ceTransporte:ClienteEndereco = null;
        public var __nfeRf:MovNFE = null;
        public var ehVendaVeiculo:Boolean = false;
        public var __nfeVeiculo:MovNfeVeiculo = null;
    }
}
