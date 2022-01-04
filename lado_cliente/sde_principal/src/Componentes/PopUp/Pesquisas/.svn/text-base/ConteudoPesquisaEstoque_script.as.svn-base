
	import Core.FoldPopup.FoldPopup;
	import Core.FoldPopup.GerenteFoldPopup;
	import Core.Sessao;
	
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.FachadaServico.FcdItem;
	import SDE.Parametro.ParamFiltroItem;
	import SDE.Parametro.ParamLoadItem;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import mx.events.ListEvent;
	
	private var retornoAutomatico:Boolean = true;
	
	private var fRetorno1:Function = null;
	private var paramFiltro:ParamFiltroItem;
	private var paramLoad:ParamLoadItem;
	
	
	
	[Bindable] private var mostrando_reserva:Boolean = false;
	
	
	
	
	
	
	
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
	
	
	
	
	
	
	
	
	private function txt_KEnter():void
	{
		if (paramFiltro==null)
			paramFiltro=new ParamFiltroItem();
		retornoAutomatico = false;
		paramFiltro.texto=txt.text;
		Pesquisa();
	}
	
	public function setParametros(paramFiltro:ParamFiltroItem, paramLoad:ParamLoadItem, fRetorno1:Function):void
	{
		this.retornoAutomatico=true;
		this.paramFiltro = paramFiltro;
		this.paramLoad = paramLoad;
		this.fRetorno1=fRetorno1;
	}
	
	public function Pesquisa():void
    {
    	//vs1.selectedIndex = 1;
    	loading1.visible=true;
    	
    	paramLoad.precos=true;
    	
    	FcdItem.unica.Pesquisa(
    		Sessao.unica.idEmp,
    		paramFiltro, paramLoad,
			function(retorno:Array):void
			{
    			//vs1.selectedIndex = 0;
    			loading1.visible=false;
				if (retorno.length==0)
					fRetorno1(null);
				else if (retorno.length==1 && retornoAutomatico)
				{
					
					
					
					paramLoad.ignorar = true;
					paramLoad.estoques = true;
					
					FcdItem.unica.Load( Sessao.unica.idEmp, retorno[0].id, paramLoad, 
						function(retorno2:Item):void
						{
							gerente.fecha();
							
							/*
							atualItem	= ev.retorno[0];
							atualIEE	= ev.retorno[1];
							atualIEP	= ev.retorno[2];
							*/
							
							
							fRetorno1([retorno[0], retorno2.__estoques[0], retorno[0].__ie.__preco]);
						}
					);
					
					//gerenciador.fecha();
				}
				else
				{
					FoldPopup.gerente.mostra();
					
					var ar:Array = [];
					for each(var item:Item in retorno)
					{
						var o:Object = {};
						o.item = item;
						o.idItem = item.id;
						o.nome = item.nome
						o.rfUnica = item.rfUnica;
						o.rfAuxiliar = item.rfAuxiliar;
						o.secao = item.secao;
						/*
						o.grupo = item.grupo;
						o.subgrupo = item.subgrupo;
						/**/
						o.marca = item.marca;
						
						o.venda = item.__ie.__preco.venda;
						
						//o.modelo = item.modelo;
						o.unidMed = item.unidMed;
						ar.push(o);
					}
					gridItens.dataProvider = ar;
					//quando temos um retorno, jogamos o foco para a grid
					if (ar.length>0)
					{
						//seleciona o primeiro, dá foco, dispara evento que busca estoques
						gridItens.selectedIndex=0;
						gridItens.setFocus();
						gridItens.dispatchEvent( new ListEvent(ListEvent.CHANGE, false, false, 0, 1) );
					}
				}	
			}
		);
    }
    
    
    
    
    
    
    
	private function gridItens_change(ev:Event):void
	{
		//ev.target.selectedItem.
		//.idItem
		//item.id
		
		var itemAtual:Item = ev.target.selectedItem.item;
		
		var fPreenche:Function =
			function(retorno:Item):void
			{
				AlertaSistema.mensagem( "Quantidade de Estoques: "+retorno.__estoques.length, true );
				itemAtual.__estoques = retorno.__estoques;
				
				var ttEstoque:Number = 0;
				
				var ar:Array = [];
				for each(var iee:ItemEmpEstoque in itemAtual.__estoques)
				{
					var o:Object = {};
					o.iee = iee;
					o.identificador = iee.identificador;
					o.qtd = iee.qtd;
					o.qtdReserva = iee.qtdReserva;
					o.qtdSaldo = iee.qtd - iee.qtdReserva;
					ar.push(o);
					
					ttEstoque += iee.qtd;
				}
				gridRet.dataProvider = ar;
				
				
				lblSomaEstoque.text = "Total de Estoque: "+ttEstoque;
				//vs2.selectedIndex=0;
				//loading2.visible=false;
			}
		
		if (itemAtual.__estoques!=null)
		{
			fPreenche(itemAtual);
			return;
		}
		
		//vs2.selectedIndex=1;
		//loading2.visible=true;
		
		var pl:ParamLoadItem = new ParamLoadItem();
		pl.ignorar = true;
		pl.estoques = true;
		
		FcdItem.unica.Load( Sessao.unica.idEmp, itemAtual.id, pl, fPreenche );
		/*
		 var ar:Array = [];
					for each(var item:Item in retorno)
					{
						var o:Object = {};
						o.item = item;
						o.idItem = item.id;
						o.nome = item.nome
						o.rfUnica = item.rfUnica;
						o.rfAuxiliar = item.rfAuxiliar;
						o.secao = item.secao;
						o.marca = item.marca;
						o.unidMed = item.unidMed;
						ar.push(o);
					}
					
					gridItens.dataProvider = ar; 
		 */
	}
	
	
	private function gridItens_kdown(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			gridItens_dclick();
	}
	
    private function gridItens_dclick():void
    {
    	gridRet.setFocus();
    }
    
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
		
		/*
		atualItem	= ev.retorno[0];
		atualIEE	= ev.retorno[1];
		atualIEP	= ev.retorno[2];
		*/
		fRetorno1([itemAtual, ieeAtual, itemAtual.__ie.__preco]);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    