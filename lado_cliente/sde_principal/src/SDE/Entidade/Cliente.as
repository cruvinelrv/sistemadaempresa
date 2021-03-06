package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Cliente')]
    public final class Cliente
    {
        public static function get CLASSE():String{return 'Cliente';}
        public static function getCampos():Array{return['id','idPessoa','idCargo','obs','dtImportacao','ehFuncionario','ehFornecedor','ehTransportador','ehParceiro','ehInativo','comissionado','usuarioSistema','tipo','estado','sexo','nome','cpf_cnpj','rg','rgUF','apelido_razsoc','nomePai','nomeMae','hash','dtNasc','dtRegistro','dtNascTicks','dtRegistroTicks','isImportado','loginUsuario','loginSenha','calculaMontanteTotal','calculaMaoDeObra','calculaMaoDeObraGeral','calculaMaoDeObraGarantia','calculaMaoDeObraGeralGarantia','calculaProdutosEmGarantia','calculaProdutos','comissaoMontanteTotal','comissaoMaoDeObra','comissaoMaoDeObraGeral','comissaoMaoDeObraGarantia','comissaoMaoDeObraGeralGarantia','comissaoProdutosEmGarantia','comissaoProdutos','__bancarios','__veiculos','__contatos','__enderecos','__familiares']};
        
        public static var campo_id:String = 'id';
        public static var campo_idPessoa:String = 'idPessoa';
        public static var campo_idCargo:String = 'idCargo';
        public static var campo_obs:String = 'obs';
        public static var campo_dtImportacao:String = 'dtImportacao';
        public static var campo_ehFuncionario:String = 'ehFuncionario';
        public static var campo_ehFornecedor:String = 'ehFornecedor';
        public static var campo_ehTransportador:String = 'ehTransportador';
        public static var campo_ehParceiro:String = 'ehParceiro';
        public static var campo_ehInativo:String = 'ehInativo';
        public static var campo_comissionado:String = 'comissionado';
        public static var campo_usuarioSistema:String = 'usuarioSistema';
        public static var campo_tipo:String = 'tipo';
        public static var campo_estado:String = 'estado';
        public static var campo_sexo:String = 'sexo';
        public static var campo_nome:String = 'nome';
        public static var campo_cpf_cnpj:String = 'cpf_cnpj';
        public static var campo_rg:String = 'rg';
        public static var campo_rgUF:String = 'rgUF';
        public static var campo_apelido_razsoc:String = 'apelido_razsoc';
        public static var campo_nomePai:String = 'nomePai';
        public static var campo_nomeMae:String = 'nomeMae';
        public static var campo_hash:String = 'hash';
        public static var campo_dtNasc:String = 'dtNasc';
        public static var campo_dtRegistro:String = 'dtRegistro';
        public static var campo_dtNascTicks:String = 'dtNascTicks';
        public static var campo_dtRegistroTicks:String = 'dtRegistroTicks';
        public static var campo_isImportado:String = 'isImportado';
        public static var campo_loginUsuario:String = 'loginUsuario';
        public static var campo_loginSenha:String = 'loginSenha';
        public static var campo_calculaMontanteTotal:String = 'calculaMontanteTotal';
        public static var campo_calculaMaoDeObra:String = 'calculaMaoDeObra';
        public static var campo_calculaMaoDeObraGeral:String = 'calculaMaoDeObraGeral';
        public static var campo_calculaMaoDeObraGarantia:String = 'calculaMaoDeObraGarantia';
        public static var campo_calculaMaoDeObraGeralGarantia:String = 'calculaMaoDeObraGeralGarantia';
        public static var campo_calculaProdutosEmGarantia:String = 'calculaProdutosEmGarantia';
        public static var campo_calculaProdutos:String = 'calculaProdutos';
        public static var campo_comissaoMontanteTotal:String = 'comissaoMontanteTotal';
        public static var campo_comissaoMaoDeObra:String = 'comissaoMaoDeObra';
        public static var campo_comissaoMaoDeObraGeral:String = 'comissaoMaoDeObraGeral';
        public static var campo_comissaoMaoDeObraGarantia:String = 'comissaoMaoDeObraGarantia';
        public static var campo_comissaoMaoDeObraGeralGarantia:String = 'comissaoMaoDeObraGeralGarantia';
        public static var campo_comissaoProdutosEmGarantia:String = 'comissaoProdutosEmGarantia';
        public static var campo_comissaoProdutos:String = 'comissaoProdutos';
        public static var campo___bancarios:String = '__bancarios';
        public static var campo___veiculos:String = '__veiculos';
        public static var campo___contatos:String = '__contatos';
        public static var campo___enderecos:String = '__enderecos';
        public static var campo___familiares:String = '__familiares';
        public function Cliente(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Cliente.getCampos()){this[campo]=o[campo];}}
        public function clone():Cliente{return new Cliente(this);}
        public function toString():String
        {
            return tipo.charAt(0)+' | '+ nome;
        }
        public var id:Number = 0;
        public var idPessoa:Number = 0;
        public var idCargo:Number = 0;
        public var obs:String = '';
        public var dtImportacao:String = '';
        public var ehFuncionario:Boolean = false;
        public var ehFornecedor:Boolean = false;
        public var ehTransportador:Boolean = false;
        public var ehParceiro:Boolean = false;
        public var ehInativo:Boolean = false;
        public var comissionado:Boolean = false;
        public var usuarioSistema:Boolean = false;
        public var tipo:String = 'nao_informado';
        public var estado:String = 'nao_informado';
        public var sexo:String = 'nao_informado';
        public var nome:String = '';
        public var cpf_cnpj:String = '';
        public var rg:String = '';
        public var rgUF:String = '';
        public var apelido_razsoc:String = '';
        public var nomePai:String = '';
        public var nomeMae:String = '';
        public var hash:String = '';
        public var dtNasc:String = '';
        public var dtRegistro:String = '';
        public var dtNascTicks:Number = 0;
        public var dtRegistroTicks:Number = 0;
        public var isImportado:Boolean = false;
        public var loginUsuario:String = '';
        public var loginSenha:String = '';
        public var calculaMontanteTotal:Boolean = false;
        public var calculaMaoDeObra:Boolean = false;
        public var calculaMaoDeObraGeral:Boolean = false;
        public var calculaMaoDeObraGarantia:Boolean = false;
        public var calculaMaoDeObraGeralGarantia:Boolean = false;
        public var calculaProdutosEmGarantia:Boolean = false;
        public var calculaProdutos:Boolean = false;
        public var comissaoMontanteTotal:Number = 0;
        public var comissaoMaoDeObra:Number = 0;
        public var comissaoMaoDeObraGeral:Number = 0;
        public var comissaoMaoDeObraGarantia:Number = 0;
        public var comissaoMaoDeObraGeralGarantia:Number = 0;
        public var comissaoProdutosEmGarantia:Number = 0;
        public var comissaoProdutos:Number = 0;
        public var __bancarios:Array = null;
        public var __veiculos:Array = null;
        public var __contatos:Array = null;
        public var __enderecos:Array = null;
        public var __familiares:Array = null;
    }
}
