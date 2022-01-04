package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Finan_TituloItem;
	import SDE.Entidade.Mov;
	import SDE.Enumerador.ETituloSituacao;
	
	import com.hillelcoren.utils.StringUtils;
	
	import mx.collections.ArrayCollection;
	
	public class PesqTituloItem extends SuperCP{
		
		private var dp:ArrayCollection = new ArrayCollection();
		
		public function PesqTituloItem(){
			super();
			dp.source = App.single.cache.arrayFinan_TituloItem;
			//prompt = "Selecione um Lançamento ("+dp.length+")";
			dataProvider = dp;
			camposPesquisa = [Finan_TituloItem.campo_identificador];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
			filterFunction = myFilterFunction;
		}
		
		private function myLabelFunction(finanTituloItem:Finan_TituloItem):String{
			var idCliente:Number;
			var idTransacao:Number = App.single.cache.getFinan_Titulo(finanTituloItem.idTitulo).idTransacao;
			
			for each (var xxx:Mov in App.single.cache.arrayMov){
				if (idTransacao != xxx.idTransacao)
					continue;
				idCliente = xxx.idCliente;
				break;
			}
			return finanTituloItem.identificador +" - "+ App.single.cache.getCliente(idCliente).nome;
		}
		
		public function myFilterFunction(xxx:Finan_TituloItem, searchStr:String):Boolean{
			if (searchStr.length == 0 && xxx.situacao == ETituloSituacao.em_aberto){
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa == null)
				stringCampos = labelFunction(xxx);
			else
				for each (var campo:String in camposPesquisa){
					stringCampos += xxx[campo] + " ";
				}
			return (StringUtils.contains(stringCampos, searchStr) && xxx.situacao == ETituloSituacao.em_aberto);
		}
	}
}