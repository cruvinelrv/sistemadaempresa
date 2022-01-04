package Componentes.Combo
{
	import Core.App;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.Finan_Conta;
	
	import mx.controls.ComboBox;
	import mx.core.Application;

	public class CmbFinanConta extends ComboBox
	{
		private var dp:Array;
		private var cache:NuvemCache;
		
		public function CmbFinanConta()
		{
			super();
			cache = App.single.cache;
			dp = cache.Finan_Conta;
			dataProvider = dp;
			labelField = Finan_Conta.campo_nome;
		}
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem = cache.getFinan_Conta(idXXX);
		}
		public function getValor():Finan_Conta
		{
			return selectedItem as Finan_Conta;
		}
		
	}
}