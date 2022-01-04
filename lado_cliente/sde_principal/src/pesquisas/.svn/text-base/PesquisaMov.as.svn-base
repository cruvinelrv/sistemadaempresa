package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Mov;
	
	public class PesquisaMov
	{
		public function PesquisaMov()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayMov:Array = [];
			var source:Array = App.single.cache.arrayMov;
			for each (var mov:Mov in source)
			{
				/* if (mov.idMovCancelada != 0 || mov.idMovCanceladora !=0)
					continue; */
				
				var arrayStringPesquisas:Array = searchStr.split();
				var arrayValorPesquisas:Array = [mov.id, mov.cliente_nome, mov.cliente_cpf, mov.dthrMovEmissao];
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
					arrayMov.push(mov.clone());
			}
			return arrayMov;
		}
	}
}