package SDE.Enumerador
{
    public final class EVeiculoCombustivel
    {
        public static function getCampos():Array{return['alcool','gasolina','diesel']};
        
        public static const alcool:String = 'alcool';
        public static const gasolina:String = 'gasolina';
        public static const diesel:String = 'diesel';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
