package Componentes.Combo
{
	import Core.App;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.Finan_Portador;
	
	import mx.controls.ComboBox;
	import mx.core.Application;

	public class CmbFinanPortador extends ComboBox
	{
		private var dp:Array;
		private var cache:NuvemCache;
		
		public function CmbFinanPortador()
		{
			super();
			cache = App.single.cache;
			dp = cache.Finan_Portador;
			dataProvider = dp;
			labelField = Finan_Portador.campo_nome;
		}
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem = cache.getFinan_Portador(idXXX);
		}
		public function getValor():Finan_Portador
		{
			return selectedItem as Finan_Portador;
		}
		
	}
}