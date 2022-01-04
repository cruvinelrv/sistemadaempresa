package Core.Load
{
	public final class LoadSistema
	{
		private static var gerente:GerenteLoad;
		
		public function LoadSistema()
		{
			super();
		}
		
		public static function loadStart():void
		{
			gerente = new GerenteLoad();
			gerente.start();
		}
		
		public static function loadStop():void
		{
			gerente.stop();
			gerente = null;
		}
	}
}