package Core.Ev
{
	import flash.events.Event;

	public final class EvRetornaArray extends Event
	{
		public static const RETORNO:String = "retorno";
		
		public function EvRetornaArray(retorno:Array)
		{
			super(RETORNO);
			this.retorno = retorno;
		}
		public var retorno:Array;
		
	}
}