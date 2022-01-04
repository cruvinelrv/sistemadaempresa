package Componentes.Combo
{
	import Core.App;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.Finan_CentroCusto;
	
	import mx.controls.ComboBox;
	import mx.core.Application;

	public class CmbFinanCentroCusto extends ComboBox
	{
		private var dp:Array;
		private var cache:NuvemCache;
		
		public function CmbFinanCentroCusto()
		{
			super();
			cache = App.single.cache;
			dp = cache.Finan_CentroCusto;
			dataProvider = dp;
			labelField = Finan_CentroCusto.campo_nome;
		}
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem = cache.getFinan_CentroCusto(idXXX);
		}
		public function getValor():Finan_CentroCusto
		{
			return selectedItem as Finan_CentroCusto;
		}
		
	}
}