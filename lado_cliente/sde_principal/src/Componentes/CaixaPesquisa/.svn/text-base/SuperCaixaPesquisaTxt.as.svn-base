package Componentes.CaixaPesquisa
{
	import Core.Ev.EvRetornaArray;
	import Core.FoldPopup.FoldPopup;
	
	import com.flexpernambuco.controls.MasterTextInput;
	
	import flash.events.Event;
	import flash.events.MouseEvent;
	
	import img.Imagens;
	
	import mx.containers.HBox;
	import mx.controls.Button;
	import mx.events.FlexEvent;
	
	[Event(name="retorno", type="Core.Ev.EvRetornaArray")]
	[Event(name="cancela", type="flash.events.Event")]
	public class SuperCaixaPesquisaTxt extends HBox
	{
		//encapsulamento
		protected var txt:MasterTextInput = new MasterTextInput();
		protected var btn:Button = new Button();
		
		//abstração
		protected function definePesquisa():void{}
		
		
		
		
		public static const CANCELA:String = "cancela";
		
		
		
		
		protected function fRetorno(retorno:Array):void
		{
			dispatchEvent(new EvRetornaArray(retorno));
			if (retorno==null)
				seleciona();
			else
				limpa();
		}
		protected function fCancela():void
		{
			dispatchEvent(new Event(CANCELA));
			seleciona();
		}
		
		
		
		
		
		public function SuperCaixaPesquisaTxt(cor:uint)
		{
			super();
			/* COMPONENTES VISUAIS */
			addChild(txt);
			addChild(btn);
			//
			
			setStyle("backgroundColor", cor );
			setStyle("themeColor", cor );
			setStyle("verticalAlign", "middle" );
			styleName = "padding3";
			
			txt.capsType = txt.CAPS_UPPERCASE;
			txt.width = 200;
			txt.styleName = "padding3";
			txt.setStyle("fontSize", 16);
			
			btn.width = 130;
			btn.height = 32;
			btn.label = "Pesquisa";
			btn.setStyle("fontSize", 19);
			btn.setStyle("icon", Imagens.unica.icn_16_pesquisa );
			btn.setStyle("textAlign", "left" );
			/**/
			
			txt.addEventListener(FlexEvent.ENTER, pesquisa);
			btn.addEventListener(MouseEvent.CLICK, pesquisa);
			
		}
		public function pesquisa(ev:Object=null):void
		{
			//Template Method Pattern
			definePesquisa();
			
			if (txt.text.length==0)
				FoldPopup.gerente.mostra();
			else
				FoldPopup.gerente.getConteudo().Pesquisa();
		}
		public function limpa():void
		{
			txt.text="";
		}
		public function seleciona():void
		{
			txt.setFocus();
			txt.setSelection(0, txt.length);
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
		
		public function set widthTxt(v:Number):void
		{
			txt.width = v;
		}
		
		public function set widthTxtPercent(v:Number):void
		{
			txt.percentWidth = v;
		}
		
		
		
		
		
		
		
		
		
	}
}