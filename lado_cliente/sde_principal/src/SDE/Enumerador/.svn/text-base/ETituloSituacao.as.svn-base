package SDE.Enumerador
{
    public final class ETituloSituacao
    {
        public static function getCampos():Array{return['em_aberto','lancado','duplicata_impressa','cheque_repassado']};
        
        public static const em_aberto:String = 'em_aberto';
        public static const lancado:String = 'lancado';
        public static const duplicata_impressa:String = 'duplicata_impressa';
        public static const cheque_repassado:String = 'cheque_repassado';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
