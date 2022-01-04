package Componentes.CaixaPesquisa.CP
{
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	
	import Core.Alerta.AlertaSistema;
	import Core.ConexaoExterna.BuscaCEP;
	import Core.Ev.EvRetornaArray;
	
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;
	
	public final class CPesqCEP extends SuperCaixaPesquisaTxt
	{
		public function CPesqCEP()
		{
			var cor:uint = 0xaaaa66;
			
			super(cor);
			txt.width = 115;
			txt.inputMask="99999-999";
			btn.width = 90;
			btn.label = "CEP";
		}
		/*
		public var pFiltro:ParamFiltroCliente = new ParamFiltroCliente();
		public var pLoad:ParamLoadCliente = new ParamLoadCliente();
		*/
		
		[Bindable] public function get CEP():String
		{
			return txt.text;
		}
		public function set CEP(v:String):void
		{
			txt.text=v;
		}
		
		override public function pesquisa(ev:Object=null):void
		{
			new BuscaCEP().Busca(
				txt.text,
				function(cepObj:Object):void
				{
					if (cepObj.resultado==1)
					{
						dispatchEvent(new EvRetornaArray([cepObj]));
					}
					else
					{
						AlertaSistema.mensagem("não encontrado");
					}
				},
				function(ev:FaultEvent):void
				{
					Alert.show(ev.toString(), "ERRO");
				}
			);
			
		}
		
		override protected function definePesquisa():void
		{
			/*
			var conteudo:ConteudoPesquisaCliente = new ConteudoPesquisaCliente();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gereciador.setConteudo(conteudo, fCancela);
			pFiltro.texto = txt.text;
			*/
		}
		
	}
}