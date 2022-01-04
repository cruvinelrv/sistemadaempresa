package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.CargoComissionamento')]
    public final class CargoComissionamento
    {
        public static function get CLASSE():String{return 'CargoComissionamento';}
        public static function getCampos():Array{return['id','idEmp','idCargo','calculaMontanteTotal','calculaMaoDeObra','calculaMaoDeObraGeral','calculaMaoDeObraGarantia','calculaMaoDeObraGeralGarantia','calculaProdutosEmGarantia','calculaProdutos','comissaoMontanteTotal','comissaoMaoDeObra','comissaoMaoDeObraGeral','comissaoMaoDeObraGarantia','comissaoMaoDeObraGeralGarantia','comissaoProdutosEmGarantia','comissaoProdutos']};
        
        public static var campo_id:String = 'id';
        public static var campo_idEmp:String = 'idEmp';
        public static var campo_idCargo:String = 'idCargo';
        public static var campo_calculaMontanteTotal:String = 'calculaMontanteTotal';
        public static var campo_calculaMaoDeObra:String = 'calculaMaoDeObra';
        public static var campo_calculaMaoDeObraGeral:String = 'calculaMaoDeObraGeral';
        public static var campo_calculaMaoDeObraGarantia:String = 'calculaMaoDeObraGarantia';
        public static var campo_calculaMaoDeObraGeralGarantia:String = 'calculaMaoDeObraGeralGarantia';
        public static var campo_calculaProdutosEmGarantia:String = 'calculaProdutosEmGarantia';
        public static var campo_calculaProdutos:String = 'calculaProdutos';
        public static var campo_comissaoMontanteTotal:String = 'comissaoMontanteTotal';
        public static var campo_comissaoMaoDeObra:String = 'comissaoMaoDeObra';
        public static var campo_comissaoMaoDeObraGeral:String = 'comissaoMaoDeObraGeral';
        public static var campo_comissaoMaoDeObraGarantia:String = 'comissaoMaoDeObraGarantia';
        public static var campo_comissaoMaoDeObraGeralGarantia:String = 'comissaoMaoDeObraGeralGarantia';
        public static var campo_comissaoProdutosEmGarantia:String = 'comissaoProdutosEmGarantia';
        public static var campo_comissaoProdutos:String = 'comissaoProdutos';
        public function CargoComissionamento(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in CargoComissionamento.getCampos()){this[campo]=o[campo];}}
        public function clone():CargoComissionamento{return new CargoComissionamento(this);}
        public function toString():String
        {
            return '[CargoComissionamento '+id+']';
        }
        public var id:Number = 0;
        public var idEmp:Number = 0;
        public var idCargo:Number = 0;
        public var calculaMontanteTotal:Boolean = false;
        public var calculaMaoDeObra:Boolean = false;
        public var calculaMaoDeObraGeral:Boolean = false;
        public var calculaMaoDeObraGarantia:Boolean = false;
        public var calculaMaoDeObraGeralGarantia:Boolean = false;
        public var calculaProdutosEmGarantia:Boolean = false;
        public var calculaProdutos:Boolean = false;
        public var comissaoMontanteTotal:Number = 0;
        public var comissaoMaoDeObra:Number = 0;
        public var comissaoMaoDeObraGeral:Number = 0;
        public var comissaoMaoDeObraGarantia:Number = 0;
        public var comissaoMaoDeObraGeralGarantia:Number = 0;
        public var comissaoProdutosEmGarantia:Number = 0;
        public var comissaoProdutos:Number = 0;
    }
}
