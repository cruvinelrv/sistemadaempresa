package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteVeiculo')]
    public final class ClienteVeiculo
    {
        public static function get CLASSE():String{return 'ClienteVeiculo';}
        public static function getCampos():Array{return['id','idCliente','isDeletado','placaNumero','placaUF','regNacTranspCarga','valorFIPE','tipo','nome','chassi','numSerieMotor','franquia','ano','marca','modelo']};
        
        public static var campo_id:String = 'id';
        public static var campo_idCliente:String = 'idCliente';
        public static var campo_isDeletado:String = 'isDeletado';
        public static var campo_placaNumero:String = 'placaNumero';
        public static var campo_placaUF:String = 'placaUF';
        public static var campo_regNacTranspCarga:String = 'regNacTranspCarga';
        public static var campo_valorFIPE:String = 'valorFIPE';
        public static var campo_tipo:String = 'tipo';
        public static var campo_nome:String = 'nome';
        public static var campo_chassi:String = 'chassi';
        public static var campo_numSerieMotor:String = 'numSerieMotor';
        public static var campo_franquia:String = 'franquia';
        public static var campo_ano:String = 'ano';
        public static var campo_marca:String = 'marca';
        public static var campo_modelo:String = 'modelo';
        public function ClienteVeiculo(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteVeiculo.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteVeiculo{return new ClienteVeiculo(this);}
        public function toString():String
        {
            return '[ClienteVeiculo '+id+']';
        }
        public var id:Number = 0;
        public var idCliente:Number = 0;
        public var isDeletado:Boolean = false;
        public var placaNumero:String = '';
        public var placaUF:String = '';
        public var regNacTranspCarga:String = '';
        public var valorFIPE:Number = 0;
        public var tipo:String = 'automovel';
        public var nome:String = '';
        public var chassi:String = '';
        public var numSerieMotor:String = '';
        public var franquia:String = '';
        public var ano:String = '';
        public var marca:String = '';
        public var modelo:String = '';
    }
}
