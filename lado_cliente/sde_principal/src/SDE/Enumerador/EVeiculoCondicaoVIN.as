package SDE.Enumerador
{
    public final class EVeiculoCondicaoVIN
    {
        public static function getCampos():Array{return['importado','nacional']};
        
        public static const importado:String = 'importado';
        public static const nacional:String = 'nacional';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
