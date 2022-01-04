package SDE.Enumerador
{
    public final class EItemTipoIdent
    {
        public static function getCampos():Array{return['identificador','grade','lote']};
        
        public static const identificador:String = 'identificador';
        public static const grade:String = 'grade';
        public static const lote:String = 'lote';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
