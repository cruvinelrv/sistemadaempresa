package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Empresa;
	
	import mx.controls.ComboBox;

	public class CmbEmpresa extends ComboBox
	{
		private var dp:Array;
		
		public function CmbEmpresa()
		{
			super();
			dp = App.single.cache.arrayEmpresa;
			dataProvider = dp;
			labelField = Empresa.campo_usuario;
		}
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idXXX)
				.getResultadoArraySimples()[0];
		}
		public function getValor():Empresa
		{
			return selectedItem as Empresa;
		}
		
	}
}