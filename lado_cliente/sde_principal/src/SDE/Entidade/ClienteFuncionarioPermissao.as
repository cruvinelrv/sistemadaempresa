package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ClienteFuncionarioPermissao')]
    public final class ClienteFuncionarioPermissao
    {
        public static function get CLASSE():String{return 'ClienteFuncionarioPermissao';}
        public static function getCampos():Array{return['id','idEmp','idClienteFuncionarioUsuario','prioridade','variavel','classe','tipo','valor','__forcaAtualizacao']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idClienteFuncionarioUsuario:String = 'idClienteFuncionarioUsuario';
        public static var campo_prioridade:String = 'prioridade';
        public static var campo_variavel:String = 'variavel';
        public static var campo_classe:String = 'classe';
        public static var campo_tipo:String = 'tipo';
        public static var campo_valor:String = 'valor';
        public static var campo___forcaAtualizacao:String = '__forcaAtualizacao';
        public function ClienteFuncionarioPermissao(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ClienteFuncionarioPermissao.getCampos()){this[campo]=o[campo];}}
        public function clone():ClienteFuncionarioPermissao{return new ClienteFuncionarioPermissao(this);}
        public function toString():String
        {
            return '[ClienteFuncionarioPermissao '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idClienteFuncionarioUsuario:Number = 0;
        public var prioridade:Number = 0;
        public var variavel:String = '';
        public var classe:String = '';
        public var tipo:String = '';
        public var valor:String = '';
        public var __forcaAtualizacao:Boolean = false;
    }
}
