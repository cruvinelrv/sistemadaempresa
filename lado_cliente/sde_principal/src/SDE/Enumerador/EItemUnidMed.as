package SDE.Enumerador
{
    public final class EItemUnidMed
    {
        public static function getCampos():Array{return['UN','KG','M','M2','M3','L','DS','SRV','MX','PC','KIT','JG','CJ','CX','BB','RL','LT','SC','GL']};
        
        public static const UN:String = 'UN';
        public static const KG:String = 'KG';
        public static const M:String = 'M';
        public static const M2:String = 'M2';
        public static const M3:String = 'M3';
        public static const L:String = 'L';
        public static const DS:String = 'DS';
        public static const SRV:String = 'SRV';
        public static const MX:String = 'MX';
        public static const PC:String = 'PC';
        public static const KIT:String = 'KIT';
        public static const JG:String = 'JG';
        public static const CJ:String = 'CJ';
        public static const CX:String = 'CX';
        public static const BB:String = 'BB';
        public static const RL:String = 'RL';
        public static const LT:String = 'LT';
        public static const SC:String = 'SC';
        public static const GL:String = 'GL';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
