package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteEndereco')]
    public final class ClienteEndereco
    {
        public static function get CLASSE():String{return 'ClienteEndereco';}
        public static function getCampos():Array{return['id','idCliente','tipo','campo','tipoLogradouro','logradouro','bairro','cep','cidade','cidadeIBGE','uf','ufIBGE','complemento','falarCom','inscr','inscrMun','fone','tamanho','obs','numero','isDeletado']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_tipo:String = 'tipo';
        public static var campo_campo:String = 'campo';
        public static var campo_tipoLogradouro:String = 'tipoLogradouro';
        public static var campo_logradouro:String = 'logradouro';
        public static var campo_bairro:String = 'bairro';
        public static var campo_cep:String = 'cep';
        public static var campo_cidade:String = 'cidade';
        public static var campo_cidadeIBGE:String = 'cidadeIBGE';
        public static var campo_uf:String = 'uf';
        public static var campo_ufIBGE:String = 'ufIBGE';
        public static var campo_complemento:String = 'complemento';
        public static var campo_falarCom:String = 'falarCom';
        public static var campo_inscr:String = 'inscr';
        public static var campo_inscrMun:String = 'inscrMun';
        public static var campo_fone:String = 'fone';
        public static var campo_tamanho:String = 'tamanho';
        public static var campo_obs:String = 'obs';
        public static var campo_numero:String = 'numero';
        public static var campo_isDeletado:String = 'isDeletado';
        public function ClienteEndereco(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteEndereco.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteEndereco{return new ClienteEndereco(this);}
        public function toString():String
        {
            return '[ClienteEndereco '+id+']';
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var tipo:String = 'residencial_comercial';
        public var campo:String = '';
        public var tipoLogradouro:String = '';
        public var logradouro:String = '';
        public var bairro:String = '';
        public var cep:String = '';
        public var cidade:String = '';
        public var cidadeIBGE:String = '';
        public var uf:String = '';
        public var ufIBGE:String = '';
        public var complemento:String = '';
        public var falarCom:String = '';
        public var inscr:String = '';
        public var inscrMun:String = '';
        public var fone:String = '';
        public var tamanho:String = '';
        public var obs:String = '';
        public var numero:Number = 0;
        public var isDeletado:Boolean = false;
    }
}
