package SDE.Enumerador
{
    public final class EOrdemServicoStatus
    {
        public static function getCampos():Array{return['nao_iniciada','em_andamento','finalizada','cancelada','reaberta']};
        
        public static const nao_iniciada:String = 'nao_iniciada';
        public static const em_andamento:String = 'em_andamento';
        public static const finalizada:String = 'finalizada';
        public static const cancelada:String = 'cancelada';
        public static const reaberta:String = 'reaberta';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
