package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqFornecedor extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqFornecedor()
		{
			super();
			
			dp.source = App.single.cache.arrayCliente;
			prompt = "Selecione um fornecedor ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Cliente.campo_nome, Cliente.campo_apelido_razsoc, Cliente.campo_cpf_cnpj, Cliente.campo_rg];
			browserFields = camposPesquisa;
			labelField = Cliente.campo_nome;
			filterFunction = myFilterFunction;
		}
		
		private function myFilterFunction(xxx:Cliente, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.ehFornecedor){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && xxx.ehFornecedor);
		}
	}
}