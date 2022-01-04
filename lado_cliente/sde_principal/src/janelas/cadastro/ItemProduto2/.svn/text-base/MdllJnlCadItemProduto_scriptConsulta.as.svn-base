
import Core.App;
import Core.Sessao;
import Core.Utils.Formatadores;

import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpPreco;

import flash.events.KeyboardEvent;
import flash.ui.Keyboard;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;

import pesquisas.PesquisaItem;


	[Bindable] private var dpItem:ArrayCollection = new ArrayCollection;
	
	private function fn_valorVenda(obj:Object, dgc:DataGridColumn):String{return Formatadores.unica.formataValor(obj.iep.venda, true);}
	private function txtItemPesquisa_KeyDown(event:KeyboardEvent):void{ if (event.keyCode == Keyboard.ENTER) btnPesquisar_Click();}
	
	private function btnPesquisar_Click():void
	{
		//if (cpItem.dropDown)
			//cpItem.dropDown.visible = false;
			
		var iep:ItemEmpPreco;
		var obj:Object;
			
		//var listaItem:ArrayCollection = cpItem.dataProvider as ArrayCollection;
		//var listaItem:Array = App.single.cache.arrayItem;
		var listaItem:Array = PesquisaItem.pesquisar(txtItemPesquisa.text, true, false);
		
		dpItem.removeAll();
		for each (var item:Item in listaItem)
		{
			obj = new Object();
			iep = new ItemEmpPreco();
			
			for each (var iept:ItemEmpPreco in App.single.cache.arrayItemEmpPreco)
			{
				if (iept.idItem != item.id || iept.idEmp != Sessao.unica.idEmp)
					continue;
				iep = iept.clone();
				break;
			}
			
			obj.item = item.clone();
			obj.iep = iep.clone();
			obj.cod = item.id;
			obj.nome = item.nome;
			obj.rfUnica = item.rfUnica;
			obj.rfAuxiliar = item.rfAuxiliar;
			obj.unidMed = item.unidMed;
			obj.venda = iep.venda;
			dpItem.addItem(obj);
		}
	}
	
	private function dgItem_KeyDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode == Keyboard.ENTER)
			btnSeleciona_Click();
	}
	
	private function btnSeleciona_Click():void
	{
		if (!dgItem.selectedItem)
			return;
		
		var idItem:Number = dgItem.selectedItem.item.id;
		importaItemSelecionado(idItem);
		mudaTela(telaEdita);
	}