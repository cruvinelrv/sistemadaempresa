package Componentes.CaixaPesquisa.CP
{
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaCfop;
	
	import Core.FoldPopup.FoldPopup;
	
	public final class CPesqCFOP extends SuperCaixaPesquisaTxt
	{
		public function CPesqCFOP()
		{
			var cor:uint = 0x66aa66;
			
			super(cor);
		}
		
		public function set btnLabel(v:String):void
		{
			btn.label = v;
		}
		
		
		
		override protected function definePesquisa():void
		{
			var conteudo:ConteudoPesquisaCfop= new ConteudoPesquisaCfop();
			conteudo.setParametros(fRetorno);
			FoldPopup.gerente.setConteudo(conteudo, fCancela);
		}
	}
}