package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cx_Lancamento;
	import SDE.Entidade.Mov;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqTipoLancamentoCaixa extends SuperCP
	{
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqTipoLancamentoCaixa()
		{
			super();
			dp = App.single.cache.arrayCx_Lancamento;
			prompt = "Selecione um Lançamento ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Cx_Lancamento.campo_tipoLancamento];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction(cxL:Cx_Lancamento):String
		{
			var mov:Mov;
			for each (var xxx:Mov in App.single.cache.arrayMov)
			{
				if (cxL.idTransacao != xxx.idTransacao)
					continue;
				mov = xxx;
				break;
			}
			
			return cxL.tipoLancamento +" - "+ App.single.cache.getCliente(mov.idCliente).nome;
		}
		
		private function myFilterFunction(xxx:Cx_Lancamento, searchStr:String):Boolean{
			if (searchStr.length == 0 && validaObjeto(xxx)){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && validaObjeto(xxx));
		}
		
		private function validaObjeto(cxL:Cx_Lancamento):Boolean{
			var mov:Mov;
			for each (var xxx:Mov in App.single.cache.arrayMov)
			{
				if (cxL.idTransacao != xxx.idTransacao)
					continue;
				mov = xxx;
				break;
			}
			
			if (cxL.tipoPagamento_geraContasReceber && App.single.cache.getCliente(mov.idCliente).id != 1)
				return true;
			else
				return false;
		}
				
	}
}