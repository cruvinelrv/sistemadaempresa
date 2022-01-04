package Componentes.CaixaPesquisa.CP
{
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaEstoque;
	
	import Core.FoldPopup.FoldPopup;
	
	import SDE.Parametro.ParamFiltroItem;
	import SDE.Parametro.ParamLoadItem;
	
	public final class CPesqEstoque extends SuperCaixaPesquisaTxt
	{
		public function CPesqEstoque()
		{
			var cor:uint = 0xaa6666;
			
			super(cor);
		}
		public var pFiltro:ParamFiltroItem = new ParamFiltroItem();
		public var pLoad:ParamLoadItem = new ParamLoadItem();
		
		override protected function definePesquisa():void
		{
			var conteudo:ConteudoPesquisaEstoque = new ConteudoPesquisaEstoque();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gerente.setConteudo(conteudo, fCancela);
			pFiltro.texto = txt.text;
		}
		
	}
}