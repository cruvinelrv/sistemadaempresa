package pesquisas
{
	import Core.App;
	import Core.Utils.Funcoes;
	
	import SDE.Entidade.Cliente;
	
	public class PesquisaFuncionario
	{
		public function PesquisaFuncionario()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayFuncionarios:Array = [];
			var source:Array = App.single.cache.arrayCliente;
			for each (var funcionario:Cliente in source)
			{
				if (funcionario.ehFuncionario)
				{
					var arrayStringPesquisas:Array = searchStr.split(' ');
					var arrayValorPesquisas:Array =
					[
						funcionario.id, funcionario.nome, funcionario.apelido_razsoc, funcionario.cpf_cnpj
					];
					var contador:Number = 0;
					for each (var strPesq:String in arrayStringPesquisas)
					{
						for each (var strValor:String in arrayValorPesquisas)
						{
							if (strValor == null)
								continue;
							if (strValor.search(strPesq.toUpperCase()) > -1)
							{
								contador++;
								break;
							}
						}
					}
					if (contador == arrayStringPesquisas.length)
						arrayFuncionarios.push(funcionario.clone());
				}
			}
			return arrayFuncionarios;
		}
	}
}