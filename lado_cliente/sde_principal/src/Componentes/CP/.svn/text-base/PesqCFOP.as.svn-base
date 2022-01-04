package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.CFOP;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqCFOP extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqCFOP()
		{
			super();
			dp.source = App.single.cache.arrayCFOP;
			prompt="Selecione um CFOP ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [CFOP.campo_codigo, CFOP.campo_descricao];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction( item:CFOP ):String{
			return item.codigo+" "+item.descricao;
		}
		
		private function myFilterFunction(xxx:CFOP, searchStr:String):Boolean{
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
		
		public function comecaCom1ou2():ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var cfop:CFOP in App.single.cache.arraycCFOP){
				if (cfop.codigo.substring(0,1) == '1' || cfop.codigo.substring(0,1) == '2')
					dp.addItem(cfop);
			}
			return dp;
		}
		
		public function dataProviderCFOPnfe():ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var for_cfop:CFOP in App.single.cache.arrayCFOP)
			{
				if (!StringUtils.beginsWith(for_cfop.codigo, '5') && !StringUtils.beginsWith(for_cfop.codigo, '6'))
					dp.addItem(for_cfop);
			}
			return dp;
		}
	}
}