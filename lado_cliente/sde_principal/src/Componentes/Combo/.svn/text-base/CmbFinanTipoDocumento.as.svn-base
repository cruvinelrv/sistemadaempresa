package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Finan_TipoDocumento;
	
	import mx.controls.ComboBox;

	public class CmbFinanTipoDocumento extends ComboBox
	{
		private var dp:Array;
		
		public function CmbFinanTipoDocumento()
		{
			super();
			dp = App.single.cache.arrayFinan_TipoDocumento;
			dataProvider = dp;
			labelField = Finan_TipoDocumento.campo_nome;
		}
		public function setValorId(idTipoDocumento:Number):void
		{
			if (idTipoDocumento==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idTipoDocumento)
				.getResultadoArraySimples()[0];
		}
		public function getValor():Finan_TipoDocumento
		{
			return selectedItem as Finan_TipoDocumento;
		}
		
	}
}