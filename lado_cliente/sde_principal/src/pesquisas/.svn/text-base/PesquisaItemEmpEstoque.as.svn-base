package pesquisas
{
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.ItemEmpEstoque;
	
	public class PesquisaItemEmpEstoque
	{
		public function PesquisaItemEmpEstoque()
		{
		}
		
		public static function pegarArrayPorCodigoBarras(idEmp:Number, codBarras:String):Array
		{
			var arrayItemEmpEstoque:Array = [];
			var source:Array = App.single.cache.arrayItemEmpEstoque;
			for each (var itemEmpEstoque:ItemEmpEstoque in source)
			{
				if (itemEmpEstoque.idEmp != Sessao.unica.idEmp)
					continue;
				
				var arrayStringPesquisas:Array = [idEmp.toString(), codBarras];
				var arrayValorPesquisas:Array = [itemEmpEstoque.idEmp, itemEmpEstoque.codBarras];
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
					arrayItemEmpEstoque.push(itemEmpEstoque.clone());
			}
			
			return arrayItemEmpEstoque;
		}
		
		public static function pegarArrayPorItem(idEmp:Number, idItem:Number):Array
		{
			var arrayItemEmpEstoque:Array = [];
			var source:Array = App.single.cache.arrayItemEmpEstoque;
			for each (var itemEmpEstoque:ItemEmpEstoque in source)
			{
				if (idEmp != itemEmpEstoque.idEmp || idItem != itemEmpEstoque.idItem)
					continue;
				arrayItemEmpEstoque.push(itemEmpEstoque.clone());
			}
			return arrayItemEmpEstoque;
		}

	}
}