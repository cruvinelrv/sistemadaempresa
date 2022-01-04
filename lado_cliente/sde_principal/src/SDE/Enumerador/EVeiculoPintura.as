package SDE.Enumerador
{
    public final class EVeiculoPintura
    {
        public static function getCampos():Array{return['metalica','solida']};
        
        public static const metalica:String = 'metalica';
        public static const solida:String = 'solida';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
