import Componentes.SDE.CaixaPesquisa;
import Componentes.SDE.DataGrid;
import Componentes.comp.DateFieldBR;

import Core.Alerta.AlertaSistema;
import Core.Sessao;
import Core.Utils.Formatadores;

import SDE.Entidade.Cad_Marca;
import SDE.Entidade.Cliente;
import SDE.Entidade.Finan_CentroCusto;
import SDE.Entidade.Finan_Conta;
import SDE.Entidade.Finan_Portador;
import SDE.Entidade.Finan_TipoLancamento;
import SDE.Entidade.Item;
import SDE.Entidade.Mov;

import flash.events.KeyboardEvent;
import flash.net.URLRequest;
import flash.net.URLVariables;
import flash.ui.Keyboard;

import img.Imagens;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.controls.Alert;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.core.IFlexDisplayObject;
import mx.managers.PopUpManager;

import pesquisas.PesquisaCliente;
import pesquisas.PesquisaFuncionario;
import pesquisas.PesquisaItem;
import pesquisas.PesquisaItemEmpPreco;
import pesquisas.PesquisaMov;
	
	/** GERAL */
	
	private function create():void
	{
		limpaUrl();
		
		sortMov = new Sort();
		sortMov.fields = [new SortField('id')];
		
		
		popups.parent.removeChild(popups);
	}
	
	private function limpaUrl():void
	{
		url = new URLRequest();
		vars = new URLVariables();
		
		vars.tipoImpressao = "relatorio";
		vars.idCorp = Sessao.unica.idCorp;
		vars.idEmp = Sessao.unica.idEmp;
		
		url.url = "imprime.aspx";
		url.data = vars;
	}
	
	private function createCmbRel():void
	{
		sortConta = new Sort();
		sortConta.fields = [new SortField('nome')];
		var arraycSortConta:ArrayCollection = new ArrayCollection();
		arraycSortConta.source = cmbRelExtratoContaCorrenteCaixaConta.dataProvider.source;
		arraycSortConta.sort = sortConta;
		arraycSortConta.refresh();
		cmbRelExtratoContaCorrenteCaixaConta.dataProvider = arraycSortConta;
		
		sortCentroCusto = new Sort();
		sortCentroCusto.fields = [new SortField('nome')];
		var arraycSortCentroCusto:ArrayCollection = new ArrayCollection();
		arraycSortCentroCusto.source = cmbRelExtratoContaCorrenteCaixaCentroCusto.dataProvider.source;
		arraycSortCentroCusto.sort = sortCentroCusto;
		arraycSortCentroCusto.refresh();
		cmbRelExtratoContaCorrenteCaixaCentroCusto.dataProvider = arraycSortCentroCusto;
		
		sortPlanoConta = new Sort();
		sortPlanoConta.fields = [new SortField('nomeTipoLancamento')];
		var arraycSortPlanoConta:ArrayCollection = new ArrayCollection();
		arraycSortPlanoConta.source = cmbRelExtratoContaCorrenteCaixaPlanoConta.dataProvider.source;
		arraycSortPlanoConta.sort = sortPlanoConta;
		arraycSortPlanoConta.refresh();
		cmbRelExtratoContaCorrenteCaixaPlanoConta.dataProvider = arraycSortPlanoConta;
		
		sortPortador = new Sort();
		sortPortador.fields = [new SortField('nome')];
		var arraycSortPortador:ArrayCollection = new ArrayCollection();
		arraycSortPortador.source = cmbRelTitulosReceberPagarPortador.dataProvider.source;
		arraycSortPortador.sort = sortPortador;
		arraycSortPortador.refresh();
		cmbRelTitulosReceberPagarPortador.dataProvider = arraycSortPortador;
		
		sortMarca = new Sort();
		sortMarca.fields = [new SortField('marca')];
		var arraycSortMarca:ArrayCollection = new ArrayCollection();
		arraycSortMarca.source = cmbRelListaPrecosMarca.dataProvider.source;
		arraycSortMarca.sort = sortMarca;
		arraycSortMarca.refresh();
		cmbRelListaPrecosMarca.dataProvider = arraycSortMarca;
		
		var dpConta:Array = ['TODOS'];
		for each (var c:Finan_Conta in cmbRelExtratoContaCorrenteCaixaConta.dataProvider)
		{
			if (c.nome != '-')
				dpConta.push(c);
		}
		cmbRelExtratoContaCorrenteCaixaConta.dataProvider = dpConta; 
		
		 var dpCentroCusto:Array = ['TODOS'];
		for each (var cc:Finan_CentroCusto in cmbRelExtratoContaCorrenteCaixaCentroCusto.dataProvider)
		{
			if (cc.nome != '-')
				dpCentroCusto.push(cc);
		}
		cmbRelExtratoContaCorrenteCaixaCentroCusto.dataProvider = dpCentroCusto;
		
		var dpPlanoConta:Array = ['TODOS'];
		for each (var pc:Finan_TipoLancamento in cmbRelExtratoContaCorrenteCaixaPlanoConta.dataProvider)
		{
			if (pc.nomeTipoLancamento != '-')
				dpPlanoConta.push(pc);
		}
		cmbRelExtratoContaCorrenteCaixaPlanoConta.dataProvider = dpPlanoConta;
		
		var dpPortador:Array = ['TODOS'];
		for each (var portador:Finan_Portador in cmbRelTitulosReceberPagarPortador.dataProvider)
		{
			if (portador.nome != '-')
				dpPortador.push(portador);
		}
		cmbRelTitulosReceberPagarPortador.dataProvider = dpPortador;
		
		var dpMarca:Array = ['TODOS'];
		for each (var marca:Cad_Marca in cmbRelListaPrecosMarca.dataProvider)
		{
			if (marca.marca != '')
				dpMarca.push(marca);
		}
		cmbRelListaPrecosMarca.dataProvider = dpMarca;
	}
	
	private function lblFn_PrecoVenda(item:Item, dgc:DataGridColumn):String
	{
		return Formatadores.unica.formataValor(PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, item.id).venda, true);
	}
	
	private function adicionaPopup(popUp:IFlexDisplayObject):void
	{
		PopUpManager.addPopUp(popUp, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popUp);
	}
	
	private function removePopup(popUp:IFlexDisplayObject):void
	{
		PopUpManager.removePopUp(popUp);
	}
	
	private function setSelectedRange(df1:DateFieldBR, df2:DateFieldBR):void
	{
		if (df2.selectedDate)
		{
			if (df1.selectedDate.getTime() > df2.selectedDate.getTime())
				df2.selectedDate = df1.selectedDate;
		}
		df2.selectableRange = {rangeStart:df1.selectedDate};
	}
	
	/** ESPELHO DE MOVIMENTAÇÕES */
	
	private function mudaState(ev:KeyboardEvent):void
	{
		if (ev.ctrlKey && ev.shiftKey && ev.keyCode == Keyboard.ENTER)
		{
			currentState = (currentState == "stateTipoMovOculta") ? null : "stateTipoMovOculta" ;
			
			ckbTipoMovTodos_click();
			ckbTipoImpressaoTodos_click();
		}
	}
	
	private function ckbTipoMovTodos_click():void
	{
		if (ckbTipoMovTodos.selected)
			ckbTipoMovTodosSelecionado();
		else
			ckbTipoMovTodosNaoSelecionado();
	}
	
	private function ckbTipoImpressaoTodos_click():void
	{
		if (ckbTipoImpressaoTodos.selected)
			ckbTipoImpressaoTodosSelecionado();
		else
			ckbTipoImpressaoTodosNaoSelecionado();
	}
	
	private function ckbTipoMovTodosSelecionado():void
	{
		ckbTipoMovCompra.selected = true;
		ckbTipoMovCompra.enabled = false;
		ckbTipoMovVenda.selected = true;
		ckbTipoMovVenda.enabled = false;
		ckbTipoMovOrcamento.selected = true;
		ckbTipoMovOrcamento.enabled = false;
		ckbTipoMovCompraCancelada.selected = true;
		ckbTipoMovCompraCancelada.enabled = false;
		ckbTipoMovVendaCancelada.selected = true;
		ckbTipoMovVendaCancelada.enabled = false;
		ckbTipoMovAjusteEstoque.selected = true;
		ckbTipoMovAjusteEstoque.enabled = false;
		ckbTipoMovBalanco.selected = true;
		ckbTipoMovBalanco.enabled = false;
		
		if (currentState == "stateTipoMovOculta")
		{
			ckbTipoMovReserva.selected = true;
			ckbTipoMovReserva.enabled = false;
			ckbTipoMovPedido.selected = true;
			ckbTipoMovPedido.enabled = false;
		}
	}
	
	private function ckbTipoMovTodosNaoSelecionado():void
	{
		ckbTipoMovCompra.selected = false;
		ckbTipoMovCompra.enabled = true;
		ckbTipoMovVenda.selected = false;
		ckbTipoMovVenda.enabled = true;
		ckbTipoMovOrcamento.selected = false;
		ckbTipoMovOrcamento.enabled = true;
		ckbTipoMovCompraCancelada.selected = false;
		ckbTipoMovCompraCancelada.enabled = true;
		ckbTipoMovVendaCancelada.selected = false;
		ckbTipoMovVendaCancelada.enabled = true;
		ckbTipoMovAjusteEstoque.selected = false;
		ckbTipoMovAjusteEstoque.enabled = true;
		ckbTipoMovBalanco.selected = false;
		ckbTipoMovBalanco.enabled = true;
		
		if (currentState == "stateTipoMovOculta")
		{
			ckbTipoMovReserva.selected = false;
			ckbTipoMovReserva.enabled = true;
			ckbTipoMovPedido.selected = false;
			ckbTipoMovPedido.enabled = true;
		}
	}
	
	private function ckbTipoImpressaoTodosSelecionado():void
	{
		ckbTipoImpressaoSemImpressao.selected = true;
		ckbTipoImpressaoSemImpressao.enabled = false;
		ckbTipoImpressaoNfe.selected = true;
		ckbTipoImpressaoNfe.enabled = false;
		ckbTipoImpressaoNf.selected = true;
		ckbTipoImpressaoNf.enabled = false;
		ckbTipoImpressaoCupomFiscal.selected = true;
		ckbTipoImpressaoCupomFiscal.enabled = false;
		ckbTipoImpressaoOrcamento.selected = true;
		ckbTipoImpressaoOrcamento.enabled = false;
		
		if (currentState == "stateTipoMovOculta")
		{
			ckbTipoImpressaoReserva.selected = true;
			ckbTipoImpressaoReserva.enabled = false;
			ckbTipoImpressaoPedido.selected = true;
			ckbTipoImpressaoPedido.enabled = false;
		}
	}
	
	private function ckbTipoImpressaoTodosNaoSelecionado():void
	{
		ckbTipoImpressaoSemImpressao.selected = false;
		ckbTipoImpressaoSemImpressao.enabled = true;
		ckbTipoImpressaoNfe.selected = false;
		ckbTipoImpressaoNfe.enabled = true;
		ckbTipoImpressaoNf.selected = false;
		ckbTipoImpressaoNf.enabled = true;
		ckbTipoImpressaoCupomFiscal.selected = false;
		ckbTipoImpressaoCupomFiscal.enabled = true;
		ckbTipoImpressaoOrcamento.selected = false;
		ckbTipoImpressaoOrcamento.enabled = true;
		
		if (currentState == "stateTipoMovOculta")
		{
			ckbTipoImpressaoReserva.selected = false;
			ckbTipoImpressaoReserva.enabled = true;
			ckbTipoImpressaoPedido.selected = false;
			ckbTipoImpressaoPedido.enabled = true;
		}
	}
	
	private function pesquisaCliente(searchStr:String):void
	{
		var retornoPesquisa:Array = PesquisaCliente.pesquisar(searchStr);
		
		if (retornoPesquisa.length == 1)
		{
			selecionaCliente((retornoPesquisa[0] as Cliente), popupCliente);
		}
		else
		{
			retornoPesquisa.sortOn('nome');
			dgCliente.dataProvider = retornoPesquisa;
			adicionaPopup(popupCliente);
		}
	}
	
	private function pesquisaClientePopUp(searchStr:String):void
	{
		var retornoPesquisa:Array = PesquisaCliente.pesquisar(searchStr);
		retornoPesquisa.sortOn('nome');
		dgCliente.dataProvider = retornoPesquisa;
	}
	
	private function selecionaCliente(cliente:Cliente, popUp:IFlexDisplayObject):void
	{
		if (!cliente)
		{
			Alert.show("Selecione um cliente na tabela para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		clienteSelecionado = cliente;
		removePopup(popUp);
		
		if (lbRelatorios.selectedIndex == 4)
		{
			cpClienteRelEspelhoMovimentacoes.text = clienteSelecionado.nome;
			cpClienteRelEspelhoMovimentacoes.enabled = false;
		}
		if (lbRelatorios.selectedIndex == 8)
		{
			cpClienteRelTitulosReceberPagar.text = clienteSelecionado.nome;
			cpClienteRelTitulosReceberPagar.enabled = false;
		}
	}
	
	private function removeCliente(cp:CaixaPesquisa):void
	{
		clienteSelecionado = null;
		cp.text = "";
		cp.enabled = true;
	}
	
	private function pesquisaFuncionario(searchStr:String, cp:CaixaPesquisa, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaFuncionario.pesquisar(searchStr);
		
		if (retornoPesquisa.length == 1)
		{
			selecionaFuncionario((retornoPesquisa[0] as Cliente), popupFuncionario)
		}
		else
		{
			retornoPesquisa.sortOn('nome');
			dataGrid.dataProvider = retornoPesquisa;
			adicionaPopup(popupFuncionario);
		}
	}
	
	private function pesquisaFuncionarioPopUp(searchStr:String, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaFuncionario.pesquisar(searchStr);
		retornoPesquisa.sortOn('nome');
		dataGrid.dataProvider = retornoPesquisa;
	}
	
	private function selecionaFuncionario(funcionario:Cliente, popUp:IFlexDisplayObject):void
	{
		if (!funcionario)
		{
			Alert.show("Selecione um funcionário na tabela para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		funcionarioSelecionado = funcionario;
		removePopup(popUp);
		
		if (lbRelatorios.selectedIndex == 4)
		{
			cpFuncionarioRelEspelhoMovimentacoes.text = funcionarioSelecionado.nome;
			cpFuncionarioRelEspelhoMovimentacoes.enabled = false;
		}
		if (lbRelatorios.selectedIndex == 10)
		{
			cpFuncionarioRelComissionamentoDinamico.text = funcionarioSelecionado.nome;
			cpFuncionarioRelComissionamentoDinamico.enabled = false;
		}
	}
	
	private function removeFuncionario(cp:CaixaPesquisa):void
	{
		funcionarioSelecionado = null;
		cp.text = "";
		cp.enabled = true;
	}
	
	private function pesquisaItem(searchStr:String, cp:CaixaPesquisa, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaItem.pesquisar(searchStr, true, false);
		
		if (retornoPesquisa.length == 1)
		{
			selecionaItem(retornoPesquisa[0] as Item, popupItem);
		}
		else
		{
			retornoPesquisa.sortOn('nome');
			dataGrid.dataProvider = retornoPesquisa;
			adicionaPopup(popupItem);
		}
	}
	
	private function pesquisaItemPoUp(searchStr:String, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaItem.pesquisar(searchStr, true, false);
		retornoPesquisa.sortOn('nome');
		dataGrid.dataProvider = retornoPesquisa;
	}
	
	private function selecionaItem(item:Item, popUp:IFlexDisplayObject):void
	{
		if (!item)
		{
			Alert.show("Selecione um item na tabela para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		itemSelecionado = item;
		removePopup(popUp);
		
		if (lbRelatorios.selectedIndex == 4)
		{
			cpItemRelEspelhoMovimentacoes.text = itemSelecionado.nome;
			cpItemRelEspelhoMovimentacoes.enabled = false;
		}
		if (lbRelatorios.selectedIndex == 11)
		{
			cpItemRelProdutosVendidosPeriodo.text = itemSelecionado.nome;
			cpItemRelProdutosVendidosPeriodo.enabled = false;
		}
	}
	
	private function removeItem(cp:CaixaPesquisa):void
	{
		itemSelecionado = null;
		cp.text = "";
		cp.enabled = true;
	}
	
	private function pesquisaMov(searchStr:String, cp:CaixaPesquisa, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaMov.pesquisar(searchStr);
		
		if (retornoPesquisa.length == 1)
		{
			selecionaMov(retornoPesquisa[0] as Mov, cp, popupMov);
		}
		else
		{
			var arraycTemp:ArrayCollection = new ArrayCollection(retornoPesquisa);
			arraycTemp.sort = sortMov;
			arraycTemp.refresh();
			retornoPesquisa = arraycTemp.source;
			retornoPesquisa.reverse();
			dataGrid.dataProvider = retornoPesquisa;
			adicionaPopup(popupMov);
		}
	}
	
	private function pesquisaMovPoUp(searchStr:String, dataGrid:DataGrid):void
	{
		var retornoPesquisa:Array = PesquisaMov.pesquisar(searchStr);
		var arraycTemp:ArrayCollection = new ArrayCollection(retornoPesquisa);
		arraycTemp.sort = sortMov;
		arraycTemp.refresh();
		retornoPesquisa = arraycTemp.source;
		retornoPesquisa.reverse();
		dataGrid.dataProvider = retornoPesquisa;
	}
	
	private function selecionaMov(mov:Mov, cp:CaixaPesquisa, popUp:IFlexDisplayObject):void
	{
		if (!mov)
		{
			Alert.show("Selecione uma movimentação na tabela para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		movSelecionada = mov;
		cp.text = movSelecionada.cliente_nome + " / " + movSelecionada.dthrMovEmissao + " / " + Formatadores.unica.formataValor(movSelecionada.vlrTotal, true);
		cp.enabled = false;
		removePopup(popUp);
	}
	
	private function removeMov(cp:CaixaPesquisa):void
	{
		movSelecionada = null;
		cp.text = "";
		cp.enabled = true;
	}
	
	private function escondeRelEspelhoMovimentacoes():void
	{
		removeCliente(cpClienteRelEspelhoMovimentacoes);
		removeFuncionario(cpFuncionarioRelEspelhoMovimentacoes);
		removeItem(cpItemRelEspelhoMovimentacoes);
		removeMov(cpMovRelEspelhoMovimentacoes);
	}
	
	private function escondeRelTitulosReceberPagar():void
	{
		removeCliente(cpClienteRelTitulosReceberPagar);
	}
	
	private function escondeRelComissionamentoDinamico():void
	{
		removeFuncionario(cpFuncionarioRelComissionamentoDinamico);
	}
	
	private function escondeRelProdutosVendidosPeriodo():void
	{
		removeItem(cpItemRelProdutosVendidosPeriodo);
	}
	
	private function statusImplementacao():void
	{
		Alert.show("Este relatório se encontra em status de implementação e não será possível gerá-lo no momento.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	