package Core.Utils
{
	public final class EsquemaCores
	{
		public function EsquemaCores()
		{
		}
		
		public static function getCores(cor:String):Array
		{
			switch (cor)
			{
				case "Vermelho":
					return [0xF76A6A,0x960707];
				case "Amarelo":
					return [0xF5F968,0x8C9706];
				case "Verde":
					//[0x6FF56B,0x0A9706]
					return [0x58DE00,0x359417];
				case "Azul":
					return [0x6969F8,0x040499];
				case "Cinza":
					return [0x666666, 0x999999];
			}
			return null;
		}
		
	}
}