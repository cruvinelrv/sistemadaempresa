package SDE.Enumerador
{
    public final class EContaTipo
    {
        public static function getCampos():Array{return['Caixa','Banco']};
        
        public static const Caixa:String = 'Caixa';
        public static const Banco:String = 'Banco';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
