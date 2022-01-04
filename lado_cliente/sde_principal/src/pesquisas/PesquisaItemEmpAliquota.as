package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.ItemEmpAliquotas;
	
	public class PesquisaItemEmpAliquota
	{
		public function PesquisaItemEmpAliquota()
		{
		}
		
		public static function pegar(idEmp:Number, idItem:Number):ItemEmpAliquotas
		{
			var retorno:ItemEmpAliquotas = null;
			var source:Array = App.single.cache.arrayItemEmpAliquotas;
			for each (var itemEmpAliquota:ItemEmpAliquotas in source)
			{
				if (idEmp != itemEmpAliquota.idEmp || idItem != itemEmpAliquota.idItem)
					continue;
				retorno = itemEmpAliquota.clone();
				break;
			}
			return retorno;
		}

	}
}