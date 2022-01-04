package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Finan_Portador')]
    public final class Finan_Portador
    {
        public static function get CLASSE():String{return 'Finan_Portador';}
        public static function getCampos():Array{return['idEmp','id','idTipoPortador','idClienteFuncionarioLogado','nome']};
        
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_id:String = 'id';
        public static var campo_idTipoPortador:String = 'idTipoPortador';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_nome:String = 'nome';
        public function Finan_Portador(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Finan_Portador.getCampos()){this[campo]=o[campo];}}
        public function clone():Finan_Portador{return new Finan_Portador(this);}
        public function toString():String
        {
            return nome;
        }
        public var idEmp:Number = 0;
        public var id:Number = 0;
        public var idTipoPortador:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var nome:String = '';
    }
}
