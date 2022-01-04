package SDE.Enumerador
{
    public final class EMovResumo
    {
        public static function getCampos():Array{return['entrada','saida','outros','ambos']};
        
        public static const entrada:String = 'entrada';
        public static const saida:String = 'saida';
        public static const outros:String = 'outros';
        public static const ambos:String = 'ambos';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
