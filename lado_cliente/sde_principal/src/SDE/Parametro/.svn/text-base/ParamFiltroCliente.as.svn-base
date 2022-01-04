package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamFiltroCliente')]
    public final class ParamFiltroCliente
    {
        public static function get CLASSE():String{return 'ParamFiltroCliente';}
        public static function getCampos():Array{return['tipo','funcionario','fornecedor','transportador','parceiro','cliente','texto','cpf','offSet','limit']};
        
        public static var campo_tipo:String = 'tipo';
        public static var campo_funcionario:String = 'funcionario';
        public static var campo_fornecedor:String = 'fornecedor';
        public static var campo_transportador:String = 'transportador';
        public static var campo_parceiro:String = 'parceiro';
        public static var campo_cliente:String = 'cliente';
        public static var campo_texto:String = 'texto';
        public static var campo_cpf:String = 'cpf';
        public static var campo_offSet:String = 'offSet';
        public static var campo_limit:String = 'limit';
        public function clone():ParamFiltroCliente { return new ParamFiltroCliente(this); }
        public function ParamFiltroCliente(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var tipo:String = 'nao_informado';
        public var funcionario:Boolean = false;
        public var fornecedor:Boolean = false;
        public var transportador:Boolean = false;
        public var parceiro:Boolean = false;
        public var cliente:Boolean = false;
        public var texto:String = '';
        public var cpf:String = '';
        public var offSet:Number = 0;
        public var limit:Number = 0;
    }
}
