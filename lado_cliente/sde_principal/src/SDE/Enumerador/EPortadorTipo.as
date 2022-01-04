package SDE.Enumerador
{
    public final class EPortadorTipo
    {
        public static function getCampos():Array{return['Carteira','Cartorio','Banco','Cobrador','Juridico']};
        
        public static const Carteira:String = 'Carteira';
        public static const Cartorio:String = 'Cartorio';
        public static const Banco:String = 'Banco';
        public static const Cobrador:String = 'Cobrador';
        public static const Juridico:String = 'Juridico';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
