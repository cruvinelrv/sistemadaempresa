package Core.Utils
{
	import Core.App;
	
	import SDE.Entidade.Item;
	
	public final class Pesquisas
	{
		public static function PesquisaItem(searchStr:String):Array
		{
			var arrayItens:Array = [];
				var source:Array = App.single.cache.arrayItem;
				for each (var item:Item in source){
					if (!item.desuso){
						var arrayStringPesquisas:Array = searchStr.split(' ');
						var arrayValorPesquisas:Array = [
							item.id.toString(), item.rfAuxiliar,
							item.rfUnica, item.marca, item.secao, item.grupo,
							item.complAplic, item.nome
							];
						var contador:Number = 0;
						for each (var strPesq:String in arrayStringPesquisas){
							for each (var strValor:String in arrayValorPesquisas){
								if (strValor == null)
									continue;
								if (strValor.search(strPesq.toUpperCase()) > -1){
									contador++;
									break;
								}
							}
						}
	 					if (contador == arrayStringPesquisas.length)
							arrayItens.push(item);
					}
				}
				
				return arrayItens;
		}

	}
}