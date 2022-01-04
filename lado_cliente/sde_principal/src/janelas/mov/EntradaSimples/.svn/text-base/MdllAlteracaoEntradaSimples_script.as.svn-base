import Componentes.DefineDetalheEstoque.FabricaDefineDetalheEstoque;
import Componentes.DefineDetalheEstoque.SuperDefineDetalheEstoque;

import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Ev.EvRetornaArray;
import Core.Load.LoadSistema;
import Core.Sessao;
import Core.Utils.Formatadores;
import Core.Utils.MeuFiltroWhere;

import SDE.CamadaNuvem.NuvemCache;
import SDE.Entidade.Cliente;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.Enumerador.EItemTipoIdent;
import SDE.Enumerador.EMovTipo;

import flash.events.Event;

import janelas.cadastro.ItemProduto2.JnlCadItemProduto;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;

	[Bindable] private var dpEntradaItens:ArrayCollection = new ArrayCollection();
	[Bindable] private var dpEntradaIdentificadores:ArrayCollection = new ArrayCollection();
	
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
	
	private var listaMovItemOriginal:Array = [];
	[Bindable] private var listaMovItem:ArrayCollection = new ArrayCollection();
	
	private var movOriginal:Mov = new Mov();
	private var movAlterada:Mov = new Mov();
	
	[Bindable] private var impr:Boolean = true;
	
	private var cache:NuvemCache = null;
	
	private var arrayIEP:Array = new Array();
	
	private function init():void
	{
		cache = App.single.cache;
		
		dpEntradaItens = cache.arraycMov;
		cpEntradaEstoque_KeyUp();
	}
	
	private function create():void
	{
		gridMI.addEventListener('deleteRowItem',
			function(ev:Event):void
			{
				var obj:Object = ev.target.data;
		    	var pos:int = listaMovItem.getItemIndex(obj);
		    	listaMovItem.removeItemAt(pos)
		    	//mudaItens();
		    	gridItensIdentificados.dataProvider = null;
		    	gridItemInfo.dataProvider = null;
		    	detalheEstoque.removeAllChildren();
		    	exibeTotaisPinta();
			}
		);
		limpaAtual();
		exibeTotaisPinta();
	}
	
	private function fn_lb_toCurrencyFormat_mov(obj:Object, dgc:DataGridColumn):String
	{
		return Formatadores.unica.formataValor(obj.vlrTotal, true);
	}
	private function fn_lb_toCurrencyFormat_mi(obj:Object, dgc:DataGridColumn):String
	{
		return Formatadores.unica.formataValor(obj.vlrCompra, true);
	}
	
	private function cpFornecedor_Change():void
	{
		cpEntradaEstoque_KeyUp();
	}
	
	private function cpEntradaEstoque_KeyUp():void
	{
		var obj:Object = null;
		if (cpEntradaEstoque.dropDown)
			cpEntradaEstoque.dropDown.visible = false;
		
		dpEntradaItens.removeAll();
		
		if (cpFornecedor.selectedItem != null)
		{
			var filtradas:ArrayCollection = new ArrayCollection();
			for each (var xxx:Mov in cpEntradaEstoque.dataProvider)
			{
				if (xxx.idCliente == (cpFornecedor.selectedItem as Cliente).id)
				{
					if (yyy.tipo == EMovTipo.entrada_compra && yyy.idEmp == App.single.idEmp)
					{
						obj = new Object();
						obj.idMov = xxx.id;
						obj.numeroNF = xxx.numeroNF;
						obj.fornecedor_nome = buscaFornecedor(xxx.idCliente);
						obj.dthrMovEmissao = xxx.dthrMovEmissao;
						obj.vlrTotal = xxx.vlrTotal;
						filtradas.addItem(obj);
					}
				}
			}
			dpEntradaItens = filtradas;
		}
		else
		{
			for each (var yyy:Mov in cpEntradaEstoque.dataProvider)
			{
				if (yyy.tipo == EMovTipo.entrada_compra && yyy.idEmp == App.single.idEmp)
				{
					obj = new Object();
					obj.idMov = yyy.id;
					obj.numeroNF = yyy.numeroNF;
					obj.fornecedor_nome = buscaFornecedor(yyy.idCliente);
					obj.dthrMovEmissao = yyy.dthrMovEmissao;
					obj.vlrTotal = yyy.vlrTotal;
					dpEntradaItens.addItem(obj);
				}
			}
		}
		
		cpEntradaEstoque.prompt = "Selecione uma Entrada ("+dpEntradaItens.length+")";
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
	
	private function dgEntradaItens_Change():void
	{
		var idMov:Number = (dgEntradaItens.selectedItem).idMov;
		var listaMovItem:Array = [];
		var obj:Object = null;
		
		dpEntradaIdentificadores.removeAll();
		
		for each (var mi:MovItem in cache.arrayMovItem)
		{
			if (mi.idMov == idMov)
			{
				for each (var mie:MovItemEstoque in cache.arrayMovItemEstoque)
				{
					if (mie.idMovItem == mi.id && mi.qtd > 0)
					{
						obj = new Object();
						obj.codItem = mi.idItem;
						obj.itemNome = cache.getItem(mi.idItem).nome;
						obj.gradeItent = mie.identificador;
						obj.qtd = mie.qtd;
						obj.vlrCompra = mi.vlrUnitCompra;
						dpEntradaIdentificadores.addItem(obj);
					}
				}
			}
		}
	}
	
	private function buscaFornecedor(idCliente:Number):String
	{
		return (cache.getCliente(idCliente)).nome;
	}
	
	private function isImprEnabled():Boolean
	{
		for each (var o:Object in listaMovItem)
		{
			if (o.qtdLancada > 0)
			{
				return false;
				return
			}
		}
		return true;
	}
	
	/**SELECIONA ENTRADA A ALTERAR*/
	
	private function btnSelecionar_Click():void
	{
		if (!dgEntradaItens.selectedItem)
		{
			AlertaSistema.mensagem("Escolha uma entrada na tabela para selecionar");
			return;
		}
		
		vsPrincipal.selectedIndex = 1;
		vsInterno.selectedIndex = 0;
		
		movOriginal = cache.getMov(dgEntradaItens.selectedItem.idMov).clone();
		
		fornecedor = cache.getCliente(movOriginal.idCliente).clone();
		if (movOriginal.idClienteTransportador != 0)
			transportador = cache.getCliente(movOriginal.idClienteTransportador).clone();
		lblFornecedor.text = fornecedor.nome;
		if (transportador)
			lblTransportador.text = transportador.nome;
		lblNumeroNota.text = movOriginal.numeroNF.toString();
		
		 dfDataEmissao.text = movOriginal.dtNF;
		 dfDataEmtrada.text = movOriginal.dtEntSai;
		
		var arrayMovItemTemp:Array = [];
		for each (var mit:MovItem in cache.arrayMovItem)
			arrayMovItemTemp.push(mit.clone());
			
			
		var movItem:MovItem = null;
		for each (var mi:MovItem in arrayMovItemTemp)
		{
			if (mi.idMov == movOriginal.id)
			{
				if (!movItem)
				{
					movItem = new MovItem();
					movItem = mi.clone();
					movItem.qtd = 0;
					movItem.__mIEstoques = [];
				}
				
				if (movItem.idItem != mi.idItem)
				{
					movItem.__item = cache.getItem(movItem.idItem).clone();
					lanca_inner(movItem);
					movItem = new MovItem();
					movItem = mi.clone();
					movItem.qtd = 0;
					movItem.__mIEstoques = [];
				}
				
				//movItem.qtd = 0;
				for each (var mie:MovItemEstoque in cache.arrayMovItemEstoque)
				{
					if (mie.idMovItem == mi.id)
					{
						movItem.__mIEstoques.push(mie.clone());
						movItem.qtd += mie.clone().qtd;
					}
				}
			}
		}
		
		movItem.__item = cache.getItem(movItem.idItem).clone();
		lanca_inner(movItem);
		
		exibeTotaisPinta();
		
		for each (var obj:Object in listaMovItem)
		{
			listaMovItemOriginal.push(obj.movItem.clone());
		}
		
		movOriginal.__mItens = [];
		movOriginal.__mItens = listaMovItemOriginal;
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
	
	private function mudaState(state:String):void
	{
		this.currentState = (state == 'Base state') ? null : state;
	}
	
	private function novoProduto():void
	{
		Application.application.gerenteJanelas.NovaJanela(new JnlCadItemProduto(), "Cadastre seu produto");
	}
	
	private function lancarAtual():void
	{
		if (atualItem==null)
		{
			AlertaSistema.mensagem("Selecione um Produto");
			return;
		}
		if (nsQtd.value==0)
		{
			AlertaSistema.mensagem("Quantidade não pode ser zero");
			return;
		}
		if (nsPrCompra.value==0)
		{
			AlertaSistema.mensagem("Valor da compra não pode ser zero");
			return;
		}
		
		var mie:MovItemEstoque = new MovItemEstoque();
		mie.qtd = nsQtd.value;
		mie.identificador = "U:U";
		
		var mi:MovItem = new MovItem();
		mi.idItem = atualItem.id;
		mi.__item = atualItem;
		mi.__mIEstoques = [mie];
		mi.item_nome = mi.__item.nome;
		
		mi.qtd = nsQtd.value;
		mi.idClienteFuncionario = Sessao.unica.idClienteFuncionarioLogado;
		
		mi.vlrUnitCompra = nsPrCompra.value;
		mi.vlrUnitCusto = nsPrCusto.value;
		mi.vlrUnitVendaInicial = nsPrVenda.value;
		mi.vlrUnitVendaFinal = nsPrVenda.value;
		
		mi.vlrDesc = nsDescontoUnit.value * nsQtd.value;
		mi.vlrIPI = (nsAliqIPI.value * (nsPrCompra.value * nsQtd.value)) / 100;
		
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
		o.movItem = miLancado.clone();
		o.codu = App.single.cache.getItem(miLancado.idItem).rfUnica;
		o.qtd = miLancado.qtd;
		o.nome = cache.getItem(miLancado.idItem).nome;
		o.rfUnica = cache.getItem(miLancado.idItem).rfUnica;
		o.compra = miLancado.vlrUnitCompra;
		o.custo = miLancado.vlrUnitCusto;
		o.total = miLancado.qtd * miLancado.vlrUnitCompra;
		o.venda = miLancado.vlrUnitVendaFinal;
		
		o.desc = miLancado.vlrDesc
		o.vlrIPI = miLancado.vlrIPI;
		
		listaMovItem.addItem(o);
		//mudaItens();
		gridItensEntrada.dataProvider = listaMovItem;
	}
	
	private function moveEstoque(qtdLancando:Number, strIdent:String, strCodBarrasGrade:String, dtFabricacao:String, dtValidade:String):void
	{
		if (strIdent != "" && strIdent != null)
		{
			for each (var obj:Object in this.listaMovItem)
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
			for each (var obj:Object in this.listaMovItem)
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
		//qtd = 0;
		//compra = 0;
		//custo = 0;
		//venda = 0;
		exibeTotaisPinta();
		cpItem.setFocus();
	}
	
	private function exibeTotaisPinta():void
	{
		totalBruto = 0;
		totalIPI = 0;
		totalDesconto = 0;
		totalNota=0;
		
		for each(var o:Object in listaMovItem)
		{
			var mi:MovItem = o.movItem;
			totalBruto += mi.qtd * mi.vlrUnitCompra;
			totalIPI += mi.vlrIPI;
			totalDesconto += mi.vlrDesc;
		}
		totalNota = (totalBruto - totalDesconto) + totalIPI;
		qtdEntradas = listaMovItem.length;
		gridMI.dataProvider = listaMovItem;
		nsValorTotalProdutos.value = totalNota;
		nsValorFrete.value = movOriginal.vlrFrete;
		nsValorSubstituicaoTributaria.value = movOriginal.vlrSubstituicaoTributaria;
	}
	
	private function itemSelecionado():void
	{
		gridItemInfo.dataProvider = gridItensEntrada.selectedItem;
		atualizaGrids();
		mudaItens();
	}
	
	private function limpaTela():void
	{
		cpEntradaEstoque.selectedItems.removeAll();
		cpEntradaEstoque.text = "";
		cpFornecedor.selectedItems.removeAll();
		cpFornecedor.text = "";
		cpEntradaEstoque.setFocus();
		dpEntradaIdentificadores.removeAll();
		
		cache = App.single.cache;
		
		fornecedor = null;
		transportador = null;
		lblFornecedor.text = '';
		lblTransportador.text = '';
		lblNumeroNota.text = '';
		qtdEntradas = 0;
		totalBruto = 0;
		atualItem= null;
		atualIEP = null;
		nsQtd.value = 0;
		nsPrCompra.value = 0;
		nsPrCusto.value = 0;
		nsPrVenda.value = 0;
		dfDataEmissao.selectedDate = null;
		dfDataEmtrada.selectedDate = null;
		listaMovItem.removeAll();
		listaMovItemOriginal = [];
		gridMI.dataProvider = null;
		gridItensEntrada.dataProvider = null;
		gridItemInfo.dataProvider = null;
		gridItensIdentificados.dataProvider = null;
		vsPrincipal.selectedIndex = 0;
		vsInterno.selectedIndex = 0;
		this.currentState = null;
		detalheEstoque.removeAllChildren();
		
		nsValorTotalProdutos.value = 0;
		nsValorFrete.value = 0;
		nsValorSubstituicaoTributaria.value = 0;
		nsValorArredondamentoNota.value = 0;
		
		var dp:ArrayCollection = new ArrayCollection();
			for each (var xxx:Mov in App.single.cache.arrayMov)
			{
				if (xxx.tipo == EMovTipo.entrada_compra)
					dp.addItem(xxx);
			}
		cpEntradaEstoque.dataProvider = dp;
		cpEntradaEstoque.prompt = "Selecione uma Entrada ("+cpEntradaEstoque.dataProvider.length+")";
		cpEntradaEstoque_KeyUp();
		
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
		
		for each(var variavel:Object in listaMovItem)
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