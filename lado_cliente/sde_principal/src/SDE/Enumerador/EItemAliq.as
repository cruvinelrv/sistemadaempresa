package SDE.Enumerador
{
    public final class EItemAliq
    {
        public static function getCampos():Array{return['entDentroUF','entForaUF','saiDentroUF','saiForaUF','ipi','pis','cofins']};
        
        public static const entDentroUF:String = 'entDentroUF';
        public static const entForaUF:String = 'entForaUF';
        public static const saiDentroUF:String = 'saiDentroUF';
        public static const saiForaUF:String = 'saiForaUF';
        public static const ipi:String = 'ipi';
        public static const pis:String = 'pis';
        public static const cofins:String = 'cofins';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
