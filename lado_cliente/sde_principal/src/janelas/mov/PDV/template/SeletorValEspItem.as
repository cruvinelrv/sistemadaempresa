package janelas.mov.PDV.template
{
	import SDE.Entidade.MovValor;
	import SDE.Enumerador.EValorEspecie;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.text.engine.FontWeight;
	import flash.ui.Keyboard;
	
	import mx.containers.HBox;
	import mx.controls.Button;
	import mx.controls.ComboBox;
	import mx.controls.NumericStepper;
	
	[Event(name="commitValor")]
	public final class SeletorValEspItem extends HBox
	{
		public static const COMMITVALOR:String="commitValor";
		public function SeletorValEspItem()
		{
			super();
			
			addChild(cmb);
			addChild(ns);
			addChild(btn);
			
			percentWidth = 100;
			setStyle("fontSize", 16);
			setStyle("fontWeight", FontWeight.BOLD);
			
			cmb.dataProvider = EValorEspecie.getCampos();
			cmb.selectedItem = EValorEspecie.dinheiro;
			ns.stepSize = .01;
			ns.width = 100;
			btn.label = "OK";
			
			ns.addEventListener(KeyboardEvent.KEY_DOWN, valueCommit);
			btn.addEventListener(MouseEvent.CLICK, valueCommit);
		}
		
		private var cmb:ComboBox = new ComboBox();
		private var ns:NumericStepper = new NumericStepper();
		private var btn:Button = new Button();
		
		//valor maximo permitido para esse seletor
		public var valorMaximo:Number = 0;
		//valor que o usuario digitou
		public var valorDefinido:Number = 0;
		
		public function defineValor(v:Number):void
		{
			valorMaximo = v;
			valorDefinido = v;
			ns.value = v;
			ns.maximum = v;
			/*
		var fmt:DateFormatter = new DateFormatter();
		fmt.formatString = "DD/MM/YYYY";
		var hoje:String = fmt.format(new Date()); 
			 */
		}
		
		public function getMV():MovValor
		{
			var mv:MovValor= new MovValor();
			mv.valor = valorDefinido;
			mv.especie = String(cmb.selectedItem);
			mv.qtdParcelas = 1;
			return mv;
		}
		
		private function valueCommit(ev:Event):void
		{
			if (ns.value==0)
				return;
			if (ev is KeyboardEvent && KeyboardEvent(ev).keyCode != Keyboard.ENTER)
				return;
			
			valorDefinido = ns.value;
			dispatchEvent(new Event(COMMITVALOR, true));
			
		}
		
		
		
		
		
		
	}
}