package SDE.Enumerador
{
    public final class ENfeAmbiente
    {
        public static function getCampos():Array{return['producao','homologacao']};
        
        public static const producao:String = 'producao';
        public static const homologacao:String = 'homologacao';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
