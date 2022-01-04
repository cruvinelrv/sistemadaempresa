// ActionScript file
import Componentes.DefineDetalheEstoque.FabricaDefineDetalheEstoque;
import Componentes.DefineDetalheEstoque.SuperDefineDetalheEstoque;

import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Ev.EvRetornaArray;
import Core.Sessao;
import Core.Utils.Formatadores;
import Core.Utils.MeuFiltroWhere;

import SDE.Entidade.Cliente;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.Enumerador.EItemTipoIdent;
import SDE.Enumerador.EMovTipo;

import janelas.cadastro.ItemProduto2.JnlCadItemProduto;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;

	[Bindable] private var atualItem:Item = null;
	[Bindable] private var atualIEP:ItemEmpPreco = null;
	[Bindable] private var atualIEE:ItemEmpEstoque = null;
	[Bindable] private var fornecedor:Cliente = null;
	[Bindable] private var transportador:Cliente = null;
	
	[Bindable] private var totalBruto:Number = 0;
	[Bindable] private var qtdEntradas:Number = 0;
	[Bindable] private var totalIPI:Number = 0;
	[Bindable] private var totalDesconto:Number = 0;
	[Bindable] private var totalNota:Number = 0;
	
	[Bindable] private var listaMI:ArrayCollection = new ArrayCollection();
	[Bindable] private var impr:Boolean = true;
	
	private var arrayIEP:Array = new Array();
	
	private function create():void
	{
		gridMI.addEventListener('deleteRowItem',
			function(ev:Event):void
			{
				var obj:Object = ev.target.data;
		    	var pos:int = listaMI.getItemIndex(obj);
		    	listaMI.removeItemAt(pos)
		    	//mudaItens();
		    	gridItensIdentificados.dataProvider = null;
		    	gridItemInfo.dataProvider = null;
		    	detalheEstoque.removeAllChildren();
		    	exibeTotaisPinta();
			}
		);
		limpaAtual();
		exibeTotaisPinta();
		txtNumNota.text = defineNumeroNF();
	}
	
	private function lb_fn_formataValorDataGrid(objeto:Object, dgc:DataGridColumn):String
	{
		var retorno:String;
		if (dgc.dataField == 'compra')
			retorno = Formatadores.unica.formataValor4(objeto.compra);
		else if (dgc.dataField == 'custo')
			retorno = Formatadores.unica.formataValor4(objeto.custo);
		else if (dgc.dataField == 'venda')
			retorno = Formatadores.unica.formataValor4(objeto.venda);
		else if (dgc.dataField == 'desc')
			retorno = Formatadores.unica.formataValor4(objeto.desc);
		else if (dgc.dataField == 'vlrIPI')
			retorno = Formatadores.unica.formataValor(objeto.vlrIPI, true);
		else if (dgc.dataField == 'total')
			retorno = Formatadores.unica.formataValor4(objeto.total);
		
		return retorno;
	}
	
	private function defineNumeroNF():String
	{
		var retorno:Number = 1;
		
		for each (var mov:Mov in App.single.cache.arraycMov)
		{
			if (mov.numeroNF > retorno && mov.tipo == EMovTipo.entrada_compra)
				retorno = mov.numeroNF;
		}
		
		return (++retorno).toString();
	}
	
	private function mudaState(state:String):void
	{
		this.currentState = (state == 'Base state') ? null : state;
	}
	
	private function novoProduto():void
	{
		Application.application.gerenteJanelas.NovaJanela(new JnlCadItemProduto(), "Cadastre seu produto");
	}
	
	private function isImprEnabled():Boolean
	{
		for each (var o:Object in listaMI)
		{
			if (o.qtdLancada > 0)
			{
				return false;
				return
			}
		}
		return true;
	}
	
	private function cpFornecedor_change():void
	{
		if (cpFornecedor.selectedItem != null)
			fornecedor = cpFornecedor.selectedItem as Cliente;
		buscaItemPreco();
	}
	
	private function cpTransportador_change():void
	{
		if (cpTransportador.selectedItem != null)
			transportador = cpTransportador.selectedItem as Cliente;
		buscaItemPreco();
	}
	
	private function cpItem_change():void
	{
		if (cpItem.selectedItem != null)
			atualItem = cpItem.selectedItem as Item;
		buscaItemPreco();
	}
	
	private function cpCodBarras_change():void
	{
		if (cpCodigoBarras.selectedItem != null)
		{
			var iee:ItemEmpEstoque = cpCodigoBarras.selectedItem as ItemEmpEstoque;
			atualItem = App.single.cache.getItem(iee.idItem);
		}
		buscaItemPreco();
	}
	
	private function buscaItemPreco():void
	{
		if (atualItem != null)
		{
			var filtro:MeuFiltroWhere =
				new MeuFiltroWhere(App.single.cache.arrayItemEmpPreco)
				.andEquals(atualItem.id, ItemEmpPreco.campo_idItem)
				.andEquals(App.single.ss.idEmp, ItemEmpPreco.campo_idEmp);
				
			if (filtro.getResultadoArraySimples().length > 0)
				atualIEP = filtro.getResultadoArraySimples()[0];
			
			nsPrCompra.value = atualIEP.compra;
			nsPrCusto.value = atualIEP.custo;
			nsPrVenda.value = atualIEP.venda;

			nsQtd.value=1;
			
			nsQtd.setFocus();
		}
	}
		
	/*
	private function txtBarrasKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		if (txtCodigoBarras.text.length<3)
			return;
		
		FcdMov.unica.LoadItemEstoque(
			Sessao.unica.idEmp,
			txtCodigoBarras.text,
			function(retorno:Array):void
			{
				if (retorno==null)
				{
					AlertaSistema.mensagem("Não encontrado");
					txtCodigoBarras.setSelection(0 , txtCodigoBarras.text.length);
					return;
				}
				atualItem = retorno[0];
				atualIEE = retorno[1];
				atualIEP = retorno[2];
				
				nsPrCompra.value = atualIEP.compra;
				nsPrCusto.value = atualIEP.custo;
				nsPrVenda.value = atualIEP.venda;

				nsQtd.value=1;
				
				txtCodigoBarras.text='';
				nsQtd.setFocus();
			}
		);
		
	}
	*/
	
	private function retornaEstoque(ev:EvRetornaArray):void
	{
		if (ev.retorno == null)
		{
			AlertaSistema.mensagem("Item não encontrado");
			return;
		}
		
		atualItem = ev.retorno[0];
		atualIEE = ev.retorno[1];
		atualIEP = ev.retorno[2];
		
		nsPrCompra.value = atualIEP.compra;
		nsPrCusto.value = atualIEP.custo;
		nsPrVenda.value = atualIEP.venda;

		nsQtd.value=1;
		
		nsQtd.setFocus();
	}
	
	/** getters e setters */
	
	private function get qtd():Number {return nsQtd.value;}
	private function get compra():Number {return nsPrCompra.value;}
	private function get custo():Number {return nsPrCusto.value;}
	private function get venda():Number {return nsPrVenda.value;}
	
	private function set qtd(v:Number):void {nsQtd.value=v;}
	private function set compra(v:Number):void {nsPrCompra.value=v;}
	private function set custo(v:Number):void {nsPrCusto.value=v;}
	private function set venda(v:Number):void {nsPrVenda.value=v;}
	
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
		if (compra==0)
		{
			AlertaSistema.mensagem("Valor da compra não pode ser zero");
			return;
		}
		
		var mie:MovItemEstoque = new MovItemEstoque();
		mie.qtd = qtd;
		mie.identificador = "U:U";
		
		var mi:MovItem = new MovItem();
		mi.idItem = atualItem.id;
		mi.__item = atualItem;
		mi.__mIEstoques = [mie];
		mi.item_nome = mi.__item.nome;
		
		mi.qtd = qtd;
		mi.idClienteFuncionario = Sessao.unica.idClienteFuncionarioLogado;
		
		mi.vlrUnitCompra = nsPrCompra.value;
		mi.vlrUnitCusto = nsPrCusto.value;
		mi.vlrUnitVendaInicial = nsPrVenda.value;
		mi.vlrUnitVendaFinal = venda;
		
		mi.vlrDesc = nsDescontoUnit.value * nsQtd.value;
		mi.vlrIPI = (nsAliqIPI.value * (compra * qtd)) / 100;
		
		lanca_inner(   mi   );
		
		/** Limpar */
		limpaAtual();
	}
	
	private function lanca_inner( miLancado:MovItem ):void
	{
		
		miLancado.id=0;
		miLancado.idMov=0;
		miLancado.idClienteFuncionario = Sessao.unica.idClienteFuncionarioLogado;
		
		var o:Object = {};
		o.qtdLancada = 0;
		o.movItem = miLancado;
		o.codu = App.single.cache.getItem(miLancado.idItem).rfUnica;
		o.qtd = miLancado.qtd;
		o.nome = miLancado.__item.nome;
		o.rfUnica = miLancado.__item.rfUnica;
		o.compra = miLancado.vlrUnitCompra;
		o.custo = miLancado.vlrUnitCusto;
		o.total = qtd * compra;
		o.venda = miLancado.vlrUnitVendaFinal;
		
		o.desc = miLancado.vlrDesc
		o.vlrIPI = miLancado.vlrIPI;
		
		listaMI.addItem(o);
		gridMI.dataProvider = listaMI;
		//mudaItens();
		gridItensEntrada.dataProvider = listaMI;
	}
	
	private function limpaAtual():void
	{
		atualItem= null;
		atualIEP = null;
		cpCodigoBarras.selectedItems.removeAll();
		cpItem.selectedItems.removeAll();
		nsQtd.value = 0;
		nsPrCompra.value = 0;
		nsPrCusto.value = 0;
		nsPrVenda.value = 0;
		nsDescontoUnit.value = 0;
		nsAliqIPI.value = 0;
		qtd = 0;
		compra = 0;
		custo = 0;
		venda = 0;
		exibeTotaisPinta();
		cpItem.setFocus();
	}
	
	private function exibeTotaisPinta():void
	{
		totalBruto = 0;
		totalIPI = 0;
		totalDesconto = 0;
		totalNota=0;
		
		for each(var o:Object in listaMI)
		{
			var mi:MovItem = o.movItem;
			totalBruto += mi.qtd * mi.vlrUnitCompra;
			totalIPI += mi.vlrIPI;
			totalDesconto += mi.vlrDesc;
		}
		totalNota = (totalBruto - totalDesconto) + totalIPI;
		qtdEntradas = listaMI.length;
		gridMI.dataProvider = listaMI;
	}
	
	/** IDENTIFICADOR / GRADE */
	
	private function itemSelecionado():void
	{
		gridItemInfo.dataProvider = gridItensEntrada.selectedItem;
		atualizaGrids();
		mudaItens();
	}
	
	private function moveEstoque(qtdLancando:Number, strIdent:String, strCodBarrasGrade:String, dtFabricacao:String, dtValidade:String):void
	{
		if (strIdent != "" && strIdent != null)
		{
			for each (var obj:Object in this.listaMI)
			{
				if (obj == gridItensEntrada.selectedItem)
				{
					var mi:MovItem = obj.movItem as MovItem;
					
					for each (var tempMie:MovItemEstoque in mi.__mIEstoques)
					{
						if (tempMie.identificador == "U:U" && tempMie.qtd > 0)
						{
							if ((obj.qtdLancada + qtdLancando) <= obj.qtd)
							{
								var mieNovo:MovItemEstoque = new MovItemEstoque();
								
								if (App.single.cache.getItem(mi.idItem).tipoIdent == EItemTipoIdent.lote)
								{
									mieNovo.lote = strIdent;
									mieNovo.dtFab = dtFabricacao;
									mieNovo.dtVal = dtValidade;
								}
								
								mieNovo.qtd = qtdLancando;
								mieNovo.identificador = strIdent;
								mieNovo.codBarrasGrade = strCodBarrasGrade;
								mi.__mIEstoques.push(mieNovo);
								tempMie.qtd -= qtdLancando;
								obj.qtdLancada += qtdLancando;
							}
							else
								AlertaSistema.mensagem("Verifique a quantidade:\n" + 
										"Lançando: "+qtdLancando.toString()+"\n" + 
										"Disponível: "+ (obj.qtd - obj.qtdLancada).toString());
						}
					}
					obj.movItem = mi;
				}
			}
			atualizaGrids();
		}
		else
			AlertaSistema.mensagem("Por favor, informe o identificador.");
		impr = isImprEnabled();
	}
	
	private function removeIdentificado():void
	{
		var qtdRemovendo:Number = gridItensIdentificados.selectedItem.qtd;
		if (gridItensIdentificados.selectedItem.identificador != "U:U")
		{
			for each (var obj:Object in this.listaMI)
			{
				if (obj == gridItensEntrada.selectedItem)
				{
					var mi:MovItem = obj.movItem as MovItem;
					var listaMIE:Array = new Array();
					
					for each (var tempMie:MovItemEstoque in mi.__mIEstoques)
					{
						if (tempMie.identificador == "U:U")
						{
							tempMie.qtd += qtdRemovendo;
							obj.qtdLancada -= qtdRemovendo;
							listaMIE.push(tempMie);
						}
						else if (tempMie != gridItensIdentificados.selectedItem)
							listaMIE.push(tempMie);
					}
					obj.movItem = mi;
					obj.movItem.__mIEstoques = listaMIE;
					
				}
			}
			atualizaGrids();
		}
		impr = isImprEnabled();
	}
	
	private function limpaTela():void
	{
		fornecedor = null;
		transportador = null;
		txtNumNota.text = defineNumeroNF();
		qtdEntradas = 0;
		totalBruto = 0;
		atualItem= null;
		atualIEP = null;
		nsQtd.value = 0;
		nsPrCompra.value = 0;
		nsPrCusto.value = 0;
		nsPrVenda.value = 0;
		qtd = 0;
		compra = 0;
		custo = 0;
		dfDataEmissao.selectedDate = new Date();
		dfDataEmtrada.selectedDate = new Date();
		listaMI.removeAll();
		gridMI.dataProvider = null;
		gridItensEntrada.dataProvider = null;
		gridItemInfo.dataProvider = null;
		gridItensIdentificados.dataProvider = null;
		vs.selectedIndex = 0;
		this.currentState = null;
		detalheEstoque.removeAllChildren();
		
		if (cmbEtiquetaModelo != null)
			cmbEtiquetaModelo.selectedIndex = 0;
	}
	
	private function atualizaGrids():void
	{
		var tempMI:MovItem =  gridItensEntrada.selectedItem.movItem as MovItem;
		var tempLista:Array = [];
		
		for each (var mie:MovItemEstoque in tempMI.__mIEstoques)
		{
			tempLista.push(mie);
		}
		gridItensIdentificados.dataProvider = tempLista;
	}
	
	private function mudaItens():void
	{
		detalheEstoque.removeAllChildren();
		
		var xxx:SuperDefineDetalheEstoque = FabricaDefineDetalheEstoque.fabrica(gridItensEntrada.selectedItem.movItem.__item);
		detalheEstoque.addChild(xxx);
		
		xxx.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				moveEstoque(ev.retorno[0].toString(), ev.retorno[1], ev.retorno[2], ev.retorno[3], ev.retorno[4]);
			}
		);
	}
	
	private function alteraPrecoItens():void
	{
		arrayIEP = new Array();
		
		for each(var variavel:Object in listaMI)
		{
			var iep:ItemEmpPreco = new ItemEmpPreco();
			
			iep.idItem = variavel.movItem.idItem;
			iep.idEmp = Sessao.unica.idEmp;
			
			iep.compra = variavel.compra;
			iep.custo = variavel.custo;
			iep.venda = variavel.venda;
			
			arrayIEP.push(iep);
		}
	}