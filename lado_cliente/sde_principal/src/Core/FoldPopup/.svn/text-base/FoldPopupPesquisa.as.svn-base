package Core.FoldPopup
{
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaCliente;
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaItem;
	
	import SDE.Parametro.ParamFiltroCliente;
	import SDE.Parametro.ParamFiltroItem;
	import SDE.Parametro.ParamLoadCliente;
	import SDE.Parametro.ParamLoadItem;
	
	import mx.core.Application;
	
	public final class FoldPopupPesquisa
	{
		public static function get gereciador():GerenteFoldPopup
		{
			return Application.application.gerenciadorFold;
		}
		
		public static function mostra():void
		{
			//Application.application.gerenciadorFold.mostra(null);
		}
		public static function et1_PesquisaCliente(paramFiltro:ParamFiltroCliente, paramLoad:ParamLoadCliente, fRetorno1:Function, fCancela:Function=null):void
		{
			var conteudo:ConteudoPesquisaCliente = new ConteudoPesquisaCliente();
			conteudo.setParametros(paramFiltro, paramLoad, fRetorno1);
			gereciador.setConteudo(conteudo, fCancela);
		}
		public static function et1_PesquisaItem(paramFiltro:ParamFiltroItem, paramLoad:ParamLoadItem, fRetorno1:Function, fCancela:Function=null):void
		{
			var conteudo:ConteudoPesquisaItem = new ConteudoPesquisaItem();
			conteudo.setParametros(paramFiltro, paramLoad, fRetorno1);
			gereciador.setConteudo(conteudo, fCancela);
		}
		public static function et2_mostra():void
		{
			gereciador.mostra();
		}
		public static function et2_pesquisa():void
		{
			gereciador.getConteudo().Pesquisa();
		}
		
		
		
	}
}