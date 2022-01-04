
	import Core.Alerta.AlertaSistema;
	import Core.FoldPopup.FoldPopup;
	import Core.FoldPopup.GerenteFoldPopup;
	
	import SDE.Entidade.Mov;
	import SDE.Entidade.MovValor;
	import SDE.Enumerador.EMovTipo;
	import SDE.FachadaServico.FcdMov;
	import SDE.Parametro.ParamFiltroMov;
	import SDE.Parametro.ParamLoadMov;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	
	import mx.formatters.DateFormatter;
	
	private var retornoAutomatico:Boolean = true;
	
	private var fRetorno1:Function = null;
	private var paramFiltro:ParamFiltroMov;
	private var paramLoad:ParamLoadMov;
	
	
	
	[Bindable] private var mostrando_reserva:Boolean = false;
	
	
	
	
	private function create():void
	{
		var hoje:Date = new Date();
		dtf1.selectableRange = {
			rangeStart : new Date(2009,0,1),
			rangeEnd : hoje
		};
		dtf2.selectableRange = dtf1.selectableRange;
		
		dtf1.selectedDate = hoje;
		dtf2.selectedDate = hoje;
		
		gridRet.addEventListener('detalhes', movDetalhes);
		gridRet.addEventListener('seleciona', movSeleciona);
		
		
	}
	
	
	private function get gerente():GerenteFoldPopup
	{
		return FoldPopup.gerente;
	}
	
	
	
	
	
	
	private function kDown(ev:KeyboardEvent):void
	{
		if (/*ev.keyCode==Keyboard.END && */ev.ctrlKey && ev.altKey)
		{
			mostrando_reserva = !mostrando_reserva;
		}
		
	}
	
	
	
	
	
	
	
	
	private function btn_click():void
	{
		if (paramFiltro==null)
			paramFiltro=new ParamFiltroMov();
		retornoAutomatico = false;
		
		var fmt:DateFormatter = new DateFormatter();
		fmt.formatString="DD/MM/YYYY";
		
		paramFiltro.dtInicial	= fmt.format( dtf1.selectedDate );
		paramFiltro.dtFinal		= fmt.format( dtf2.selectedDate );
		Pesquisa();
	}
	
	public function setParametros(paramFiltro:ParamFiltroMov, paramLoad:ParamLoadMov, fRetorno1:Function):void
	{
		this.retornoAutomatico=true;
		this.paramFiltro = paramFiltro;
		this.paramLoad = paramLoad;
		this.fRetorno1=fRetorno1;
		
		
	}
	
	public function Pesquisa():void
    {
    	//AlertaSistema.mensagem( "Em desenvolvimento \r"+paramFiltro.dtInicial+"\r"+paramFiltro.dtFinal );
    	//paramFiltro.idMov = 0;
    	//return;
    	
    	if (paramFiltro.idMov > 0)
    	{
    		
    		var pl:ParamLoadMov = new ParamLoadMov();
	    	pl.ignora = true;
	    	pl.movItens = true;
	    	pl.itens = true;
	    	
	    	FcdMov.unica.Load( paramFiltro.idMov, pl,
	    		function (mov:Mov):void
	    		{
	    			fRetorno1([mov]);
	    		}
	    	);
    		return;
    	}
    	
    	//verificacao do paramfiltro
    	if(paramFiltro.tipos == "")
    	{
    		paramFiltro.tipos = EMovTipo.outros_reserva+"_"+EMovTipo.outros_orcamento+"_"+EMovTipo.saida_venda;
    	}
    	
    	//vs1.selectedIndex = 1;
    	loading1.visible=true;
    	
    	FcdMov.unica.Pesquisa(
    		paramLoad, paramFiltro,
			function(retorno:Array):void
			{
				AlertaSistema.mensagem(paramFiltro.tipos);
    			//vs1.selectedIndex = 0;
    			loading1.visible=false;
				if (retorno.length==0 && retornoAutomatico)
					fRetorno1(null);
				else if (retorno.length==1 && retornoAutomatico)
				{
					gerente.fecha();
					fRetorno1(retorno);
					//gerenciador.fecha();
				}
				else
				{
					FoldPopup.gerente.mostra();
					
					var ar:Array = [];
					for each(var mov:Mov in retorno)
					{
						var o:Object = {};
						o.mov = mov;
						o.idMov = mov.id;
						o.numeroNF =mov.numeroNF;
						o.resumo = mov.resumo;
						o.tipo = mov.tipo;
						o.impressao = mov.impressao; 
						o.idCliente = mov.idCliente;
						o.idCliFunc = mov.idClienteFuncionarioLogado;
						o.isNfeEnviada = mov.isNfeEnviada;
						o.isNfePreenchida = mov.isNfePreenchida;
						
						/*
						o.nome = mov.nome;
						o.rfUnica = item.rfUnica;
						o.rfAuxiliar = item.rfAuxiliar;
						o.secao = item.secao;
						o.marca = item.marca;
						//o.modelo = item.modelo;
						o.unidMed = item.unidMed;
						*/
						ar.push(o);
					}
					gridRet.dataProvider = ar;
					//quando temos um retorno, jogamos o foco para a grid
					/*
					if (ar.length>0)
					{
						//seleciona o primeiro, dá foco, dispara evento que busca estoques
						gridItens.selectedIndex=0;
						gridItens.setFocus();
						gridItens.dispatchEvent( new ListEvent(ListEvent.CHANGE, false, false, 0, 1) );
					}
					*/
				}	
			}
		);
    }
    
    
    
    private function movDetalhes(ev:Event):void
    {
    	var o:Object = ev.target.data;
    	//var mov:Mov = o.mov;
    	
    	var pl:ParamLoadMov = new ParamLoadMov();
    	pl.ignora = true;
    	pl.movValores = true;
    	
    	FcdMov.unica.Load( o.idMov, pl,
    		function (mov:Mov):void
    		{
    			var s:String = "Valores:\r";
    			for each (var mv:MovValor in mov.__mValores)
    			{
    				s += mv.especie+": "+mv.valor+"\r";
    			}
    			AlertaSistema.mensagem( s );   			
    		}
    	);
    	
    }
    
    private function movSeleciona(ev:Event):void
    {
		gerente.fecha();
    	var o:Object = ev.target.data;
		//var mov:Mov = o.mov;
		
    	var pl:ParamLoadMov = new ParamLoadMov();
    	pl.ignora = true;
    	pl.movItens = true;
    	pl.itens = true;
    	
    	FcdMov.unica.Load( o.idMov, pl,
    		function (retorno:Mov):void
    		{
    			var thisMov:Mov = o.mov;
    			thisMov.__mItens = retorno.__mItens;
    			
    			fRetorno1([thisMov]);
    		}
    	);
		
    	
    }
    
	/*
	private function gridRet_kdown(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			gridItens_dclick();
	}
	
    private function gridRet_dclick():void
    {
    	if (gridRet.selectedItem==null)
    		return;
		gerente.fecha();
		
		
		var itemAtual:Item = gridItens.selectedItem.item;
		var ieeAtual:ItemEmpEstoque = gridRet.selectedItem.iee;
		
		
		var pl:ParamLoadItem = new ParamLoadItem();
		pl.ignorar = true;
		pl.precos = true;
		
		FcdItem.unica.Load( Sessao.unica.idEmp, itemAtual.id, pl, 
			function(retorno:Item):void
			{
				fRetorno1([itemAtual, ieeAtual, retorno.__ie.__preco]);
			}
		);
		
    }
    */
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    