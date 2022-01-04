package Core.ConexaoDesktop
{
	import Core.Alerta.AlertaSistema;
	import Core.Ev.EventoGenerico;
	
	import flash.events.EventDispatcher;
	import flash.net.LocalConnection;
	
	import mx.controls.Alert;
	import mx.core.Application;
	
	[Event(name="RetornoTEF", type="Core.Ev.EventoGenerico")]
	[Event(name="MudaEstadoProxy")]
	public class ConnDesktop extends EventDispatcher
	{
		
		private var LCSender:LocalConnection;
		private var LCReceiver:LocalConnection;
		private var LCReceiverConnectionName:String;
		private var LCSenderConnectionName:String;
		
		public static var RetornoTEF:String="RetornoTEF";
		
		[Bindable] public var proxyEstaAberto:Boolean = false;
		
		
		
		public function ConnDesktop(gerente:GerenteConexaoDesktop, receiverweb:String,receiverproxy:String)
		{
			//conecta();
			if (LCSender!=null)
				return;
			
			//aqui é a web, vou receber na 'web' e enviar para o 'proxy'
			LCReceiverConnectionName = "_sde"+receiverweb;
			LCSenderConnectionName = "_sde"+receiverproxy;
			//Alert.show("proxy habilitado\nsender: "+LCSenderConnectionName+"\nreceiver:"+LCReceiverConnectionName);
			
			//enviador de dados
			LCSender = new LocalConnection();
			//receptor de dados
			LCReceiver = new LocalConnection();
			LCReceiver.client = {};
			LCReceiver.client.proxy_chama_web_executar = this.proxy_chama_web_executar;
			LCReceiver.allowDomain("*");
			LCReceiver.connect(LCReceiverConnectionName);
			
		}
		public function proxyEnvia(...parametros):void
		{
			LCSender.send(LCSenderConnectionName, "web_chama_proxy_executar", parametros);
			//localConnSender.send("_sdeConnectionDesktop", funcao, parametros);
		}
		public function proxyPinga():void
		{
			LCSender.send(LCSenderConnectionName, "web_pinga_proxy");
		}
		
		//private var idIntervaloProxy:uint = 0;
		private function proxyPingouWeb():void
		{
			AlertaSistema.mensagem("O SDE está conectado com os periféricos deste computador");
			//Application.application.setStyle('backgroundColor', 0xffffff*Math.random());
			if (!proxyEstaAberto)
				Application.application.ProxyEstaAberto = true;
			proxyEstaAberto = true;
		}
		
		/**
		 * A FUNÇÃO ABAIXO É CHAMADA PELO PROXY, DEVE SER MANTIDA AQUI
		 * 
		 * */
		private function proxy_chama_web_executar():void
		{
			var args:Array = arguments[0];
			while (args[0] is Array)
				args = args[0];
			
			var comando:String = String(args[0]);
			
			/**
			 * no caso da web, se o proxy estiver aberto, o desktop também estará
			 * no caso do desktop, se o proxy estiver aberto, a web talvez não estará
			 **/
			//todas as funções abaixo chamam a web
			
			switch(comando)
			{
				case "ping":
					proxyPingouWeb();
					break;
				case "desktop_chama_web_retornotef":
					//
					var ev:EventoGenerico = new EventoGenerico(ConnDesktop.RetornoTEF);
					ev.number = args[1];
					this.dispatchEvent( ev );
					//Alert.show("retorno do tef: "+args[1]);
					//
					break;
				default:
					Alert.show(comando,"comando não tratado");
					break;
			}
		}
		/*
		public function Fecha():void
		{
			if (localConnReceiver!=null)
				return;
			
			localConnReceiver.close();
			localConnReceiver=null;
			localConnSender=null;
		}
		*/
	}
}