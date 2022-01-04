package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Item;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;

	public class PesqItem extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqItem()
		{
			super();
			dp.source = App.single.cache.arrayItem;
			prompt="Selecione um Item ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa =
				[
					Item.campo_id, Item.campo_secao, Item.campo_grupo, Item.campo_subgrupo, Item.campo_marca,
					Item.campo_modelo, Item.campo_rfUnica, Item.campo_rfAuxiliar, Item.campo_nome,
					Item.campo_complAplic, Item.campo_obs, Item.campo_classificacaoFiscal, Item.campo_complAplic
				];
			browserFields = [
					Item.campo_id, Item.campo_secao, Item.campo_grupo, Item.campo_subgrupo, Item.campo_marca,
					Item.campo_modelo, Item.campo_rfUnica, Item.campo_rfAuxiliar, Item.campo_nome,
					Item.campo_complAplic, Item.campo_obs, Item.campo_classificacaoFiscal
				];
			labelField = Item.campo_nome;
			filterFunction = myFilterFunction;
		}
		
		public function atualiza():ArrayCollection{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var item:Item in App.single.cache.arrayItem)
				if (!item.desuso)
					dp.addItem(item);
			return dp;
		}
		
		public function myFilterFunction(xxx:Item, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.desuso == false){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa==null)
				stringCampos = labelFunction( xxx );
			else
				for each(var campo:String in camposPesquisa){
					stringCampos+=xxx[campo]+" ";
				}
			return (StringUtils.contains( stringCampos, searchStr ) && xxx.desuso == false);
		}
		
	}
}