package Componentes.Paginacao
{
	import flash.events.MouseEvent;
	
	import mx.controls.Button;
	
	/**
	 * 
	 * @autor Marcos Rosa
	 *
	 *  
	 * */
 	[Event(name="exibirPagina", type="PaginacaoEvent")]
 	public class BotaoPagina extends Button {
 		
		[Bindable]
		private var _pagina:int;
		
		[Bindable]
		private var _intervaloInicial:int;
		private var _evtPaginacao:PaginacaoEvent;
		
		public function BotaoPagina() {
			super();
		}
		
		override protected function childrenCreated():void{
			super.childrenCreated();
			this.buttonMode = true;
			this.useHandCursor = true;
			/* Define a label do botao com o mesmo numero de pagina */
            if(this.label == '')
                this.label = String(_pagina);
		}
		/**
         * Define a pagina a qual este botao deve amostrar os dados para o usuario.
         * @param int Numero da pagina
         */
     	public function set pagina(value:int):void{
     		_pagina = value;
     	}
     	/**
         * Retorna o numero da pagina na qual este botao deve amostrar os dados.
         * @return
         */
        public function get pagina():int {
            return _pagina;
        }
        /**
         * Define o intervalo Inicial que deverá ser amostrado na nova listagem
         * @param int Numero da pagina
         */
        public function set intervaloInicial(value:int):void {
            _intervaloInicial = value;
        }
        /**
         * @return
         */
        public function get intervaloInicial():int {
            return _intervaloInicial;
        }
        override protected function clickHandler(event:MouseEvent):void{
        	super.clickHandler(event);
        	if (this.selected){
        		_evtPaginacao = new PaginacaoEvent(PaginacaoEvent.EXIBIR_PAGINA);
        		_evtPaginacao.botaoPagina = this;
        		this.dispatchEvent(_evtPaginacao);
        	}
        }
	}
}