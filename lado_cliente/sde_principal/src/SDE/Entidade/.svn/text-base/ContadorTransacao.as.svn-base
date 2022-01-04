package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.ContadorTransacao')]
    public final class ContadorTransacao
    {
        public static function get CLASSE():String{return 'ContadorTransacao';}
        public static function getCampos():Array{return['id','idEmp','idClienteFuncionarioLogado','dthr']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_dthr:String = 'dthr';
        public function ContadorTransacao(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in ContadorTransacao.getCampos()){this[campo]=o[campo];}}
        public function clone():ContadorTransacao{return new ContadorTransacao(this);}
        public function toString():String
        {
            return '[ContadorTransacao '+id+']';
        }
        public var dthr:String = '';
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
    }
}
