package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Cad_Marca')]
    public final class Cad_Marca
    {
        public static function get CLASSE():String{return 'Cad_Marca';}
        public static function getCampos():Array{return['id','idClienteFuncionarioLogado','marca','modelo','__orderBy']};
        
        public static var campo_id:String = 'id';
        public static var campo_idClienteFuncionarioLogado:String = 'idClienteFuncionarioLogado';
        public static var campo_marca:String = 'marca';
        public static var campo_modelo:String = 'modelo';
        public static var campo___orderBy:String = '__orderBy';
        public function Cad_Marca(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Cad_Marca.getCampos()){this[campo]=o[campo];}}
        public function clone():Cad_Marca{return new Cad_Marca(this);}
        public function toString():String
        {
            return '[Cad_Marca '+id+']';
        }
        public var id:Number = 0;
        public var idClienteFuncionarioLogado:Number = 0;
        public var marca:String = '';
        public var modelo:String = '';
        public var __orderBy:String = '';
    }
}
