package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqFuncionario extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqFuncionario()
		{
			super();
			
			dp.source = App.single.cache.arrayCliente;
			prompt = "Selecione um funcionário ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Cliente.campo_nome, Cliente.campo_apelido_razsoc, Cliente.campo_cpf_cnpj, Cliente.campo_rg];
			browserFields = camposPesquisa;
			labelField = Cliente.campo_nome;
			filterFunction = myFiterFunction;
		}
		
		private function myFiterFunction(xxx:Cliente, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.ehFuncionario){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && xxx.ehFuncionario);
		}
	}
}