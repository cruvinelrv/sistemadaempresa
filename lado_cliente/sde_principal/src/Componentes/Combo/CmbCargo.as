package Componentes.Combo
{
	import Core.App;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.Cargo;
	
	import mx.controls.ComboBox;

	public class CmbCargo extends ComboBox
	{
		private var dp:Array;
		private var cache:NuvemCache;
		
		public function CmbCargo()
		{
			super();
			cache = App.single.cache;
			dp = [];
			dp.push(new Cargo());
			for each (var cargo:Cargo in cache.arrayCargo)
				dp.push(cargo);
			dataProvider = dp;
			labelField = Cargo.campo_nomeCargo;
		}
		
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem = cache.getCargo(idXXX);
		}
		
		public function getValor():Cargo
		{
			return selectedItem as Cargo;
		}
	}
}