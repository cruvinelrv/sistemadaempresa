package SDE.Outros
{
    [Bindable]
    [RemoteClass(alias='SDE.Outros.PlanoContaTipo')]
    public final class PlanoContaTipo
    {
        public static function get CLASSE():String{return 'PlanoContaTipo';}
        public static function getCampos():Array{return['direcao','tipo','nome']};
        
        public static var campo_direcao:String = 'direcao';
        public static var campo_tipo:String = 'tipo';
        public static var campo_nome:String = 'nome';
        public function clone():PlanoContaTipo { return new PlanoContaTipo(this); }
        public function PlanoContaTipo(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var direcao:String = 'nenhum';
        public var tipo:String = 'Im';
        public var nome:String = '';
    }
}
