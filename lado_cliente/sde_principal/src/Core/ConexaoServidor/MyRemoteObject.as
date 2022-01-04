package Core.ConexaoServidor
{
	import Core.Alerta.AlertaSistema;
	
	import mx.controls.Alert;
	import mx.events.CloseEvent;
	import mx.rpc.AbstractOperation;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	
	public final class MyRemoteObject extends RemoteObject
	{
		private var isDesconectado:Boolean = false;
		private var tentativasConexao:Number = 0;
		
		private var alert:Alert;
		
		public function MyRemoteObject(source:String)
		{
			super("GenericDestination");
			this.source = source;
			//requestTimeout = 25000;
			requestTimeout = 30000;
			this["showBusyCursor"] = true;
			this["makeObjectsBindable"] = false;
		}
		protected function _faultHandler(ev:FaultEvent):void
		{
			/*
			if (ev.fault.faultCode=="Channel.Call.Failed")
				Alert.show("Não foi possivel conectar-se ao servidor\nVerifique sua conexão com a internet", "Erro de Conexão");
			else
			if (ev.fault.faultCode=="Client.Error.MessageSend")
				Alert.show("Aparentemente nossos servidores estão em manutenção\nA melhor coisa a fazer é fechar o SDE,\naguardar 60 segundos\ne abrí-lo novamente", "Atualização");
			else
				Alert.show(ev.fault.faultString, "ERRO "+ev.fault.faultCode);
				*/
			
			
			if (ev.fault.faultCode == "Channel.Call.Failed")
			{
				tentativasConexao++;
				isDesconectado = true;
			}
			else if (ev.fault.faultCode == "Client.Error.MessageSend")
			{
				tentativasConexao++;
				isDesconectado = true;
			}
			else
			{
				AlertaSistema.mensagem("ERRO: " + ev.fault.faultCode + "\n" + ev.fault.faultString, false, 40000);
				isDesconectado = false;
			}
			
			if (tentativasConexao > 9)
			{
				//showAlert();
				tentativasConexao = 0;
				isDesconectado = false;
			}
			
			if (!isDesconectado)
			{
				tentativasConexao = 0;
			}
			
			
			
			
			/*
			if (ev.fault.faultCode == "Channel.Call.Failed")
			{
				AlertaSistema.mensagem("A conexão com o servidor foi encerrada, o SDE está tentando se reconectar\nTentativa: " + (++tentativasConexao), false, 40000);
				isDesconectado = true;
			}
			else if (ev.fault.faultCode == "Client.Error.MessageSend")
			{
				AlertaSistema.mensagem("Nossos servidores estão em manutenção\nA melhor coisa a fazer é fechar o SDE,\naguardar 60 segundos\ne abrí-lo novamente\nContudo, o SDE continuará tentando se conectar\nTentativa: " + (++tentativasConexao), false, 40000);
				isDesconectado = true;
			}
			else
			{
				AlertaSistema.mensagem("ERRO: " + ev.fault.faultCode + "\n" + ev.fault.faultString, false, 40000);
			}
			
			if (tentativasConexao > 10 || tentativasConexao == 10)
			{
				Alert.show("A conexão com o servidor se tornou instável\nSaia do sistema e entre novamente para que a conexão seja reestabelecida", "ALERTA");
				tentativasConexao = 0;
				isDesconectado = false;
			}
			
			if (!isDesconectado)
			{
				tentativasConexao = 0;
			}
			*/
			//
		}
		public function Invoca(nomeOperacao:String, args:Object, fRetorno:Function):void
		{
			//maisCursor();
			var op:AbstractOperation = getOperation(nomeOperacao);
			op.arguments = args;
			//operations = {};
			/*
		 	var token:AsyncToken = op.send();
		 	token.addResponder(
		 		new Responder(
		 			function(ev:ResultEvent):void
		 			{
						fRetorno(ev.result);//quando void, parametro null não entra na função, ou seja, fRetorno() == fRetorno(null)
		 			},
		 			_faultHandler
		 		)
		 	);
		 	*/
		 	op.send().addResponder(
		 		new Responder(
		 			function(ev:ResultEvent):void
		 			{
						fRetorno(ev.result);//quando void, parametro null não entra na função, ou seja, fRetorno() == fRetorno(null)
		 			},
		 			_faultHandler
		 		)
		 	);
		}
		
		private function showAlert():void{
			var message:String = "Sua conexão com a internet está com problemas, reinicie o moden e o computador.";
			var title:String = "Conexão Instável";
			
			alert = Alert.show(message, title, Alert.OK);
			alert.addEventListener(CloseEvent.CLOSE, alertClose);
		}
		
		private function alertClose(ev:CloseEvent):void{
			switch (ev.detail){
				case Alert.OK:
					logout();
					break;
			}
		}
	}
}