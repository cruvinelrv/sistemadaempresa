package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.ItemEmpEstoque;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqEstoque extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqEstoque()
		{
			super();
			dp.source = App.single.cache.arrayItemEmpEstoque;
			dataProvider = dp;
			camposPesquisa = [ItemEmpEstoque.campo_codBarras, ItemEmpEstoque.campo_codBarrasGrade, ItemEmpEstoque.campo_identificador];
			browserFields = [ItemEmpEstoque.campo_codBarras, ItemEmpEstoque.campo_identificador, ItemEmpEstoque.campo_qtd];
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		private function myLabelFunction( item:ItemEmpEstoque ):String
		{
			return item.codBarras+" "+item.identificador;
		}
		
		private function myFilterFunction(xxx:ItemEmpEstoque, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.idEmp == App.single.ss.idEmp){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && xxx.idEmp == App.single.ss.idEmp);
		}
	}
}