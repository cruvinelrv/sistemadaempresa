package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Finan_Conta;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqFinanConta extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqFinanConta()
		{
			super();
			dp.source = App.single.cache.arrayFinan_Conta;
			prompt = "Selecione uma Conta("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Finan_Conta.campo_id, Finan_Conta.campo_nome];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction(finanConta:Finan_Conta):String
		{
			return finanConta.id +" - "+ finanConta.nome +" - Tipo: "+ finanConta.tipo; 
		}
		
		private function myFilterFunction(xxx:Finan_Conta, searchStr:String):Boolean{
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