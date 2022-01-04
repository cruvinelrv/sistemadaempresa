// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Ev.EvRetornaArray;
import Core.Sessao;
import Core.Utils.Formatadores;

import SDE.Entidade.CFOP;
import SDE.Entidade.Cliente;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.FachadaServico.FcdMov;

import flash.events.Event;
import flash.events.KeyboardEvent;
import flash.ui.Keyboard;

	[Bindable] private var atualItem:Item = null;
	[Bindable] private var atualIEP:ItemEmpPreco = null;
	[Bindable] private var atualIEE:ItemEmpEstoque = null;
	[Bindable] private var cliente:Cliente = null;
	[Bindable] private var vendedor:Cliente = null;
	
	[Bindable] private var listaMI:Array = [];
	
	private var ttUnitI:Number = 0;
	private var ttUnitF1:Number = 0;
	private var ttUnitF2:Number = 0;
	
	[Bindable] private var idMovImportada:Number = 0;
	private var tipoMovImportada:String = '';
	
	private function create():void
	{
		AlertaSistema.mensagem("Sessao.unica.cfops.length: "+App.single.cache.arrayCFOP.length, true);
		for each (var obj:CFOP in App.single.cache.arrayCFOP)
		{
			if (String(obj.codigo).charAt(0)!="5" && String(obj.codigo).charAt(0)!="6")
				arCfopSaida.addItem(obj);
		}
		
		
		cpCliente.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if (ev.retorno!=null)
					cliente = ev.retorno[0];
			}
		);
		cpVendedor.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if (ev.retorno!=null)
					vendedor = ev.retorno[0];
			}
		);
		grid.addEventListener('deleteRow',
			function(ev:Event):void
			{
				var obj:Object = ev.target.data;
		    	//remove da grid
		    	var pos:int = listaMI.indexOf(obj);
		    	listaMI.splice(pos, 1);
		    	//atualiza o dataprovider
		    	grid.dataProvider = listaMI;
		    	exibeTotaisPinta();
			}
		);
		limpaTela();
		exibeTotaisPinta();
	}
	private function limpaTela():void
	{
		cpVendedor.pesquisaInternaID( Sessao.unica.idClienteFuncionarioLogado );
		cpCliente.pesquisaInternaCPF( "00000000000" );
		nsDescontoFinal.value = 0;
		nsDescontoFinalpct.value = 0;
		vs.selectedIndex = 0;
		idMovImportada=0;
		tipoMovImportada='';
		limpaAtual();
		//txtBarras.setFocus();
	}
	
	private function txtBarrasKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		if (txtBarras.text.length<3)
			return;
		
		//pesquisa Cod Barras
		FcdMov.unica.LoadItemEstoque(
			Sessao.unica.idEmp,
			txtBarras.text,
			function(retorno:Array):void
			{
				if (retorno==null)
				{
					AlertaSistema.mensagem("Não encontrado");
					txtBarras.setSelection(0 , txtBarras.text.length);
					return;
				}
				atualItem = retorno[0];
				atualIEE = retorno[1];
				atualIEP = retorno[2];
				lblItemAtual.text = atualItem.nome+", "+atualIEE.identificador;
				
				nsQtd.value=1;
				nsVlr.value = atualIEP.venda;
				
				txtBarras.text='';
				nsQtd.setFocus();
			}
		);
		
	}
	
	private function retornaEstoque(ev:EvRetornaArray):void
	{
		if (ev.retorno==null)
		{
			AlertaSistema.mensagem("produto não encontrado");
			return;
		}
		atualItem	= ev.retorno[0];
		atualIEE	= ev.retorno[1];
		atualIEP	= ev.retorno[2];
		lblItemAtual.text = atualItem.nome+", "+atualIEE.identificador;
		
		nsQtd.value=1;
		nsVlr.value = atualIEP.venda;
		
		txtBarras.text='';
		nsQtd.setFocus();
	}
	
	private function retornaMov(ev:EvRetornaArray):void
	{
		var mov:Mov = ev.retorno[0];
		
		listaMI.splice(0, listaMI.length);
		//grid.dataProvider = listaMI;
		
		idMovImportada = mov.id;
		tipoMovImportada=mov.tipo;
		//cbCancelarImportada.selected=false;
		
		for each (var mi:MovItem in mov.__mItens)
		{
			/*
			mi.qtd;
			mi.vlrUnitVendaInicial;
			mi.vlrUnitVendaFinal;
			*/
			
			lanca_inner( mi );
		}
		
		limpaAtual();
		
	}
	
	
	
	
	
	
	
	
	
	
	
	/** getters e setters */
	
	private var acrescimo:Number = 0;
	
	private function get unit():Number {return nsVlr.value;}
	private function get qtd():Number {return nsQtd.value;}
	private function get total():Number {return nsVlrTot.value;}
	
	private function set unit(v:Number):void {nsVlr.value=v;}
	private function set qtd(v:Number):void {nsQtd.value=v;}
	private function set total(v:Number):void {nsVlrTot.value=v;}
	
	public function altereiQtdUnit():void
	{
		total = qtd*unit;
		_altereiValor();
	}
	public function altereiTotal():void
	{
		qtd = nsQtd.value;
		unit = total/qtd;
		_altereiValor();
	}
	private function _altereiValor():void
	{
		acrescimo = 0;
		lblAcrescimo.text="";
		try
		{			
			acrescimo = (unit - atualIEP.venda) * qtd;
			if (acrescimo>0)
				lblAcrescimo.text = Formatadores.unica.formataValor(acrescimo, true);
			else if (acrescimo<0)
				lblAcrescimo.text = Formatadores.unica.formataValor(-acrescimo, true);
			
		}
		catch(err:Error)
		{
		}
	}
	
	private function nsQtdKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		lancarAtual();
	}
	private function lancarAtual():void
	{
		if (atualItem==null)
		{
			AlertaSistema.mensagem("Selecione um Produto");
			return;
		}
		if (qtd==0)
		{
			AlertaSistema.mensagem("Quantidade não pode ser zero");
			return;
		}
		if (unit==0)
		{
			AlertaSistema.mensagem("Valor não pode ser zero");
			return;
		}
		
		var mie:MovItemEstoque = new MovItemEstoque();
		mie.idIEE = atualIEE.id;
		//mie.__iee = atualIEE;
		mie.identificador = atualIEE.identificador;
		mie.qtd = qtd;
		//mie.idEmp = Sessao.unica.idEmp;
		
		//Cria Mov Item
		var mi:MovItem = new MovItem();
		mi.idItem = atualItem.id;
		mi.__item = atualItem;
		mi.__mIEstoques = [mie];
		
		mi.qtd = qtd;
		mi.idClienteFuncionario = Sessao.unica.idClienteFuncionarioLogado;
		
		mi.cfop = cmbCFOP.selectedItem.codigo;//propriedade codigo
		
		//valores reais
		//compra original, custo original, venda original, venda digitado pelo usuário
		mi.vlrUnitCompra = atualIEP.compra;
		mi.vlrUnitCusto = atualIEP.custo;
		mi.vlrUnitVendaInicial = atualIEP.venda;
		mi.vlrUnitVendaFinal = unit;
		
		mi.vlrFrete = nsFrete.value;
		mi.vlrSeguro= nsSeguro.value;
		
		lanca_inner(   mi   );
		/** Limpar */
		limpaAtual();
	}
	/**
	 * o motivo de essa função existir, é para desacoplar o lançamento de um item
	 * assim, podemos lançar itens buscando-os um-a-um
	 * ou sequencialmente usando os MI de uma MOV,
	 * (você ainda poderá fazer outras formas caso seja necessario)
	 * 
	 * */
	private function lanca_inner( miLancado:MovItem ):void
	{
		miLancado.id=0;
		miLancado.idMov=0;
		miLancado.idClienteFuncionario = vendedor.id;
		//joga na lista para o grid
		var o:Object = {};
		o.movItem = miLancado;
		o.qtd = miLancado.qtd;
		o.nome = miLancado.__item.nome;
		o.unit = miLancado.vlrUnitVendaFinal;
		o.total = Math.round(miLancado.qtd * miLancado.vlrUnitVendaFinal*100)/100;
		listaMI.push(o);
		grid.dataProvider = listaMI;
	}
	
	private function limpaAtual():void
	{
		atualItem= null;
		atualIEE = null;
		atualIEP = null;
		lblItemAtual.text="";
		txtBarras.text="";
		txtBarras.setFocus();
		unit=0;
		qtd=0;
		exibeTotaisPinta();
	}
	
	private function exibeTotaisPinta():void
	{
		//var ttQtd:Number = 0;
		ttUnitI = 0;
		ttUnitF1 = 0;
		ttUnitF2 = 0;
		
		for each(var o:Object in listaMI)
		{
			var mi:MovItem = o.movItem;
			//ttQtd   += mi.qtd;
			ttUnitI += mi.qtd * mi.vlrUnitVendaInicial;
			ttUnitF1 += mi.qtd * mi.vlrUnitVendaFinal;
		}
		
		nsDescontoFinal.maximum = ttUnitF1;//não deixa dar desconto além
		ttUnitF2 = ttUnitF1 - nsDescontoFinal.value;
		
		txtMostraTotalBru.text = Formatadores.unica.formataValor(ttUnitI, true);
		txtMostraTotalLiq1.text = Formatadores.unica.formataValor(ttUnitF1, true);
		txtMostraTotalLiq2.text = Formatadores.unica.formataValor(ttUnitF2, true);
		txtMostraTotalAcr.text = Formatadores.unica.formataValor(ttUnitF2-ttUnitI, true);
		
		//não podemos exibir mensagens em caso de valor zerado
		//pois esse método é chamado de vários lugares
		//só cria seletores para valor > 0
		seletorEspecies.limpar( ttUnitF2 );
		
		boxFechamento1.enabled = (listaMI.length>0);
		boxFechamento2.enabled = boxFechamento1.enabled;
		grid.dataProvider = listaMI;
	}
		
	private function keyDownDescontoFinal(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			focusOutDescontoFinal(ev);
	}
	private function focusOutDescontoFinal(ev:Event):void
	{
		AlertaSistema.mensagem(ev.currentTarget.name);
		
		if (ev.currentTarget == nsDescontoFinal)
		{
			nsDescontoFinalpct.value = 100 * nsDescontoFinal.value / ttUnitF1;
		}
		else if (ev.currentTarget == nsDescontoFinalpct)
		{
			nsDescontoFinal.value = nsDescontoFinalpct.value * ttUnitF1 / 100;
		}
		exibeTotaisPinta();		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	