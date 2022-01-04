package Componentes.SDE
{
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import mx.controls.Button;

	public class Botao extends Button
	{
		public function Botao()
		{
			super();
			buttonMode = true;
			mouseChildren = false;
			useHandCursor = true;
		}
		
		public function set clica1x(v:Boolean):void
		{
			if (v)
				this.addEventListener(MouseEvent.CLICK, _trata_clica1x);
		}
		private function get clica1x():Boolean{return false;}
		
		private function _trata_clica1x(ev:Event):void
		{
			enabled = false;
			setTimeout(function():void{enabled=true;}, 1500);
		}
	}
}