package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.OrdemServico;
	
	public class PesquisaListaCasamento
	{
		public function PesquisaListaCasamento()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayListas:Array = [];
			var source:Array = App.single.cache.arrayOrdemServico;
			for each (var lista:OrdemServico in source)
			{
				var arrayStringPesquisas:Array = searchStr.split(' ');
				var arrayValorPesquisas:Array = [lista.id, lista.cliente_cpf, lista.cliente_contato, lista.cliente_nome, lista.dthrInicio];
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
					arrayListas.push(lista.clone());
			}
			return arrayListas;
		}
		
		public static function pegar(idListaCasamento:Number):OrdemServico
		{
			var retorno:OrdemServico = null;
			var source:Array = App.single.cache.arrayOrdemServico;
			for each (var lista:OrdemServico in source)
			{
				if (idListaCasamento != lista.id)
					continue;
				retorno = lista.clone();
				break;
			}
			return retorno;
		}

	}
}