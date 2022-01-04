package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamLoadCliente')]
    public final class ParamLoadCliente
    {
        public static function get CLASSE():String{return 'ParamLoadCliente';}
        public static function getCampos():Array{return['ignorar','funcionario','fornecedor','transportador','parceiro','bancarios','veiculos','contatos','enderecos','familiares']};
        
        public static var campo_ignorar:String = 'ignorar';
        public static var campo_funcionario:String = 'funcionario';
        public static var campo_fornecedor:String = 'fornecedor';
        public static var campo_transportador:String = 'transportador';
        public static var campo_parceiro:String = 'parceiro';
        public static var campo_bancarios:String = 'bancarios';
        public static var campo_veiculos:String = 'veiculos';
        public static var campo_contatos:String = 'contatos';
        public static var campo_enderecos:String = 'enderecos';
        public static var campo_familiares:String = 'familiares';
        public function clone():ParamLoadCliente { return new ParamLoadCliente(this); }
        public function ParamLoadCliente(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var ignorar:Boolean = false;
        public var funcionario:Boolean = false;
        public var fornecedor:Boolean = false;
        public var transportador:Boolean = false;
        public var parceiro:Boolean = false;
        public var bancarios:Boolean = false;
        public var veiculos:Boolean = false;
        public var contatos:Boolean = false;
        public var enderecos:Boolean = false;
        public var familiares:Boolean = false;
    }
}
