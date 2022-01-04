package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Mov;
	import SDE.Enumerador.EMovTipo;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqMov extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqMov()
		{
			super();
			dp.source = App.single.cache.arrayMov;
			prompt = "Selecione uma Movimentação ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Mov.campo_id, Mov.campo_numeroNF, Mov.campo_cliente_nome, Mov.campo_dthrMovEmissao];
			browserFields = [Mov.campo_id, Mov.campo_numeroNF, Mov.campo_cliente_nome, Mov.campo_dthrMovEmissao, Mov.campo_vlrTotal, Mov.campo_tipo];
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction(mov:Mov):String
		{
			return mov.id.toString()+" - "+mov.numeroNF.toString()+" - "+((mov.cliente_nome)?mov.cliente_nome:'')+" - "+ mov.dthrMovEmissao;
		}
		
		private function myFilterFunction(xxx:Mov, searchStr:String):Boolean{
			if (searchStr.length == 0 &&
				(xxx.tipo == EMovTipo.saida_venda || xxx.tipo == EMovTipo.entrada_compra || xxx.tipo == EMovTipo.outros_pedido
				|| xxx.tipo == EMovTipo.outros_reserva || xxx.tipo == EMovTipo.ambos_balan || xxx.tipo == EMovTipo.entrada_devolucao) &&
				xxx.idEmp == App.single.ss.idEmp){
					return true;
				}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) &&
				(xxx.tipo == EMovTipo.saida_venda || xxx.tipo == EMovTipo.entrada_compra || xxx.tipo == EMovTipo.outros_pedido
				|| xxx.tipo == EMovTipo.outros_reserva || xxx.tipo == EMovTipo.ambos_balan) &&
				xxx.idEmp == App.single.ss.idEmp);
		}
		
		public function somenteVenda():ArrayCollection
		{
			dp.removeAll();
			for each (var mov:Mov in App.single.cache.arrayMov){
				if (mov.tipo == EMovTipo.saida_venda)
					dp.addItem(mov);
			}
			prompt = "Selecione a Venda("+ dp.length +")";
			return dp;
		}
	}
}