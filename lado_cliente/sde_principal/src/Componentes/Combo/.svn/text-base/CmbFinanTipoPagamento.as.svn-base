package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Finan_TipoPagamento;
	
	import mx.controls.ComboBox;

	public class CmbFinanTipoPagamento extends ComboBox
	{
		private var dp:Array;
		
		public function CmbFinanTipoPagamento()
		{
			super();
			dp = App.single.cache.arrayFinan_TipoPagamento;
			dataProvider = dp;
			labelField = Finan_TipoPagamento.campo_nome;
		}
		public function setValorId(idTipoPagamento:Number):void
		{
			if (idTipoPagamento==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idTipoPagamento)
				.getResultadoArraySimples()[0];
		}
		public function getValor():Finan_TipoPagamento
		{
			return selectedItem as Finan_TipoPagamento;
		}
		
	}
}