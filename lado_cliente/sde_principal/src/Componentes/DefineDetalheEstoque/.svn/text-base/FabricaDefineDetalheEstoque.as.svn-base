package Componentes.DefineDetalheEstoque
{
	import SDE.Entidade.Item;
	import SDE.Enumerador.EItemTipoIdent;
	
	public class FabricaDefineDetalheEstoque
	{
		public function FabricaDefineDetalheEstoque()
		{
		}
		 
		public static function fabrica(it:Item):SuperDefineDetalheEstoque
		{
			var retorno:SuperDefineDetalheEstoque;
			if (it.tipoIdent == EItemTipoIdent.identificador)
			{
				var xxx:DefineDetalheEstoque_veiculo = new DefineDetalheEstoque_veiculo();
				retorno = xxx;
			}
			else if (it.tipoIdent == EItemTipoIdent.grade)
			{
				var yyy:DefineDetalheEstoque_grade = new DefineDetalheEstoque_grade();
				retorno = yyy;
			}
			else if (it.tipoIdent == EItemTipoIdent.lote)
			{
				var zzz:DefineDetalheEstoque_lote = new DefineDetalheEstoque_lote();
				retorno = zzz;
			}
			
			return retorno;
		}

	}
}