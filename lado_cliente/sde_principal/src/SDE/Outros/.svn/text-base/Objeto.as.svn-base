package SDE.Outros
{
    [Bindable]
    [RemoteClass(alias='SDE.Outros.Objeto')]
    public final class Objeto
    {
        public static function get CLASSE():String{return 'Objeto';}
        public static function getCampos():Array{return['valor','classe','campo','primitivo','uuID','campos']};
        
        public static var campo_valor:String = 'valor';
        public static var campo_classe:String = 'classe';
        public static var campo_campo:String = 'campo';
        public static var campo_primitivo:String = 'primitivo';
        public static var campo_uuID:String = 'uuID';
        public static var campo_campos:String = 'campos';
        public function clone():Objeto { return new Objeto(this); }
        public function Objeto(obj:Object=null)
        {
            if (obj==null)return;
            for each(var campo:String in getCampos())this[campo]=obj[campo];
        }
        public var valor:Object = null;
        public var classe:String = '';
        public var campo:String = '';
        public var primitivo:Boolean = false;
        public var uuID:Number = 0;
        public var campos:Array = null;
    }
}
