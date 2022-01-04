package SDE.Enumerador
{
    public final class EVeiculoCondicao
    {
        public static function getCampos():Array{return['acabado','inacabado','semi_acabado']};
        
        public static const acabado:String = 'acabado';
        public static const inacabado:String = 'inacabado';
        public static const semi_acabado:String = 'semi_acabado';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
