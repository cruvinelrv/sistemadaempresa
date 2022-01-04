package SDE.Enumerador
{
    public final class ECxLancamentoSituacao
    {
        public static function getCampos():Array{return['aberto','lancado']};
        
        public static const aberto:String = 'aberto';
        public static const lancado:String = 'lancado';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
