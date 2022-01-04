package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.ItemEmpPreco;
	
	public class PesquisaItemEmpPreco
	{
		public function PesquisaItemEmpPreco()
		{
		}
		
		public static function pegar(idEmp:Number, idItem:Number):ItemEmpPreco
		{
			var retorno:ItemEmpPreco = null;
			var source:Array = App.single.cache.arrayItemEmpPreco;
			for each (var itemEmpPreco:ItemEmpPreco in source)
			{
				if (idEmp != itemEmpPreco.idEmp || idItem != itemEmpPreco.idItem)
					continue;
				retorno = itemEmpPreco.clone();
				break;
			}
			return retorno;
		}

	}
}