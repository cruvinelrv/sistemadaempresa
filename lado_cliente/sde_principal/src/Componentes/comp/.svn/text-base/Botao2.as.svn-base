package Componentes.comp
{
	import Core.Utils.EsquemaCores;
	
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import img.Imagens;
	
	import mx.controls.Button;
	
	public class Botao2 extends Button
	{
		public function Botao2()
		{
			super();
			minWidth = 150;
			labelPlacement="right";
			buttonMode = true;
			mouseChildren = true;
			useHandCursor = true;
			styleName="padding3";
			setStyle("fontSize",16);
			setStyle("textAlign","left");
			setStyle("fontWeight","bold");
			setStyle("color", 0xffffff);
			setStyle("textRollOverColor", 0xffffff);
			setStyle("textSelectedColor", 0xffffff);
			setStyle("fillAlphas", [1.0, 1.0, 1.0, 1.0]);
			/**/
			setStyle("fillColors", [0x666666, 0x999999]);
			setStyle("themeColor", 0x666666);
			/**/
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
		
		private var _cor:String;
		private var _icone:String;
		public function get cor():String
		{
			return _cor;
		}
		public function get icone():String
		{
			return _icone;
		}
		[Inspectable(category="SisEmpresa", enumeration="Branco,Azul,Vermelho,Verde,Amarelo,Cinza", defaultValue="Branco")]
		public function set cor(v:String):void
		{
			_cor = v;
			var esquema:Array = EsquemaCores.getCores(v);
			if (esquema!=null)
			{
				setStyle("fillColors", esquema);
				setStyle("themeColor", esquema[1]);
			}
			else
				setStyle("color", 0x333333);
		}
		[Inspectable(category="SisEmpresa", 
			enumeration="Fecha,Salva,Pesquisa,Adiciona,Deleta,Aceita",
			defaultValue="")]
		public function set icone(v:String):void
		{
			_icone = v;
			var xxx:Class=null;
			switch (v)
			{
				case "Salva":
					xxx = Imagens.unica.icn_32_salva;
					break;				
				case "Fecha":
					xxx = Imagens.unica.icn_32_close;
					break;
				case "Pesquisa":
					xxx = Imagens.unica.icn_32_pesquisa;
					break;
				case "Adiciona":
					xxx = Imagens.unica.icn_32_adiciona;
					break;
				case "Deleta":
					xxx = Imagens.unica.icn_32_deleta;
					break;
				case "Aceita":
					xxx = Imagens.unica.icn_32_aceita;
					break;
			}
			if (xxx!=null)
				setStyle("icon", xxx );
		}
		
	}
}