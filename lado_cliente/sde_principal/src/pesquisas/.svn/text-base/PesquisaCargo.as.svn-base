package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Cargo;
	
	public class PesquisaCargo
	{
		public function PesquisaCargo()
		{
		}
		
		public static function pequisar(searchStr:String):Array
		{
			var arrayCargos:Array = [];
			var source:Array = App.single.cache.arrayCargo;
			for each (var cargo:Cargo in source)
			{
				var arrayStringPesquisas:Array = searchStr.split(' ');
				var arrayValorPesquisas:Array = [cargo.id, cargo.nomeCargo];
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
					arrayCargos.push(cargo.clone());
			}
			
			return arrayCargos;
		}
	}
}