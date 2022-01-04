package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Item;
	import SDE.Enumerador.EItemTipo;
	
	public class PesquisaItem
	{
		public function PesquisaItem()
		{
		}
		
		public static function pesquisar(searchStr:String, tipo_produto:Boolean, tipo_servico:Boolean):Array
		{
			var arrayItens:Array = [];
			var source:Array = App.single.cache.arrayItem;
			for each (var item:Item in source)
			{
				var condicao_negativa1:Boolean = (!tipo_produto && item.tipo == EItemTipo.produto);
				var condicao_negativa2:Boolean = (!tipo_servico && item.tipo == EItemTipo.servico);
				
				//caso alguma dessas terriveis condições ocorra, então, nós não estamos interessados neste item para nosso resultado
				if (item.desuso || condicao_negativa1 || condicao_negativa2)
					continue;
				
				var arrayStringPesquisas:Array = searchStr.split(' ');
				var arrayValorPesquisas:Array = [
					item.id.toString(), item.rfAuxiliar,
					item.rfUnica, item.marca, item.secao, item.grupo,
					item.complAplic, item.nome
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
					arrayItens.push(item.clone());
			}
			
			return arrayItens;
		}
		
		public static function pegar(idItem:Number):Item
		{
			var retorno:Item = null;
			var source:Array = App.single.cache.arrayItem;
			for each (var item:Item in source)
			{
				if (idItem != item.id)
					continue;
				retorno = item.clone();
				break;
			}
			return retorno;
		}
	}
}