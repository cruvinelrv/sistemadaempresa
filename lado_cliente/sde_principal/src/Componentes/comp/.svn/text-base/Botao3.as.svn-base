package Componentes.comp
{
	import Core.Utils.EsquemaCores;
	
	import img.Imagens;
	
	import mx.controls.Button;
	
	public class Botao3 extends Button
	{
		public function Botao3()
		{
			super();
			minWidth = 170;
			labelPlacement="right";
			buttonMode = true;
			mouseChildren = true;
			useHandCursor = true;
			styleName="padding5";
			setStyle("fontSize",48);
			setStyle("textAlign","left");
			setStyle("fontWeight","bold");
			setStyle("color", 0xffffff);
			setStyle("textRollOverColor", 0xffffff);
			setStyle("textSelectedColor", 0xffffff);
			setStyle("fillAlphas", [1.0, 1.0, 1.0, 1.0]);
			/**/
			setStyle("fillColors", [0x666666, 0x999999]);
			setStyle("themeColor", 0x666666);
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
		}
		[Inspectable(category="SisEmpresa", 
			enumeration="Adiciona",
			defaultValue="")]
		public function set icone(v:String):void
		{
			_icone = v;
			var xxx:Class=null;
			switch (v)
			{
				case "Adiciona":
					xxx = Imagens.unica.icn_64_adiciona;
					break;
			}
			if (xxx!=null)
				setStyle("icon", xxx );
		}
		
	}
}