package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;

	public class PesqCliente extends SuperCP
	{
		[Bindable] public var ehFuncionario:Boolean=false;
		[Bindable] public var ehFornecedor:Boolean=false;
		[Bindable] public var ehTransportador:Boolean=false;
		[Bindable] public var ehParceiro:Boolean=false;
		
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqCliente()
		{
			super();
			dp.source = App.single.cache.arrayCliente;
			prompt="Selecione um Cliente ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Cliente.campo_nome, Cliente.campo_apelido_razsoc, Cliente.campo_cpf_cnpj, Cliente.campo_rg];
			browserFields = camposPesquisa;
			labelField = "nome";
			//labelFunction = myLabelFunction;//veja CFOP
			filterFunction = myFilterFunction;
		}
		
		private function myFilterFunction( xxx:Cliente, searchStr:String ):Boolean{
			if (searchStr.length == 0){
				return true;
			}
			/*
			if (
				(this.ehFornecedor && !xxx.ehFornecedor)
				||
				(this.ehFuncionario && !xxx.ehFornecedor)
				||
				(this.ehTransportador && !xxx.ehTransportador)
				||
				(this.ehParceiro && !xxx.ehParceiro)
				)
				return false;
			/**/
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction( xxx );
			else
				for each(var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			
			return StringUtils.contains( stringCampos, searchStr );
		}
	}
}