package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.CFOP;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqCFOPimportMov extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqCFOPimportMov()
		{
			super();
			showBrowseButton = false;
			dp.source = App.single.cache.arrayCFOP;
			prompt="CFOP";
			dataProvider = dp;
			camposPesquisa = [CFOP.campo_codigo];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction( item:CFOP ):String{
			return item.codigo;
		}
		
		private function myFilterFunction(xxx:CFOP, searchStr:String):Boolean
		{
			if (searchStr.length == 0){
				return true;
			}
		
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa)
				{
					stringCampos += xxx[campo] + " ";
				}
			return StringUtils.contains(stringCampos, searchStr) && 
				   !StringUtils.beginsWith(xxx.codigo,"0") && 
				   !StringUtils.beginsWith(xxx.codigo,"5") && 
				   !StringUtils.beginsWith(xxx.codigo,"6");
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
	}
}