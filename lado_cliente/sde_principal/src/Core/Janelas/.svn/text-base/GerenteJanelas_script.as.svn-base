	// ActionScript file
	import Componentes.PopUpPesquisa.PopPesquisa_Produtos;
	import Componentes.PopUpPesquisa.PopPesquisa_Vendas;
	
	import Core.App;
	import Core.Janelas.FabricaJanela;
	import Core.Sessao;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.ClienteFuncionarioPermissao;
	import SDE.Entidade.ClienteFuncionarioUsuario;
	import SDE.Entidade.SdeConfig;
	
	import com.hillelcoren.utils.StringUtils;
	
	import flexmdi.containers.MDIWindow;
	import flexmdi.effects.effectsLib.MDIVistaEffects;
	import flexmdi.events.MDIManagerEvent;
	
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.core.Container;
	import mx.events.CloseEvent;
	import mx.events.MenuEvent;
	import mx.managers.PopUpManager;
	
	[Bindable] public var menuItens:Array;
	[Bindable] public var versao:String="";
	
	private var rodape:Container;
	
	[Bindable] private var acessaPdv:Boolean = false; 
	
	private function init():void
	{
		setAba(0);
		janelaMdiCanvas.effects = new MDIVistaEffects();
		janelaMdiCanvas.windowManager.addEventListener(MDIManagerEvent.WINDOW_CLOSE, confirmWindowClose);
		rodape = rodapeMXML;
	}
	
	private function permissoesApplicationBarUsuario():void
	{
		var clienteFuncionarioUsuario:ClienteFuncionarioUsuario;
		var clienteFuncionarioPermissao:ClienteFuncionarioPermissao;
		var nuvemCache:NuvemCache = App.single.cache;
		
		for each (var cfu:ClienteFuncionarioUsuario in nuvemCache.arrayClienteFuncionarioUsuario)
		{
			if (cfu.idCliente != Sessao.unica.idClienteFuncionarioLogado)
				continue;
			else
			{
				clienteFuncionarioUsuario = cfu;
				break;
			}
		}
		
		if (clienteFuncionarioUsuario.usuarioTecnico)
			acessaPdv = true;
		else
		{
			for each (var cfp:ClienteFuncionarioPermissao in nuvemCache.arrayClienteFuncionarioPermissao)
			{
				if (cfp.idClienteFuncionarioUsuario != clienteFuncionarioUsuario.id ||
				cfp.variavel != "Menu.Movimentacoes.PDV Titulos" ||
				cfp.idEmp != Sessao.unica.idEmp)
					continue;
				else
				{
					clienteFuncionarioPermissao = cfp;
					break;
				}
			}
			
			if (clienteFuncionarioPermissao)
				acessaPdv = (clienteFuncionarioPermissao.valor == "1");
			else
				acessaPdv = false;
		}
	}
	
	public function alinhaCascata():void
	{
		janelaMdiCanvas.windowManager.cascade();
	}
	public function alinhaQuadros():void
	{
		janelaMdiCanvas.windowManager.tile(true, 5);
	}
	private function setAba(i:int):void
	{
		vsCentral.selectedIndex = i;
		applicationcontrolbar1.visible = (i==2);//apenas visivel quando for 2
	}
	
	/*
	private function telaOpcoes():void
	{
		var mdiWin:MDIWindow = new MDIWindow();
		mdiWin.title = "SDE . Configurações";
		mdiWin.width = 800;
		mdiWin.height= 400;
		mdiWin.addChild( new JanelaJornal() );
		janelaMdiCanvas.windowManager.add(mdiWin);
	}
	private function telaNovidades():void
	{
		var mdiWin:MDIWindow = new MDIWindow();
		mdiWin.title = "SDE . Novidades";
		mdiWin.width = 800;
		mdiWin.height= 400;
		mdiWin.addChild( new JanelaJornal() );
		janelaMdiCanvas.windowManager.add(mdiWin);
	}
	*/
	
	
	
	
	/**
	 * 
	 * LOGIN
	 * 
	 ***/
	
	
	
	
	private var fabrica:FabricaJanela = new FabricaJanela();
	
	private function janelaLogin_trata_retornoLogin(e:EventoGenerico):void
	{
		//Alert.show(  (e.bool) ? 'logou :)' : 'n logou =/'   );
		if (e.bool)
			setAba(1);
	}
	private var atual:Number = 0;
	public function CompletouDownloadUmaTabela(maximo:Number):void
	{	
		if (atual>=maximo)
			return;
		atual++;
		
		//var pct:Number = Math.round(100*100*atual/maximo)/100;
		
		progresso.setProgress(atual, maximo);
		//progresso.label = pct+"%";
		
		//AlertaSistema.mensagem( atual+" / "+maximo );
		
		if (atual>=maximo)
		{
			atual=0;
			setAba(2);
			this.dispatchEvent(new Event("BaixouTodosCache"));
			
			BaixouTodosCache();
		}
	}
	private function BaixouTodosCache():void
	{
		var nc:* = App.single.cache;
		var configuracoesEmpresa:Array = App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel);
		var configuracoesUsuario:Array = [];
		
		//var empresa:Empresa = App.single.cache.getEmpresa(Sessao.unica.idEmp);
		//lblEmpresa.text = App.single.cache.getCliente(empresa.idCliente).apelido_razsoc;
		//lblUsuario.text = App.single.cache.getCliente(Sessao.unica.idClienteFuncionarioLogado).nome;
		 
		//carrega as configurações do usuario logado
		var clienteFuncionarioUsuario:ClienteFuncionarioUsuario;
		for each (var cfu:ClienteFuncionarioUsuario in App.single.cache.arrayClienteFuncionarioUsuario)
		{
			if (cfu.idCliente != Sessao.unica.idClienteFuncionarioLogado || cfu.idEmp != Sessao.unica.idEmp)
				continue;
			clienteFuncionarioUsuario = cfu;
			break;
		}
		for each (var cfp:ClienteFuncionarioPermissao in App.single.cache.arrayClienteFuncionarioPermissao)
		{
			if (cfp.idClienteFuncionarioUsuario != clienteFuncionarioUsuario.id || cfp.idEmp != Sessao.unica.idEmp)
				continue;
			configuracoesUsuario.push(cfp);
		}  
		//
		
		menuItens = [];//{label:"Menu", children:[]};
		var listaMenuStr:Array = [];
		var iSequencia:int = 0;
		
		 
		if (Sessao.unica.modoTecnico)
		{
			for each (var sdeConfig:SdeConfig in configuracoesEmpresa)
			{
				if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))
					continue;
					
				var o:* = fabrica.criaMenu(sdeConfig.variavel);
				if (o == null)
					continue;
				
				var telaSplit:Array = sdeConfig.variavel.split('.');
				var menuStr:String = telaSplit[1];
				var menuInt:int = listaMenuStr.indexOf(menuStr);
				
				if (menuInt == -1)
				{
					menuInt = iSequencia++;
					listaMenuStr.push(menuStr);
					menuItens.push({label:menuStr, children:[]});
				}
				
				var objMenuItem:Object = menuItens[menuInt];
				
				objMenuItem.children.push(
					{label:telaSplit[2], classe:o.classe, labelFull:sdeConfig.variavel}
				);
			}
		}
		else
		{
			for each (var sdeConfig2:SdeConfig in configuracoesEmpresa)
			{
				if (!StringUtils.beginsWith(sdeConfig2.variavel, "Menu"))
					continue;
				if (sdeConfig2.valor == "0")
					continue;
					
				for each (var usuarioConfig:ClienteFuncionarioPermissao in configuracoesUsuario)
				{
					if (usuarioConfig.variavel != sdeConfig2.variavel)
						continue;
					
					if (usuarioConfig.valor == "0")
						break;
					
					var o2:* = fabrica.criaMenu(usuarioConfig.variavel);
					if (o2 == null)
						continue;
					
					var telaSplit2:Array = usuarioConfig.variavel.split('.');
					var menuStr2:String = telaSplit2[1];
					var menuInt2:int = listaMenuStr.indexOf(menuStr2);
					
					if (menuInt2 == -1)
					{
						menuInt2 = iSequencia++;
						listaMenuStr.push(menuStr2);
						menuItens.push({label:menuStr2, children:[]});
					}
					
					var objMenuItem2:Object = menuItens[menuInt2];
					
					objMenuItem2.children.push(
						{label:telaSplit2[2], classe:o2.classe, labelFull:sdeConfig2.variavel}
					);
					
					break;
				}
			}
		}
		
		permissoesApplicationBarUsuario();  
		
		/* 
		for each(var sdeConfig:SdeConfig in configuracoesEmpresa)
		{
			//só deixa passar variaveis que comecem com MENU e não sejam == false
			if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))// || sdeConfig.valor=="false")
				continue;
			//
			var ss:Sessao = Sessao.unica;
			if (sdeConfig.valor=="0" && !ss.modoTecnico)
				continue;
			
			
			
			
			
			var o:* = fabrica.criaMenu(sdeConfig.variavel);
			if (o==null)
				continue;
			
			var telaSplit:Array = sdeConfig.variavel.split('.');
			var menuStr:String = telaSplit[1];
			var menuInt:int = listaMenuStr.indexOf(menuStr);
			//var menuInt:int = menuObj.sequencia;
			if ( menuInt==-1 )
			{
				menuInt = iSequencia++;
				listaMenuStr.push(menuStr);
				menuItens.push( {label:menuStr, children:[]} );
			}
			var objMenuItem:Object = menuItens[menuInt];
			
			objMenuItem.children.push(
				{label:telaSplit[2], classe:o.classe, labelFull:sdeConfig.variavel}
			);
		}   */
	
	}
	
	
	
	
	
	
	
		/*
	private function loginSucesso():void
	{
		currentState = null;
		var ss:Sessao = Sessao.unica;
		lblUsuario.text = Sessao.unica.login.__cliente.nome;
		lblEmpresa.text = Sessao.unica.login.__emp.__cliente.nome;
		AlertaSistema.mensagem( "Bom Dia "+Sessao.unica.login.__cliente.nome.split(' ')[0]+"!" );
		AlertaSistema.mensagem( Sessao.unica.desktopExecutando ? "Desktop Executando" : "Desktop Não Executando", true );
		
		var telas:LoginTelas = Sessao.unica.login.telas;
		
		//Sessao.unica.nuvens.Inicializa();
		telas.tecPrin = Sessao.unica.modoTecnico;
		
		menuItens = [];//{ label: "Menu", children: [] };
		var menuTemp:Array = [];
		
		for each (var campo:String in LoginTelas.getCampos())
		{
			if (telas[campo]==true)
			{
				
				var janela:Object = FabricaJanela.unica.menuJanela(campo);
				if (janela==null)
					continue;
				
				var objMenuItem:Object = menuTemp[janela.menu.index];
				if (objMenuItem==null)
				{
					objMenuItem = {label:janela.menu.nome, children:[], icon:janela.menu.icon};
					menuTemp[janela.menu.index] = objMenuItem;
				}
				
				objMenuItem.children.push(
					{label:janela.nome, janela: janela}
				);
			}
		}
		for each (var o:Object in menuTemp)
		{
			menuItens.push(o);
		}
		popMenu.setStyle("icon", Imagens.unica.icn_32_antena);
	}
	
		*/
	private function logout():void
	{
		Sessao.unica.logOut();
		janelaMdiCanvas.removeAllChildren();
		janelaMdiCanvas.addChild(rodapeMXML);
		setAba(0);	
		//menuItens = null;
	}
	
	/**
	 * 
	 * CRIANDO TELAS
	 * 
	 ***/
	
	private function popMenuItemClickHandler(ev:MenuEvent):void
	{
		if (ev.item.classe!=null)
			NovaJanela(new ev.item.classe(), ev.item.labelFull);
	}
	
	public function NovaJanela(janela:Container, tit:String):void
	{
		var mdiWin:MDIWindow = new MDIWindow();
		mdiWin.width = 900;
		mdiWin.height = 500;
		mdiWin.title = tit;
		mdiWin.addChild(janela);
		
		janelaMdiCanvas.windowManager.add(mdiWin);
	}
	
	/*
	public function MostraJanela(tela:Object):Container
	{
		return _novaTela(tela, true);
	}
	
	private function _novaTela(janela:Object, podeExcederLimite:Boolean=false):Container
	{
		var xxx:int = janelaMdiCanvas.getChildren().length;
		
		if (xxx>=12 && !podeExcederLimite)
		{
			AlertaSistema.mensagem("Você já tem 12 janelas abertas, feche algumas", false, 10000);
			return null;
		}
		
		//cria
		var mdiWin:MDIWindow = new MDIWindow();
		//var mdll:ModuleLoader = new ModuleLoader();
		mdiWin.width = 900;
		mdiWin.height= 500;
		//mdiWin.maximize();
		//mdiWin.maximizeRestoreBtn.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
		
		mdiWin.titleIcon = Imagens.unica.icn_16_salva;
		mdiWin.title = "Menu . " + janela.menu.nome+" . "+janela.nome;
		
		//adiciona
		/*
		var cont:Container = FabricaJanela.unica.instanciaJanela(janela);
		cont.styleName="padding3";
		mdiWin.addChild( cont );
		
		//mdiWin.addChild(mdll);
		janelaMdiCanvas.windowManager.add(mdiWin);
		
		
		//return cont;
		var cont:Container = new Container()
		return new Container();
		
	    mdll.setStyle("paddingTop", 3);
	    mdll.setStyle("paddingLeft", 3);
	    mdll.setStyle("paddingRight", 3);
	    mdll.setStyle("paddingBottom", 3);
	    mdll.horizontalScrollPolicy = ScrollPolicy.AUTO;
	    mdll.verticalScrollPolicy = ScrollPolicy.AUTO;
	    
	}
	*/
	// the flex framework dispatches all kinds of events
    // in order to avoid catching one of those and throwing a coercion error
    // have your listener accept Event and check the type inside the function
    // this is good practice for all Flex development, not specific to flexmdi
    private function confirmWindowClose(ev1:MDIManagerEvent):void
    {
        // this is the line that prevents the default behavior from executing as usual
        // because the default handler checks event.isDefaultPrevented()
        
        if (Sessao.unica.modoTecnico)
        	return;
        
        var queuedEvent:MDIManagerEvent = ev1.clone() as MDIManagerEvent;
        ev1.preventDefault();
        
        Alert.show("Fechar a Janela?", null, 3, null,
            function(ev2:CloseEvent):void
            {
                if(ev2.detail == Alert.YES)
                {
                    janelaMdiCanvas.windowManager.executeDefaultBehavior(queuedEvent);
                }
            }
        );
    }
    
    
    
    
    
    
    /**gerencia Popups*/
    
    private function removePopup(popup:Container):void
    {
    	popup.parent.removeChild(popup);
    }
	
	private function abrePopupPesquisaItens():void
	{
		var popupPesquisaProdutos:PopPesquisa_Produtos = new PopPesquisa_Produtos();
		
		PopUpManager.addPopUp(popupPesquisaProdutos, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupPesquisaProdutos);
	}
	private function abrePopupPesquisaMov():void
	{
		var popupPesquisaMov:PopPesquisa_Vendas = new PopPesquisa_Vendas();
		
		PopUpManager.addPopUp(popupPesquisaMov, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupPesquisaMov);
		popupPesquisaMov.retornaMov = false;
	}