package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Cad_Secao')]
    public final class Cad_Secao
    {
        public static function get CLASSE():String{return 'Cad_Secao';}
        public static function getCampos():Array{return['id','idClienteFuncionarioLogado','secao','grupo','subgrupo','__orderBy']};
        
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_secao:String = 'secao';
        public static var campo_grupo:String = 'grupo';
        public static var campo_subgrupo:String = 'subgrupo';
        public static var campo___orderBy:String = '__orderBy';
        public function Cad_Secao(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Cad_Secao.getCampos()){this[campo]=o[campo];}}
        public function clone():Cad_Secao{return new Cad_Secao(this);}
        public function toString():String
        {
            return '[Cad_Secao '+id+']';
        }
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var secao:String = '';
        public var grupo:String = '';
        public var subgrupo:String = '';
        public var __orderBy:String = '';
    }
}
