package Core.Ev
{
	import flash.events.Event;

	public final class Evento extends Event
	{
		public static const CANCELA:String = "cancela";
		
		public function Evento(tipo:String)
		{
			super(tipo);
		}
		
	}
}