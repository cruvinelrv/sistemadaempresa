package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Finan_GrupoTipoPagamento;
	
	import mx.controls.ComboBox;

	public class CmbFinanGrupoTipoPagamento extends ComboBox
	{
		private var dp:Array;
		
		public function CmbFinanGrupoTipoPagamento()
		{
			super();
			dp = App.single.cache.arrayFinan_GrupoTipoPagamento;
			dataProvider = dp;
			labelField = Finan_GrupoTipoPagamento.campo_nome;
		}
		public function setValorId(idGrupoTipoPagamento:Number):void
		{
			if (idGrupoTipoPagamento==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idGrupoTipoPagamento)
				.getResultadoArraySimples()[0];
		}
		public function getValor():Finan_GrupoTipoPagamento
		{
			return selectedItem as Finan_GrupoTipoPagamento;
		}
		
	}
}