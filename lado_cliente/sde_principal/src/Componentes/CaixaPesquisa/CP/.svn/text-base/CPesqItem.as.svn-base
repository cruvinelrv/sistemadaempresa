package Componentes.CaixaPesquisa.CP
{
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaItem;
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	
	import Core.FoldPopup.FoldPopup;
	
	import SDE.Parametro.ParamFiltroItem;
	import SDE.Parametro.ParamLoadItem;
	
	public final class CPesqItem extends SuperCaixaPesquisaTxt
	{
		public function CPesqItem()
		{
			var cor:uint = 0x66aa66;
			
			super(cor);
		}
		public var pFiltro:ParamFiltroItem = new ParamFiltroItem();
		public var pLoad:ParamLoadItem = new ParamLoadItem();
		
		override protected function definePesquisa():void
		{
			var conteudo:ConteudoPesquisaItem = new ConteudoPesquisaItem();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gerente.setConteudo(conteudo, fCancela);
			pFiltro.texto = txt.text;
		}
		
	}
}