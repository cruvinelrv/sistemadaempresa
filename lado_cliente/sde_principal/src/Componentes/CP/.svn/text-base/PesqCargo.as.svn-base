package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cargo;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqCargo extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqCargo()
		{
			super();
			
			dp.source = App.single.cache.arrayCargo
			//prompt = "Selecione um Cargo ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Cargo.campo_nomeCargo];
			browserFields = camposPesquisa;
			labelField = Cargo.campo_nomeCargo;
			filterFunction = myFilterFunction;
		}
		
		private function myFilterFunction(xxx:Cargo, searchStr:String):Boolean{
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