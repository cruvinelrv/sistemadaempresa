package SDE.Enumerador
{
    public final class ESituacaoNota
    {
        public static function getCampos():Array{return['normal','cancel','normal_extemporaneo','cancel_extemporaneo']};
        
        public static const normal:String = 'normal';
        public static const cancel:String = 'cancel';
        public static const normal_extemporaneo:String = 'normal_extemporaneo';
        public static const cancel_extemporaneo:String = 'cancel_extemporaneo';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
