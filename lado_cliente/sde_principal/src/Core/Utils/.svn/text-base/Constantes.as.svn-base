package Core.Utils
{
	import SDE.Enumerador.EItemUnidMed;
	import SDE.Enumerador.EValorEspecie;
	
	[Bindable]
	public final class Constantes
	{
		private static var _:Constantes;
		public static function get unica():Constantes
		{
			if (_==null)
				_ = new Constantes();
			return _;
		}
		
		public var CST:Array = ["000", "020", "040", "060"];
		public var UM:Array  = EItemUnidMed.getCampos(); /*["PC", "UN", "CX", "KG", "JG", "PAR", "M", "M2", "M3", "L", "ML", "SC"]*/;
		public var ESPECIES:Array  = EValorEspecie.getCampos();
		
		public var cpf_cliente_consumidor:String = "00000000000";
		
	}
}