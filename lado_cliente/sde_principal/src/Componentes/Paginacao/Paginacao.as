package Componentes.Paginacao
{
	import flash.events.MouseEvent;
	
	import mx.collections.ArrayCollection;
	import mx.controls.listClasses.ListBase;
	import mx.events.ListEvent;
	
	/**
     *
     * @author Marcos Rosa
     *
     */
	public class Paginacao extends PaginacaoUI{
		[Bindable] private var _totalPorPagina:int = 5;
		[Bindable] private var _totalDados:int;
		[Bindable] private var _listaBase:ArrayCollection;
		[Bindable] private var _listaBaseAux:ArrayCollection;
		private var _btnAux:BotaoPagina;
		private var _btnAtual:BotaoPagina;
		private var _listaAlvo:ListBase;
		private var _botoesPagina:Array = [];
		private var totalBotoes:int;
		public function Paginacao(){
			super();
		}
		
		override protected function childrenCreated():void{
			super.childrenCreated();
			/* Adicionando os Listeners */
			this.btnPaginaAnterior.addEventListener(MouseEvent.CLICK, trateExibirPaginaAterior, false, 0, true);
			this.btnProximaPagina.addEventListener(MouseEvent.CLICK, trateExibirProximaPagina, false, 0, true);
			this.cbIntervalo.addEventListener(ListEvent.CHANGE, trateTrocaIntervalo, false, 0, true);
		}
		
		override public function invalidateProperties():void{
			super.invalidateProperties();
			/* Inicializa a configuração da paginação */
            configurarBotoesPagina();
		}
		
		/**
         * @private
         * Fica escutando quando o usuario trocou o total de intervalo de dados a ser amostrado na lista
         * @param evt
         */
         private function trateTrocaIntervalo(ev:ListEvent):void{
         	_totalPorPagina = cbIntervalo.selectedItem.value;
         	/* Inicializa a configuração da paginação */
            configurarBotoesPagina();
         }
         
         /**
         * @private
         * Responsavel por configurar os botoes de paginação na tela.
         */
         private function configurarBotoesPagina():void{
         	/* Calcula a quantidade de botoes que deverao ser criados baseado no total de dados e total de dados por pagina */
            totalBotoes = Math.ceil(totalDados/totalPorPagina);
            /* Se houver BotoesPagina ja configurado no container, deve-se remove-los para
             * recalcular o total de paginas */
            if (containerBtnIntermediarios != null && containerBtnIntermediarios.getChildren() != null){
            	containerBtnIntermediarios.removeAllChildren();
            	_botoesPagina = [];
            }
            /* Realiza um loop criando os botoes */
            for (var i:int = 1; i <= totalBotoes; i++){
            	/* Cria e configura o BotaoPagina */
            	_btnAux = new BotaoPagina();
            	_btnAux.toggle = true;
            	_btnAux.pagina = 1;
            	_btnAux.intervaloInicial = (i - 1) * totalPorPagina;
            	_btnAux.addEventListener(PaginacaoEvent.EXIBIR_PAGINA, trateExibirPagina);
            	_botoesPagina[i] = _btnAux;
            	/* Se for o primeiro BotaoPagina criado deve-se configurar a lista de dados na componente tipo ListBase */
                if (i == 1){
                	_btnAux.selected = true;
                	_btnAtual = _btnAux;
                	configurarListaNaPagina(_btnAux);
                }
                /* Adiciona os botoes no container */
                containerBtnIntermediarios.addChild(_btnAux);
            }
         }
         
         /**
         * @private
         * Configura e renderiza os dados Base para ser amostrado na tela levendo-se em consideração
         * o intervalo passado.
         * @param intervloIncial
         *
         */
         
         private function configurarListaNaPagina(btnAtual:BotaoPagina):void{
         	_listaBaseAux = new ArrayCollection();
         	/* para cada Loop é copiado o objeto que se encontra no intervalo passado como parametro */
            for (var j:int = 0; j < _totalPorPagina; j++){
            	if (btnAtual.intervaloInicial + j < _totalDados)
            		_listaBaseAux.addItem(_listaBase.getItemAt(btnAtual.intervaloInicial + j));
            }
            /* Configurando os botoes de avançar e retornar */
            if (btnAtual.pagina > 1){
            	configBtnPaginaAnterior();
            } else{
            	configBtnPaginaAnterior(false);
            }
            if (btnAtual.pagina == totalBotoes){
            	configBtnProximaPagina(false);
            } else {
            	configBtnProximaPagina();
            }
            /* Define o provider clonado e define na ListBase */
            this._listaAlvo.dataProvider = _listaBaseAux;
         }
         
	     /**
	     * @private
	     * Responsavel por ficar escutando quando o usuario deseja visualizar o conteudo de cada pagina
	     */
	     private function trateExibirPagina(ev:PaginacaoEvent):void{
	     	_btnAtual.selected = false;
	     	_btnAtual = ev.botaoPagina;
	     	configurarListaNaPagina(_btnAtual);
	     }
         
		 /**
		 * @private
		 * Responsavel por exibir os dados da paragina anterior.
		 */
		 private function trateExibirPaginaAterior(ev:MouseEvent):void{
		 	/* Verifica qual a pagina em que esta para pegar a anterior */
		 	var i:int = (_btnAtual.pagina >= i) ? _btnAtual.pagina - 1 : 0;
		 	/* Desmarca o botao atual */
		 	_btnAtual.selected = false;
		 	/* Recupera o proximo botao e marca o mesmo */
		 	_btnAtual = _botoesPagina[i];
		 	_btnAtual.selected = true;
		 	/* Configura a lista na pagina */
		 	configurarListaNaPagina(_btnAtual);
		 }
         
	     /**
	     * @private
	     * Responsavel por amostrar os dados da proxima pagina
	     */
	     private function trateExibirProximaPagina(ev:MouseEvent):void{
	     	/* Verifica qual a pagina em que esta para pegar a proxima */
	     	var i:int = (_btnAtual.pagina < (_botoesPagina.length - 1)) ? _btnAtual.pagina + 1 : _botoesPagina.length;
	     	if (i < _botoesPagina.length){
	     		/* Desmarca o botao atual */
	     		_btnAtual.selected = false;
	     		/* Recupera o proximo botao e marca o mesmo */
	     		_btnAtual = _botoesPagina[i];
	     		_btnAtual.selected = true;
	     		/* Configura a lista na pagina */
	     		configurarListaNaPagina(_btnAtual);
	     	}
	     }
         
	      /**
	     * Define o total de dados que serão amostrados em cada pagina
	     * @param value
	     *
	     */
	    public function set totalPorPagina(value:int):void{
	        _totalPorPagina = value;
	    }
	    /**
	     * Retorna o numero de dados que esta/serão amostrados na tela.
	     * @return
	     */
	    public function get totalPorPagina():int{
	        return _totalPorPagina;
	    }
	    /**
	     * Define o total de dados que serão amostrados em cada pagina
	     * @param value
	     *
	     */
	    public function set totalDados(value:int):void{
	        _totalDados = value;
	    }
         
	     /**
	     * Retorna o numero de dados que esta/serão amostrados na tela.
	     * @return
	     */
	    public function get totalDados():int{
	        return _totalDados;
	    }
	    
	    public function set listaAlvo(value:ListBase):void{
	        _listaAlvo = value;
	    }
	    
	    public function get listaAlvo():ListBase{
	        return _listaAlvo;
	    }
         
	     /**
	     * Define a lista que será utilizada para exibir na paginacao
	     * @param value
	     *
	     */
	    public function set listaBase(value:ArrayCollection):void{
	        _listaBase = value;
	        if(_listaBase){
	            _totalDados = _listaBase.length;
	        }
	    }
         
	     public function get listaBase():ArrayCollection{
	     	return _listaBase;
	     }
	     
	     private function configBtnPaginaAnterior(value:Boolean = true):void{
	     	this.btnPaginaAnterior.enabled = value;
	     }
	     
	     private function configBtnProximaPagina(value:Boolean = true):void{
	     	this.btnProximaPagina.enabled = value;
	     }
	}
}