package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.ItemEmpEstoque;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqCodBarras extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqCodBarras()
		{
			super();
			
			dp.source = App.single.cache.arrayItemEmpEstoque;
			prompt="BARRAS";
			showBrowseButton = false;
			dataProvider = dp;
			camposPesquisa = [ItemEmpEstoque.campo_codBarras];
			browserFields = [ItemEmpEstoque.campo_codBarras, ItemEmpEstoque.campo_identificador, ItemEmpEstoque.campo_qtd];
			labelFunction = myLabelFunction;
		}
		
		private function myLabelFunction( item:ItemEmpEstoque ):String
		{
			return item.codBarras;
		}
		
		private function myFilterFunction(xxx:ItemEmpEstoque, searchStr:String):Boolean{
			if (searchStr.length == 0){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return StringUtils.contains(stringCampos, searchStr);
		}
	}
}