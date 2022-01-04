package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamFiltroItem')]
    public final class ParamFiltroItem
    {
        public static function get CLASSE():String{return 'ParamFiltroItem';}
        public static function getCampos():Array{return['offSet','limit','texto','secao','grupo','subgrupo','produto','servico']};
        
        public static var campo_offSet:String = 'offSet';
        public static var campo_limit:String = 'limit';
        public static var campo_texto:String = 'texto';
        public static var campo_secao:String = 'secao';
        public static var campo_grupo:String = 'grupo';
        public static var campo_subgrupo:String = 'subgrupo';
        public static var campo_produto:String = 'produto';
        public static var campo_servico:String = 'servico';
        public function clone():ParamFiltroItem { return new ParamFiltroItem(this); }
        public function ParamFiltroItem(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var offSet:Number = 0;
        public var limit:Number = 0;
        public var texto:String = '';
        public var secao:String = '';
        public var grupo:String = '';
        public var subgrupo:String = '';
        public var produto:Boolean = false;
        public var servico:Boolean = false;
    }
}
