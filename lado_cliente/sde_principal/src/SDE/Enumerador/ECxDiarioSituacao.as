package SDE.Enumerador
{
    public final class ECxDiarioSituacao
    {
        public static function getCampos():Array{return['aberto_pelo_sistema','aberto','fechado']};
        
        public static const aberto_pelo_sistema:String = 'aberto_pelo_sistema';
        public static const aberto:String = 'aberto';
        public static const fechado:String = 'fechado';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
