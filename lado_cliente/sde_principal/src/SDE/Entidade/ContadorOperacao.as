package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ContadorOperacao')]
    public final class ContadorOperacao
    {
        public static function get CLASSE():String{return 'ContadorOperacao';}
        public static function getCampos():Array{return['id','idEmp','idClienteFuncionarioLogado','dthr']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_dthr:String = 'dthr';
        public function ContadorOperacao(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ContadorOperacao.getCampos()){this[campo]=o[campo];}}
        public function clone():ContadorOperacao{return new ContadorOperacao(this);}
        public function toString():String
        {
            return '[ContadorOperacao '+id+']';
        }
        public var dthr:String = '';
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
    }
}
