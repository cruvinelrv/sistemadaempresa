package SDE.Enumerador
{
    public final class EBalancoSituacao
    {
        public static function getCampos():Array{return['em_andamento','cancelado','efetuado']};
        
        public static const em_andamento:String = 'em_andamento';
        public static const cancelado:String = 'cancelado';
        public static const efetuado:String = 'efetuado';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
