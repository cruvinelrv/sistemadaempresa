package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_CentroCusto')]
    public final class Finan_CentroCusto
    {
        public static function get CLASSE():String{return 'Finan_CentroCusto';}
        public static function getCampos():Array{return['idEmp','id','idClienteFuncionarioLogado','nome']};
        
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_nome:String = 'nome';
        public function Finan_CentroCusto(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_CentroCusto.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_CentroCusto{return new Finan_CentroCusto(this);}
        public function toString():String
        {
            return nome;
        }
        public var idEmp:Number = 0;
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var nome:String = '';
    }
}
