package SDE.Parametro
{
    [Bindable]
    [RemoteClass(alias='SDE.Parametro.ParamLoadItem')]
    public final class ParamLoadItem
    {
        public static function get CLASSE():String{return 'ParamLoadItem';}
        public static function getCampos():Array{return['ignorar','precos','estoques','movimentacoes','fornecedores','aliquotas','aliqEntDentro','aliqEntFora','aliqSaiDentro','aliqSaiFora']};
        
        public static var campo_ignorar:String = 'ignorar';
        public static var campo_precos:String = 'precos';
        public static var campo_estoques:String = 'estoques';
        public static var campo_movimentacoes:String = 'movimentacoes';
        public static var campo_fornecedores:String = 'fornecedores';
        public static var campo_aliquotas:String = 'aliquotas';
        public static var campo_aliqEntDentro:String = 'aliqEntDentro';
        public static var campo_aliqEntFora:String = 'aliqEntFora';
        public static var campo_aliqSaiDentro:String = 'aliqSaiDentro';
        public static var campo_aliqSaiFora:String = 'aliqSaiFora';
        public function clone():ParamLoadItem { return new ParamLoadItem(this); }
        public function ParamLoadItem(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var ignorar:Boolean = false;
        public var precos:Boolean = false;
        public var estoques:Boolean = false;
        public var movimentacoes:Boolean = false;
        public var fornecedores:Boolean = false;
        public var aliquotas:Boolean = false;
        public var aliqEntDentro:Boolean = false;
        public var aliqEntFora:Boolean = false;
        public var aliqSaiDentro:Boolean = false;
        public var aliqSaiFora:Boolean = false;
    }
}
