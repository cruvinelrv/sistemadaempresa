package Componentes.comp
{
	import Core.Utils.EsquemaCores;
	
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import img.Imagens;
	
	import mx.controls.Button;

	public class Botao extends Button
	{
		public function Botao()
		{
			super()
			labelPlacement = "right";
			//mãozinha, requer 3 campos
			buttonMode = true;
			mouseChildren = true;
			useHandCursor = true;
			//
			
			styleName = "padding5";
			this.addEventListener(MouseEvent.CLICK, _trata_clica1x);
		}
		private function _trata_clica1x(ev:Event):void
		{
			visible = false;
			setTimeout(function():void{visible=true;}, 1500);
		}
		
		
		
		
		
		
		
		/*
		//private var _cor:String;
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
		}
		
		[Inspectable(category="SisEmpresa", 
			enumeration="Adiciona,Atualiza,Cancela,Pesquisa,Confirma,Salva,SetaBai,SetaDir,SetaEsq",
			defaultValue="")]
		public function set icone(v:String):void
		{
			_icone = v;
			var xxx:Class=null;
			switch (v)
			{
				case "Adiciona":
					xxx = Imagens.unica.icn_16_adiciona;
					break;
				case "Atualiza":
					xxx = Imagens.unica.icn_16_atualiza;
					break;
				case "Cancela":
					xxx = Imagens.unica.icn_16_cancela;
					break;
				case "Pesquisa":
					xxx = Imagens.unica.icn_16_pesquisa;
					break;
				case "Confirma":
					xxx = Imagens.unica.icn_16_confirma;
					break;
				case "Salva":
					xxx = Imagens.unica.icn_16_salva;
					break;
				case "SetaBai":
					xxx = Imagens.unica.icn_16_seta_bai;
					break;
				case "SetaDir":
					xxx = Imagens.unica.icn_16_seta_dir;
					break;
				case "SetaEsq":
					xxx = Imagens.unica.icn_16_seta_esq;
					break;
			}
			if (xxx!=null)
				setStyle("icon", xxx );
		}
		*/
		
		
	}
}