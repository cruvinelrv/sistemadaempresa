package Core.Alerta
{
	import Core.Sessao;
	
	import mx.core.Application;
	import mx.core.ScrollPolicy;
	
	public final class AlertaSistema
	{
		private static var gerente:GerenteAlerta;
		
		public static function mensagem(msg:String, apenasSuporte:Boolean=false, delay:Number=5000):void
		{
			if (!gerente)
			{
				gerente = new GerenteAlerta();
				Application.application.addChild(gerente);
				Application.application.horizontalScrollPolicy = ScrollPolicy.OFF;
				Application.application.verticalScrollPolicy = ScrollPolicy.OFF;
			}
			if (!apenasSuporte || Sessao.unica.modoTecnico)
				gerente.mensagem(msg, delay);
		}
	}
}