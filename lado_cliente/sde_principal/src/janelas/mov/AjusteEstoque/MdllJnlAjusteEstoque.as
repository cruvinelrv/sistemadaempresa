// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.MeuFiltroWhere;

import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Enumerador.EMovImpressao;
import SDE.Enumerador.EMovResumo;
import SDE.Enumerador.EMovTipo;

import flash.events.Event;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.core.Container;
import mx.managers.PopUpManager;

	[Bindable] private var arraycAjustes:ArrayCollection;
	[Bindable] private var estoque_a_ajustar:ItemEmpEstoque = null;
	[Bindable] private var item:Item = null;
	
	/**************************************************/
	
	private function create():void
	{
		dtGridPrincipal.addEventListener("deleteRow", removeLinha);
		removePopup(popupEstoque);
		arraycAjustes = new ArrayCollection();
	}
	
	private function removeLinha(ev:Event):void
	{
		var pos:int = arraycAjustes.getItemIndex(ev.target.data);
		arraycAjustes.removeItemAt(pos);
	}
	
	private function lbl_fn_itemNome(iee:ItemEmpEstoque, dgc:DataGridColumn):String
	{
		return App.single.cache.getItem(iee.idItem).nome;
	}
	
	/**************************************************/
	
	private function abrePopupEstoque():void
	{
		PopUpManager.addPopUp(popupEstoque, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEstoque);
		dtGridEstoques.setFocus();
	}
	private function fechaPopupEstoque():void
	{
		PopUpManager.removePopUp(popupEstoque);
	}
	private function removePopup(popup:Container):void
	{
		popup.parent.removeChild(popup);
	}
	
	/**************************************************/
	
	private function quantidadeAjusteAlterada():void
	{
		nsEstoqueProjecao.value = nsEstoqueAtual.value + nsEstoqueAjuste.value;
	}
	
	private function itemSelecionadoCP():void
	{
		if (cpItem.selectedItem == null)
			return;
		
		item = cpItem.selectedItem;
		
		var mfw:MeuFiltroWhere =
			new MeuFiltroWhere(App.single.cache.arrayItemEmpEstoque)
			.andEquals(item.id, ItemEmpEstoque.campo_idItem)
			.andEquals(App.single.ss.idEmp, ItemEmpEstoque.campo_idEmp);
			
		var estoques:Array = mfw.getResultadoArraySimples();
		
		if (estoques.length == 1)
		{
			estoque_a_ajustar = estoques[0];
			lancaEstoqueEdicao();
			nsEstoqueAjuste.setFocus();
		}
		else
		{
			dtGridEstoques.dataProvider = estoques;
			abrePopupEstoque();
		}
	}
	private function estoqueSelecionadoCP():void
	{
		if (cpEstoque.selectedItem == null)
			return;
		
		estoque_a_ajustar = cpEstoque.selectedItem;
		item = App.single.cache.getItem(estoque_a_ajustar.idItem);
		lancaEstoqueEdicao();
		nsEstoqueAjuste.setFocus();
	}
	private function estoqueSelecionadoDG():void
	{
		if (dtGridEstoques.selectedItem == null)
		{
			AlertaSistema.mensagem("Selecione um estoque")
			return;
		}
		estoque_a_ajustar = dtGridEstoques.selectedItem as ItemEmpEstoque;
		item = App.single.cache.getItem(estoque_a_ajustar.idItem);
		PopUpManager.removePopUp(popupEstoque);
		lancaEstoqueEdicao();
		nsEstoqueAjuste.setFocus();
	}
	private function lancaEstoqueEdicao():void
	{
		nsEstoqueAtual.value = estoque_a_ajustar.qtd;
	}
	private function lancaEstoque():void
	{
		if (estoque_a_ajustar==null)		
		{
			AlertaSistema.mensagem("Selecione um item");
			return;
		}
		
		var obj:Object = new Object();
		
		obj.idIEE = estoque_a_ajustar.id;
		obj.idItem = item.id;
		obj.item_nome = item.nome;
		obj.identificador = estoque_a_ajustar.identificador
		obj.barras = estoque_a_ajustar.codBarras;
		obj.estoque_atual = estoque_a_ajustar.qtd;
		obj.estoque_ajuste = nsEstoqueAjuste.value;
		obj.estoque_projecao = nsEstoqueProjecao.value;
		
		arraycAjustes.addItem(obj);
		
		limpaItemEstoque();
	}
	
	private function confirmaAjusteEstoque():void
	{
		if (arraycAjustes.length == 0)
		{
			AlertaSistema.mensagem("Não ha estoque para ajustar")
			return;
		}
		
		var mi:MovItem;
		var mov:Mov = new Mov();
		mov.__mItens = [];
		mov.idClienteFuncionarioLogado = App.single.ss.idClienteFuncionarioLogado;
		mov.tipo = EMovTipo.ambos_ajuste_estoque;
		mov.resumo = EMovResumo.ambos;
		mov.impressao = EMovImpressao.sem_impressao;
		mov.obs = txtObs.text;
		
		for each (var obj:Object in arraycAjustes)
		{
			mi = new MovItem();
			mi.idItem = obj.idItem;
			mi.idIEE = obj.idIEE;
			mi.item_nome = obj.item_nome;
			mi.estoque_identificador = obj.identificador;
			mi.qtd = obj.estoque_ajuste;
			mov.__mItens.push(mi);
		}
		
		App.single.n.modificacoes.RealizaAjusteEstoque(mov,
			function():void{AlertaSistema.mensagem("OK");});
			
		limpaTela();
		
	}
	
	/**************************************************/
	
	private function limpaItemEstoque():void
	{
		cpEstoque.selectedItem = null;
		cpItem.selectedItem = null;
		estoque_a_ajustar = null;
		item = null;
		nsEstoqueAtual.value = 0;
		nsEstoqueProjecao.value = 0;
		nsEstoqueAjuste.value = 0;
		cpItem.setFocus();
	}
	
	private function limpaTela():void
	{
		arraycAjustes.removeAll();
		txtObs.text = "";
		limpaItemEstoque();
	}