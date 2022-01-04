package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_PortadorTipo')]
    public final class Finan_PortadorTipo
    {
        public static function get CLASSE():String{return 'Finan_PortadorTipo';}
        public static function getCampos():Array{return['idEmp','id','idClienteFuncionarioLogado','nome']};
        
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_nome:String = 'nome';
        public function Finan_PortadorTipo(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_PortadorTipo.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_PortadorTipo{return new Finan_PortadorTipo(this);}
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
