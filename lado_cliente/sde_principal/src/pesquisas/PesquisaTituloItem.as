package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Finan_TituloItem;
	
	public class PesquisaTituloItem
	{
		public function PesquisaTituloItem()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayTituloItens:Array = [];
			var source:Array = App.single.cache.arrayFinan_TituloItem;
			for each (var tituloItem:Finan_TituloItem in source)
			{
				var arrayStringPesquisas:Array = searchStr.split(' ');
					
				var arrayValorPesquisas:Array =
				[
					tituloItem.identificador
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
					arrayTituloItens.push(tituloItem.clone());
			}
			return arrayTituloItens;
		}
	}
}