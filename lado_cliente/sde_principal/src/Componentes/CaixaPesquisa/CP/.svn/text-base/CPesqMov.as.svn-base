package Componentes.CaixaPesquisa.CP
{
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaMov;
	
	import Core.Alerta.AlertaSistema;
	import Core.FoldPopup.FoldPopup;
	
	import SDE.Parametro.ParamFiltroMov;
	import SDE.Parametro.ParamLoadMov;
	
	public final class CPesqMov extends SuperCaixaPesquisaTxt
	{
		public function CPesqMov()
		{
			var cor:uint = 0xaa6666;
			
			super(cor);
			txt.onlyRestrict = txt.ONLY_NUMBER;
			widthTxt = 80;
		}
		public var pFiltro:ParamFiltroMov = new ParamFiltroMov();
		public var pLoad:ParamLoadMov = new ParamLoadMov();
		
		override public function pesquisa(ev:Object=null):void
		{
			//Template Method Pattern
			definePesquisa();
			
			var idMov:Number = Number(txt.text);
			if (idMov>0)
			{
				AlertaSistema.mensagem( "pesquisar mov "+idMov );
				pFiltro.idMov = idMov;
				FoldPopup.gerente.getConteudo().Pesquisa();
			}
			else
			{
				pFiltro.idMov = 0;
				FoldPopup.gerente.mostra();
			}
		}
		
		override protected function definePesquisa():void
		{
			var conteudo:ConteudoPesquisaMov = new ConteudoPesquisaMov();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gerente.setConteudo(conteudo, fCancela);
		}
		override public function limpa():void
		{
			txt.text="0";
		}
		
	}
}