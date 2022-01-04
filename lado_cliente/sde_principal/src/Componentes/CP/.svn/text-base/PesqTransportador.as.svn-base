package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqTransportador extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqTransportador()
		{
			super();
			
			dp.source = App.single.cache.arrayCliente;
			prompt= "Selecione um Transportador ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = ["nome", "apelido_razsoc","cpf_cnpj", "rg"];
			browserFields = camposPesquisa;
		    labelField = Cliente.campo_nome;
		    filterFunction = myFilterFunction;
		}
		
		private function myFilterFunction(xxx:Cliente, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.ehTransportador){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && xxx.ehTransportador);
		}
	}
}