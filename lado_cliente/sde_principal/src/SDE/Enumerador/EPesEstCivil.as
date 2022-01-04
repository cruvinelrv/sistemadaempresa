package SDE.Enumerador
{
    public final class EPesEstCivil
    {
        public static function getCampos():Array{return['nao_informado','solteiro','casado','divorciado','viuvo','outros']};
        
        public static const nao_informado:String = 'nao_informado';
        public static const solteiro:String = 'solteiro';
        public static const casado:String = 'casado';
        public static const divorciado:String = 'divorciado';
        public static const viuvo:String = 'viuvo';
        public static const outros:String = 'outros';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
