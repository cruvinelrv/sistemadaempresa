package Componentes.PopUpPesquisa
{
	import Componentes.comp.Botao;
	
	import flash.events.Event;
	import flash.events.MouseEvent;
	
	import img.Imagens;
	
	import mx.containers.HBox;
	import mx.containers.TitleWindow;
	import mx.core.ContainerLayout;
	import mx.events.CloseEvent;
	import mx.events.FlexEvent;
	
	[Event(name="retorna",type="flash.events.Event")]
	public class SuperPopPesq extends TitleWindow
	{
		import Core.Ev.EventoGenerico;
		public function SuperPopPesq()
		{
			super();
			width=1000;
			height=550;
			layout = ContainerLayout.VERTICAL;
			
			
			
			styleName="padding10";
			setStyle("titleStyleName","tituloPopupPesquisa");
			setStyle("borderAlpha",1);
			setStyle("backgroundAlpha",1);
			setStyle("borderColor",0x3B5998); 
			setStyle("themeColor",0x3B5998);
			
			setStyle("border-thickness-bottom", 3);
			setStyle("border-thickness-left", 3);
			setStyle("border-thickness-right", 3);
			setStyle("border-thickness-top",0);
			
			titleIcon = Imagens.unica.icn_32_pesquisa;
			title="Pesquisa";
			showCloseButton=true;
			//addEventListener(FlexEvent.INITIALIZE, fn_ev_init);
			//addEventListener(CloseEvent.CLOSE, fn_ev_close);
		}
		/*
		private function fn_ev_close(e:CloseEvent):void
		{
			//e.bubbles = true;
			//this.dispatchEvent(new CloseEvent(CloseEvent.CLOSE, true));
		}*/
	}
}