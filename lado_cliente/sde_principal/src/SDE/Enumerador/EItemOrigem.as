package SDE.Enumerador
{
    public final class EItemOrigem
    {
        public static function getCampos():Array{return['nacional','internacional','internacional_mi']};
        
        public static const nacional:String = 'nacional';
        public static const internacional:String = 'internacional';
        public static const internacional_mi:String = 'internacional_mi';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
