package SDE.Outros
{
    [Bindable]
    [RemoteClass(alias='SDE.Outros.Atualizacao')]
    public final class Atualizacao
    {
        public static function get CLASSE():String{return 'Atualizacao';}
        public static function getCampos():Array{return['classe','idObj','idAtualizacao','obj','ehInsercao']};
        
        public static var campo_classe:String = 'classe';
        public static var campo_idObj:String = 'idObj';
        public static var campo_idAtualizacao:String = 'idAtualizacao';
        public static var campo_obj:String = 'obj';
        public static var campo_ehInsercao:String = 'ehInsercao';
        public function clone():Atualizacao { return new Atualizacao(this); }
        public function Atualizacao(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var classe:String = '';
        public var idObj:Number = 0;
        public var idAtualizacao:Number = 0;
        public var obj:Object = null;
        public var ehInsercao:Boolean = false;
    }
}
