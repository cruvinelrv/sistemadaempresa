package Componentes.CaixaPesquisa.CP
{
	import Core.Alerta.AlertaSistema;
	import Core.FoldPopup.FoldPopup;
	
	import SDE.Parametro.ParamFiltroMov;
	import SDE.Parametro.ParamLoadMov;
	
	import com.flexpernambuco.controls.MasterTextInput;
	
	import flash.events.MouseEvent;
	
	import img.Imagens;
	
	import mx.containers.HBox;
	import mx.controls.Button;
	import mx.controls.DateField;
	import mx.events.FlexEvent;
	
	public final class CPesqMovnaousada extends HBox
	{
		
		protected var dtf:DateField = new DateField();
		protected var btn:Button = new Button();
		
		
		public function CPesqMovnaousada()
		{
			super();
			
			
			var cor:uint = 0xaa6666;
			/* COMPONENTES VISUAIS */
			addChild(dtf);
			addChild(btn);
			//
			
			dtf.selectableRange = {
				rangeStart : new Date(2009,0,1),
				rangeEnd : new Date()
			};
			
			
			
			
			setStyle("backgroundColor", cor );
			setStyle("themeColor", cor );
			setStyle("verticalAlign", "middle" );
			styleName = "padding3";
			
			//dtf.capsType = txt.CAPS_UPPERCASE;
			dtf.width = 130;
			dtf.styleName = "padding3";
			dtf.setStyle("fontSize", 16);
			
			btn.width = 130;
			btn.height = 32;
			btn.label = "Pesquisa";
			btn.setStyle("fontSize", 19);
			btn.setStyle("icon", Imagens.unica.icn_16_pesquisa );
			btn.setStyle("textAlign", "left" );
			/**/
			
			dtf.addEventListener(FlexEvent.ENTER, pesquisa);
			btn.addEventListener(MouseEvent.CLICK, pesquisa);
			
		}
		public var pFiltro:ParamFiltroMov = new ParamFiltroMov();
		public var pLoad:ParamLoadMov = new ParamLoadMov();
		
		
		public function pesquisa(ev:Object=null):void
		{
			AlertaSistema.mensagem( "esse código ainda nao foi feito"
				+dtf.selectedItem
			);
			return;
			/*
			definePesquisa();
			
			if (dtf.text.length==0)
				FoldPopup.gereciador.mostra();
			else
				FoldPopup.gereciador.getConteudo().Pesquisa();
				*/
		}
		
		protected function definePesquisa():void
		{
			
			//pFiltro.
			//pLoad.
			/*
			var conteudo:ConteudoPesquisaEstoque = new ConteudoPesquisaEstoque();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gereciador.setConteudo(conteudo, fCancela);
			pFiltro.texto = txt.text;
			/**/
		}
		
		
		
		
		
		
		
		
		public function set labelBtn(v:String):void
		{
			btn.label = v;
		}
		
		public function set widthBtn(v:Number):void
		{
			btn.width = v;
		}
		
		public function set widthBtnPercent(v:Number):void
		{
			btn.percentWidth = v;
		}
		
		
		
		
		
	}
}