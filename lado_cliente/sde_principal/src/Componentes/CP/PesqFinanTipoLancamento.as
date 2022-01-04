package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Finan_TipoLancamento;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqFinanTipoLancamento extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqFinanTipoLancamento()
		{
			super();
			dp.source = App.single.cache.arrayFinan_TipoLancamento;
			prompt = "Selecione um tipo de movimento ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Finan_TipoLancamento.campo_codigo, Finan_TipoLancamento.campo_nomeTipoLancamento];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction(tipoLancamento:Finan_TipoLancamento):String
		{
			return (tipoLancamento.codigo +" - "+ tipoLancamento.nomeTipoLancamento);
		}
		
		private function myFilterFunction(xxx:Finan_TipoLancamento, searchStr:String):Boolean{
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